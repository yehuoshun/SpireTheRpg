using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SpireTheRpg.Core
{
	// Token: 0x0200004C RID: 76
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgLoadout
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00014AD4 File Offset: 0x00012CD4
		public static IReadOnlyList<RpgItemData> Bag
		{
			get
			{
				RpgProgress.Ensure();
				return RpgLoadout._bag;
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00014AE0 File Offset: 0x00012CE0
		[NullableContext(2)]
		public static RpgItemData GetEquipped(RpgEquipSlot slot)
		{
			RpgProgress.Ensure();
			return RpgLoadout.Equipped.GetValueOrDefault(slot);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00014AF2 File Offset: 0x00012CF2
		public static IEnumerable<RpgItemData> EquippedItems()
		{
			return new RpgLoadout.<EquippedItems>d__5(-2);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00014AFC File Offset: 0x00012CFC
		public static void LoadFromSave(RpgSaveDto dto)
		{
			RpgLoadout._bag = new List<RpgItemData>();
			RpgLoadout.Equipped.Clear();
			foreach (RpgEquipSlot key in Enum.GetValues<RpgEquipSlot>())
			{
				RpgLoadout.Equipped[key] = null;
			}
			if (dto.BagItems.Count > 0 || dto.EquippedItems.Count > 0)
			{
				foreach (RpgItemData rpgItemData in dto.BagItems)
				{
					if (string.IsNullOrWhiteSpace(rpgItemData.InstanceId))
					{
						rpgItemData.InstanceId = Guid.NewGuid().ToString("N");
					}
					rpgItemData.SyncVisualFromTemplate();
					RpgLoadout._bag.Add(rpgItemData);
				}
				foreach (KeyValuePair<string, RpgItemData> keyValuePair in dto.EquippedItems)
				{
					string text;
					RpgItemData rpgItemData2;
					keyValuePair.Deconstruct(out text, out rpgItemData2);
					string value = text;
					RpgItemData rpgItemData3 = rpgItemData2;
					RpgEquipSlot key2;
					if (Enum.TryParse<RpgEquipSlot>(value, true, out key2) && rpgItemData3 != null)
					{
						if (string.IsNullOrWhiteSpace(rpgItemData3.InstanceId))
						{
							rpgItemData3.InstanceId = Guid.NewGuid().ToString("N");
						}
						rpgItemData3.SyncVisualFromTemplate();
						RpgLoadout.Equipped[key2] = rpgItemData3;
					}
				}
				RpgItemRoller.EnsureUniqueInstanceIds(RpgLoadout._bag.Concat(RpgLoadout.Equipped.Values.OfType<RpgItemData>()));
				return;
			}
			foreach (string id in dto.BagIds)
			{
				RpgItemTemplate rpgItemTemplate = RpgItemCatalog.Get(id);
				if (rpgItemTemplate != null)
				{
					RpgLoadout._bag.Add(RpgItemRoller.Roll(rpgItemTemplate, RpgItemRarity.Common, null));
				}
			}
			foreach (KeyValuePair<string, string> keyValuePair2 in dto.Equipped)
			{
				string text;
				string text2;
				keyValuePair2.Deconstruct(out text, out text2);
				string value2 = text;
				string text3 = text2;
				RpgEquipSlot key3;
				if (Enum.TryParse<RpgEquipSlot>(value2, true, out key3) && !string.IsNullOrWhiteSpace(text3))
				{
					RpgItemTemplate rpgItemTemplate2 = RpgItemCatalog.Get(text3);
					if (rpgItemTemplate2 != null)
					{
						RpgLoadout.Equipped[key3] = RpgItemRoller.Roll(rpgItemTemplate2, RpgItemRarity.Uncommon, null);
					}
				}
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00014D70 File Offset: 0x00012F70
		public static void AddToBag(RpgItemData item, bool persist = true)
		{
			RpgProgress.Ensure();
			RpgItemData copy = item.Clone();
			if (string.IsNullOrWhiteSpace(copy.InstanceId) || RpgLoadout._bag.Any((RpgItemData x) => x.InstanceId == copy.InstanceId) || RpgLoadout.Equipped.Values.Any((RpgItemData x) => x != null && x.InstanceId == copy.InstanceId))
			{
				copy.InstanceId = Guid.NewGuid().ToString("N");
			}
			RpgLoadout._bag.Add(copy);
			if (persist)
			{
				RpgProgress.Persist();
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00014E10 File Offset: 0x00013010
		public static bool MeetsLevelRequirement(RpgItemData item)
		{
			return (SpireTheRpgConfig.EnableDevCheats && SpireTheRpgConfig.IgnoreLevelRequirement) || RpgProgress.Level >= item.RequireLevel;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00014E32 File Offset: 0x00013032
		public static bool CanEquipRarePlus(RpgItemData item)
		{
			return RpgLoadout.CanEquipRarePlus(RpgNet.LocalNetId(), item);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00014E40 File Offset: 0x00013040
		public static bool CanEquipRarePlus(ulong netId, RpgItemData item)
		{
			if (!item.IsRarePlus)
			{
				return true;
			}
			int maxRareEquipped = RpgEffectiveConfig.MaxRareEquipped;
			if (maxRareEquipped <= 0)
			{
				return true;
			}
			int num;
			if (!RpgEffectiveConfig.EnableMetaEquipment)
			{
				num = RpgRunLoadout.EquippedItemsOf(netId).Count((RpgItemData x) => x.IsRarePlus);
			}
			else
			{
				num = RpgLoadout.EquippedItems().Count((RpgItemData x) => x.IsRarePlus);
			}
			int num2 = num;
			RpgItemData rpgItemData = RpgEffectiveConfig.EnableMetaEquipment ? RpgLoadout.GetEquipped(item.EquipSlot) : RpgRunLoadout.GetEquipped(netId, item.EquipSlot);
			if (rpgItemData != null && rpgItemData.IsRarePlus)
			{
				num2--;
			}
			return num2 < maxRareEquipped;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00014EF4 File Offset: 0x000130F4
		public static bool TryRemoveFromBag(RpgItemData item)
		{
			RpgProgress.Ensure();
			int num = RpgLoadout.FindBagIndex(item);
			if (num < 0)
			{
				return false;
			}
			RpgLoadout._bag.RemoveAt(num);
			RpgProgress.Persist();
			return true;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00014F24 File Offset: 0x00013124
		public static bool TryEquipFromBag(RpgItemData item)
		{
			RpgProgress.Ensure();
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
			if (!RpgAccessoryGates.MeetsEquipRequirements(item, null))
			{
				return false;
			}
			if (!RpgLoadout.CanEquipRarePlus(item))
			{
				return false;
			}
			int num = RpgLoadout.FindBagIndex(item);
			if (num < 0)
			{
				return false;
			}
			RpgItemData rpgItemData = RpgLoadout._bag[num];
			RpgLoadout._bag.RemoveAt(num);
			RpgItemData valueOrDefault = RpgLoadout.Equipped.GetValueOrDefault(rpgItemData.EquipSlot);
			if (valueOrDefault != null && !RpgCurseSystem.CanUnequip(valueOrDefault))
			{
				RpgLoadout._bag.Insert(num, rpgItemData);
				return false;
			}
			RpgLoadout.Equipped[rpgItemData.EquipSlot] = rpgItemData;
			RpgCurseSystem.OnEquipped(rpgItemData);
			if (valueOrDefault != null)
			{
				RpgLoadout._bag.Add(valueOrDefault);
			}
			RpgProgress.Persist();
			return true;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00014FE4 File Offset: 0x000131E4
		private static int FindBagIndex(RpgItemData item)
		{
			return RpgLoadout._bag.FindIndex((RpgItemData x) => x == item || (!string.IsNullOrEmpty(item.InstanceId) && x.InstanceId == item.InstanceId));
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00015014 File Offset: 0x00013214
		public static bool TryUnequip(RpgEquipSlot slot)
		{
			RpgProgress.Ensure();
			RpgItemData rpgItemData;
			if (!RpgLoadout.Equipped.TryGetValue(slot, out rpgItemData) || rpgItemData == null)
			{
				return false;
			}
			if (!RpgCurseSystem.CanUnequip(rpgItemData))
			{
				return false;
			}
			RpgLoadout.Equipped[slot] = null;
			RpgLoadout._bag.Add(rpgItemData);
			foreach (RpgEquipSlot key in Enum.GetValues<RpgEquipSlot>())
			{
				RpgItemData rpgItemData2;
				if (RpgLoadout.Equipped.TryGetValue(key, out rpgItemData2) && rpgItemData2 != null && (!rpgItemData2.IsIdentified || !RpgAccessoryGates.MeetsEquipRequirements(rpgItemData2, null)))
				{
					RpgLoadout.Equipped[key] = null;
					rpgItemData2.IsBound = false;
					RpgLoadout._bag.Add(rpgItemData2);
				}
			}
			RpgProgress.Persist();
			return true;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000150C0 File Offset: 0x000132C0
		public static void UnequipUnidentified()
		{
			RpgProgress.Ensure();
			foreach (RpgEquipSlot key in Enum.GetValues<RpgEquipSlot>())
			{
				RpgItemData rpgItemData;
				if (RpgLoadout.Equipped.TryGetValue(key, out rpgItemData) && rpgItemData != null && (!rpgItemData.IsIdentified || !RpgAccessoryGates.MeetsEquipRequirements(rpgItemData, null)))
				{
					RpgLoadout.Equipped[key] = null;
					rpgItemData.IsBound = false;
					RpgLoadout._bag.Add(rpgItemData);
				}
			}
			RpgProgress.Persist();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00015130 File Offset: 0x00013330
		public static IEnumerable<RpgItemData> AllItems()
		{
			return new RpgLoadout.<AllItems>d__16(-2);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0001513C File Offset: 0x0001333C
		public static bool TryReplaceByInstanceId(string instanceId, RpgItemData replacement, bool persist = true)
		{
			RpgProgress.Ensure();
			if (string.IsNullOrWhiteSpace(instanceId))
			{
				return false;
			}
			foreach (RpgEquipSlot key in Enum.GetValues<RpgEquipSlot>())
			{
				RpgItemData valueOrDefault = RpgLoadout.Equipped.GetValueOrDefault(key);
				if (valueOrDefault != null && string.Equals(valueOrDefault.InstanceId, instanceId, StringComparison.Ordinal))
				{
					replacement.InstanceId = instanceId;
					RpgLoadout.Equipped[key] = replacement.Clone();
					if (persist)
					{
						RpgProgress.Persist();
					}
					return true;
				}
			}
			int num = RpgLoadout._bag.FindIndex((RpgItemData x) => !string.IsNullOrEmpty(x.InstanceId) && string.Equals(x.InstanceId, instanceId, StringComparison.Ordinal));
			if (num < 0)
			{
				return false;
			}
			replacement.InstanceId = instanceId;
			RpgLoadout._bag[num] = replacement.Clone();
			if (persist)
			{
				RpgProgress.Persist();
			}
			return true;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00015218 File Offset: 0x00013418
		public static bool TryRemoveManyByInstanceIds(IEnumerable<string> instanceIds, bool persist = true)
		{
			RpgProgress.Ensure();
			HashSet<string> hashSet = (from x in instanceIds
			where !string.IsNullOrWhiteSpace(x)
			select x).ToHashSet(StringComparer.Ordinal);
			if (hashSet.Count == 0)
			{
				return false;
			}
			int num = 0;
			foreach (RpgEquipSlot key in Enum.GetValues<RpgEquipSlot>())
			{
				RpgItemData valueOrDefault = RpgLoadout.Equipped.GetValueOrDefault(key);
				if (valueOrDefault != null && hashSet.Contains(valueOrDefault.InstanceId))
				{
					RpgLoadout.Equipped[key] = null;
					hashSet.Remove(valueOrDefault.InstanceId);
					num++;
				}
			}
			for (int j = RpgLoadout._bag.Count - 1; j >= 0; j--)
			{
				if (hashSet.Contains(RpgLoadout._bag[j].InstanceId))
				{
					hashSet.Remove(RpgLoadout._bag[j].InstanceId);
					RpgLoadout._bag.RemoveAt(j);
					num++;
				}
			}
			if (num == 0)
			{
				return false;
			}
			if (persist)
			{
				RpgProgress.Persist();
			}
			return true;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0001532C File Offset: 0x0001352C
		public static string EffectsSummary(int maxLines = 8)
		{
			List<string> list = RpgLoadout.MergedMods().Values.OrderBy((RpgStatMod m) => m.Id, StringComparer.OrdinalIgnoreCase).Select(delegate(RpgStatMod m)
			{
				if (!(m.Id == "damage_reduction"))
				{
					return m.FormatAffix();
				}
				return m.FormatDetail();
			}).Take(maxLines).ToList<string>();
			if (list.Count != 0)
			{
				return string.Join("\n", list);
			}
			return SpireLoc.T("SPIRE_INVENTORY.effects_empty", "No equipment bonuses");
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000153C0 File Offset: 0x000135C0
		public static int FlatStat(string id)
		{
			double num = RpgLoadout.SumFlat(id);
			if (RpgLoadout.IsPrimaryAttribute(id))
			{
				num += RpgLoadout.SumFlat("all_stats");
			}
			return (int)Math.Round(num);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000153F0 File Offset: 0x000135F0
		public static double PercentStat(string id)
		{
			double num = 0.0;
			foreach (RpgStatMod rpgStatMod in RpgLoadout.MergedMods().Values)
			{
				if (rpgStatMod.Percent && string.Equals(rpgStatMod.Id, id, StringComparison.OrdinalIgnoreCase))
				{
					num += rpgStatMod.Value;
				}
			}
			return num;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0001546C File Offset: 0x0001366C
		private static bool IsPrimaryAttribute(string id)
		{
			return id.Equals("strength", StringComparison.OrdinalIgnoreCase) || id.Equals("dexterity", StringComparison.OrdinalIgnoreCase) || id.Equals("intelligence", StringComparison.OrdinalIgnoreCase) || id.Equals("vitality", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000154A8 File Offset: 0x000136A8
		private static double SumFlat(string id)
		{
			double num = 0.0;
			foreach (RpgStatMod rpgStatMod in RpgLoadout.MergedMods().Values)
			{
				if (!rpgStatMod.Percent && string.Equals(rpgStatMod.Id, id, StringComparison.OrdinalIgnoreCase))
				{
					num += rpgStatMod.Value;
				}
			}
			return num;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00015524 File Offset: 0x00013724
		public static IEnumerable<RpgStatMod> MergedCombatMods()
		{
			return RpgLoadout.MergedMods().Values;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00015530 File Offset: 0x00013730
		private static Dictionary<string, RpgStatMod> MergedMods()
		{
			RpgProgress.Ensure();
			Dictionary<string, RpgStatMod> dictionary = new Dictionary<string, RpgStatMod>(StringComparer.OrdinalIgnoreCase);
			foreach (RpgItemData rpgItemData in RpgLoadout.EquippedItems())
			{
				if (rpgItemData.IsIdentified && RpgLoadout.MeetsLevelRequirement(rpgItemData) && RpgAccessoryGates.ContributesStats(rpgItemData, null))
				{
					RpgLoadout.Merge(dictionary, rpgItemData.Primary);
					foreach (string shorthand in rpgItemData.Details)
					{
						RpgLoadout.Merge(dictionary, shorthand);
					}
					foreach (string shorthand2 in rpgItemData.EffectiveAbilityLines())
					{
						RpgLoadout.Merge(dictionary, shorthand2);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00015640 File Offset: 0x00013840
		private static void Merge(Dictionary<string, RpgStatMod> merged, [Nullable(2)] string shorthand)
		{
			RpgStatMod rpgStatMod = RpgStatMod.Parse(shorthand);
			if (rpgStatMod == null || string.IsNullOrWhiteSpace(rpgStatMod.Id))
			{
				return;
			}
			string key = rpgStatMod.Id + "|" + (rpgStatMod.Percent ? "p" : "f");
			RpgStatMod rpgStatMod2;
			if (merged.TryGetValue(key, out rpgStatMod2))
			{
				merged[key] = new RpgStatMod
				{
					Id = rpgStatMod2.Id,
					Value = rpgStatMod2.Value + rpgStatMod.Value,
					Percent = rpgStatMod2.Percent
				};
				return;
			}
			merged[key] = rpgStatMod;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000156D5 File Offset: 0x000138D5
		// Note: this type is marked as 'beforefieldinit'.
		static RpgLoadout()
		{
		}

		// Token: 0x040000F6 RID: 246
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private static List<RpgItemData> _bag;

		// Token: 0x040000F7 RID: 247
		[Nullable(new byte[]
		{
			1,
			2
		})]
		private static readonly Dictionary<RpgEquipSlot, RpgItemData> Equipped = new Dictionary<RpgEquipSlot, RpgItemData>();

		// Token: 0x02000103 RID: 259
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000766 RID: 1894 RVA: 0x00026A11 File Offset: 0x00024C11
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000767 RID: 1895 RVA: 0x00026A1D File Offset: 0x00024C1D
			public <>c()
			{
			}

			// Token: 0x06000768 RID: 1896 RVA: 0x00026A25 File Offset: 0x00024C25
			[NullableContext(0)]
			internal bool <CanEquipRarePlus>b__10_0(RpgItemData x)
			{
				return x.IsRarePlus;
			}

			// Token: 0x06000769 RID: 1897 RVA: 0x00026A2D File Offset: 0x00024C2D
			[NullableContext(0)]
			internal bool <CanEquipRarePlus>b__10_1(RpgItemData x)
			{
				return x.IsRarePlus;
			}

			// Token: 0x0600076A RID: 1898 RVA: 0x00026A35 File Offset: 0x00024C35
			[NullableContext(0)]
			internal bool <TryRemoveManyByInstanceIds>b__18_0(string x)
			{
				return !string.IsNullOrWhiteSpace(x);
			}

			// Token: 0x0600076B RID: 1899 RVA: 0x00026A40 File Offset: 0x00024C40
			[NullableContext(0)]
			internal string <EffectsSummary>b__19_0(RpgStatMod m)
			{
				return m.Id;
			}

			// Token: 0x0600076C RID: 1900 RVA: 0x00026A48 File Offset: 0x00024C48
			[NullableContext(0)]
			internal string <EffectsSummary>b__19_1(RpgStatMod m)
			{
				if (!(m.Id == "damage_reduction"))
				{
					return m.FormatAffix();
				}
				return m.FormatDetail();
			}

			// Token: 0x040003B6 RID: 950
			[Nullable(0)]
			public static readonly RpgLoadout.<>c <>9 = new RpgLoadout.<>c();

			// Token: 0x040003B7 RID: 951
			[Nullable(0)]
			public static Func<RpgItemData, bool> <>9__10_0;

			// Token: 0x040003B8 RID: 952
			[Nullable(0)]
			public static Func<RpgItemData, bool> <>9__10_1;

			// Token: 0x040003B9 RID: 953
			[Nullable(0)]
			public static Func<string, bool> <>9__18_0;

			// Token: 0x040003BA RID: 954
			[Nullable(0)]
			public static Func<RpgStatMod, string> <>9__19_0;

			// Token: 0x040003BB RID: 955
			[Nullable(0)]
			public static Func<RpgStatMod, string> <>9__19_1;
		}

		// Token: 0x02000104 RID: 260
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0
		{
			// Token: 0x0600076D RID: 1901 RVA: 0x00026A69 File Offset: 0x00024C69
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x0600076E RID: 1902 RVA: 0x00026A71 File Offset: 0x00024C71
			internal bool <FindBagIndex>b__0(RpgItemData x)
			{
				return x == this.item || (!string.IsNullOrEmpty(this.item.InstanceId) && x.InstanceId == this.item.InstanceId);
			}

			// Token: 0x040003BC RID: 956
			[Nullable(0)]
			public RpgItemData item;
		}

		// Token: 0x02000105 RID: 261
		[CompilerGenerated]
		private sealed class <>c__DisplayClass17_0
		{
			// Token: 0x0600076F RID: 1903 RVA: 0x00026AA8 File Offset: 0x00024CA8
			public <>c__DisplayClass17_0()
			{
			}

			// Token: 0x06000770 RID: 1904 RVA: 0x00026AB0 File Offset: 0x00024CB0
			internal bool <TryReplaceByInstanceId>b__0(RpgItemData x)
			{
				return !string.IsNullOrEmpty(x.InstanceId) && string.Equals(x.InstanceId, this.instanceId, StringComparison.Ordinal);
			}

			// Token: 0x040003BD RID: 957
			[Nullable(0)]
			public string instanceId;
		}

		// Token: 0x02000106 RID: 262
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x06000771 RID: 1905 RVA: 0x00026AD3 File Offset: 0x00024CD3
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x06000772 RID: 1906 RVA: 0x00026ADB File Offset: 0x00024CDB
			[NullableContext(0)]
			internal bool <AddToBag>b__0(RpgItemData x)
			{
				return x.InstanceId == this.copy.InstanceId;
			}

			// Token: 0x06000773 RID: 1907 RVA: 0x00026AF3 File Offset: 0x00024CF3
			[NullableContext(0)]
			internal bool <AddToBag>b__1(RpgItemData x)
			{
				return x != null && x.InstanceId == this.copy.InstanceId;
			}

			// Token: 0x040003BE RID: 958
			[Nullable(0)]
			public RpgItemData copy;
		}

		// Token: 0x02000107 RID: 263
		[CompilerGenerated]
		private sealed class <AllItems>d__16 : IEnumerable<RpgItemData>, IEnumerable, IEnumerator<RpgItemData>, IEnumerator, IDisposable
		{
			// Token: 0x06000774 RID: 1908 RVA: 0x00026B10 File Offset: 0x00024D10
			[DebuggerHidden]
			public <AllItems>d__16(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000775 RID: 1909 RVA: 0x00026B2C File Offset: 0x00024D2C
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

			// Token: 0x06000776 RID: 1910 RVA: 0x00026BB4 File Offset: 0x00024DB4
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
						this.<>1__state = -1;
						RpgProgress.Ensure();
						this.<>7__wrap1 = RpgLoadout.EquippedItems().GetEnumerator();
						this.<>1__state = -3;
						break;
					case 1:
						this.<>1__state = -3;
						break;
					case 2:
						this.<>1__state = -4;
						goto IL_C9;
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
					this.<>7__wrap2 = RpgLoadout._bag.GetEnumerator();
					this.<>1__state = -4;
					IL_C9:
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

			// Token: 0x06000777 RID: 1911 RVA: 0x00026CC8 File Offset: 0x00024EC8
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (this.<>7__wrap1 != null)
				{
					this.<>7__wrap1.Dispose();
				}
			}

			// Token: 0x06000778 RID: 1912 RVA: 0x00026CE4 File Offset: 0x00024EE4
			private void <>m__Finally2()
			{
				this.<>1__state = -1;
				((IDisposable)this.<>7__wrap2).Dispose();
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x06000779 RID: 1913 RVA: 0x00026CFE File Offset: 0x00024EFE
			RpgItemData IEnumerator<RpgItemData>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600077A RID: 1914 RVA: 0x00026D06 File Offset: 0x00024F06
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x0600077B RID: 1915 RVA: 0x00026D0D File Offset: 0x00024F0D
			object IEnumerator.Current
			{
				[DebuggerHidden]
				[return: Nullable(0)]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600077C RID: 1916 RVA: 0x00026D18 File Offset: 0x00024F18
			[DebuggerHidden]
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			IEnumerator<RpgItemData> IEnumerable<RpgItemData>.GetEnumerator()
			{
				RpgLoadout.<AllItems>d__16 result;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					result = this;
				}
				else
				{
					result = new RpgLoadout.<AllItems>d__16(0);
				}
				return result;
			}

			// Token: 0x0600077D RID: 1917 RVA: 0x00026D4F File Offset: 0x00024F4F
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<SpireTheRpg.Core.RpgItemData>.GetEnumerator();
			}

			// Token: 0x040003BF RID: 959
			private int <>1__state;

			// Token: 0x040003C0 RID: 960
			private RpgItemData <>2__current;

			// Token: 0x040003C1 RID: 961
			private int <>l__initialThreadId;

			// Token: 0x040003C2 RID: 962
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private IEnumerator<RpgItemData> <>7__wrap1;

			// Token: 0x040003C3 RID: 963
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private List<RpgItemData>.Enumerator <>7__wrap2;
		}

		// Token: 0x02000108 RID: 264
		[CompilerGenerated]
		private sealed class <EquippedItems>d__5 : IEnumerable<RpgItemData>, IEnumerable, IEnumerator<RpgItemData>, IEnumerator, IDisposable
		{
			// Token: 0x0600077E RID: 1918 RVA: 0x00026D57 File Offset: 0x00024F57
			[DebuggerHidden]
			public <EquippedItems>d__5(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600077F RID: 1919 RVA: 0x00026D71 File Offset: 0x00024F71
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				this.<>7__wrap1 = null;
				this.<>1__state = -2;
			}

			// Token: 0x06000780 RID: 1920 RVA: 0x00026D84 File Offset: 0x00024F84
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					RpgProgress.Ensure();
					this.<>7__wrap1 = Enum.GetValues<RpgEquipSlot>();
					this.<>7__wrap2 = 0;
					goto IL_72;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				IL_64:
				this.<>7__wrap2++;
				IL_72:
				if (this.<>7__wrap2 >= this.<>7__wrap1.Length)
				{
					this.<>7__wrap1 = null;
					return false;
				}
				RpgEquipSlot key = this.<>7__wrap1[this.<>7__wrap2];
				RpgItemData valueOrDefault = RpgLoadout.Equipped.GetValueOrDefault(key);
				if (valueOrDefault != null)
				{
					this.<>2__current = valueOrDefault;
					this.<>1__state = 1;
					return true;
				}
				goto IL_64;
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x06000781 RID: 1921 RVA: 0x00026E1B File Offset: 0x0002501B
			RpgItemData IEnumerator<RpgItemData>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000782 RID: 1922 RVA: 0x00026E23 File Offset: 0x00025023
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x06000783 RID: 1923 RVA: 0x00026E2A File Offset: 0x0002502A
			object IEnumerator.Current
			{
				[DebuggerHidden]
				[return: Nullable(0)]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000784 RID: 1924 RVA: 0x00026E34 File Offset: 0x00025034
			[DebuggerHidden]
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			IEnumerator<RpgItemData> IEnumerable<RpgItemData>.GetEnumerator()
			{
				RpgLoadout.<EquippedItems>d__5 result;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					result = this;
				}
				else
				{
					result = new RpgLoadout.<EquippedItems>d__5(0);
				}
				return result;
			}

			// Token: 0x06000785 RID: 1925 RVA: 0x00026E6B File Offset: 0x0002506B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<SpireTheRpg.Core.RpgItemData>.GetEnumerator();
			}

			// Token: 0x040003C4 RID: 964
			private int <>1__state;

			// Token: 0x040003C5 RID: 965
			private RpgItemData <>2__current;

			// Token: 0x040003C6 RID: 966
			private int <>l__initialThreadId;

			// Token: 0x040003C7 RID: 967
			[Nullable(0)]
			private RpgEquipSlot[] <>7__wrap1;

			// Token: 0x040003C8 RID: 968
			private int <>7__wrap2;
		}
	}
}
