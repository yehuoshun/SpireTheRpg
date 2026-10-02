using System;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Relics;

namespace SpireTheRpg.Patches
{
	// Token: 0x0200002F RID: 47
	[HarmonyPatch(typeof(NRelic), "Model", 2)]
	internal static class RpgRelicModelSetPatch
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x0000F457 File Offset: 0x0000D657
		[NullableContext(1)]
		private static void Postfix(NRelic __instance)
		{
			RpgRelicIconShared.ApplyToNode(__instance);
		}
	}
}
