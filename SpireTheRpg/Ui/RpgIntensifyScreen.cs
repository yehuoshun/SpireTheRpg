using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Entities.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Screens.Overlays;
using MegaCrit.Sts2.Core.Nodes.Screens.ScreenContext;
using SpireTheRpg.Core;

namespace SpireTheRpg.Ui
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RpgIntensifyScreen : Control, IOverlayScreen, IScreenContext
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00004ED8 File Offset: 0x000030D8
		public NetScreenType ScreenType
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00004EDC File Offset: 0x000030DC
		public bool UseSharedBackstop
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00004EDF File Offset: 0x000030DF
		[Nullable(2)]
		public Control DefaultFocusedControl
		{
			[NullableContext(2)]
			get
			{
				return this._confirm;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00004EE7 File Offset: 0x000030E7
		[Nullable(2)]
		public Control FocusedControlFromTopBar
		{
			[NullableContext(2)]
			get
			{
				return this.DefaultFocusedControl;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004EF0 File Offset: 0x000030F0
		private RpgIntensifyScreen(List<RpgItemData> items, RpgIntensifyScreen.PickChrome chrome)
		{
			this._items = items;
			this._chrome = chrome;
			base.Name = "SpireRpgIntensifyScreen";
			base.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			base.MouseFilter = 0L;
			base.FocusMode = 2L;
			base.Visible = true;
			this.Build();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004F7B File Offset: 0x0000317B
		public override void _Ready()
		{
			base.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			base.MoveToFront();
			base._Ready();
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00004F98 File Offset: 0x00003198
		public static RpgIntensifyScreen.PickChrome IntensifyChrome
		{
			get
			{
				SpireLoc.ForceInject();
				string title = SpireLoc.Item("ui.intensify.title", "Intensify");
				string hint = SpireLoc.Item("ui.intensify.hint", "Select an item. Preview shows the result.");
				string confirm = SpireLoc.Item("ui.intensify.confirm", "Intensify");
				Func<RpgItemData, RpgItemData> preview;
				if ((preview = RpgIntensifyScreen.<>O.<0>__Preview) == null)
				{
					preview = (RpgIntensifyScreen.<>O.<0>__Preview = new Func<RpgItemData, RpgItemData>(RpgItemIntensify.Preview));
				}
				return new RpgIntensifyScreen.PickChrome(title, hint, confirm, preview);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00004FF8 File Offset: 0x000031F8
		public static RpgIntensifyScreen.PickChrome CleanseChrome
		{
			get
			{
				SpireLoc.ForceInject();
				return new RpgIntensifyScreen.PickChrome(SpireLoc.Item("ui.cleanse.title", "Cleanse"), SpireLoc.Item("ui.cleanse.hint", "Select a cursed or bound item. Preview shows cleansed result."), SpireLoc.Item("ui.cleanse.confirm", "Cleanse"), delegate(RpgItemData item)
				{
					RpgItemData rpgItemData = item.Clone();
					rpgItemData.ClearCurses();
					return rpgItemData;
				});
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000505B File Offset: 0x0000325B
		public static RpgIntensifyScreen.PickChrome IdentifyChrome
		{
			get
			{
				SpireLoc.ForceInject();
				return new RpgIntensifyScreen.PickChrome(SpireLoc.Item("ui.identify.title", "Identify"), SpireLoc.Item("ui.identify.hint", "Select an unidentified item. Confirm to reveal stats."), SpireLoc.Item("ui.identify.confirm", "Identify"), null);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00005098 File Offset: 0x00003298
		public static Task<bool> OfferAsync()
		{
			RpgIntensifyScreen.<OfferAsync>d__33 <OfferAsync>d__;
			<OfferAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<OfferAsync>d__.<>1__state = -1;
			<OfferAsync>d__.<>t__builder.Start<RpgIntensifyScreen.<OfferAsync>d__33>(ref <OfferAsync>d__);
			return <OfferAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000050D4 File Offset: 0x000032D4
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Task<RpgIntensifyScreen.PickResult> OfferPickAsync(Player owner, [Nullable(new byte[]
		{
			2,
			1
		})] Func<RpgItemData, bool> filter = null, RpgIntensifyScreen.PickChrome? chrome = null, Control preferHost = null)
		{
			RpgIntensifyScreen.<OfferPickAsync>d__34 <OfferPickAsync>d__;
			<OfferPickAsync>d__.<>t__builder = AsyncTaskMethodBuilder<RpgIntensifyScreen.PickResult>.Create();
			<OfferPickAsync>d__.owner = owner;
			<OfferPickAsync>d__.filter = filter;
			<OfferPickAsync>d__.chrome = chrome;
			<OfferPickAsync>d__.preferHost = preferHost;
			<OfferPickAsync>d__.<>1__state = -1;
			<OfferPickAsync>d__.<>t__builder.Start<RpgIntensifyScreen.<OfferPickAsync>d__34>(ref <OfferPickAsync>d__);
			return <OfferPickAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00005130 File Offset: 0x00003330
		private static Task AttachAsync(RpgIntensifyScreen screen)
		{
			RpgIntensifyScreen.<AttachAsync>d__35 <AttachAsync>d__;
			<AttachAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AttachAsync>d__.screen = screen;
			<AttachAsync>d__.<>1__state = -1;
			<AttachAsync>d__.<>t__builder.Start<RpgIntensifyScreen.<AttachAsync>d__35>(ref <AttachAsync>d__);
			return <AttachAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00005174 File Offset: 0x00003374
		private static Task WaitOneFrame()
		{
			RpgIntensifyScreen.<WaitOneFrame>d__36 <WaitOneFrame>d__;
			<WaitOneFrame>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WaitOneFrame>d__.<>1__state = -1;
			<WaitOneFrame>d__.<>t__builder.Start<RpgIntensifyScreen.<WaitOneFrame>d__36>(ref <WaitOneFrame>d__);
			return <WaitOneFrame>d__.<>t__builder.Task;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000051AF File Offset: 0x000033AF
		public void AfterOverlayOpened()
		{
			base.MoveToFront();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000051B7 File Offset: 0x000033B7
		public void AfterOverlayClosed()
		{
			if (this._closed)
			{
				return;
			}
			this._closed = true;
			this._done.TrySetResult(new RpgIntensifyScreen.PickResult(false, ""));
			base.QueueFree();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000051E6 File Offset: 0x000033E6
		public void AfterOverlayShown()
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000051E8 File Offset: 0x000033E8
		public void AfterOverlayHidden()
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000051EA File Offset: 0x000033EA
		public override void _ExitTree()
		{
			this._done.TrySetResult(new RpgIntensifyScreen.PickResult(false, ""));
			base._ExitTree();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000520C File Offset: 0x0000340C
		private void Dismiss()
		{
			bool closed = this._closed;
			this._closed = true;
			if (!closed)
			{
				this._done.TrySetResult(new RpgIntensifyScreen.PickResult(false, ""));
			}
			if (this._pushedToOverlay && NOverlayStack.Instance != null)
			{
				try
				{
					NOverlayStack.Instance.Remove(this);
				}
				catch
				{
				}
				if (GodotObject.IsInstanceValid(this) && base.IsInsideTree())
				{
					base.QueueFree();
				}
				return;
			}
			if (GodotObject.IsInstanceValid(this) && base.IsInsideTree())
			{
				Node parent = base.GetParent();
				if (parent != null)
				{
					parent.RemoveChild(this);
				}
				base.QueueFree();
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000052B0 File Offset: 0x000034B0
		private void Build()
		{
			ColorRect colorRect = new ColorRect
			{
				Color = new Color(0.05f, 0.05f, 0.07f, 1f),
				MouseFilter = 0L
			};
			colorRect.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			base.AddChild(colorRect, false, 0L);
			Panel panel = new Panel
			{
				CustomMinimumSize = new Vector2(1120f, 780f),
				MouseFilter = 0L
			};
			panel.SetAnchorsPreset(8L, false);
			panel.GrowHorizontal = 2L;
			panel.GrowVertical = 2L;
			panel.OffsetLeft = -560f;
			panel.OffsetRight = 560f;
			panel.OffsetTop = -390f;
			panel.OffsetBottom = 390f;
			panel.ClipContents = true;
			panel.AddThemeStyleboxOverride("panel", RpgIntensifyScreen.Flat(new Color(0.11f, 0.11f, 0.13f, 0.98f), new Color(1f, 1f, 1f, 0.12f), 1));
			base.AddChild(panel, false, 0L);
			VBoxContainer vboxContainer = new VBoxContainer
			{
				MouseFilter = 0L
			};
			vboxContainer.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			vboxContainer.OffsetLeft = 28f;
			vboxContainer.OffsetRight = -28f;
			vboxContainer.OffsetTop = 20f;
			vboxContainer.OffsetBottom = -20f;
			vboxContainer.AddThemeConstantOverride("separation", 12);
			panel.AddChild(vboxContainer, false, 0L);
			Label label = new Label
			{
				Text = this._chrome.Title,
				HorizontalAlignment = 1L
			};
			label.AddThemeFontSizeOverride("font_size", 28);
			vboxContainer.AddChild(label, false, 0L);
			Label label2 = new Label
			{
				Text = this._chrome.Hint,
				HorizontalAlignment = 1L
			};
			label2.AddThemeFontSizeOverride("font_size", 14);
			label2.Modulate = new Color(1f, 1f, 1f, 0.65f);
			vboxContainer.AddChild(label2, false, 0L);
			HBoxContainer hboxContainer = new HBoxContainer
			{
				CustomMinimumSize = new Vector2(0f, 260f),
				SizeFlagsVertical = 0L,
				Alignment = 1L
			};
			hboxContainer.AddThemeConstantOverride("separation", 16);
			vboxContainer.AddChild(hboxContainer, false, 0L);
			Panel panel2;
			this._leftBody = RpgIntensifyScreen.MakeCardShell(out panel2);
			hboxContainer.AddChild(panel2, false, 0L);
			Label label3 = new Label
			{
				Text = ">",
				VerticalAlignment = 1L,
				HorizontalAlignment = 1L,
				CustomMinimumSize = new Vector2(36f, 260f)
			};
			label3.AddThemeFontSizeOverride("font_size", 36);
			hboxContainer.AddChild(label3, false, 0L);
			Panel panel3;
			this._rightBody = RpgIntensifyScreen.MakeCardShell(out panel3);
			hboxContainer.AddChild(panel3, false, 0L);
			this._filterHost = new HBoxContainer
			{
				SizeFlagsHorizontal = 3L,
				MouseFilter = 0L
			};
			vboxContainer.AddChild(this._filterHost, false, 0L);
			this.RebuildFilterTabs();
			ScrollContainer scrollContainer = new ScrollContainer
			{
				SizeFlagsVertical = 3L,
				CustomMinimumSize = new Vector2(0f, 280f)
			};
			vboxContainer.AddChild(scrollContainer, false, 0L);
			this._grid = new HFlowContainer
			{
				SizeFlagsHorizontal = 3L
			};
			this._grid.AddThemeConstantOverride("h_separation", 10);
			this._grid.AddThemeConstantOverride("v_separation", 10);
			scrollContainer.AddChild(this._grid, false, 0L);
			this.RebuildGrid();
			HBoxContainer hboxContainer2 = new HBoxContainer
			{
				Alignment = 1L
			};
			hboxContainer2.AddThemeConstantOverride("separation", 16);
			vboxContainer.AddChild(hboxContainer2, false, 0L);
			Button button = new Button
			{
				Text = SpireLoc.Item("ui.intensify.cancel", "Cancel"),
				CustomMinimumSize = new Vector2(140f, 44f)
			};
			button.Pressed += delegate()
			{
				this.Complete(false);
			};
			hboxContainer2.AddChild(button, false, 0L);
			this._confirm = new Button
			{
				Text = this._chrome.Confirm,
				CustomMinimumSize = new Vector2(160f, 44f),
				Disabled = true
			};
			this._confirm.Pressed += this.Confirm;
			hboxContainer2.AddChild(this._confirm, false, 0L);
			this.RefreshPreview();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005726 File Offset: 0x00003926
		private void SetBrowseChanged()
		{
			this.RebuildFilterTabs();
			this.RebuildGrid();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005734 File Offset: 0x00003934
		private void RebuildFilterTabs()
		{
			if (this._filterHost == null)
			{
				return;
			}
			foreach (Node node in this._filterHost.GetChildren(false))
			{
				node.QueueFree();
			}
			this._filterHost.AddChild(RpgItemBrowseBar.Create(this._browse, new Action(this.SetBrowseChanged)), false, 0L);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000057B4 File Offset: 0x000039B4
		private void RebuildGrid()
		{
			if (this._grid == null)
			{
				return;
			}
			foreach (Node node in this._grid.GetChildren(false))
			{
				node.QueueFree();
			}
			this._slotById.Clear();
			foreach (RpgItemData item in RpgItemBrowseBar.Apply(this._items, this._browse))
			{
				this._grid.AddChild(this.MakePickSlot(item), false, 0L);
			}
			if (this._selected != null && !this._slotById.ContainsKey(this._selected.InstanceId))
			{
				this.ClearSelection();
				return;
			}
			Panel panel;
			if (this._selected != null && this._slotById.TryGetValue(this._selected.InstanceId, out panel))
			{
				RpgIntensifyScreen.StyleSlot(panel, this._selected, true);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000058C4 File Offset: 0x00003AC4
		private static VBoxContainer MakeCardShell(out Panel shell)
		{
			shell = new Panel
			{
				CustomMinimumSize = new Vector2(420f, 260f),
				SizeFlagsHorizontal = 3L,
				ClipContents = true,
				MouseFilter = 2L
			};
			shell.AddThemeStyleboxOverride("panel", RpgIntensifyScreen.Flat(new Color(0.16f, 0.16f, 0.18f, 1f), new Color(1f, 1f, 1f, 0.1f), 1));
			ScrollContainer scrollContainer = new ScrollContainer
			{
				MouseFilter = 0L
			};
			scrollContainer.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			scrollContainer.OffsetLeft = 12f;
			scrollContainer.OffsetTop = 10f;
			scrollContainer.OffsetRight = -12f;
			scrollContainer.OffsetBottom = -10f;
			shell.AddChild(scrollContainer, false, 0L);
			VBoxContainer vboxContainer = new VBoxContainer
			{
				SizeFlagsHorizontal = 3L,
				MouseFilter = 2L
			};
			vboxContainer.AddThemeConstantOverride("separation", 6);
			scrollContainer.AddChild(vboxContainer, false, 0L);
			return vboxContainer;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000059D0 File Offset: 0x00003BD0
		private void Confirm()
		{
			if (this._locked || this._closed || this._selected == null)
			{
				return;
			}
			this._locked = true;
			this._pickedId = this._selected.InstanceId;
			this.Complete(true);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005A0A File Offset: 0x00003C0A
		private void Complete(bool ok)
		{
			if (this._closed)
			{
				return;
			}
			this._locked = true;
			this._closed = true;
			this._done.TrySetResult(new RpgIntensifyScreen.PickResult(ok, ok ? this._pickedId : ""));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005A48 File Offset: 0x00003C48
		private void Select(RpgItemData item)
		{
			if (this._selected != null && this._selected.InstanceId == item.InstanceId)
			{
				this.ClearSelection();
				return;
			}
			this.ClearSelectionHighlight();
			this._selected = item;
			Panel panel;
			if (this._slotById.TryGetValue(item.InstanceId, out panel))
			{
				RpgIntensifyScreen.StyleSlot(panel, item, true);
			}
			if (this._confirm != null)
			{
				this._confirm.Disabled = false;
			}
			this.RefreshPreview();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005AC0 File Offset: 0x00003CC0
		private void ClearSelection()
		{
			this.ClearSelectionHighlight();
			this._selected = null;
			if (this._confirm != null)
			{
				this._confirm.Disabled = true;
			}
			this.RefreshPreview();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005AEC File Offset: 0x00003CEC
		private void ClearSelectionHighlight()
		{
			if (this._selected == null)
			{
				return;
			}
			Panel panel;
			if (this._slotById.TryGetValue(this._selected.InstanceId, out panel))
			{
				RpgIntensifyScreen.StyleSlot(panel, this._selected, false);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00005B2C File Offset: 0x00003D2C
		private void RefreshPreview()
		{
			RpgIntensifyScreen.FillCard(this._leftBody, this._selected);
			if (this._selected == null)
			{
				RpgIntensifyScreen.FillCard(this._rightBody, null);
				return;
			}
			if (this._chrome.Preview == null)
			{
				RpgIntensifyScreen.FillRevealPlaceholder(this._rightBody);
				return;
			}
			RpgIntensifyScreen.FillCard(this._rightBody, this._chrome.Preview(this._selected));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005B9C File Offset: 0x00003D9C
		[NullableContext(2)]
		private static void FillRevealPlaceholder(VBoxContainer body)
		{
			if (body == null)
			{
				return;
			}
			foreach (Node node in body.GetChildren(false))
			{
				node.QueueFree();
			}
			Label label = new Label
			{
				Text = SpireLoc.Item("ui.identify.reveal_hint", "Confirm to reveal true stats."),
				HorizontalAlignment = 1L,
				AutowrapMode = 3L
			};
			label.AddThemeFontSizeOverride("font_size", 15);
			label.Modulate = new Color(1f, 1f, 1f, 0.55f);
			body.AddChild(label, false, 0L);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005C54 File Offset: 0x00003E54
		[NullableContext(2)]
		private static void FillCard(VBoxContainer body, RpgItemData item)
		{
			if (body == null)
			{
				return;
			}
			foreach (Node node in body.GetChildren(false))
			{
				node.QueueFree();
			}
			if (item == null)
			{
				Label label = new Label
				{
					Text = "—",
					HorizontalAlignment = 1L
				};
				label.AddThemeFontSizeOverride("font_size", 18);
				label.Modulate = new Color(1f, 1f, 1f, 0.35f);
				body.AddChild(label, false, 0L);
				return;
			}
			HBoxContainer hboxContainer = new HBoxContainer
			{
				MouseFilter = 2L
			};
			hboxContainer.AddThemeConstantOverride("separation", 10);
			body.AddChild(hboxContainer, false, 0L);
			Panel panel = new Panel
			{
				CustomMinimumSize = new Vector2(64f, 64f),
				MouseFilter = 2L
			};
			panel.AddThemeStyleboxOverride("panel", RpgIntensifyScreen.Flat(new Color(0.12f, 0.12f, 0.14f, 1f), RpgItemTooltip.RarityColor(item.Rarity), 2));
			hboxContainer.AddChild(panel, false, 0L);
			Texture2D texture2D = SpireAssets.ItemIcon(item.IconPath);
			if (texture2D != null)
			{
				TextureRect textureRect = new TextureRect
				{
					Texture = texture2D,
					ExpandMode = 1L,
					StretchMode = 5L,
					MouseFilter = 2L
				};
				textureRect.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
				textureRect.OffsetLeft = 4f;
				textureRect.OffsetTop = 4f;
				textureRect.OffsetRight = -4f;
				textureRect.OffsetBottom = -4f;
				panel.AddChild(textureRect, false, 0L);
			}
			VBoxContainer vboxContainer = new VBoxContainer
			{
				SizeFlagsHorizontal = 3L,
				MouseFilter = 2L
			};
			vboxContainer.AddThemeConstantOverride("separation", 2);
			hboxContainer.AddChild(vboxContainer, false, 0L);
			string text = RpgIntensifyScreen.FormatItemName(item);
			Label label2 = new Label
			{
				Text = text,
				AutowrapMode = 3L
			};
			label2.AddThemeFontSizeOverride("font_size", 17);
			label2.AddThemeColorOverride("font_color", RpgItemTooltip.RarityColor(item.Rarity));
			vboxContainer.AddChild(label2, false, 0L);
			Label label3 = new Label
			{
				Text = item.LocalizedTypeLine
			};
			label3.AddThemeFontSizeOverride("font_size", 13);
			label3.Modulate = new Color(1f, 1f, 1f, 0.7f);
			vboxContainer.AddChild(label3, false, 0L);
			body.AddChild(RpgIntensifyScreen.MakeStatRow(item.LocalizedItemPower, null), false, 0L);
			if (!item.IsIdentified && item.IsRarePlus)
			{
				string localizedPrimaryStat = item.LocalizedPrimaryStat;
				if (!string.IsNullOrEmpty(localizedPrimaryStat))
				{
					body.AddChild(RpgIntensifyScreen.MakeStatRow(localizedPrimaryStat, null), false, 0L);
				}
				foreach (string text2 in item.ObscuredAffixLines().Take(8))
				{
					body.AddChild(RpgIntensifyScreen.MakeStatRow(text2, null), false, 0L);
				}
				if (item.IsBound && !item.HasCurse)
				{
					body.AddChild(RpgIntensifyScreen.MakeStatRow(SpireLoc.Item("ui.bound", "Bound"), null), false, 0L);
				}
				return;
			}
			string localizedPrimaryStat2 = item.LocalizedPrimaryStat;
			if (!string.IsNullOrEmpty(localizedPrimaryStat2))
			{
				RpgStatMod rpgStatMod = RpgStatMod.Parse(item.Primary);
				body.AddChild(RpgIntensifyScreen.MakeStatRow(localizedPrimaryStat2, (rpgStatMod == null) ? null : RpgIntensifyScreen.StatIconPath(rpgStatMod.Id)), false, 0L);
			}
			foreach (string text3 in item.EffectiveAbilityLines().Take(8))
			{
				RpgStatMod rpgStatMod2 = RpgStatMod.Parse(text3);
				if (rpgStatMod2 != null)
				{
					body.AddChild(RpgIntensifyScreen.MakeStatRow(rpgStatMod2.FormatAffix(), RpgIntensifyScreen.StatIconPath(rpgStatMod2.Id)), false, 0L);
				}
			}
			foreach (string text4 in item.Curses)
			{
				body.AddChild(RpgIntensifyScreen.MakeStatRow(SpireLoc.ItemFormat("ui.curse_line", "Curse: {0}", new object[]
				{
					SpireLoc.Item("curse." + text4, text4)
				}), null), false, 0L);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000060E0 File Offset: 0x000042E0
		private static string FormatItemName(RpgItemData item)
		{
			return item.DisplayName;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000060E8 File Offset: 0x000042E8
		private static Control MakeStatRow(string text, [Nullable(2)] string iconPath)
		{
			HBoxContainer hboxContainer = new HBoxContainer
			{
				MouseFilter = 2L
			};
			hboxContainer.AddThemeConstantOverride("separation", 8);
			if (!string.IsNullOrWhiteSpace(iconPath))
			{
				Texture2D texture2D = ResourceLoader.Load<Texture2D>(iconPath, null, 1L);
				if (texture2D != null)
				{
					hboxContainer.AddChild(new TextureRect
					{
						Texture = texture2D,
						CustomMinimumSize = new Vector2(18f, 18f),
						ExpandMode = 1L,
						StretchMode = 5L,
						MouseFilter = 2L
					}, false, 0L);
				}
			}
			Label label = new Label
			{
				Text = text,
				SizeFlagsHorizontal = 3L,
				AutowrapMode = 3L
			};
			label.AddThemeFontSizeOverride("font_size", 14);
			hboxContainer.AddChild(label, false, 0L);
			return hboxContainer;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000061A8 File Offset: 0x000043A8
		[return: Nullable(2)]
		private static string StatIconPath(string id)
		{
			if (id != null)
			{
				int length = id.Length;
				if (length <= 16)
				{
					switch (length)
					{
					case 5:
						if (!(id == "armor"))
						{
							goto IL_237;
						}
						goto IL_22F;
					case 6:
					case 7:
					case 10:
					case 11:
						goto IL_237;
					case 8:
					{
						char c = id[0];
						if (c != 's')
						{
							if (c != 'v')
							{
								goto IL_237;
							}
							if (!(id == "vitality"))
							{
								goto IL_237;
							}
							goto IL_227;
						}
						else
						{
							if (!(id == "strength"))
							{
								goto IL_237;
							}
							goto IL_20F;
						}
						break;
					}
					case 9:
					{
						char c = id[0];
						if (c != 'a')
						{
							if (c != 'd')
							{
								goto IL_237;
							}
							if (!(id == "dexterity"))
							{
								goto IL_237;
							}
							goto IL_217;
						}
						else
						{
							if (!(id == "all_stats"))
							{
								goto IL_237;
							}
							goto IL_227;
						}
						break;
					}
					case 12:
					{
						char c = id[0];
						if (c != 'b')
						{
							if (c != 'i')
							{
								goto IL_237;
							}
							if (!(id == "intelligence"))
							{
								goto IL_237;
							}
						}
						else
						{
							if (!(id == "block_chance"))
							{
								goto IL_237;
							}
							goto IL_217;
						}
						break;
					}
					case 13:
					{
						char c = id[0];
						if (c != 'n')
						{
							if (c != 's')
							{
								goto IL_237;
							}
							if (!(id == "shadow_damage"))
							{
								goto IL_237;
							}
						}
						else if (!(id == "nature_damage"))
						{
							goto IL_237;
						}
						break;
					}
					default:
					{
						if (length != 16)
						{
							goto IL_237;
						}
						char c = id[0];
						if (c != 'd')
						{
							if (c != 'h')
							{
								if (c != 'o')
								{
									goto IL_237;
								}
								if (!(id == "overpower_damage"))
								{
									goto IL_237;
								}
								goto IL_20F;
							}
							else if (!(id == "healing_received"))
							{
								goto IL_237;
							}
						}
						else
						{
							if (!(id == "damage_reduction"))
							{
								goto IL_237;
							}
							goto IL_22F;
						}
						break;
					}
					}
					return "res://images/atlases/stats_screen_atlas.sprites/stats_potions_seen.tres";
					IL_227:
					return "res://images/atlases/stats_screen_atlas.sprites/stats_chest.tres";
					IL_22F:
					return "res://images/atlases/stats_screen_atlas.sprites/stats_chest.tres";
				}
				if (length != 18)
				{
					if (length != 22)
					{
						if (length != 23)
						{
							goto IL_237;
						}
						if (!(id == "damage_while_berserking"))
						{
							goto IL_237;
						}
					}
					else if (!(id == "critical_strike_damage"))
					{
						goto IL_237;
					}
				}
				else
				{
					if (!(id == "cooldown_reduction"))
					{
						goto IL_237;
					}
					goto IL_217;
				}
				IL_20F:
				return "res://images/atlases/stats_screen_atlas.sprites/stats_swords.tres";
				IL_217:
				return "res://images/atlases/stats_screen_atlas.sprites/stats_clock.tres";
			}
			IL_237:
			return "res://images/atlases/stats_screen_atlas.sprites/stats_chest.tres";
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000063F4 File Offset: 0x000045F4
		private Panel MakePickSlot(RpgItemData item)
		{
			Panel panel = new Panel
			{
				CustomMinimumSize = new Vector2(72f, 72f),
				MouseFilter = 0L
			};
			RpgIntensifyScreen.StyleSlot(panel, item, false);
			this._slotById[item.InstanceId] = panel;
			Texture2D texture2D = SpireAssets.ItemIcon(item.IconPath);
			if (texture2D != null)
			{
				TextureRect textureRect = new TextureRect
				{
					Texture = texture2D,
					ExpandMode = 1L,
					StretchMode = 5L,
					MouseFilter = 2L
				};
				textureRect.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
				textureRect.OffsetLeft = 4f;
				textureRect.OffsetTop = 4f;
				textureRect.OffsetRight = -4f;
				textureRect.OffsetBottom = -4f;
				panel.AddChild(textureRect, false, 0L);
			}
			panel.GuiInput += delegate(InputEvent e)
			{
				InputEventMouseButton inputEventMouseButton = e as InputEventMouseButton;
				if (inputEventMouseButton != null && inputEventMouseButton.Pressed && inputEventMouseButton.ButtonIndex == 1L)
				{
					panel.AcceptEvent();
					this.Select(item);
				}
			};
			return panel;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00006508 File Offset: 0x00004708
		private static void StyleSlot(Panel panel, RpgItemData item, bool selected)
		{
			Color border = RpgItemTooltip.RarityColor(item.Rarity);
			if (selected)
			{
				panel.AddThemeStyleboxOverride("panel", RpgIntensifyScreen.Flat(new Color(0.28f, 0.22f, 0.12f, 1f), new Color(1f, 0.82f, 0.35f, 1f), 3));
				return;
			}
			panel.AddThemeStyleboxOverride("panel", RpgIntensifyScreen.Flat(new Color(0.16f, 0.14f, 0.16f, 1f), border, 2));
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000065A0 File Offset: 0x000047A0
		private static StyleBoxFlat Flat(Color bg, Color border, int borderWidth = 1)
		{
			return new StyleBoxFlat
			{
				BgColor = bg,
				BorderColor = border,
				BorderWidthLeft = borderWidth,
				BorderWidthTop = borderWidth,
				BorderWidthRight = borderWidth,
				BorderWidthBottom = borderWidth,
				CornerRadiusTopLeft = 10,
				CornerRadiusTopRight = 10,
				CornerRadiusBottomRight = 10,
				CornerRadiusBottomLeft = 10,
				ContentMarginLeft = 10f,
				ContentMarginRight = 10f,
				ContentMarginTop = 10f,
				ContentMarginBottom = 10f
			};
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00006628 File Offset: 0x00004828
		[CompilerGenerated]
		private void <Build>b__43_0()
		{
			this.Complete(false);
		}

		// Token: 0x04000025 RID: 37
		private readonly TaskCompletionSource<RpgIntensifyScreen.PickResult> _done = new TaskCompletionSource<RpgIntensifyScreen.PickResult>();

		// Token: 0x04000026 RID: 38
		private readonly List<RpgItemData> _items;

		// Token: 0x04000027 RID: 39
		private readonly RpgIntensifyScreen.PickChrome _chrome;

		// Token: 0x04000028 RID: 40
		private readonly Dictionary<string, Panel> _slotById = new Dictionary<string, Panel>(StringComparer.Ordinal);

		// Token: 0x04000029 RID: 41
		[Nullable(2)]
		private RpgItemData _selected;

		// Token: 0x0400002A RID: 42
		private readonly RpgItemBrowseState _browse = new RpgItemBrowseState();

		// Token: 0x0400002B RID: 43
		[Nullable(2)]
		private HBoxContainer _filterHost;

		// Token: 0x0400002C RID: 44
		[Nullable(2)]
		private HFlowContainer _grid;

		// Token: 0x0400002D RID: 45
		[Nullable(2)]
		private VBoxContainer _leftBody;

		// Token: 0x0400002E RID: 46
		[Nullable(2)]
		private VBoxContainer _rightBody;

		// Token: 0x0400002F RID: 47
		[Nullable(2)]
		private Button _confirm;

		// Token: 0x04000030 RID: 48
		private bool _pushedToOverlay;

		// Token: 0x04000031 RID: 49
		private bool _closed;

		// Token: 0x04000032 RID: 50
		private bool _locked;

		// Token: 0x04000033 RID: 51
		private string _pickedId = "";

		// Token: 0x0200008F RID: 143
		[Nullable(0)]
		internal readonly struct PickResult : IEquatable<RpgIntensifyScreen.PickResult>
		{
			// Token: 0x060005DA RID: 1498 RVA: 0x0001E992 File Offset: 0x0001CB92
			public PickResult(bool Ok, string InstanceId)
			{
				this.Ok = Ok;
				this.InstanceId = InstanceId;
			}

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x060005DB RID: 1499 RVA: 0x0001E9A2 File Offset: 0x0001CBA2
			// (set) Token: 0x060005DC RID: 1500 RVA: 0x0001E9AA File Offset: 0x0001CBAA
			public bool Ok
			{
				[CompilerGenerated]
				get
				{
					return this.<Ok>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Ok>k__BackingField = value;
				}
			}

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x060005DD RID: 1501 RVA: 0x0001E9B3 File Offset: 0x0001CBB3
			// (set) Token: 0x060005DE RID: 1502 RVA: 0x0001E9BB File Offset: 0x0001CBBB
			public string InstanceId
			{
				[CompilerGenerated]
				get
				{
					return this.<InstanceId>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<InstanceId>k__BackingField = value;
				}
			}

			// Token: 0x060005DF RID: 1503 RVA: 0x0001E9C4 File Offset: 0x0001CBC4
			[NullableContext(0)]
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("PickResult");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x060005E0 RID: 1504 RVA: 0x0001EA10 File Offset: 0x0001CC10
			[NullableContext(0)]
			[CompilerGenerated]
			private bool PrintMembers(StringBuilder builder)
			{
				builder.Append("Ok = ");
				builder.Append(this.Ok.ToString());
				builder.Append(", InstanceId = ");
				builder.Append(this.InstanceId);
				return true;
			}

			// Token: 0x060005E1 RID: 1505 RVA: 0x0001EA5E File Offset: 0x0001CC5E
			[CompilerGenerated]
			public static bool operator !=(RpgIntensifyScreen.PickResult left, RpgIntensifyScreen.PickResult right)
			{
				return !(left == right);
			}

			// Token: 0x060005E2 RID: 1506 RVA: 0x0001EA6A File Offset: 0x0001CC6A
			[CompilerGenerated]
			public static bool operator ==(RpgIntensifyScreen.PickResult left, RpgIntensifyScreen.PickResult right)
			{
				return left.Equals(right);
			}

			// Token: 0x060005E3 RID: 1507 RVA: 0x0001EA74 File Offset: 0x0001CC74
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return EqualityComparer<bool>.Default.GetHashCode(this.<Ok>k__BackingField) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<InstanceId>k__BackingField);
			}

			// Token: 0x060005E4 RID: 1508 RVA: 0x0001EA9D File Offset: 0x0001CC9D
			[NullableContext(0)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return obj is RpgIntensifyScreen.PickResult && this.Equals((RpgIntensifyScreen.PickResult)obj);
			}

			// Token: 0x060005E5 RID: 1509 RVA: 0x0001EAB5 File Offset: 0x0001CCB5
			[CompilerGenerated]
			public bool Equals(RpgIntensifyScreen.PickResult other)
			{
				return EqualityComparer<bool>.Default.Equals(this.<Ok>k__BackingField, other.<Ok>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<InstanceId>k__BackingField, other.<InstanceId>k__BackingField);
			}

			// Token: 0x060005E6 RID: 1510 RVA: 0x0001EAE7 File Offset: 0x0001CCE7
			[CompilerGenerated]
			public void Deconstruct(out bool Ok, out string InstanceId)
			{
				Ok = this.Ok;
				InstanceId = this.InstanceId;
			}

			// Token: 0x040001F1 RID: 497
			[CompilerGenerated]
			private readonly bool <Ok>k__BackingField;

			// Token: 0x040001F2 RID: 498
			[CompilerGenerated]
			private readonly string <InstanceId>k__BackingField;
		}

		// Token: 0x02000090 RID: 144
		[Nullable(0)]
		internal readonly struct PickChrome : IEquatable<RpgIntensifyScreen.PickChrome>
		{
			// Token: 0x060005E7 RID: 1511 RVA: 0x0001EAF9 File Offset: 0x0001CCF9
			public PickChrome(string Title, string Hint, string Confirm, [Nullable(new byte[]
			{
				2,
				1,
				1
			})] Func<RpgItemData, RpgItemData> Preview)
			{
				this.Title = Title;
				this.Hint = Hint;
				this.Confirm = Confirm;
				this.Preview = Preview;
			}

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0001EB18 File Offset: 0x0001CD18
			// (set) Token: 0x060005E9 RID: 1513 RVA: 0x0001EB20 File Offset: 0x0001CD20
			public string Title
			{
				[CompilerGenerated]
				get
				{
					return this.<Title>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Title>k__BackingField = value;
				}
			}

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x060005EA RID: 1514 RVA: 0x0001EB29 File Offset: 0x0001CD29
			// (set) Token: 0x060005EB RID: 1515 RVA: 0x0001EB31 File Offset: 0x0001CD31
			public string Hint
			{
				[CompilerGenerated]
				get
				{
					return this.<Hint>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Hint>k__BackingField = value;
				}
			}

			// Token: 0x17000181 RID: 385
			// (get) Token: 0x060005EC RID: 1516 RVA: 0x0001EB3A File Offset: 0x0001CD3A
			// (set) Token: 0x060005ED RID: 1517 RVA: 0x0001EB42 File Offset: 0x0001CD42
			public string Confirm
			{
				[CompilerGenerated]
				get
				{
					return this.<Confirm>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Confirm>k__BackingField = value;
				}
			}

			// Token: 0x17000182 RID: 386
			// (get) Token: 0x060005EE RID: 1518 RVA: 0x0001EB4B File Offset: 0x0001CD4B
			// (set) Token: 0x060005EF RID: 1519 RVA: 0x0001EB53 File Offset: 0x0001CD53
			[Nullable(new byte[]
			{
				2,
				1,
				1
			})]
			public Func<RpgItemData, RpgItemData> Preview
			{
				[CompilerGenerated]
				[return: Nullable(new byte[]
				{
					2,
					1,
					1
				})]
				get
				{
					return this.<Preview>k__BackingField;
				}
				[CompilerGenerated]
				[param: Nullable(new byte[]
				{
					2,
					1,
					1
				})]
				set
				{
					this.<Preview>k__BackingField = value;
				}
			}

			// Token: 0x060005F0 RID: 1520 RVA: 0x0001EB5C File Offset: 0x0001CD5C
			[NullableContext(0)]
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("PickChrome");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x060005F1 RID: 1521 RVA: 0x0001EBA8 File Offset: 0x0001CDA8
			[NullableContext(0)]
			[CompilerGenerated]
			private bool PrintMembers(StringBuilder builder)
			{
				builder.Append("Title = ");
				builder.Append(this.Title);
				builder.Append(", Hint = ");
				builder.Append(this.Hint);
				builder.Append(", Confirm = ");
				builder.Append(this.Confirm);
				builder.Append(", Preview = ");
				builder.Append(this.Preview);
				return true;
			}

			// Token: 0x060005F2 RID: 1522 RVA: 0x0001EC1A File Offset: 0x0001CE1A
			[CompilerGenerated]
			public static bool operator !=(RpgIntensifyScreen.PickChrome left, RpgIntensifyScreen.PickChrome right)
			{
				return !(left == right);
			}

			// Token: 0x060005F3 RID: 1523 RVA: 0x0001EC26 File Offset: 0x0001CE26
			[CompilerGenerated]
			public static bool operator ==(RpgIntensifyScreen.PickChrome left, RpgIntensifyScreen.PickChrome right)
			{
				return left.Equals(right);
			}

			// Token: 0x060005F4 RID: 1524 RVA: 0x0001EC30 File Offset: 0x0001CE30
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return ((EqualityComparer<string>.Default.GetHashCode(this.<Title>k__BackingField) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Hint>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Confirm>k__BackingField)) * -1521134295 + EqualityComparer<Func<RpgItemData, RpgItemData>>.Default.GetHashCode(this.<Preview>k__BackingField);
			}

			// Token: 0x060005F5 RID: 1525 RVA: 0x0001EC92 File Offset: 0x0001CE92
			[NullableContext(0)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return obj is RpgIntensifyScreen.PickChrome && this.Equals((RpgIntensifyScreen.PickChrome)obj);
			}

			// Token: 0x060005F6 RID: 1526 RVA: 0x0001ECAC File Offset: 0x0001CEAC
			[CompilerGenerated]
			public bool Equals(RpgIntensifyScreen.PickChrome other)
			{
				return EqualityComparer<string>.Default.Equals(this.<Title>k__BackingField, other.<Title>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Hint>k__BackingField, other.<Hint>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Confirm>k__BackingField, other.<Confirm>k__BackingField) && EqualityComparer<Func<RpgItemData, RpgItemData>>.Default.Equals(this.<Preview>k__BackingField, other.<Preview>k__BackingField);
			}

			// Token: 0x060005F7 RID: 1527 RVA: 0x0001ED19 File Offset: 0x0001CF19
			[CompilerGenerated]
			public void Deconstruct(out string Title, out string Hint, out string Confirm, [Nullable(new byte[]
			{
				2,
				1,
				1
			})] out Func<RpgItemData, RpgItemData> Preview)
			{
				Title = this.Title;
				Hint = this.Hint;
				Confirm = this.Confirm;
				Preview = this.Preview;
			}

			// Token: 0x040001F3 RID: 499
			[CompilerGenerated]
			private readonly string <Title>k__BackingField;

			// Token: 0x040001F4 RID: 500
			[CompilerGenerated]
			private readonly string <Hint>k__BackingField;

			// Token: 0x040001F5 RID: 501
			[CompilerGenerated]
			private readonly string <Confirm>k__BackingField;

			// Token: 0x040001F6 RID: 502
			[Nullable(new byte[]
			{
				2,
				1,
				1
			})]
			[CompilerGenerated]
			private readonly Func<RpgItemData, RpgItemData> <Preview>k__BackingField;
		}

		// Token: 0x02000091 RID: 145
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040001F7 RID: 503
			[Nullable(new byte[]
			{
				0,
				1,
				1
			})]
			public static Func<RpgItemData, RpgItemData> <0>__Preview;
		}

		// Token: 0x02000092 RID: 146
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005F8 RID: 1528 RVA: 0x0001ED3C File Offset: 0x0001CF3C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005F9 RID: 1529 RVA: 0x0001ED48 File Offset: 0x0001CF48
			public <>c()
			{
			}

			// Token: 0x060005FA RID: 1530 RVA: 0x0001ED50 File Offset: 0x0001CF50
			internal RpgItemData <get_CleanseChrome>b__30_0(RpgItemData item)
			{
				RpgItemData rpgItemData = item.Clone();
				rpgItemData.ClearCurses();
				return rpgItemData;
			}

			// Token: 0x060005FB RID: 1531 RVA: 0x0001ED5E File Offset: 0x0001CF5E
			[NullableContext(0)]
			internal RpgItemData <OfferPickAsync>b__34_1(RpgItemData x)
			{
				return x.Clone();
			}

			// Token: 0x040001F8 RID: 504
			[Nullable(0)]
			public static readonly RpgIntensifyScreen.<>c <>9 = new RpgIntensifyScreen.<>c();

			// Token: 0x040001F9 RID: 505
			[Nullable(new byte[]
			{
				0,
				1,
				1
			})]
			public static Func<RpgItemData, RpgItemData> <>9__30_0;

			// Token: 0x040001FA RID: 506
			[Nullable(0)]
			public static Func<RpgItemData, RpgItemData> <>9__34_1;
		}

		// Token: 0x02000093 RID: 147
		[CompilerGenerated]
		private sealed class <>c__DisplayClass33_0
		{
			// Token: 0x060005FC RID: 1532 RVA: 0x0001ED66 File Offset: 0x0001CF66
			public <>c__DisplayClass33_0()
			{
			}

			// Token: 0x060005FD RID: 1533 RVA: 0x0001ED6E File Offset: 0x0001CF6E
			[NullableContext(0)]
			internal bool <OfferAsync>b__0(RpgItemData x)
			{
				return string.Equals(x.InstanceId, this.pick.InstanceId, StringComparison.Ordinal);
			}

			// Token: 0x040001FB RID: 507
			public RpgIntensifyScreen.PickResult pick;
		}

		// Token: 0x02000094 RID: 148
		[CompilerGenerated]
		private sealed class <>c__DisplayClass34_0
		{
			// Token: 0x060005FE RID: 1534 RVA: 0x0001ED87 File Offset: 0x0001CF87
			public <>c__DisplayClass34_0()
			{
			}

			// Token: 0x060005FF RID: 1535 RVA: 0x0001ED8F File Offset: 0x0001CF8F
			[NullableContext(0)]
			internal bool <OfferPickAsync>b__0(RpgItemData x)
			{
				return !x.IsScroll && (this.filter == null || this.filter(x));
			}

			// Token: 0x040001FC RID: 508
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public Func<RpgItemData, bool> filter;
		}

		// Token: 0x02000095 RID: 149
		[CompilerGenerated]
		private sealed class <>c__DisplayClass59_0
		{
			// Token: 0x06000600 RID: 1536 RVA: 0x0001EDB1 File Offset: 0x0001CFB1
			public <>c__DisplayClass59_0()
			{
			}

			// Token: 0x06000601 RID: 1537 RVA: 0x0001EDBC File Offset: 0x0001CFBC
			[NullableContext(0)]
			internal void <MakePickSlot>b__0(InputEvent e)
			{
				InputEventMouseButton inputEventMouseButton = e as InputEventMouseButton;
				if (inputEventMouseButton != null && inputEventMouseButton.Pressed && inputEventMouseButton.ButtonIndex == 1L)
				{
					this.panel.AcceptEvent();
					this.<>4__this.Select(this.item);
				}
			}

			// Token: 0x040001FD RID: 509
			[Nullable(0)]
			public Panel panel;

			// Token: 0x040001FE RID: 510
			[Nullable(0)]
			public RpgIntensifyScreen <>4__this;

			// Token: 0x040001FF RID: 511
			[Nullable(0)]
			public RpgItemData item;
		}

		// Token: 0x02000096 RID: 150
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <AttachAsync>d__35 : IAsyncStateMachine
		{
			// Token: 0x06000602 RID: 1538 RVA: 0x0001EE04 File Offset: 0x0001D004
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					if (num != 0)
					{
						this.<i>5__2 = 0;
						goto IL_7F;
					}
					TaskAwaiter awaiter = this.<>u__1;
					this.<>u__1 = default(TaskAwaiter);
					this.<>1__state = -1;
					IL_66:
					awaiter.GetResult();
					int num2 = this.<i>5__2;
					this.<i>5__2 = num2 + 1;
					IL_7F:
					if (this.<i>5__2 >= 120 || NOverlayStack.Instance != null)
					{
						NOverlayStack instance = NOverlayStack.Instance;
						if (instance != null)
						{
							this.screen._pushedToOverlay = true;
							instance.Push(this.screen);
							this.screen.MoveToFront();
						}
						else
						{
							NGame instance2 = NGame.Instance;
							if (instance2 != null && GodotObject.IsInstanceValid(instance2))
							{
								instance2.AddChild(this.screen, false, 0L);
								this.screen.MoveToFront();
							}
							else
							{
								this.screen._done.TrySetResult(new RpgIntensifyScreen.PickResult(false, ""));
							}
						}
					}
					else
					{
						awaiter = RpgIntensifyScreen.WaitOneFrame().GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgIntensifyScreen.<AttachAsync>d__35>(ref awaiter, ref this);
							return;
						}
						goto IL_66;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000603 RID: 1539 RVA: 0x0001EF64 File Offset: 0x0001D164
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000200 RID: 512
			public int <>1__state;

			// Token: 0x04000201 RID: 513
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000202 RID: 514
			[Nullable(0)]
			public RpgIntensifyScreen screen;

			// Token: 0x04000203 RID: 515
			private int <i>5__2;

			// Token: 0x04000204 RID: 516
			private TaskAwaiter <>u__1;
		}

		// Token: 0x02000097 RID: 151
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <OfferAsync>d__33 : IAsyncStateMachine
		{
			// Token: 0x06000604 RID: 1540 RVA: 0x0001EF74 File Offset: 0x0001D174
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result2;
				try
				{
					TaskAwaiter<RpgIntensifyScreen.PickResult> awaiter;
					if (num != 0)
					{
						this.<>8__1 = new RpgIntensifyScreen.<>c__DisplayClass33_0();
						awaiter = RpgIntensifyScreen.OfferPickAsync(null, null, null, null).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgIntensifyScreen.PickResult>, RpgIntensifyScreen.<OfferAsync>d__33>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<RpgIntensifyScreen.PickResult>);
						this.<>1__state = -1;
					}
					RpgIntensifyScreen.PickResult result = awaiter.GetResult();
					this.<>8__1.pick = result;
					if (!this.<>8__1.pick.Ok || string.IsNullOrWhiteSpace(this.<>8__1.pick.InstanceId))
					{
						result2 = false;
					}
					else
					{
						RpgItemData rpgItemData = RpgActiveLoadout.AllItems().FirstOrDefault(new Func<RpgItemData, bool>(this.<>8__1.<OfferAsync>b__0));
						if (rpgItemData == null)
						{
							result2 = false;
						}
						else
						{
							RpgItemData replacement = RpgItemIntensify.Apply(rpgItemData);
							result2 = RpgActiveLoadout.TryReplaceByInstanceId(this.<>8__1.pick.InstanceId, replacement);
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>8__1 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>8__1 = null;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06000605 RID: 1541 RVA: 0x0001F0CC File Offset: 0x0001D2CC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000205 RID: 517
			public int <>1__state;

			// Token: 0x04000206 RID: 518
			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000207 RID: 519
			[Nullable(0)]
			private RpgIntensifyScreen.<>c__DisplayClass33_0 <>8__1;

			// Token: 0x04000208 RID: 520
			[Nullable(0)]
			private TaskAwaiter<RpgIntensifyScreen.PickResult> <>u__1;
		}

		// Token: 0x02000098 RID: 152
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <OfferPickAsync>d__34 : IAsyncStateMachine
		{
			// Token: 0x06000606 RID: 1542 RVA: 0x0001F0DC File Offset: 0x0001D2DC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				RpgIntensifyScreen.PickResult result;
				try
				{
					if (num > 1)
					{
						RpgIntensifyScreen.<>c__DisplayClass34_0 CS$<>8__locals1 = new RpgIntensifyScreen.<>c__DisplayClass34_0();
						CS$<>8__locals1.filter = this.filter;
						SpireLoc.ForceInject();
						RpgItemRoller.EnsureUniqueInstanceIds(RpgActiveLoadout.AllItems(this.owner));
						List<RpgItemData> list = RpgActiveLoadout.AllItems(this.owner).Where(new Func<RpgItemData, bool>(CS$<>8__locals1.<OfferPickAsync>b__0)).Select(new Func<RpgItemData, RpgItemData>(RpgIntensifyScreen.<>c.<>9.<OfferPickAsync>b__34_1)).ToList<RpgItemData>();
						if (list.Count == 0)
						{
							result = new RpgIntensifyScreen.PickResult(false, "");
							goto IL_1FB;
						}
						this.<screen>5__2 = new RpgIntensifyScreen(list, this.chrome ?? RpgIntensifyScreen.IntensifyChrome);
					}
					try
					{
						TaskAwaiter<RpgIntensifyScreen.PickResult> awaiter;
						TaskAwaiter awaiter2;
						if (num != 0)
						{
							if (num == 1)
							{
								awaiter = this.<>u__2;
								this.<>u__2 = default(TaskAwaiter<RpgIntensifyScreen.PickResult>);
								num = (this.<>1__state = -1);
								goto IL_1C1;
							}
							if (this.preferHost != null && GodotObject.IsInstanceValid(this.preferHost))
							{
								this.preferHost.AddChild(this.<screen>5__2, false, 0L);
								this.<screen>5__2.MoveToFront();
								goto IL_163;
							}
							awaiter2 = RpgIntensifyScreen.AttachAsync(this.<screen>5__2).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgIntensifyScreen.<OfferPickAsync>d__34>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							num = (this.<>1__state = -1);
						}
						awaiter2.GetResult();
						IL_163:
						awaiter = this.<screen>5__2._done.Task.GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 1);
							this.<>u__2 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgIntensifyScreen.PickResult>, RpgIntensifyScreen.<OfferPickAsync>d__34>(ref awaiter, ref this);
							return;
						}
						IL_1C1:
						result = awaiter.GetResult();
					}
					finally
					{
						if (num < 0)
						{
							this.<screen>5__2.Dismiss();
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<screen>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1FB:
				this.<>1__state = -2;
				this.<screen>5__2 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000607 RID: 1543 RVA: 0x0001F334 File Offset: 0x0001D534
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000209 RID: 521
			public int <>1__state;

			// Token: 0x0400020A RID: 522
			[Nullable(0)]
			public AsyncTaskMethodBuilder<RpgIntensifyScreen.PickResult> <>t__builder;

			// Token: 0x0400020B RID: 523
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public Func<RpgItemData, bool> filter;

			// Token: 0x0400020C RID: 524
			[Nullable(0)]
			public Player owner;

			// Token: 0x0400020D RID: 525
			public RpgIntensifyScreen.PickChrome? chrome;

			// Token: 0x0400020E RID: 526
			[Nullable(0)]
			public Control preferHost;

			// Token: 0x0400020F RID: 527
			[Nullable(0)]
			private RpgIntensifyScreen <screen>5__2;

			// Token: 0x04000210 RID: 528
			private TaskAwaiter <>u__1;

			// Token: 0x04000211 RID: 529
			[Nullable(0)]
			private TaskAwaiter<RpgIntensifyScreen.PickResult> <>u__2;
		}

		// Token: 0x02000099 RID: 153
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WaitOneFrame>d__36 : IAsyncStateMachine
		{
			// Token: 0x06000608 RID: 1544 RVA: 0x0001F344 File Offset: 0x0001D544
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					TaskAwaiter awaiter;
					if (num != 0)
					{
						IAwaiter<Variant[]> awaiter2;
						if (num != 1)
						{
							SceneTree sceneTree = Engine.GetMainLoop() as SceneTree;
							if (sceneTree == null)
							{
								awaiter = Task.Delay(16).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgIntensifyScreen.<WaitOneFrame>d__36>(ref awaiter, ref this);
									return;
								}
								goto IL_74;
							}
							else
							{
								awaiter2 = sceneTree.ToSignal(sceneTree, SceneTree.SignalName.ProcessFrame).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitOnCompleted<IAwaiter<Variant[]>, RpgIntensifyScreen.<WaitOneFrame>d__36>(ref awaiter2, ref this);
									return;
								}
							}
						}
						else
						{
							awaiter2 = (IAwaiter<Variant[]>)this.<>u__2;
							this.<>u__2 = null;
							this.<>1__state = -1;
						}
						awaiter2.GetResult();
						goto IL_F5;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(TaskAwaiter);
					this.<>1__state = -1;
					IL_74:
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_F5:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000609 RID: 1545 RVA: 0x0001F46C File Offset: 0x0001D66C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000212 RID: 530
			public int <>1__state;

			// Token: 0x04000213 RID: 531
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000214 RID: 532
			private TaskAwaiter <>u__1;

			// Token: 0x04000215 RID: 533
			[Nullable(0)]
			private object <>u__2;
		}
	}
}
