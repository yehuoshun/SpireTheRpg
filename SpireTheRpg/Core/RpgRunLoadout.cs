using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;

namespace SpireTheRpg.Core
{
	// Token: 0x0200005B RID: 91
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgRunLoadout
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x00018910 File Offset: 0x00016B10
		private static RpgRunLoadout.Store Of(ulong netId)
		{
			RpgRunLoadout.Store store;
			if (!RpgRunLoadout.ByNetId.TryGetValue(netId, out store))
			{
				store = new RpgRunLoadout.Store();
				RpgRunLoadout.ByNetId[netId] = store;
			}
			return store;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001893F File Offset: 0x00016B3F
		private static RpgRunLoadout.Store Local()
		{
			return RpgRunLoadout.Of(RpgNet.LocalNetId());
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0001894B File Offset: 0x00016B4B
		public static IReadOnlyList<RpgItemData> Bag
		{
			get
			{
				return RpgRunLoadout.Local().BagList;
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00018957 File Offset: 0x00016B57
		public static IReadOnlyList<RpgItemData> BagOf(ulong netId)
		{
			return RpgRunLoadout.Of(netId).BagList;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00018964 File Offset: 0x00016B64
		public static int BagCount
		{
			get
			{
				return RpgRunLoadout.Local().BagList.Count;
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00018975 File Offset: 0x00016B75
		public static int BagCountOf(ulong netId)
		{
			return RpgRunLoadout.Of(netId).BagList.Count;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00018987 File Offset: 0x00016B87
		[NullableContext(2)]
		public static RpgItemData GetEquipped(RpgEquipSlot slot)
		{
			return RpgRunLoadout.Local().Equipped.GetValueOrDefault(slot);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00018999 File Offset: 0x00016B99
		[NullableContext(2)]
		public static RpgItemData GetEquipped(ulong netId, RpgEquipSlot slot)
		{
			return RpgRunLoadout.Of(netId).Equipped.GetValueOrDefault(slot);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000189AC File Offset: 0x00016BAC
		public static IEnumerable<RpgItemData> EquippedItems()
		{
			return RpgRunLoadout.EquippedItemsOf(RpgNet.LocalNetId());
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000189B8 File Offset: 0x00016BB8
		public static IEnumerable<RpgItemData> EquippedItemsOf(ulong netId)
		{
			RpgRunLoadout.<EquippedItemsOf>d__13 <EquippedItemsOf>d__ = new RpgRunLoadout.<EquippedItemsOf>d__13(-2);
			<EquippedItemsOf>d__.<>3__netId = netId;
			return <EquippedItemsOf>d__;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000189C8 File Offset: 0x00016BC8
		public static void Clear()
		{
			RpgRunLoadout.ByNetId.Clear();
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000189D4 File Offset: 0x00016BD4
		public static void RebindLocalNetId()
		{
			ulong num = RpgNet.LocalNetId();
			if (RpgRunLoadout.ByNetId.ContainsKey(num) && RpgRunLoadout.BagCountOf(num) > 0)
			{
				return;
			}
			if (RpgRunLoadout.ByNetId.Count != 1)
			{
				return;
			}
			ulong num2;
			RpgRunLoadout.Store store;
			RpgRunLoadout.ByNetId.First<KeyValuePair<ulong, RpgRunLoadout.Store>>().Deconstruct(out num2, out store);
			ulong num3 = num2;
			RpgRunLoadout.Store value = store;
			if (num3 == num)
			{
				return;
			}
			RpgRunLoadout.ByNetId.Remove(num3);
			RpgRunLoadout.ByNetId[num] = value;
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 2);
			defaultInterpolatedStringHandler.AppendLiteral("RpgRunLoadout rebound ");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(num3);
			defaultInterpolatedStringHandler.AppendLiteral(" -> ");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(num);
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00018A8C File Offset: 0x00016C8C
		public static void Clear(ulong netId)
		{
			RpgRunLoadout.Store store;
			if (RpgRunLoadout.ByNetId.TryGetValue(netId, out store))
			{
				store.Clear();
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00018AAE File Offset: 0x00016CAE
		public static void LoadFromDto(RpgRunEquipmentDto dto)
		{
			RpgRunLoadout.LoadFromDto(RpgNet.LocalNetId(), dto);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00018ABC File Offset: 0x00016CBC
		public static void LoadFromDto(ulong netId, RpgRunEquipmentDto dto)
		{
			RpgRunLoadout.Store store = RpgRunLoadout.Of(netId);
			store.Clear();
			foreach (RpgItemData rpgItemData in dto.BagItems)
			{
				if (string.IsNullOrWhiteSpace(rpgItemData.InstanceId))
				{
					rpgItemData.InstanceId = Guid.NewGuid().ToString("N");
				}
				rpgItemData.SyncVisualFromTemplate();
				store.BagList.Add(rpgItemData.Clone());
			}
			foreach (KeyValuePair<string, RpgItemData> keyValuePair in dto.EquippedItems)
			{
				string text;
				RpgItemData rpgItemData2;
				keyValuePair.Deconstruct(out text, out rpgItemData2);
				string value = text;
				RpgItemData rpgItemData3 = rpgItemData2;
				RpgEquipSlot key;
				if (Enum.TryParse<RpgEquipSlot>(value, true, out key) && rpgItemData3 != null)
				{
					if (string.IsNullOrWhiteSpace(rpgItemData3.InstanceId))
					{
						rpgItemData3.InstanceId = Guid.NewGuid().ToString("N");
					}
					rpgItemData3.SyncVisualFromTemplate();
					store.Equipped[key] = rpgItemData3.Clone();
				}
			}
			RpgItemRoller.EnsureUniqueInstanceIds(store.BagList.Concat(store.Equipped.Values.OfType<RpgItemData>()));
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00018C10 File Offset: 0x00016E10
		public static void LoadEquippedFromDto(ulong netId, RpgRunEquipmentDto dto)
		{
			RpgRunLoadout.Store store = RpgRunLoadout.Of(netId);
			foreach (RpgEquipSlot key in Enum.GetValues<RpgEquipSlot>())
			{
				store.Equipped[key] = null;
			}
			foreach (KeyValuePair<string, RpgItemData> keyValuePair in dto.EquippedItems)
			{
				string text;
				RpgItemData rpgItemData;
				keyValuePair.Deconstruct(out text, out rpgItemData);
				string value = text;
				RpgItemData rpgItemData2 = rpgItemData;
				RpgEquipSlot key2;
				if (Enum.TryParse<RpgEquipSlot>(value, true, out key2) && rpgItemData2 != null)
				{
					if (string.IsNullOrWhiteSpace(rpgItemData2.InstanceId))
					{
						rpgItemData2.InstanceId = Guid.NewGuid().ToString("N");
					}
					rpgItemData2.SyncVisualFromTemplate();
					store.Equipped[key2] = rpgItemData2.Clone();
				}
			}
			RpgItemRoller.EnsureUniqueInstanceIds(store.BagList.Concat(store.Equipped.Values.OfType<RpgItemData>()));
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00018D0C File Offset: 0x00016F0C
		public static RpgRunEquipmentDto CaptureEquippedDto(ulong netId)
		{
			RpgRunLoadout.Store store = RpgRunLoadout.Of(netId);
			RpgRunEquipmentDto rpgRunEquipmentDto = new RpgRunEquipmentDto();
			rpgRunEquipmentDto.BagItems = new List<RpgItemData>();
			rpgRunEquipmentDto.EquippedItems = Enum.GetValues<RpgEquipSlot>().ToDictionary((RpgEquipSlot s) => s.ToString(), delegate(RpgEquipSlot s)
			{
				RpgItemData valueOrDefault = store.Equipped.GetValueOrDefault(s);
				if (valueOrDefault == null)
				{
					return null;
				}
				return valueOrDefault.Clone();
			}, StringComparer.OrdinalIgnoreCase);
			rpgRunEquipmentDto.PendingLoot = new List<RpgItemData>();
			rpgRunEquipmentDto.Scrolls = new List<RpgItemData>();
			return rpgRunEquipmentDto;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00018D94 File Offset: 0x00016F94
		public static RpgRunEquipmentDto CaptureDto(ulong netId)
		{
			RpgRunLoadout.Store store = RpgRunLoadout.Of(netId);
			RpgRunEquipmentDto rpgRunEquipmentDto = new RpgRunEquipmentDto();
			rpgRunEquipmentDto.BagItems = (from x in store.BagList
			select x.Clone()).ToList<RpgItemData>();
			rpgRunEquipmentDto.EquippedItems = Enum.GetValues<RpgEquipSlot>().ToDictionary((RpgEquipSlot s) => s.ToString(), delegate(RpgEquipSlot s)
			{
				RpgItemData valueOrDefault = store.Equipped.GetValueOrDefault(s);
				if (valueOrDefault == null)
				{
					return null;
				}
				return valueOrDefault.Clone();
			}, StringComparer.OrdinalIgnoreCase);
			rpgRunEquipmentDto.PendingLoot = new List<RpgItemData>();
			return rpgRunEquipmentDto;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00018E40 File Offset: 0x00017040
		public static IReadOnlyDictionary<ulong, RpgRunLoadout.StoreSnapshot> SnapshotAll()
		{
			Dictionary<ulong, RpgRunLoadout.StoreSnapshot> dictionary = new Dictionary<ulong, RpgRunLoadout.StoreSnapshot>();
			foreach (KeyValuePair<ulong, RpgRunLoadout.Store> keyValuePair in RpgRunLoadout.ByNetId)
			{
				ulong num;
				RpgRunLoadout.Store store2;
				keyValuePair.Deconstruct(out num, out store2);
				ulong key = num;
				RpgRunLoadout.Store store = store2;
				dictionary[key] = new RpgRunLoadout.StoreSnapshot((from x in store.BagList
				select x.Clone()).ToList<RpgItemData>(), Enum.GetValues<RpgEquipSlot>().ToDictionary((RpgEquipSlot s) => s.ToString(), delegate(RpgEquipSlot s)
				{
					RpgItemData valueOrDefault = store.Equipped.GetValueOrDefault(s);
					if (valueOrDefault == null)
					{
						return null;
					}
					return valueOrDefault.Clone();
				}, StringComparer.OrdinalIgnoreCase));
			}
			return dictionary;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00018F30 File Offset: 0x00017130
		public static void AddToBag(RpgItemData item)
		{
			RpgRunLoadout.AddToBag(RpgNet.LocalNetId(), item);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00018F40 File Offset: 0x00017140
		public static void AddToBag(ulong netId, RpgItemData item)
		{
			RpgRunLoadout.Store store = RpgRunLoadout.Of(netId);
			RpgItemData copy = item.Clone();
			if (string.IsNullOrWhiteSpace(copy.InstanceId) || store.BagList.Any((RpgItemData x) => x.InstanceId == copy.InstanceId) || store.Equipped.Values.Any((RpgItemData x) => x != null && x.InstanceId == copy.InstanceId))
			{
				copy.InstanceId = Guid.NewGuid().ToString("N");
			}
			store.BagList.Add(copy);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00018FDD File Offset: 0x000171DD
		public static bool TryRemoveFromBag(RpgItemData item)
		{
			return RpgRunLoadout.TryRemoveFromBag(RpgNet.LocalNetId(), item);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00018FEC File Offset: 0x000171EC
		public static bool TryRemoveFromBag(ulong netId, RpgItemData item)
		{
			RpgRunLoadout.Store store = RpgRunLoadout.Of(netId);
			int num = RpgRunLoadout.FindBagIndex(store, item);
			if (num < 0)
			{
				return false;
			}
			store.BagList.RemoveAt(num);
			return true;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001901B File Offset: 0x0001721B
		public static bool TryEquipFromBag(RpgItemData item)
		{
			return RpgRunLoadout.TryEquipFromBag(RpgNet.LocalNetId(), item);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00019028 File Offset: 0x00017228
		public static bool TryEquipFromBag(ulong netId, RpgItemData item)
		{
			if (item.IsScroll)
			{
				return false;
			}
			if (!item.IsIdentified)
			{
				return false;
			}
			if (!RpgLoadout.MeetsLevelRequirement(item))
			{
				return false;
			}
			if (!RpgAccessoryGates.MeetsEquipRequirements(item, RpgNet.PlayerByNetId(netId)))
			{
				return false;
			}
			if (!RpgLoadout.CanEquipRarePlus(netId, item))
			{
				return false;
			}
			RpgRunLoadout.Store store = RpgRunLoadout.Of(netId);
			int num = RpgRunLoadout.FindBagIndex(store, item);
			if (num < 0)
			{
				return false;
			}
			RpgItemData rpgItemData = store.BagList[num];
			store.BagList.RemoveAt(num);
			RpgItemData valueOrDefault = store.Equipped.GetValueOrDefault(rpgItemData.EquipSlot);
			if (valueOrDefault != null && !RpgCurseSystem.CanUnequip(valueOrDefault))
			{
				store.BagList.Insert(num, rpgItemData);
				return false;
			}
			store.Equipped[rpgItemData.EquipSlot] = rpgItemData;
			RpgCurseSystem.OnEquipped(rpgItemData);
			if (valueOrDefault != null)
			{
				store.BagList.Add(valueOrDefault);
			}
			RpgLoadoutNetSync.PushEquipped(netId);
			return true;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000190F6 File Offset: 0x000172F6
		public static bool TryUnequip(RpgEquipSlot slot)
		{
			return RpgRunLoadout.TryUnequip(RpgNet.LocalNetId(), slot);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00019104 File Offset: 0x00017304
		public static bool TryUnequip(ulong netId, RpgEquipSlot slot)
		{
			RpgRunLoadout.Store store = RpgRunLoadout.Of(netId);
			RpgItemData rpgItemData;
			if (!store.Equipped.TryGetValue(slot, out rpgItemData) || rpgItemData == null)
			{
				return false;
			}
			if (!RpgCurseSystem.CanUnequip(rpgItemData))
			{
				return false;
			}
			store.Equipped[slot] = null;
			store.BagList.Add(rpgItemData);
			RpgRunLoadout.StripInvalidAccessories(netId, false);
			RpgLoadoutNetSync.PushEquipped(netId);
			return true;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001915E File Offset: 0x0001735E
		public static void UnequipUnidentified(ulong netId)
		{
			RpgRunLoadout.StripInvalidAccessories(netId, false);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00019168 File Offset: 0x00017368
		private static void StripInvalidAccessories(ulong netId, bool push)
		{
			RpgRunLoadout.Store store = RpgRunLoadout.Of(netId);
			Player player = RpgNet.PlayerByNetId(netId);
			bool flag = false;
			foreach (RpgEquipSlot key in Enum.GetValues<RpgEquipSlot>())
			{
				RpgItemData rpgItemData;
				if (store.Equipped.TryGetValue(key, out rpgItemData) && rpgItemData != null && (!rpgItemData.IsIdentified || !RpgAccessoryGates.MeetsEquipRequirements(rpgItemData, player)))
				{
					store.Equipped[key] = null;
					rpgItemData.IsBound = false;
					store.BagList.Add(rpgItemData);
					flag = true;
				}
			}
			if (flag && push)
			{
				RpgLoadoutNetSync.PushEquipped(netId);
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000191FB File Offset: 0x000173FB
		public static IEnumerable<RpgItemData> AllItems()
		{
			return RpgRunLoadout.AllItemsOf(RpgNet.LocalNetId());
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00019207 File Offset: 0x00017407
		public static IEnumerable<RpgItemData> AllItemsOf(ulong netId)
		{
			RpgRunLoadout.<AllItemsOf>d__35 <AllItemsOf>d__ = new RpgRunLoadout.<AllItemsOf>d__35(-2);
			<AllItemsOf>d__.<>3__netId = netId;
			return <AllItemsOf>d__;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00019217 File Offset: 0x00017417
		public static bool TryReplaceByInstanceId(string instanceId, RpgItemData replacement)
		{
			return RpgRunLoadout.TryReplaceByInstanceId(RpgNet.LocalNetId(), instanceId, replacement);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00019228 File Offset: 0x00017428
		public static bool TryReplaceByInstanceId(ulong netId, string instanceId, RpgItemData replacement)
		{
			if (string.IsNullOrWhiteSpace(instanceId))
			{
				return false;
			}
			RpgRunLoadout.Store store = RpgRunLoadout.Of(netId);
			foreach (RpgEquipSlot key in Enum.GetValues<RpgEquipSlot>())
			{
				RpgItemData valueOrDefault = store.Equipped.GetValueOrDefault(key);
				if (valueOrDefault != null && string.Equals(valueOrDefault.InstanceId, instanceId, StringComparison.Ordinal))
				{
					replacement.InstanceId = instanceId;
					store.Equipped[key] = replacement.Clone();
					RpgLoadoutNetSync.PushEquipped(netId);
					return true;
				}
			}
			int num = store.BagList.FindIndex((RpgItemData x) => !string.IsNullOrEmpty(x.InstanceId) && string.Equals(x.InstanceId, instanceId, StringComparison.Ordinal));
			if (num < 0)
			{
				return false;
			}
			replacement.InstanceId = instanceId;
			store.BagList[num] = replacement.Clone();
			return true;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00019302 File Offset: 0x00017502
		public static bool TryRemoveManyByInstanceIds(IEnumerable<string> instanceIds)
		{
			return RpgRunLoadout.TryRemoveManyByInstanceIds(RpgNet.LocalNetId(), instanceIds);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00019310 File Offset: 0x00017510
		public static bool TryRemoveManyByInstanceIds(ulong netId, IEnumerable<string> instanceIds)
		{
			HashSet<string> hashSet = (from x in instanceIds
			where !string.IsNullOrWhiteSpace(x)
			select x).ToHashSet(StringComparer.Ordinal);
			if (hashSet.Count == 0)
			{
				return false;
			}
			RpgRunLoadout.Store store = RpgRunLoadout.Of(netId);
			int num = 0;
			foreach (RpgEquipSlot key in Enum.GetValues<RpgEquipSlot>())
			{
				RpgItemData valueOrDefault = store.Equipped.GetValueOrDefault(key);
				if (valueOrDefault != null && hashSet.Contains(valueOrDefault.InstanceId))
				{
					store.Equipped[key] = null;
					hashSet.Remove(valueOrDefault.InstanceId);
					num++;
				}
			}
			for (int j = store.BagList.Count - 1; j >= 0; j--)
			{
				if (hashSet.Contains(store.BagList[j].InstanceId))
				{
					hashSet.Remove(store.BagList[j].InstanceId);
					store.BagList.RemoveAt(j);
					num++;
				}
			}
			if (num > 0)
			{
				RpgLoadoutNetSync.PushEquipped(netId);
			}
			return num > 0;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001942E File Offset: 0x0001762E
		public static IEnumerable<RpgItemData> AllForDiscovery()
		{
			return new RpgRunLoadout.<AllForDiscovery>d__40(-2);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00019438 File Offset: 0x00017638
		public static string NamesSummary(int maxLines = 8)
		{
			List<RpgItemData> list = RpgRunLoadout.AllItems().ToList<RpgItemData>();
			if (list.Count == 0)
			{
				return SpireLoc.T("SPIRE_LOOT.run_empty", "(none this run)");
			}
			return string.Join("\n", from i in list.Take(maxLines)
			select "· " + i.DisplayName);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000194A0 File Offset: 0x000176A0
		private static int FindBagIndex(RpgRunLoadout.Store store, RpgItemData item)
		{
			return store.BagList.FindIndex((RpgItemData x) => x == item || (!string.IsNullOrEmpty(item.InstanceId) && x.InstanceId == item.InstanceId));
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000194D1 File Offset: 0x000176D1
		// Note: this type is marked as 'beforefieldinit'.
		static RpgRunLoadout()
		{
		}

		// Token: 0x0400014E RID: 334
		private static readonly Dictionary<ulong, RpgRunLoadout.Store> ByNetId = new Dictionary<ulong, RpgRunLoadout.Store>();

		// Token: 0x02000122 RID: 290
		[NullableContext(0)]
		private sealed class Store
		{
			// Token: 0x060007CB RID: 1995 RVA: 0x00028EE0 File Offset: 0x000270E0
			public Store()
			{
				foreach (RpgEquipSlot key in Enum.GetValues<RpgEquipSlot>())
				{
					this.Equipped[key] = null;
				}
			}

			// Token: 0x060007CC RID: 1996 RVA: 0x00028F30 File Offset: 0x00027130
			public void Clear()
			{
				this.BagList.Clear();
				foreach (RpgEquipSlot key in Enum.GetValues<RpgEquipSlot>())
				{
					this.Equipped[key] = null;
				}
			}

			// Token: 0x0400043C RID: 1084
			[Nullable(1)]
			public readonly List<RpgItemData> BagList = new List<RpgItemData>();

			// Token: 0x0400043D RID: 1085
			[Nullable(new byte[]
			{
				1,
				2
			})]
			public readonly Dictionary<RpgEquipSlot, RpgItemData> Equipped = new Dictionary<RpgEquipSlot, RpgItemData>();
		}

		// Token: 0x02000123 RID: 291
		[Nullable(0)]
		public readonly struct StoreSnapshot : IEquatable<RpgRunLoadout.StoreSnapshot>
		{
			// Token: 0x060007CD RID: 1997 RVA: 0x00028F6D File Offset: 0x0002716D
			public StoreSnapshot(List<RpgItemData> BagItems, [Nullable(new byte[]
			{
				1,
				1,
				2
			})] Dictionary<string, RpgItemData> EquippedItems)
			{
				this.BagItems = BagItems;
				this.EquippedItems = EquippedItems;
			}

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x060007CE RID: 1998 RVA: 0x00028F7D File Offset: 0x0002717D
			// (set) Token: 0x060007CF RID: 1999 RVA: 0x00028F85 File Offset: 0x00027185
			public List<RpgItemData> BagItems
			{
				[CompilerGenerated]
				get
				{
					return this.<BagItems>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<BagItems>k__BackingField = value;
				}
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00028F8E File Offset: 0x0002718E
			// (set) Token: 0x060007D1 RID: 2001 RVA: 0x00028F96 File Offset: 0x00027196
			[Nullable(new byte[]
			{
				1,
				1,
				2
			})]
			public Dictionary<string, RpgItemData> EquippedItems
			{
				[CompilerGenerated]
				[return: Nullable(new byte[]
				{
					1,
					1,
					2
				})]
				get
				{
					return this.<EquippedItems>k__BackingField;
				}
				[CompilerGenerated]
				[param: Nullable(new byte[]
				{
					1,
					1,
					2
				})]
				set
				{
					this.<EquippedItems>k__BackingField = value;
				}
			}

			// Token: 0x060007D2 RID: 2002 RVA: 0x00028FA0 File Offset: 0x000271A0
			[NullableContext(0)]
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("StoreSnapshot");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x060007D3 RID: 2003 RVA: 0x00028FEC File Offset: 0x000271EC
			[NullableContext(0)]
			[CompilerGenerated]
			private bool PrintMembers(StringBuilder builder)
			{
				builder.Append("BagItems = ");
				builder.Append(this.BagItems);
				builder.Append(", EquippedItems = ");
				builder.Append(this.EquippedItems);
				return true;
			}

			// Token: 0x060007D4 RID: 2004 RVA: 0x00029021 File Offset: 0x00027221
			[CompilerGenerated]
			public static bool operator !=(RpgRunLoadout.StoreSnapshot left, RpgRunLoadout.StoreSnapshot right)
			{
				return !(left == right);
			}

			// Token: 0x060007D5 RID: 2005 RVA: 0x0002902D File Offset: 0x0002722D
			[CompilerGenerated]
			public static bool operator ==(RpgRunLoadout.StoreSnapshot left, RpgRunLoadout.StoreSnapshot right)
			{
				return left.Equals(right);
			}

			// Token: 0x060007D6 RID: 2006 RVA: 0x00029037 File Offset: 0x00027237
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return EqualityComparer<List<RpgItemData>>.Default.GetHashCode(this.<BagItems>k__BackingField) * -1521134295 + EqualityComparer<Dictionary<string, RpgItemData>>.Default.GetHashCode(this.<EquippedItems>k__BackingField);
			}

			// Token: 0x060007D7 RID: 2007 RVA: 0x00029060 File Offset: 0x00027260
			[NullableContext(0)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return obj is RpgRunLoadout.StoreSnapshot && this.Equals((RpgRunLoadout.StoreSnapshot)obj);
			}

			// Token: 0x060007D8 RID: 2008 RVA: 0x00029078 File Offset: 0x00027278
			[CompilerGenerated]
			public bool Equals(RpgRunLoadout.StoreSnapshot other)
			{
				return EqualityComparer<List<RpgItemData>>.Default.Equals(this.<BagItems>k__BackingField, other.<BagItems>k__BackingField) && EqualityComparer<Dictionary<string, RpgItemData>>.Default.Equals(this.<EquippedItems>k__BackingField, other.<EquippedItems>k__BackingField);
			}

			// Token: 0x060007D9 RID: 2009 RVA: 0x000290AA File Offset: 0x000272AA
			[CompilerGenerated]
			public void Deconstruct(out List<RpgItemData> BagItems, [Nullable(new byte[]
			{
				1,
				1,
				2
			})] out Dictionary<string, RpgItemData> EquippedItems)
			{
				BagItems = this.BagItems;
				EquippedItems = this.EquippedItems;
			}

			// Token: 0x0400043E RID: 1086
			[CompilerGenerated]
			private readonly List<RpgItemData> <BagItems>k__BackingField;

			// Token: 0x0400043F RID: 1087
			[Nullable(new byte[]
			{
				1,
				1,
				2
			})]
			[CompilerGenerated]
			private readonly Dictionary<string, RpgItemData> <EquippedItems>k__BackingField;
		}

		// Token: 0x02000124 RID: 292
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060007DA RID: 2010 RVA: 0x000290BC File Offset: 0x000272BC
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060007DB RID: 2011 RVA: 0x000290C8 File Offset: 0x000272C8
			public <>c()
			{
			}

			// Token: 0x060007DC RID: 2012 RVA: 0x000290D0 File Offset: 0x000272D0
			internal string <CaptureEquippedDto>b__20_0(RpgEquipSlot s)
			{
				return s.ToString();
			}

			// Token: 0x060007DD RID: 2013 RVA: 0x000290DF File Offset: 0x000272DF
			[NullableContext(0)]
			internal RpgItemData <CaptureDto>b__21_0(RpgItemData x)
			{
				return x.Clone();
			}

			// Token: 0x060007DE RID: 2014 RVA: 0x000290E7 File Offset: 0x000272E7
			internal string <CaptureDto>b__21_1(RpgEquipSlot s)
			{
				return s.ToString();
			}

			// Token: 0x060007DF RID: 2015 RVA: 0x000290F6 File Offset: 0x000272F6
			[NullableContext(0)]
			internal RpgItemData <SnapshotAll>b__22_0(RpgItemData x)
			{
				return x.Clone();
			}

			// Token: 0x060007E0 RID: 2016 RVA: 0x000290FE File Offset: 0x000272FE
			internal string <SnapshotAll>b__22_1(RpgEquipSlot s)
			{
				return s.ToString();
			}

			// Token: 0x060007E1 RID: 2017 RVA: 0x0002910D File Offset: 0x0002730D
			[NullableContext(0)]
			internal bool <TryRemoveManyByInstanceIds>b__39_0(string x)
			{
				return !string.IsNullOrWhiteSpace(x);
			}

			// Token: 0x060007E2 RID: 2018 RVA: 0x00029118 File Offset: 0x00027318
			[NullableContext(0)]
			internal string <NamesSummary>b__41_0(RpgItemData i)
			{
				return "· " + i.DisplayName;
			}

			// Token: 0x04000440 RID: 1088
			[Nullable(0)]
			public static readonly RpgRunLoadout.<>c <>9 = new RpgRunLoadout.<>c();

			// Token: 0x04000441 RID: 1089
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Func<RpgEquipSlot, string> <>9__20_0;

			// Token: 0x04000442 RID: 1090
			[Nullable(0)]
			public static Func<RpgItemData, RpgItemData> <>9__21_0;

			// Token: 0x04000443 RID: 1091
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Func<RpgEquipSlot, string> <>9__21_1;

			// Token: 0x04000444 RID: 1092
			[Nullable(0)]
			public static Func<RpgItemData, RpgItemData> <>9__22_0;

			// Token: 0x04000445 RID: 1093
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Func<RpgEquipSlot, string> <>9__22_1;

			// Token: 0x04000446 RID: 1094
			[Nullable(0)]
			public static Func<string, bool> <>9__39_0;

			// Token: 0x04000447 RID: 1095
			[Nullable(0)]
			public static Func<RpgItemData, string> <>9__41_0;
		}

		// Token: 0x02000125 RID: 293
		[CompilerGenerated]
		private sealed class <>c__DisplayClass20_0
		{
			// Token: 0x060007E3 RID: 2019 RVA: 0x0002912A File Offset: 0x0002732A
			public <>c__DisplayClass20_0()
			{
			}

			// Token: 0x060007E4 RID: 2020 RVA: 0x00029132 File Offset: 0x00027332
			[NullableContext(0)]
			internal RpgItemData <CaptureEquippedDto>b__1(RpgEquipSlot s)
			{
				RpgItemData valueOrDefault = this.store.Equipped.GetValueOrDefault(s);
				if (valueOrDefault == null)
				{
					return null;
				}
				return valueOrDefault.Clone();
			}

			// Token: 0x04000448 RID: 1096
			[Nullable(0)]
			public RpgRunLoadout.Store store;
		}

		// Token: 0x02000126 RID: 294
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_0
		{
			// Token: 0x060007E5 RID: 2021 RVA: 0x00029150 File Offset: 0x00027350
			public <>c__DisplayClass21_0()
			{
			}

			// Token: 0x060007E6 RID: 2022 RVA: 0x00029158 File Offset: 0x00027358
			[NullableContext(0)]
			internal RpgItemData <CaptureDto>b__2(RpgEquipSlot s)
			{
				RpgItemData valueOrDefault = this.store.Equipped.GetValueOrDefault(s);
				if (valueOrDefault == null)
				{
					return null;
				}
				return valueOrDefault.Clone();
			}

			// Token: 0x04000449 RID: 1097
			[Nullable(0)]
			public RpgRunLoadout.Store store;
		}

		// Token: 0x02000127 RID: 295
		[CompilerGenerated]
		private sealed class <>c__DisplayClass22_0
		{
			// Token: 0x060007E7 RID: 2023 RVA: 0x00029176 File Offset: 0x00027376
			public <>c__DisplayClass22_0()
			{
			}

			// Token: 0x060007E8 RID: 2024 RVA: 0x0002917E File Offset: 0x0002737E
			[NullableContext(0)]
			internal RpgItemData <SnapshotAll>b__2(RpgEquipSlot s)
			{
				RpgItemData valueOrDefault = this.store.Equipped.GetValueOrDefault(s);
				if (valueOrDefault == null)
				{
					return null;
				}
				return valueOrDefault.Clone();
			}

			// Token: 0x0400044A RID: 1098
			[Nullable(0)]
			public RpgRunLoadout.Store store;
		}

		// Token: 0x02000128 RID: 296
		[CompilerGenerated]
		private sealed class <>c__DisplayClass25_0
		{
			// Token: 0x060007E9 RID: 2025 RVA: 0x0002919C File Offset: 0x0002739C
			public <>c__DisplayClass25_0()
			{
			}

			// Token: 0x060007EA RID: 2026 RVA: 0x000291A4 File Offset: 0x000273A4
			[NullableContext(0)]
			internal bool <AddToBag>b__0(RpgItemData x)
			{
				return x.InstanceId == this.copy.InstanceId;
			}

			// Token: 0x060007EB RID: 2027 RVA: 0x000291BC File Offset: 0x000273BC
			[NullableContext(0)]
			internal bool <AddToBag>b__1(RpgItemData x)
			{
				return x != null && x.InstanceId == this.copy.InstanceId;
			}

			// Token: 0x0400044B RID: 1099
			[Nullable(0)]
			public RpgItemData copy;
		}

		// Token: 0x02000129 RID: 297
		[CompilerGenerated]
		private sealed class <>c__DisplayClass37_0
		{
			// Token: 0x060007EC RID: 2028 RVA: 0x000291D9 File Offset: 0x000273D9
			public <>c__DisplayClass37_0()
			{
			}

			// Token: 0x060007ED RID: 2029 RVA: 0x000291E1 File Offset: 0x000273E1
			internal bool <TryReplaceByInstanceId>b__0(RpgItemData x)
			{
				return !string.IsNullOrEmpty(x.InstanceId) && string.Equals(x.InstanceId, this.instanceId, StringComparison.Ordinal);
			}

			// Token: 0x0400044C RID: 1100
			[Nullable(0)]
			public string instanceId;
		}

		// Token: 0x0200012A RID: 298
		[CompilerGenerated]
		private sealed class <>c__DisplayClass42_0
		{
			// Token: 0x060007EE RID: 2030 RVA: 0x00029204 File Offset: 0x00027404
			public <>c__DisplayClass42_0()
			{
			}

			// Token: 0x060007EF RID: 2031 RVA: 0x0002920C File Offset: 0x0002740C
			internal bool <FindBagIndex>b__0(RpgItemData x)
			{
				return x == this.item || (!string.IsNullOrEmpty(this.item.InstanceId) && x.InstanceId == this.item.InstanceId);
			}

			// Token: 0x0400044D RID: 1101
			[Nullable(0)]
			public RpgItemData item;
		}

		// Token: 0x0200012B RID: 299
		[CompilerGenerated]
		private sealed class <AllForDiscovery>d__40 : IEnumerable<RpgItemData>, IEnumerable, IEnumerator<RpgItemData>, IEnumerator, IDisposable
		{
			// Token: 0x060007F0 RID: 2032 RVA: 0x00029243 File Offset: 0x00027443
			[DebuggerHidden]
			public <AllForDiscovery>d__40(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060007F1 RID: 2033 RVA: 0x00029260 File Offset: 0x00027460
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num - -4 <= 1 || num == 1)
				{
					try
					{
						if (num == -4 || num == 1)
						{
							try
							{
							}
							finally
							{
								this.<>m__Finally2();
							}
						}
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
				this.<>7__wrap1 = default(List<KeyValuePair<ulong, RpgRunLoadout.Store>>.Enumerator);
				this.<>7__wrap2 = null;
				this.<>1__state = -2;
			}

			// Token: 0x060007F2 RID: 2034 RVA: 0x000292D4 File Offset: 0x000274D4
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					if (num == 0)
					{
						this.<>1__state = -1;
						this.<>7__wrap1 = RpgRunLoadout.ByNetId.ToList<KeyValuePair<ulong, RpgRunLoadout.Store>>().GetEnumerator();
						this.<>1__state = -3;
						goto IL_B5;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -4;
					IL_9B:
					if (this.<>7__wrap2.MoveNext())
					{
						RpgItemData rpgItemData = this.<>7__wrap2.Current;
						this.<>2__current = rpgItemData;
						this.<>1__state = 1;
						return true;
					}
					this.<>m__Finally2();
					this.<>7__wrap2 = null;
					IL_B5:
					if (this.<>7__wrap1.MoveNext())
					{
						KeyValuePair<ulong, RpgRunLoadout.Store> keyValuePair = this.<>7__wrap1.Current;
						ulong num2;
						RpgRunLoadout.Store store;
						keyValuePair.Deconstruct(out num2, out store);
						ulong netId = num2;
						this.<>7__wrap2 = RpgRunLoadout.AllItemsOf(netId).GetEnumerator();
						this.<>1__state = -4;
						goto IL_9B;
					}
					this.<>m__Finally1();
					this.<>7__wrap1 = default(List<KeyValuePair<ulong, RpgRunLoadout.Store>>.Enumerator);
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x060007F3 RID: 2035 RVA: 0x000293D4 File Offset: 0x000275D4
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				((IDisposable)this.<>7__wrap1).Dispose();
			}

			// Token: 0x060007F4 RID: 2036 RVA: 0x000293EE File Offset: 0x000275EE
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				if (this.<>7__wrap2 != null)
				{
					this.<>7__wrap2.Dispose();
				}
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0002940B File Offset: 0x0002760B
			RpgItemData IEnumerator<RpgItemData>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060007F6 RID: 2038 RVA: 0x00029413 File Offset: 0x00027613
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0002941A File Offset: 0x0002761A
			object IEnumerator.Current
			{
				[DebuggerHidden]
				[return: Nullable(0)]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060007F8 RID: 2040 RVA: 0x00029424 File Offset: 0x00027624
			[DebuggerHidden]
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			IEnumerator<RpgItemData> IEnumerable<RpgItemData>.GetEnumerator()
			{
				RpgRunLoadout.<AllForDiscovery>d__40 result;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					result = this;
				}
				else
				{
					result = new RpgRunLoadout.<AllForDiscovery>d__40(0);
				}
				return result;
			}

			// Token: 0x060007F9 RID: 2041 RVA: 0x0002945B File Offset: 0x0002765B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<SpireTheRpg.Core.RpgItemData>.GetEnumerator();
			}

			// Token: 0x0400044E RID: 1102
			private int <>1__state;

			// Token: 0x0400044F RID: 1103
			private RpgItemData <>2__current;

			// Token: 0x04000450 RID: 1104
			private int <>l__initialThreadId;

			// Token: 0x04000451 RID: 1105
			[Nullable(0)]
			private List<KeyValuePair<ulong, RpgRunLoadout.Store>>.Enumerator <>7__wrap1;

			// Token: 0x04000452 RID: 1106
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private IEnumerator<RpgItemData> <>7__wrap2;
		}

		// Token: 0x0200012C RID: 300
		[CompilerGenerated]
		private sealed class <AllItemsOf>d__35 : IEnumerable<RpgItemData>, IEnumerable, IEnumerator<RpgItemData>, IEnumerator, IDisposable
		{
			// Token: 0x060007FA RID: 2042 RVA: 0x00029463 File Offset: 0x00027663
			[DebuggerHidden]
			public <AllItemsOf>d__35(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060007FB RID: 2043 RVA: 0x00029480 File Offset: 0x00027680
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				switch (this.<>1__state)
				{
				case -4:
				case 2:
					break;
				case -3:
				case 1:
					try
					{
						goto IL_42;
					}
					finally
					{
						this.<>m__Finally1();
					}
					break;
				case -2:
				case -1:
				case 0:
					goto IL_42;
				default:
					goto IL_42;
				}
				try
				{
				}
				finally
				{
					this.<>m__Finally2();
				}
				IL_42:
				this.<>7__wrap1 = null;
				this.<>7__wrap2 = default(List<RpgItemData>.Enumerator);
				this.<>1__state = -2;
			}

			// Token: 0x060007FC RID: 2044 RVA: 0x00029508 File Offset: 0x00027708
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
						this.<>1__state = -1;
						this.<>7__wrap1 = RpgRunLoadout.EquippedItemsOf(netId).GetEnumerator();
						this.<>1__state = -3;
						break;
					case 1:
						this.<>1__state = -3;
						break;
					case 2:
						this.<>1__state = -4;
						goto IL_D5;
					default:
						return false;
					}
					if (this.<>7__wrap1.MoveNext())
					{
						RpgItemData rpgItemData = this.<>7__wrap1.Current;
						this.<>2__current = rpgItemData;
						this.<>1__state = 1;
						return true;
					}
					this.<>m__Finally1();
					this.<>7__wrap1 = null;
					this.<>7__wrap2 = RpgRunLoadout.Of(netId).BagList.GetEnumerator();
					this.<>1__state = -4;
					IL_D5:
					if (!this.<>7__wrap2.MoveNext())
					{
						this.<>m__Finally2();
						this.<>7__wrap2 = default(List<RpgItemData>.Enumerator);
						result = false;
					}
					else
					{
						RpgItemData rpgItemData2 = this.<>7__wrap2.Current;
						this.<>2__current = rpgItemData2;
						this.<>1__state = 2;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x060007FD RID: 2045 RVA: 0x00029628 File Offset: 0x00027828
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (this.<>7__wrap1 != null)
				{
					this.<>7__wrap1.Dispose();
				}
			}

			// Token: 0x060007FE RID: 2046 RVA: 0x00029644 File Offset: 0x00027844
			private void <>m__Finally2()
			{
				this.<>1__state = -1;
				((IDisposable)this.<>7__wrap2).Dispose();
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x060007FF RID: 2047 RVA: 0x0002965E File Offset: 0x0002785E
			RpgItemData IEnumerator<RpgItemData>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000800 RID: 2048 RVA: 0x00029666 File Offset: 0x00027866
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x06000801 RID: 2049 RVA: 0x0002966D File Offset: 0x0002786D
			object IEnumerator.Current
			{
				[DebuggerHidden]
				[return: Nullable(0)]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000802 RID: 2050 RVA: 0x00029678 File Offset: 0x00027878
			[DebuggerHidden]
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			IEnumerator<RpgItemData> IEnumerable<RpgItemData>.GetEnumerator()
			{
				RpgRunLoadout.<AllItemsOf>d__35 <AllItemsOf>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<AllItemsOf>d__ = this;
				}
				else
				{
					<AllItemsOf>d__ = new RpgRunLoadout.<AllItemsOf>d__35(0);
				}
				<AllItemsOf>d__.netId = netId;
				return <AllItemsOf>d__;
			}

			// Token: 0x06000803 RID: 2051 RVA: 0x000296BB File Offset: 0x000278BB
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<SpireTheRpg.Core.RpgItemData>.GetEnumerator();
			}

			// Token: 0x04000453 RID: 1107
			private int <>1__state;

			// Token: 0x04000454 RID: 1108
			private RpgItemData <>2__current;

			// Token: 0x04000455 RID: 1109
			private int <>l__initialThreadId;

			// Token: 0x04000456 RID: 1110
			private ulong netId;

			// Token: 0x04000457 RID: 1111
			public ulong <>3__netId;

			// Token: 0x04000458 RID: 1112
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private IEnumerator<RpgItemData> <>7__wrap1;

			// Token: 0x04000459 RID: 1113
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private List<RpgItemData>.Enumerator <>7__wrap2;
		}

		// Token: 0x0200012D RID: 301
		[CompilerGenerated]
		private sealed class <EquippedItemsOf>d__13 : IEnumerable<RpgItemData>, IEnumerable, IEnumerator<RpgItemData>, IEnumerator, IDisposable
		{
			// Token: 0x06000804 RID: 2052 RVA: 0x000296C3 File Offset: 0x000278C3
			[DebuggerHidden]
			public <EquippedItemsOf>d__13(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000805 RID: 2053 RVA: 0x000296DD File Offset: 0x000278DD
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				this.<store>5__2 = null;
				this.<>7__wrap2 = null;
				this.<>1__state = -2;
			}

			// Token: 0x06000806 RID: 2054 RVA: 0x000296F8 File Offset: 0x000278F8
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					this.<store>5__2 = RpgRunLoadout.Of(netId);
					this.<>7__wrap2 = Enum.GetValues<RpgEquipSlot>();
					this.<>7__wrap3 = 0;
					goto IL_84;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				IL_76:
				this.<>7__wrap3++;
				IL_84:
				if (this.<>7__wrap3 >= this.<>7__wrap2.Length)
				{
					this.<>7__wrap2 = null;
					return false;
				}
				RpgEquipSlot key = this.<>7__wrap2[this.<>7__wrap3];
				RpgItemData valueOrDefault = this.<store>5__2.Equipped.GetValueOrDefault(key);
				if (valueOrDefault != null)
				{
					this.<>2__current = valueOrDefault;
					this.<>1__state = 1;
					return true;
				}
				goto IL_76;
			}

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x06000807 RID: 2055 RVA: 0x000297A1 File Offset: 0x000279A1
			RpgItemData IEnumerator<RpgItemData>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000808 RID: 2056 RVA: 0x000297A9 File Offset: 0x000279A9
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x06000809 RID: 2057 RVA: 0x000297B0 File Offset: 0x000279B0
			object IEnumerator.Current
			{
				[DebuggerHidden]
				[return: Nullable(0)]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600080A RID: 2058 RVA: 0x000297B8 File Offset: 0x000279B8
			[DebuggerHidden]
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			IEnumerator<RpgItemData> IEnumerable<RpgItemData>.GetEnumerator()
			{
				RpgRunLoadout.<EquippedItemsOf>d__13 <EquippedItemsOf>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<EquippedItemsOf>d__ = this;
				}
				else
				{
					<EquippedItemsOf>d__ = new RpgRunLoadout.<EquippedItemsOf>d__13(0);
				}
				<EquippedItemsOf>d__.netId = netId;
				return <EquippedItemsOf>d__;
			}

			// Token: 0x0600080B RID: 2059 RVA: 0x000297FB File Offset: 0x000279FB
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<SpireTheRpg.Core.RpgItemData>.GetEnumerator();
			}

			// Token: 0x0400045A RID: 1114
			private int <>1__state;

			// Token: 0x0400045B RID: 1115
			private RpgItemData <>2__current;

			// Token: 0x0400045C RID: 1116
			private int <>l__initialThreadId;

			// Token: 0x0400045D RID: 1117
			private ulong netId;

			// Token: 0x0400045E RID: 1118
			public ulong <>3__netId;

			// Token: 0x0400045F RID: 1119
			[Nullable(0)]
			private RpgRunLoadout.Store <store>5__2;

			// Token: 0x04000460 RID: 1120
			[Nullable(0)]
			private RpgEquipSlot[] <>7__wrap2;

			// Token: 0x04000461 RID: 1121
			private int <>7__wrap3;
		}
	}
}
