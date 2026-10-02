using System;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Nodes.Screens.MainMenu;
using SpireTheRpg.Core;

namespace SpireTheRpg.Ui
{
	// Token: 0x02000011 RID: 17
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgPlayerScreen
	{
		// Token: 0x060000DA RID: 218 RVA: 0x0000ADC8 File Offset: 0x00008FC8
		public static void Show(NCompendiumSubmenu hub)
		{
			RpgAbilityScreen.Hide(hub);
			RpgInventoryScreen.Hide(hub);
			RpgPlayerScreen.Hide(hub);
			RpgPlayerProfile rpgPlayerProfile = RpgPlayerProfile.Current();
			ColorRect colorRect = RpgPlayerScreen.MakeDim("SpireRpgPlayerOverlay");
			Control control = RpgPlayerScreen.MakePlainPanel(new Vector2(760f, 720f));
			colorRect.AddChild(control, false, 0L);
			VBoxContainer vboxContainer = new VBoxContainer
			{
				Name = "Content",
				MouseFilter = 2L
			};
			vboxContainer.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			vboxContainer.OffsetLeft = 48f;
			vboxContainer.OffsetRight = -48f;
			vboxContainer.OffsetTop = 36f;
			vboxContainer.OffsetBottom = -36f;
			vboxContainer.AddThemeConstantOverride("separation", 12);
			control.AddChild(vboxContainer, false, 0L);
			HBoxContainer hboxContainer = new HBoxContainer
			{
				MouseFilter = 2L
			};
			Label label = new Label
			{
				Text = rpgPlayerProfile.Name,
				SizeFlagsHorizontal = 3L,
				HorizontalAlignment = 0L,
				MouseFilter = 2L
			};
			label.AddThemeFontSizeOverride("font_size", 32);
			Label label2 = new Label();
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Lv");
			defaultInterpolatedStringHandler.AppendFormatted<int>(rpgPlayerProfile.Level);
			label2.Text = defaultInterpolatedStringHandler.ToStringAndClear();
			label2.HorizontalAlignment = 2L;
			label2.MouseFilter = 2L;
			Label label3 = label2;
			label3.AddThemeFontSizeOverride("font_size", 32);
			hboxContainer.AddChild(label, false, 0L);
			hboxContainer.AddChild(label3, false, 0L);
			vboxContainer.AddChild(hboxContainer, false, 0L);
			HBoxContainer hboxContainer2 = new HBoxContainer
			{
				MouseFilter = 2L
			};
			hboxContainer2.AddThemeConstantOverride("separation", 10);
			ProgressBar progressBar = new ProgressBar
			{
				MinValue = 0.0,
				MaxValue = (double)Math.Max(1, rpgPlayerProfile.XpToNext),
				Value = (double)rpgPlayerProfile.Xp,
				CustomMinimumSize = new Vector2(0f, 26f),
				SizeFlagsHorizontal = 3L,
				ShowPercentage = false,
				MouseFilter = 2L
			};
			Label label4 = new Label();
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted<int>(rpgPlayerProfile.Xp);
			defaultInterpolatedStringHandler.AppendLiteral("/");
			defaultInterpolatedStringHandler.AppendFormatted<int>(rpgPlayerProfile.XpToNext);
			label4.Text = defaultInterpolatedStringHandler.ToStringAndClear();
			label4.CustomMinimumSize = new Vector2(110f, 0f);
			label4.HorizontalAlignment = 2L;
			label4.MouseFilter = 2L;
			Label label5 = label4;
			hboxContainer2.AddChild(progressBar, false, 0L);
			hboxContainer2.AddChild(label5, false, 0L);
			vboxContainer.AddChild(hboxContainer2, false, 0L);
			VBoxContainer vboxContainer2 = new VBoxContainer
			{
				Name = "Stats",
				SizeFlagsVertical = 3L,
				MouseFilter = 2L
			};
			vboxContainer2.AddThemeConstantOverride("separation", 10);
			vboxContainer.AddChild(vboxContainer2, false, 0L);
			vboxContainer2.AddChild(RpgPlayerScreen.MakeStatRow("res://images/atlases/stats_screen_atlas.sprites/stats_swords.tres", SpireLoc.T("SPIRE_CHARACTER.strength", "Strength"), rpgPlayerProfile.Strength.ToString(), string.Empty), false, 0L);
			vboxContainer2.AddChild(RpgPlayerScreen.MakeStatRow("res://images/atlases/stats_screen_atlas.sprites/stats_clock.tres", SpireLoc.T("SPIRE_CHARACTER.dexterity", "Agility"), rpgPlayerProfile.Dexterity.ToString(), string.Empty), false, 0L);
			vboxContainer2.AddChild(RpgPlayerScreen.MakeStatRow("res://images/atlases/stats_screen_atlas.sprites/stats_potions_seen.tres", SpireLoc.T("SPIRE_CHARACTER.intelligence", "Intellect"), rpgPlayerProfile.Intelligence.ToString(), string.Empty), false, 0L);
			vboxContainer2.AddChild(RpgPlayerScreen.MakeStatRow("res://images/atlases/stats_screen_atlas.sprites/stats_chest.tres", SpireLoc.T("SPIRE_CHARACTER.vitality", "Vitality"), rpgPlayerProfile.Vitality.ToString(), string.Empty), false, 0L);
			vboxContainer.AddChild(RpgPlayerScreen.MakeBackButton(delegate
			{
				RpgPlayerScreen.Hide(hub);
			}), false, 0L);
			hub.AddChild(colorRect, false, 0L);
			colorRect.MoveToFront();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000B1D9 File Offset: 0x000093D9
		public static void Hide(NCompendiumSubmenu hub)
		{
			RpgPlayerScreen.FreeNamed(hub, "SpireRpgPlayerOverlay");
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000B1E8 File Offset: 0x000093E8
		internal static void FreeNamed(Node parent, string name)
		{
			for (;;)
			{
				Node nodeOrNull = parent.GetNodeOrNull<Node>(name);
				if (nodeOrNull == null)
				{
					break;
				}
				nodeOrNull.Name = name + "_dying";
				nodeOrNull.QueueFree();
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000B224 File Offset: 0x00009424
		internal static Control MakeStatRow(string iconPath, string topLabel, string topValue, string bottom)
		{
			HBoxContainer hboxContainer = new HBoxContainer
			{
				CustomMinimumSize = new Vector2(0f, 64f),
				SizeFlagsHorizontal = 3L,
				MouseFilter = 2L
			};
			hboxContainer.AddThemeConstantOverride("separation", 14);
			TextureRect textureRect = new TextureRect
			{
				CustomMinimumSize = new Vector2(56f, 56f),
				ExpandMode = 1L,
				StretchMode = 5L,
				MouseFilter = 2L
			};
			try
			{
				textureRect.Texture = ResourceLoader.Load<Texture2D>(iconPath, null, 1L);
			}
			catch
			{
			}
			hboxContainer.AddChild(textureRect, false, 0L);
			VBoxContainer vboxContainer = new VBoxContainer
			{
				SizeFlagsHorizontal = 3L,
				SizeFlagsVertical = 4L,
				MouseFilter = 2L
			};
			vboxContainer.AddThemeConstantOverride("separation", 2);
			Label label = new Label
			{
				Text = (string.IsNullOrEmpty(topValue) ? topLabel : (topLabel + "  " + topValue)),
				MouseFilter = 2L
			};
			label.AddThemeFontSizeOverride("font_size", 22);
			vboxContainer.AddChild(label, false, 0L);
			if (!string.IsNullOrEmpty(bottom))
			{
				Label label2 = new Label
				{
					Text = bottom,
					MouseFilter = 2L
				};
				label2.AddThemeFontSizeOverride("font_size", 18);
				label2.Modulate = new Color(0.75f, 0.8f, 0.9f, 1f);
				vboxContainer.AddChild(label2, false, 0L);
			}
			hboxContainer.AddChild(vboxContainer, false, 0L);
			return hboxContainer;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000B3AC File Offset: 0x000095AC
		internal static ColorRect MakeDim(string name)
		{
			ColorRect colorRect = new ColorRect();
			colorRect.Name = name;
			colorRect.Color = new Color(0.05f, 0.05f, 0.07f, 0.92f);
			colorRect.MouseFilter = 0L;
			colorRect.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			return colorRect;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000B400 File Offset: 0x00009600
		internal static Control MakePlainPanel(Vector2 size)
		{
			Control control = new Control();
			control.CustomMinimumSize = size;
			control.Size = size;
			control.MouseFilter = 0L;
			control.SetAnchorsPreset(8L, false);
			control.GrowHorizontal = 2L;
			control.GrowVertical = 2L;
			control.OffsetLeft = -size.X * 0.5f;
			control.OffsetRight = size.X * 0.5f;
			control.OffsetTop = -size.Y * 0.5f;
			control.OffsetBottom = size.Y * 0.5f;
			ColorRect colorRect = new ColorRect
			{
				Color = new Color(0.12f, 0.12f, 0.14f, 0.96f),
				MouseFilter = 2L
			};
			colorRect.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			control.AddChild(colorRect, false, 0L);
			return control;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000B4D0 File Offset: 0x000096D0
		internal static Button MakeBackButton(Action onBack)
		{
			Button button = new Button();
			button.Text = SpireLoc.T("SPIRE_PLACEHOLDER_BACK", "Back");
			button.CustomMinimumSize = new Vector2(160f, 48f);
			button.SizeFlagsHorizontal = 4L;
			button.MouseFilter = 0L;
			button.Pressed += onBack;
			return button;
		}

		// Token: 0x04000065 RID: 101
		private const string OverlayName = "SpireRpgPlayerOverlay";

		// Token: 0x04000066 RID: 102
		internal const string StrengthIcon = "res://images/atlases/stats_screen_atlas.sprites/stats_swords.tres";

		// Token: 0x04000067 RID: 103
		internal const string AgilityIcon = "res://images/atlases/stats_screen_atlas.sprites/stats_clock.tres";

		// Token: 0x04000068 RID: 104
		internal const string IntellectIcon = "res://images/atlases/stats_screen_atlas.sprites/stats_potions_seen.tres";

		// Token: 0x04000069 RID: 105
		internal const string VitalityIcon = "res://images/atlases/stats_screen_atlas.sprites/stats_chest.tres";

		// Token: 0x0400006A RID: 106
		internal const string ArmorIcon = "res://images/atlases/stats_screen_atlas.sprites/stats_chest.tres";

		// Token: 0x020000B5 RID: 181
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x06000667 RID: 1639 RVA: 0x00020A3E File Offset: 0x0001EC3E
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06000668 RID: 1640 RVA: 0x00020A46 File Offset: 0x0001EC46
			internal void <Show>b__0()
			{
				RpgPlayerScreen.Hide(this.hub);
			}

			// Token: 0x0400027C RID: 636
			[Nullable(0)]
			public NCompendiumSubmenu hub;
		}
	}
}
