using System;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Nodes.Screens.MainMenu;
using SpireTheRpg.Core;

namespace SpireTheRpg.Ui
{
	// Token: 0x02000005 RID: 5
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgAbilityScreen
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00003018 File Offset: 0x00001218
		public static void Show(NCompendiumSubmenu hub)
		{
			RpgPlayerScreen.Hide(hub);
			RpgInventoryScreen.Hide(hub);
			RpgAbilityScreen.Hide(hub);
			RpgPlayerProfile rpgPlayerProfile = RpgPlayerProfile.Current();
			ColorRect colorRect = RpgPlayerScreen.MakeDim("SpireRpgAbilityOverlay");
			Control control = RpgPlayerScreen.MakePlainPanel(new Vector2(820f, 720f));
			colorRect.AddChild(control, false, 0L);
			VBoxContainer vboxContainer = new VBoxContainer
			{
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
				MouseFilter = 2L
			};
			label.AddThemeFontSizeOverride("font_size", 30);
			Label label2 = new Label();
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Lv");
			defaultInterpolatedStringHandler.AppendFormatted<int>(rpgPlayerProfile.Level);
			label2.Text = defaultInterpolatedStringHandler.ToStringAndClear();
			label2.HorizontalAlignment = 2L;
			label2.MouseFilter = 2L;
			Label label3 = label2;
			label3.AddThemeFontSizeOverride("font_size", 30);
			hboxContainer.AddChild(label, false, 0L);
			hboxContainer.AddChild(label3, false, 0L);
			vboxContainer.AddChild(hboxContainer, false, 0L);
			HBoxContainer hboxContainer2 = new HBoxContainer
			{
				MouseFilter = 2L
			};
			ProgressBar progressBar = new ProgressBar
			{
				MinValue = 0.0,
				MaxValue = (double)Math.Max(1, rpgPlayerProfile.XpToNext),
				Value = (double)rpgPlayerProfile.Xp,
				CustomMinimumSize = new Vector2(0f, 24f),
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
				SizeFlagsVertical = 3L,
				MouseFilter = 2L
			};
			vboxContainer2.AddThemeConstantOverride("separation", 8);
			vboxContainer.AddChild(vboxContainer2, false, 0L);
			foreach (RpgAbilityInfo rpgAbilityInfo in rpgPlayerProfile.Abilities)
			{
				string id = rpgAbilityInfo.Id;
				string text;
				if (!(id == "strike"))
				{
					if (!(id == "guard"))
					{
						text = "res://images/atlases/stats_screen_atlas.sprites/stats_potions_seen.tres";
					}
					else
					{
						text = "res://images/atlases/stats_screen_atlas.sprites/stats_chest.tres";
					}
				}
				else
				{
					text = "res://images/atlases/stats_screen_atlas.sprites/stats_swords.tres";
				}
				string iconPath = text;
				vboxContainer2.AddChild(RpgPlayerScreen.MakeStatRow(iconPath, rpgAbilityInfo.Title, string.Empty, rpgAbilityInfo.Description), false, 0L);
			}
			vboxContainer.AddChild(RpgPlayerScreen.MakeBackButton(delegate
			{
				RpgAbilityScreen.Hide(hub);
			}), false, 0L);
			hub.AddChild(colorRect, false, 0L);
			colorRect.MoveToFront();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000033B8 File Offset: 0x000015B8
		public static void Hide(NCompendiumSubmenu hub)
		{
			RpgPlayerScreen.FreeNamed(hub, "SpireRpgAbilityOverlay");
		}

		// Token: 0x0400000D RID: 13
		private const string OverlayName = "SpireRpgAbilityOverlay";

		// Token: 0x0400000E RID: 14
		private const string StrikeIcon = "res://images/atlases/stats_screen_atlas.sprites/stats_swords.tres";

		// Token: 0x0400000F RID: 15
		private const string GuardIcon = "res://images/atlases/stats_screen_atlas.sprites/stats_chest.tres";

		// Token: 0x04000010 RID: 16
		private const string BrewIcon = "res://images/atlases/stats_screen_atlas.sprites/stats_potions_seen.tres";

		// Token: 0x02000081 RID: 129
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x060005A4 RID: 1444 RVA: 0x0001DDBB File Offset: 0x0001BFBB
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x060005A5 RID: 1445 RVA: 0x0001DDC3 File Offset: 0x0001BFC3
			internal void <Show>b__0()
			{
				RpgAbilityScreen.Hide(this.hub);
			}

			// Token: 0x040001C7 RID: 455
			[Nullable(0)]
			public NCompendiumSubmenu hub;
		}
	}
}
