using System;
using System.Runtime.CompilerServices;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;

namespace SpireTheRpg.Patches
{
	// Token: 0x0200002C RID: 44
	[HarmonyPatch(typeof(RelicModel), "IconOutline", 1)]
	internal static class RpgRelicIconOutlinePatch
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x0000F41F File Offset: 0x0000D61F
		[NullableContext(1)]
		private static bool Prefix(RelicModel __instance, ref Texture2D __result)
		{
			return !RpgRelicIconShared.TryOverride(__instance, ref __result);
		}
	}
}
