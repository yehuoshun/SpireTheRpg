using System;
using System.Runtime.CompilerServices;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;

namespace SpireTheRpg.Patches
{
	// Token: 0x0200002D RID: 45
	[HarmonyPatch(typeof(RelicModel), "BigIcon", 1)]
	internal static class RpgRelicBigIconPatch
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x0000F42B File Offset: 0x0000D62B
		[NullableContext(1)]
		private static bool Prefix(RelicModel __instance, ref Texture2D __result)
		{
			return !RpgRelicIconShared.TryOverride(__instance, ref __result);
		}
	}
}
