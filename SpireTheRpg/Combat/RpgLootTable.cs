using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using SpireTheRpg.Core;

namespace SpireTheRpg.Combat
{
	// Token: 0x02000072 RID: 114
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgLootTable
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x0001BA82 File Offset: 0x00019C82
		public static ulong RunSeedOf(Player player)
		{
			return RpgRng.RunSeedOf(player);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001BA8A File Offset: 0x00019C8A
		public static void ResetSharedVictoryCount()
		{
			RpgLootTable._sharedVictoryCount = 0;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001BA92 File Offset: 0x00019C92
		public static void NoteSharedVictory()
		{
			RpgLootTable._sharedVictoryCount++;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001BAA0 File Offset: 0x00019CA0
		[NullableContext(2)]
		internal static ulong TimeSalt(Player player = null)
		{
			RunManager instance = RunManager.Instance;
			if (((instance != null) ? instance.NetService : null) == null || instance.NetService.Type == 1)
			{
				return (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
			}
			int? num;
			if (player == null)
			{
				num = null;
			}
			else
			{
				IRunState runState = player.RunState;
				num = ((runState != null) ? new int?(runState.TotalFloor) : null);
			}
			int? num2 = num;
			ulong a = (ulong)num2.GetValueOrDefault();
			ulong b = (ulong)RpgLootTable._sharedVictoryCount;
			return RpgRng.Mix(a, b, 1414090053UL, 0UL, 0UL);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001BB2C File Offset: 0x00019D2C
		public static Rng DropRng(Player player, RoomType roomType)
		{
			IRunState runState = player.RunState;
			ulong c = (ulong)((runState != null) ? runState.TotalFloor : 0);
			return RpgRng.FromSeed(RpgRng.Mix(RpgRng.RunSeedOf(player), player.NetId, c, roomType, RpgLootTable.TimeSalt(player)));
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001BB6C File Offset: 0x00019D6C
		public static Rng LootRng(Player player, RoomType roomType = 1, int salt = 0)
		{
			IRunState runState = player.RunState;
			ulong c = (ulong)((runState != null) ? runState.TotalFloor : 0);
			return RpgRng.FromSeed(RpgRng.Mix(RpgRng.RunSeedOf(player), player.NetId, c, roomType ^ (ulong)salt * (ulong)-1640531527, RpgLootTable.TimeSalt(player) ^ 1380732756UL));
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001BBC0 File Offset: 0x00019DC0
		[NullableContext(2)]
		public static int RollXp(RoomType roomType, Rng rng = null)
		{
			ValueTuple<int, int> valueTuple;
			switch (roomType)
			{
			case 1:
				valueTuple = new ValueTuple<int, int>(RpgEffectiveConfig.MonsterXpMin, RpgEffectiveConfig.MonsterXpMax);
				break;
			case 2:
				valueTuple = new ValueTuple<int, int>(RpgEffectiveConfig.EliteXpMin, RpgEffectiveConfig.EliteXpMax);
				break;
			case 3:
				valueTuple = new ValueTuple<int, int>(RpgEffectiveConfig.BossXpMin, RpgEffectiveConfig.BossXpMax);
				break;
			default:
				valueTuple = new ValueTuple<int, int>(RpgEffectiveConfig.MonsterXpMin / 2, RpgEffectiveConfig.MonsterXpMax / 2);
				break;
			}
			ValueTuple<int, int> valueTuple2 = valueTuple;
			int item = valueTuple2.Item1;
			int item2 = valueTuple2.Item2;
			return RpgLootTable.RollRange(item, item2, rng);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001BC43 File Offset: 0x00019E43
		[NullableContext(2)]
		private static int RollRange(int min, int max, Rng rng)
		{
			min = Math.Max(0, min);
			max = Math.Max(0, max);
			if (max < min)
			{
				int num = max;
				max = min;
				min = num;
			}
			return RpgItemRoller.NextInt(rng, min, max + 1);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001BC6C File Offset: 0x00019E6C
		public static double DropChance(RoomType roomType)
		{
			double result;
			switch (roomType)
			{
			case 1:
				result = RpgLootTable.Clamp01(RpgEffectiveConfig.MonsterDropChance);
				break;
			case 2:
				result = RpgLootTable.Clamp01(RpgEffectiveConfig.EliteDropChance);
				break;
			case 3:
				result = RpgLootTable.Clamp01(RpgEffectiveConfig.BossDropChance);
				break;
			default:
				result = RpgLootTable.Clamp01(RpgEffectiveConfig.MonsterDropChance) * 0.5;
				break;
			}
			return result;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001BCCC File Offset: 0x00019ECC
		public static bool IsMonsterAllRarities(RoomType roomType)
		{
			return roomType == 1 && SpireTheRpgConfig.EnableDevCheats && SpireTheRpgConfig.MonsterDropsAllRarities;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001BCE0 File Offset: 0x00019EE0
		[NullableContext(2)]
		public static bool RollDrop(RoomType roomType, Rng rng = null)
		{
			return RpgLootTable.IsMonsterAllRarities(roomType) || RpgItemRoller.NextDouble(rng) < RpgLootTable.DropChance(roomType);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001BCFC File Offset: 0x00019EFC
		[NullableContext(2)]
		public static RpgItemData RollOne(RpgItemRarity rarity, Rng rng = null)
		{
			IReadOnlyList<RpgItemTemplate> all = RpgItemCatalog.All;
			if (all.Count == 0)
			{
				return null;
			}
			List<IGrouping<RpgEquipSlot, RpgItemTemplate>> list = (from t in all
			group t by t.EquipSlot).ToList<IGrouping<RpgEquipSlot, RpgItemTemplate>>();
			List<RpgItemTemplate> list2 = list[RpgItemRoller.NextInt(rng, list.Count)].ToList<RpgItemTemplate>();
			return RpgItemRoller.Roll(list2[RpgItemRoller.NextInt(rng, list2.Count)], rarity, rng);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001BD78 File Offset: 0x00019F78
		public static IReadOnlyList<RpgItemData> RollChoices(RoomType roomType, int? count = null, [Nullable(2)] Rng rng = null)
		{
			IReadOnlyCollection<RpgItemTemplate> all = RpgItemCatalog.All;
			List<RpgItemData> list = new List<RpgItemData>();
			if (all.Count == 0)
			{
				return list;
			}
			if (RpgLootTable.IsMonsterAllRarities(roomType))
			{
				RpgItemRarity[] allRarities = RpgLootTable.AllRarities;
				for (int i = 0; i < allRarities.Length; i++)
				{
					RpgItemData rpgItemData = RpgLootTable.RollOne(allRarities[i], rng);
					if (rpgItemData != null)
					{
						list.Add(rpgItemData);
					}
				}
				return list;
			}
			int num = Math.Clamp(count.GetValueOrDefault(1), 1, 5);
			Dictionary<RpgItemRarity, int> weights = RpgLootTable.RarityWeights(roomType);
			for (int j = 0; j < num; j++)
			{
				RpgItemData rpgItemData2 = RpgLootTable.RollOne(RpgLootTable.PickRarity(weights, rng), rng);
				if (rpgItemData2 != null)
				{
					list.Add(rpgItemData2);
				}
			}
			return list;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001BE18 File Offset: 0x0001A018
		private static Dictionary<RpgItemRarity, int> RarityWeights(RoomType roomType)
		{
			Dictionary<RpgItemRarity, int> result;
			if (roomType != 2)
			{
				if (roomType == 3)
				{
					Dictionary<RpgItemRarity, int> dictionary = new Dictionary<RpgItemRarity, int>();
					dictionary[RpgItemRarity.Common] = Math.Max(0, RpgEffectiveConfig.BossCommonWeight);
					dictionary[RpgItemRarity.Uncommon] = Math.Max(0, RpgEffectiveConfig.BossUncommonWeight);
					dictionary[RpgItemRarity.Rare] = Math.Max(0, RpgEffectiveConfig.BossRareWeight);
					dictionary[RpgItemRarity.Epic] = Math.Max(0, RpgEffectiveConfig.BossEpicWeight);
					dictionary[RpgItemRarity.Legendary] = Math.Max(0, RpgEffectiveConfig.BossLegendaryWeight);
					result = dictionary;
				}
				else
				{
					Dictionary<RpgItemRarity, int> dictionary2 = new Dictionary<RpgItemRarity, int>();
					dictionary2[RpgItemRarity.Common] = Math.Max(0, RpgEffectiveConfig.MonsterCommonWeight);
					dictionary2[RpgItemRarity.Uncommon] = Math.Max(0, RpgEffectiveConfig.MonsterUncommonWeight);
					dictionary2[RpgItemRarity.Rare] = Math.Max(0, RpgEffectiveConfig.MonsterRareWeight);
					dictionary2[RpgItemRarity.Epic] = Math.Max(0, RpgEffectiveConfig.MonsterEpicWeight);
					dictionary2[RpgItemRarity.Legendary] = Math.Max(0, RpgEffectiveConfig.MonsterLegendaryWeight);
					result = dictionary2;
				}
			}
			else
			{
				Dictionary<RpgItemRarity, int> dictionary3 = new Dictionary<RpgItemRarity, int>();
				dictionary3[RpgItemRarity.Common] = Math.Max(0, RpgEffectiveConfig.EliteCommonWeight);
				dictionary3[RpgItemRarity.Uncommon] = Math.Max(0, RpgEffectiveConfig.EliteUncommonWeight);
				dictionary3[RpgItemRarity.Rare] = Math.Max(0, RpgEffectiveConfig.EliteRareWeight);
				dictionary3[RpgItemRarity.Epic] = Math.Max(0, RpgEffectiveConfig.EliteEpicWeight);
				dictionary3[RpgItemRarity.Legendary] = Math.Max(0, RpgEffectiveConfig.EliteLegendaryWeight);
				result = dictionary3;
			}
			return result;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001BF58 File Offset: 0x0001A158
		private static RpgItemRarity PickRarity(Dictionary<RpgItemRarity, int> weights, [Nullable(2)] Rng rng)
		{
			int num = weights.Values.Sum();
			if (num <= 0)
			{
				return RpgItemRarity.Common;
			}
			int num2 = RpgItemRoller.NextInt(rng, num);
			foreach (KeyValuePair<RpgItemRarity, int> keyValuePair in weights)
			{
				RpgItemRarity rpgItemRarity;
				int num3;
				keyValuePair.Deconstruct(out rpgItemRarity, out num3);
				RpgItemRarity result = rpgItemRarity;
				int num4 = num3;
				if (num4 > 0)
				{
					if (num2 < num4)
					{
						return result;
					}
					num2 -= num4;
				}
			}
			return RpgItemRarity.Common;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001BFE4 File Offset: 0x0001A1E4
		private static double Clamp01(double value)
		{
			if (value > 1.0)
			{
				value /= 100.0;
			}
			return Math.Clamp(value, 0.0, 1.0);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001C017 File Offset: 0x0001A217
		// Note: this type is marked as 'beforefieldinit'.
		static RpgLootTable()
		{
		}

		// Token: 0x040001A5 RID: 421
		private static int _sharedVictoryCount;

		// Token: 0x040001A6 RID: 422
		public static readonly RpgItemRarity[] AllRarities = new RpgItemRarity[]
		{
			RpgItemRarity.Common,
			RpgItemRarity.Uncommon,
			RpgItemRarity.Rare,
			RpgItemRarity.Epic,
			RpgItemRarity.Legendary
		};

		// Token: 0x02000145 RID: 325
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600083D RID: 2109 RVA: 0x0002B422 File Offset: 0x00029622
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600083E RID: 2110 RVA: 0x0002B42E File Offset: 0x0002962E
			public <>c()
			{
			}

			// Token: 0x0600083F RID: 2111 RVA: 0x0002B436 File Offset: 0x00029636
			[NullableContext(0)]
			internal RpgEquipSlot <RollOne>b__13_0(RpgItemTemplate t)
			{
				return t.EquipSlot;
			}

			// Token: 0x040004C2 RID: 1218
			[Nullable(0)]
			public static readonly RpgLootTable.<>c <>9 = new RpgLootTable.<>c();

			// Token: 0x040004C3 RID: 1219
			[Nullable(0)]
			public static Func<RpgItemTemplate, RpgEquipSlot> <>9__13_0;
		}
	}
}
