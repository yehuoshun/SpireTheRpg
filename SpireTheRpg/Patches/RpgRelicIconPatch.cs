using System;
using System.Runtime.CompilerServices;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;

namespace SpireTheRpg.Patches
{
	// Token: 0x0200002B RID: 43
	[HarmonyPatch(typeof(RelicModel), "Icon", 1)]
	internal static class RpgRelicIconPatch
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x0000F413 File Offset: 0x0000D613
		[NullableContext(1)]
		private static bool Prefix(RelicModel __instance, ref Texture2D __result)
		{
			return !RpgRelicIconShared.TryOverride(__instance, ref __result);
		}
	}
}
