using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;
using SpireTheRpg.Combat;

namespace SpireTheRpg.Patches
{
	// Token: 0x02000023 RID: 35
	[NullableContext(1)]
	[Nullable(0)]
	[HarmonyPatch]
	internal static class RpgExtraRewardSavePatches
	{
		// Token: 0x0600017E RID: 382 RVA: 0x0000E6C8 File Offset: 0x0000C8C8
		[HarmonyPatch(typeof(CombatRoom), "ToSerializable")]
		[HarmonyPostfix]
		private static void CaptureExtraRewards(CombatRoom __instance, SerializableRoom __result)
		{
			try
			{
				if (__result.EncounterState == null)
				{
					__result.EncounterState = new Dictionary<string, string>();
				}
				int num = 0;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				foreach (KeyValuePair<Player, List<Reward>> keyValuePair in __instance.ExtraRewards)
				{
					Player player;
					List<Reward> list;
					keyValuePair.Deconstruct(out player, out list);
					Player player2 = player;
					List<Reward> list2 = list;
					RpgPendingExtraRewards.CaptureFromRoom(player2, __instance);
					for (int i = 0; i < list2.Count; i++)
					{
						string text = RpgExtraRewardSavePatches.Export(list2[i]);
						if (string.IsNullOrWhiteSpace(text))
						{
							Logger logger = SpireTheRpgMainFile.Logger;
							defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 3);
							defaultInterpolatedStringHandler.AppendLiteral("RpgExtraRewardSave capture EMPTY net=");
							defaultInterpolatedStringHandler.AppendFormatted<ulong>(player2.NetId);
							defaultInterpolatedStringHandler.AppendLiteral(" idx=");
							defaultInterpolatedStringHandler.AppendFormatted<int>(i);
							defaultInterpolatedStringHandler.AppendLiteral(" type=");
							defaultInterpolatedStringHandler.AppendFormatted(list2[i].GetType().Name);
							logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
						}
						else
						{
							__result.EncounterState[RpgExtraRewardSavePatches.MakeKey(player2.NetId, i)] = text;
							num++;
							Logger logger2 = SpireTheRpgMainFile.Logger;
							defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 3);
							defaultInterpolatedStringHandler.AppendLiteral("RpgExtraRewardSave capture OK net=");
							defaultInterpolatedStringHandler.AppendFormatted<ulong>(player2.NetId);
							defaultInterpolatedStringHandler.AppendLiteral(" idx=");
							defaultInterpolatedStringHandler.AppendFormatted<int>(i);
							defaultInterpolatedStringHandler.AppendLiteral(" bytes=");
							defaultInterpolatedStringHandler.AppendFormatted<int>(text.Length);
							logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
						}
					}
				}
				Logger logger3 = SpireTheRpgMainFile.Logger;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 1);
				defaultInterpolatedStringHandler.AppendLiteral("RpgExtraRewardSave capture done wrote=");
				defaultInterpolatedStringHandler.AppendFormatted<int>(num);
				logger3.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			}
			catch (Exception value)
			{
				Logger logger4 = SpireTheRpgMainFile.Logger;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 1);
				defaultInterpolatedStringHandler.AppendLiteral("RpgExtraRewardSave capture failed: ");
				defaultInterpolatedStringHandler.AppendFormatted<Exception>(value);
				logger4.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000E904 File Offset: 0x0000CB04
		[HarmonyPatch(typeof(CombatRoom), "FromSerializable")]
		[HarmonyPostfix]
		private static void RestoreExtraRewards(SerializableRoom serializableRoom, CombatRoom __result)
		{
			try
			{
				Dictionary<string, string> encounterState = serializableRoom.EncounterState;
				int num;
				if (encounterState != null)
				{
					num = encounterState.Keys.Count((string k) => k.StartsWith("__spire_rpg_reward_", StringComparison.Ordinal));
				}
				else
				{
					num = 0;
				}
				int value = num;
				Logger logger = SpireTheRpgMainFile.Logger;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(58, 2);
				defaultInterpolatedStringHandler.AppendLiteral("RpgExtraRewardSave restore enter extrasPlayers=");
				defaultInterpolatedStringHandler.AppendFormatted<int>(__result.ExtraRewards.Count);
				defaultInterpolatedStringHandler.AppendLiteral(" stateKeys=");
				defaultInterpolatedStringHandler.AppendFormatted<int>(value);
				logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
				if (encounterState != null && encounterState.Count != 0)
				{
					foreach (KeyValuePair<Player, List<Reward>> keyValuePair in __result.ExtraRewards)
					{
						Player player;
						List<Reward> list;
						keyValuePair.Deconstruct(out player, out list);
						Player player2 = player;
						List<Reward> list2 = list;
						for (int i = 0; i < list2.Count; i++)
						{
							string text;
							if (!encounterState.TryGetValue(RpgExtraRewardSavePatches.MakeKey(player2.NetId, i), out text) || string.IsNullOrWhiteSpace(text))
							{
								Logger logger2 = SpireTheRpgMainFile.Logger;
								defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 3);
								defaultInterpolatedStringHandler.AppendLiteral("RpgExtraRewardSave restore MISS net=");
								defaultInterpolatedStringHandler.AppendFormatted<ulong>(player2.NetId);
								defaultInterpolatedStringHandler.AppendLiteral(" idx=");
								defaultInterpolatedStringHandler.AppendFormatted<int>(i);
								defaultInterpolatedStringHandler.AppendLiteral(" type=");
								defaultInterpolatedStringHandler.AppendFormatted(list2[i].GetType().Name);
								logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
							}
							else
							{
								RpgExtraRewardSavePatches.Import(list2[i], text);
								Logger logger3 = SpireTheRpgMainFile.Logger;
								defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 3);
								defaultInterpolatedStringHandler.AppendLiteral("RpgExtraRewardSave restore OK net=");
								defaultInterpolatedStringHandler.AppendFormatted<ulong>(player2.NetId);
								defaultInterpolatedStringHandler.AppendLiteral(" idx=");
								defaultInterpolatedStringHandler.AppendFormatted<int>(i);
								defaultInterpolatedStringHandler.AppendLiteral(" bytes=");
								defaultInterpolatedStringHandler.AppendFormatted<int>(text.Length);
								logger3.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
							}
						}
					}
				}
			}
			catch (Exception value2)
			{
				Logger logger4 = SpireTheRpgMainFile.Logger;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 1);
				defaultInterpolatedStringHandler.AppendLiteral("RpgExtraRewardSave restore failed: ");
				defaultInterpolatedStringHandler.AppendFormatted<Exception>(value2);
				logger4.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000EB78 File Offset: 0x0000CD78
		private static string MakeKey(ulong netId, int index)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 3);
			defaultInterpolatedStringHandler.AppendFormatted("__spire_rpg_reward_");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(netId);
			defaultInterpolatedStringHandler.AppendLiteral("_");
			defaultInterpolatedStringHandler.AppendFormatted<int>(index);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		[return: Nullable(2)]
		private static string Export(Reward reward)
		{
			RpgEquipmentReward rpgEquipmentReward = reward as RpgEquipmentReward;
			string result;
			if (rpgEquipmentReward == null)
			{
				RpgScrollReward rpgScrollReward = reward as RpgScrollReward;
				if (rpgScrollReward == null)
				{
					result = null;
				}
				else
				{
					result = RpgExtraRewardSavePatches.Prefixed("sc", rpgScrollReward.ExportSaveJson(RpgExtraRewardSavePatches.JsonOptions));
				}
			}
			else
			{
				result = RpgExtraRewardSavePatches.Prefixed("eq", rpgEquipmentReward.ExportSaveJson(RpgExtraRewardSavePatches.JsonOptions));
			}
			return result;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000EC16 File Offset: 0x0000CE16
		[return: Nullable(2)]
		private static string Prefixed(string kind, string json)
		{
			if (!string.IsNullOrWhiteSpace(json))
			{
				return kind + "|" + json;
			}
			return null;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000EC30 File Offset: 0x0000CE30
		private static void Import(Reward reward, string packed)
		{
			int num = packed.IndexOf('|');
			if (num <= 0 || num >= packed.Length - 1)
			{
				return;
			}
			string text = packed.Substring(0, num);
			int num2 = num + 1;
			string json = packed.Substring(num2, packed.Length - num2);
			string a = text;
			if (!(a == "eq"))
			{
				if (!(a == "sc"))
				{
					return;
				}
				RpgScrollReward rpgScrollReward = reward as RpgScrollReward;
				if (rpgScrollReward != null)
				{
					rpgScrollReward.ImportSaveJson(json, RpgExtraRewardSavePatches.JsonOptions);
				}
			}
			else
			{
				RpgEquipmentReward rpgEquipmentReward = reward as RpgEquipmentReward;
				if (rpgEquipmentReward != null)
				{
					rpgEquipmentReward.ImportSaveJson(json, RpgExtraRewardSavePatches.JsonOptions);
					return;
				}
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000ECC3 File Offset: 0x0000CEC3
		// Note: this type is marked as 'beforefieldinit'.
		static RpgExtraRewardSavePatches()
		{
		}

		// Token: 0x0400008A RID: 138
		private const string KeyPrefix = "__spire_rpg_reward_";

		// Token: 0x0400008B RID: 139
		private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			Converters = 
			{
				new JsonStringEnumConverter()
			}
		};

		// Token: 0x020000C9 RID: 201
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000699 RID: 1689 RVA: 0x0002241E File Offset: 0x0002061E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600069A RID: 1690 RVA: 0x0002242A File Offset: 0x0002062A
			public <>c()
			{
			}

			// Token: 0x0600069B RID: 1691 RVA: 0x00022432 File Offset: 0x00020632
			[NullableContext(0)]
			internal bool <RestoreExtraRewards>b__3_0(string k)
			{
				return k.StartsWith("__spire_rpg_reward_", StringComparison.Ordinal);
			}

			// Token: 0x040002C8 RID: 712
			[Nullable(0)]
			public static readonly RpgExtraRewardSavePatches.<>c <>9 = new RpgExtraRewardSavePatches.<>c();

			// Token: 0x040002C9 RID: 713
			[Nullable(0)]
			public static Func<string, bool> <>9__3_0;
		}
	}
}
