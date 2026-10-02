using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using SpireTheRpg.Core;

namespace SpireTheRpg.Ui
{
	// Token: 0x0200000D RID: 13
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgItemBrowseBar
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00008C28 File Offset: 0x00006E28
		public static Control Create(RpgItemBrowseState state, Action onChanged)
		{
			HBoxContainer hboxContainer = new HBoxContainer
			{
				SizeFlagsHorizontal = 3L,
				MouseFilter = 0L
			};
			hboxContainer.AddThemeConstantOverride("separation", 6);
			hboxContainer.AddChild(RpgItemBrowseBar.MakeSlotTab(SpireLoc.T("SPIRE_INVENTORY.tab_all", "All"), null, false, state, onChanged), false, 0L);
			foreach (RpgEquipSlot rpgEquipSlot in RpgItemBrowseBar.Slots)
			{
				hboxContainer.AddChild(RpgItemBrowseBar.MakeSlotTab(RpgItemBrowseBar.SlotLabel(rpgEquipSlot), new RpgEquipSlot?(rpgEquipSlot), false, state, onChanged), false, 0L);
			}
			hboxContainer.AddChild(RpgItemBrowseBar.MakeSlotTab(SpireLoc.T("SPIRE_INVENTORY.tab_other", "Other"), null, true, state, onChanged), false, 0L);
			hboxContainer.AddChild(new Control
			{
				SizeFlagsHorizontal = 3L,
				MouseFilter = 2L
			}, false, 0L);
			hboxContainer.AddChild(RpgItemBrowseBar.MakeRarityCombo(state, onChanged), false, 0L);
			hboxContainer.AddChild(RpgItemBrowseBar.MakeSortCombo(state, onChanged), false, 0L);
			return hboxContainer;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00008D28 File Offset: 0x00006F28
		public static IEnumerable<RpgItemData> Apply(IEnumerable<RpgItemData> items, RpgItemBrowseState state)
		{
			IEnumerable<RpgItemData> enumerable = items;
			if (state.OtherOnly)
			{
				enumerable = from x in enumerable
				where x.IsScroll
				select x;
			}
			else if (state.Slot != null)
			{
				enumerable = enumerable.Where(delegate(RpgItemData x)
				{
					if (!x.IsScroll)
					{
						RpgEquipSlot equipSlot = x.EquipSlot;
						RpgEquipSlot? slot = state.Slot;
						return equipSlot == slot.GetValueOrDefault() & slot != null;
					}
					return false;
				});
			}
			if (state.Rarity != null)
			{
				enumerable = enumerable.Where(delegate(RpgItemData x)
				{
					RpgItemRarity rarity = x.Rarity;
					RpgItemRarity? rarity2 = state.Rarity;
					return rarity == rarity2.GetValueOrDefault() & rarity2 != null;
				});
			}
			IEnumerable<RpgItemData> result;
			switch (state.Sort)
			{
			case RpgItemSortMode.RarityDesc:
				result = from x in enumerable
				orderby (int)x.Rarity descending, x.RequireLevel descending, x.ItemPower descending
				select x;
				break;
			case RpgItemSortMode.LevelDesc:
				result = from x in enumerable
				orderby x.RequireLevel descending, (int)x.Rarity descending, x.ItemPower descending
				select x;
				break;
			case RpgItemSortMode.PowerDesc:
				result = from x in enumerable
				orderby x.ItemPower descending, (int)x.Rarity descending, x.RequireLevel descending
				select x;
				break;
			default:
				result = enumerable;
				break;
			}
			return result;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00008F4C File Offset: 0x0000714C
		private static Button MakeSlotTab(string text, RpgEquipSlot? slot, bool otherOnly, RpgItemBrowseState state, Action onChanged)
		{
			RpgItemBrowseBar.<>c__DisplayClass5_0 CS$<>8__locals1 = new RpgItemBrowseBar.<>c__DisplayClass5_0();
			CS$<>8__locals1.state = state;
			CS$<>8__locals1.otherOnly = otherOnly;
			CS$<>8__locals1.slot = slot;
			CS$<>8__locals1.onChanged = onChanged;
			RpgItemBrowseBar.<>c__DisplayClass5_0 CS$<>8__locals2 = CS$<>8__locals1;
			bool selected;
			if (!CS$<>8__locals1.otherOnly)
			{
				if (!CS$<>8__locals1.state.OtherOnly)
				{
					RpgEquipSlot? slot2 = CS$<>8__locals1.state.Slot;
					RpgEquipSlot? slot3 = CS$<>8__locals1.slot;
					selected = (slot2.GetValueOrDefault() == slot3.GetValueOrDefault() & slot2 != null == (slot3 != null));
				}
				else
				{
					selected = false;
				}
			}
			else
			{
				selected = CS$<>8__locals1.state.OtherOnly;
			}
			CS$<>8__locals2.selected = selected;
			Button button = RpgItemBrowseBar.MakeBaseButton(text, CS$<>8__locals1.selected, true);
			button.Pressed += delegate()
			{
				if (CS$<>8__locals1.selected)
				{
					return;
				}
				CS$<>8__locals1.state.OtherOnly = CS$<>8__locals1.otherOnly;
				CS$<>8__locals1.state.Slot = (CS$<>8__locals1.otherOnly ? null : CS$<>8__locals1.slot);
				CS$<>8__locals1.onChanged();
			};
			return button;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00008FFC File Offset: 0x000071FC
		private static OptionButton MakeRarityCombo(RpgItemBrowseState state, Action onChanged)
		{
			OptionButton optionButton = RpgItemBrowseBar.MakeComboShell();
			optionButton.AddItem(SpireLoc.T("SPIRE_INVENTORY.filter_rarity_all", "Rarity: All"), -1);
			foreach (RpgItemRarity value in RpgItemBrowseBar.Rarities)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
				defaultInterpolatedStringHandler.AppendLiteral("rarity.");
				defaultInterpolatedStringHandler.AppendFormatted<RpgItemRarity>(value);
				string str = SpireLoc.Item(defaultInterpolatedStringHandler.ToStringAndClear(), value.ToString());
				optionButton.AddItem(SpireLoc.T("SPIRE_INVENTORY.filter_rarity_prefix", "Rarity") + ": " + str, -1);
			}
			int num = (state.Rarity == null) ? 0 : (Array.IndexOf<RpgItemRarity>(RpgItemBrowseBar.Rarities, state.Rarity.Value) + 1);
			optionButton.Select((num < 0) ? 0 : num);
			optionButton.ItemSelected += delegate(long idx)
			{
				RpgItemRarity? rpgItemRarity = (idx <= 0L) ? null : new RpgItemRarity?(RpgItemBrowseBar.Rarities[(int)idx - 1]);
				RpgItemRarity? rarity = state.Rarity;
				RpgItemRarity? rpgItemRarity2 = rpgItemRarity;
				if (rarity.GetValueOrDefault() == rpgItemRarity2.GetValueOrDefault() & rarity != null == (rpgItemRarity2 != null))
				{
					return;
				}
				state.Rarity = rpgItemRarity;
				onChanged();
			};
			return optionButton;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00009110 File Offset: 0x00007310
		private static OptionButton MakeSortCombo(RpgItemBrowseState state, Action onChanged)
		{
			OptionButton optionButton = RpgItemBrowseBar.MakeComboShell();
			foreach (RpgItemSortMode sort in RpgItemBrowseBar.Sorts)
			{
				optionButton.AddItem(RpgItemBrowseBar.SortLabel(sort), -1);
			}
			int num = Array.IndexOf<RpgItemSortMode>(RpgItemBrowseBar.Sorts, state.Sort);
			optionButton.Select((num < 0) ? 0 : num);
			optionButton.ItemSelected += delegate(long idx)
			{
				int num2 = (int)idx;
				RpgItemSortMode rpgItemSortMode = (num2 >= 0 && num2 < RpgItemBrowseBar.Sorts.Length) ? RpgItemBrowseBar.Sorts[num2] : RpgItemSortMode.Default;
				if (state.Sort == rpgItemSortMode)
				{
					return;
				}
				state.Sort = rpgItemSortMode;
				onChanged();
			};
			return optionButton;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000919C File Offset: 0x0000739C
		private static OptionButton MakeComboShell()
		{
			OptionButton optionButton = new OptionButton();
			optionButton.CustomMinimumSize = new Vector2(128f, 30f);
			optionButton.SizeFlagsHorizontal = 4L;
			optionButton.FitToLongestItem = false;
			optionButton.MouseFilter = 0L;
			optionButton.AddThemeFontSizeOverride("font_size", 13);
			Color bg;
			bg..ctor(0.14f, 0.14f, 0.16f, 1f);
			Color bg2;
			bg2..ctor(0.2f, 0.2f, 0.24f, 1f);
			Color border;
			border..ctor(1f, 1f, 1f, 0.12f);
			optionButton.AddThemeStyleboxOverride("normal", RpgItemBrowseBar.Flat(bg, border, 1));
			optionButton.AddThemeStyleboxOverride("hover", RpgItemBrowseBar.Flat(bg2, new Color(1f, 1f, 1f, 0.35f), 2));
			optionButton.AddThemeStyleboxOverride("pressed", RpgItemBrowseBar.Flat(bg2, new Color(1f, 1f, 1f, 0.45f), 2));
			return optionButton;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000092B8 File Offset: 0x000074B8
		private static Button MakeBaseButton(string text, bool selected, bool shrink)
		{
			Button button = new Button();
			button.Text = text;
			button.CustomMinimumSize = new Vector2(0f, 30f);
			button.SizeFlagsHorizontal = (shrink ? 4L : 3L);
			button.MouseFilter = 0L;
			button.AddThemeFontSizeOverride("font_size", 13);
			Color color;
			color..ctor(0.14f, 0.14f, 0.16f, 1f);
			Color color2;
			color2..ctor(0.2f, 0.2f, 0.24f, 1f);
			Color border = selected ? new Color(1f, 1f, 1f, 0.35f) : new Color(1f, 1f, 1f, 0.12f);
			button.AddThemeStyleboxOverride("normal", RpgItemBrowseBar.Flat(selected ? color2 : color, border, selected ? 2 : 1));
			button.AddThemeStyleboxOverride("hover", RpgItemBrowseBar.Flat(color2, new Color(1f, 1f, 1f, 0.35f), 2));
			button.AddThemeStyleboxOverride("pressed", RpgItemBrowseBar.Flat(color2, new Color(1f, 1f, 1f, 0.45f), 2));
			return button;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00009404 File Offset: 0x00007604
		private static string SortLabel(RpgItemSortMode sort)
		{
			string result;
			switch (sort)
			{
			case RpgItemSortMode.RarityDesc:
				result = SpireLoc.T("SPIRE_INVENTORY.sort_rarity", "Sort: Rarity");
				break;
			case RpgItemSortMode.LevelDesc:
				result = SpireLoc.T("SPIRE_INVENTORY.sort_level", "Sort: Level");
				break;
			case RpgItemSortMode.PowerDesc:
				result = SpireLoc.T("SPIRE_INVENTORY.sort_power", "Sort: Power");
				break;
			default:
				result = SpireLoc.T("SPIRE_INVENTORY.sort_default", "Sort: Default");
				break;
			}
			return result;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00009470 File Offset: 0x00007670
		private static string SlotLabel(RpgEquipSlot slot)
		{
			string result;
			switch (slot)
			{
			case RpgEquipSlot.Helmet:
				result = SpireLoc.T("SPIRE_INVENTORY.slot.helmet", "Helmet");
				break;
			case RpgEquipSlot.Chest:
				result = SpireLoc.T("SPIRE_INVENTORY.slot.chest", "Chest");
				break;
			case RpgEquipSlot.Gloves:
				result = SpireLoc.T("SPIRE_INVENTORY.slot.gloves", "Gloves");
				break;
			case RpgEquipSlot.Boots:
				result = SpireLoc.T("SPIRE_INVENTORY.slot.boots", "Boots");
				break;
			case RpgEquipSlot.Weapon:
				result = SpireLoc.T("SPIRE_INVENTORY.slot.weapon", "Weapon");
				break;
			case RpgEquipSlot.Accessory:
				result = SpireLoc.T("SPIRE_INVENTORY.slot.accessory", "Accessory");
				break;
			default:
				result = slot.ToString();
				break;
			}
			return result;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00009518 File Offset: 0x00007718
		private static StyleBoxFlat Flat(Color bg, Color border, int width)
		{
			return new StyleBoxFlat
			{
				BgColor = bg,
				BorderColor = border,
				BorderWidthLeft = width,
				BorderWidthTop = width,
				BorderWidthRight = width,
				BorderWidthBottom = width,
				CornerRadiusTopLeft = 6,
				CornerRadiusTopRight = 6,
				CornerRadiusBottomRight = 6,
				CornerRadiusBottomLeft = 6,
				ContentMarginLeft = 8f,
				ContentMarginRight = 8f,
				ContentMarginTop = 4f,
				ContentMarginBottom = 4f
			};
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000959C File Offset: 0x0000779C
		// Note: this type is marked as 'beforefieldinit'.
		static RpgItemBrowseBar()
		{
		}

		// Token: 0x0400004F RID: 79
		private static readonly RpgEquipSlot[] Slots = new RpgEquipSlot[]
		{
			RpgEquipSlot.Weapon,
			RpgEquipSlot.Helmet,
			RpgEquipSlot.Chest,
			RpgEquipSlot.Gloves,
			RpgEquipSlot.Boots,
			RpgEquipSlot.Accessory
		};

		// Token: 0x04000050 RID: 80
		private static readonly RpgItemRarity[] Rarities = new RpgItemRarity[]
		{
			RpgItemRarity.Common,
			RpgItemRarity.Uncommon,
			RpgItemRarity.Rare,
			RpgItemRarity.Epic,
			RpgItemRarity.Legendary
		};

		// Token: 0x04000051 RID: 81
		private static readonly RpgItemSortMode[] Sorts = new RpgItemSortMode[]
		{
			RpgItemSortMode.Default,
			RpgItemSortMode.RarityDesc,
			RpgItemSortMode.LevelDesc,
			RpgItemSortMode.PowerDesc
		};

		// Token: 0x020000A8 RID: 168
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000643 RID: 1603 RVA: 0x000202FA File Offset: 0x0001E4FA
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000644 RID: 1604 RVA: 0x00020306 File Offset: 0x0001E506
			public <>c()
			{
			}

			// Token: 0x06000645 RID: 1605 RVA: 0x0002030E File Offset: 0x0001E50E
			[NullableContext(0)]
			internal bool <Apply>b__4_0(RpgItemData x)
			{
				return x.IsScroll;
			}

			// Token: 0x06000646 RID: 1606 RVA: 0x00020316 File Offset: 0x0001E516
			[NullableContext(0)]
			internal int <Apply>b__4_3(RpgItemData x)
			{
				return (int)x.Rarity;
			}

			// Token: 0x06000647 RID: 1607 RVA: 0x0002031E File Offset: 0x0001E51E
			[NullableContext(0)]
			internal int <Apply>b__4_4(RpgItemData x)
			{
				return x.RequireLevel;
			}

			// Token: 0x06000648 RID: 1608 RVA: 0x00020326 File Offset: 0x0001E526
			[NullableContext(0)]
			internal int <Apply>b__4_5(RpgItemData x)
			{
				return x.ItemPower;
			}

			// Token: 0x06000649 RID: 1609 RVA: 0x0002032E File Offset: 0x0001E52E
			[NullableContext(0)]
			internal int <Apply>b__4_6(RpgItemData x)
			{
				return x.RequireLevel;
			}

			// Token: 0x0600064A RID: 1610 RVA: 0x00020336 File Offset: 0x0001E536
			[NullableContext(0)]
			internal int <Apply>b__4_7(RpgItemData x)
			{
				return (int)x.Rarity;
			}

			// Token: 0x0600064B RID: 1611 RVA: 0x0002033E File Offset: 0x0001E53E
			[NullableContext(0)]
			internal int <Apply>b__4_8(RpgItemData x)
			{
				return x.ItemPower;
			}

			// Token: 0x0600064C RID: 1612 RVA: 0x00020346 File Offset: 0x0001E546
			[NullableContext(0)]
			internal int <Apply>b__4_9(RpgItemData x)
			{
				return x.ItemPower;
			}

			// Token: 0x0600064D RID: 1613 RVA: 0x0002034E File Offset: 0x0001E54E
			[NullableContext(0)]
			internal int <Apply>b__4_10(RpgItemData x)
			{
				return (int)x.Rarity;
			}

			// Token: 0x0600064E RID: 1614 RVA: 0x00020356 File Offset: 0x0001E556
			[NullableContext(0)]
			internal int <Apply>b__4_11(RpgItemData x)
			{
				return x.RequireLevel;
			}

			// Token: 0x0400024D RID: 589
			[Nullable(0)]
			public static readonly RpgItemBrowseBar.<>c <>9 = new RpgItemBrowseBar.<>c();

			// Token: 0x0400024E RID: 590
			[Nullable(0)]
			public static Func<RpgItemData, bool> <>9__4_0;

			// Token: 0x0400024F RID: 591
			[Nullable(0)]
			public static Func<RpgItemData, int> <>9__4_3;

			// Token: 0x04000250 RID: 592
			[Nullable(0)]
			public static Func<RpgItemData, int> <>9__4_4;

			// Token: 0x04000251 RID: 593
			[Nullable(0)]
			public static Func<RpgItemData, int> <>9__4_5;

			// Token: 0x04000252 RID: 594
			[Nullable(0)]
			public static Func<RpgItemData, int> <>9__4_6;

			// Token: 0x04000253 RID: 595
			[Nullable(0)]
			public static Func<RpgItemData, int> <>9__4_7;

			// Token: 0x04000254 RID: 596
			[Nullable(0)]
			public static Func<RpgItemData, int> <>9__4_8;

			// Token: 0x04000255 RID: 597
			[Nullable(0)]
			public static Func<RpgItemData, int> <>9__4_9;

			// Token: 0x04000256 RID: 598
			[Nullable(0)]
			public static Func<RpgItemData, int> <>9__4_10;

			// Token: 0x04000257 RID: 599
			[Nullable(0)]
			public static Func<RpgItemData, int> <>9__4_11;
		}

		// Token: 0x020000A9 RID: 169
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x0600064F RID: 1615 RVA: 0x0002035E File Offset: 0x0001E55E
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x06000650 RID: 1616 RVA: 0x00020368 File Offset: 0x0001E568
			[NullableContext(0)]
			internal bool <Apply>b__1(RpgItemData x)
			{
				if (!x.IsScroll)
				{
					RpgEquipSlot equipSlot = x.EquipSlot;
					RpgEquipSlot? slot = this.state.Slot;
					return equipSlot == slot.GetValueOrDefault() & slot != null;
				}
				return false;
			}

			// Token: 0x06000651 RID: 1617 RVA: 0x000203A4 File Offset: 0x0001E5A4
			[NullableContext(0)]
			internal bool <Apply>b__2(RpgItemData x)
			{
				RpgItemRarity rarity = x.Rarity;
				RpgItemRarity? rarity2 = this.state.Rarity;
				return rarity == rarity2.GetValueOrDefault() & rarity2 != null;
			}

			// Token: 0x04000258 RID: 600
			[Nullable(0)]
			public RpgItemBrowseState state;
		}

		// Token: 0x020000AA RID: 170
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x06000652 RID: 1618 RVA: 0x000203D4 File Offset: 0x0001E5D4
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x06000653 RID: 1619 RVA: 0x000203DC File Offset: 0x0001E5DC
			internal void <MakeSlotTab>b__0()
			{
				if (this.selected)
				{
					return;
				}
				this.state.OtherOnly = this.otherOnly;
				this.state.Slot = (this.otherOnly ? null : this.slot);
				this.onChanged();
			}

			// Token: 0x04000259 RID: 601
			public bool selected;

			// Token: 0x0400025A RID: 602
			[Nullable(0)]
			public RpgItemBrowseState state;

			// Token: 0x0400025B RID: 603
			public bool otherOnly;

			// Token: 0x0400025C RID: 604
			public RpgEquipSlot? slot;

			// Token: 0x0400025D RID: 605
			[Nullable(0)]
			public Action onChanged;
		}

		// Token: 0x020000AB RID: 171
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x06000654 RID: 1620 RVA: 0x00020432 File Offset: 0x0001E632
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06000655 RID: 1621 RVA: 0x0002043C File Offset: 0x0001E63C
			internal void <MakeRarityCombo>b__0(long idx)
			{
				RpgItemRarity? rpgItemRarity = (idx <= 0L) ? null : new RpgItemRarity?(RpgItemBrowseBar.Rarities[(int)idx - 1]);
				RpgItemRarity? rarity = this.state.Rarity;
				RpgItemRarity? rpgItemRarity2 = rpgItemRarity;
				if (rarity.GetValueOrDefault() == rpgItemRarity2.GetValueOrDefault() & rarity != null == (rpgItemRarity2 != null))
				{
					return;
				}
				this.state.Rarity = rpgItemRarity;
				this.onChanged();
			}

			// Token: 0x0400025E RID: 606
			[Nullable(0)]
			public RpgItemBrowseState state;

			// Token: 0x0400025F RID: 607
			[Nullable(0)]
			public Action onChanged;
		}

		// Token: 0x020000AC RID: 172
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x06000656 RID: 1622 RVA: 0x000204B2 File Offset: 0x0001E6B2
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x06000657 RID: 1623 RVA: 0x000204BC File Offset: 0x0001E6BC
			internal void <MakeSortCombo>b__0(long idx)
			{
				int num = (int)idx;
				RpgItemSortMode rpgItemSortMode = (num >= 0 && num < RpgItemBrowseBar.Sorts.Length) ? RpgItemBrowseBar.Sorts[num] : RpgItemSortMode.Default;
				if (this.state.Sort == rpgItemSortMode)
				{
					return;
				}
				this.state.Sort = rpgItemSortMode;
				this.onChanged();
			}

			// Token: 0x04000260 RID: 608
			[Nullable(0)]
			public RpgItemBrowseState state;

			// Token: 0x04000261 RID: 609
			[Nullable(0)]
			public Action onChanged;
		}
	}
}
