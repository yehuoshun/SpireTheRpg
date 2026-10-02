using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using SpireTheRpg.Core;

namespace SpireTheRpg.Ui
{
	// Token: 0x0200000E RID: 14
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgItemTooltip
	{
		// Token: 0x060000BB RID: 187 RVA: 0x000095EC File Offset: 0x000077EC
		public static Color RarityColor(RpgItemRarity rarity)
		{
			Color result;
			switch (rarity)
			{
			case RpgItemRarity.Common:
				result = new Color(0.75f, 0.75f, 0.75f, 1f);
				break;
			case RpgItemRarity.Uncommon:
				result = new Color(0.4f, 0.82f, 0.45f, 1f);
				break;
			case RpgItemRarity.Rare:
				result = new Color(0.4f, 0.6f, 1f, 1f);
				break;
			case RpgItemRarity.Epic:
				result = new Color(0.75f, 0.4f, 0.95f, 1f);
				break;
			case RpgItemRarity.Legendary:
				result = new Color(0.95f, 0.62f, 0.25f, 1f);
				break;
			default:
				result = Colors.White;
				break;
			}
			return result;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000096AC File Offset: 0x000078AC
		public static void Show(Control host, RpgItemData item, Rect2 slotGlobalRect)
		{
			RpgItemTooltip.Hide(host);
			RpgPowerTip.Hide(host);
			PanelContainer tip = RpgItemTooltip.Build(item, host);
			tip.Name = "SpireRpgItemTooltip";
			tip.Visible = false;
			tip.ZIndex = 128;
			host.AddChild(tip, false, 0L);
			tip.MoveToFront();
			Callable.From(delegate()
			{
				if (!GodotObject.IsInstanceValid(tip) || !GodotObject.IsInstanceValid(host))
				{
					return;
				}
				tip.GlobalPosition = RpgItemTooltip.PlaceBeside(host, tip, slotGlobalRect);
				tip.Visible = true;
			}).CallDeferred(Array.Empty<Variant>());
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00009763 File Offset: 0x00007963
		public static void Hide(Control host)
		{
			RpgPowerTip.Hide(host);
			RpgPlayerScreen.FreeNamed(host, "SpireRpgItemTooltip");
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00009778 File Offset: 0x00007978
		private static PanelContainer Build(RpgItemData item, Control tipHost)
		{
			Color color = RpgItemTooltip.RarityColor(item.Rarity);
			PanelContainer root = new PanelContainer
			{
				CustomMinimumSize = new Vector2(300f, 0f),
				SizeFlagsVertical = 0L,
				MouseFilter = 0L
			};
			root.AddThemeStyleboxOverride("panel", new StyleBoxFlat
			{
				BgColor = RpgItemTooltip.TipBg,
				BorderColor = color,
				BorderWidthLeft = 3,
				BorderWidthTop = 1,
				BorderWidthRight = 1,
				BorderWidthBottom = 1,
				CornerRadiusTopLeft = 8,
				CornerRadiusTopRight = 8,
				CornerRadiusBottomRight = 8,
				CornerRadiusBottomLeft = 8,
				ContentMarginLeft = 12f,
				ContentMarginTop = 10f,
				ContentMarginRight = 12f,
				ContentMarginBottom = 10f
			});
			VBoxContainer vboxContainer = new VBoxContainer
			{
				CustomMinimumSize = new Vector2(276f, 0f),
				MouseFilter = 2L
			};
			vboxContainer.AddThemeConstantOverride("separation", 4);
			root.AddChild(vboxContainer, false, 0L);
			vboxContainer.AddChild(RpgItemTooltip.MakeHeader(item, color), false, 0L);
			vboxContainer.AddChild(RpgItemTooltip.MakeRule(), false, 0L);
			if (item.IsScroll)
			{
				string text = RpgItemTooltip.ScrollDesc(item);
				if (!string.IsNullOrEmpty(text))
				{
					vboxContainer.AddChild(RpgItemTooltip.MakeLine(text, 13, RpgItemTooltip.Body, null), false, 0L);
				}
				vboxContainer.AddChild(RpgItemTooltip.MakeLine(SpireLoc.Item("ui.scroll_use_hint", "Right-click to use."), 11, RpgItemTooltip.Muted, null), false, 0L);
				return root;
			}
			RpgStatMod rpgStatMod = item.IsIdentified ? RpgStatMod.Parse(item.Primary) : null;
			string localizedPrimaryStat = item.LocalizedPrimaryStat;
			if (!string.IsNullOrEmpty(localizedPrimaryStat) && rpgStatMod != null)
			{
				vboxContainer.AddChild(RpgItemTooltip.MakeModRow(rpgStatMod, localizedPrimaryStat, 15, RpgItemTooltip.Body, tipHost), false, 0L);
			}
			else if (!string.IsNullOrEmpty(localizedPrimaryStat))
			{
				vboxContainer.AddChild(RpgItemTooltip.MakeLine(localizedPrimaryStat, 15, RpgItemTooltip.Body, null), false, 0L);
			}
			foreach (string text2 in item.Details)
			{
				if (!item.IsIdentified)
				{
					break;
				}
				RpgStatMod rpgStatMod2 = RpgStatMod.Parse(text2);
				string text3 = (rpgStatMod2 != null) ? rpgStatMod2.FormatDetail() : null;
				if (!string.IsNullOrEmpty(text3))
				{
					vboxContainer.AddChild(RpgItemTooltip.MakeModRow(rpgStatMod2, text3, 12, RpgItemTooltip.Muted, tipHost), false, 0L);
				}
			}
			List<RpgStatMod> list;
			if (!item.IsIdentified)
			{
				list = new List<RpgStatMod>();
			}
			else
			{
				IEnumerable<string> source = item.EffectiveAbilityLines();
				Func<string, RpgStatMod> selector;
				if ((selector = RpgItemTooltip.<>O.<0>__Parse) == null)
				{
					selector = (RpgItemTooltip.<>O.<0>__Parse = new Func<string, RpgStatMod>(RpgStatMod.Parse));
				}
				list = (from x in source.Select(selector)
				where x != null
				select x).Cast<RpgStatMod>().ToList<RpgStatMod>();
			}
			List<RpgStatMod> list2 = list;
			string localizedLegendaryText = item.LocalizedLegendaryText;
			if (item.LocalizedAffixes().ToList<string>().Count > 0 || !string.IsNullOrEmpty(localizedLegendaryText) || (!item.IsIdentified && item.IsRarePlus))
			{
				vboxContainer.AddChild(RpgItemTooltip.MakeRule(), false, 0L);
			}
			if (!item.IsIdentified && item.IsRarePlus)
			{
				using (IEnumerator<string> enumerator2 = item.LocalizedAffixes().GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						string text4 = enumerator2.Current;
						vboxContainer.AddChild(RpgItemTooltip.MakeLine(text4, 13, RpgItemTooltip.Muted, null), false, 0L);
					}
					goto IL_46A;
				}
			}
			foreach (RpgStatMod rpgStatMod3 in list2)
			{
				vboxContainer.AddChild(RpgItemTooltip.MakeModRow(rpgStatMod3, rpgStatMod3.FormatAffix(), 13, rpgStatMod3.IsDebuff ? RpgItemTooltip.Debuff : RpgItemTooltip.Body, tipHost), false, 0L);
			}
			foreach (string text5 in item.Curses)
			{
				vboxContainer.AddChild(RpgItemTooltip.MakeLine(SpireLoc.ItemFormat("ui.curse_line", "Curse: {0}", new object[]
				{
					SpireLoc.Item("curse." + text5, text5)
				}), 13, RpgItemTooltip.Debuff, null), false, 0L);
			}
			IL_46A:
			if (!string.IsNullOrEmpty(localizedLegendaryText))
			{
				Color color2 = (item.EquipSlot == RpgEquipSlot.Accessory && !RpgAccessoryGates.IsActive(item, null)) ? RpgItemTooltip.Muted : RpgItemTooltip.Roll;
				vboxContainer.AddChild(RpgItemTooltip.MakeLine(localizedLegendaryText, 13, color2, null), false, 0L);
			}
			foreach (string text6 in RpgAccessoryGates.RequirementLines(item))
			{
				vboxContainer.AddChild(RpgItemTooltip.MakeLine(text6, 11, RpgItemTooltip.Muted, null), false, 0L);
			}
			if (item.EquipSlot == RpgEquipSlot.Accessory && !RpgAccessoryGates.IsActive(item, null))
			{
				vboxContainer.AddChild(RpgItemTooltip.MakeLine(SpireLoc.Item("ui.accessory_inactive", "(Inactive — requirements not met)"), 11, new Color(0.92f, 0.55f, 0.35f, 1f), null), false, 0L);
			}
			if (!item.IsIdentified)
			{
				vboxContainer.AddChild(RpgItemTooltip.MakeRule(), false, 0L);
				vboxContainer.AddChild(RpgItemTooltip.MakeLine(SpireLoc.Item("ui.identify_hint", "Identify to reveal stats."), 11, RpgItemTooltip.Muted, null), false, 0L);
			}
			vboxContainer.AddChild(RpgItemTooltip.MakeRule(), false, 0L);
			if (!SpireTheRpgConfig.EnableDevCheats || !SpireTheRpgConfig.IgnoreLevelRequirement)
			{
				Color color3 = RpgActiveLoadout.MeetsLevelRequirement(item) ? RpgItemTooltip.Muted : new Color(0.92f, 0.35f, 0.35f, 1f);
				vboxContainer.AddChild(RpgItemTooltip.MakeLine(item.LocalizedRequireLevel, 11, color3, null), false, 0L);
			}
			root.MouseExited += delegate()
			{
				RpgItemTooltip.HideFromTree(root);
			};
			return root;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00009DE4 File Offset: 0x00007FE4
		private static void HideFromTree(Control tip)
		{
			try
			{
				Control control = tip.GetParent() as Control;
				if (control != null)
				{
					RpgItemTooltip.Hide(control);
				}
			}
			catch
			{
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00009E1C File Offset: 0x0000801C
		private static Control MakeModRow(RpgStatMod mod, string text, int size, Color color, Control tipHost)
		{
			HBoxContainer hboxContainer = new HBoxContainer();
			hboxContainer.MouseFilter = 0L;
			hboxContainer.SizeFlagsHorizontal = 3L;
			hboxContainer.AddThemeConstantOverride("separation", 6);
			TextureRect textureRect = new TextureRect
			{
				CustomMinimumSize = new Vector2(18f, 18f),
				ExpandMode = 1L,
				StretchMode = 5L,
				MouseFilter = 2L
			};
			textureRect.Texture = RpgPowerLookup.IconForAffix(mod.Id);
			hboxContainer.AddChild(textureRect, false, 0L);
			hboxContainer.AddChild(RpgItemTooltip.MakeLine(text, size, color, new float?(244f)), false, 0L);
			int num = (int)Math.Round(Math.Abs(mod.Value));
			RpgPowerLookup.BindPowerHover(hboxContainer, mod.Id, (num > 0) ? new int?(num) : null, tipHost);
			return hboxContainer;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00009EF0 File Offset: 0x000080F0
		private static Control MakeHeader(RpgItemData item, Color rarity)
		{
			HBoxContainer hboxContainer = new HBoxContainer
			{
				MouseFilter = 2L
			};
			hboxContainer.AddThemeConstantOverride("separation", 10);
			VBoxContainer vboxContainer = new VBoxContainer
			{
				SizeFlagsHorizontal = 3L,
				MouseFilter = 2L
			};
			vboxContainer.AddThemeConstantOverride("separation", 2);
			vboxContainer.AddChild(RpgItemTooltip.MakeLine(item.DisplayName, 17, rarity, new float?(200f)), false, 0L);
			vboxContainer.AddChild(RpgItemTooltip.MakeLine(item.LocalizedTypeLine, 13, rarity, new float?(200f)), false, 0L);
			if (!item.IsScroll)
			{
				vboxContainer.AddChild(RpgItemTooltip.MakeLine(item.LocalizedItemPower, 12, RpgItemTooltip.Muted, new float?(200f)), false, 0L);
			}
			hboxContainer.AddChild(vboxContainer, false, 0L);
			Texture2D texture2D = SpireAssets.ItemIcon(item.IconPath);
			if (texture2D != null)
			{
				Panel panel = new Panel
				{
					CustomMinimumSize = new Vector2(56f, 56f),
					MouseFilter = 2L
				};
				panel.AddThemeStyleboxOverride("panel", new StyleBoxFlat
				{
					BgColor = new Color(0.14f, 0.14f, 0.16f, 1f),
					BorderColor = rarity,
					BorderWidthLeft = 1,
					BorderWidthTop = 1,
					BorderWidthRight = 1,
					BorderWidthBottom = 1,
					CornerRadiusTopLeft = 6,
					CornerRadiusTopRight = 6,
					CornerRadiusBottomRight = 6,
					CornerRadiusBottomLeft = 6
				});
				TextureRect textureRect = new TextureRect
				{
					Texture = texture2D,
					ExpandMode = 1L,
					StretchMode = 5L,
					MouseFilter = 2L
				};
				textureRect.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
				textureRect.OffsetLeft = 3f;
				textureRect.OffsetTop = 3f;
				textureRect.OffsetRight = -3f;
				textureRect.OffsetBottom = -3f;
				panel.AddChild(textureRect, false, 0L);
				hboxContainer.AddChild(panel, false, 0L);
			}
			return hboxContainer;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000A0E0 File Offset: 0x000082E0
		private static string ScrollDesc(RpgItemData item)
		{
			string a = item.Id.ToLowerInvariant();
			string result;
			if (!(a == "scroll_gold"))
			{
				if (!(a == "scroll_magic"))
				{
					result = SpireLoc.Item(item.Id + ".desc", "");
				}
				else
				{
					result = SpireLoc.ItemFormat("scroll_magic.desc", "Gain {0} gold.", new object[]
					{
						RpgEffectiveConfig.ScrollGoldAmount
					});
				}
			}
			else
			{
				result = SpireLoc.ItemFormat("scroll_gold.desc", "Gain {0} gold.", new object[]
				{
					RpgEffectiveConfig.ScrollGoldAmount
				});
			}
			return result;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000A17C File Offset: 0x0000837C
		private static Label MakeLine(string text, int size, Color color, float? width = null)
		{
			Label label = new Label();
			label.Text = text;
			label.AutowrapMode = 3L;
			label.CustomMinimumSize = new Vector2(width.GetValueOrDefault(272f), 0f);
			label.MouseFilter = 2L;
			label.AddThemeFontSizeOverride("font_size", size);
			label.AddThemeColorOverride("font_color", color);
			return label;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000A1E3 File Offset: 0x000083E3
		private static ColorRect MakeRule()
		{
			return new ColorRect
			{
				CustomMinimumSize = new Vector2(0f, 1f),
				Color = RpgItemTooltip.Divider,
				MouseFilter = 2L
			};
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000A214 File Offset: 0x00008414
		private static Vector2 PlaceBeside(Control host, Control tip, Rect2 slotRect)
		{
			Vector2 combinedMinimumSize = tip.GetCombinedMinimumSize();
			if (combinedMinimumSize.X < 8f)
			{
				combinedMinimumSize.X = 300f;
			}
			if (combinedMinimumSize.Y < 8f)
			{
				combinedMinimumSize.Y = 120f;
			}
			Rect2 globalRect = host.GetGlobalRect();
			Vector2 vector = new Vector2(slotRect.End.X + 12f, slotRect.Position.Y);
			Vector2 vector2;
			vector2..ctor(slotRect.Position.X - 12f - combinedMinimumSize.X, slotRect.Position.Y);
			Vector2 vector3 = vector;
			if (vector.X + combinedMinimumSize.X > globalRect.End.X - 10f)
			{
				vector3 = vector2;
			}
			if (vector3.X < globalRect.Position.X + 10f)
			{
				vector3.X = globalRect.Position.X + 10f;
			}
			float num = globalRect.Position.Y + 10f;
			float num2 = globalRect.End.Y - combinedMinimumSize.Y - 10f;
			vector3.Y = ((num2 < num) ? num : Mathf.Clamp(vector3.Y, num, num2));
			return vector3;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000A358 File Offset: 0x00008558
		// Note: this type is marked as 'beforefieldinit'.
		static RpgItemTooltip()
		{
		}

		// Token: 0x04000052 RID: 82
		public const string OverlayTipName = "SpireRpgItemTooltip";

		// Token: 0x04000053 RID: 83
		private const string TooltipName = "SpireRpgItemTooltip";

		// Token: 0x04000054 RID: 84
		private const float TipWidth = 300f;

		// Token: 0x04000055 RID: 85
		private static readonly Color TipBg = new Color(0.08f, 0.08f, 0.09f, 0.98f);

		// Token: 0x04000056 RID: 86
		private static readonly Color Divider = new Color(1f, 1f, 1f, 0.1f);

		// Token: 0x04000057 RID: 87
		private static readonly Color Body = new Color(0.9f, 0.9f, 0.9f, 1f);

		// Token: 0x04000058 RID: 88
		private static readonly Color Muted = new Color(0.62f, 0.62f, 0.64f, 1f);

		// Token: 0x04000059 RID: 89
		private static readonly Color DetailDesc = new Color(0.52f, 0.52f, 0.54f, 0.92f);

		// Token: 0x0400005A RID: 90
		private static readonly Color Roll = new Color(0.75f, 0.8f, 1f, 1f);

		// Token: 0x0400005B RID: 91
		private static readonly Color Debuff = new Color(0.92f, 0.42f, 0.42f, 1f);

		// Token: 0x020000AD RID: 173
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000262 RID: 610
			[Nullable(0)]
			public static Func<string, RpgStatMod> <0>__Parse;
		}

		// Token: 0x020000AE RID: 174
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000658 RID: 1624 RVA: 0x0002050B File Offset: 0x0001E70B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000659 RID: 1625 RVA: 0x00020517 File Offset: 0x0001E717
			public <>c()
			{
			}

			// Token: 0x0600065A RID: 1626 RVA: 0x0002051F File Offset: 0x0001E71F
			[NullableContext(0)]
			internal bool <Build>b__13_0(RpgStatMod x)
			{
				return x != null;
			}

			// Token: 0x04000263 RID: 611
			[Nullable(0)]
			public static readonly RpgItemTooltip.<>c <>9 = new RpgItemTooltip.<>c();

			// Token: 0x04000264 RID: 612
			[Nullable(0)]
			public static Func<RpgStatMod, bool> <>9__13_0;
		}

		// Token: 0x020000AF RID: 175
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0
		{
			// Token: 0x0600065B RID: 1627 RVA: 0x00020525 File Offset: 0x0001E725
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x0600065C RID: 1628 RVA: 0x00020530 File Offset: 0x0001E730
			internal void <Show>b__0()
			{
				if (!GodotObject.IsInstanceValid(this.tip) || !GodotObject.IsInstanceValid(this.host))
				{
					return;
				}
				this.tip.GlobalPosition = RpgItemTooltip.PlaceBeside(this.host, this.tip, this.slotGlobalRect);
				this.tip.Visible = true;
			}

			// Token: 0x04000265 RID: 613
			[Nullable(0)]
			public PanelContainer tip;

			// Token: 0x04000266 RID: 614
			[Nullable(0)]
			public Control host;

			// Token: 0x04000267 RID: 615
			public Rect2 slotGlobalRect;
		}

		// Token: 0x020000B0 RID: 176
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0
		{
			// Token: 0x0600065D RID: 1629 RVA: 0x00020586 File Offset: 0x0001E786
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x0600065E RID: 1630 RVA: 0x0002058E File Offset: 0x0001E78E
			internal void <Build>b__1()
			{
				RpgItemTooltip.HideFromTree(this.root);
			}

			// Token: 0x04000268 RID: 616
			[Nullable(0)]
			public PanelContainer root;
		}
	}
}
