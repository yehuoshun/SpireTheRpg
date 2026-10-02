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
using SpireTheRpg.Combat;
using SpireTheRpg.Core;

namespace SpireTheRpg.Ui
{
	// Token: 0x02000007 RID: 7
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RpgFusionScreen : Control, IOverlayScreen, IScreenContext
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003953 File Offset: 0x00001B53
		public NetScreenType ScreenType
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00003957 File Offset: 0x00001B57
		public bool UseSharedBackstop
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000395A File Offset: 0x00001B5A
		[Nullable(2)]
		public Control DefaultFocusedControl
		{
			[NullableContext(2)]
			get
			{
				return this._confirm;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003962 File Offset: 0x00001B62
		[Nullable(2)]
		public Control FocusedControlFromTopBar
		{
			[NullableContext(2)]
			get
			{
				return this.DefaultFocusedControl;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000396C File Offset: 0x00001B6C
		private RpgFusionScreen(List<RpgItemData> items)
		{
			this._items = items;
			base.Name = "SpireRpgFusionScreen";
			base.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			base.MouseFilter = 0L;
			base.FocusMode = 2L;
			base.Visible = true;
			this.Build();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003A08 File Offset: 0x00001C08
		public static Task<bool> OfferAsync()
		{
			RpgFusionScreen.<OfferAsync>d__28 <OfferAsync>d__;
			<OfferAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<OfferAsync>d__.<>1__state = -1;
			<OfferAsync>d__.<>t__builder.Start<RpgFusionScreen.<OfferAsync>d__28>(ref <OfferAsync>d__);
			return <OfferAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003A44 File Offset: 0x00001C44
		public static Task<RpgFusionScreen.PickResult> OfferPickAsync([Nullable(2)] Player owner)
		{
			RpgFusionScreen.<OfferPickAsync>d__29 <OfferPickAsync>d__;
			<OfferPickAsync>d__.<>t__builder = AsyncTaskMethodBuilder<RpgFusionScreen.PickResult>.Create();
			<OfferPickAsync>d__.owner = owner;
			<OfferPickAsync>d__.<>1__state = -1;
			<OfferPickAsync>d__.<>t__builder.Start<RpgFusionScreen.<OfferPickAsync>d__29>(ref <OfferPickAsync>d__);
			return <OfferPickAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003A88 File Offset: 0x00001C88
		private static Task AttachAsync(RpgFusionScreen screen)
		{
			RpgFusionScreen.<AttachAsync>d__30 <AttachAsync>d__;
			<AttachAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AttachAsync>d__.screen = screen;
			<AttachAsync>d__.<>1__state = -1;
			<AttachAsync>d__.<>t__builder.Start<RpgFusionScreen.<AttachAsync>d__30>(ref <AttachAsync>d__);
			return <AttachAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003ACC File Offset: 0x00001CCC
		private static Task WaitOneFrame()
		{
			RpgFusionScreen.<WaitOneFrame>d__31 <WaitOneFrame>d__;
			<WaitOneFrame>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WaitOneFrame>d__.<>1__state = -1;
			<WaitOneFrame>d__.<>t__builder.Start<RpgFusionScreen.<WaitOneFrame>d__31>(ref <WaitOneFrame>d__);
			return <WaitOneFrame>d__.<>t__builder.Task;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003B07 File Offset: 0x00001D07
		public void AfterOverlayOpened()
		{
			base.MoveToFront();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003B0F File Offset: 0x00001D0F
		public void AfterOverlayClosed()
		{
			if (this._closed)
			{
				return;
			}
			this._closed = true;
			this._done.TrySetResult(new RpgFusionScreen.PickResult(false, 0, Array.Empty<string>()));
			base.QueueFree();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003B3F File Offset: 0x00001D3F
		public void AfterOverlayShown()
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003B41 File Offset: 0x00001D41
		public void AfterOverlayHidden()
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003B43 File Offset: 0x00001D43
		public override void _ExitTree()
		{
			this._done.TrySetResult(new RpgFusionScreen.PickResult(false, 0, Array.Empty<string>()));
			base._ExitTree();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003B64 File Offset: 0x00001D64
		private void Dismiss()
		{
			if (this._closed)
			{
				return;
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
				if (!this._closed && GodotObject.IsInstanceValid(this) && base.IsInsideTree())
				{
					this._closed = true;
					base.QueueFree();
				}
				return;
			}
			this._closed = true;
			this._done.TrySetResult(new RpgFusionScreen.PickResult(false, 0, Array.Empty<string>()));
			if (GodotObject.IsInstanceValid(this) && base.IsInsideTree())
			{
				Node parent = base.GetParent();
				if (parent != null)
				{
					parent.RemoveChild(this);
				}
				base.Free();
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003C18 File Offset: 0x00001E18
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
			panel.AddThemeStyleboxOverride("panel", RpgFusionScreen.Flat(new Color(0.11f, 0.11f, 0.13f, 0.98f), new Color(1f, 1f, 1f, 0.12f), 1));
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
				Text = SpireLoc.Item("ui.fusion.title", "Fusion"),
				HorizontalAlignment = 1L
			};
			label.AddThemeFontSizeOverride("font_size", 28);
			vboxContainer.AddChild(label, false, 0L);
			Label label2 = new Label
			{
				Text = SpireLoc.Item("ui.fusion.hint", "Select 5 items of the same rarity and slot."),
				HorizontalAlignment = 1L
			};
			label2.AddThemeFontSizeOverride("font_size", 14);
			label2.Modulate = new Color(1f, 1f, 1f, 0.65f);
			vboxContainer.AddChild(label2, false, 0L);
			HBoxContainer hboxContainer = new HBoxContainer
			{
				Alignment = 1L,
				CustomMinimumSize = new Vector2(0f, 88f)
			};
			hboxContainer.AddThemeConstantOverride("separation", 12);
			vboxContainer.AddChild(hboxContainer, false, 0L);
			for (int i = 0; i < 5; i++)
			{
				int index = i;
				Panel slot = new Panel
				{
					CustomMinimumSize = new Vector2(80f, 80f),
					MouseFilter = 0L
				};
				RpgFusionScreen.StyleEmptySlot(slot);
				slot.GuiInput += delegate(InputEvent e)
				{
					InputEventMouseButton inputEventMouseButton = e as InputEventMouseButton;
					if (inputEventMouseButton != null && inputEventMouseButton.Pressed && inputEventMouseButton.ButtonIndex == 1L)
					{
						slot.AcceptEvent();
						this.RemoveAt(index);
					}
				};
				this._materialSlots[i] = slot;
				hboxContainer.AddChild(slot, false, 0L);
			}
			this._progress = new Label
			{
				Text = this.FormatProgress(),
				HorizontalAlignment = 1L
			};
			this._progress.AddThemeFontSizeOverride("font_size", 16);
			vboxContainer.AddChild(this._progress, false, 0L);
			this._preview = new Label
			{
				Text = "",
				HorizontalAlignment = 1L,
				AutowrapMode = 3L
			};
			this._preview.AddThemeFontSizeOverride("font_size", 16);
			this._preview.Modulate = new Color(1f, 0.85f, 0.55f, 1f);
			vboxContainer.AddChild(this._preview, false, 0L);
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
				CustomMinimumSize = new Vector2(0f, 320f)
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
				Text = SpireLoc.Item("ui.fusion.cancel", "Cancel"),
				CustomMinimumSize = new Vector2(140f, 44f)
			};
			button.Pressed += delegate()
			{
				this.Complete(false);
			};
			hboxContainer2.AddChild(button, false, 0L);
			Button button2 = new Button
			{
				Text = SpireLoc.Item("ui.fusion.auto", "Auto Select"),
				CustomMinimumSize = new Vector2(160f, 44f)
			};
			button2.Pressed += this.AutoSelect;
			hboxContainer2.AddChild(button2, false, 0L);
			this._confirm = new Button
			{
				Text = SpireLoc.Item("ui.fusion.confirm", "Fuse"),
				CustomMinimumSize = new Vector2(160f, 44f),
				Disabled = true
			};
			this._confirm.Pressed += this.Confirm;
			hboxContainer2.AddChild(this._confirm, false, 0L);
			this.RefreshSlots();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000041AC File Offset: 0x000023AC
		private void SetBrowseChanged()
		{
			this.RebuildFilterTabs();
			this.RebuildGrid();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000041BC File Offset: 0x000023BC
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

		// Token: 0x06000046 RID: 70 RVA: 0x0000423C File Offset: 0x0000243C
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
			this._gridById.Clear();
			foreach (RpgItemData item in RpgItemBrowseBar.Apply(this._items, this._browse))
			{
				this._grid.AddChild(this.MakePickSlot(item), false, 0L);
			}
			this.RefreshSlots();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000042FC File Offset: 0x000024FC
		private void AutoSelect()
		{
			if (this._locked || this._closed)
			{
				return;
			}
			RpgItemRarity? preferRarity = (this._picked.Count > 0) ? new RpgItemRarity?(this._picked[0].Rarity) : null;
			RpgEquipSlot? slot = this._browse.Slot;
			RpgEquipSlot? preferSlot = (slot != null) ? slot : ((this._picked.Count > 0) ? new RpgEquipSlot?(this._picked[0].EquipSlot) : null);
			List<RpgItemData> list = RpgItemFusion.PickAutoMaterials(this._items, preferSlot, preferRarity);
			if (list == null || list.Count == 0)
			{
				return;
			}
			this._picked.Clear();
			this._picked.AddRange(list);
			this._lockRarity = new RpgItemRarity?(list[0].Rarity);
			this._lockSlot = new RpgEquipSlot?(list[0].EquipSlot);
			if (this._browse.Slot != null)
			{
				slot = this._browse.Slot;
				RpgEquipSlot? lockSlot = this._lockSlot;
				if (!(slot.GetValueOrDefault() == lockSlot.GetValueOrDefault() & slot != null == (lockSlot != null)))
				{
					this._browse.Slot = this._lockSlot;
					this.RebuildFilterTabs();
					this.RebuildGrid();
					return;
				}
			}
			this.RefreshSlots();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004468 File Offset: 0x00002668
		private void Toggle(RpgItemData item)
		{
			int num = this._picked.FindIndex((RpgItemData x) => x.InstanceId == item.InstanceId);
			if (num >= 0)
			{
				this._picked.RemoveAt(num);
				if (this._picked.Count == 0)
				{
					this._lockRarity = null;
					this._lockSlot = null;
				}
				this.RefreshSlots();
				return;
			}
			if (this._picked.Count >= 5)
			{
				return;
			}
			if (this._lockRarity != null)
			{
				RpgItemRarity rarity = item.Rarity;
				RpgItemRarity? lockRarity = this._lockRarity;
				if (rarity == lockRarity.GetValueOrDefault() & lockRarity != null)
				{
					RpgEquipSlot equipSlot = item.EquipSlot;
					RpgEquipSlot? lockSlot = this._lockSlot;
					if (equipSlot == lockSlot.GetValueOrDefault() & lockSlot != null)
					{
						goto IL_F6;
					}
				}
				return;
			}
			this._lockRarity = new RpgItemRarity?(item.Rarity);
			this._lockSlot = new RpgEquipSlot?(item.EquipSlot);
			IL_F6:
			this._picked.Add(item);
			this.RefreshSlots();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00004584 File Offset: 0x00002784
		private void RemoveAt(int index)
		{
			if (index < 0 || index >= this._picked.Count)
			{
				return;
			}
			this._picked.RemoveAt(index);
			if (this._picked.Count == 0)
			{
				this._lockRarity = null;
				this._lockSlot = null;
			}
			this.RefreshSlots();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000045DC File Offset: 0x000027DC
		private void RefreshSlots()
		{
			for (int i = 0; i < this._materialSlots.Length; i++)
			{
				Panel panel = this._materialSlots[i];
				RpgFusionScreen.ClearChildren(panel);
				if (i < this._picked.Count)
				{
					RpgItemData item = this._picked[i];
					RpgFusionScreen.StyleFilledSlot(panel, item);
					RpgFusionScreen.AddItemIcon(panel, item);
				}
				else
				{
					RpgFusionScreen.StyleEmptySlot(panel);
				}
			}
			foreach (KeyValuePair<string, Panel> keyValuePair in this._gridById)
			{
				string id2;
				Panel panel2;
				keyValuePair.Deconstruct(out id2, out panel2);
				string id = id2;
				Panel panel3 = panel2;
				RpgItemData rpgItemData = this._items.FirstOrDefault((RpgItemData x) => x.InstanceId == id);
				if (rpgItemData != null)
				{
					bool selected = this._picked.Any((RpgItemData x) => x.InstanceId == id);
					RpgFusionScreen.StyleGridSlot(panel3, rpgItemData, selected);
				}
			}
			if (this._progress != null)
			{
				this._progress.Text = this.FormatProgress();
			}
			if (this._preview != null)
			{
				if (this._picked.Count == 5 && this._lockRarity != null && this._lockSlot != null)
				{
					RpgItemRarity? value = RpgItemFusion.NextRarity(this._lockRarity.Value);
					if (value != null)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
						defaultInterpolatedStringHandler.AppendLiteral("rarity.");
						defaultInterpolatedStringHandler.AppendFormatted<RpgItemRarity?>(value);
						string text = SpireLoc.Item(defaultInterpolatedStringHandler.ToStringAndClear(), value.ToString());
						defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
						defaultInterpolatedStringHandler.AppendLiteral("type.");
						defaultInterpolatedStringHandler.AppendFormatted<RpgEquipSlot?>(this._lockSlot);
						string text2 = SpireLoc.Item(defaultInterpolatedStringHandler.ToStringAndClear(), this._lockSlot.ToString());
						this._preview.Text = SpireLoc.ItemFormat("ui.fusion.preview", "Will forge a random {0} {1}", new object[]
						{
							text,
							text2
						});
					}
					else
					{
						this._preview.Text = "";
					}
				}
				else
				{
					this._preview.Text = "";
				}
			}
			if (this._confirm != null)
			{
				this._confirm.Disabled = (this._picked.Count != 5);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004840 File Offset: 0x00002A40
		private string FormatProgress()
		{
			return SpireLoc.ItemFormat("ui.fusion.progress", "{0} / 5 selected", new object[]
			{
				this._picked.Count
			});
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000486C File Offset: 0x00002A6C
		private void Confirm()
		{
			if (this._locked || this._closed || this._picked.Count != 5)
			{
				return;
			}
			this._locked = true;
			this._pickedIds = (from x in this._picked
			select x.InstanceId).ToList<string>();
			this._seed = (int)((uint)DateTime.UtcNow.Ticks ^ (uint)(this._pickedIds.Count * -1640531535));
			foreach (string text in this._pickedIds)
			{
				this._seed = this._seed * 31 + text.GetHashCode();
			}
			this.Complete(true);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004958 File Offset: 0x00002B58
		private void Complete(bool ok)
		{
			if (this._closed)
			{
				return;
			}
			this._locked = true;
			this._done.TrySetResult(new RpgFusionScreen.PickResult(ok, ok ? this._seed : 0, ok ? this._pickedIds : new List<string>()));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004998 File Offset: 0x00002B98
		private Panel MakePickSlot(RpgItemData item)
		{
			Panel panel = new Panel
			{
				CustomMinimumSize = new Vector2(72f, 72f),
				MouseFilter = 0L
			};
			RpgFusionScreen.StyleGridSlot(panel, item, false);
			RpgFusionScreen.AddItemIcon(panel, item);
			this._gridById[item.InstanceId] = panel;
			panel.GuiInput += delegate(InputEvent e)
			{
				InputEventMouseButton inputEventMouseButton = e as InputEventMouseButton;
				if (inputEventMouseButton != null && inputEventMouseButton.Pressed && inputEventMouseButton.ButtonIndex == 1L)
				{
					panel.AcceptEvent();
					this.Toggle(item);
				}
			};
			return panel;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004A40 File Offset: 0x00002C40
		private static void AddItemIcon(Panel panel, RpgItemData item)
		{
			Texture2D texture2D = SpireAssets.ItemIcon(item.IconPath);
			if (texture2D == null)
			{
				return;
			}
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

		// Token: 0x06000050 RID: 80 RVA: 0x00004AC4 File Offset: 0x00002CC4
		private static void ClearChildren(Panel panel)
		{
			foreach (Node node in panel.GetChildren(false))
			{
				node.QueueFree();
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004B10 File Offset: 0x00002D10
		private static void StyleEmptySlot(Panel panel)
		{
			panel.AddThemeStyleboxOverride("panel", RpgFusionScreen.Flat(new Color(0.14f, 0.14f, 0.16f, 1f), new Color(1f, 1f, 1f, 0.18f), 2));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004B65 File Offset: 0x00002D65
		private static void StyleFilledSlot(Panel panel, RpgItemData item)
		{
			panel.AddThemeStyleboxOverride("panel", RpgFusionScreen.Flat(new Color(0.22f, 0.18f, 0.12f, 1f), RpgItemTooltip.RarityColor(item.Rarity), 3));
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004BA4 File Offset: 0x00002DA4
		private static void StyleGridSlot(Panel panel, RpgItemData item, bool selected)
		{
			Color border = RpgItemTooltip.RarityColor(item.Rarity);
			if (selected)
			{
				panel.AddThemeStyleboxOverride("panel", RpgFusionScreen.Flat(new Color(0.28f, 0.2f, 0.12f, 1f), new Color(1f, 0.75f, 0.35f, 1f), 3));
				return;
			}
			panel.AddThemeStyleboxOverride("panel", RpgFusionScreen.Flat(new Color(0.16f, 0.14f, 0.16f, 1f), border, 2));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004C3C File Offset: 0x00002E3C
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
				CornerRadiusBottomLeft = 10
			};
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004C98 File Offset: 0x00002E98
		[CompilerGenerated]
		private void <Build>b__38_0()
		{
			this.Complete(false);
		}

		// Token: 0x04000013 RID: 19
		private readonly TaskCompletionSource<RpgFusionScreen.PickResult> _done = new TaskCompletionSource<RpgFusionScreen.PickResult>();

		// Token: 0x04000014 RID: 20
		private readonly List<RpgItemData> _items;

		// Token: 0x04000015 RID: 21
		private readonly List<RpgItemData> _picked = new List<RpgItemData>();

		// Token: 0x04000016 RID: 22
		private readonly Dictionary<string, Panel> _gridById = new Dictionary<string, Panel>(StringComparer.Ordinal);

		// Token: 0x04000017 RID: 23
		private readonly Panel[] _materialSlots = new Panel[5];

		// Token: 0x04000018 RID: 24
		private RpgItemRarity? _lockRarity;

		// Token: 0x04000019 RID: 25
		private RpgEquipSlot? _lockSlot;

		// Token: 0x0400001A RID: 26
		private readonly RpgItemBrowseState _browse = new RpgItemBrowseState();

		// Token: 0x0400001B RID: 27
		[Nullable(2)]
		private HBoxContainer _filterHost;

		// Token: 0x0400001C RID: 28
		[Nullable(2)]
		private HFlowContainer _grid;

		// Token: 0x0400001D RID: 29
		[Nullable(2)]
		private Label _progress;

		// Token: 0x0400001E RID: 30
		[Nullable(2)]
		private Label _preview;

		// Token: 0x0400001F RID: 31
		[Nullable(2)]
		private Button _confirm;

		// Token: 0x04000020 RID: 32
		private bool _pushedToOverlay;

		// Token: 0x04000021 RID: 33
		private bool _closed;

		// Token: 0x04000022 RID: 34
		private bool _locked;

		// Token: 0x04000023 RID: 35
		private int _seed;

		// Token: 0x04000024 RID: 36
		private List<string> _pickedIds = new List<string>();

		// Token: 0x02000084 RID: 132
		[Nullable(0)]
		internal readonly struct PickResult : IEquatable<RpgFusionScreen.PickResult>
		{
			// Token: 0x060005B1 RID: 1457 RVA: 0x0001E005 File Offset: 0x0001C205
			public PickResult(bool Ok, int Seed, IReadOnlyList<string> MaterialIds)
			{
				this.Ok = Ok;
				this.Seed = Seed;
				this.MaterialIds = MaterialIds;
			}

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001E01C File Offset: 0x0001C21C
			// (set) Token: 0x060005B3 RID: 1459 RVA: 0x0001E024 File Offset: 0x0001C224
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

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001E02D File Offset: 0x0001C22D
			// (set) Token: 0x060005B5 RID: 1461 RVA: 0x0001E035 File Offset: 0x0001C235
			public int Seed
			{
				[CompilerGenerated]
				get
				{
					return this.<Seed>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Seed>k__BackingField = value;
				}
			}

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001E03E File Offset: 0x0001C23E
			// (set) Token: 0x060005B7 RID: 1463 RVA: 0x0001E046 File Offset: 0x0001C246
			public IReadOnlyList<string> MaterialIds
			{
				[CompilerGenerated]
				get
				{
					return this.<MaterialIds>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<MaterialIds>k__BackingField = value;
				}
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x0001E050 File Offset: 0x0001C250
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

			// Token: 0x060005B9 RID: 1465 RVA: 0x0001E09C File Offset: 0x0001C29C
			[NullableContext(0)]
			[CompilerGenerated]
			private bool PrintMembers(StringBuilder builder)
			{
				builder.Append("Ok = ");
				builder.Append(this.Ok.ToString());
				builder.Append(", Seed = ");
				builder.Append(this.Seed.ToString());
				builder.Append(", MaterialIds = ");
				builder.Append(this.MaterialIds);
				return true;
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x0001E111 File Offset: 0x0001C311
			[CompilerGenerated]
			public static bool operator !=(RpgFusionScreen.PickResult left, RpgFusionScreen.PickResult right)
			{
				return !(left == right);
			}

			// Token: 0x060005BB RID: 1467 RVA: 0x0001E11D File Offset: 0x0001C31D
			[CompilerGenerated]
			public static bool operator ==(RpgFusionScreen.PickResult left, RpgFusionScreen.PickResult right)
			{
				return left.Equals(right);
			}

			// Token: 0x060005BC RID: 1468 RVA: 0x0001E127 File Offset: 0x0001C327
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return (EqualityComparer<bool>.Default.GetHashCode(this.<Ok>k__BackingField) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Seed>k__BackingField)) * -1521134295 + EqualityComparer<IReadOnlyList<string>>.Default.GetHashCode(this.<MaterialIds>k__BackingField);
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x0001E167 File Offset: 0x0001C367
			[NullableContext(0)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return obj is RpgFusionScreen.PickResult && this.Equals((RpgFusionScreen.PickResult)obj);
			}

			// Token: 0x060005BE RID: 1470 RVA: 0x0001E180 File Offset: 0x0001C380
			[CompilerGenerated]
			public bool Equals(RpgFusionScreen.PickResult other)
			{
				return EqualityComparer<bool>.Default.Equals(this.<Ok>k__BackingField, other.<Ok>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Seed>k__BackingField, other.<Seed>k__BackingField) && EqualityComparer<IReadOnlyList<string>>.Default.Equals(this.<MaterialIds>k__BackingField, other.<MaterialIds>k__BackingField);
			}

			// Token: 0x060005BF RID: 1471 RVA: 0x0001E1D5 File Offset: 0x0001C3D5
			[CompilerGenerated]
			public void Deconstruct(out bool Ok, out int Seed, out IReadOnlyList<string> MaterialIds)
			{
				Ok = this.Ok;
				Seed = this.Seed;
				MaterialIds = this.MaterialIds;
			}

			// Token: 0x040001CD RID: 461
			[CompilerGenerated]
			private readonly bool <Ok>k__BackingField;

			// Token: 0x040001CE RID: 462
			[CompilerGenerated]
			private readonly int <Seed>k__BackingField;

			// Token: 0x040001CF RID: 463
			[CompilerGenerated]
			private readonly IReadOnlyList<string> <MaterialIds>k__BackingField;
		}

		// Token: 0x02000085 RID: 133
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005C0 RID: 1472 RVA: 0x0001E1EF File Offset: 0x0001C3EF
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x0001E1FB File Offset: 0x0001C3FB
			public <>c()
			{
			}

			// Token: 0x060005C2 RID: 1474 RVA: 0x0001E204 File Offset: 0x0001C404
			[NullableContext(0)]
			internal RpgItemData <OfferAsync>b__28_0(string id)
			{
				RpgFusionScreen.<>c__DisplayClass28_0 CS$<>8__locals1 = new RpgFusionScreen.<>c__DisplayClass28_0();
				CS$<>8__locals1.id = id;
				return RpgActiveLoadout.AllItems().FirstOrDefault(new Func<RpgItemData, bool>(CS$<>8__locals1.<OfferAsync>b__2));
			}

			// Token: 0x060005C3 RID: 1475 RVA: 0x0001E234 File Offset: 0x0001C434
			[NullableContext(0)]
			internal bool <OfferAsync>b__28_1(RpgItemData x)
			{
				return x != null;
			}

			// Token: 0x060005C4 RID: 1476 RVA: 0x0001E23A File Offset: 0x0001C43A
			[NullableContext(0)]
			internal bool <OfferPickAsync>b__29_0(RpgItemData x)
			{
				return RpgItemFusion.CanFuse(x.Rarity);
			}

			// Token: 0x060005C5 RID: 1477 RVA: 0x0001E247 File Offset: 0x0001C447
			[NullableContext(0)]
			internal RpgItemData <OfferPickAsync>b__29_1(RpgItemData x)
			{
				return x.Clone();
			}

			// Token: 0x060005C6 RID: 1478 RVA: 0x0001E24F File Offset: 0x0001C44F
			[NullableContext(0)]
			internal string <Confirm>b__47_0(RpgItemData x)
			{
				return x.InstanceId;
			}

			// Token: 0x040001D0 RID: 464
			[Nullable(0)]
			public static readonly RpgFusionScreen.<>c <>9 = new RpgFusionScreen.<>c();

			// Token: 0x040001D1 RID: 465
			[Nullable(0)]
			public static Func<string, RpgItemData> <>9__28_0;

			// Token: 0x040001D2 RID: 466
			[Nullable(0)]
			public static Func<RpgItemData, bool> <>9__28_1;

			// Token: 0x040001D3 RID: 467
			[Nullable(0)]
			public static Func<RpgItemData, bool> <>9__29_0;

			// Token: 0x040001D4 RID: 468
			[Nullable(0)]
			public static Func<RpgItemData, RpgItemData> <>9__29_1;

			// Token: 0x040001D5 RID: 469
			[Nullable(0)]
			public static Func<RpgItemData, string> <>9__47_0;
		}

		// Token: 0x02000086 RID: 134
		[CompilerGenerated]
		private sealed class <>c__DisplayClass28_0
		{
			// Token: 0x060005C7 RID: 1479 RVA: 0x0001E257 File Offset: 0x0001C457
			public <>c__DisplayClass28_0()
			{
			}

			// Token: 0x060005C8 RID: 1480 RVA: 0x0001E25F File Offset: 0x0001C45F
			[NullableContext(0)]
			internal bool <OfferAsync>b__2(RpgItemData x)
			{
				return string.Equals(x.InstanceId, this.id, StringComparison.Ordinal);
			}

			// Token: 0x040001D6 RID: 470
			[Nullable(0)]
			public string id;
		}

		// Token: 0x02000087 RID: 135
		[CompilerGenerated]
		private sealed class <>c__DisplayClass38_0
		{
			// Token: 0x060005C9 RID: 1481 RVA: 0x0001E273 File Offset: 0x0001C473
			public <>c__DisplayClass38_0()
			{
			}

			// Token: 0x060005CA RID: 1482 RVA: 0x0001E27C File Offset: 0x0001C47C
			[NullableContext(0)]
			internal void <Build>b__1(InputEvent e)
			{
				InputEventMouseButton inputEventMouseButton = e as InputEventMouseButton;
				if (inputEventMouseButton != null && inputEventMouseButton.Pressed && inputEventMouseButton.ButtonIndex == 1L)
				{
					this.slot.AcceptEvent();
					this.<>4__this.RemoveAt(this.index);
				}
			}

			// Token: 0x040001D7 RID: 471
			[Nullable(0)]
			public Panel slot;

			// Token: 0x040001D8 RID: 472
			public int index;

			// Token: 0x040001D9 RID: 473
			[Nullable(0)]
			public RpgFusionScreen <>4__this;
		}

		// Token: 0x02000088 RID: 136
		[CompilerGenerated]
		private sealed class <>c__DisplayClass43_0
		{
			// Token: 0x060005CB RID: 1483 RVA: 0x0001E2C1 File Offset: 0x0001C4C1
			public <>c__DisplayClass43_0()
			{
			}

			// Token: 0x060005CC RID: 1484 RVA: 0x0001E2C9 File Offset: 0x0001C4C9
			internal bool <Toggle>b__0(RpgItemData x)
			{
				return x.InstanceId == this.item.InstanceId;
			}

			// Token: 0x040001DA RID: 474
			[Nullable(0)]
			public RpgItemData item;
		}

		// Token: 0x02000089 RID: 137
		[CompilerGenerated]
		private sealed class <>c__DisplayClass45_0
		{
			// Token: 0x060005CD RID: 1485 RVA: 0x0001E2E1 File Offset: 0x0001C4E1
			public <>c__DisplayClass45_0()
			{
			}

			// Token: 0x060005CE RID: 1486 RVA: 0x0001E2E9 File Offset: 0x0001C4E9
			[NullableContext(0)]
			internal bool <RefreshSlots>b__0(RpgItemData x)
			{
				return x.InstanceId == this.id;
			}

			// Token: 0x060005CF RID: 1487 RVA: 0x0001E2FC File Offset: 0x0001C4FC
			[NullableContext(0)]
			internal bool <RefreshSlots>b__1(RpgItemData x)
			{
				return x.InstanceId == this.id;
			}

			// Token: 0x040001DB RID: 475
			[Nullable(0)]
			public string id;
		}

		// Token: 0x0200008A RID: 138
		[CompilerGenerated]
		private sealed class <>c__DisplayClass49_0
		{
			// Token: 0x060005D0 RID: 1488 RVA: 0x0001E30F File Offset: 0x0001C50F
			public <>c__DisplayClass49_0()
			{
			}

			// Token: 0x060005D1 RID: 1489 RVA: 0x0001E318 File Offset: 0x0001C518
			[NullableContext(0)]
			internal void <MakePickSlot>b__0(InputEvent e)
			{
				InputEventMouseButton inputEventMouseButton = e as InputEventMouseButton;
				if (inputEventMouseButton != null && inputEventMouseButton.Pressed && inputEventMouseButton.ButtonIndex == 1L)
				{
					this.panel.AcceptEvent();
					this.<>4__this.Toggle(this.item);
				}
			}

			// Token: 0x040001DC RID: 476
			[Nullable(0)]
			public Panel panel;

			// Token: 0x040001DD RID: 477
			[Nullable(0)]
			public RpgFusionScreen <>4__this;

			// Token: 0x040001DE RID: 478
			[Nullable(0)]
			public RpgItemData item;
		}

		// Token: 0x0200008B RID: 139
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <AttachAsync>d__30 : IAsyncStateMachine
		{
			// Token: 0x060005D2 RID: 1490 RVA: 0x0001E360 File Offset: 0x0001C560
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
								this.screen._done.TrySetResult(new RpgFusionScreen.PickResult(false, 0, Array.Empty<string>()));
							}
						}
					}
					else
					{
						awaiter = RpgFusionScreen.WaitOneFrame().GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgFusionScreen.<AttachAsync>d__30>(ref awaiter, ref this);
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

			// Token: 0x060005D3 RID: 1491 RVA: 0x0001E4C0 File Offset: 0x0001C6C0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040001DF RID: 479
			public int <>1__state;

			// Token: 0x040001E0 RID: 480
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040001E1 RID: 481
			[Nullable(0)]
			public RpgFusionScreen screen;

			// Token: 0x040001E2 RID: 482
			private int <i>5__2;

			// Token: 0x040001E3 RID: 483
			private TaskAwaiter <>u__1;
		}

		// Token: 0x0200008C RID: 140
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <OfferAsync>d__28 : IAsyncStateMachine
		{
			// Token: 0x060005D4 RID: 1492 RVA: 0x0001E4D0 File Offset: 0x0001C6D0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result2;
				try
				{
					TaskAwaiter<RpgFusionScreen.PickResult> awaiter;
					if (num != 0)
					{
						awaiter = RpgFusionScreen.OfferPickAsync(null).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgFusionScreen.PickResult>, RpgFusionScreen.<OfferAsync>d__28>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<RpgFusionScreen.PickResult>);
						this.<>1__state = -1;
					}
					RpgFusionScreen.PickResult result = awaiter.GetResult();
					RpgItemData rpgItemData;
					if (!result.Ok || result.MaterialIds.Count != 5)
					{
						result2 = false;
					}
					else if (!RpgItemFusion.TryFuse(result.MaterialIds.Select(new Func<string, RpgItemData>(RpgFusionScreen.<>c.<>9.<OfferAsync>b__28_0)).Where(new Func<RpgItemData, bool>(RpgFusionScreen.<>c.<>9.<OfferAsync>b__28_1)).Cast<RpgItemData>().ToList<RpgItemData>(), RpgRng.FromSeed((ulong)result.Seed), out rpgItemData) || rpgItemData == null)
					{
						result2 = false;
					}
					else if (!RpgActiveLoadout.TryRemoveManyByInstanceIds(result.MaterialIds))
					{
						result2 = false;
					}
					else
					{
						RpgActiveLoadout.AddToBag(rpgItemData);
						result2 = true;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x060005D5 RID: 1493 RVA: 0x0001E640 File Offset: 0x0001C840
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040001E4 RID: 484
			public int <>1__state;

			// Token: 0x040001E5 RID: 485
			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x040001E6 RID: 486
			[Nullable(0)]
			private TaskAwaiter<RpgFusionScreen.PickResult> <>u__1;
		}

		// Token: 0x0200008D RID: 141
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <OfferPickAsync>d__29 : IAsyncStateMachine
		{
			// Token: 0x060005D6 RID: 1494 RVA: 0x0001E650 File Offset: 0x0001C850
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				RpgFusionScreen.PickResult result;
				try
				{
					if (num > 1)
					{
						RpgItemRoller.EnsureUniqueInstanceIds(RpgActiveLoadout.AllItems(this.owner));
						List<RpgItemData> items = RpgActiveLoadout.AllItems(this.owner).Where(new Func<RpgItemData, bool>(RpgFusionScreen.<>c.<>9.<OfferPickAsync>b__29_0)).Select(new Func<RpgItemData, RpgItemData>(RpgFusionScreen.<>c.<>9.<OfferPickAsync>b__29_1)).ToList<RpgItemData>();
						if (!RpgItemFusion.HasFusableGroup(items))
						{
							result = new RpgFusionScreen.PickResult(false, 0, Array.Empty<string>());
							goto IL_1A0;
						}
						this.<screen>5__2 = new RpgFusionScreen(items);
					}
					try
					{
						TaskAwaiter<RpgFusionScreen.PickResult> awaiter;
						TaskAwaiter awaiter2;
						if (num != 0)
						{
							if (num == 1)
							{
								awaiter = this.<>u__2;
								this.<>u__2 = default(TaskAwaiter<RpgFusionScreen.PickResult>);
								num = (this.<>1__state = -1);
								goto IL_166;
							}
							awaiter2 = RpgFusionScreen.AttachAsync(this.<screen>5__2).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgFusionScreen.<OfferPickAsync>d__29>(ref awaiter2, ref this);
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
						awaiter = this.<screen>5__2._done.Task.GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 1);
							this.<>u__2 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgFusionScreen.PickResult>, RpgFusionScreen.<OfferPickAsync>d__29>(ref awaiter, ref this);
							return;
						}
						IL_166:
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
				IL_1A0:
				this.<>1__state = -2;
				this.<screen>5__2 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060005D7 RID: 1495 RVA: 0x0001E84C File Offset: 0x0001CA4C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040001E7 RID: 487
			public int <>1__state;

			// Token: 0x040001E8 RID: 488
			[Nullable(0)]
			public AsyncTaskMethodBuilder<RpgFusionScreen.PickResult> <>t__builder;

			// Token: 0x040001E9 RID: 489
			[Nullable(0)]
			public Player owner;

			// Token: 0x040001EA RID: 490
			[Nullable(0)]
			private RpgFusionScreen <screen>5__2;

			// Token: 0x040001EB RID: 491
			private TaskAwaiter <>u__1;

			// Token: 0x040001EC RID: 492
			[Nullable(0)]
			private TaskAwaiter<RpgFusionScreen.PickResult> <>u__2;
		}

		// Token: 0x0200008E RID: 142
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WaitOneFrame>d__31 : IAsyncStateMachine
		{
			// Token: 0x060005D8 RID: 1496 RVA: 0x0001E85C File Offset: 0x0001CA5C
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
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgFusionScreen.<WaitOneFrame>d__31>(ref awaiter, ref this);
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
									this.<>t__builder.AwaitOnCompleted<IAwaiter<Variant[]>, RpgFusionScreen.<WaitOneFrame>d__31>(ref awaiter2, ref this);
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

			// Token: 0x060005D9 RID: 1497 RVA: 0x0001E984 File Offset: 0x0001CB84
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040001ED RID: 493
			public int <>1__state;

			// Token: 0x040001EE RID: 494
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040001EF RID: 495
			private TaskAwaiter <>u__1;

			// Token: 0x040001F0 RID: 496
			[Nullable(0)]
			private object <>u__2;
		}
	}
}
