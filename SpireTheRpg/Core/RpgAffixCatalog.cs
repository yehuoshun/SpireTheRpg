using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using MegaCrit.Sts2.Core.Logging;

namespace SpireTheRpg.Core
{
	// Token: 0x02000039 RID: 57
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgAffixCatalog
	{
		// Token: 0x06000209 RID: 521 RVA: 0x00010E00 File Offset: 0x0000F000
		public static bool IsBannedId(string id)
		{
			return RpgAffixCatalog.BannedIds.Contains(id);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00010E0D File Offset: 0x0000F00D
		public static IReadOnlyList<RpgAffixDef> All
		{
			get
			{
				RpgAffixCatalog.EnsureLoaded();
				return RpgAffixCatalog._all;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00010E19 File Offset: 0x0000F019
		public static IReadOnlyList<RpgAffixDef> Buffs
		{
			get
			{
				RpgAffixCatalog.EnsureLoaded();
				return (from x in RpgAffixCatalog._all
				where !x.IsDebuff
				select x).ToList<RpgAffixDef>();
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00010E4E File Offset: 0x0000F04E
		public static IReadOnlyList<RpgAffixDef> Debuffs
		{
			get
			{
				RpgAffixCatalog.EnsureLoaded();
				return (from x in RpgAffixCatalog._all
				where x.IsDebuff
				select x).ToList<RpgAffixDef>();
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00010E83 File Offset: 0x0000F083
		public static IReadOnlyList<RpgAffixDef> Rollable
		{
			get
			{
				RpgAffixCatalog.EnsureLoaded();
				return (from x in RpgAffixCatalog._all
				where x.Rollable || string.Equals(x.Apply, "combatStart", StringComparison.OrdinalIgnoreCase)
				select x).ToList<RpgAffixDef>();
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00010EB8 File Offset: 0x0000F0B8
		[return: Nullable(2)]
		public static RpgAffixDef Get(string id)
		{
			RpgAffixCatalog.EnsureLoaded();
			return RpgAffixCatalog._byId.GetValueOrDefault(id);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00010ECA File Offset: 0x0000F0CA
		public static bool IsDebuffId(string id)
		{
			RpgAffixDef rpgAffixDef = RpgAffixCatalog.Get(id);
			return rpgAffixDef != null && rpgAffixDef.IsDebuff;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00010EE0 File Offset: 0x0000F0E0
		private static void EnsureLoaded()
		{
			if (RpgAffixCatalog._all != null)
			{
				return;
			}
			List<RpgAffixDef> list = new List<RpgAffixDef>();
			RpgAffixCatalog.LoadFile(list, "assets/affixes/player_buffs.json", false);
			RpgAffixCatalog.LoadFile(list, "assets/affixes/player_debuffs.json", true);
			foreach (RpgAffixDef rpgAffixDef in list)
			{
				if (rpgAffixDef.Max < rpgAffixDef.Min)
				{
					rpgAffixDef.Max = rpgAffixDef.Min;
				}
				if (rpgAffixDef.Min < 1)
				{
					rpgAffixDef.Min = 1;
				}
				rpgAffixDef.Rollable = true;
			}
			RpgAffixCatalog._all = list;
			RpgAffixCatalog._byId = list.GroupBy((RpgAffixDef x) => x.Id, StringComparer.OrdinalIgnoreCase).ToDictionary((IGrouping<string, RpgAffixDef> g) => g.Key, (IGrouping<string, RpgAffixDef> g) => g.First<RpgAffixDef>(), StringComparer.OrdinalIgnoreCase);
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 3);
			defaultInterpolatedStringHandler.AppendLiteral("Affix catalog loaded: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(RpgAffixCatalog._all.Count);
			defaultInterpolatedStringHandler.AppendLiteral(" (buffs=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(RpgAffixCatalog._all.Count((RpgAffixDef x) => !x.IsDebuff));
			defaultInterpolatedStringHandler.AppendLiteral(" debuffs=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(RpgAffixCatalog._all.Count((RpgAffixDef x) => x.IsDebuff));
			defaultInterpolatedStringHandler.AppendLiteral(")");
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000110B8 File Offset: 0x0000F2B8
		private static void LoadFile(List<RpgAffixDef> list, string relative, bool forceDebuff)
		{
			try
			{
				string text = SpireAssets.ReadText(relative);
				if (!string.IsNullOrWhiteSpace(text))
				{
					List<RpgAffixDef> list2 = JsonSerializer.Deserialize<List<RpgAffixDef>>(text, RpgAffixCatalog.JsonOptions);
					if (list2 != null)
					{
						foreach (RpgAffixDef rpgAffixDef in list2)
						{
							if (!string.IsNullOrWhiteSpace(rpgAffixDef.Id) && !string.IsNullOrWhiteSpace(rpgAffixDef.Power) && !RpgAffixCatalog.BannedIds.Contains(rpgAffixDef.Id))
							{
								if (forceDebuff)
								{
									rpgAffixDef.IsDebuff = true;
								}
								list.Add(rpgAffixDef);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("Affix catalog load failed (" + relative + "): " + ex.Message, 1);
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00011198 File Offset: 0x0000F398
		// Note: this type is marked as 'beforefieldinit'.
		static RpgAffixCatalog()
		{
		}

		// Token: 0x040000A5 RID: 165
		private static readonly HashSet<string> BannedIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"nightmare",
			"intercept",
			"toric_toughness"
		};

		// Token: 0x040000A6 RID: 166
		private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			ReadCommentHandling = JsonCommentHandling.Skip,
			AllowTrailingCommas = true,
			Converters = 
			{
				new JsonStringEnumConverter()
			}
		};

		// Token: 0x040000A7 RID: 167
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private static IReadOnlyList<RpgAffixDef> _all;

		// Token: 0x040000A8 RID: 168
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		private static Dictionary<string, RpgAffixDef> _byId;

		// Token: 0x020000DC RID: 220
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006C6 RID: 1734 RVA: 0x000234E8 File Offset: 0x000216E8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006C7 RID: 1735 RVA: 0x000234F4 File Offset: 0x000216F4
			public <>c()
			{
			}

			// Token: 0x060006C8 RID: 1736 RVA: 0x000234FC File Offset: 0x000216FC
			[NullableContext(0)]
			internal bool <get_Buffs>b__8_0(RpgAffixDef x)
			{
				return !x.IsDebuff;
			}

			// Token: 0x060006C9 RID: 1737 RVA: 0x00023507 File Offset: 0x00021707
			[NullableContext(0)]
			internal bool <get_Debuffs>b__10_0(RpgAffixDef x)
			{
				return x.IsDebuff;
			}

			// Token: 0x060006CA RID: 1738 RVA: 0x0002350F File Offset: 0x0002170F
			[NullableContext(0)]
			internal bool <get_Rollable>b__12_0(RpgAffixDef x)
			{
				return x.Rollable || string.Equals(x.Apply, "combatStart", StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x060006CB RID: 1739 RVA: 0x0002352C File Offset: 0x0002172C
			[NullableContext(0)]
			internal string <EnsureLoaded>b__15_0(RpgAffixDef x)
			{
				return x.Id;
			}

			// Token: 0x060006CC RID: 1740 RVA: 0x00023534 File Offset: 0x00021734
			[NullableContext(0)]
			[return: Nullable(1)]
			internal string <EnsureLoaded>b__15_1(IGrouping<string, RpgAffixDef> g)
			{
				return g.Key;
			}

			// Token: 0x060006CD RID: 1741 RVA: 0x0002353C File Offset: 0x0002173C
			[NullableContext(0)]
			internal RpgAffixDef <EnsureLoaded>b__15_2(IGrouping<string, RpgAffixDef> g)
			{
				return g.First<RpgAffixDef>();
			}

			// Token: 0x060006CE RID: 1742 RVA: 0x00023544 File Offset: 0x00021744
			[NullableContext(0)]
			internal bool <EnsureLoaded>b__15_3(RpgAffixDef x)
			{
				return !x.IsDebuff;
			}

			// Token: 0x060006CF RID: 1743 RVA: 0x0002354F File Offset: 0x0002174F
			[NullableContext(0)]
			internal bool <EnsureLoaded>b__15_4(RpgAffixDef x)
			{
				return x.IsDebuff;
			}

			// Token: 0x04000301 RID: 769
			[Nullable(0)]
			public static readonly RpgAffixCatalog.<>c <>9 = new RpgAffixCatalog.<>c();

			// Token: 0x04000302 RID: 770
			[Nullable(0)]
			public static Func<RpgAffixDef, bool> <>9__8_0;

			// Token: 0x04000303 RID: 771
			[Nullable(0)]
			public static Func<RpgAffixDef, bool> <>9__10_0;

			// Token: 0x04000304 RID: 772
			[Nullable(0)]
			public static Func<RpgAffixDef, bool> <>9__12_0;

			// Token: 0x04000305 RID: 773
			[Nullable(0)]
			public static Func<RpgAffixDef, string> <>9__15_0;

			// Token: 0x04000306 RID: 774
			[Nullable(new byte[]
			{
				0,
				0,
				0,
				0,
				1
			})]
			public static Func<IGrouping<string, RpgAffixDef>, string> <>9__15_1;

			// Token: 0x04000307 RID: 775
			[Nullable(0)]
			public static Func<IGrouping<string, RpgAffixDef>, RpgAffixDef> <>9__15_2;

			// Token: 0x04000308 RID: 776
			[Nullable(0)]
			public static Func<RpgAffixDef, bool> <>9__15_3;

			// Token: 0x04000309 RID: 777
			[Nullable(0)]
			public static Func<RpgAffixDef, bool> <>9__15_4;
		}
	}
}
