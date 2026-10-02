using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;

namespace SpireTheRpg.Core
{
	// Token: 0x02000037 RID: 55
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgActiveLoadout
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00010707 File Offset: 0x0000E907
		public static bool UseMeta
		{
			get
			{
				return RpgEffectiveConfig.EnableMetaEquipment;
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0001070E File Offset: 0x0000E90E
		[NullableContext(2)]
		private static ulong Id(Player player)
		{
			return RpgNet.NetIdOf(player);
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00010716 File Offset: 0x0000E916
		public static IReadOnlyList<RpgItemData> Bag
		{
			get
			{
				if (!RpgActiveLoadout.UseMeta)
				{
					return RpgRunLoadout.Bag;
				}
				return RpgLoadout.Bag;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0001072A File Offset: 0x0000E92A
		public static IReadOnlyList<RpgItemData> BagOf([Nullable(2)] Player player)
		{
			if (!RpgActiveLoadout.UseMeta)
			{
				return RpgRunLoadout.BagOf(RpgActiveLoadout.Id(player));
			}
			return RpgLoadout.Bag;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00010744 File Offset: 0x0000E944
		[NullableContext(2)]
		public static RpgItemData GetEquipped(RpgEquipSlot slot)
		{
			if (!RpgActiveLoadout.UseMeta)
			{
				return RpgRunLoadout.GetEquipped(slot);
			}
			return RpgLoadout.GetEquipped(slot);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0001075A File Offset: 0x0000E95A
		[NullableContext(2)]
		public static RpgItemData GetEquipped(Player player, RpgEquipSlot slot)
		{
			if (!RpgActiveLoadout.UseMeta)
			{
				return RpgRunLoadout.GetEquipped(RpgActiveLoadout.Id(player), slot);
			}
			return RpgLoadout.GetEquipped(slot);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00010776 File Offset: 0x0000E976
		public static IEnumerable<RpgItemData> EquippedItems()
		{
			if (!RpgActiveLoadout.UseMeta)
			{
				return RpgRunLoadout.EquippedItems();
			}
			return RpgLoadout.EquippedItems();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0001078A File Offset: 0x0000E98A
		public static IEnumerable<RpgItemData> EquippedItems([Nullable(2)] Player player)
		{
			if (player != null && !RpgNet.IsLocal(player))
			{
				return RpgRunLoadout.EquippedItemsOf(RpgActiveLoadout.Id(player));
			}
			if (!RpgActiveLoadout.UseMeta)
			{
				return RpgRunLoadout.EquippedItemsOf(RpgActiveLoadout.Id(player));
			}
			return RpgLoadout.EquippedItems();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000107BB File Offset: 0x0000E9BB
		public static bool TryEquipFromBag(RpgItemData item)
		{
			bool flag = RpgActiveLoadout.UseMeta ? RpgLoadout.TryEquipFromBag(item) : RpgRunLoadout.TryEquipFromBag(item);
			if (flag)
			{
				RpgEquippedGearSync.Request(null);
				if (RpgActiveLoadout.UseMeta)
				{
					RpgLoadoutNetSync.PushEquipped(RpgNet.LocalNetId());
				}
			}
			return flag;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000107EC File Offset: 0x0000E9EC
		public static bool TryUnequip(RpgEquipSlot slot)
		{
			bool flag = RpgActiveLoadout.UseMeta ? RpgLoadout.TryUnequip(slot) : RpgRunLoadout.TryUnequip(slot);
			if (flag)
			{
				RpgEquippedGearSync.Request(null);
				if (RpgActiveLoadout.UseMeta)
				{
					RpgLoadoutNetSync.PushEquipped(RpgNet.LocalNetId());
				}
			}
			return flag;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00010820 File Offset: 0x0000EA20
		[NullableContext(2)]
		public static void UnequipUnidentified(Player player = null)
		{
			if (RpgActiveLoadout.UseMeta)
			{
				if (player == null || RpgNet.IsLocal(player))
				{
					RpgLoadout.UnequipUnidentified();
					RpgEquippedGearSync.Request(null);
					RpgLoadoutNetSync.PushEquipped(RpgNet.LocalNetId());
				}
				return;
			}
			RpgRunLoadout.UnequipUnidentified(RpgActiveLoadout.Id(player));
			if (player == null || RpgNet.IsLocal(player))
			{
				RpgEquippedGearSync.Request(player);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00010871 File Offset: 0x0000EA71
		public static bool TryRemoveFromBag(RpgItemData item)
		{
			if (!RpgActiveLoadout.UseMeta)
			{
				return RpgRunLoadout.TryRemoveFromBag(item);
			}
			return RpgLoadout.TryRemoveFromBag(item);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00010887 File Offset: 0x0000EA87
		public static IEnumerable<RpgItemData> AllItems()
		{
			if (!RpgActiveLoadout.UseMeta)
			{
				return RpgRunLoadout.AllItems();
			}
			return RpgLoadout.AllItems();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0001089B File Offset: 0x0000EA9B
		public static IEnumerable<RpgItemData> AllItems([Nullable(2)] Player player)
		{
			if (player != null && !RpgNet.IsLocal(player))
			{
				return RpgRunLoadout.AllItemsOf(RpgActiveLoadout.Id(player));
			}
			if (!RpgActiveLoadout.UseMeta)
			{
				return RpgRunLoadout.AllItemsOf(RpgActiveLoadout.Id(player));
			}
			return RpgLoadout.AllItems();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000108CC File Offset: 0x0000EACC
		public static bool TryReplaceByInstanceId(string instanceId, RpgItemData replacement)
		{
			if (!RpgActiveLoadout.UseMeta)
			{
				return RpgRunLoadout.TryReplaceByInstanceId(instanceId, replacement);
			}
			return RpgLoadout.TryReplaceByInstanceId(instanceId, replacement, true);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000108E5 File Offset: 0x0000EAE5
		public static bool TryRemoveManyByInstanceIds(IEnumerable<string> instanceIds)
		{
			if (RpgActiveLoadout.UseMeta)
			{
				return RpgLoadout.TryRemoveManyByInstanceIds(instanceIds, true);
			}
			return RpgRunLoadout.TryRemoveManyByInstanceIds(instanceIds);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000108FC File Offset: 0x0000EAFC
		public static void AddToBag(RpgItemData item)
		{
			RpgActiveLoadout.AddToBag(null, item);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00010905 File Offset: 0x0000EB05
		public static void AddToBag([Nullable(2)] Player player, RpgItemData item)
		{
			if (RpgActiveLoadout.UseMeta)
			{
				RpgLoadout.AddToBag(item, true);
				return;
			}
			RpgRunLoadout.AddToBag(RpgActiveLoadout.Id(player), item);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00010922 File Offset: 0x0000EB22
		public static bool MeetsLevelRequirement(RpgItemData item)
		{
			return RpgLoadout.MeetsLevelRequirement(item);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0001092C File Offset: 0x0000EB2C
		public static int FlatStat(string id, [Nullable(2)] Player player = null)
		{
			if (RpgActiveLoadout.UseMeta && (player == null || RpgNet.IsLocal(player)))
			{
				return RpgLoadout.FlatStat(id);
			}
			double num = RpgActiveLoadout.SumFlat(id, player);
			if (RpgActiveLoadout.IsPrimaryAttribute(id))
			{
				num += RpgActiveLoadout.SumFlat("all_stats", player);
			}
			return (int)Math.Round(num);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00010978 File Offset: 0x0000EB78
		public static double PercentStat(string id, [Nullable(2)] Player player = null)
		{
			if (RpgActiveLoadout.UseMeta && (player == null || RpgNet.IsLocal(player)))
			{
				return RpgLoadout.PercentStat(id);
			}
			double num = 0.0;
			foreach (RpgStatMod rpgStatMod in RpgActiveLoadout.MergedCombatMods(player))
			{
				if (rpgStatMod.Percent && string.Equals(rpgStatMod.Id, id, StringComparison.OrdinalIgnoreCase))
				{
					num += rpgStatMod.Value;
				}
			}
			return num;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00010A04 File Offset: 0x0000EC04
		public static IEnumerable<RpgStatMod> MergedCombatMods([Nullable(2)] Player player = null)
		{
			if (RpgActiveLoadout.UseMeta && (player == null || RpgNet.IsLocal(player)))
			{
				return RpgLoadout.MergedCombatMods();
			}
			Dictionary<string, RpgStatMod> dictionary = new Dictionary<string, RpgStatMod>(StringComparer.OrdinalIgnoreCase);
			foreach (RpgItemData rpgItemData in RpgActiveLoadout.EquippedItems(player))
			{
				if (rpgItemData.IsIdentified && RpgActiveLoadout.MeetsLevelRequirement(rpgItemData) && RpgAccessoryGates.ContributesStats(rpgItemData, player))
				{
					RpgActiveLoadout.Merge(dictionary, rpgItemData.Primary);
					foreach (string shorthand in rpgItemData.Details)
					{
						RpgActiveLoadout.Merge(dictionary, shorthand);
					}
					foreach (string shorthand2 in rpgItemData.EffectiveAbilityLines())
					{
						RpgActiveLoadout.Merge(dictionary, shorthand2);
					}
				}
			}
			return dictionary.Values;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00010B2C File Offset: 0x0000ED2C
		public static string EffectsSummary(int maxLines = 8, [Nullable(2)] Player player = null)
		{
			if (RpgActiveLoadout.UseMeta)
			{
				return RpgLoadout.EffectsSummary(maxLines);
			}
			List<string> list = RpgActiveLoadout.MergedCombatMods(player).OrderBy((RpgStatMod m) => m.Id, StringComparer.OrdinalIgnoreCase).Select(delegate(RpgStatMod m)
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

		// Token: 0x060001F3 RID: 499 RVA: 0x00010BC9 File Offset: 0x0000EDC9
		private static bool IsPrimaryAttribute(string id)
		{
			return id.Equals("strength", StringComparison.OrdinalIgnoreCase) || id.Equals("dexterity", StringComparison.OrdinalIgnoreCase) || id.Equals("intelligence", StringComparison.OrdinalIgnoreCase) || id.Equals("vitality", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00010C04 File Offset: 0x0000EE04
		private static double SumFlat(string id, [Nullable(2)] Player player)
		{
			double num = 0.0;
			foreach (RpgStatMod rpgStatMod in RpgActiveLoadout.MergedCombatMods(player))
			{
				if (!rpgStatMod.Percent && string.Equals(rpgStatMod.Id, id, StringComparison.OrdinalIgnoreCase))
				{
					num += rpgStatMod.Value;
				}
			}
			return num;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00010C78 File Offset: 0x0000EE78
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

		// Token: 0x020000DB RID: 219
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006C2 RID: 1730 RVA: 0x000234AB File Offset: 0x000216AB
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006C3 RID: 1731 RVA: 0x000234B7 File Offset: 0x000216B7
			public <>c()
			{
			}

			// Token: 0x060006C4 RID: 1732 RVA: 0x000234BF File Offset: 0x000216BF
			[NullableContext(0)]
			internal string <EffectsSummary>b__24_0(RpgStatMod m)
			{
				return m.Id;
			}

			// Token: 0x060006C5 RID: 1733 RVA: 0x000234C7 File Offset: 0x000216C7
			[NullableContext(0)]
			internal string <EffectsSummary>b__24_1(RpgStatMod m)
			{
				if (!(m.Id == "damage_reduction"))
				{
					return m.FormatAffix();
				}
				return m.FormatDetail();
			}

			// Token: 0x040002FE RID: 766
			[Nullable(0)]
			public static readonly RpgActiveLoadout.<>c <>9 = new RpgActiveLoadout.<>c();

			// Token: 0x040002FF RID: 767
			[Nullable(0)]
			public static Func<RpgStatMod, string> <>9__24_0;

			// Token: 0x04000300 RID: 768
			[Nullable(0)]
			public static Func<RpgStatMod, string> <>9__24_1;
		}
	}
}
