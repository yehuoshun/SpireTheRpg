using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rewards;
using SpireTheRpg.Core;

namespace SpireTheRpg.Patches
{
	// Token: 0x02000033 RID: 51
	[NullableContext(1)]
	[Nullable(0)]
	[HarmonyPatch]
	internal static class RpgTwinExistencePatches
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00010040 File Offset: 0x0000E240
		[HarmonyPatch(typeof(CardReward), "OnSelect")]
		[HarmonyPrefix]
		private static void CardRewardOnSelectPrefix(CardReward __instance, [Nullable(2)] out object __state)
		{
			__state = RpgTwinExistence.Begin(__instance.Player);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0001004F File Offset: 0x0000E24F
		[HarmonyPatch(typeof(CardReward), "OnSelect")]
		[HarmonyPostfix]
		private static void CardRewardOnSelectPostfix([Nullable(2)] object __state, ref Task<bool> __result)
		{
			__result = RpgTwinExistence.Complete(__result, __state);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0001005B File Offset: 0x0000E25B
		[HarmonyPatch(typeof(CardPileCmd), "Add", new Type[]
		{
			typeof(CardModel),
			typeof(PileType),
			typeof(CardPilePosition),
			typeof(AbstractModel),
			typeof(bool)
		})]
		[HarmonyPostfix]
		private static void CardPileAddPostfix(CardModel card, PileType newPileType, [Nullable(2)] AbstractModel clonedBy, ref Task<CardPileAddResult> __result)
		{
			RpgTwinExistence.TrackAdd(card, newPileType, clonedBy, ref __result);
		}
	}
}
