using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using SpireTheRpg.Powers;

namespace SpireTheRpg.Patches
{
	// Token: 0x02000032 RID: 50
	[NullableContext(2)]
	[Nullable(0)]
	[HarmonyPatch]
	internal static class RpgStrikeFormDamageCompatPatch
	{
		// Token: 0x060001BF RID: 447 RVA: 0x0000FF90 File Offset: 0x0000E190
		private static MethodBase TargetMethod()
		{
			return (from m in (from m in typeof(AbstractModel).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
			where m.Name == "ModifyDamageAdditive"
			select m).ToArray<MethodInfo>()
			orderby m.GetParameters().Length descending
			select m).FirstOrDefault<MethodInfo>();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00010000 File Offset: 0x0000E200
		private static void Postfix([Nullable(1)] AbstractModel __instance, Creature dealer, CardModel cardSource, ref decimal __result)
		{
			StrikeFormPower strikeFormPower = __instance as StrikeFormPower;
			if (strikeFormPower == null)
			{
				return;
			}
			decimal num = strikeFormPower.CalcStrikeDamageBonus(dealer, cardSource);
			if (num != 0m)
			{
				__result += num;
			}
		}

		// Token: 0x020000D4 RID: 212
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006AE RID: 1710 RVA: 0x00022B3E File Offset: 0x00020D3E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006AF RID: 1711 RVA: 0x00022B4A File Offset: 0x00020D4A
			public <>c()
			{
			}

			// Token: 0x060006B0 RID: 1712 RVA: 0x00022B52 File Offset: 0x00020D52
			[NullableContext(0)]
			internal bool <TargetMethod>b__0_0(MethodInfo m)
			{
				return m.Name == "ModifyDamageAdditive";
			}

			// Token: 0x060006B1 RID: 1713 RVA: 0x00022B64 File Offset: 0x00020D64
			[NullableContext(0)]
			internal int <TargetMethod>b__0_1(MethodInfo m)
			{
				return m.GetParameters().Length;
			}

			// Token: 0x040002E4 RID: 740
			[Nullable(0)]
			public static readonly RpgStrikeFormDamageCompatPatch.<>c <>9 = new RpgStrikeFormDamageCompatPatch.<>c();

			// Token: 0x040002E5 RID: 741
			[Nullable(0)]
			public static Func<MethodInfo, bool> <>9__0_0;

			// Token: 0x040002E6 RID: 742
			[Nullable(0)]
			public static Func<MethodInfo, int> <>9__0_1;
		}
	}
}
