using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using SpireTheRpg.Relics;

namespace SpireTheRpg.Patches
{
	// Token: 0x02000028 RID: 40
	[NullableContext(2)]
	[Nullable(0)]
	[HarmonyPatch]
	internal static class RpgModifyDamageMultCompatPatch
	{
		// Token: 0x06000190 RID: 400 RVA: 0x0000EFFC File Offset: 0x0000D1FC
		private static MethodBase TargetMethod()
		{
			return (from m in (from m in typeof(AbstractModel).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
			where m.Name == "ModifyDamageMultiplicative"
			select m).ToArray<MethodInfo>()
			orderby m.GetParameters().Length descending
			select m).FirstOrDefault<MethodInfo>();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000F06C File Offset: 0x0000D26C
		private static void Postfix([Nullable(1)] AbstractModel __instance, Creature dealer, CardModel cardSource, ref decimal __result)
		{
			RpgDataRelic rpgDataRelic = __instance as RpgDataRelic;
			if (rpgDataRelic == null)
			{
				return;
			}
			decimal num = rpgDataRelic.CalcDamageMult(dealer, cardSource);
			if (num != 1m)
			{
				__result *= num;
			}
		}

		// Token: 0x020000CC RID: 204
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006A0 RID: 1696 RVA: 0x000225A2 File Offset: 0x000207A2
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006A1 RID: 1697 RVA: 0x000225AE File Offset: 0x000207AE
			public <>c()
			{
			}

			// Token: 0x060006A2 RID: 1698 RVA: 0x000225B6 File Offset: 0x000207B6
			[NullableContext(0)]
			internal bool <TargetMethod>b__0_0(MethodInfo m)
			{
				return m.Name == "ModifyDamageMultiplicative";
			}

			// Token: 0x060006A3 RID: 1699 RVA: 0x000225C8 File Offset: 0x000207C8
			[NullableContext(0)]
			internal int <TargetMethod>b__0_1(MethodInfo m)
			{
				return m.GetParameters().Length;
			}

			// Token: 0x040002D0 RID: 720
			[Nullable(0)]
			public static readonly RpgModifyDamageMultCompatPatch.<>c <>9 = new RpgModifyDamageMultCompatPatch.<>c();

			// Token: 0x040002D1 RID: 721
			[Nullable(0)]
			public static Func<MethodInfo, bool> <>9__0_0;

			// Token: 0x040002D2 RID: 722
			[Nullable(0)]
			public static Func<MethodInfo, int> <>9__0_1;
		}
	}
}
