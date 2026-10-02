using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.addons.mega_text;
using MegaCrit.Sts2.Core.Nodes.Screens.MainMenu;
using SpireTheRpg.Core;

namespace SpireTheRpg.Ui
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgSubmenu
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000BE67 File Offset: 0x0000A067
		// (set) Token: 0x060000ED RID: 237 RVA: 0x0000BE6E File Offset: 0x0000A06E
		public static bool HubMode
		{
			[CompilerGenerated]
			get
			{
				return RpgSubmenu.<HubMode>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				RpgSubmenu.<HubMode>k__BackingField = value;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000BE76 File Offset: 0x0000A076
		public static void Open(NMainMenu menu)
		{
			RpgSubmenu._hubOpenRequested = true;
			RpgSubmenu.HubMode = true;
			menu.OpenCompendiumSubmenu();
			SpireTheRpgMainFile.Logger.Info("Opened Spire The RPG hub via cached Compendium submenu", 1);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000BE9B File Offset: 0x0000A09B
		public static bool ConsumeHubOpenRequest()
		{
			if (!RpgSubmenu._hubOpenRequested)
			{
				return false;
			}
			RpgSubmenu._hubOpenRequested = false;
			RpgSubmenu.HubMode = true;
			return true;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		public static void ApplyHubVisuals(NCompendiumSubmenu sub)
		{
			RpgSubmenu.HideAllScreens(sub);
			NShortSubmenuButton nodeOrNull = sub.GetNodeOrNull<NShortSubmenuButton>("%CardLibraryButton");
			NShortSubmenuButton nodeOrNull2 = sub.GetNodeOrNull<NShortSubmenuButton>("%RelicCollectionButton");
			NShortSubmenuButton nodeOrNull3 = sub.GetNodeOrNull<NShortSubmenuButton>("%PotionLabButton");
			NShortSubmenuButton nodeOrNull4 = sub.GetNodeOrNull<NShortSubmenuButton>("%BestiaryButton");
			Control nodeOrNull5 = sub.GetNodeOrNull<Control>("%LeaderboardsButton");
			Control nodeOrNull6 = sub.GetNodeOrNull<Control>("%StatisticsButton");
			Control nodeOrNull7 = sub.GetNodeOrNull<Control>("%RunHistoryButton");
			if (nodeOrNull != null)
			{
				if (SpireTheRpgConfig.EnableMetaEquipment)
				{
					nodeOrNull.Visible = true;
					RpgSubmenu.Relabel(nodeOrNull, "SPIRE_INVENTORY", "Inventory", "Equipment and gems");
				}
				else
				{
					nodeOrNull.Visible = false;
				}
			}
			if (nodeOrNull2 != null)
			{
				nodeOrNull2.Visible = true;
				RpgSubmenu.Relabel(nodeOrNull2, "SPIRE_ABILITY", "Skills", "Abilities and specials");
			}
			if (nodeOrNull4 != null)
			{
				nodeOrNull4.Visible = true;
				RpgSubmenu.Relabel(nodeOrNull4, "SPIRE_CODEX", "Codex", "Stats and affixes");
			}
			if (nodeOrNull3 != null)
			{
				nodeOrNull3.Visible = false;
			}
			if (nodeOrNull5 != null)
			{
				nodeOrNull5.Visible = false;
			}
			if (nodeOrNull6 != null)
			{
				nodeOrNull6.Visible = false;
			}
			if (nodeOrNull7 != null)
			{
				nodeOrNull7.Visible = false;
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000BFDC File Offset: 0x0000A1DC
		public static void RestoreCompendiumVisuals(NCompendiumSubmenu sub)
		{
			RpgSubmenu.HideAllScreens(sub);
			NShortSubmenuButton nodeOrNull = sub.GetNodeOrNull<NShortSubmenuButton>("%CardLibraryButton");
			NShortSubmenuButton nodeOrNull2 = sub.GetNodeOrNull<NShortSubmenuButton>("%RelicCollectionButton");
			NShortSubmenuButton nodeOrNull3 = sub.GetNodeOrNull<NShortSubmenuButton>("%PotionLabButton");
			NShortSubmenuButton nodeOrNull4 = sub.GetNodeOrNull<NShortSubmenuButton>("%BestiaryButton");
			Control nodeOrNull5 = sub.GetNodeOrNull<Control>("%LeaderboardsButton");
			Control nodeOrNull6 = sub.GetNodeOrNull<Control>("%StatisticsButton");
			Control nodeOrNull7 = sub.GetNodeOrNull<Control>("%RunHistoryButton");
			try
			{
				if (nodeOrNull != null)
				{
					nodeOrNull.SetIconAndLocalization("COMPENDIUM_CARD_LIBRARY");
				}
				if (nodeOrNull2 != null)
				{
					nodeOrNull2.SetIconAndLocalization("COMPENDIUM_RELIC_COLLECTION");
				}
				if (nodeOrNull3 != null)
				{
					nodeOrNull3.SetIconAndLocalization("COMPENDIUM_POTION_LAB");
				}
				if (nodeOrNull4 != null)
				{
					nodeOrNull4.SetIconAndLocalization("COMPENDIUM_BESTIARY");
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RestoreCompendiumVisuals loc failed: " + ex.Message, 1);
			}
			if (nodeOrNull != null)
			{
				nodeOrNull.Visible = true;
			}
			if (nodeOrNull2 != null)
			{
				nodeOrNull2.Visible = true;
			}
			if (nodeOrNull3 != null)
			{
				nodeOrNull3.Visible = true;
			}
			if (nodeOrNull4 != null)
			{
				nodeOrNull4.Visible = true;
			}
			if (nodeOrNull5 != null)
			{
				nodeOrNull5.Visible = false;
			}
			if (nodeOrNull6 != null)
			{
				nodeOrNull6.Visible = true;
			}
			if (nodeOrNull7 != null)
			{
				nodeOrNull7.Visible = true;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000C120 File Offset: 0x0000A320
		private static void HideAllScreens(NCompendiumSubmenu sub)
		{
			RpgPlaceholderScreen.Hide(sub);
			RpgPlayerScreen.Hide(sub);
			RpgAbilityScreen.Hide(sub);
			RpgInventoryScreen.Hide(sub);
			RpgWipScreen.Hide(sub);
			RpgCodexScreen.Hide(sub);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000C148 File Offset: 0x0000A348
		private static void Relabel(NShortSubmenuButton button, string prefix, string titleFb, string descFb)
		{
			FieldInfo locKeyPrefixField = RpgSubmenu.LocKeyPrefixField;
			if (locKeyPrefixField != null)
			{
				locKeyPrefixField.SetValue(button, prefix);
			}
			string text = SpireLoc.T(prefix + ".title", titleFb);
			string text2 = SpireLoc.T(prefix + ".description", descFb);
			FieldInfo titleField = RpgSubmenu.TitleField;
			MegaLabel megaLabel = ((titleField != null) ? titleField.GetValue(button) : null) as MegaLabel;
			if (megaLabel != null)
			{
				megaLabel.Text = text;
			}
			FieldInfo descriptionField = RpgSubmenu.DescriptionField;
			MegaRichTextLabel megaRichTextLabel = ((descriptionField != null) ? descriptionField.GetValue(button) : null) as MegaRichTextLabel;
			if (megaRichTextLabel != null)
			{
				megaRichTextLabel.Text = text2;
			}
			MegaLabel megaLabel2 = button.GetNodeOrNull<MegaLabel>("%Title") ?? button.GetNodeOrNull<MegaLabel>("Title");
			if (megaLabel2 != null)
			{
				megaLabel2.Text = text;
			}
			MegaRichTextLabel megaRichTextLabel2 = button.GetNodeOrNull<MegaRichTextLabel>("%Description") ?? button.GetNodeOrNull<MegaRichTextLabel>("Description");
			if (megaRichTextLabel2 != null)
			{
				megaRichTextLabel2.Text = text2;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000C234 File Offset: 0x0000A434
		// Note: this type is marked as 'beforefieldinit'.
		static RpgSubmenu()
		{
		}

		// Token: 0x04000070 RID: 112
		[CompilerGenerated]
		private static bool <HubMode>k__BackingField;

		// Token: 0x04000071 RID: 113
		private static bool _hubOpenRequested;

		// Token: 0x04000072 RID: 114
		[Nullable(2)]
		private static readonly FieldInfo TitleField = typeof(NShortSubmenuButton).GetField("_title", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000073 RID: 115
		[Nullable(2)]
		private static readonly FieldInfo DescriptionField = typeof(NShortSubmenuButton).GetField("_description", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000074 RID: 116
		[Nullable(2)]
		private static readonly FieldInfo LocKeyPrefixField = typeof(NShortSubmenuButton).GetField("_locKeyPrefix", BindingFlags.Instance | BindingFlags.NonPublic);
	}
}
