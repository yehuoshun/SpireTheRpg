using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Random;

namespace SpireTheRpg.Core
{
	// Token: 0x02000047 RID: 71
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgItemFusion
	{
		// Token: 0x060002BA RID: 698 RVA: 0x00013252 File Offset: 0x00011452
		public static bool CanFuse(RpgItemRarity rarity)
		{
			return rarity < RpgItemRarity.Legendary;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00013258 File Offset: 0x00011458
		public static RpgItemRarity? NextRarity(RpgItemRarity rarity)
		{
			RpgItemRarity? result;
			switch (rarity)
			{
			case RpgItemRarity.Common:
				result = new RpgItemRarity?(RpgItemRarity.Uncommon);
				break;
			case RpgItemRarity.Uncommon:
				result = new RpgItemRarity?(RpgItemRarity.Rare);
				break;
			case RpgItemRarity.Rare:
				result = new RpgItemRarity?(RpgItemRarity.Epic);
				break;
			case RpgItemRarity.Epic:
				result = new RpgItemRarity?(RpgItemRarity.Legendary);
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000132AC File Offset: 0x000114AC
		public static bool HasFusableGroup(IEnumerable<RpgItemData> items)
		{
			return (from x in items
			where RpgItemFusion.CanFuse(x.Rarity)
			group x by new ValueTuple<RpgItemRarity, RpgEquipSlot>(x.Rarity, x.EquipSlot)).Any(([TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})] IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData> g) => g.Count<RpgItemData>() >= 5);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00013328 File Offset: 0x00011528
		[return: Nullable(new byte[]
		{
			2,
			1
		})]
		public static List<RpgItemData> PickAutoMaterials(IEnumerable<RpgItemData> items, RpgEquipSlot? preferSlot = null, RpgItemRarity? preferRarity = null)
		{
			List<RpgItemData> list = (from x in items
			where RpgItemFusion.CanFuse(x.Rarity)
			select x).ToList<RpgItemData>();
			if (preferSlot != null)
			{
				List<RpgItemData> list2 = RpgItemFusion.PickBestGroup((from x in list
				where x.EquipSlot == preferSlot.Value
				select x).ToList<RpgItemData>(), preferRarity);
				if (list2 != null)
				{
					return list2;
				}
			}
			return RpgItemFusion.PickBestGroup(list, preferRarity);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000133A4 File Offset: 0x000115A4
		[return: Nullable(new byte[]
		{
			2,
			1
		})]
		private static List<RpgItemData> PickBestGroup(List<RpgItemData> pool, RpgItemRarity? preferRarity)
		{
			List<IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData>> list = (from x in pool
			group x by new ValueTuple<RpgItemRarity, RpgEquipSlot>(x.Rarity, x.EquipSlot) into g
			where g.Count<RpgItemData>() >= 5
			select g).ToList<IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData>>();
			if (list.Count == 0)
			{
				return null;
			}
			IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData> source;
			if (preferRarity != null)
			{
				IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData> grouping = (from g in list
				where g.Key.Item1 == preferRarity.Value
				orderby g.Count<RpgItemData>() descending
				select g).FirstOrDefault<IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData>>();
				IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData> grouping2;
				if ((grouping2 = grouping) == null)
				{
					grouping2 = (from g in list
					orderby g.Key.Item1 descending, g.Count<RpgItemData>() descending
					select g).First<IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData>>();
				}
				source = grouping2;
			}
			else
			{
				source = (from g in list
				orderby g.Key.Item1 descending, g.Count<RpgItemData>() descending
				select g).First<IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData>>();
			}
			return source.Take(5).ToList<RpgItemData>();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00013514 File Offset: 0x00011714
		public static bool TryFuse(IReadOnlyList<RpgItemData> materials, [Nullable(2)] out RpgItemData result)
		{
			return RpgItemFusion.TryFuse(materials, null, out result);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00013520 File Offset: 0x00011720
		[NullableContext(2)]
		public static bool TryFuse([Nullable(1)] IReadOnlyList<RpgItemData> materials, Rng rng, out RpgItemData result)
		{
			result = null;
			if (materials.Count != 5)
			{
				return false;
			}
			RpgItemRarity rarity = materials[0].Rarity;
			RpgEquipSlot slot = materials[0].EquipSlot;
			if (!RpgItemFusion.CanFuse(rarity))
			{
				return false;
			}
			if (materials.Any((RpgItemData x) => x.Rarity != rarity || x.EquipSlot != slot))
			{
				return false;
			}
			RpgItemRarity? rpgItemRarity = RpgItemFusion.NextRarity(rarity);
			if (rpgItemRarity == null)
			{
				return false;
			}
			List<RpgItemTemplate> list = (from t in RpgItemCatalog.All
			where t.EquipSlot == slot
			select t).ToList<RpgItemTemplate>();
			if (list.Count == 0)
			{
				return false;
			}
			RpgItemTemplate template = list[RpgItemRoller.NextInt(rng, list.Count)];
			result = RpgItemRoller.Roll(template, rpgItemRarity.Value, rng);
			return result != null;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000135F0 File Offset: 0x000117F0
		public static void SelfCheck()
		{
			try
			{
				if (RpgItemFusion.NextRarity(RpgItemRarity.Rare).GetValueOrDefault() != RpgItemRarity.Epic)
				{
					throw new Exception("next rare");
				}
				if (RpgItemFusion.CanFuse(RpgItemRarity.Legendary))
				{
					throw new Exception("legendary fuse");
				}
				if (RpgItemFusion.NextRarity(RpgItemRarity.Legendary) != null)
				{
					throw new Exception("legendary next");
				}
				RpgItemData rpgItemData;
				if (!RpgItemFusion.TryFuse((from _ in Enumerable.Range(0, 5)
				select new RpgItemData
				{
					Id = "fusion_mat",
					Rarity = RpgItemRarity.Common,
					EquipSlot = RpgEquipSlot.Weapon
				}).ToList<RpgItemData>(), out rpgItemData) || rpgItemData == null)
				{
					throw new Exception("fuse failed");
				}
				if (rpgItemData.Rarity != RpgItemRarity.Uncommon || rpgItemData.EquipSlot != RpgEquipSlot.Weapon)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 2);
					defaultInterpolatedStringHandler.AppendLiteral("result=");
					defaultInterpolatedStringHandler.AppendFormatted<RpgItemRarity>(rpgItemData.Rarity);
					defaultInterpolatedStringHandler.AppendLiteral("/");
					defaultInterpolatedStringHandler.AppendFormatted<RpgEquipSlot>(rpgItemData.EquipSlot);
					throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				SpireTheRpgMainFile.Logger.Info("RpgItemFusion self-check ok id=" + rpgItemData.Id, 1);
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RpgItemFusion self-check FAIL: " + ex.Message, 1);
			}
		}

		// Token: 0x040000E0 RID: 224
		public const int MaterialCount = 5;

		// Token: 0x020000F8 RID: 248
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600072C RID: 1836 RVA: 0x00026497 File Offset: 0x00024697
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600072D RID: 1837 RVA: 0x000264A3 File Offset: 0x000246A3
			public <>c()
			{
			}

			// Token: 0x0600072E RID: 1838 RVA: 0x000264AB File Offset: 0x000246AB
			[NullableContext(0)]
			internal bool <HasFusableGroup>b__3_0(RpgItemData x)
			{
				return RpgItemFusion.CanFuse(x.Rarity);
			}

			// Token: 0x0600072F RID: 1839 RVA: 0x000264B8 File Offset: 0x000246B8
			[NullableContext(0)]
			[return: TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})]
			internal ValueTuple<RpgItemRarity, RpgEquipSlot> <HasFusableGroup>b__3_1(RpgItemData x)
			{
				return new ValueTuple<RpgItemRarity, RpgEquipSlot>(x.Rarity, x.EquipSlot);
			}

			// Token: 0x06000730 RID: 1840 RVA: 0x000264CB File Offset: 0x000246CB
			[NullableContext(0)]
			internal bool <HasFusableGroup>b__3_2([TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})] IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData> g)
			{
				return g.Count<RpgItemData>() >= 5;
			}

			// Token: 0x06000731 RID: 1841 RVA: 0x000264D9 File Offset: 0x000246D9
			[NullableContext(0)]
			internal bool <PickAutoMaterials>b__4_0(RpgItemData x)
			{
				return RpgItemFusion.CanFuse(x.Rarity);
			}

			// Token: 0x06000732 RID: 1842 RVA: 0x000264E6 File Offset: 0x000246E6
			[NullableContext(0)]
			[return: TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})]
			internal ValueTuple<RpgItemRarity, RpgEquipSlot> <PickBestGroup>b__5_0(RpgItemData x)
			{
				return new ValueTuple<RpgItemRarity, RpgEquipSlot>(x.Rarity, x.EquipSlot);
			}

			// Token: 0x06000733 RID: 1843 RVA: 0x000264F9 File Offset: 0x000246F9
			[NullableContext(0)]
			internal bool <PickBestGroup>b__5_1([TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})] IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData> g)
			{
				return g.Count<RpgItemData>() >= 5;
			}

			// Token: 0x06000734 RID: 1844 RVA: 0x00026507 File Offset: 0x00024707
			[NullableContext(0)]
			internal int <PickBestGroup>b__5_5([TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})] IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData> g)
			{
				return g.Count<RpgItemData>();
			}

			// Token: 0x06000735 RID: 1845 RVA: 0x0002650F File Offset: 0x0002470F
			[NullableContext(0)]
			internal RpgItemRarity <PickBestGroup>b__5_6([TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})] IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData> g)
			{
				return g.Key.Item1;
			}

			// Token: 0x06000736 RID: 1846 RVA: 0x0002651C File Offset: 0x0002471C
			[NullableContext(0)]
			internal int <PickBestGroup>b__5_7([TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})] IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData> g)
			{
				return g.Count<RpgItemData>();
			}

			// Token: 0x06000737 RID: 1847 RVA: 0x00026524 File Offset: 0x00024724
			[NullableContext(0)]
			internal RpgItemRarity <PickBestGroup>b__5_2([TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})] IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData> g)
			{
				return g.Key.Item1;
			}

			// Token: 0x06000738 RID: 1848 RVA: 0x00026531 File Offset: 0x00024731
			[NullableContext(0)]
			internal int <PickBestGroup>b__5_3([TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})] IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData> g)
			{
				return g.Count<RpgItemData>();
			}

			// Token: 0x06000739 RID: 1849 RVA: 0x00026539 File Offset: 0x00024739
			[NullableContext(0)]
			internal RpgItemData <SelfCheck>b__8_0(int _)
			{
				return new RpgItemData
				{
					Id = "fusion_mat",
					Rarity = RpgItemRarity.Common,
					EquipSlot = RpgEquipSlot.Weapon
				};
			}

			// Token: 0x04000394 RID: 916
			[Nullable(0)]
			public static readonly RpgItemFusion.<>c <>9 = new RpgItemFusion.<>c();

			// Token: 0x04000395 RID: 917
			[Nullable(0)]
			public static Func<RpgItemData, bool> <>9__3_0;

			// Token: 0x04000396 RID: 918
			[TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})]
			[Nullable(0)]
			public static Func<RpgItemData, ValueTuple<RpgItemRarity, RpgEquipSlot>> <>9__3_1;

			// Token: 0x04000397 RID: 919
			[TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})]
			[Nullable(0)]
			public static Func<IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData>, bool> <>9__3_2;

			// Token: 0x04000398 RID: 920
			[Nullable(0)]
			public static Func<RpgItemData, bool> <>9__4_0;

			// Token: 0x04000399 RID: 921
			[TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})]
			[Nullable(0)]
			public static Func<RpgItemData, ValueTuple<RpgItemRarity, RpgEquipSlot>> <>9__5_0;

			// Token: 0x0400039A RID: 922
			[TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})]
			[Nullable(0)]
			public static Func<IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData>, bool> <>9__5_1;

			// Token: 0x0400039B RID: 923
			[TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})]
			[Nullable(0)]
			public static Func<IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData>, int> <>9__5_5;

			// Token: 0x0400039C RID: 924
			[TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})]
			[Nullable(0)]
			public static Func<IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData>, RpgItemRarity> <>9__5_6;

			// Token: 0x0400039D RID: 925
			[TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})]
			[Nullable(0)]
			public static Func<IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData>, int> <>9__5_7;

			// Token: 0x0400039E RID: 926
			[TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})]
			[Nullable(0)]
			public static Func<IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData>, RpgItemRarity> <>9__5_2;

			// Token: 0x0400039F RID: 927
			[TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})]
			[Nullable(0)]
			public static Func<IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData>, int> <>9__5_3;

			// Token: 0x040003A0 RID: 928
			[Nullable(0)]
			public static Func<int, RpgItemData> <>9__8_0;
		}

		// Token: 0x020000F9 RID: 249
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x0600073A RID: 1850 RVA: 0x00026559 File Offset: 0x00024759
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x0600073B RID: 1851 RVA: 0x00026561 File Offset: 0x00024761
			[NullableContext(0)]
			internal bool <PickAutoMaterials>b__1(RpgItemData x)
			{
				return x.EquipSlot == this.preferSlot.Value;
			}

			// Token: 0x040003A1 RID: 929
			public RpgEquipSlot? preferSlot;
		}

		// Token: 0x020000FA RID: 250
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x0600073C RID: 1852 RVA: 0x00026576 File Offset: 0x00024776
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x0600073D RID: 1853 RVA: 0x0002657E File Offset: 0x0002477E
			[NullableContext(0)]
			internal bool <PickBestGroup>b__4([TupleElementNames(new string[]
			{
				"Rarity",
				"EquipSlot"
			})] IGrouping<ValueTuple<RpgItemRarity, RpgEquipSlot>, RpgItemData> g)
			{
				return g.Key.Item1 == this.preferRarity.Value;
			}

			// Token: 0x040003A2 RID: 930
			public RpgItemRarity? preferRarity;
		}

		// Token: 0x020000FB RID: 251
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x0600073E RID: 1854 RVA: 0x00026598 File Offset: 0x00024798
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x0600073F RID: 1855 RVA: 0x000265A0 File Offset: 0x000247A0
			[NullableContext(0)]
			internal bool <TryFuse>b__0(RpgItemData x)
			{
				return x.Rarity != this.rarity || x.EquipSlot != this.slot;
			}

			// Token: 0x06000740 RID: 1856 RVA: 0x000265C3 File Offset: 0x000247C3
			[NullableContext(0)]
			internal bool <TryFuse>b__1(RpgItemTemplate t)
			{
				return t.EquipSlot == this.slot;
			}

			// Token: 0x040003A3 RID: 931
			public RpgItemRarity rarity;

			// Token: 0x040003A4 RID: 932
			public RpgEquipSlot slot;
		}
	}
}
