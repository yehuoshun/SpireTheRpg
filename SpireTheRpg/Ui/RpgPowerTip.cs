using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Godot;
using MegaCrit.Sts2.addons.mega_text;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using SpireTheRpg.Core;

namespace SpireTheRpg.Ui
{
	// Token: 0x02000012 RID: 18
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgPowerTip
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x0000B524 File Offset: 0x00009724
		public static void Show(Control host, Control anchor, string affixId, int? amount = null)
		{
			RpgPowerTip.Hide(host);
			IHoverTip model = RpgPowerLookup.TipForAffix(affixId, amount);
			PowerModel power = RpgPowerLookup.ResolveByAffixId(affixId);
			string text;
			if ((text = RpgPowerTip.ReadText(model, "Title")) == null)
			{
				text = (RpgPowerTip.ReadText(model, "Header") ?? RpgPowerTip.TitleOf(power, affixId));
			}
			string text2 = text;
			string text3;
			if ((text3 = RpgPowerTip.ReadText(model, "Description")) == null)
			{
				text3 = (RpgPowerTip.ReadText(model, "Body") ?? RpgPowerTip.BodyOf(power, amount));
			}
			string text4 = text3;
			if (string.IsNullOrWhiteSpace(text2) && string.IsNullOrWhiteSpace(text4))
			{
				return;
			}
			PanelContainer tip = RpgPowerTip.Build(text2, text4, RpgPowerLookup.IconForAffix(affixId));
			tip.Name = "SpireRpgPowerTip";
			tip.Visible = false;
			tip.ZIndex = 160;
			host.AddChild(tip, false, 0L);
			tip.MoveToFront();
			Callable.From(delegate()
			{
				if (!GodotObject.IsInstanceValid(tip) || !GodotObject.IsInstanceValid(host) || !GodotObject.IsInstanceValid(anchor))
				{
					return;
				}
				tip.GlobalPosition = RpgPowerTip.PlaceBeside(host, tip, anchor.GetGlobalRect());
				tip.Visible = true;
			}).CallDeferred(Array.Empty<Variant>());
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000B641 File Offset: 0x00009841
		[NullableContext(2)]
		public static void Hide(Control host)
		{
			if (host == null || !GodotObject.IsInstanceValid(host))
			{
				return;
			}
			RpgPlayerScreen.FreeNamed(host, "SpireRpgPowerTip");
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000B65C File Offset: 0x0000985C
		private static PanelContainer Build(string title, string body, [Nullable(2)] Texture2D icon)
		{
			PanelContainer panelContainer = new PanelContainer();
			panelContainer.CustomMinimumSize = new Vector2(280f, 0f);
			panelContainer.MouseFilter = 2L;
			panelContainer.ZIndex = 64;
			panelContainer.AddThemeStyleboxOverride("panel", new StyleBoxFlat
			{
				BgColor = RpgPowerTip.TipBg,
				BorderColor = RpgPowerTip.Border,
				BorderWidthLeft = 2,
				BorderWidthTop = 2,
				BorderWidthRight = 2,
				BorderWidthBottom = 2,
				CornerRadiusTopLeft = 4,
				CornerRadiusTopRight = 4,
				CornerRadiusBottomRight = 4,
				CornerRadiusBottomLeft = 4,
				ContentMarginLeft = 12f,
				ContentMarginTop = 10f,
				ContentMarginRight = 12f,
				ContentMarginBottom = 10f
			});
			VBoxContainer vboxContainer = new VBoxContainer
			{
				MouseFilter = 2L
			};
			vboxContainer.AddThemeConstantOverride("separation", 6);
			panelContainer.AddChild(vboxContainer, false, 0L);
			HBoxContainer hboxContainer = new HBoxContainer
			{
				MouseFilter = 2L
			};
			hboxContainer.AddThemeConstantOverride("separation", 8);
			vboxContainer.AddChild(hboxContainer, false, 0L);
			if (icon != null)
			{
				hboxContainer.AddChild(new TextureRect
				{
					Texture = icon,
					CustomMinimumSize = new Vector2(22f, 22f),
					ExpandMode = 1L,
					StretchMode = 5L,
					MouseFilter = 2L
				}, false, 0L);
			}
			Label label = new Label
			{
				Text = RpgPowerTip.StripTags(title),
				SizeFlagsHorizontal = 3L,
				MouseFilter = 2L
			};
			label.AddThemeFontSizeOverride("font_size", 16);
			label.AddThemeColorOverride("font_color", RpgPowerTip.TitleColor);
			hboxContainer.AddChild(label, false, 0L);
			if (!string.IsNullOrWhiteSpace(body))
			{
				MegaRichTextLabel megaRichTextLabel = new MegaRichTextLabel
				{
					BbcodeEnabled = true,
					FitContent = true,
					AutoSizeEnabled = false,
					ScrollActive = false,
					AutowrapMode = 3L,
					CustomMinimumSize = new Vector2(256f, 0f),
					SizeFlagsHorizontal = 3L,
					MouseFilter = 2L
				};
				RpgPowerTip.ApplyMegaRichFonts(megaRichTextLabel);
				megaRichTextLabel.AddThemeFontSizeOverride("normal_font_size", 13);
				megaRichTextLabel.AddThemeFontSizeOverride("bold_font_size", 13);
				megaRichTextLabel.Text = RpgPowerTip.ToGodotBbcode(body);
				vboxContainer.AddChild(megaRichTextLabel, false, 0L);
			}
			return panelContainer;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000B8A8 File Offset: 0x00009AA8
		private static void ApplyMegaRichFonts(MegaRichTextLabel label)
		{
			Font font;
			if ((font = label.GetThemeDefaultFont()) == null)
			{
				font = (ResourceLoader.Load<Font>("res://themes/kreon_regular_glyph_space_one.tres", null, 1L) ?? ResourceLoader.Load<Font>("res://fonts/source_code_pro_medium.ttf", null, 1L));
			}
			Font font2 = font;
			if (font2 == null)
			{
				return;
			}
			label.AddThemeFontOverride("normal_font", font2);
			label.AddThemeFontOverride("bold_font", font2);
			label.AddThemeFontOverride("italics_font", font2);
			label.AddThemeFontOverride("bold_italics_font", font2);
			label.AddThemeFontOverride("mono_font", font2);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000B937 File Offset: 0x00009B37
		private static string StripTags(string text)
		{
			if (string.IsNullOrEmpty(text) || text.IndexOf('[') < 0)
			{
				return text;
			}
			return Regex.Replace(text, "\\[/?[^\\]]+\\]", "");
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000B960 File Offset: 0x00009B60
		private static string ToGodotBbcode(string text)
		{
			if (string.IsNullOrEmpty(text) || text.IndexOf('[') < 0)
			{
				return text;
			}
			return text.Replace("[gold]", "[color=#EFC851]", StringComparison.OrdinalIgnoreCase).Replace("[/gold]", "[/color]", StringComparison.OrdinalIgnoreCase).Replace("[blue]", "[color=#87CEEB]", StringComparison.OrdinalIgnoreCase).Replace("[/blue]", "[/color]", StringComparison.OrdinalIgnoreCase).Replace("[purple]", "[color=#EE82EE]", StringComparison.OrdinalIgnoreCase).Replace("[/purple]", "[/color]", StringComparison.OrdinalIgnoreCase).Replace("[red]", "[color=#FF5555]", StringComparison.OrdinalIgnoreCase).Replace("[/red]", "[/color]", StringComparison.OrdinalIgnoreCase).Replace("[green]", "[color=#7FFF00]", StringComparison.OrdinalIgnoreCase).Replace("[/green]", "[/color]", StringComparison.OrdinalIgnoreCase).Replace("[orange]", "[color=#FFA518]", StringComparison.OrdinalIgnoreCase).Replace("[/orange]", "[/color]", StringComparison.OrdinalIgnoreCase).Replace("[pink]", "[color=#FF78A0]", StringComparison.OrdinalIgnoreCase).Replace("[/pink]", "[/color]", StringComparison.OrdinalIgnoreCase).Replace("[aqua]", "[color=#2AEBBE]", StringComparison.OrdinalIgnoreCase).Replace("[/aqua]", "[/color]", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000BA84 File Offset: 0x00009C84
		private static Vector2 PlaceBeside(Control host, Control tip, Rect2 anchor)
		{
			Vector2 vector = tip.GetCombinedMinimumSize();
			if (vector.X < 8f || vector.Y < 8f)
			{
				vector = tip.Size;
			}
			Rect2 globalRect = host.GetGlobalRect();
			Vector2 vector2;
			vector2..ctor(anchor.End.X + 12f, anchor.Position.Y);
			if (vector2.X + vector.X > globalRect.End.X - 8f)
			{
				vector2.X = anchor.Position.X - vector.X - 12f;
			}
			if (vector2.Y + vector.Y > globalRect.End.Y - 8f)
			{
				vector2.Y = globalRect.End.Y - vector.Y - 8f;
			}
			if (vector2.X < globalRect.Position.X + 8f)
			{
				vector2.X = globalRect.Position.X + 8f;
			}
			if (vector2.Y < globalRect.Position.Y + 8f)
			{
				vector2.Y = globalRect.Position.Y + 8f;
			}
			return vector2;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000BBD0 File Offset: 0x00009DD0
		private static string TitleOf([Nullable(2)] PowerModel power, string affixId)
		{
			string text = RpgPowerTip.ReadText(power, "Title") ?? RpgPowerTip.ReadText(power, "Name");
			if (!string.IsNullOrWhiteSpace(text))
			{
				return text;
			}
			RpgAffixDef rpgAffixDef = RpgAffixCatalog.Get(affixId);
			if (rpgAffixDef != null && !string.IsNullOrWhiteSpace(rpgAffixDef.GameName))
			{
				return rpgAffixDef.GameName;
			}
			return SpireLoc.Item("affix." + affixId, affixId);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000BC34 File Offset: 0x00009E34
		private static string BodyOf([Nullable(2)] PowerModel power, int? amount)
		{
			string text;
			if ((text = RpgPowerTip.ReadText(power, "Description")) == null && (text = RpgPowerTip.ReadText(power, "SmartDescription")) == null)
			{
				text = (RpgPowerTip.ReadText(power, "ExtraDescription") ?? "");
			}
			string text2 = text;
			if (string.IsNullOrWhiteSpace(text2))
			{
				return "";
			}
			if (amount != null)
			{
				int valueOrDefault = amount.GetValueOrDefault();
				if (valueOrDefault > 0)
				{
					text2 = text2.Replace("{Amount}", valueOrDefault.ToString()).Replace("{0}", valueOrDefault.ToString());
				}
			}
			return text2;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000BCBC File Offset: 0x00009EBC
		[NullableContext(2)]
		private static string ReadText(object model, [Nullable(1)] string prop)
		{
			if (model == null)
			{
				return null;
			}
			string result;
			try
			{
				PropertyInfo property = model.GetType().GetProperty(prop);
				object obj = (property != null) ? property.GetValue(model) : null;
				if (obj == null)
				{
					result = null;
				}
				else
				{
					string text = obj as string;
					if (text != null)
					{
						result = text;
					}
					else
					{
						MethodInfo methodInfo = obj.GetType().GetMethod("GetFormattedText", Type.EmptyTypes) ?? obj.GetType().GetMethod("ToString", Type.EmptyTypes);
						if (methodInfo != null && methodInfo.DeclaringType != typeof(object))
						{
							object obj2 = methodInfo.Invoke(obj, null);
							string text2 = (obj2 != null) ? obj2.ToString() : null;
							if (!string.IsNullOrWhiteSpace(text2) && text2 != obj.GetType().FullName)
							{
								return text2;
							}
						}
						PropertyInfo propertyInfo = obj.GetType().GetProperty("Text") ?? obj.GetType().GetProperty("Value");
						string text3;
						if (propertyInfo == null)
						{
							text3 = null;
						}
						else
						{
							object value = propertyInfo.GetValue(obj);
							text3 = ((value != null) ? value.ToString() : null);
						}
						string text4 = text3;
						result = (string.IsNullOrWhiteSpace(text4) ? null : text4);
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000BE00 File Offset: 0x0000A000
		// Note: this type is marked as 'beforefieldinit'.
		static RpgPowerTip()
		{
		}

		// Token: 0x0400006B RID: 107
		public const string TipName = "SpireRpgPowerTip";

		// Token: 0x0400006C RID: 108
		private const float TipWidth = 280f;

		// Token: 0x0400006D RID: 109
		private static readonly Color TipBg = new Color(0.1f, 0.08f, 0.06f, 0.98f);

		// Token: 0x0400006E RID: 110
		private static readonly Color Border = new Color(0.55f, 0.42f, 0.28f, 1f);

		// Token: 0x0400006F RID: 111
		private static readonly Color TitleColor = new Color(0.95f, 0.92f, 0.86f, 1f);

		// Token: 0x020000B6 RID: 182
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x06000669 RID: 1641 RVA: 0x00020A53 File Offset: 0x0001EC53
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x0600066A RID: 1642 RVA: 0x00020A5C File Offset: 0x0001EC5C
			internal void <Show>b__0()
			{
				if (!GodotObject.IsInstanceValid(this.tip) || !GodotObject.IsInstanceValid(this.host) || !GodotObject.IsInstanceValid(this.anchor))
				{
					return;
				}
				this.tip.GlobalPosition = RpgPowerTip.PlaceBeside(this.host, this.tip, this.anchor.GetGlobalRect());
				this.tip.Visible = true;
			}

			// Token: 0x0400027D RID: 637
			[Nullable(0)]
			public PanelContainer tip;

			// Token: 0x0400027E RID: 638
			[Nullable(0)]
			public Control host;

			// Token: 0x0400027F RID: 639
			[Nullable(0)]
			public Control anchor;
		}
	}
}
