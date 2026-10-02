using System;
using BaseLib.Config.UI;
using HarmonyLib;

namespace SpireTheRpg.Patches
{
	// Token: 0x0200001E RID: 30
	[HarmonyPatch]
	internal static class ModConfigLocPatches
	{
		// Token: 0x0600016E RID: 366 RVA: 0x0000E179 File Offset: 0x0000C379
		[HarmonyPatch(typeof(NModConfigSubmenu), "_Ready")]
		[HarmonyPrefix]
		private static void ReadyPrefix()
		{
			SpireLoc.ForceInject();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000E180 File Offset: 0x0000C380
		[HarmonyPatch(typeof(NModConfigSubmenu), "LoadModConfig")]
		[HarmonyPrefix]
		private static void LoadModConfigPrefix()
		{
			SpireLoc.ForceInject();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000E187 File Offset: 0x0000C387
		[HarmonyPatch(typeof(NModConfigSubmenu), "OnSubmenuShown")]
		[HarmonyPrefix]
		private static void ShownPrefix()
		{
			SpireLoc.ForceInject();
		}
	}
}
