using System;
using HarmonyLib;
using MegaCrit.Sts2.Core.GameActions;
using SpireTheRpg.Ui;

namespace SpireTheRpg.Patches
{
	// Token: 0x02000027 RID: 39
	[HarmonyPatch(typeof(MoveToMapCoordAction), "ExecuteAction")]
	internal static class RpgMoveHideInventoryPatch
	{
		// Token: 0x0600018F RID: 399 RVA: 0x0000EFF1 File Offset: 0x0000D1F1
		[HarmonyPrefix]
		private static void Prefix()
		{
			RpgInRunInventory.Hide(true);
		}
	}
}
