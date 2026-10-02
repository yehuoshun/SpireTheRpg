using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using SpireTheRpg.Core;

namespace SpireTheRpg.Combat
{
	// Token: 0x02000074 RID: 116
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgPendingExtraRewards
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x0001C08D File Offset: 0x0001A28D
		public static void Clear()
		{
			RpgPendingExtraRewards.ByNetId.Clear();
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001C099 File Offset: 0x0001A299
		public static void Clear(ulong netId)
		{
			RpgPendingExtraRewards.ByNetId.Remove(netId);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001C0A8 File Offset: 0x0001A2A8
		public static Dictionary<ulong, List<RpgPendingExtraRewardEntry>> SnapshotAll()
		{
			Dictionary<ulong, List<RpgPendingExtraRewardEntry>> dictionary = new Dictionary<ulong, List<RpgPendingExtraRewardEntry>>();
			foreach (KeyValuePair<ulong, List<RpgPendingExtraRewardEntry>> keyValuePair in RpgPendingExtraRewards.ByNetId)
			{
				ulong num;
				List<RpgPendingExtraRewardEntry> list;
				keyValuePair.Deconstruct(out num, out list);
				ulong num2 = num;
				List<RpgPendingExtraRewardEntry> list2 = list;
				Dictionary<ulong, List<RpgPendingExtraRewardEntry>> dictionary2 = dictionary;
				ulong key = num2;
				IEnumerable<RpgPendingExtraRewardEntry> source = list2;
				Func<RpgPendingExtraRewardEntry, RpgPendingExtraRewardEntry> selector;
				if ((selector = RpgPendingExtraRewards.<>O.<0>__CloneEntry) == null)
				{
					selector = (RpgPendingExtraRewards.<>O.<0>__CloneEntry = new Func<RpgPendingExtraRewardEntry, RpgPendingExtraRewardEntry>(RpgPendingExtraRewards.CloneEntry));
				}
				dictionary2[key] = source.Select(selector).ToList<RpgPendingExtraRewardEntry>();
			}
			return dictionary;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001C13C File Offset: 0x0001A33C
		public static void LoadAll([Nullable(new byte[]
		{
			2,
			1,
			1,
			1
		})] Dictionary<string, List<RpgPendingExtraRewardEntry>> players)
		{
			RpgPendingExtraRewards.ByNetId.Clear();
			if (players == null)
			{
				return;
			}
			foreach (KeyValuePair<string, List<RpgPendingExtraRewardEntry>> keyValuePair in players)
			{
				string text;
				List<RpgPendingExtraRewardEntry> list;
				keyValuePair.Deconstruct(out text, out list);
				string s = text;
				List<RpgPendingExtraRewardEntry> list2 = list;
				ulong num;
				if (ulong.TryParse(s, out num) && list2 != null)
				{
					Dictionary<ulong, List<RpgPendingExtraRewardEntry>> byNetId = RpgPendingExtraRewards.ByNetId;
					ulong key = num;
					IEnumerable<RpgPendingExtraRewardEntry> source = from x in list2
					where x.Item != null && !string.IsNullOrWhiteSpace(x.Kind)
					select x;
					Func<RpgPendingExtraRewardEntry, RpgPendingExtraRewardEntry> selector;
					if ((selector = RpgPendingExtraRewards.<>O.<0>__CloneEntry) == null)
					{
						selector = (RpgPendingExtraRewards.<>O.<0>__CloneEntry = new Func<RpgPendingExtraRewardEntry, RpgPendingExtraRewardEntry>(RpgPendingExtraRewards.CloneEntry));
					}
					byNetId[key] = source.Select(selector).ToList<RpgPendingExtraRewardEntry>();
					Logger logger = SpireTheRpgMainFile.Logger;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 3);
					defaultInterpolatedStringHandler.AppendLiteral("RpgPendingExtraRewards load net=");
					defaultInterpolatedStringHandler.AppendFormatted<ulong>(num);
					defaultInterpolatedStringHandler.AppendLiteral(" count=");
					defaultInterpolatedStringHandler.AppendFormatted<int>(RpgPendingExtraRewards.ByNetId[num].Count);
					defaultInterpolatedStringHandler.AppendLiteral(" ids=");
					defaultInterpolatedStringHandler.AppendFormatted(RpgPendingExtraRewards.Summarize(RpgPendingExtraRewards.ByNetId[num]));
					logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
				}
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001C294 File Offset: 0x0001A494
		public static void CaptureFromRoom(Player player, CombatRoom room)
		{
			List<Reward> list;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			if (!room.ExtraRewards.TryGetValue(player, out list) || list == null)
			{
				RpgPendingExtraRewards.Clear(player.NetId);
				Logger logger = SpireTheRpgMainFile.Logger;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(41, 1);
				defaultInterpolatedStringHandler.AppendLiteral("RpgPendingExtraRewards capture empty net=");
				defaultInterpolatedStringHandler.AppendFormatted<ulong>(player.NetId);
				logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
				return;
			}
			List<RpgPendingExtraRewardEntry> list2 = new List<RpgPendingExtraRewardEntry>();
			foreach (Reward reward in list)
			{
				RpgEquipmentReward rpgEquipmentReward = reward as RpgEquipmentReward;
				if (rpgEquipmentReward == null)
				{
					RpgScrollReward rpgScrollReward = reward as RpgScrollReward;
					if (rpgScrollReward != null)
					{
						string text = rpgScrollReward.ExportSaveJson(SharedJson.Options);
						if (!string.IsNullOrWhiteSpace(text))
						{
							RpgItemData rpgItemData = JsonSerializer.Deserialize<RpgItemData>(text, SharedJson.Options);
							if (rpgItemData != null)
							{
								list2.Add(new RpgPendingExtraRewardEntry
								{
									Kind = "sc",
									Item = rpgItemData.Clone()
								});
							}
						}
					}
				}
				else
				{
					string text2 = rpgEquipmentReward.ExportSaveJson(SharedJson.Options);
					if (string.IsNullOrWhiteSpace(text2))
					{
						Logger logger2 = SpireTheRpgMainFile.Logger;
						defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 1);
						defaultInterpolatedStringHandler.AppendLiteral("RpgPendingExtraRewards skip empty equipment net=");
						defaultInterpolatedStringHandler.AppendFormatted<ulong>(player.NetId);
						logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
					}
					else
					{
						RpgEquipmentRewardSave rpgEquipmentRewardSave = JsonSerializer.Deserialize<RpgEquipmentRewardSave>(text2, SharedJson.Options);
						if (((rpgEquipmentRewardSave != null) ? rpgEquipmentRewardSave.Item : null) != null)
						{
							list2.Add(new RpgPendingExtraRewardEntry
							{
								Kind = "eq",
								RoomType = rpgEquipmentRewardSave.RoomType,
								ForcedRarity = rpgEquipmentRewardSave.ForcedRarity,
								Item = rpgEquipmentRewardSave.Item.Clone()
							});
						}
					}
				}
			}
			RpgPendingExtraRewards.ByNetId[player.NetId] = list2;
			Logger logger3 = SpireTheRpgMainFile.Logger;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 3);
			defaultInterpolatedStringHandler.AppendLiteral("RpgPendingExtraRewards capture net=");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(player.NetId);
			defaultInterpolatedStringHandler.AppendLiteral(" count=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(list2.Count);
			defaultInterpolatedStringHandler.AppendLiteral(" ids=");
			defaultInterpolatedStringHandler.AppendFormatted(RpgPendingExtraRewards.Summarize(list2));
			logger3.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001C4E4 File Offset: 0x0001A6E4
		public static void RememberEquipment(ulong netId, int roomType, int forcedRarity, RpgItemData item)
		{
			List<RpgPendingExtraRewardEntry> list = RpgPendingExtraRewards.ListOf(netId);
			list.RemoveAll((RpgPendingExtraRewardEntry x) => x.Kind == "eq");
			list.Insert(0, new RpgPendingExtraRewardEntry
			{
				Kind = "eq",
				RoomType = roomType,
				ForcedRarity = forcedRarity,
				Item = item.Clone()
			});
			SpireTheRpgMainFile.Logger.Info("RPG_PENDING_REMEMBER_EQ " + item.Id, 1);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001C568 File Offset: 0x0001A768
		public static void RememberScroll(ulong netId, RpgItemData scroll)
		{
			List<RpgPendingExtraRewardEntry> list = RpgPendingExtraRewards.ListOf(netId);
			list.RemoveAll((RpgPendingExtraRewardEntry x) => x.Kind == "sc");
			list.Add(new RpgPendingExtraRewardEntry
			{
				Kind = "sc",
				Item = scroll.Clone()
			});
			SpireTheRpgMainFile.Logger.Info("RPG_PENDING_REMEMBER_SC " + scroll.Id, 1);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001C5E0 File Offset: 0x0001A7E0
		[NullableContext(2)]
		public static void RemoveEquipment(ulong netId, string instanceId)
		{
			List<RpgPendingExtraRewardEntry> list;
			if (!RpgPendingExtraRewards.ByNetId.TryGetValue(netId, out list))
			{
				return;
			}
			list.RemoveAll(delegate(RpgPendingExtraRewardEntry x)
			{
				if (!(x.Kind == "eq"))
				{
					return false;
				}
				if (!string.IsNullOrEmpty(instanceId))
				{
					RpgItemData item = x.Item;
					return string.Equals((item != null) ? item.InstanceId : null, instanceId, StringComparison.Ordinal);
				}
				return true;
			});
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001C620 File Offset: 0x0001A820
		[NullableContext(2)]
		public static void RemoveScroll(ulong netId, string instanceId)
		{
			List<RpgPendingExtraRewardEntry> list;
			if (!RpgPendingExtraRewards.ByNetId.TryGetValue(netId, out list))
			{
				return;
			}
			list.RemoveAll(delegate(RpgPendingExtraRewardEntry x)
			{
				if (!(x.Kind == "sc"))
				{
					return false;
				}
				if (!string.IsNullOrEmpty(instanceId))
				{
					RpgItemData item = x.Item;
					return string.Equals((item != null) ? item.InstanceId : null, instanceId, StringComparison.Ordinal);
				}
				return true;
			});
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001C65D File Offset: 0x0001A85D
		public static void RefreshFromOpenCombatRooms()
		{
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001C660 File Offset: 0x0001A860
		private static List<RpgPendingExtraRewardEntry> ListOf(ulong netId)
		{
			List<RpgPendingExtraRewardEntry> list;
			if (!RpgPendingExtraRewards.ByNetId.TryGetValue(netId, out list))
			{
				list = new List<RpgPendingExtraRewardEntry>();
				RpgPendingExtraRewards.ByNetId[netId] = list;
			}
			return list;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001C690 File Offset: 0x0001A890
		public static bool TryApplyEquipment(ulong netId, out int roomType, out int forcedRarity, out RpgItemData item)
		{
			if (RpgPendingExtraRewards.TryPeekEquipmentExact(netId, out roomType, out forcedRarity, out item))
			{
				return true;
			}
			roomType = 0;
			forcedRarity = -1;
			item = null;
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 2);
			defaultInterpolatedStringHandler.AppendLiteral("RpgPendingExtraRewards apply eq MISS net=");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(netId);
			defaultInterpolatedStringHandler.AppendLiteral(" maps=");
			defaultInterpolatedStringHandler.AppendFormatted(RpgPendingExtraRewards.MapsSummary());
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			return false;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001C6FC File Offset: 0x0001A8FC
		private static bool TryPeekEquipmentExact(ulong netId, out int roomType, out int forcedRarity, out RpgItemData item)
		{
			roomType = 0;
			forcedRarity = -1;
			item = null;
			List<RpgPendingExtraRewardEntry> list;
			if (!RpgPendingExtraRewards.ByNetId.TryGetValue(netId, out list))
			{
				return false;
			}
			int num = list.FindIndex((RpgPendingExtraRewardEntry x) => x.Kind == "eq" && x.Item != null);
			if (num < 0)
			{
				return false;
			}
			RpgPendingExtraRewardEntry rpgPendingExtraRewardEntry = list[num];
			roomType = rpgPendingExtraRewardEntry.RoomType;
			forcedRarity = rpgPendingExtraRewardEntry.ForcedRarity;
			item = rpgPendingExtraRewardEntry.Item.Clone();
			item.SyncVisualFromTemplate();
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(50, 3);
			defaultInterpolatedStringHandler.AppendLiteral("RpgPendingExtraRewards apply eq HIT net=");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(netId);
			defaultInterpolatedStringHandler.AppendLiteral(" id=");
			defaultInterpolatedStringHandler.AppendFormatted(item.Id);
			defaultInterpolatedStringHandler.AppendLiteral(" kept=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			return true;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001C7E0 File Offset: 0x0001A9E0
		public static bool TryApplyScroll(ulong netId, out RpgItemData scroll)
		{
			if (RpgPendingExtraRewards.TryPeekScrollExact(netId, out scroll))
			{
				return true;
			}
			scroll = null;
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 2);
			defaultInterpolatedStringHandler.AppendLiteral("RpgPendingExtraRewards apply sc MISS net=");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(netId);
			defaultInterpolatedStringHandler.AppendLiteral(" maps=");
			defaultInterpolatedStringHandler.AppendFormatted(RpgPendingExtraRewards.MapsSummary());
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			return false;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001C844 File Offset: 0x0001AA44
		private static bool TryPeekScrollExact(ulong netId, out RpgItemData scroll)
		{
			scroll = null;
			List<RpgPendingExtraRewardEntry> list;
			if (!RpgPendingExtraRewards.ByNetId.TryGetValue(netId, out list))
			{
				return false;
			}
			int num = list.FindIndex((RpgPendingExtraRewardEntry x) => x.Kind == "sc" && x.Item != null);
			if (num < 0)
			{
				return false;
			}
			RpgPendingExtraRewardEntry rpgPendingExtraRewardEntry = list[num];
			scroll = rpgPendingExtraRewardEntry.Item.Clone();
			scroll.IsScroll = true;
			scroll.SyncVisualFromTemplate();
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(50, 3);
			defaultInterpolatedStringHandler.AppendLiteral("RpgPendingExtraRewards apply sc HIT net=");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(netId);
			defaultInterpolatedStringHandler.AppendLiteral(" id=");
			defaultInterpolatedStringHandler.AppendFormatted(scroll.Id);
			defaultInterpolatedStringHandler.AppendLiteral(" kept=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			return true;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001C919 File Offset: 0x0001AB19
		private static string MapsSummary()
		{
			return string.Join(";", RpgPendingExtraRewards.ByNetId.Select(delegate(KeyValuePair<ulong, List<RpgPendingExtraRewardEntry>> kv)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendFormatted<ulong>(kv.Key);
				defaultInterpolatedStringHandler.AppendLiteral("=[");
				defaultInterpolatedStringHandler.AppendFormatted(RpgPendingExtraRewards.Summarize(kv.Value));
				defaultInterpolatedStringHandler.AppendLiteral("]");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}));
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001C950 File Offset: 0x0001AB50
		public static void RebindLocalNetId()
		{
			ulong num = RpgNet.LocalNetId();
			if (RpgPendingExtraRewards.ByNetId.ContainsKey(num) && RpgPendingExtraRewards.ByNetId[num].Count > 0)
			{
				return;
			}
			if (RpgPendingExtraRewards.ByNetId.Count != 1)
			{
				return;
			}
			ulong num2;
			List<RpgPendingExtraRewardEntry> list;
			RpgPendingExtraRewards.ByNetId.First<KeyValuePair<ulong, List<RpgPendingExtraRewardEntry>>>().Deconstruct(out num2, out list);
			ulong num3 = num2;
			List<RpgPendingExtraRewardEntry> value = list;
			if (num3 == num)
			{
				return;
			}
			RpgPendingExtraRewards.ByNetId.Remove(num3);
			RpgPendingExtraRewards.ByNetId[num] = value;
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 2);
			defaultInterpolatedStringHandler.AppendLiteral("RpgPendingExtraRewards rebound ");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(num3);
			defaultInterpolatedStringHandler.AppendLiteral(" -> ");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(num);
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001CA10 File Offset: 0x0001AC10
		private static RpgPendingExtraRewardEntry CloneEntry(RpgPendingExtraRewardEntry x)
		{
			RpgPendingExtraRewardEntry rpgPendingExtraRewardEntry = new RpgPendingExtraRewardEntry();
			rpgPendingExtraRewardEntry.Kind = x.Kind;
			rpgPendingExtraRewardEntry.RoomType = x.RoomType;
			rpgPendingExtraRewardEntry.ForcedRarity = x.ForcedRarity;
			RpgItemData item = x.Item;
			rpgPendingExtraRewardEntry.Item = ((item != null) ? item.Clone() : null);
			return rpgPendingExtraRewardEntry;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001CA5E File Offset: 0x0001AC5E
		private static string Summarize(List<RpgPendingExtraRewardEntry> list)
		{
			return string.Join(",", list.Select(delegate(RpgPendingExtraRewardEntry x)
			{
				string kind = x.Kind;
				string str = ":";
				RpgItemData item = x.Item;
				return kind + str + (((item != null) ? item.Id : null) ?? "?");
			}));
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001CA8F File Offset: 0x0001AC8F
		// Note: this type is marked as 'beforefieldinit'.
		static RpgPendingExtraRewards()
		{
		}

		// Token: 0x040001AB RID: 427
		private static readonly Dictionary<ulong, List<RpgPendingExtraRewardEntry>> ByNetId = new Dictionary<ulong, List<RpgPendingExtraRewardEntry>>();

		// Token: 0x02000146 RID: 326
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040004C4 RID: 1220
			[Nullable(0)]
			public static Func<RpgPendingExtraRewardEntry, RpgPendingExtraRewardEntry> <0>__CloneEntry;
		}

		// Token: 0x02000147 RID: 327
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000840 RID: 2112 RVA: 0x0002B43E File Offset: 0x0002963E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000841 RID: 2113 RVA: 0x0002B44A File Offset: 0x0002964A
			public <>c()
			{
			}

			// Token: 0x06000842 RID: 2114 RVA: 0x0002B452 File Offset: 0x00029652
			[NullableContext(0)]
			internal bool <LoadAll>b__4_0(RpgPendingExtraRewardEntry x)
			{
				return x.Item != null && !string.IsNullOrWhiteSpace(x.Kind);
			}

			// Token: 0x06000843 RID: 2115 RVA: 0x0002B46C File Offset: 0x0002966C
			internal bool <RememberEquipment>b__6_0(RpgPendingExtraRewardEntry x)
			{
				return x.Kind == "eq";
			}

			// Token: 0x06000844 RID: 2116 RVA: 0x0002B47E File Offset: 0x0002967E
			internal bool <RememberScroll>b__7_0(RpgPendingExtraRewardEntry x)
			{
				return x.Kind == "sc";
			}

			// Token: 0x06000845 RID: 2117 RVA: 0x0002B490 File Offset: 0x00029690
			internal bool <TryPeekEquipmentExact>b__13_0(RpgPendingExtraRewardEntry x)
			{
				return x.Kind == "eq" && x.Item != null;
			}

			// Token: 0x06000846 RID: 2118 RVA: 0x0002B4AF File Offset: 0x000296AF
			internal bool <TryPeekScrollExact>b__15_0(RpgPendingExtraRewardEntry x)
			{
				return x.Kind == "sc" && x.Item != null;
			}

			// Token: 0x06000847 RID: 2119 RVA: 0x0002B4D0 File Offset: 0x000296D0
			[NullableContext(0)]
			internal string <MapsSummary>b__16_0(KeyValuePair<ulong, List<RpgPendingExtraRewardEntry>> kv)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendFormatted<ulong>(kv.Key);
				defaultInterpolatedStringHandler.AppendLiteral("=[");
				defaultInterpolatedStringHandler.AppendFormatted(RpgPendingExtraRewards.Summarize(kv.Value));
				defaultInterpolatedStringHandler.AppendLiteral("]");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}

			// Token: 0x06000848 RID: 2120 RVA: 0x0002B526 File Offset: 0x00029726
			[NullableContext(0)]
			internal string <Summarize>b__19_0(RpgPendingExtraRewardEntry x)
			{
				string kind = x.Kind;
				string str = ":";
				RpgItemData item = x.Item;
				return kind + str + (((item != null) ? item.Id : null) ?? "?");
			}

			// Token: 0x040004C5 RID: 1221
			[Nullable(0)]
			public static readonly RpgPendingExtraRewards.<>c <>9 = new RpgPendingExtraRewards.<>c();

			// Token: 0x040004C6 RID: 1222
			[Nullable(0)]
			public static Func<RpgPendingExtraRewardEntry, bool> <>9__4_0;

			// Token: 0x040004C7 RID: 1223
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Predicate<RpgPendingExtraRewardEntry> <>9__6_0;

			// Token: 0x040004C8 RID: 1224
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Predicate<RpgPendingExtraRewardEntry> <>9__7_0;

			// Token: 0x040004C9 RID: 1225
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Predicate<RpgPendingExtraRewardEntry> <>9__13_0;

			// Token: 0x040004CA RID: 1226
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Predicate<RpgPendingExtraRewardEntry> <>9__15_0;

			// Token: 0x040004CB RID: 1227
			[Nullable(0)]
			public static Func<KeyValuePair<ulong, List<RpgPendingExtraRewardEntry>>, string> <>9__16_0;

			// Token: 0x040004CC RID: 1228
			[Nullable(0)]
			public static Func<RpgPendingExtraRewardEntry, string> <>9__19_0;
		}

		// Token: 0x02000148 RID: 328
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x06000849 RID: 2121 RVA: 0x0002B553 File Offset: 0x00029753
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x0600084A RID: 2122 RVA: 0x0002B55C File Offset: 0x0002975C
			internal bool <RemoveEquipment>b__0(RpgPendingExtraRewardEntry x)
			{
				if (!(x.Kind == "eq"))
				{
					return false;
				}
				if (!string.IsNullOrEmpty(this.instanceId))
				{
					RpgItemData item = x.Item;
					return string.Equals((item != null) ? item.InstanceId : null, this.instanceId, StringComparison.Ordinal);
				}
				return true;
			}

			// Token: 0x040004CD RID: 1229
			[Nullable(0)]
			public string instanceId;
		}

		// Token: 0x02000149 RID: 329
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0
		{
			// Token: 0x0600084B RID: 2123 RVA: 0x0002B5AA File Offset: 0x000297AA
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x0600084C RID: 2124 RVA: 0x0002B5B4 File Offset: 0x000297B4
			internal bool <RemoveScroll>b__0(RpgPendingExtraRewardEntry x)
			{
				if (!(x.Kind == "sc"))
				{
					return false;
				}
				if (!string.IsNullOrEmpty(this.instanceId))
				{
					RpgItemData item = x.Item;
					return string.Equals((item != null) ? item.InstanceId : null, this.instanceId, StringComparison.Ordinal);
				}
				return true;
			}

			// Token: 0x040004CE RID: 1230
			[Nullable(0)]
			public string instanceId;
		}
	}
}
