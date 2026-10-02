using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Relics;

namespace SpireTheRpg.Patches
{
	// Token: 0x0200002E RID: 46
	[HarmonyPatch]
	internal static class RpgRelicNodeReloadPatch
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x0000F437 File Offset: 0x0000D637
		[NullableContext(2)]
		private static MethodBase TargetMethod()
		{
			return AccessTools.Method(typeof(NRelic), "Reload", null, null);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000F44F File Offset: 0x0000D64F
		[NullableContext(1)]
		private static void Postfix(NRelic __instance)
		{
			RpgRelicIconShared.ApplyToNode(__instance);
		}
	}
}
