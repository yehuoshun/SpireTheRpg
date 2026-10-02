using System;
using System.Runtime.CompilerServices;

namespace SpireTheRpg.Core
{
	// Token: 0x0200003D RID: 61
	internal static class RpgEffectiveConfig
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0001191E File Offset: 0x0000FB1E
		public static bool HasRunSnapshot
		{
			get
			{
				return RpgEffectiveConfig._run != null;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00011928 File Offset: 0x0000FB28
		[NullableContext(1)]
		public static void SetRunSnapshot(RpgRunConfigSnapshot snapshot)
		{
			RpgEffectiveConfig._run = snapshot;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00011930 File Offset: 0x0000FB30
		public static void ClearRunSnapshot()
		{
			RpgEffectiveConfig._run = null;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00011938 File Offset: 0x0000FB38
		public static bool EnableMetaEquipment
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.EnableMetaEquipment;
				}
				return run.EnableMetaEquipment;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0001194E File Offset: 0x0000FB4E
		public static bool KeepLootOnAbandon
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.KeepLootOnAbandon;
				}
				return run.KeepLootOnAbandon;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00011964 File Offset: 0x0000FB64
		public static double MonsterDropChance
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.MonsterDropChance;
				}
				return run.MonsterDropChance;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0001197A File Offset: 0x0000FB7A
		public static double EliteDropChance
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.EliteDropChance;
				}
				return run.EliteDropChance;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00011990 File Offset: 0x0000FB90
		public static double BossDropChance
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.BossDropChance;
				}
				return run.BossDropChance;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000119A6 File Offset: 0x0000FBA6
		public static int MonsterXpMin
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.MonsterXpMin;
				}
				return run.MonsterXpMin;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000230 RID: 560 RVA: 0x000119BC File Offset: 0x0000FBBC
		public static int MonsterXpMax
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.MonsterXpMax;
				}
				return run.MonsterXpMax;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000119D2 File Offset: 0x0000FBD2
		public static int EliteXpMin
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.EliteXpMin;
				}
				return run.EliteXpMin;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000232 RID: 562 RVA: 0x000119E8 File Offset: 0x0000FBE8
		public static int EliteXpMax
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.EliteXpMax;
				}
				return run.EliteXpMax;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000233 RID: 563 RVA: 0x000119FE File Offset: 0x0000FBFE
		public static int BossXpMin
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.BossXpMin;
				}
				return run.BossXpMin;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00011A14 File Offset: 0x0000FC14
		public static int BossXpMax
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.BossXpMax;
				}
				return run.BossXpMax;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00011A2A File Offset: 0x0000FC2A
		public static int MonsterCommonWeight
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.MonsterCommonWeight;
				}
				return run.MonsterCommonWeight;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00011A40 File Offset: 0x0000FC40
		public static int MonsterUncommonWeight
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.MonsterUncommonWeight;
				}
				return run.MonsterUncommonWeight;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00011A56 File Offset: 0x0000FC56
		public static int MonsterRareWeight
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.MonsterRareWeight;
				}
				return run.MonsterRareWeight;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00011A6C File Offset: 0x0000FC6C
		public static int MonsterEpicWeight
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.MonsterEpicWeight;
				}
				return run.MonsterEpicWeight;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00011A82 File Offset: 0x0000FC82
		public static int MonsterLegendaryWeight
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.MonsterLegendaryWeight;
				}
				return run.MonsterLegendaryWeight;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00011A98 File Offset: 0x0000FC98
		public static int EliteCommonWeight
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.EliteCommonWeight;
				}
				return run.EliteCommonWeight;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00011AAE File Offset: 0x0000FCAE
		public static int EliteUncommonWeight
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.EliteUncommonWeight;
				}
				return run.EliteUncommonWeight;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		public static int EliteRareWeight
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.EliteRareWeight;
				}
				return run.EliteRareWeight;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00011ADA File Offset: 0x0000FCDA
		public static int EliteEpicWeight
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.EliteEpicWeight;
				}
				return run.EliteEpicWeight;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00011AF0 File Offset: 0x0000FCF0
		public static int EliteLegendaryWeight
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.EliteLegendaryWeight;
				}
				return run.EliteLegendaryWeight;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00011B06 File Offset: 0x0000FD06
		public static int BossCommonWeight
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.BossCommonWeight;
				}
				return run.BossCommonWeight;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00011B1C File Offset: 0x0000FD1C
		public static int BossUncommonWeight
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.BossUncommonWeight;
				}
				return run.BossUncommonWeight;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00011B32 File Offset: 0x0000FD32
		public static int BossRareWeight
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.BossRareWeight;
				}
				return run.BossRareWeight;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00011B48 File Offset: 0x0000FD48
		public static int BossEpicWeight
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.BossEpicWeight;
				}
				return run.BossEpicWeight;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00011B5E File Offset: 0x0000FD5E
		public static int BossLegendaryWeight
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.BossLegendaryWeight;
				}
				return run.BossLegendaryWeight;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00011B74 File Offset: 0x0000FD74
		public static int RandomItemShopPrice
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.RandomItemShopPrice;
				}
				return run.RandomItemShopPrice;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00011B8A File Offset: 0x0000FD8A
		public static int MaxRareEquipped
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.MaxRareEquipped;
				}
				return run.MaxRareEquipped;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00011BA0 File Offset: 0x0000FDA0
		public static int EarlyCombatBonusTurns
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.EarlyCombatBonusTurns;
				}
				return run.EarlyCombatBonusTurns;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00011BB6 File Offset: 0x0000FDB6
		public static int IntensifyHpCost
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.IntensifyHpCost;
				}
				return run.IntensifyHpCost;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00011BCC File Offset: 0x0000FDCC
		public static bool EnableCurses
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.EnableCurses;
				}
				return run.EnableCurses;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00011BE2 File Offset: 0x0000FDE2
		public static double CurseChanceRare
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.CurseChanceRare;
				}
				return run.CurseChanceRare;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00011BF8 File Offset: 0x0000FDF8
		public static double CurseChanceEpic
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.CurseChanceEpic;
				}
				return run.CurseChanceEpic;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00011C0E File Offset: 0x0000FE0E
		public static double CurseChanceLegendary
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.CurseChanceLegendary;
				}
				return run.CurseChanceLegendary;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00011C24 File Offset: 0x0000FE24
		public static double UnidentifiedChanceRarePlus
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.UnidentifiedChanceRarePlus;
				}
				return run.UnidentifiedChanceRarePlus;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00011C3A File Offset: 0x0000FE3A
		public static int CleanseHpCost
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.CleanseHpCost;
				}
				return run.CleanseHpCost;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00011C50 File Offset: 0x0000FE50
		public static bool EnableScrolls
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.EnableScrolls;
				}
				return run.EnableScrolls;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00011C66 File Offset: 0x0000FE66
		public static double ScrollDropChance
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.ScrollDropChance;
				}
				return run.ScrollDropChance;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00011C7C File Offset: 0x0000FE7C
		public static int ScrollGoldAmount
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.ScrollGoldAmount;
				}
				return run.ScrollGoldAmount;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00011C92 File Offset: 0x0000FE92
		public static bool EnableTrade
		{
			get
			{
				RpgRunConfigSnapshot run = RpgEffectiveConfig._run;
				if (run == null)
				{
					return SpireTheRpgConfig.EnableTrade;
				}
				return run.EnableTrade;
			}
		}

		// Token: 0x040000AD RID: 173
		[Nullable(2)]
		private static RpgRunConfigSnapshot _run;
	}
}
