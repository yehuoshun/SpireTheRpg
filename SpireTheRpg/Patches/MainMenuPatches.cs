using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.addons.mega_text;
using MegaCrit.Sts2.Core.Nodes.GodotExtensions;
using MegaCrit.Sts2.Core.Nodes.Screens.MainMenu;
using SpireTheRpg.Core;
using SpireTheRpg.Ui;

namespace SpireTheRpg.Patches
{
	// Token: 0x0200001D RID: 29
	[NullableContext(1)]
	[Nullable(0)]
	[HarmonyPatch]
	internal static class MainMenuPatches
	{
		// Token: 0x0600015D RID: 349 RVA: 0x0000DD92 File Offset: 0x0000BF92
		[HarmonyPatch(typeof(NMainMenu), "_Ready")]
		[HarmonyPostfix]
		private static void ReadyPostfix(NMainMenu __instance)
		{
			SpireLoc.Inject();
			MainMenuPatches.TryInjectButton(__instance);
			RpgUpdateNotice.OnMainMenuReady(__instance);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000DDA5 File Offset: 0x0000BFA5
		[HarmonyPatch(typeof(NMainMenu), "RefreshButtons")]
		[HarmonyPostfix]
		private static void RefreshButtonsPostfix(NMainMenu __instance)
		{
			SpireLoc.Inject();
			MainMenuPatches.TryInjectButton(__instance);
			MainMenuPatches.RefreshButtonLabel(__instance);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000DDB8 File Offset: 0x0000BFB8
		[HarmonyPatch(typeof(NMainMenu), "OpenCompendiumSubmenu", new Type[]
		{

		})]
		[HarmonyPrefix]
		private static void OpenCompendiumPrefix()
		{
			if (!RpgSubmenu.ConsumeHubOpenRequest())
			{
				RpgSubmenu.HubMode = false;
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000DDC7 File Offset: 0x0000BFC7
		[HarmonyPatch(typeof(NCompendiumSubmenu), "OnSubmenuOpened")]
		[HarmonyPostfix]
		private static void CompendiumOpenedPostfix(NCompendiumSubmenu __instance)
		{
			if (RpgSubmenu.HubMode)
			{
				RpgSubmenu.ApplyHubVisuals(__instance);
				return;
			}
			RpgSubmenu.RestoreCompendiumVisuals(__instance);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000DDDD File Offset: 0x0000BFDD
		[HarmonyPatch(typeof(NCompendiumSubmenu), "OpenCardLibrary")]
		[HarmonyPrefix]
		private static bool OpenCardLibraryPrefix(NCompendiumSubmenu __instance)
		{
			if (!RpgSubmenu.HubMode)
			{
				return true;
			}
			if (!SpireTheRpgConfig.EnableMetaEquipment)
			{
				RpgWipScreen.Show(__instance);
				return false;
			}
			RpgInventoryScreen.Show(__instance, false);
			return false;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000DDFF File Offset: 0x0000BFFF
		[HarmonyPatch(typeof(NCompendiumSubmenu), "OpenRelicCollection")]
		[HarmonyPrefix]
		private static bool OpenRelicCollectionPrefix(NCompendiumSubmenu __instance)
		{
			if (!RpgSubmenu.HubMode)
			{
				return true;
			}
			RpgWipScreen.Show(__instance);
			return false;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000DE11 File Offset: 0x0000C011
		[HarmonyPatch(typeof(NCompendiumSubmenu), "OpenPotionLab")]
		[HarmonyPrefix]
		private static bool OpenPotionLabPrefix(NCompendiumSubmenu __instance)
		{
			if (!RpgSubmenu.HubMode)
			{
				return true;
			}
			RpgPlayerScreen.Show(__instance);
			return false;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000DE23 File Offset: 0x0000C023
		[HarmonyPatch(typeof(NCompendiumSubmenu), "OpenBestiary", new Type[]
		{

		})]
		[HarmonyPrefix]
		private static bool OpenBestiaryNoArgPrefix(NCompendiumSubmenu __instance)
		{
			if (!RpgSubmenu.HubMode)
			{
				return true;
			}
			RpgCodexScreen.Show(__instance);
			return false;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000DE35 File Offset: 0x0000C035
		[HarmonyPatch(typeof(NCompendiumSubmenu), "OpenBestiary", new Type[]
		{
			typeof(NButton)
		})]
		[HarmonyPrefix]
		private static bool OpenBestiaryButtonPrefix(NCompendiumSubmenu __instance)
		{
			if (!RpgSubmenu.HubMode)
			{
				return true;
			}
			RpgCodexScreen.Show(__instance);
			return false;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000DE48 File Offset: 0x0000C048
		private static void TryInjectButton(NMainMenu menu)
		{
			Control control = menu.GetNodeOrNull<Control>("%MainMenuTextButtons") ?? menu.GetNodeOrNull<Control>("MainMenuTextButtons");
			if (control == null)
			{
				return;
			}
			NMainMenuTextButton nodeOrNull = control.GetNodeOrNull<NMainMenuTextButton>("SpireTheRpgButton");
			if (nodeOrNull != null)
			{
				MainMenuPatches.PlaceAfterMultiplayer(control, nodeOrNull);
				MainMenuPatches.RefreshButtonLabel(menu);
				MainMenuPatches.WireReticle(menu, nodeOrNull);
				return;
			}
			NMainMenuTextButton nmainMenuTextButton;
			if ((nmainMenuTextButton = control.GetNodeOrNull<NMainMenuTextButton>("MultiplayerButton")) == null)
			{
				nmainMenuTextButton = (control.GetNodeOrNull<NMainMenuTextButton>("CompendiumButton") ?? control.GetChildren(false).OfType<NMainMenuTextButton>().FirstOrDefault<NMainMenuTextButton>());
			}
			NMainMenuTextButton nmainMenuTextButton2 = nmainMenuTextButton;
			if (nmainMenuTextButton2 == null)
			{
				return;
			}
			NMainMenuTextButton nmainMenuTextButton3 = nmainMenuTextButton2.Duplicate(6) as NMainMenuTextButton;
			if (nmainMenuTextButton3 == null)
			{
				return;
			}
			nmainMenuTextButton3.Name = "SpireTheRpgButton";
			nmainMenuTextButton3.Visible = true;
			control.AddChild(nmainMenuTextButton3, false, 0L);
			MainMenuPatches.PlaceAfterMultiplayer(control, nmainMenuTextButton3);
			MainMenuPatches.ApplyButtonLabel(nmainMenuTextButton3);
			NClickableControl nclickableControl = nmainMenuTextButton3;
			NClickableControl.ReleasedEventHandler releasedEventHandler;
			if ((releasedEventHandler = MainMenuPatches.<>O.<0>__OnRpgButtonReleased) == null)
			{
				releasedEventHandler = (MainMenuPatches.<>O.<0>__OnRpgButtonReleased = new NClickableControl.ReleasedEventHandler(MainMenuPatches.OnRpgButtonReleased));
			}
			nclickableControl.Released += releasedEventHandler;
			MainMenuPatches.WireReticle(menu, nmainMenuTextButton3);
			SpireTheRpgMainFile.Logger.Info("Injected Spire The RPG main menu button under Multiplayer", 1);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000DF5C File Offset: 0x0000C15C
		private static void WireReticle(NMainMenu menu, NMainMenuTextButton button)
		{
			if (button.HasMeta("spire_rpg_reticle_wired") || MainMenuPatches.FocusedMethod == null || MainMenuPatches.UnfocusedMethod == null)
			{
				return;
			}
			button.Focused += delegate(NClickableControl b)
			{
				try
				{
					MainMenuPatches.FocusedMethod.Invoke(menu, new object[]
					{
						(b as NMainMenuTextButton) ?? button
					});
				}
				catch (Exception ex)
				{
					SpireTheRpgMainFile.Logger.Info("Reticle focus failed: " + ex.Message, 1);
				}
			};
			button.Unfocused += delegate(NClickableControl _)
			{
				try
				{
					MainMenuPatches.UnfocusedMethod.Invoke(menu, new object[]
					{
						button
					});
				}
				catch (Exception ex)
				{
					SpireTheRpgMainFile.Logger.Info("Reticle unfocus failed: " + ex.Message, 1);
				}
			};
			button.SetMeta("spire_rpg_reticle_wired", true);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000DFF8 File Offset: 0x0000C1F8
		private static void PlaceAfterMultiplayer(Control buttons, Node clone)
		{
			Node nodeOrNull = buttons.GetNodeOrNull<Node>("MultiplayerButton");
			if (nodeOrNull != null)
			{
				buttons.MoveChild(clone, nodeOrNull.GetIndex(false) + 1);
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000E02C File Offset: 0x0000C22C
		private static void RefreshButtonLabel(NMainMenu menu)
		{
			Control control = menu.GetNodeOrNull<Control>("%MainMenuTextButtons") ?? menu.GetNodeOrNull<Control>("MainMenuTextButtons");
			NMainMenuTextButton nmainMenuTextButton = (control != null) ? control.GetNodeOrNull<NMainMenuTextButton>("SpireTheRpgButton") : null;
			if (nmainMenuTextButton != null)
			{
				MainMenuPatches.ApplyButtonLabel(nmainMenuTextButton);
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000E080 File Offset: 0x0000C280
		private static void ApplyButtonLabel(NMainMenuTextButton button)
		{
			string text = SpireLoc.T("SPIRE_THE_RPG", "Spire The RPG");
			if (button.label != null)
			{
				button.label.Text = text;
				return;
			}
			MegaLabel megaLabel = button.GetNodeOrNull<MegaLabel>("Label") ?? (button.FindChild("Label", true, false) as MegaLabel);
			if (megaLabel != null)
			{
				megaLabel.Text = text;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
		private static void OnRpgButtonReleased(NClickableControl button)
		{
			NMainMenu nmainMenu = MainMenuPatches.FindAncestor<NMainMenu>(button);
			if (nmainMenu == null)
			{
				return;
			}
			SpireLoc.Inject();
			RpgSubmenu.Open(nmainMenu);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000E108 File Offset: 0x0000C308
		[NullableContext(2)]
		private static T FindAncestor<[Nullable(1)] T>(Node node) where T : class
		{
			while (node != null)
			{
				T t = node as T;
				if (t != null)
				{
					return t;
				}
				node = node.GetParent();
			}
			return default(T);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000E141 File Offset: 0x0000C341
		// Note: this type is marked as 'beforefieldinit'.
		static MainMenuPatches()
		{
		}

		// Token: 0x04000085 RID: 133
		private const string ButtonName = "SpireTheRpgButton";

		// Token: 0x04000086 RID: 134
		private const string ReticleMeta = "spire_rpg_reticle_wired";

		// Token: 0x04000087 RID: 135
		[Nullable(2)]
		private static readonly MethodInfo FocusedMethod = typeof(NMainMenu).GetMethod("MainMenuButtonFocused", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000088 RID: 136
		[Nullable(2)]
		private static readonly MethodInfo UnfocusedMethod = typeof(NMainMenu).GetMethod("MainMenuButtonUnfocused", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x020000C4 RID: 196
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040002B4 RID: 692
			[Nullable(0)]
			public static NClickableControl.ReleasedEventHandler <0>__OnRpgButtonReleased;
		}

		// Token: 0x020000C5 RID: 197
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x06000690 RID: 1680 RVA: 0x00021C5A File Offset: 0x0001FE5A
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x06000691 RID: 1681 RVA: 0x00021C64 File Offset: 0x0001FE64
			internal void <WireReticle>b__0(NClickableControl b)
			{
				try
				{
					MainMenuPatches.FocusedMethod.Invoke(this.menu, new object[]
					{
						(b as NMainMenuTextButton) ?? this.button
					});
				}
				catch (Exception ex)
				{
					SpireTheRpgMainFile.Logger.Info("Reticle focus failed: " + ex.Message, 1);
				}
			}

			// Token: 0x06000692 RID: 1682 RVA: 0x00021CCC File Offset: 0x0001FECC
			internal void <WireReticle>b__1(NClickableControl _)
			{
				try
				{
					MainMenuPatches.UnfocusedMethod.Invoke(this.menu, new object[]
					{
						this.button
					});
				}
				catch (Exception ex)
				{
					SpireTheRpgMainFile.Logger.Info("Reticle unfocus failed: " + ex.Message, 1);
				}
			}

			// Token: 0x040002B5 RID: 693
			[Nullable(0)]
			public NMainMenu menu;

			// Token: 0x040002B6 RID: 694
			[Nullable(0)]
			public NMainMenuTextButton button;
		}
	}
}
