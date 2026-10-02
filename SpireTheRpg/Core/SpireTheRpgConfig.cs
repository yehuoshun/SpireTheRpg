using System;
using System.Runtime.CompilerServices;
using BaseLib.Config;

namespace SpireTheRpg.Core
{
	// Token: 0x02000068 RID: 104
	public sealed class SpireTheRpgConfig : SimpleModConfig
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0001AC5D File Offset: 0x00018E5D
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x0001AC64 File Offset: 0x00018E64
		[ConfigSection("Mode")]
		public static bool EnableMetaEquipment
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<EnableMetaEquipment>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<EnableMetaEquipment>k__BackingField = value;
			}
		} = false;

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0001AC6C File Offset: 0x00018E6C
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x0001AC73 File Offset: 0x00018E73
		[ConfigSection("Drops")]
		[ConfigSlider(0.0, 1.0, 0.05, Format = "{0:0%}")]
		public static double MonsterDropChance
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<MonsterDropChance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<MonsterDropChance>k__BackingField = value;
			}
		} = 0.4;

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0001AC7B File Offset: 0x00018E7B
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x0001AC82 File Offset: 0x00018E82
		[ConfigSlider(0.0, 1.0, 0.05, Format = "{0:0%}")]
		public static double EliteDropChance
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<EliteDropChance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<EliteDropChance>k__BackingField = value;
			}
		} = 0.8;

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0001AC8A File Offset: 0x00018E8A
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0001AC91 File Offset: 0x00018E91
		[ConfigSlider(0.0, 1.0, 0.05, Format = "{0:0%}")]
		public static double BossDropChance
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<BossDropChance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<BossDropChance>k__BackingField = value;
			}
		} = 1.0;

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0001AC99 File Offset: 0x00018E99
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x0001ACA0 File Offset: 0x00018EA0
		[ConfigSection("RunLoot")]
		public static bool KeepLootOnAbandon
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<KeepLootOnAbandon>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<KeepLootOnAbandon>k__BackingField = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0001ACA8 File Offset: 0x00018EA8
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x0001ACAF File Offset: 0x00018EAF
		[ConfigSection("Xp")]
		[ConfigSlider(0.0, 200.0, 1.0)]
		public static int MonsterXpMin
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<MonsterXpMin>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<MonsterXpMin>k__BackingField = value;
			}
		} = 8;

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0001ACB7 File Offset: 0x00018EB7
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x0001ACBE File Offset: 0x00018EBE
		[ConfigSlider(0.0, 200.0, 1.0)]
		public static int MonsterXpMax
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<MonsterXpMax>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<MonsterXpMax>k__BackingField = value;
			}
		} = 18;

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0001ACC6 File Offset: 0x00018EC6
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x0001ACCD File Offset: 0x00018ECD
		[ConfigSlider(0.0, 300.0, 1.0)]
		public static int EliteXpMin
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<EliteXpMin>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<EliteXpMin>k__BackingField = value;
			}
		} = 22;

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0001ACD5 File Offset: 0x00018ED5
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x0001ACDC File Offset: 0x00018EDC
		[ConfigSlider(0.0, 300.0, 1.0)]
		public static int EliteXpMax
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<EliteXpMax>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<EliteXpMax>k__BackingField = value;
			}
		} = 40;

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0001ACE4 File Offset: 0x00018EE4
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0001ACEB File Offset: 0x00018EEB
		[ConfigSlider(0.0, 500.0, 1.0)]
		public static int BossXpMin
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<BossXpMin>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<BossXpMin>k__BackingField = value;
			}
		} = 45;

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001ACF3 File Offset: 0x00018EF3
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x0001ACFA File Offset: 0x00018EFA
		[ConfigSlider(0.0, 500.0, 1.0)]
		public static int BossXpMax
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<BossXpMax>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<BossXpMax>k__BackingField = value;
			}
		} = 80;

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0001AD02 File Offset: 0x00018F02
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x0001AD09 File Offset: 0x00018F09
		[ConfigSection("MonsterRarity")]
		[ConfigSlider(0.0, 100.0, 1.0)]
		public static int MonsterCommonWeight
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<MonsterCommonWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<MonsterCommonWeight>k__BackingField = value;
			}
		} = 90;

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0001AD11 File Offset: 0x00018F11
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x0001AD18 File Offset: 0x00018F18
		[ConfigSlider(0.0, 100.0, 1.0)]
		public static int MonsterUncommonWeight
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<MonsterUncommonWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<MonsterUncommonWeight>k__BackingField = value;
			}
		} = 10;

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0001AD20 File Offset: 0x00018F20
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x0001AD27 File Offset: 0x00018F27
		[ConfigSlider(0.0, 100.0, 1.0)]
		public static int MonsterRareWeight
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<MonsterRareWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<MonsterRareWeight>k__BackingField = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0001AD2F File Offset: 0x00018F2F
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x0001AD36 File Offset: 0x00018F36
		[ConfigSlider(0.0, 100.0, 1.0)]
		public static int MonsterEpicWeight
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<MonsterEpicWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<MonsterEpicWeight>k__BackingField = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0001AD3E File Offset: 0x00018F3E
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x0001AD45 File Offset: 0x00018F45
		[ConfigSlider(0.0, 100.0, 1.0)]
		public static int MonsterLegendaryWeight
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<MonsterLegendaryWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<MonsterLegendaryWeight>k__BackingField = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0001AD4D File Offset: 0x00018F4D
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x0001AD54 File Offset: 0x00018F54
		[ConfigSection("EliteRarity")]
		[ConfigSlider(0.0, 100.0, 1.0)]
		public static int EliteCommonWeight
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<EliteCommonWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<EliteCommonWeight>k__BackingField = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0001AD5C File Offset: 0x00018F5C
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x0001AD63 File Offset: 0x00018F63
		[ConfigSlider(0.0, 100.0, 1.0)]
		public static int EliteUncommonWeight
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<EliteUncommonWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<EliteUncommonWeight>k__BackingField = value;
			}
		} = 55;

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0001AD6B File Offset: 0x00018F6B
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x0001AD72 File Offset: 0x00018F72
		[ConfigSlider(0.0, 100.0, 1.0)]
		public static int EliteRareWeight
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<EliteRareWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<EliteRareWeight>k__BackingField = value;
			}
		} = 40;

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0001AD7A File Offset: 0x00018F7A
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x0001AD81 File Offset: 0x00018F81
		[ConfigSlider(0.0, 100.0, 1.0)]
		public static int EliteEpicWeight
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<EliteEpicWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<EliteEpicWeight>k__BackingField = value;
			}
		} = 5;

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0001AD89 File Offset: 0x00018F89
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x0001AD90 File Offset: 0x00018F90
		[ConfigSlider(0.0, 100.0, 1.0)]
		public static int EliteLegendaryWeight
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<EliteLegendaryWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<EliteLegendaryWeight>k__BackingField = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0001AD98 File Offset: 0x00018F98
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x0001AD9F File Offset: 0x00018F9F
		[ConfigSection("BossRarity")]
		[ConfigSlider(0.0, 100.0, 1.0)]
		public static int BossCommonWeight
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<BossCommonWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<BossCommonWeight>k__BackingField = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0001ADA7 File Offset: 0x00018FA7
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x0001ADAE File Offset: 0x00018FAE
		[ConfigSlider(0.0, 100.0, 1.0)]
		public static int BossUncommonWeight
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<BossUncommonWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<BossUncommonWeight>k__BackingField = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0001ADB6 File Offset: 0x00018FB6
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x0001ADBD File Offset: 0x00018FBD
		[ConfigSlider(0.0, 100.0, 1.0)]
		public static int BossRareWeight
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<BossRareWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<BossRareWeight>k__BackingField = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0001ADC5 File Offset: 0x00018FC5
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x0001ADCC File Offset: 0x00018FCC
		[ConfigSlider(0.0, 100.0, 1.0)]
		public static int BossEpicWeight
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<BossEpicWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<BossEpicWeight>k__BackingField = value;
			}
		} = 55;

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0001ADD4 File Offset: 0x00018FD4
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x0001ADDB File Offset: 0x00018FDB
		[ConfigSlider(0.0, 100.0, 1.0)]
		public static int BossLegendaryWeight
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<BossLegendaryWeight>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<BossLegendaryWeight>k__BackingField = value;
			}
		} = 45;

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0001ADE3 File Offset: 0x00018FE3
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x0001ADEA File Offset: 0x00018FEA
		[ConfigSection("Shop")]
		[ConfigSlider(0.0, 9999.0, 1.0)]
		public static int RandomItemShopPrice
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<RandomItemShopPrice>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<RandomItemShopPrice>k__BackingField = value;
			}
		} = 100;

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0001ADF2 File Offset: 0x00018FF2
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x0001ADF9 File Offset: 0x00018FF9
		[ConfigSection("Balance")]
		[ConfigSlider(0.0, 6.0, 1.0)]
		public static int MaxRareEquipped
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<MaxRareEquipped>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<MaxRareEquipped>k__BackingField = value;
			}
		} = 2;

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0001AE01 File Offset: 0x00019001
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x0001AE08 File Offset: 0x00019008
		[ConfigSlider(0.0, 5.0, 1.0)]
		public static int EarlyCombatBonusTurns
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<EarlyCombatBonusTurns>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<EarlyCombatBonusTurns>k__BackingField = value;
			}
		} = 1;

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0001AE10 File Offset: 0x00019010
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x0001AE17 File Offset: 0x00019017
		[ConfigSlider(0.0, 50.0, 1.0)]
		public static int IntensifyHpCost
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<IntensifyHpCost>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<IntensifyHpCost>k__BackingField = value;
			}
		} = 8;

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0001AE1F File Offset: 0x0001901F
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x0001AE26 File Offset: 0x00019026
		[ConfigSection("Curses")]
		public static bool EnableCurses
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<EnableCurses>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<EnableCurses>k__BackingField = value;
			}
		} = true;

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0001AE2E File Offset: 0x0001902E
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0001AE35 File Offset: 0x00019035
		[ConfigSlider(0.0, 1.0, 0.05, Format = "{0:0%}")]
		public static double CurseChanceRare
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<CurseChanceRare>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<CurseChanceRare>k__BackingField = value;
			}
		} = 0.25;

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0001AE3D File Offset: 0x0001903D
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x0001AE44 File Offset: 0x00019044
		[ConfigSlider(0.0, 1.0, 0.05, Format = "{0:0%}")]
		public static double CurseChanceEpic
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<CurseChanceEpic>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<CurseChanceEpic>k__BackingField = value;
			}
		} = 0.45;

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0001AE4C File Offset: 0x0001904C
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x0001AE53 File Offset: 0x00019053
		[ConfigSlider(0.0, 1.0, 0.05, Format = "{0:0%}")]
		public static double CurseChanceLegendary
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<CurseChanceLegendary>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<CurseChanceLegendary>k__BackingField = value;
			}
		} = 0.65;

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0001AE5B File Offset: 0x0001905B
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x0001AE62 File Offset: 0x00019062
		[ConfigSlider(0.0, 1.0, 0.05, Format = "{0:0%}")]
		public static double UnidentifiedChanceRarePlus
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<UnidentifiedChanceRarePlus>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<UnidentifiedChanceRarePlus>k__BackingField = value;
			}
		} = 0.7;

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0001AE6A File Offset: 0x0001906A
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x0001AE71 File Offset: 0x00019071
		[ConfigSlider(0.0, 50.0, 1.0)]
		public static int CleanseHpCost
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<CleanseHpCost>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<CleanseHpCost>k__BackingField = value;
			}
		} = 12;

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0001AE79 File Offset: 0x00019079
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x0001AE80 File Offset: 0x00019080
		[ConfigSection("Scrolls")]
		public static bool EnableScrolls
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<EnableScrolls>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<EnableScrolls>k__BackingField = value;
			}
		} = true;

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0001AE88 File Offset: 0x00019088
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0001AE8F File Offset: 0x0001908F
		[ConfigSlider(0.0, 1.0, 0.05, Format = "{0:0%}")]
		public static double ScrollDropChance
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<ScrollDropChance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<ScrollDropChance>k__BackingField = value;
			}
		} = 0.12;

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0001AE97 File Offset: 0x00019097
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x0001AE9E File Offset: 0x0001909E
		[ConfigSlider(0.0, 999.0, 1.0)]
		public static int ScrollGoldAmount
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<ScrollGoldAmount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<ScrollGoldAmount>k__BackingField = value;
			}
		} = 50;

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0001AEA6 File Offset: 0x000190A6
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x0001AEAD File Offset: 0x000190AD
		[ConfigSection("Trade")]
		public static bool EnableTrade
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<EnableTrade>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<EnableTrade>k__BackingField = value;
			}
		} = true;

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0001AEB5 File Offset: 0x000190B5
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x0001AEBC File Offset: 0x000190BC
		[ConfigSection("UI")]
		public static bool ShowUpdateNotice
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<ShowUpdateNotice>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<ShowUpdateNotice>k__BackingField = value;
			}
		} = true;

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0001AEC4 File Offset: 0x000190C4
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x0001AECB File Offset: 0x000190CB
		[ConfigSection("Dev")]
		public static bool EnableDevCheats
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<EnableDevCheats>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<EnableDevCheats>k__BackingField = value;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0001AED3 File Offset: 0x000190D3
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x0001AEDA File Offset: 0x000190DA
		[ConfigVisibleIf("EnableDevCheats", new object[]
		{

		})]
		public static bool IgnoreLevelRequirement
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<IgnoreLevelRequirement>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<IgnoreLevelRequirement>k__BackingField = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0001AEE2 File Offset: 0x000190E2
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x0001AEE9 File Offset: 0x000190E9
		[ConfigVisibleIf("EnableDevCheats", new object[]
		{

		})]
		public static bool MonsterDropsAllRarities
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgConfig.<MonsterDropsAllRarities>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				SpireTheRpgConfig.<MonsterDropsAllRarities>k__BackingField = value;
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001AEF1 File Offset: 0x000190F1
		public SpireTheRpgConfig()
		{
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001AEFC File Offset: 0x000190FC
		// Note: this type is marked as 'beforefieldinit'.
		static SpireTheRpgConfig()
		{
		}

		// Token: 0x0400016E RID: 366
		[CompilerGenerated]
		private static bool <EnableMetaEquipment>k__BackingField;

		// Token: 0x0400016F RID: 367
		[CompilerGenerated]
		private static double <MonsterDropChance>k__BackingField;

		// Token: 0x04000170 RID: 368
		[CompilerGenerated]
		private static double <EliteDropChance>k__BackingField;

		// Token: 0x04000171 RID: 369
		[CompilerGenerated]
		private static double <BossDropChance>k__BackingField;

		// Token: 0x04000172 RID: 370
		[CompilerGenerated]
		private static bool <KeepLootOnAbandon>k__BackingField;

		// Token: 0x04000173 RID: 371
		[CompilerGenerated]
		private static int <MonsterXpMin>k__BackingField;

		// Token: 0x04000174 RID: 372
		[CompilerGenerated]
		private static int <MonsterXpMax>k__BackingField;

		// Token: 0x04000175 RID: 373
		[CompilerGenerated]
		private static int <EliteXpMin>k__BackingField;

		// Token: 0x04000176 RID: 374
		[CompilerGenerated]
		private static int <EliteXpMax>k__BackingField;

		// Token: 0x04000177 RID: 375
		[CompilerGenerated]
		private static int <BossXpMin>k__BackingField;

		// Token: 0x04000178 RID: 376
		[CompilerGenerated]
		private static int <BossXpMax>k__BackingField;

		// Token: 0x04000179 RID: 377
		[CompilerGenerated]
		private static int <MonsterCommonWeight>k__BackingField;

		// Token: 0x0400017A RID: 378
		[CompilerGenerated]
		private static int <MonsterUncommonWeight>k__BackingField;

		// Token: 0x0400017B RID: 379
		[CompilerGenerated]
		private static int <MonsterRareWeight>k__BackingField;

		// Token: 0x0400017C RID: 380
		[CompilerGenerated]
		private static int <MonsterEpicWeight>k__BackingField;

		// Token: 0x0400017D RID: 381
		[CompilerGenerated]
		private static int <MonsterLegendaryWeight>k__BackingField;

		// Token: 0x0400017E RID: 382
		[CompilerGenerated]
		private static int <EliteCommonWeight>k__BackingField;

		// Token: 0x0400017F RID: 383
		[CompilerGenerated]
		private static int <EliteUncommonWeight>k__BackingField;

		// Token: 0x04000180 RID: 384
		[CompilerGenerated]
		private static int <EliteRareWeight>k__BackingField;

		// Token: 0x04000181 RID: 385
		[CompilerGenerated]
		private static int <EliteEpicWeight>k__BackingField;

		// Token: 0x04000182 RID: 386
		[CompilerGenerated]
		private static int <EliteLegendaryWeight>k__BackingField;

		// Token: 0x04000183 RID: 387
		[CompilerGenerated]
		private static int <BossCommonWeight>k__BackingField;

		// Token: 0x04000184 RID: 388
		[CompilerGenerated]
		private static int <BossUncommonWeight>k__BackingField;

		// Token: 0x04000185 RID: 389
		[CompilerGenerated]
		private static int <BossRareWeight>k__BackingField;

		// Token: 0x04000186 RID: 390
		[CompilerGenerated]
		private static int <BossEpicWeight>k__BackingField;

		// Token: 0x04000187 RID: 391
		[CompilerGenerated]
		private static int <BossLegendaryWeight>k__BackingField;

		// Token: 0x04000188 RID: 392
		[CompilerGenerated]
		private static int <RandomItemShopPrice>k__BackingField;

		// Token: 0x04000189 RID: 393
		[CompilerGenerated]
		private static int <MaxRareEquipped>k__BackingField;

		// Token: 0x0400018A RID: 394
		[CompilerGenerated]
		private static int <EarlyCombatBonusTurns>k__BackingField;

		// Token: 0x0400018B RID: 395
		[CompilerGenerated]
		private static int <IntensifyHpCost>k__BackingField;

		// Token: 0x0400018C RID: 396
		[CompilerGenerated]
		private static bool <EnableCurses>k__BackingField;

		// Token: 0x0400018D RID: 397
		[CompilerGenerated]
		private static double <CurseChanceRare>k__BackingField;

		// Token: 0x0400018E RID: 398
		[CompilerGenerated]
		private static double <CurseChanceEpic>k__BackingField;

		// Token: 0x0400018F RID: 399
		[CompilerGenerated]
		private static double <CurseChanceLegendary>k__BackingField;

		// Token: 0x04000190 RID: 400
		[CompilerGenerated]
		private static double <UnidentifiedChanceRarePlus>k__BackingField;

		// Token: 0x04000191 RID: 401
		[CompilerGenerated]
		private static int <CleanseHpCost>k__BackingField;

		// Token: 0x04000192 RID: 402
		[CompilerGenerated]
		private static bool <EnableScrolls>k__BackingField;

		// Token: 0x04000193 RID: 403
		[CompilerGenerated]
		private static double <ScrollDropChance>k__BackingField;

		// Token: 0x04000194 RID: 404
		[CompilerGenerated]
		private static int <ScrollGoldAmount>k__BackingField;

		// Token: 0x04000195 RID: 405
		[CompilerGenerated]
		private static bool <EnableTrade>k__BackingField;

		// Token: 0x04000196 RID: 406
		[CompilerGenerated]
		private static bool <ShowUpdateNotice>k__BackingField;

		// Token: 0x04000197 RID: 407
		[CompilerGenerated]
		private static bool <EnableDevCheats>k__BackingField;

		// Token: 0x04000198 RID: 408
		[CompilerGenerated]
		private static bool <IgnoreLevelRequirement>k__BackingField;

		// Token: 0x04000199 RID: 409
		[CompilerGenerated]
		private static bool <MonsterDropsAllRarities>k__BackingField;
	}
}
