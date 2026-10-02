using System;
using System.Runtime.CompilerServices;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.HoverTips;
using MegaCrit.Sts2.Core.Nodes.Relics;
using SpireTheRpg.Core;
using SpireTheRpg.Relics;
using SpireTheRpg.Ui;

namespace SpireTheRpg.Patches
{
	// Token: 0x02000022 RID: 34
	[NullableContext(1)]
	[Nullable(0)]
	[HarmonyPatch]
	internal static class RpgEquippedGearRelicUiPatches
	{
		// Token: 0x06000179 RID: 377 RVA: 0x0000E4F8 File Offset: 0x0000C6F8
		[HarmonyPatch(typeof(NRelicInventoryHolder), "OnFocus")]
		[HarmonyPrefix]
		private static bool OnFocusPrefix(NRelicInventoryHolder __instance)
		{
			RpgItemData item;
			if (!RpgEquippedGearRelicUiPatches.TryItem(__instance, out item))
			{
				return true;
			}
			try
			{
				TextureRect icon = __instance.Relic.Icon;
				__instance.CreateTween().TweenProperty(icon, "scale", Vector2.One * 1.25f, 0.05);
				Control control = RpgEquippedGearRelicUiPatches.TipHost();
				if (control != null)
				{
					RpgItemTooltip.Show(control, item, __instance.Relic.GetGlobalRect());
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000E584 File Offset: 0x0000C784
		[HarmonyPatch(typeof(NRelicInventoryHolder), "OnUnfocus")]
		[HarmonyPrefix]
		private static bool OnUnfocusPrefix(NRelicInventoryHolder __instance)
		{
			RpgItemData rpgItemData;
			if (!RpgEquippedGearRelicUiPatches.TryItem(__instance, out rpgItemData))
			{
				return true;
			}
			try
			{
				TextureRect icon = __instance.Relic.Icon;
				__instance.CreateTween().TweenProperty(icon, "scale", Vector2.One, 1.0).SetEase(1L).SetTrans(5L);
				NHoverTipSet.Remove(__instance);
				Control control = RpgEquippedGearRelicUiPatches.TipHost();
				if (control != null)
				{
					RpgItemTooltip.Hide(control);
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000E610 File Offset: 0x0000C810
		[HarmonyPatch(typeof(NRelicInventory), "OnRelicClicked")]
		[HarmonyPrefix]
		private static bool OnRelicClickedPrefix(RelicModel model)
		{
			return !(model is RpgEquippedGearRelic);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000E620 File Offset: 0x0000C820
		private static bool TryItem(NRelicInventoryHolder holder, out RpgItemData item)
		{
			item = null;
			try
			{
				NRelic relic = holder.Relic;
				RpgEquippedGearRelic rpgEquippedGearRelic = ((relic != null) ? relic.Model : null) as RpgEquippedGearRelic;
				if (rpgEquippedGearRelic != null)
				{
					RpgItemData item2 = rpgEquippedGearRelic.Item;
					if (item2 != null)
					{
						item = item2;
						return true;
					}
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000E674 File Offset: 0x0000C874
		[NullableContext(2)]
		private static Control TipHost()
		{
			try
			{
				Control instance = NGame.Instance;
				if (instance != null)
				{
					return instance;
				}
			}
			catch
			{
			}
			try
			{
				Control instance2 = NRun.Instance;
				if (instance2 != null)
				{
					return instance2;
				}
			}
			catch
			{
			}
			return null;
		}
	}
}
