using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SpireTheRpg.Core
{
	// Token: 0x02000057 RID: 87
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RpgRunConfigSnapshot
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0001735B File Offset: 0x0001555B
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00017363 File Offset: 0x00015563
		public bool EnableMetaEquipment
		{
			[CompilerGenerated]
			get
			{
				return this.<EnableMetaEquipment>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EnableMetaEquipment>k__BackingField = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0001736C File Offset: 0x0001556C
		// (set) Token: 0x0600036A RID: 874 RVA: 0x00017374 File Offset: 0x00015574
		public bool KeepLootOnAbandon
		{
			[CompilerGenerated]
			get
			{
				return this.<KeepLootOnAbandon>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<KeepLootOnAbandon>k__BackingField = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0001737D File Offset: 0x0001557D
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00017385 File Offset: 0x00015585
		public double MonsterDropChance
		{
			[CompilerGenerated]
			get
			{
				return this.<MonsterDropChance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MonsterDropChance>k__BackingField = value;
			}
		} = 0.4;

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0001738E File Offset: 0x0001558E
		// (set) Token: 0x0600036E RID: 878 RVA: 0x00017396 File Offset: 0x00015596
		public double EliteDropChance
		{
			[CompilerGenerated]
			get
			{
				return this.<EliteDropChance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EliteDropChance>k__BackingField = value;
			}
		} = 0.8;

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0001739F File Offset: 0x0001559F
		// (set) Token: 0x06000370 RID: 880 RVA: 0x000173A7 File Offset: 0x000155A7
		public double BossDropChance
		{
			[CompilerGenerated]
			get
			{
				return this.<BossDropChance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BossDropChance>k__BackingField = value;
			}
		} = 1.0;

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000371 RID: 881 RVA: 0x000173B0 File Offset: 0x000155B0
		// (set) Token: 0x06000372 RID: 882 RVA: 0x000173B8 File Offset: 0x000155B8
		public int MonsterXpMin
		{
			[CompilerGenerated]
			get
			{
				return this.<MonsterXpMin>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MonsterXpMin>k__BackingField = value;
			}
		} = 8;

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000373 RID: 883 RVA: 0x000173C1 File Offset: 0x000155C1
		// (set) Token: 0x06000374 RID: 884 RVA: 0x000173C9 File Offset: 0x000155C9
		public int MonsterXpMax
		{
			[CompilerGenerated]
			get
			{
				return this.<MonsterXpMax>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MonsterXpMax>k__BackingField = value;
			}
		} = 18;

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000375 RID: 885 RVA: 0x000173D2 File Offset: 0x000155D2
		// (set) Token: 0x06000376 RID: 886 RVA: 0x000173DA File Offset: 0x000155DA
		public int EliteXpMin
		{
			[CompilerGenerated]
			get
			{
				return this.<EliteXpMin>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EliteXpMin>k__BackingField = value;
			}
		} = 22;

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000377 RID: 887 RVA: 0x000173E3 File Offset: 0x000155E3
		// (set) Token: 0x06000378 RID: 888 RVA: 0x000173EB File Offset: 0x000155EB
		public int EliteXpMax
		{
			[CompilerGenerated]
			get
			{
				return this.<EliteXpMax>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EliteXpMax>k__BackingField = value;
			}
		} = 40;

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000379 RID: 889 RVA: 0x000173F4 File Offset: 0x000155F4
		// (set) Token: 0x0600037A RID: 890 RVA: 0x000173FC File Offset: 0x000155FC
		public int BossXpMin
		{
			[CompilerGenerated]
			get
			{
				return this.<BossXpMin>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BossXpMin>k__BackingField = value;
			}
		} = 45;

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00017405 File Offset: 0x00015605
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0001740D File Offset: 0x0001560D
		public int BossXpMax
		{
			[CompilerGenerated]
			get
			{
				return this.<BossXpMax>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BossXpMax>k__BackingField = value;
			}
		} = 80;

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00017416 File Offset: 0x00015616
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0001741E File Offset: 0x0001561E
		public int MonsterCommonWeight
		{
			[CompilerGenerated]
			get
			{
				return this.<MonsterCommonWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MonsterCommonWeight>k__BackingField = value;
			}
		} = 90;

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00017427 File Offset: 0x00015627
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0001742F File Offset: 0x0001562F
		public int MonsterUncommonWeight
		{
			[CompilerGenerated]
			get
			{
				return this.<MonsterUncommonWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MonsterUncommonWeight>k__BackingField = value;
			}
		} = 10;

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00017438 File Offset: 0x00015638
		// (set) Token: 0x06000382 RID: 898 RVA: 0x00017440 File Offset: 0x00015640
		public int MonsterRareWeight
		{
			[CompilerGenerated]
			get
			{
				return this.<MonsterRareWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MonsterRareWeight>k__BackingField = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00017449 File Offset: 0x00015649
		// (set) Token: 0x06000384 RID: 900 RVA: 0x00017451 File Offset: 0x00015651
		public int MonsterEpicWeight
		{
			[CompilerGenerated]
			get
			{
				return this.<MonsterEpicWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MonsterEpicWeight>k__BackingField = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0001745A File Offset: 0x0001565A
		// (set) Token: 0x06000386 RID: 902 RVA: 0x00017462 File Offset: 0x00015662
		public int MonsterLegendaryWeight
		{
			[CompilerGenerated]
			get
			{
				return this.<MonsterLegendaryWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MonsterLegendaryWeight>k__BackingField = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0001746B File Offset: 0x0001566B
		// (set) Token: 0x06000388 RID: 904 RVA: 0x00017473 File Offset: 0x00015673
		public int EliteCommonWeight
		{
			[CompilerGenerated]
			get
			{
				return this.<EliteCommonWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EliteCommonWeight>k__BackingField = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0001747C File Offset: 0x0001567C
		// (set) Token: 0x0600038A RID: 906 RVA: 0x00017484 File Offset: 0x00015684
		public int EliteUncommonWeight
		{
			[CompilerGenerated]
			get
			{
				return this.<EliteUncommonWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EliteUncommonWeight>k__BackingField = value;
			}
		} = 55;

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0001748D File Offset: 0x0001568D
		// (set) Token: 0x0600038C RID: 908 RVA: 0x00017495 File Offset: 0x00015695
		public int EliteRareWeight
		{
			[CompilerGenerated]
			get
			{
				return this.<EliteRareWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EliteRareWeight>k__BackingField = value;
			}
		} = 40;

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0001749E File Offset: 0x0001569E
		// (set) Token: 0x0600038E RID: 910 RVA: 0x000174A6 File Offset: 0x000156A6
		public int EliteEpicWeight
		{
			[CompilerGenerated]
			get
			{
				return this.<EliteEpicWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EliteEpicWeight>k__BackingField = value;
			}
		} = 5;

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600038F RID: 911 RVA: 0x000174AF File Offset: 0x000156AF
		// (set) Token: 0x06000390 RID: 912 RVA: 0x000174B7 File Offset: 0x000156B7
		public int EliteLegendaryWeight
		{
			[CompilerGenerated]
			get
			{
				return this.<EliteLegendaryWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EliteLegendaryWeight>k__BackingField = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000391 RID: 913 RVA: 0x000174C0 File Offset: 0x000156C0
		// (set) Token: 0x06000392 RID: 914 RVA: 0x000174C8 File Offset: 0x000156C8
		public int BossCommonWeight
		{
			[CompilerGenerated]
			get
			{
				return this.<BossCommonWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BossCommonWeight>k__BackingField = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000393 RID: 915 RVA: 0x000174D1 File Offset: 0x000156D1
		// (set) Token: 0x06000394 RID: 916 RVA: 0x000174D9 File Offset: 0x000156D9
		public int BossUncommonWeight
		{
			[CompilerGenerated]
			get
			{
				return this.<BossUncommonWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BossUncommonWeight>k__BackingField = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000395 RID: 917 RVA: 0x000174E2 File Offset: 0x000156E2
		// (set) Token: 0x06000396 RID: 918 RVA: 0x000174EA File Offset: 0x000156EA
		public int BossRareWeight
		{
			[CompilerGenerated]
			get
			{
				return this.<BossRareWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BossRareWeight>k__BackingField = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000397 RID: 919 RVA: 0x000174F3 File Offset: 0x000156F3
		// (set) Token: 0x06000398 RID: 920 RVA: 0x000174FB File Offset: 0x000156FB
		public int BossEpicWeight
		{
			[CompilerGenerated]
			get
			{
				return this.<BossEpicWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BossEpicWeight>k__BackingField = value;
			}
		} = 55;

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00017504 File Offset: 0x00015704
		// (set) Token: 0x0600039A RID: 922 RVA: 0x0001750C File Offset: 0x0001570C
		public int BossLegendaryWeight
		{
			[CompilerGenerated]
			get
			{
				return this.<BossLegendaryWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BossLegendaryWeight>k__BackingField = value;
			}
		} = 45;

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00017515 File Offset: 0x00015715
		// (set) Token: 0x0600039C RID: 924 RVA: 0x0001751D File Offset: 0x0001571D
		public int RandomItemShopPrice
		{
			[CompilerGenerated]
			get
			{
				return this.<RandomItemShopPrice>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RandomItemShopPrice>k__BackingField = value;
			}
		} = 100;

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00017526 File Offset: 0x00015726
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0001752E File Offset: 0x0001572E
		public int MaxRareEquipped
		{
			[CompilerGenerated]
			get
			{
				return this.<MaxRareEquipped>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MaxRareEquipped>k__BackingField = value;
			}
		} = 2;

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00017537 File Offset: 0x00015737
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0001753F File Offset: 0x0001573F
		public int EarlyCombatBonusTurns
		{
			[CompilerGenerated]
			get
			{
				return this.<EarlyCombatBonusTurns>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EarlyCombatBonusTurns>k__BackingField = value;
			}
		} = 1;

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00017548 File Offset: 0x00015748
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x00017550 File Offset: 0x00015750
		public int IntensifyHpCost
		{
			[CompilerGenerated]
			get
			{
				return this.<IntensifyHpCost>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IntensifyHpCost>k__BackingField = value;
			}
		} = 8;

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00017559 File Offset: 0x00015759
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00017561 File Offset: 0x00015761
		public bool EnableCurses
		{
			[CompilerGenerated]
			get
			{
				return this.<EnableCurses>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EnableCurses>k__BackingField = value;
			}
		} = true;

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0001756A File Offset: 0x0001576A
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x00017572 File Offset: 0x00015772
		public double CurseChanceRare
		{
			[CompilerGenerated]
			get
			{
				return this.<CurseChanceRare>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CurseChanceRare>k__BackingField = value;
			}
		} = 0.25;

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0001757B File Offset: 0x0001577B
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x00017583 File Offset: 0x00015783
		public double CurseChanceEpic
		{
			[CompilerGenerated]
			get
			{
				return this.<CurseChanceEpic>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CurseChanceEpic>k__BackingField = value;
			}
		} = 0.45;

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0001758C File Offset: 0x0001578C
		// (set) Token: 0x060003AA RID: 938 RVA: 0x00017594 File Offset: 0x00015794
		public double CurseChanceLegendary
		{
			[CompilerGenerated]
			get
			{
				return this.<CurseChanceLegendary>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CurseChanceLegendary>k__BackingField = value;
			}
		} = 0.65;

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0001759D File Offset: 0x0001579D
		// (set) Token: 0x060003AC RID: 940 RVA: 0x000175A5 File Offset: 0x000157A5
		public double UnidentifiedChanceRarePlus
		{
			[CompilerGenerated]
			get
			{
				return this.<UnidentifiedChanceRarePlus>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UnidentifiedChanceRarePlus>k__BackingField = value;
			}
		} = 0.7;

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003AD RID: 941 RVA: 0x000175AE File Offset: 0x000157AE
		// (set) Token: 0x060003AE RID: 942 RVA: 0x000175B6 File Offset: 0x000157B6
		public int CleanseHpCost
		{
			[CompilerGenerated]
			get
			{
				return this.<CleanseHpCost>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CleanseHpCost>k__BackingField = value;
			}
		} = 12;

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000175BF File Offset: 0x000157BF
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x000175C7 File Offset: 0x000157C7
		public bool EnableScrolls
		{
			[CompilerGenerated]
			get
			{
				return this.<EnableScrolls>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EnableScrolls>k__BackingField = value;
			}
		} = true;

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x000175D0 File Offset: 0x000157D0
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x000175D8 File Offset: 0x000157D8
		public double ScrollDropChance
		{
			[CompilerGenerated]
			get
			{
				return this.<ScrollDropChance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ScrollDropChance>k__BackingField = value;
			}
		} = 0.12;

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x000175E1 File Offset: 0x000157E1
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x000175E9 File Offset: 0x000157E9
		public int ScrollGoldAmount
		{
			[CompilerGenerated]
			get
			{
				return this.<ScrollGoldAmount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ScrollGoldAmount>k__BackingField = value;
			}
		} = 50;

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x000175F2 File Offset: 0x000157F2
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x000175FA File Offset: 0x000157FA
		public bool EnableTrade
		{
			[CompilerGenerated]
			get
			{
				return this.<EnableTrade>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EnableTrade>k__BackingField = value;
			}
		} = true;

		// Token: 0x060003B7 RID: 951 RVA: 0x00017604 File Offset: 0x00015804
		public static RpgRunConfigSnapshot FromLocal()
		{
			return new RpgRunConfigSnapshot
			{
				EnableMetaEquipment = SpireTheRpgConfig.EnableMetaEquipment,
				KeepLootOnAbandon = SpireTheRpgConfig.KeepLootOnAbandon,
				MonsterDropChance = SpireTheRpgConfig.MonsterDropChance,
				EliteDropChance = SpireTheRpgConfig.EliteDropChance,
				BossDropChance = SpireTheRpgConfig.BossDropChance,
				MonsterXpMin = SpireTheRpgConfig.MonsterXpMin,
				MonsterXpMax = SpireTheRpgConfig.MonsterXpMax,
				EliteXpMin = SpireTheRpgConfig.EliteXpMin,
				EliteXpMax = SpireTheRpgConfig.EliteXpMax,
				BossXpMin = SpireTheRpgConfig.BossXpMin,
				BossXpMax = SpireTheRpgConfig.BossXpMax,
				MonsterCommonWeight = SpireTheRpgConfig.MonsterCommonWeight,
				MonsterUncommonWeight = SpireTheRpgConfig.MonsterUncommonWeight,
				MonsterRareWeight = SpireTheRpgConfig.MonsterRareWeight,
				MonsterEpicWeight = SpireTheRpgConfig.MonsterEpicWeight,
				MonsterLegendaryWeight = SpireTheRpgConfig.MonsterLegendaryWeight,
				EliteCommonWeight = SpireTheRpgConfig.EliteCommonWeight,
				EliteUncommonWeight = SpireTheRpgConfig.EliteUncommonWeight,
				EliteRareWeight = SpireTheRpgConfig.EliteRareWeight,
				EliteEpicWeight = SpireTheRpgConfig.EliteEpicWeight,
				EliteLegendaryWeight = SpireTheRpgConfig.EliteLegendaryWeight,
				BossCommonWeight = SpireTheRpgConfig.BossCommonWeight,
				BossUncommonWeight = SpireTheRpgConfig.BossUncommonWeight,
				BossRareWeight = SpireTheRpgConfig.BossRareWeight,
				BossEpicWeight = SpireTheRpgConfig.BossEpicWeight,
				BossLegendaryWeight = SpireTheRpgConfig.BossLegendaryWeight,
				RandomItemShopPrice = SpireTheRpgConfig.RandomItemShopPrice,
				MaxRareEquipped = SpireTheRpgConfig.MaxRareEquipped,
				EarlyCombatBonusTurns = SpireTheRpgConfig.EarlyCombatBonusTurns,
				IntensifyHpCost = SpireTheRpgConfig.IntensifyHpCost,
				EnableCurses = SpireTheRpgConfig.EnableCurses,
				CurseChanceRare = SpireTheRpgConfig.CurseChanceRare,
				CurseChanceEpic = SpireTheRpgConfig.CurseChanceEpic,
				CurseChanceLegendary = SpireTheRpgConfig.CurseChanceLegendary,
				UnidentifiedChanceRarePlus = SpireTheRpgConfig.UnidentifiedChanceRarePlus,
				CleanseHpCost = SpireTheRpgConfig.CleanseHpCost,
				EnableScrolls = SpireTheRpgConfig.EnableScrolls,
				ScrollDropChance = SpireTheRpgConfig.ScrollDropChance,
				ScrollGoldAmount = SpireTheRpgConfig.ScrollGoldAmount,
				EnableTrade = SpireTheRpgConfig.EnableTrade
			};
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000177D0 File Offset: 0x000159D0
		public unsafe List<int> Encode()
		{
			int num = 41;
			List<int> list = new List<int>(num);
			CollectionsMarshal.SetCount<int>(list, num);
			Span<int> span = CollectionsMarshal.AsSpan<int>(list);
			int num2 = 0;
			*span[num2] = 1397772871;
			num2++;
			*span[num2] = ((this.EnableMetaEquipment > false) ? 1 : 0);
			num2++;
			*span[num2] = ((this.KeepLootOnAbandon > false) ? 1 : 0);
			num2++;
			*span[num2] = RpgRunConfigSnapshot.EncodeChance(this.MonsterDropChance);
			num2++;
			*span[num2] = RpgRunConfigSnapshot.EncodeChance(this.EliteDropChance);
			num2++;
			*span[num2] = RpgRunConfigSnapshot.EncodeChance(this.BossDropChance);
			num2++;
			*span[num2] = this.MonsterXpMin;
			num2++;
			*span[num2] = this.MonsterXpMax;
			num2++;
			*span[num2] = this.EliteXpMin;
			num2++;
			*span[num2] = this.EliteXpMax;
			num2++;
			*span[num2] = this.BossXpMin;
			num2++;
			*span[num2] = this.BossXpMax;
			num2++;
			*span[num2] = this.MonsterCommonWeight;
			num2++;
			*span[num2] = this.MonsterUncommonWeight;
			num2++;
			*span[num2] = this.MonsterRareWeight;
			num2++;
			*span[num2] = this.MonsterEpicWeight;
			num2++;
			*span[num2] = this.MonsterLegendaryWeight;
			num2++;
			*span[num2] = this.EliteCommonWeight;
			num2++;
			*span[num2] = this.EliteUncommonWeight;
			num2++;
			*span[num2] = this.EliteRareWeight;
			num2++;
			*span[num2] = this.EliteEpicWeight;
			num2++;
			*span[num2] = this.EliteLegendaryWeight;
			num2++;
			*span[num2] = this.BossCommonWeight;
			num2++;
			*span[num2] = this.BossUncommonWeight;
			num2++;
			*span[num2] = this.BossRareWeight;
			num2++;
			*span[num2] = this.BossEpicWeight;
			num2++;
			*span[num2] = this.BossLegendaryWeight;
			num2++;
			*span[num2] = this.RandomItemShopPrice;
			num2++;
			*span[num2] = this.MaxRareEquipped;
			num2++;
			*span[num2] = this.EarlyCombatBonusTurns;
			num2++;
			*span[num2] = this.IntensifyHpCost;
			num2++;
			*span[num2] = ((this.EnableCurses > false) ? 1 : 0);
			num2++;
			*span[num2] = RpgRunConfigSnapshot.EncodeChance(this.CurseChanceRare);
			num2++;
			*span[num2] = RpgRunConfigSnapshot.EncodeChance(this.CurseChanceEpic);
			num2++;
			*span[num2] = RpgRunConfigSnapshot.EncodeChance(this.CurseChanceLegendary);
			num2++;
			*span[num2] = RpgRunConfigSnapshot.EncodeChance(this.UnidentifiedChanceRarePlus);
			num2++;
			*span[num2] = this.CleanseHpCost;
			num2++;
			*span[num2] = ((this.EnableScrolls > false) ? 1 : 0);
			num2++;
			*span[num2] = RpgRunConfigSnapshot.EncodeChance(this.ScrollDropChance);
			num2++;
			*span[num2] = this.ScrollGoldAmount;
			num2++;
			*span[num2] = ((this.EnableTrade > false) ? 1 : 0);
			return list;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00017B34 File Offset: 0x00015D34
		public static bool TryDecode(IReadOnlyList<int> indexes, out RpgRunConfigSnapshot snapshot)
		{
			snapshot = RpgRunConfigSnapshot.FromLocal();
			if (indexes.Count < 27 || indexes[0] != 1397772871)
			{
				return false;
			}
			snapshot = new RpgRunConfigSnapshot
			{
				EnableMetaEquipment = (indexes[1] != 0),
				KeepLootOnAbandon = (indexes[2] != 0),
				MonsterDropChance = RpgRunConfigSnapshot.DecodeChance(indexes[3]),
				EliteDropChance = RpgRunConfigSnapshot.DecodeChance(indexes[4]),
				BossDropChance = RpgRunConfigSnapshot.DecodeChance(indexes[5]),
				MonsterXpMin = indexes[6],
				MonsterXpMax = indexes[7],
				EliteXpMin = indexes[8],
				EliteXpMax = indexes[9],
				BossXpMin = indexes[10],
				BossXpMax = indexes[11],
				MonsterCommonWeight = indexes[12],
				MonsterUncommonWeight = indexes[13],
				MonsterRareWeight = indexes[14],
				MonsterEpicWeight = indexes[15],
				MonsterLegendaryWeight = indexes[16],
				EliteCommonWeight = indexes[17],
				EliteUncommonWeight = indexes[18],
				EliteRareWeight = indexes[19],
				EliteEpicWeight = indexes[20],
				EliteLegendaryWeight = indexes[21],
				BossCommonWeight = indexes[22],
				BossUncommonWeight = indexes[23],
				BossRareWeight = indexes[24],
				BossEpicWeight = indexes[25],
				BossLegendaryWeight = indexes[26],
				RandomItemShopPrice = ((indexes.Count >= 28) ? Math.Max(0, indexes[27]) : 100),
				MaxRareEquipped = ((indexes.Count >= 29) ? Math.Clamp(indexes[28], 0, 6) : 2),
				EarlyCombatBonusTurns = ((indexes.Count >= 30) ? Math.Clamp(indexes[29], 0, 5) : 1),
				IntensifyHpCost = ((indexes.Count >= 31) ? Math.Max(0, indexes[30]) : 8),
				EnableCurses = (indexes.Count < 32 || indexes[31] != 0),
				CurseChanceRare = ((indexes.Count >= 33) ? RpgRunConfigSnapshot.DecodeChance(indexes[32]) : 0.25),
				CurseChanceEpic = ((indexes.Count >= 34) ? RpgRunConfigSnapshot.DecodeChance(indexes[33]) : 0.45),
				CurseChanceLegendary = ((indexes.Count >= 35) ? RpgRunConfigSnapshot.DecodeChance(indexes[34]) : 0.65),
				UnidentifiedChanceRarePlus = ((indexes.Count >= 36) ? RpgRunConfigSnapshot.DecodeChance(indexes[35]) : 0.7),
				CleanseHpCost = ((indexes.Count >= 37) ? Math.Max(0, indexes[36]) : 12),
				EnableScrolls = (indexes.Count < 38 || indexes[37] != 0),
				ScrollDropChance = ((indexes.Count >= 39) ? RpgRunConfigSnapshot.DecodeChance(indexes[38]) : 0.12),
				ScrollGoldAmount = ((indexes.Count >= 40) ? Math.Max(0, indexes[39]) : 50),
				EnableTrade = (indexes.Count < 41 || indexes[40] != 0)
			};
			return true;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00017ED0 File Offset: 0x000160D0
		private static int EncodeChance(double value)
		{
			if (value > 1.0)
			{
				value /= 100.0;
			}
			return (int)Math.Round(Math.Clamp(value, 0.0, 1.0) * 1000.0);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00017F1E File Offset: 0x0001611E
		private static double DecodeChance(int encoded)
		{
			return Math.Clamp((double)encoded / 1000.0, 0.0, 1.0);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00017F44 File Offset: 0x00016144
		public RpgRunConfigSnapshot()
		{
		}

		// Token: 0x04000119 RID: 281
		public const int SyncMagic = 1397772871;

		// Token: 0x0400011A RID: 282
		[CompilerGenerated]
		private readonly bool <EnableMetaEquipment>k__BackingField;

		// Token: 0x0400011B RID: 283
		[CompilerGenerated]
		private readonly bool <KeepLootOnAbandon>k__BackingField;

		// Token: 0x0400011C RID: 284
		[CompilerGenerated]
		private readonly double <MonsterDropChance>k__BackingField;

		// Token: 0x0400011D RID: 285
		[CompilerGenerated]
		private readonly double <EliteDropChance>k__BackingField;

		// Token: 0x0400011E RID: 286
		[CompilerGenerated]
		private readonly double <BossDropChance>k__BackingField;

		// Token: 0x0400011F RID: 287
		[CompilerGenerated]
		private readonly int <MonsterXpMin>k__BackingField;

		// Token: 0x04000120 RID: 288
		[CompilerGenerated]
		private readonly int <MonsterXpMax>k__BackingField;

		// Token: 0x04000121 RID: 289
		[CompilerGenerated]
		private readonly int <EliteXpMin>k__BackingField;

		// Token: 0x04000122 RID: 290
		[CompilerGenerated]
		private readonly int <EliteXpMax>k__BackingField;

		// Token: 0x04000123 RID: 291
		[CompilerGenerated]
		private readonly int <BossXpMin>k__BackingField;

		// Token: 0x04000124 RID: 292
		[CompilerGenerated]
		private readonly int <BossXpMax>k__BackingField;

		// Token: 0x04000125 RID: 293
		[CompilerGenerated]
		private readonly int <MonsterCommonWeight>k__BackingField;

		// Token: 0x04000126 RID: 294
		[CompilerGenerated]
		private readonly int <MonsterUncommonWeight>k__BackingField;

		// Token: 0x04000127 RID: 295
		[CompilerGenerated]
		private readonly int <MonsterRareWeight>k__BackingField;

		// Token: 0x04000128 RID: 296
		[CompilerGenerated]
		private readonly int <MonsterEpicWeight>k__BackingField;

		// Token: 0x04000129 RID: 297
		[CompilerGenerated]
		private readonly int <MonsterLegendaryWeight>k__BackingField;

		// Token: 0x0400012A RID: 298
		[CompilerGenerated]
		private readonly int <EliteCommonWeight>k__BackingField;

		// Token: 0x0400012B RID: 299
		[CompilerGenerated]
		private readonly int <EliteUncommonWeight>k__BackingField;

		// Token: 0x0400012C RID: 300
		[CompilerGenerated]
		private readonly int <EliteRareWeight>k__BackingField;

		// Token: 0x0400012D RID: 301
		[CompilerGenerated]
		private readonly int <EliteEpicWeight>k__BackingField;

		// Token: 0x0400012E RID: 302
		[CompilerGenerated]
		private readonly int <EliteLegendaryWeight>k__BackingField;

		// Token: 0x0400012F RID: 303
		[CompilerGenerated]
		private readonly int <BossCommonWeight>k__BackingField;

		// Token: 0x04000130 RID: 304
		[CompilerGenerated]
		private readonly int <BossUncommonWeight>k__BackingField;

		// Token: 0x04000131 RID: 305
		[CompilerGenerated]
		private readonly int <BossRareWeight>k__BackingField;

		// Token: 0x04000132 RID: 306
		[CompilerGenerated]
		private readonly int <BossEpicWeight>k__BackingField;

		// Token: 0x04000133 RID: 307
		[CompilerGenerated]
		private readonly int <BossLegendaryWeight>k__BackingField;

		// Token: 0x04000134 RID: 308
		[CompilerGenerated]
		private readonly int <RandomItemShopPrice>k__BackingField;

		// Token: 0x04000135 RID: 309
		[CompilerGenerated]
		private readonly int <MaxRareEquipped>k__BackingField;

		// Token: 0x04000136 RID: 310
		[CompilerGenerated]
		private readonly int <EarlyCombatBonusTurns>k__BackingField;

		// Token: 0x04000137 RID: 311
		[CompilerGenerated]
		private readonly int <IntensifyHpCost>k__BackingField;

		// Token: 0x04000138 RID: 312
		[CompilerGenerated]
		private readonly bool <EnableCurses>k__BackingField;

		// Token: 0x04000139 RID: 313
		[CompilerGenerated]
		private readonly double <CurseChanceRare>k__BackingField;

		// Token: 0x0400013A RID: 314
		[CompilerGenerated]
		private readonly double <CurseChanceEpic>k__BackingField;

		// Token: 0x0400013B RID: 315
		[CompilerGenerated]
		private readonly double <CurseChanceLegendary>k__BackingField;

		// Token: 0x0400013C RID: 316
		[CompilerGenerated]
		private readonly double <UnidentifiedChanceRarePlus>k__BackingField;

		// Token: 0x0400013D RID: 317
		[CompilerGenerated]
		private readonly int <CleanseHpCost>k__BackingField;

		// Token: 0x0400013E RID: 318
		[CompilerGenerated]
		private readonly bool <EnableScrolls>k__BackingField;

		// Token: 0x0400013F RID: 319
		[CompilerGenerated]
		private readonly double <ScrollDropChance>k__BackingField;

		// Token: 0x04000140 RID: 320
		[CompilerGenerated]
		private readonly int <ScrollGoldAmount>k__BackingField;

		// Token: 0x04000141 RID: 321
		[CompilerGenerated]
		private readonly bool <EnableTrade>k__BackingField;
	}
}
