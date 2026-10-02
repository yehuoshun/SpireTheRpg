using System;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Nodes.Screens.MainMenu;

namespace SpireTheRpg.Ui
{
	// Token: 0x02000016 RID: 22
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgWipScreen
	{
		// Token: 0x06000113 RID: 275 RVA: 0x0000CF7C File Offset: 0x0000B17C
		public static void Show(NCompendiumSubmenu hub)
		{
			RpgPlayerScreen.Hide(hub);
			RpgAbilityScreen.Hide(hub);
			RpgInventoryScreen.Hide(hub);
			RpgCodexScreen.Hide(hub);
			RpgWipScreen.Hide(hub);
			SpireLoc.ForceInject();
			ColorRect colorRect = RpgPlayerScreen.MakeDim("SpireRpgWipOverlay");
			Control control = RpgPlayerScreen.MakePlainPanel(new Vector2(720f, 520f));
			colorRect.AddChild(control, false, 0L);
			VBoxContainer vboxContainer = new VBoxContainer
			{
				MouseFilter = 2L
			};
			vboxContainer.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			vboxContainer.OffsetLeft = 40f;
			vboxContainer.OffsetRight = -40f;
			vboxContainer.OffsetTop = 32f;
			vboxContainer.OffsetBottom = -32f;
			vboxContainer.AddThemeConstantOverride("separation", 18);
			control.AddChild(vboxContainer, false, 0L);
			Label label = new Label
			{
				Text = SpireLoc.T("SPIRE_ABILITY.title", "Skills"),
				HorizontalAlignment = 1L,
				MouseFilter = 2L
			};
			label.AddThemeFontSizeOverride("font_size", 28);
			vboxContainer.AddChild(label, false, 0L);
			TextureRect textureRect = new TextureRect
			{
				Texture = SpireAssets.WipConstruction(),
				ExpandMode = 1L,
				StretchMode = 5L,
				CustomMinimumSize = new Vector2(0f, 280f),
				SizeFlagsVertical = 3L,
				SizeFlagsHorizontal = 3L,
				MouseFilter = 2L
			};
			vboxContainer.AddChild(textureRect, false, 0L);
			Label label2 = new Label
			{
				Text = SpireLoc.T("SPIRE_WIP.message", "Under construction"),
				HorizontalAlignment = 1L,
				MouseFilter = 2L
			};
			label2.AddThemeFontSizeOverride("font_size", 20);
			label2.Modulate = new Color(0.95f, 0.45f, 0.4f, 1f);
			vboxContainer.AddChild(label2, false, 0L);
			vboxContainer.AddChild(RpgPlayerScreen.MakeBackButton(delegate
			{
				RpgWipScreen.Hide(hub);
			}), false, 0L);
			hub.AddChild(colorRect, false, 0L);
			colorRect.MoveToFront();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000D19B File Offset: 0x0000B39B
		public static void Hide(NCompendiumSubmenu hub)
		{
			RpgPlayerScreen.FreeNamed(hub, "SpireRpgWipOverlay");
		}

		// Token: 0x04000082 RID: 130
		private const string OverlayName = "SpireRpgWipOverlay";

		// Token: 0x020000C0 RID: 192
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x06000689 RID: 1673 RVA: 0x00021716 File Offset: 0x0001F916
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x0600068A RID: 1674 RVA: 0x0002171E File Offset: 0x0001F91E
			internal void <Show>b__0()
			{
				RpgWipScreen.Hide(this.hub);
			}

			// Token: 0x040002A8 RID: 680
			[Nullable(0)]
			public NCompendiumSubmenu hub;
		}
	}
}
