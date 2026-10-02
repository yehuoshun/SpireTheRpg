using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using MegaCrit.Sts2.Core.Logging;

namespace SpireTheRpg.Core
{
	// Token: 0x02000044 RID: 68
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgItemCatalog
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000126FB File Offset: 0x000108FB
		public static IReadOnlyList<RpgItemTemplate> All
		{
			get
			{
				RpgItemCatalog.EnsureLoaded();
				return RpgItemCatalog._all;
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00012707 File Offset: 0x00010907
		[return: Nullable(2)]
		public static RpgItemTemplate Get(string id)
		{
			RpgItemCatalog.EnsureLoaded();
			return RpgItemCatalog._byId.GetValueOrDefault(id);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0001271C File Offset: 0x0001091C
		private static void EnsureLoaded()
		{
			if (RpgItemCatalog._all != null)
			{
				return;
			}
			List<RpgItemTemplate> list = new List<RpgItemTemplate>();
			List<string> list2 = SpireAssets.ListSubdirs("assets/item").ToList<string>();
			if (list2.Count == 0)
			{
				SpireTheRpgMainFile.Logger.Info("Item catalog: no category folders found", 1);
			}
			foreach (string str in list2)
			{
				string text = "assets/item/" + str + "/items.json";
				string text2 = SpireAssets.ReadText(text);
				if (!string.IsNullOrWhiteSpace(text2))
				{
					try
					{
						List<RpgItemTemplate> list3 = JsonSerializer.Deserialize<List<RpgItemTemplate>>(text2, RpgItemCatalog.JsonOptions);
						if (list3 != null && list3.Count > 0)
						{
							list.AddRange(from x in list3
							where !string.IsNullOrWhiteSpace(x.Id)
							select x);
						}
					}
					catch (Exception ex)
					{
						SpireTheRpgMainFile.Logger.Info("Item catalog load failed (" + text + "): " + ex.Message, 1);
					}
				}
			}
			RpgItemCatalog._all = list;
			RpgItemCatalog._byId = list.GroupBy((RpgItemTemplate x) => x.Id, StringComparer.OrdinalIgnoreCase).ToDictionary((IGrouping<string, RpgItemTemplate> g) => g.Key, (IGrouping<string, RpgItemTemplate> g) => g.First<RpgItemTemplate>(), StringComparer.OrdinalIgnoreCase);
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(31, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Item catalog loaded: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(RpgItemCatalog._all.Count);
			defaultInterpolatedStringHandler.AppendLiteral(" templates");
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00012900 File Offset: 0x00010B00
		// Note: this type is marked as 'beforefieldinit'.
		static RpgItemCatalog()
		{
		}

		// Token: 0x040000BF RID: 191
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

		// Token: 0x040000C0 RID: 192
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private static IReadOnlyList<RpgItemTemplate> _all;

		// Token: 0x040000C1 RID: 193
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		private static Dictionary<string, RpgItemTemplate> _byId;

		// Token: 0x020000F2 RID: 242
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000701 RID: 1793 RVA: 0x00025B22 File Offset: 0x00023D22
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000702 RID: 1794 RVA: 0x00025B2E File Offset: 0x00023D2E
			public <>c()
			{
			}

			// Token: 0x06000703 RID: 1795 RVA: 0x00025B36 File Offset: 0x00023D36
			[NullableContext(0)]
			internal bool <EnsureLoaded>b__6_3(RpgItemTemplate x)
			{
				return !string.IsNullOrWhiteSpace(x.Id);
			}

			// Token: 0x06000704 RID: 1796 RVA: 0x00025B46 File Offset: 0x00023D46
			[NullableContext(0)]
			internal string <EnsureLoaded>b__6_0(RpgItemTemplate x)
			{
				return x.Id;
			}

			// Token: 0x06000705 RID: 1797 RVA: 0x00025B4E File Offset: 0x00023D4E
			[NullableContext(0)]
			[return: Nullable(1)]
			internal string <EnsureLoaded>b__6_1(IGrouping<string, RpgItemTemplate> g)
			{
				return g.Key;
			}

			// Token: 0x06000706 RID: 1798 RVA: 0x00025B56 File Offset: 0x00023D56
			[NullableContext(0)]
			internal RpgItemTemplate <EnsureLoaded>b__6_2(IGrouping<string, RpgItemTemplate> g)
			{
				return g.First<RpgItemTemplate>();
			}

			// Token: 0x04000376 RID: 886
			[Nullable(0)]
			public static readonly RpgItemCatalog.<>c <>9 = new RpgItemCatalog.<>c();

			// Token: 0x04000377 RID: 887
			[Nullable(0)]
			public static Func<RpgItemTemplate, bool> <>9__6_3;

			// Token: 0x04000378 RID: 888
			[Nullable(0)]
			public static Func<RpgItemTemplate, string> <>9__6_0;

			// Token: 0x04000379 RID: 889
			[Nullable(new byte[]
			{
				0,
				0,
				0,
				0,
				1
			})]
			public static Func<IGrouping<string, RpgItemTemplate>, string> <>9__6_1;

			// Token: 0x0400037A RID: 890
			[Nullable(0)]
			public static Func<IGrouping<string, RpgItemTemplate>, RpgItemTemplate> <>9__6_2;
		}
	}
}
