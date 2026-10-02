using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using BaseLib.Patches.Saves;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Multiplayer.Serialization;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves;
using SpireTheRpg.Combat;
using SpireTheRpg.Relics;

namespace SpireTheRpg.Core
{
	// Token: 0x0200005A RID: 90
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgRunEquipmentPersistence
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x000181A0 File Offset: 0x000163A0
		public static void Register()
		{
			if (RpgRunEquipmentPersistence._registered)
			{
				return;
			}
			RpgRunEquipmentPersistence._registered = true;
			try
			{
				ExtendedSaveHandlers<IRunState, SerializableRun>.RegisterSave<string>("SpireTheRpg_RunEquipment", (IRunState _) => RpgRunEquipmentPersistence.CaptureJson(), delegate(IRunState _, [Nullable(2)] string json)
				{
					RpgRunEquipmentPersistence.RestoreJson(json);
				}, delegate(string json, [Nullable(1)] PacketWriter writer)
				{
					writer.WriteString(json ?? "");
				}, ([Nullable(1)] PacketReader reader) => reader.ReadString() ?? "");
				SpireTheRpgMainFile.Logger.Info("RpgRunEquipmentPersistence registered on SerializableRun", 1);
			}
			catch (Exception value)
			{
				Logger logger = SpireTheRpgMainFile.Logger;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 1);
				defaultInterpolatedStringHandler.AppendLiteral("RpgRunEquipmentPersistence register failed: ");
				defaultInterpolatedStringHandler.AppendFormatted<Exception>(value);
				logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00018298 File Offset: 0x00016498
		public static string CaptureJson()
		{
			string result;
			try
			{
				RpgPendingExtraRewards.RefreshFromOpenCombatRooms();
				RpgRunEquipmentAllDto rpgRunEquipmentAllDto = new RpgRunEquipmentAllDto();
				Dictionary<ulong, List<RpgPendingExtraRewardEntry>> dictionary = RpgPendingExtraRewards.SnapshotAll();
				foreach (KeyValuePair<ulong, RpgRunLoadout.StoreSnapshot> keyValuePair in RpgRunLoadout.SnapshotAll())
				{
					ulong num;
					RpgRunLoadout.StoreSnapshot storeSnapshot;
					keyValuePair.Deconstruct(out num, out storeSnapshot);
					ulong num2 = num;
					RpgRunLoadout.StoreSnapshot storeSnapshot2 = storeSnapshot;
					rpgRunEquipmentAllDto.Players[num2.ToString()] = new RpgRunEquipmentDto
					{
						BagItems = storeSnapshot2.BagItems,
						EquippedItems = storeSnapshot2.EquippedItems,
						PendingLoot = new List<RpgItemData>(),
						Scrolls = RpgScrollBag.Snapshot(num2),
						PendingExtraRewards = (dictionary.GetValueOrDefault(num2) ?? new List<RpgPendingExtraRewardEntry>())
					};
				}
				foreach (KeyValuePair<ulong, List<RpgItemData>> keyValuePair2 in RpgRunLoot.SnapshotAll())
				{
					ulong num;
					List<RpgItemData> list;
					keyValuePair2.Deconstruct(out num, out list);
					ulong num3 = num;
					List<RpgItemData> pendingLoot = list;
					string key = num3.ToString();
					RpgRunEquipmentDto rpgRunEquipmentDto;
					if (!rpgRunEquipmentAllDto.Players.TryGetValue(key, out rpgRunEquipmentDto))
					{
						rpgRunEquipmentDto = new RpgRunEquipmentDto();
						rpgRunEquipmentAllDto.Players[key] = rpgRunEquipmentDto;
					}
					rpgRunEquipmentDto.PendingLoot = pendingLoot;
				}
				foreach (KeyValuePair<ulong, List<RpgPendingExtraRewardEntry>> keyValuePair3 in dictionary)
				{
					ulong num;
					List<RpgPendingExtraRewardEntry> list2;
					keyValuePair3.Deconstruct(out num, out list2);
					ulong num4 = num;
					List<RpgPendingExtraRewardEntry> pendingExtraRewards = list2;
					string key2 = num4.ToString();
					RpgRunEquipmentDto rpgRunEquipmentDto2;
					if (!rpgRunEquipmentAllDto.Players.TryGetValue(key2, out rpgRunEquipmentDto2))
					{
						rpgRunEquipmentDto2 = new RpgRunEquipmentDto();
						rpgRunEquipmentAllDto.Players[key2] = rpgRunEquipmentDto2;
					}
					if (rpgRunEquipmentDto2.PendingExtraRewards.Count == 0)
					{
						rpgRunEquipmentDto2.PendingExtraRewards = pendingExtraRewards;
					}
				}
				string str = string.Join(" | ", rpgRunEquipmentAllDto.Players.Select(delegate(KeyValuePair<string, RpgRunEquipmentDto> kv)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 2);
					defaultInterpolatedStringHandler.AppendFormatted(kv.Key);
					defaultInterpolatedStringHandler.AppendLiteral(":extras=");
					defaultInterpolatedStringHandler.AppendFormatted<int>(kv.Value.PendingExtraRewards.Count);
					return defaultInterpolatedStringHandler.ToStringAndClear();
				}));
				SpireTheRpgMainFile.Logger.Info("RpgRunEquipmentPersistence capture " + str, 1);
				result = JsonSerializer.Serialize<RpgRunEquipmentAllDto>(rpgRunEquipmentAllDto, RpgRunEquipmentPersistence.JsonOptions);
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RpgRunEquipmentPersistence capture failed: " + ex.Message, 1);
				result = "";
			}
			return result;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00018540 File Offset: 0x00016740
		[NullableContext(2)]
		public static void RestoreJson(string json)
		{
			try
			{
				RpgRunLoadout.Clear();
				RpgRunLoot.Clear();
				RpgScrollBag.Clear();
				RpgPendingExtraRewards.Clear();
				if (string.IsNullOrWhiteSpace(json))
				{
					SpireTheRpgMainFile.Logger.Info("RpgRunEquipmentPersistence restore empty", 1);
				}
				else
				{
					RpgRunEquipmentAllDto rpgRunEquipmentAllDto = JsonSerializer.Deserialize<RpgRunEquipmentAllDto>(json, RpgRunEquipmentPersistence.JsonOptions);
					if (rpgRunEquipmentAllDto != null)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
						if (rpgRunEquipmentAllDto.Players.Count > 0)
						{
							Dictionary<string, List<RpgPendingExtraRewardEntry>> dictionary = new Dictionary<string, List<RpgPendingExtraRewardEntry>>(StringComparer.Ordinal);
							foreach (KeyValuePair<string, RpgRunEquipmentDto> keyValuePair in rpgRunEquipmentAllDto.Players)
							{
								string text;
								RpgRunEquipmentDto rpgRunEquipmentDto;
								keyValuePair.Deconstruct(out text, out rpgRunEquipmentDto);
								string text2 = text;
								RpgRunEquipmentDto rpgRunEquipmentDto2 = rpgRunEquipmentDto;
								ulong num;
								if (ulong.TryParse(text2, out num))
								{
									RpgRunLoadout.LoadFromDto(num, rpgRunEquipmentDto2);
									RpgRunLoot.LoadFromList(num, rpgRunEquipmentDto2.PendingLoot);
									RpgScrollBag.Load(num, rpgRunEquipmentDto2.Scrolls);
									List<RpgPendingExtraRewardEntry> list = rpgRunEquipmentDto2.PendingExtraRewards ?? new List<RpgPendingExtraRewardEntry>();
									dictionary[text2] = list;
									int value = rpgRunEquipmentDto2.EquippedItems.Count((KeyValuePair<string, RpgItemData> kv) => kv.Value != null);
									Logger logger = SpireTheRpgMainFile.Logger;
									defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(75, 6);
									defaultInterpolatedStringHandler.AppendLiteral("RpgRunEquipmentPersistence player=");
									defaultInterpolatedStringHandler.AppendFormatted<ulong>(num);
									defaultInterpolatedStringHandler.AppendLiteral(" bag=");
									defaultInterpolatedStringHandler.AppendFormatted<int>(rpgRunEquipmentDto2.BagItems.Count);
									defaultInterpolatedStringHandler.AppendLiteral(" equipped=");
									defaultInterpolatedStringHandler.AppendFormatted<int>(value);
									defaultInterpolatedStringHandler.AppendLiteral(" pending=");
									defaultInterpolatedStringHandler.AppendFormatted<int>(rpgRunEquipmentDto2.PendingLoot.Count);
									defaultInterpolatedStringHandler.AppendLiteral(" scrolls=");
									defaultInterpolatedStringHandler.AppendFormatted<int>(rpgRunEquipmentDto2.Scrolls.Count);
									defaultInterpolatedStringHandler.AppendLiteral(" extras=");
									defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
									logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
								}
							}
							RpgPendingExtraRewards.LoadAll(dictionary);
						}
						else
						{
							RpgRunEquipmentDto rpgRunEquipmentDto3 = new RpgRunEquipmentDto();
							rpgRunEquipmentDto3.BagItems = rpgRunEquipmentAllDto.BagItems;
							rpgRunEquipmentDto3.EquippedItems = rpgRunEquipmentAllDto.EquippedItems;
							rpgRunEquipmentDto3.PendingLoot = rpgRunEquipmentAllDto.PendingLoot;
							RpgRunLoadout.LoadFromDto(rpgRunEquipmentDto3);
							RpgRunLoot.LoadFromList(rpgRunEquipmentDto3.PendingLoot);
						}
						Logger logger2 = SpireTheRpgMainFile.Logger;
						defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(69, 3);
						defaultInterpolatedStringHandler.AppendLiteral("RpgRunEquipmentPersistence restored players=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(rpgRunEquipmentAllDto.Players.Count);
						defaultInterpolatedStringHandler.AppendLiteral(" localBag=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(RpgRunLoadout.BagCount);
						defaultInterpolatedStringHandler.AppendLiteral(" localEquipped=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(RpgRunLoadout.EquippedItems().Count<RpgItemData>());
						logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
						RpgRunLoadout.RebindLocalNetId();
						RpgRunLoot.RebindLocalNetId();
						RpgPendingExtraRewards.RebindLocalNetId();
						try
						{
							RunManager instance = RunManager.Instance;
							PropertyInfo propertyInfo = AccessTools.Property(typeof(RunManager), "State");
							RunState runState = ((propertyInfo != null) ? propertyInfo.GetValue(instance) : null) as RunState;
							if (runState != null)
							{
								foreach (Player player in runState.Players)
								{
									RpgItemRelic relic = player.GetRelic<RpgItemRelic>();
									if (relic != null)
									{
										relic.SyncAmountPublic();
									}
								}
							}
						}
						catch
						{
						}
					}
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RpgRunEquipmentPersistence restore failed: " + ex.Message, 1);
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000188EC File Offset: 0x00016AEC
		// Note: this type is marked as 'beforefieldinit'.
		static RpgRunEquipmentPersistence()
		{
		}

		// Token: 0x0400014B RID: 331
		private const string SaveId = "SpireTheRpg_RunEquipment";

		// Token: 0x0400014C RID: 332
		private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			Converters = 
			{
				new JsonStringEnumConverter()
			}
		};

		// Token: 0x0400014D RID: 333
		private static bool _registered;

		// Token: 0x02000121 RID: 289
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060007C3 RID: 1987 RVA: 0x00028E3D File Offset: 0x0002703D
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060007C4 RID: 1988 RVA: 0x00028E49 File Offset: 0x00027049
			public <>c()
			{
			}

			// Token: 0x060007C5 RID: 1989 RVA: 0x00028E51 File Offset: 0x00027051
			[return: Nullable(2)]
			internal string <Register>b__3_0(IRunState _)
			{
				return RpgRunEquipmentPersistence.CaptureJson();
			}

			// Token: 0x060007C6 RID: 1990 RVA: 0x00028E58 File Offset: 0x00027058
			internal void <Register>b__3_1(IRunState _, [Nullable(2)] string json)
			{
				RpgRunEquipmentPersistence.RestoreJson(json);
			}

			// Token: 0x060007C7 RID: 1991 RVA: 0x00028E60 File Offset: 0x00027060
			[NullableContext(0)]
			internal void <Register>b__3_2(string json, [Nullable(1)] PacketWriter writer)
			{
				writer.WriteString(json ?? "");
			}

			// Token: 0x060007C8 RID: 1992 RVA: 0x00028E72 File Offset: 0x00027072
			[NullableContext(0)]
			internal string <Register>b__3_3([Nullable(1)] PacketReader reader)
			{
				return reader.ReadString() ?? "";
			}

			// Token: 0x060007C9 RID: 1993 RVA: 0x00028E84 File Offset: 0x00027084
			[NullableContext(0)]
			internal string <CaptureJson>b__4_0(KeyValuePair<string, RpgRunEquipmentDto> kv)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 2);
				defaultInterpolatedStringHandler.AppendFormatted(kv.Key);
				defaultInterpolatedStringHandler.AppendLiteral(":extras=");
				defaultInterpolatedStringHandler.AppendFormatted<int>(kv.Value.PendingExtraRewards.Count);
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}

			// Token: 0x060007CA RID: 1994 RVA: 0x00028ED3 File Offset: 0x000270D3
			[NullableContext(0)]
			internal bool <RestoreJson>b__5_0(KeyValuePair<string, RpgItemData> kv)
			{
				return kv.Value != null;
			}

			// Token: 0x04000435 RID: 1077
			[Nullable(0)]
			public static readonly RpgRunEquipmentPersistence.<>c <>9 = new RpgRunEquipmentPersistence.<>c();

			// Token: 0x04000436 RID: 1078
			[Nullable(new byte[]
			{
				0,
				1,
				2
			})]
			public static Func<IRunState, string> <>9__3_0;

			// Token: 0x04000437 RID: 1079
			[Nullable(new byte[]
			{
				0,
				1,
				2
			})]
			public static Action<IRunState, string> <>9__3_1;

			// Token: 0x04000438 RID: 1080
			[Nullable(new byte[]
			{
				0,
				0,
				1
			})]
			public static Action<string, PacketWriter> <>9__3_2;

			// Token: 0x04000439 RID: 1081
			[Nullable(new byte[]
			{
				0,
				1,
				0
			})]
			public static Func<PacketReader, string> <>9__3_3;

			// Token: 0x0400043A RID: 1082
			[Nullable(0)]
			public static Func<KeyValuePair<string, RpgRunEquipmentDto>, string> <>9__4_0;

			// Token: 0x0400043B RID: 1083
			[Nullable(0)]
			public static Func<KeyValuePair<string, RpgItemData>, bool> <>9__5_0;
		}
	}
}
