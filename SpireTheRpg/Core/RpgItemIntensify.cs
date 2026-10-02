using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SpireTheRpg.Core
{
	// Token: 0x02000048 RID: 72
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgItemIntensify
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x00013740 File Offset: 0x00011940
		public static RpgItemData Preview(RpgItemData source)
		{
			RpgItemData rpgItemData = source.Clone();
			RpgItemIntensify.ApplyInPlace(rpgItemData);
			return rpgItemData;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0001374E File Offset: 0x0001194E
		public static RpgItemData Apply(RpgItemData source)
		{
			RpgItemData rpgItemData = source.Clone();
			RpgItemIntensify.ApplyInPlace(rpgItemData);
			return rpgItemData;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001375C File Offset: 0x0001195C
		public unsafe static void SelfCheck()
		{
			try
			{
				RpgItemData rpgItemData = new RpgItemData();
				rpgItemData.Id = "intensify_check";
				rpgItemData.Primary = "strength:3";
				int num = 3;
				List<string> list = new List<string>(num);
				CollectionsMarshal.SetCount<string>(list, num);
				Span<string> span = CollectionsMarshal.AsSpan<string>(list);
				int num2 = 0;
				*span[num2] = "thorns:1";
				num2++;
				*span[num2] = "shadow_damage:17%";
				num2++;
				*span[num2] = "weak:3";
				rpgItemData.Ability = list;
				rpgItemData.ItemPower = 100;
				RpgItemData rpgItemData2 = rpgItemData;
				RpgItemIntensify.ApplyInPlace(rpgItemData2);
				if (rpgItemData2.Primary != "strength:4")
				{
					throw new Exception("primary=" + rpgItemData2.Primary);
				}
				if (rpgItemData2.Ability[0] != "thorns:2")
				{
					throw new Exception("flat=" + rpgItemData2.Ability[0]);
				}
				if (rpgItemData2.Ability[1] != "shadow_damage:20%")
				{
					throw new Exception("pct=" + rpgItemData2.Ability[1]);
				}
				if (rpgItemData2.Ability[2] != "weak:2")
				{
					throw new Exception("debuff=" + rpgItemData2.Ability[2]);
				}
				if (rpgItemData2.ItemPower != 105)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
					defaultInterpolatedStringHandler.AppendLiteral("power=");
					defaultInterpolatedStringHandler.AppendFormatted<int>(rpgItemData2.ItemPower);
					throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				if (rpgItemData2.IntensifyLevel != 1)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
					defaultInterpolatedStringHandler.AppendLiteral("level=");
					defaultInterpolatedStringHandler.AppendFormatted<int>(rpgItemData2.IntensifyLevel);
					throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				if (rpgItemData2.DisplayName != rpgItemData2.LocalizedName + " +1")
				{
					throw new Exception("name=" + rpgItemData2.DisplayName);
				}
				SpireTheRpgMainFile.Logger.Info("RpgItemIntensify self-check ok", 1);
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RpgItemIntensify self-check FAIL: " + ex.Message, 1);
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0001399C File Offset: 0x00011B9C
		private static void ApplyInPlace(RpgItemData item)
		{
			item.Primary = RpgItemIntensify.BumpText(item.Primary);
			IEnumerable<string> ability = item.Ability;
			Func<string, string> selector;
			if ((selector = RpgItemIntensify.<>O.<0>__BumpText) == null)
			{
				selector = (RpgItemIntensify.<>O.<0>__BumpText = new Func<string, string>(RpgItemIntensify.BumpText));
			}
			item.Ability = (from x in ability.Select(selector)
			where x != null
			select x).Cast<string>().ToList<string>();
			IEnumerable<string> hiddenAbility = item.HiddenAbility;
			Func<string, string> selector2;
			if ((selector2 = RpgItemIntensify.<>O.<0>__BumpText) == null)
			{
				selector2 = (RpgItemIntensify.<>O.<0>__BumpText = new Func<string, string>(RpgItemIntensify.BumpText));
			}
			item.HiddenAbility = (from x in hiddenAbility.Select(selector2)
			where x != null
			select x).Cast<string>().ToList<string>();
			IEnumerable<string> details = item.Details;
			Func<string, string> selector3;
			if ((selector3 = RpgItemIntensify.<>O.<0>__BumpText) == null)
			{
				selector3 = (RpgItemIntensify.<>O.<0>__BumpText = new Func<string, string>(RpgItemIntensify.BumpText));
			}
			item.Details = (from x in details.Select(selector3)
			where x != null
			select x).Cast<string>().ToList<string>();
			item.ItemPower += 5;
			int intensifyLevel = item.IntensifyLevel;
			item.IntensifyLevel = intensifyLevel + 1;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00013AE8 File Offset: 0x00011CE8
		[NullableContext(2)]
		private static string BumpText(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				return text;
			}
			RpgStatMod rpgStatMod = RpgStatMod.Parse(text);
			if (rpgStatMod == null)
			{
				return text;
			}
			int num = rpgStatMod.Percent ? 3 : 1;
			double value = rpgStatMod.IsDebuff ? Math.Max(1.0, Math.Abs(rpgStatMod.Value) - (double)num) : (Math.Abs(rpgStatMod.Value) + (double)num);
			return new RpgStatMod
			{
				Id = rpgStatMod.Id,
				Percent = rpgStatMod.Percent,
				Value = value
			}.ToShorthand();
		}

		// Token: 0x040000E1 RID: 225
		public const int FlatBump = 1;

		// Token: 0x040000E2 RID: 226
		public const int PercentBump = 3;

		// Token: 0x040000E3 RID: 227
		public const int ItemPowerBump = 5;

		// Token: 0x020000FC RID: 252
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040003A5 RID: 933
			[Nullable(0)]
			public static Func<string, string> <0>__BumpText;
		}

		// Token: 0x020000FD RID: 253
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000741 RID: 1857 RVA: 0x000265D3 File Offset: 0x000247D3
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000742 RID: 1858 RVA: 0x000265DF File Offset: 0x000247DF
			public <>c()
			{
			}

			// Token: 0x06000743 RID: 1859 RVA: 0x000265E7 File Offset: 0x000247E7
			[NullableContext(0)]
			internal bool <ApplyInPlace>b__6_0(string x)
			{
				return x != null;
			}

			// Token: 0x06000744 RID: 1860 RVA: 0x000265ED File Offset: 0x000247ED
			[NullableContext(0)]
			internal bool <ApplyInPlace>b__6_1(string x)
			{
				return x != null;
			}

			// Token: 0x06000745 RID: 1861 RVA: 0x000265F3 File Offset: 0x000247F3
			[NullableContext(0)]
			internal bool <ApplyInPlace>b__6_2(string x)
			{
				return x != null;
			}

			// Token: 0x040003A6 RID: 934
			[Nullable(0)]
			public static readonly RpgItemIntensify.<>c <>9 = new RpgItemIntensify.<>c();

			// Token: 0x040003A7 RID: 935
			[Nullable(0)]
			public static Func<string, bool> <>9__6_0;

			// Token: 0x040003A8 RID: 936
			[Nullable(0)]
			public static Func<string, bool> <>9__6_1;

			// Token: 0x040003A9 RID: 937
			[Nullable(0)]
			public static Func<string, bool> <>9__6_2;
		}
	}
}
