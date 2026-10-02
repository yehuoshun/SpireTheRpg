using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Nodes.Screens.MainMenu;
using SpireTheRpg.Core;

namespace SpireTheRpg.Ui
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgInventoryScreen
	{
		// Token: 0x06000085 RID: 133 RVA: 0x00006631 File Offset: 0x00004831
		public static bool IsOpenOn(Control host)
		{
			return GodotObject.IsInstanceValid(host) && host.GetNodeOrNull("SpireRpgInventoryOverlay") != null;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00006650 File Offset: 0x00004850
		public static void Show(Control host, bool preserveDeleteMode = false)
		{
			SpireLoc.ForceInject();
			RpgActiveLoadout.UnequipUnidentified(null);
			RpgItemRoller.EnsureUniqueInstanceIds(RpgActiveLoadout.AllItems());
			bool deleteMode = preserveDeleteMode && RpgInventoryScreen._deleteMode;
			RpgItemBrowseState rpgItemBrowseState;
			if (!preserveDeleteMode)
			{
				rpgItemBrowseState = null;
			}
			else
			{
				RpgItemBrowseState rpgItemBrowseState2 = new RpgItemBrowseState();
				rpgItemBrowseState2.Slot = RpgInventoryScreen.BagBrowse.Slot;
				rpgItemBrowseState2.OtherOnly = RpgInventoryScreen.BagBrowse.OtherOnly;
				rpgItemBrowseState2.Rarity = RpgInventoryScreen.BagBrowse.Rarity;
				rpgItemBrowseState = rpgItemBrowseState2;
				rpgItemBrowseState2.Sort = RpgInventoryScreen.BagBrowse.Sort;
			}
			RpgItemBrowseState rpgItemBrowseState3 = rpgItemBrowseState;
			NCompendiumSubmenu ncompendiumSubmenu = host as NCompendiumSubmenu;
			if (ncompendiumSubmenu != null)
			{
				RpgPlayerScreen.Hide(ncompendiumSubmenu);
				RpgAbilityScreen.Hide(ncompendiumSubmenu);
				RpgWipScreen.Hide(ncompendiumSubmenu);
				RpgCodexScreen.Hide(ncompendiumSubmenu);
			}
			RpgInventoryScreen.Hide(host);
			RpgInventoryScreen._deleteMode = deleteMode;
			if (rpgItemBrowseState3 != null)
			{
				RpgInventoryScreen.BagBrowse.Slot = rpgItemBrowseState3.Slot;
				RpgInventoryScreen.BagBrowse.OtherOnly = rpgItemBrowseState3.OtherOnly;
				RpgInventoryScreen.BagBrowse.Rarity = rpgItemBrowseState3.Rarity;
				RpgInventoryScreen.BagBrowse.Sort = rpgItemBrowseState3.Sort;
			}
			else
			{
				RpgInventoryScreen.BagBrowse.Slot = null;
				RpgInventoryScreen.BagBrowse.OtherOnly = false;
				RpgInventoryScreen.BagBrowse.Rarity = null;
				RpgInventoryScreen.BagBrowse.Sort = RpgItemSortMode.Default;
			}
			RpgInventoryScreen._host = host;
			RpgPlayerProfile profile = RpgPlayerProfile.Current();
			ColorRect colorRect = RpgPlayerScreen.MakeDim("SpireRpgInventoryOverlay");
			Control control = RpgInventoryScreen.MakePanel(new Vector2(1180f, 780f));
			colorRect.AddChild(control, false, 0L);
			VBoxContainer vboxContainer = new VBoxContainer
			{
				MouseFilter = 2L
			};
			vboxContainer.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			vboxContainer.OffsetLeft = 40f;
			vboxContainer.OffsetRight = -40f;
			vboxContainer.OffsetTop = 28f;
			vboxContainer.OffsetBottom = -28f;
			vboxContainer.AddThemeConstantOverride("separation", 16);
			control.AddChild(vboxContainer, false, 0L);
			vboxContainer.AddChild(RpgInventoryScreen.MakeHeader(profile), false, 0L);
			HBoxContainer hboxContainer = new HBoxContainer
			{
				SizeFlagsVertical = 3L,
				MouseFilter = 2L
			};
			hboxContainer.AddThemeConstantOverride("separation", 24);
			hboxContainer.AddChild(RpgInventoryScreen.MakeEquipmentArea(colorRect), false, 0L);
			hboxContainer.AddChild(RpgInventoryScreen.MakeInventoryArea(colorRect), false, 0L);
			vboxContainer.AddChild(hboxContainer, false, 0L);
			vboxContainer.AddChild(RpgInventoryScreen.MakeFooter(host, colorRect), false, 0L);
			host.AddChild(colorRect, false, 0L);
			colorRect.MoveToFront();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000068A4 File Offset: 0x00004AA4
		[NullableContext(2)]
		public static void Hide(Control host = null)
		{
			Control control = host ?? RpgInventoryScreen._host;
			if (control != null && GodotObject.IsInstanceValid(control))
			{
				RpgPlayerScreen.FreeNamed(control, "SpireRpgInventoryOverlay");
			}
			RpgInventoryScreen._deleteMode = false;
			RpgInventoryScreen.BagBrowse.Slot = null;
			RpgInventoryScreen.BagBrowse.OtherOnly = false;
			RpgInventoryScreen.BagBrowse.Rarity = null;
			RpgInventoryScreen.BagBrowse.Sort = RpgItemSortMode.Default;
			if (host == null || host == RpgInventoryScreen._host)
			{
				RpgInventoryScreen._host = null;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00006928 File Offset: 0x00004B28
		private static void Refresh()
		{
			if (RpgInventoryScreen._host == null || !GodotObject.IsInstanceValid(RpgInventoryScreen._host))
			{
				return;
			}
			Control host = RpgInventoryScreen._host;
			Callable.From(delegate()
			{
				if (GodotObject.IsInstanceValid(host))
				{
					RpgInventoryScreen.Show(host, true);
				}
			}).CallDeferred(Array.Empty<Variant>());
			RpgInRunInventory.NotifyRelicChanged();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00006980 File Offset: 0x00004B80
		private static Control MakePanel(Vector2 size)
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
			Panel panel = new Panel
			{
				MouseFilter = 2L
			};
			panel.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			panel.AddThemeStyleboxOverride("panel", RpgInventoryScreen.Flat(RpgInventoryScreen.PanelBg, RpgInventoryScreen.SlotBorder, 0f, 16));
			control.AddChild(panel, false, 0L);
			return control;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00006A58 File Offset: 0x00004C58
		private static Control MakeHeader(RpgPlayerProfile profile)
		{
			HBoxContainer hboxContainer = new HBoxContainer();
			hboxContainer.MouseFilter = 2L;
			hboxContainer.AddThemeConstantOverride("separation", 24);
			VBoxContainer vboxContainer = new VBoxContainer
			{
				SizeFlagsHorizontal = 3L,
				MouseFilter = 2L
			};
			vboxContainer.AddThemeConstantOverride("separation", 8);
			Label label = new Label
			{
				Text = profile.Name,
				MouseFilter = 2L
			};
			label.AddThemeFontSizeOverride("font_size", 26);
			vboxContainer.AddChild(label, false, 0L);
			ProgressBar progressBar = new ProgressBar
			{
				MinValue = 0.0,
				MaxValue = (double)Math.Max(1, profile.XpToNext),
				Value = (double)profile.Xp,
				CustomMinimumSize = new Vector2(0f, 10f),
				SizeFlagsHorizontal = 3L,
				ShowPercentage = false,
				MouseFilter = 2L
			};
			vboxContainer.AddChild(progressBar, false, 0L);
			hboxContainer.AddChild(vboxContainer, false, 0L);
			VBoxContainer vboxContainer2 = new VBoxContainer
			{
				CustomMinimumSize = new Vector2(120f, 0f),
				MouseFilter = 2L
			};
			Label label2 = new Label();
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Lv");
			defaultInterpolatedStringHandler.AppendFormatted<int>(profile.Level);
			label2.Text = defaultInterpolatedStringHandler.ToStringAndClear();
			label2.HorizontalAlignment = 2L;
			label2.MouseFilter = 2L;
			Label label3 = label2;
			label3.AddThemeFontSizeOverride("font_size", 26);
			Label label4 = new Label();
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted<int>(profile.Xp);
			defaultInterpolatedStringHandler.AppendLiteral("/");
			defaultInterpolatedStringHandler.AppendFormatted<int>(profile.XpToNext);
			label4.Text = defaultInterpolatedStringHandler.ToStringAndClear();
			label4.HorizontalAlignment = 2L;
			label4.MouseFilter = 2L;
			Label label5 = label4;
			label5.AddThemeFontSizeOverride("font_size", 16);
			label5.Modulate = new Color(1f, 1f, 1f, 0.55f);
			vboxContainer2.AddChild(label3, false, 0L);
			vboxContainer2.AddChild(label5, false, 0L);
			hboxContainer.AddChild(vboxContainer2, false, 0L);
			return hboxContainer;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00006C78 File Offset: 0x00004E78
		private static Control MakeEquipmentArea(Control tooltipHost)
		{
			VBoxContainer vboxContainer = new VBoxContainer
			{
				CustomMinimumSize = new Vector2(320f, 0f),
				SizeFlagsVertical = 3L,
				MouseFilter = 2L
			};
			vboxContainer.AddThemeConstantOverride("separation", 10);
			Label label = new Label
			{
				Text = SpireLoc.T("SPIRE_INVENTORY.equipment", "Equipment"),
				MouseFilter = 2L
			};
			label.AddThemeFontSizeOverride("font_size", 18);
			label.Modulate = new Color(1f, 1f, 1f, 0.55f);
			vboxContainer.AddChild(label, false, 0L);
			HBoxContainer hboxContainer = new HBoxContainer
			{
				Alignment = 1L,
				MouseFilter = 2L
			};
			hboxContainer.AddThemeConstantOverride("separation", 10);
			hboxContainer.AddChild(RpgInventoryScreen.MakeEquipColumn(new RpgEquipSlot[]
			{
				RpgEquipSlot.Helmet,
				RpgEquipSlot.Chest,
				RpgEquipSlot.Boots
			}, tooltipHost), false, 0L);
			hboxContainer.AddChild(RpgInventoryScreen.MakeEquipColumn(new RpgEquipSlot[]
			{
				RpgEquipSlot.Weapon,
				RpgEquipSlot.Gloves,
				RpgEquipSlot.Accessory
			}, tooltipHost), false, 0L);
			vboxContainer.AddChild(hboxContainer, false, 0L);
			Label label2 = new Label
			{
				Text = SpireLoc.T("SPIRE_INVENTORY.effects", "Bonuses"),
				MouseFilter = 2L
			};
			label2.AddThemeFontSizeOverride("font_size", 16);
			label2.Modulate = new Color(1f, 1f, 1f, 0.55f);
			vboxContainer.AddChild(label2, false, 0L);
			vboxContainer.AddChild(RpgInventoryScreen.MakeEquippedEffectsPanel(tooltipHost), false, 0L);
			return vboxContainer;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00006E00 File Offset: 0x00005000
		private static Control MakeEquippedEffectsPanel(Control tipHost)
		{
			Panel panel = new Panel
			{
				SizeFlagsVertical = 3L,
				SizeFlagsHorizontal = 3L,
				CustomMinimumSize = new Vector2(0f, 180f),
				MouseFilter = 0L
			};
			panel.AddThemeStyleboxOverride("panel", RpgInventoryScreen.Flat(RpgInventoryScreen.CharacterIdle, RpgInventoryScreen.SlotBorder, 1f, 10));
			ScrollContainer scrollContainer = new ScrollContainer
			{
				HorizontalScrollMode = 0L,
				VerticalScrollMode = 1L,
				MouseFilter = 0L
			};
			scrollContainer.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			scrollContainer.OffsetLeft = 10f;
			scrollContainer.OffsetTop = 10f;
			scrollContainer.OffsetRight = -10f;
			scrollContainer.OffsetBottom = -10f;
			panel.AddChild(scrollContainer, false, 0L);
			VBoxContainer vboxContainer = new VBoxContainer
			{
				SizeFlagsHorizontal = 3L,
				MouseFilter = 2L
			};
			vboxContainer.AddThemeConstantOverride("separation", 8);
			scrollContainer.AddChild(vboxContainer, false, 0L);
			List<RpgInventoryScreen.EffectLine> list = RpgInventoryScreen.BuildEquippedEffectLines();
			if (list.Count == 0)
			{
				Label label = new Label
				{
					Text = SpireLoc.T("SPIRE_INVENTORY.effects_empty", "No equipment bonuses"),
					AutowrapMode = 3L,
					MouseFilter = 2L
				};
				label.AddThemeFontSizeOverride("font_size", 13);
				label.Modulate = new Color(1f, 1f, 1f, 0.4f);
				vboxContainer.AddChild(label, false, 0L);
				return panel;
			}
			foreach (RpgInventoryScreen.EffectLine line in list)
			{
				vboxContainer.AddChild(RpgInventoryScreen.MakeEffectRow(line, tipHost), false, 0L);
			}
			return panel;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00006FC0 File Offset: 0x000051C0
		private static Control MakeEffectRow(RpgInventoryScreen.EffectLine line, Control tipHost)
		{
			PanelContainer panelContainer = new PanelContainer
			{
				SizeFlagsHorizontal = 3L,
				MouseFilter = 0L
			};
			panelContainer.AddThemeStyleboxOverride("panel", new StyleBoxFlat
			{
				BgColor = new Color(1f, 1f, 1f, 0.04f),
				ContentMarginLeft = 8f,
				ContentMarginRight = 10f,
				ContentMarginTop = 5f,
				ContentMarginBottom = 5f,
				CornerRadiusTopLeft = 4,
				CornerRadiusTopRight = 4,
				CornerRadiusBottomRight = 4,
				CornerRadiusBottomLeft = 4
			});
			HBoxContainer hboxContainer = new HBoxContainer
			{
				SizeFlagsHorizontal = 3L,
				CustomMinimumSize = new Vector2(0f, 26f),
				MouseFilter = 2L
			};
			hboxContainer.AddThemeConstantOverride("separation", 10);
			panelContainer.AddChild(hboxContainer, false, 0L);
			TextureRect textureRect = new TextureRect
			{
				CustomMinimumSize = new Vector2(22f, 22f),
				ExpandMode = 1L,
				StretchMode = 5L,
				MouseFilter = 2L
			};
			Texture2D texture2D = RpgPowerLookup.IconForAffix(line.AffixId);
			if (texture2D != null)
			{
				textureRect.Texture = texture2D;
			}
			else if (!string.IsNullOrEmpty(line.FallbackIconPath))
			{
				try
				{
					textureRect.Texture = ResourceLoader.Load<Texture2D>(line.FallbackIconPath, null, 1L);
				}
				catch
				{
				}
			}
			hboxContainer.AddChild(textureRect, false, 0L);
			Label label = new Label
			{
				Text = line.Name,
				SizeFlagsHorizontal = 3L,
				VerticalAlignment = 1L,
				ClipText = true,
				MouseFilter = 2L
			};
			label.AddThemeFontSizeOverride("font_size", 14);
			label.AddThemeColorOverride("font_color", new Color(0.9f, 0.9f, 0.92f, 1f));
			hboxContainer.AddChild(label, false, 0L);
			if (!string.IsNullOrEmpty(line.ValueText))
			{
				Label label2 = new Label
				{
					Text = line.ValueText,
					HorizontalAlignment = 2L,
					VerticalAlignment = 1L,
					CustomMinimumSize = new Vector2(56f, 0f),
					MouseFilter = 2L
				};
				label2.AddThemeFontSizeOverride("font_size", 16);
				label2.AddThemeColorOverride("font_color", line.Color);
				hboxContainer.AddChild(label2, false, 0L);
			}
			if (!string.IsNullOrWhiteSpace(line.AffixId))
			{
				RpgPowerLookup.BindPowerHover(panelContainer, line.AffixId, new int?(line.Amount), tipHost);
			}
			return panelContainer;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00007260 File Offset: 0x00005460
		private static List<RpgInventoryScreen.EffectLine> BuildEquippedEffectLines()
		{
			Color color;
			color..ctor(0.55f, 0.92f, 0.7f, 1f);
			Color color2;
			color2..ctor(0.72f, 0.72f, 0.74f, 1f);
			Color color3;
			color3..ctor(0.75f, 0.8f, 1f, 1f);
			Color color4;
			color4..ctor(1f, 0.48f, 0.48f, 1f);
			List<RpgInventoryScreen.EffectLine> list = new List<RpgInventoryScreen.EffectLine>();
			Dictionary<string, RpgStatMod> dictionary = new Dictionary<string, RpgStatMod>(StringComparer.OrdinalIgnoreCase);
			List<string> list2 = new List<string>();
			List<string> list3 = new List<string>();
			foreach (RpgItemData rpgItemData in RpgActiveLoadout.EquippedItems())
			{
				if (rpgItemData.IsIdentified && RpgAccessoryGates.ContributesStats(rpgItemData, null))
				{
					RpgInventoryScreen.MergeStat(dictionary, rpgItemData.Primary);
					foreach (string shorthand in rpgItemData.Details)
					{
						RpgInventoryScreen.MergeStat(dictionary, shorthand);
					}
					foreach (string shorthand2 in rpgItemData.Ability)
					{
						RpgInventoryScreen.MergeStat(dictionary, shorthand2);
					}
					string localizedLegendaryText = rpgItemData.LocalizedLegendaryText;
					if (!string.IsNullOrEmpty(localizedLegendaryText))
					{
						list2.Add(localizedLegendaryText);
					}
					foreach (string text in rpgItemData.Curses)
					{
						if (!string.IsNullOrWhiteSpace(text))
						{
							list3.Add(text);
						}
					}
				}
			}
			foreach (RpgStatMod rpgStatMod in dictionary.Values.OrderBy((RpgStatMod m) => m.Id, StringComparer.OrdinalIgnoreCase))
			{
				bool flag = rpgStatMod.Id == "damage_reduction";
				Color color5 = flag ? color2 : (rpgStatMod.IsDebuff ? color4 : color);
				string valueText = flag ? (rpgStatMod.Percent ? (rpgStatMod.FormatValue() + "%") : rpgStatMod.FormatValue()) : (rpgStatMod.IsDebuff ? (rpgStatMod.Percent ? ("-" + rpgStatMod.FormatValue() + "%") : ("-" + rpgStatMod.FormatValue())) : (rpgStatMod.Percent ? ("+" + rpgStatMod.FormatValue() + "%") : ("+" + rpgStatMod.FormatValue())));
				int amount = (int)Math.Round(Math.Abs(rpgStatMod.Value));
				string affixId = string.Equals(rpgStatMod.Id, "armor", StringComparison.OrdinalIgnoreCase) ? "plating" : rpgStatMod.Id;
				list.Add(new RpgInventoryScreen.EffectLine(rpgStatMod.LocalizedName, valueText, color5, affixId, amount, RpgInventoryScreen.StatIconPath(rpgStatMod.Id)));
			}
			foreach (string name in list2)
			{
				list.Add(new RpgInventoryScreen.EffectLine(name, "", color3, "", 0, "res://images/atlases/stats_screen_atlas.sprites/stats_chest.tres"));
			}
			foreach (string text2 in list3.Distinct(StringComparer.OrdinalIgnoreCase))
			{
				string text3 = SpireLoc.Item("curse." + text2, text2);
				list.Add(new RpgInventoryScreen.EffectLine(SpireLoc.ItemFormat("ui.curse_line", "Curse: {0}", new object[]
				{
					text3
				}), "", color4, text2, 1, null));
			}
			return list;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000771C File Offset: 0x0000591C
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

		// Token: 0x06000090 RID: 144 RVA: 0x00007968 File Offset: 0x00005B68
		private static void MergeStat(Dictionary<string, RpgStatMod> merged, [Nullable(2)] string shorthand)
		{
			RpgStatMod rpgStatMod = RpgStatMod.Parse(shorthand);
			if (rpgStatMod == null || string.IsNullOrWhiteSpace(rpgStatMod.Id))
			{
				return;
			}
			string key = rpgStatMod.Id + "|" + (rpgStatMod.Percent ? "p" : "f");
			RpgStatMod rpgStatMod2;
			if (merged.TryGetValue(key, out rpgStatMod2))
			{
				merged[key] = new RpgStatMod
				{
					Id = rpgStatMod2.Id,
					Value = rpgStatMod2.Value + rpgStatMod.Value,
					Percent = rpgStatMod2.Percent
				};
				return;
			}
			merged[key] = rpgStatMod;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00007A00 File Offset: 0x00005C00
		private static Control MakeInventoryArea(Control tooltipHost)
		{
			VBoxContainer vboxContainer = new VBoxContainer
			{
				SizeFlagsHorizontal = 3L,
				SizeFlagsVertical = 3L,
				MouseFilter = 2L
			};
			vboxContainer.AddThemeConstantOverride("separation", 10);
			HBoxContainer hboxContainer = new HBoxContainer
			{
				SizeFlagsHorizontal = 3L,
				MouseFilter = 2L
			};
			hboxContainer.AddThemeConstantOverride("separation", 12);
			Label label = new Label
			{
				Text = SpireLoc.T("SPIRE_INVENTORY.title", "Inventory"),
				SizeFlagsHorizontal = 3L,
				MouseFilter = 2L
			};
			label.AddThemeFontSizeOverride("font_size", 18);
			label.Modulate = new Color(1f, 1f, 1f, 0.55f);
			hboxContainer.AddChild(label, false, 0L);
			if (RpgInventoryScreen._deleteMode)
			{
				Label label2 = new Label
				{
					Text = SpireLoc.T("SPIRE_INVENTORY.delete_hint", "Click item to delete"),
					MouseFilter = 2L
				};
				label2.AddThemeFontSizeOverride("font_size", 13);
				label2.Modulate = RpgInventoryScreen.DeleteBorder;
				hboxContainer.AddChild(label2, false, 0L);
			}
			vboxContainer.AddChild(hboxContainer, false, 0L);
			vboxContainer.AddChild(RpgItemBrowseBar.Create(RpgInventoryScreen.BagBrowse, delegate
			{
				RpgItemTooltip.Hide(RpgInventoryScreen._host);
				RpgInventoryScreen.Refresh();
			}), false, 0L);
			ScrollContainer scrollContainer = new ScrollContainer
			{
				SizeFlagsHorizontal = 3L,
				SizeFlagsVertical = 3L,
				HorizontalScrollMode = 0L,
				VerticalScrollMode = 1L,
				MouseFilter = 0L
			};
			GridContainer gridContainer = new GridContainer
			{
				Columns = 10,
				SizeFlagsHorizontal = 3L,
				MouseFilter = 2L
			};
			gridContainer.AddThemeConstantOverride("h_separation", 8);
			gridContainer.AddThemeConstantOverride("v_separation", 8);
			List<RpgItemData> list = RpgInventoryScreen.FilteredBag();
			int num = Math.Max(6, (list.Count + 10 - 1) / 10) * 10;
			for (int i = 0; i < num; i++)
			{
				RpgItemData item = (i < list.Count) ? list[i] : null;
				gridContainer.AddChild(RpgInventoryScreen.MakeBagSlot(68f, item, tooltipHost), false, 0L);
			}
			scrollContainer.AddChild(gridContainer, false, 0L);
			vboxContainer.AddChild(scrollContainer, false, 0L);
			return vboxContainer;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00007C48 File Offset: 0x00005E48
		private static List<RpgItemData> FilteredBag()
		{
			IEnumerable<RpgItemData> enumerable = RpgActiveLoadout.Bag;
			if (RpgEffectiveConfig.EnableScrolls)
			{
				enumerable = enumerable.Concat(RpgScrollBag.Local());
			}
			return RpgItemBrowseBar.Apply(enumerable, RpgInventoryScreen.BagBrowse).ToList<RpgItemData>();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00007C80 File Offset: 0x00005E80
		private static Control MakeFooter(Control host, Control tooltipHost)
		{
			HBoxContainer hboxContainer = new HBoxContainer();
			hboxContainer.SizeFlagsHorizontal = 3L;
			hboxContainer.MouseFilter = 2L;
			hboxContainer.AddChild(new Control
			{
				SizeFlagsHorizontal = 3L,
				MouseFilter = 2L
			}, false, 0L);
			hboxContainer.AddChild(RpgPlayerScreen.MakeBackButton(delegate
			{
				RpgInventoryScreen.Hide(host);
			}), false, 0L);
			HBoxContainer hboxContainer2 = new HBoxContainer
			{
				SizeFlagsHorizontal = 3L,
				Alignment = 2L,
				MouseFilter = 2L
			};
			hboxContainer2.AddChild(RpgInventoryScreen.MakeDeleteToggle(tooltipHost), false, 0L);
			hboxContainer.AddChild(hboxContainer2, false, 0L);
			return hboxContainer;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00007D20 File Offset: 0x00005F20
		private static Button MakeDeleteToggle(Control tooltipHost)
		{
			Button button = new Button();
			button.Text = (RpgInventoryScreen._deleteMode ? SpireLoc.T("SPIRE_INVENTORY.delete_on", "Deleting…") : SpireLoc.T("SPIRE_INVENTORY.delete", "Delete"));
			button.CustomMinimumSize = new Vector2(140f, 48f);
			button.MouseFilter = 0L;
			Color bg = RpgInventoryScreen._deleteMode ? RpgInventoryScreen.DeleteOn : RpgInventoryScreen.DeleteIdle;
			Color border = RpgInventoryScreen._deleteMode ? RpgInventoryScreen.DeleteBorder : RpgInventoryScreen.SlotBorder;
			button.AddThemeStyleboxOverride("normal", RpgInventoryScreen.Flat(bg, border, RpgInventoryScreen._deleteMode ? 2f : 1f, 8));
			button.AddThemeStyleboxOverride("hover", RpgInventoryScreen.Flat(RpgInventoryScreen.SlotHover, RpgInventoryScreen.DeleteBorder, 2f, 8));
			button.AddThemeStyleboxOverride("pressed", RpgInventoryScreen.Flat(RpgInventoryScreen.DeleteOn, RpgInventoryScreen.DeleteBorder, 2f, 8));
			button.Pressed += delegate()
			{
				RpgInventoryScreen._deleteMode = !RpgInventoryScreen._deleteMode;
				RpgInventoryScreen.FreeDeleteConfirm(tooltipHost);
				RpgItemTooltip.Hide(tooltipHost);
				RpgInventoryScreen.Refresh();
			};
			return button;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00007E38 File Offset: 0x00006038
		private static void ShowDeleteConfirm(Control tooltipHost, RpgItemData item)
		{
			RpgInventoryScreen.FreeDeleteConfirm(tooltipHost);
			RpgItemTooltip.Hide(tooltipHost);
			ColorRect colorRect = new ColorRect
			{
				Name = "SpireRpgDeleteConfirm",
				Color = new Color(0f, 0f, 0f, 0.55f),
				MouseFilter = 0L
			};
			colorRect.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			tooltipHost.AddChild(colorRect, false, 0L);
			colorRect.MoveToFront();
			Panel panel = new Panel
			{
				CustomMinimumSize = new Vector2(420f, 180f),
				MouseFilter = 0L
			};
			panel.SetAnchorsPreset(8L, false);
			panel.GrowHorizontal = 2L;
			panel.GrowVertical = 2L;
			panel.OffsetLeft = -210f;
			panel.OffsetRight = 210f;
			panel.OffsetTop = -90f;
			panel.OffsetBottom = 90f;
			panel.AddThemeStyleboxOverride("panel", RpgInventoryScreen.Flat(RpgInventoryScreen.PanelBg, RpgInventoryScreen.DeleteBorder, 2f, 14));
			colorRect.AddChild(panel, false, 0L);
			VBoxContainer vboxContainer = new VBoxContainer
			{
				MouseFilter = 2L
			};
			vboxContainer.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			vboxContainer.OffsetLeft = 28f;
			vboxContainer.OffsetRight = -28f;
			vboxContainer.OffsetTop = 24f;
			vboxContainer.OffsetBottom = -24f;
			vboxContainer.AddThemeConstantOverride("separation", 20);
			panel.AddChild(vboxContainer, false, 0L);
			Label label = new Label
			{
				Text = string.Format(SpireLoc.T("SPIRE_INVENTORY.delete_confirm", "Delete {0}?"), item.DisplayName),
				HorizontalAlignment = 1L,
				AutowrapMode = 3L,
				SizeFlagsVertical = 3L,
				MouseFilter = 2L
			};
			label.AddThemeFontSizeOverride("font_size", 18);
			vboxContainer.AddChild(label, false, 0L);
			HBoxContainer hboxContainer = new HBoxContainer
			{
				Alignment = 1L,
				MouseFilter = 2L
			};
			hboxContainer.AddThemeConstantOverride("separation", 16);
			Button button = new Button
			{
				Text = SpireLoc.T("SPIRE_INVENTORY.delete_cancel", "Cancel"),
				CustomMinimumSize = new Vector2(120f, 42f),
				MouseFilter = 0L
			};
			button.Pressed += delegate()
			{
				RpgInventoryScreen.FreeDeleteConfirm(tooltipHost);
			};
			hboxContainer.AddChild(button, false, 0L);
			Button button2 = new Button
			{
				Text = SpireLoc.T("SPIRE_INVENTORY.delete_ok", "Delete"),
				CustomMinimumSize = new Vector2(120f, 42f),
				MouseFilter = 0L
			};
			button2.AddThemeStyleboxOverride("normal", RpgInventoryScreen.Flat(RpgInventoryScreen.DeleteOn, RpgInventoryScreen.DeleteBorder, 2f, 8));
			button2.AddThemeStyleboxOverride("hover", RpgInventoryScreen.Flat(new Color(0.65f, 0.22f, 0.22f, 1f), RpgInventoryScreen.DeleteBorder, 2f, 8));
			button2.Pressed += delegate()
			{
				RpgInventoryScreen.FreeDeleteConfirm(tooltipHost);
				if (item.IsScroll)
				{
					if (RpgScrollBag.TryRemove(RpgNet.LocalNetId(), item.InstanceId))
					{
						RpgInventoryScreen.Refresh();
					}
					return;
				}
				if (RpgActiveLoadout.TryRemoveFromBag(item))
				{
					RpgInventoryScreen.Refresh();
				}
			};
			hboxContainer.AddChild(button2, false, 0L);
			vboxContainer.AddChild(hboxContainer, false, 0L);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00008176 File Offset: 0x00006376
		private static void FreeDeleteConfirm(Control tooltipHost)
		{
			Node nodeOrNull = tooltipHost.GetNodeOrNull("SpireRpgDeleteConfirm");
			if (nodeOrNull == null)
			{
				return;
			}
			nodeOrNull.QueueFree();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00008194 File Offset: 0x00006394
		private static VBoxContainer MakeEquipColumn(RpgEquipSlot[] slots, Control tooltipHost)
		{
			VBoxContainer vboxContainer = new VBoxContainer
			{
				Alignment = 1L,
				MouseFilter = 2L
			};
			vboxContainer.AddThemeConstantOverride("separation", 10);
			foreach (RpgEquipSlot slot in slots)
			{
				vboxContainer.AddChild(RpgInventoryScreen.MakeEquipSlot(slot, 56f, tooltipHost), false, 0L);
			}
			return vboxContainer;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000081F4 File Offset: 0x000063F4
		private static Control MakeEquipSlot(RpgEquipSlot slot, float size, Control tooltipHost)
		{
			VBoxContainer vboxContainer = new VBoxContainer();
			vboxContainer.MouseFilter = 2L;
			vboxContainer.AddThemeConstantOverride("separation", 4);
			RpgItemData item = RpgActiveLoadout.GetEquipped(slot);
			Panel panel = RpgInventoryScreen.MakeItemPanel(size, item, tooltipHost, delegate
			{
				if (item == null || RpgInventoryScreen._deleteMode)
				{
					return;
				}
				if (RpgInventoryScreen.BlockCombatLoadoutChange(tooltipHost))
				{
					return;
				}
				if (RpgActiveLoadout.TryUnequip(slot))
				{
					RpgItemTooltip.Hide(tooltipHost);
					RpgInventoryScreen.Refresh();
				}
			}, null);
			vboxContainer.AddChild(panel, false, 0L);
			Label label = new Label
			{
				Text = RpgInventoryScreen.SlotLabel(slot),
				HorizontalAlignment = 1L,
				MouseFilter = 2L
			};
			label.AddThemeFontSizeOverride("font_size", 12);
			label.AddThemeColorOverride("font_color", RpgInventoryScreen.SlotLabelColor);
			vboxContainer.AddChild(label, false, 0L);
			return vboxContainer;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000082C8 File Offset: 0x000064C8
		private static Panel MakeBagSlot(float size, [Nullable(2)] RpgItemData item, Control tooltipHost)
		{
			return RpgInventoryScreen.MakeItemPanel(size, item, tooltipHost, delegate
			{
				if (item == null || RpgInventoryScreen._deleteMode)
				{
					return;
				}
				if (item.IsScroll)
				{
					if (RpgInventoryScreen.BlockCombatLoadoutChange(tooltipHost))
					{
						return;
					}
					RpgInventoryScreen.UseScrollFromBag(tooltipHost, item);
					return;
				}
				else
				{
					if (RpgInventoryScreen.BlockCombatLoadoutChange(tooltipHost))
					{
						return;
					}
					if (!item.IsIdentified || !RpgActiveLoadout.TryEquipFromBag(item))
					{
						RpgInventoryScreen.ShowEquipBlocked(tooltipHost, item);
						return;
					}
					RpgItemTooltip.Hide(tooltipHost);
					RpgInventoryScreen.Refresh();
					return;
				}
			}, delegate
			{
				if (item == null || !RpgInventoryScreen._deleteMode)
				{
					return;
				}
				if (RpgInventoryScreen.BlockCombatLoadoutChange(tooltipHost))
				{
					return;
				}
				RpgInventoryScreen.ShowDeleteConfirm(tooltipHost, item);
			});
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00008314 File Offset: 0x00006514
		private static Task UseScrollFromBag(Control tooltipHost, RpgItemData scroll)
		{
			RpgInventoryScreen.<UseScrollFromBag>d__40 <UseScrollFromBag>d__;
			<UseScrollFromBag>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UseScrollFromBag>d__.tooltipHost = tooltipHost;
			<UseScrollFromBag>d__.scroll = scroll;
			<UseScrollFromBag>d__.<>1__state = -1;
			<UseScrollFromBag>d__.<>t__builder.Start<RpgInventoryScreen.<UseScrollFromBag>d__40>(ref <UseScrollFromBag>d__);
			return <UseScrollFromBag>d__.<>t__builder.Task;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00008360 File Offset: 0x00006560
		private static Task WaitForMouseIdle()
		{
			RpgInventoryScreen.<WaitForMouseIdle>d__41 <WaitForMouseIdle>d__;
			<WaitForMouseIdle>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WaitForMouseIdle>d__.<>1__state = -1;
			<WaitForMouseIdle>d__.<>t__builder.Start<RpgInventoryScreen.<WaitForMouseIdle>d__41>(ref <WaitForMouseIdle>d__);
			return <WaitForMouseIdle>d__.<>t__builder.Task;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000839C File Offset: 0x0000659C
		private static Task WaitOneFrame()
		{
			RpgInventoryScreen.<WaitOneFrame>d__42 <WaitOneFrame>d__;
			<WaitOneFrame>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WaitOneFrame>d__.<>1__state = -1;
			<WaitOneFrame>d__.<>t__builder.Start<RpgInventoryScreen.<WaitOneFrame>d__42>(ref <WaitOneFrame>d__);
			return <WaitOneFrame>d__.<>t__builder.Task;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000083D8 File Offset: 0x000065D8
		[NullableContext(2)]
		[return: Nullable(1)]
		private static Panel MakeItemPanel(float size, RpgItemData item, [Nullable(1)] Control tooltipHost, Action onRightClick, Action onLeftClick = null)
		{
			Color color = (item == null) ? RpgInventoryScreen.SlotBorder : RpgItemTooltip.RarityColor(item.Rarity);
			Color idle = (RpgInventoryScreen._deleteMode && item != null) ? new Color(0.22f, 0.14f, 0.14f, 1f) : RpgInventoryScreen.SlotIdle;
			Color hover = (RpgInventoryScreen._deleteMode && item != null) ? new Color(0.38f, 0.2f, 0.2f, 1f) : RpgInventoryScreen.SlotHover;
			Color border = (RpgInventoryScreen._deleteMode && item != null) ? RpgInventoryScreen.DeleteBorder : color;
			Panel panel = new Panel
			{
				CustomMinimumSize = new Vector2(size, size),
				SizeFlagsHorizontal = 0L,
				SizeFlagsVertical = 0L,
				MouseFilter = 0L
			};
			panel.AddThemeStyleboxOverride("panel", RpgInventoryScreen.Flat(idle, border, (item == null) ? 1f : 2f, 10));
			if (item != null)
			{
				RpgInventoryScreen.FillItemVisual(panel, item, color);
			}
			panel.MouseEntered += delegate()
			{
				panel.AddThemeStyleboxOverride("panel", RpgInventoryScreen.Flat(hover, border, (item == null) ? 1f : 2f, 10));
				if (item != null && !RpgInventoryScreen._deleteMode)
				{
					RpgItemTooltip.Show(tooltipHost, item, panel.GetGlobalRect());
				}
			};
			Action <>9__3;
			panel.MouseExited += delegate()
			{
				panel.AddThemeStyleboxOverride("panel", RpgInventoryScreen.Flat(idle, border, (item == null) ? 1f : 2f, 10));
				Action action;
				if ((action = <>9__3) == null)
				{
					action = (<>9__3 = delegate()
					{
						if (!GodotObject.IsInstanceValid(tooltipHost))
						{
							return;
						}
						Control control = tooltipHost.GetNodeOrNull("SpireRpgItemTooltip") as Control;
						if (control != null && control.GetGlobalRect().HasPoint(tooltipHost.GetGlobalMousePosition()))
						{
							return;
						}
						RpgItemTooltip.Hide(tooltipHost);
					});
				}
				Callable.From(action).CallDeferred(Array.Empty<Variant>());
			};
			if (onRightClick != null || onLeftClick != null)
			{
				panel.GuiInput += delegate(InputEvent e)
				{
					InputEventMouseButton inputEventMouseButton = e as InputEventMouseButton;
					if (inputEventMouseButton == null)
					{
						return;
					}
					if (inputEventMouseButton.ButtonIndex == 2L && onRightClick != null)
					{
						panel.AcceptEvent();
						if (inputEventMouseButton.Pressed)
						{
							onRightClick();
						}
						return;
					}
					if (inputEventMouseButton.ButtonIndex == 1L && onLeftClick != null)
					{
						panel.AcceptEvent();
						if (inputEventMouseButton.Pressed)
						{
							onLeftClick();
						}
					}
				};
			}
			return panel;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00008590 File Offset: 0x00006790
		private static void FillItemVisual(Panel panel, RpgItemData item, Color rarityColor)
		{
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
			else
			{
				Label label = new Label
				{
					Text = item.IconLetter,
					HorizontalAlignment = 1L,
					VerticalAlignment = 1L,
					MouseFilter = 2L
				};
				label.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
				label.AddThemeFontSizeOverride("font_size", 22);
				label.Modulate = rarityColor;
				panel.AddChild(label, false, 0L);
			}
			ColorRect colorRect = new ColorRect
			{
				Color = rarityColor,
				CustomMinimumSize = new Vector2(10f, 10f),
				MouseFilter = 2L
			};
			colorRect.SetAnchorsPreset(1L, false);
			colorRect.OffsetLeft = -16f;
			colorRect.OffsetTop = 6f;
			colorRect.OffsetRight = -6f;
			colorRect.OffsetBottom = 16f;
			panel.AddChild(colorRect, false, 0L);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000086D7 File Offset: 0x000068D7
		private static bool BlockCombatLoadoutChange(Control tooltipHost)
		{
			if (!RpgInRunInventory.IsInCombat())
			{
				return false;
			}
			RpgInventoryScreen.ShowBlockedMessage(tooltipHost, SpireLoc.T("SPIRE_INVENTORY.combat_lock", "Cannot change equipment during combat."));
			return true;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000086F8 File Offset: 0x000068F8
		private static void ShowEquipBlocked(Control tooltipHost, RpgItemData item)
		{
			RpgInventoryScreen.ShowBlockedMessage(tooltipHost, RpgInventoryScreen.EquipBlockedText(item));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00008708 File Offset: 0x00006908
		private static void ShowBlockedMessage(Control tooltipHost, string text)
		{
			Node nodeOrNull = tooltipHost.GetNodeOrNull("SpireRpgEquipBlocked");
			if (nodeOrNull != null)
			{
				nodeOrNull.QueueFree();
			}
			Label label = new Label
			{
				Name = "SpireRpgEquipBlocked",
				Text = text,
				HorizontalAlignment = 1L,
				MouseFilter = 2L
			};
			label.AddThemeFontSizeOverride("font_size", 16);
			label.AddThemeColorOverride("font_color", new Color(1f, 0.72f, 0.45f, 1f));
			label.SetAnchorsPreset(7L, false);
			label.OffsetTop = -96f;
			label.OffsetBottom = -64f;
			label.OffsetLeft = -280f;
			label.OffsetRight = 280f;
			tooltipHost.AddChild(label, false, 0L);
			label.MoveToFront();
			SceneTree tree = label.GetTree();
			if (tree == null)
			{
				return;
			}
			RpgInventoryScreen.ClearEquipBlockedSoon(label, tree);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000087F0 File Offset: 0x000069F0
		private static Task ClearEquipBlockedSoon(Label tip, SceneTree tree)
		{
			RpgInventoryScreen.<ClearEquipBlockedSoon>d__48 <ClearEquipBlockedSoon>d__;
			<ClearEquipBlockedSoon>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ClearEquipBlockedSoon>d__.tip = tip;
			<ClearEquipBlockedSoon>d__.tree = tree;
			<ClearEquipBlockedSoon>d__.<>1__state = -1;
			<ClearEquipBlockedSoon>d__.<>t__builder.Start<RpgInventoryScreen.<ClearEquipBlockedSoon>d__48>(ref <ClearEquipBlockedSoon>d__);
			return <ClearEquipBlockedSoon>d__.<>t__builder.Task;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000883C File Offset: 0x00006A3C
		private static string EquipBlockedText(RpgItemData item)
		{
			if (!item.IsIdentified)
			{
				return SpireLoc.T("SPIRE_INVENTORY.equip_unidentified", "Identify it before equipping.");
			}
			if (!RpgAccessoryGates.MeetsCharacterRequirement(item, null))
			{
				string newValue = SpireLoc.Item("character." + item.RequireCharacter, item.RequireCharacter ?? "?");
				return SpireLoc.T("SPIRE_INVENTORY.equip_character", "Requires character: {0}").Replace("{0}", newValue);
			}
			if (!RpgAccessoryGates.MeetsSlotRequirements(item, null))
			{
				if (item.RequireSlots.Count > 0)
				{
					string newValue2 = string.Join(", ", from s in item.RequireSlots
					select SpireLoc.Item("type." + s, s));
					return SpireLoc.Item("ui.require_slots", "Requires equipped: {0}").Replace("{0}", newValue2);
				}
				return SpireLoc.T("SPIRE_INVENTORY.equip_slots", "Requirements not met.");
			}
			else
			{
				if (!RpgLoadout.MeetsLevelRequirement(item))
				{
					return SpireLoc.T("SPIRE_INVENTORY.equip_level", "Requires level {0}").Replace("{0}", item.RequireLevel.ToString());
				}
				if (!RpgLoadout.CanEquipRarePlus(item))
				{
					int maxRareEquipped = RpgEffectiveConfig.MaxRareEquipped;
					return SpireLoc.T("SPIRE_INVENTORY.equip_rare_cap", "Rare+ equip limit {0}/{0}. Unequip one first.").Replace("{0}", maxRareEquipped.ToString());
				}
				RpgItemData equipped = RpgActiveLoadout.GetEquipped(item.EquipSlot);
				if (equipped != null && !RpgCurseSystem.CanUnequip(equipped))
				{
					return SpireLoc.T("SPIRE_INVENTORY.equip_bound", "Current item is bound. Cleanse it first.");
				}
				return SpireLoc.T("SPIRE_INVENTORY.equip_fail", "Cannot equip.");
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000089B8 File Offset: 0x00006BB8
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
				result = "?";
				break;
			}
			return result;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00008A58 File Offset: 0x00006C58
		private static StyleBoxFlat Flat(Color bg, Color border, float borderWidth, int radius)
		{
			return new StyleBoxFlat
			{
				BgColor = bg,
				BorderColor = border,
				BorderWidthLeft = (int)borderWidth,
				BorderWidthTop = (int)borderWidth,
				BorderWidthRight = (int)borderWidth,
				BorderWidthBottom = (int)borderWidth,
				CornerRadiusTopLeft = radius,
				CornerRadiusTopRight = radius,
				CornerRadiusBottomRight = radius,
				CornerRadiusBottomLeft = radius
			};
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00008AB4 File Offset: 0x00006CB4
		// Note: this type is marked as 'beforefieldinit'.
		static RpgInventoryScreen()
		{
		}

		// Token: 0x04000034 RID: 52
		private const string OverlayName = "SpireRpgInventoryOverlay";

		// Token: 0x04000035 RID: 53
		private const string DeleteConfirmName = "SpireRpgDeleteConfirm";

		// Token: 0x04000036 RID: 54
		private const int InvColumns = 10;

		// Token: 0x04000037 RID: 55
		private const int InvRows = 6;

		// Token: 0x04000038 RID: 56
		private const float SlotSize = 68f;

		// Token: 0x04000039 RID: 57
		private const float EquipSize = 56f;

		// Token: 0x0400003A RID: 58
		private static readonly Color PanelBg = new Color(0.11f, 0.11f, 0.12f, 0.98f);

		// Token: 0x0400003B RID: 59
		private static readonly Color SlotIdle = new Color(0.17f, 0.17f, 0.19f, 1f);

		// Token: 0x0400003C RID: 60
		private static readonly Color SlotHover = new Color(0.28f, 0.28f, 0.31f, 1f);

		// Token: 0x0400003D RID: 61
		private static readonly Color SlotBorder = new Color(1f, 1f, 1f, 0.08f);

		// Token: 0x0400003E RID: 62
		private static readonly Color CharacterIdle = new Color(0.14f, 0.14f, 0.15f, 1f);

		// Token: 0x0400003F RID: 63
		private static readonly Color SlotLabelColor = new Color(1f, 1f, 1f, 0.35f);

		// Token: 0x04000040 RID: 64
		private static readonly Color DeleteIdle = new Color(0.22f, 0.16f, 0.16f, 1f);

		// Token: 0x04000041 RID: 65
		private static readonly Color DeleteOn = new Color(0.55f, 0.18f, 0.18f, 1f);

		// Token: 0x04000042 RID: 66
		private static readonly Color DeleteBorder = new Color(0.85f, 0.35f, 0.35f, 1f);

		// Token: 0x04000043 RID: 67
		[Nullable(2)]
		private static Control _host;

		// Token: 0x04000044 RID: 68
		private static bool _deleteMode;

		// Token: 0x04000045 RID: 69
		private static readonly RpgItemBrowseState BagBrowse = new RpgItemBrowseState();

		// Token: 0x0200009A RID: 154
		[Nullable(0)]
		private readonly struct EffectLine : IEquatable<RpgInventoryScreen.EffectLine>
		{
			// Token: 0x0600060A RID: 1546 RVA: 0x0001F47A File Offset: 0x0001D67A
			public EffectLine(string Name, string ValueText, Color Color, string AffixId, int Amount, [Nullable(2)] string FallbackIconPath)
			{
				this.Name = Name;
				this.ValueText = ValueText;
				this.Color = Color;
				this.AffixId = AffixId;
				this.Amount = Amount;
				this.FallbackIconPath = FallbackIconPath;
			}

			// Token: 0x17000183 RID: 387
			// (get) Token: 0x0600060B RID: 1547 RVA: 0x0001F4A9 File Offset: 0x0001D6A9
			// (set) Token: 0x0600060C RID: 1548 RVA: 0x0001F4B1 File Offset: 0x0001D6B1
			public string Name
			{
				[CompilerGenerated]
				get
				{
					return this.<Name>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Name>k__BackingField = value;
				}
			}

			// Token: 0x17000184 RID: 388
			// (get) Token: 0x0600060D RID: 1549 RVA: 0x0001F4BA File Offset: 0x0001D6BA
			// (set) Token: 0x0600060E RID: 1550 RVA: 0x0001F4C2 File Offset: 0x0001D6C2
			public string ValueText
			{
				[CompilerGenerated]
				get
				{
					return this.<ValueText>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<ValueText>k__BackingField = value;
				}
			}

			// Token: 0x17000185 RID: 389
			// (get) Token: 0x0600060F RID: 1551 RVA: 0x0001F4CB File Offset: 0x0001D6CB
			// (set) Token: 0x06000610 RID: 1552 RVA: 0x0001F4D3 File Offset: 0x0001D6D3
			public Color Color
			{
				[CompilerGenerated]
				get
				{
					return this.<Color>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Color>k__BackingField = value;
				}
			}

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001F4DC File Offset: 0x0001D6DC
			// (set) Token: 0x06000612 RID: 1554 RVA: 0x0001F4E4 File Offset: 0x0001D6E4
			public string AffixId
			{
				[CompilerGenerated]
				get
				{
					return this.<AffixId>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<AffixId>k__BackingField = value;
				}
			}

			// Token: 0x17000187 RID: 391
			// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001F4ED File Offset: 0x0001D6ED
			// (set) Token: 0x06000614 RID: 1556 RVA: 0x0001F4F5 File Offset: 0x0001D6F5
			public int Amount
			{
				[CompilerGenerated]
				get
				{
					return this.<Amount>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Amount>k__BackingField = value;
				}
			}

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x06000615 RID: 1557 RVA: 0x0001F4FE File Offset: 0x0001D6FE
			// (set) Token: 0x06000616 RID: 1558 RVA: 0x0001F506 File Offset: 0x0001D706
			[Nullable(2)]
			public string FallbackIconPath
			{
				[NullableContext(2)]
				[CompilerGenerated]
				get
				{
					return this.<FallbackIconPath>k__BackingField;
				}
				[NullableContext(2)]
				[CompilerGenerated]
				set
				{
					this.<FallbackIconPath>k__BackingField = value;
				}
			}

			// Token: 0x06000617 RID: 1559 RVA: 0x0001F510 File Offset: 0x0001D710
			[NullableContext(0)]
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("EffectLine");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x06000618 RID: 1560 RVA: 0x0001F55C File Offset: 0x0001D75C
			[NullableContext(0)]
			[CompilerGenerated]
			private bool PrintMembers(StringBuilder builder)
			{
				builder.Append("Name = ");
				builder.Append(this.Name);
				builder.Append(", ValueText = ");
				builder.Append(this.ValueText);
				builder.Append(", Color = ");
				builder.Append(this.Color.ToString());
				builder.Append(", AffixId = ");
				builder.Append(this.AffixId);
				builder.Append(", Amount = ");
				builder.Append(this.Amount.ToString());
				builder.Append(", FallbackIconPath = ");
				builder.Append(this.FallbackIconPath);
				return true;
			}

			// Token: 0x06000619 RID: 1561 RVA: 0x0001F61C File Offset: 0x0001D81C
			[CompilerGenerated]
			public static bool operator !=(RpgInventoryScreen.EffectLine left, RpgInventoryScreen.EffectLine right)
			{
				return !(left == right);
			}

			// Token: 0x0600061A RID: 1562 RVA: 0x0001F628 File Offset: 0x0001D828
			[CompilerGenerated]
			public static bool operator ==(RpgInventoryScreen.EffectLine left, RpgInventoryScreen.EffectLine right)
			{
				return left.Equals(right);
			}

			// Token: 0x0600061B RID: 1563 RVA: 0x0001F634 File Offset: 0x0001D834
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return ((((EqualityComparer<string>.Default.GetHashCode(this.<Name>k__BackingField) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<ValueText>k__BackingField)) * -1521134295 + EqualityComparer<Color>.Default.GetHashCode(this.<Color>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<AffixId>k__BackingField)) * -1521134295 + EqualityComparer<int>.Default.GetHashCode(this.<Amount>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<FallbackIconPath>k__BackingField);
			}

			// Token: 0x0600061C RID: 1564 RVA: 0x0001F6C4 File Offset: 0x0001D8C4
			[NullableContext(0)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return obj is RpgInventoryScreen.EffectLine && this.Equals((RpgInventoryScreen.EffectLine)obj);
			}

			// Token: 0x0600061D RID: 1565 RVA: 0x0001F6DC File Offset: 0x0001D8DC
			[CompilerGenerated]
			public bool Equals(RpgInventoryScreen.EffectLine other)
			{
				return EqualityComparer<string>.Default.Equals(this.<Name>k__BackingField, other.<Name>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<ValueText>k__BackingField, other.<ValueText>k__BackingField) && EqualityComparer<Color>.Default.Equals(this.<Color>k__BackingField, other.<Color>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<AffixId>k__BackingField, other.<AffixId>k__BackingField) && EqualityComparer<int>.Default.Equals(this.<Amount>k__BackingField, other.<Amount>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<FallbackIconPath>k__BackingField, other.<FallbackIconPath>k__BackingField);
			}

			// Token: 0x0600061E RID: 1566 RVA: 0x0001F779 File Offset: 0x0001D979
			[CompilerGenerated]
			public void Deconstruct(out string Name, out string ValueText, out Color Color, out string AffixId, out int Amount, [Nullable(2)] out string FallbackIconPath)
			{
				Name = this.Name;
				ValueText = this.ValueText;
				Color = this.Color;
				AffixId = this.AffixId;
				Amount = this.Amount;
				FallbackIconPath = this.FallbackIconPath;
			}

			// Token: 0x04000216 RID: 534
			[CompilerGenerated]
			private readonly string <Name>k__BackingField;

			// Token: 0x04000217 RID: 535
			[CompilerGenerated]
			private readonly string <ValueText>k__BackingField;

			// Token: 0x04000218 RID: 536
			[CompilerGenerated]
			private readonly Color <Color>k__BackingField;

			// Token: 0x04000219 RID: 537
			[CompilerGenerated]
			private readonly string <AffixId>k__BackingField;

			// Token: 0x0400021A RID: 538
			[CompilerGenerated]
			private readonly int <Amount>k__BackingField;

			// Token: 0x0400021B RID: 539
			[Nullable(2)]
			[CompilerGenerated]
			private readonly string <FallbackIconPath>k__BackingField;
		}

		// Token: 0x0200009B RID: 155
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600061F RID: 1567 RVA: 0x0001F7B2 File Offset: 0x0001D9B2
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000620 RID: 1568 RVA: 0x0001F7BE File Offset: 0x0001D9BE
			public <>c()
			{
			}

			// Token: 0x06000621 RID: 1569 RVA: 0x0001F7C6 File Offset: 0x0001D9C6
			[NullableContext(0)]
			internal string <BuildEquippedEffectLines>b__28_0(RpgStatMod m)
			{
				return m.Id;
			}

			// Token: 0x06000622 RID: 1570 RVA: 0x0001F7CE File Offset: 0x0001D9CE
			internal void <MakeInventoryArea>b__31_0()
			{
				RpgItemTooltip.Hide(RpgInventoryScreen._host);
				RpgInventoryScreen.Refresh();
			}

			// Token: 0x06000623 RID: 1571 RVA: 0x0001F7DF File Offset: 0x0001D9DF
			internal bool <UseScrollFromBag>b__40_0(RpgItemData x)
			{
				return x.HasCurse || x.IsBound;
			}

			// Token: 0x06000624 RID: 1572 RVA: 0x0001F7F1 File Offset: 0x0001D9F1
			internal bool <UseScrollFromBag>b__40_1(RpgItemData x)
			{
				return !x.IsIdentified;
			}

			// Token: 0x06000625 RID: 1573 RVA: 0x0001F7FC File Offset: 0x0001D9FC
			[NullableContext(0)]
			internal string <EquipBlockedText>b__49_0(string s)
			{
				return SpireLoc.Item("type." + s, s);
			}

			// Token: 0x0400021C RID: 540
			[Nullable(0)]
			public static readonly RpgInventoryScreen.<>c <>9 = new RpgInventoryScreen.<>c();

			// Token: 0x0400021D RID: 541
			[Nullable(0)]
			public static Func<RpgStatMod, string> <>9__28_0;

			// Token: 0x0400021E RID: 542
			[Nullable(0)]
			public static Action <>9__31_0;

			// Token: 0x0400021F RID: 543
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Func<RpgItemData, bool> <>9__40_0;

			// Token: 0x04000220 RID: 544
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Func<RpgItemData, bool> <>9__40_1;

			// Token: 0x04000221 RID: 545
			[Nullable(0)]
			public static Func<string, string> <>9__49_0;
		}

		// Token: 0x0200009C RID: 156
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_0
		{
			// Token: 0x06000626 RID: 1574 RVA: 0x0001F80F File Offset: 0x0001DA0F
			public <>c__DisplayClass21_0()
			{
			}

			// Token: 0x06000627 RID: 1575 RVA: 0x0001F817 File Offset: 0x0001DA17
			internal void <Refresh>b__0()
			{
				if (GodotObject.IsInstanceValid(this.host))
				{
					RpgInventoryScreen.Show(this.host, true);
				}
			}

			// Token: 0x04000222 RID: 546
			[Nullable(0)]
			public Control host;
		}

		// Token: 0x0200009D RID: 157
		[CompilerGenerated]
		private sealed class <>c__DisplayClass33_0
		{
			// Token: 0x06000628 RID: 1576 RVA: 0x0001F832 File Offset: 0x0001DA32
			public <>c__DisplayClass33_0()
			{
			}

			// Token: 0x06000629 RID: 1577 RVA: 0x0001F83A File Offset: 0x0001DA3A
			internal void <MakeFooter>b__0()
			{
				RpgInventoryScreen.Hide(this.host);
			}

			// Token: 0x04000223 RID: 547
			[Nullable(0)]
			public Control host;
		}

		// Token: 0x0200009E RID: 158
		[CompilerGenerated]
		private sealed class <>c__DisplayClass34_0
		{
			// Token: 0x0600062A RID: 1578 RVA: 0x0001F847 File Offset: 0x0001DA47
			public <>c__DisplayClass34_0()
			{
			}

			// Token: 0x0600062B RID: 1579 RVA: 0x0001F84F File Offset: 0x0001DA4F
			internal void <MakeDeleteToggle>b__0()
			{
				RpgInventoryScreen._deleteMode = !RpgInventoryScreen._deleteMode;
				RpgInventoryScreen.FreeDeleteConfirm(this.tooltipHost);
				RpgItemTooltip.Hide(this.tooltipHost);
				RpgInventoryScreen.Refresh();
			}

			// Token: 0x04000224 RID: 548
			[Nullable(0)]
			public Control tooltipHost;
		}

		// Token: 0x0200009F RID: 159
		[CompilerGenerated]
		private sealed class <>c__DisplayClass35_0
		{
			// Token: 0x0600062C RID: 1580 RVA: 0x0001F879 File Offset: 0x0001DA79
			public <>c__DisplayClass35_0()
			{
			}

			// Token: 0x0600062D RID: 1581 RVA: 0x0001F881 File Offset: 0x0001DA81
			internal void <ShowDeleteConfirm>b__0()
			{
				RpgInventoryScreen.FreeDeleteConfirm(this.tooltipHost);
			}

			// Token: 0x0600062E RID: 1582 RVA: 0x0001F890 File Offset: 0x0001DA90
			internal void <ShowDeleteConfirm>b__1()
			{
				RpgInventoryScreen.FreeDeleteConfirm(this.tooltipHost);
				if (this.item.IsScroll)
				{
					if (RpgScrollBag.TryRemove(RpgNet.LocalNetId(), this.item.InstanceId))
					{
						RpgInventoryScreen.Refresh();
					}
					return;
				}
				if (RpgActiveLoadout.TryRemoveFromBag(this.item))
				{
					RpgInventoryScreen.Refresh();
				}
			}

			// Token: 0x04000225 RID: 549
			[Nullable(0)]
			public Control tooltipHost;

			// Token: 0x04000226 RID: 550
			[Nullable(0)]
			public RpgItemData item;
		}

		// Token: 0x020000A0 RID: 160
		[CompilerGenerated]
		private sealed class <>c__DisplayClass38_0
		{
			// Token: 0x0600062F RID: 1583 RVA: 0x0001F8E4 File Offset: 0x0001DAE4
			public <>c__DisplayClass38_0()
			{
			}

			// Token: 0x06000630 RID: 1584 RVA: 0x0001F8EC File Offset: 0x0001DAEC
			internal void <MakeEquipSlot>b__0()
			{
				if (this.item == null || RpgInventoryScreen._deleteMode)
				{
					return;
				}
				if (RpgInventoryScreen.BlockCombatLoadoutChange(this.tooltipHost))
				{
					return;
				}
				if (RpgActiveLoadout.TryUnequip(this.slot))
				{
					RpgItemTooltip.Hide(this.tooltipHost);
					RpgInventoryScreen.Refresh();
				}
			}

			// Token: 0x04000227 RID: 551
			[Nullable(0)]
			public RpgItemData item;

			// Token: 0x04000228 RID: 552
			[Nullable(0)]
			public Control tooltipHost;

			// Token: 0x04000229 RID: 553
			public RpgEquipSlot slot;
		}

		// Token: 0x020000A1 RID: 161
		[CompilerGenerated]
		private sealed class <>c__DisplayClass39_0
		{
			// Token: 0x06000631 RID: 1585 RVA: 0x0001F929 File Offset: 0x0001DB29
			public <>c__DisplayClass39_0()
			{
			}

			// Token: 0x06000632 RID: 1586 RVA: 0x0001F934 File Offset: 0x0001DB34
			internal void <MakeBagSlot>b__0()
			{
				if (this.item == null || RpgInventoryScreen._deleteMode)
				{
					return;
				}
				if (this.item.IsScroll)
				{
					if (RpgInventoryScreen.BlockCombatLoadoutChange(this.tooltipHost))
					{
						return;
					}
					RpgInventoryScreen.UseScrollFromBag(this.tooltipHost, this.item);
					return;
				}
				else
				{
					if (RpgInventoryScreen.BlockCombatLoadoutChange(this.tooltipHost))
					{
						return;
					}
					if (!this.item.IsIdentified || !RpgActiveLoadout.TryEquipFromBag(this.item))
					{
						RpgInventoryScreen.ShowEquipBlocked(this.tooltipHost, this.item);
						return;
					}
					RpgItemTooltip.Hide(this.tooltipHost);
					RpgInventoryScreen.Refresh();
					return;
				}
			}

			// Token: 0x06000633 RID: 1587 RVA: 0x0001F9C9 File Offset: 0x0001DBC9
			internal void <MakeBagSlot>b__1()
			{
				if (this.item == null || !RpgInventoryScreen._deleteMode)
				{
					return;
				}
				if (RpgInventoryScreen.BlockCombatLoadoutChange(this.tooltipHost))
				{
					return;
				}
				RpgInventoryScreen.ShowDeleteConfirm(this.tooltipHost, this.item);
			}

			// Token: 0x0400022A RID: 554
			[Nullable(0)]
			public RpgItemData item;

			// Token: 0x0400022B RID: 555
			[Nullable(0)]
			public Control tooltipHost;
		}

		// Token: 0x020000A2 RID: 162
		[CompilerGenerated]
		private sealed class <>c__DisplayClass40_0
		{
			// Token: 0x06000634 RID: 1588 RVA: 0x0001F9FA File Offset: 0x0001DBFA
			public <>c__DisplayClass40_0()
			{
			}

			// Token: 0x06000635 RID: 1589 RVA: 0x0001FA02 File Offset: 0x0001DC02
			[NullableContext(0)]
			internal bool <UseScrollFromBag>b__2(RpgItemData x)
			{
				return !x.IsScroll && this.filter(x);
			}

			// Token: 0x0400022C RID: 556
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public Func<RpgItemData, bool> filter;
		}

		// Token: 0x020000A3 RID: 163
		[CompilerGenerated]
		private sealed class <>c__DisplayClass43_0
		{
			// Token: 0x06000636 RID: 1590 RVA: 0x0001FA1A File Offset: 0x0001DC1A
			public <>c__DisplayClass43_0()
			{
			}

			// Token: 0x06000637 RID: 1591 RVA: 0x0001FA24 File Offset: 0x0001DC24
			internal void <MakeItemPanel>b__0()
			{
				this.panel.AddThemeStyleboxOverride("panel", RpgInventoryScreen.Flat(this.hover, this.border, (this.item == null) ? 1f : 2f, 10));
				if (this.item != null && !RpgInventoryScreen._deleteMode)
				{
					RpgItemTooltip.Show(this.tooltipHost, this.item, this.panel.GetGlobalRect());
				}
			}

			// Token: 0x06000638 RID: 1592 RVA: 0x0001FA98 File Offset: 0x0001DC98
			internal void <MakeItemPanel>b__1()
			{
				this.panel.AddThemeStyleboxOverride("panel", RpgInventoryScreen.Flat(this.idle, this.border, (this.item == null) ? 1f : 2f, 10));
				Action action;
				if ((action = this.<>9__3) == null)
				{
					action = (this.<>9__3 = delegate()
					{
						if (!GodotObject.IsInstanceValid(this.tooltipHost))
						{
							return;
						}
						Control control = this.tooltipHost.GetNodeOrNull("SpireRpgItemTooltip") as Control;
						if (control != null && control.GetGlobalRect().HasPoint(this.tooltipHost.GetGlobalMousePosition()))
						{
							return;
						}
						RpgItemTooltip.Hide(this.tooltipHost);
					});
				}
				Callable.From(action).CallDeferred(Array.Empty<Variant>());
			}

			// Token: 0x06000639 RID: 1593 RVA: 0x0001FB14 File Offset: 0x0001DD14
			internal void <MakeItemPanel>b__3()
			{
				if (!GodotObject.IsInstanceValid(this.tooltipHost))
				{
					return;
				}
				Control control = this.tooltipHost.GetNodeOrNull("SpireRpgItemTooltip") as Control;
				if (control != null && control.GetGlobalRect().HasPoint(this.tooltipHost.GetGlobalMousePosition()))
				{
					return;
				}
				RpgItemTooltip.Hide(this.tooltipHost);
			}

			// Token: 0x0600063A RID: 1594 RVA: 0x0001FB74 File Offset: 0x0001DD74
			[NullableContext(0)]
			internal void <MakeItemPanel>b__2(InputEvent e)
			{
				InputEventMouseButton inputEventMouseButton = e as InputEventMouseButton;
				if (inputEventMouseButton == null)
				{
					return;
				}
				if (inputEventMouseButton.ButtonIndex == 2L && this.onRightClick != null)
				{
					this.panel.AcceptEvent();
					if (inputEventMouseButton.Pressed)
					{
						this.onRightClick();
					}
					return;
				}
				if (inputEventMouseButton.ButtonIndex == 1L && this.onLeftClick != null)
				{
					this.panel.AcceptEvent();
					if (inputEventMouseButton.Pressed)
					{
						this.onLeftClick();
					}
				}
			}

			// Token: 0x0400022D RID: 557
			[Nullable(0)]
			public Panel panel;

			// Token: 0x0400022E RID: 558
			public Color hover;

			// Token: 0x0400022F RID: 559
			public Color border;

			// Token: 0x04000230 RID: 560
			[Nullable(0)]
			public RpgItemData item;

			// Token: 0x04000231 RID: 561
			[Nullable(0)]
			public Control tooltipHost;

			// Token: 0x04000232 RID: 562
			public Color idle;

			// Token: 0x04000233 RID: 563
			[Nullable(0)]
			public Action onRightClick;

			// Token: 0x04000234 RID: 564
			[Nullable(0)]
			public Action onLeftClick;

			// Token: 0x04000235 RID: 565
			[Nullable(0)]
			public Action <>9__3;
		}

		// Token: 0x020000A4 RID: 164
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ClearEquipBlockedSoon>d__48 : IAsyncStateMachine
		{
			// Token: 0x0600063B RID: 1595 RVA: 0x0001FBF0 File Offset: 0x0001DDF0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					try
					{
						IAwaiter<Variant[]> awaiter;
						if (num != 0)
						{
							awaiter = this.tree.ToSignal(this.tree.CreateTimer(2.2, true, false, false), SceneTreeTimer.SignalName.Timeout).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitOnCompleted<IAwaiter<Variant[]>, RpgInventoryScreen.<ClearEquipBlockedSoon>d__48>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = (IAwaiter<Variant[]>)this.<>u__1;
							this.<>u__1 = null;
							this.<>1__state = -1;
						}
						awaiter.GetResult();
						if (GodotObject.IsInstanceValid(this.tip))
						{
							this.tip.QueueFree();
						}
					}
					catch
					{
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

			// Token: 0x0600063C RID: 1596 RVA: 0x0001FCE8 File Offset: 0x0001DEE8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000236 RID: 566
			public int <>1__state;

			// Token: 0x04000237 RID: 567
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000238 RID: 568
			[Nullable(0)]
			public SceneTree tree;

			// Token: 0x04000239 RID: 569
			[Nullable(0)]
			public Label tip;

			// Token: 0x0400023A RID: 570
			[Nullable(0)]
			private object <>u__1;
		}

		// Token: 0x020000A5 RID: 165
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <UseScrollFromBag>d__40 : IAsyncStateMachine
		{
			// Token: 0x0600063D RID: 1597 RVA: 0x0001FCF8 File Offset: 0x0001DEF8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					try
					{
						TaskAwaiter awaiter;
						TaskAwaiter<RpgIntensifyScreen.PickResult> awaiter2;
						TaskAwaiter<bool> awaiter3;
						string targetInstanceId;
						switch (num)
						{
						case 0:
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							this.<>1__state = -1;
							break;
						case 1:
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(TaskAwaiter<RpgIntensifyScreen.PickResult>);
							this.<>1__state = -1;
							goto IL_21D;
						case 2:
							awaiter3 = this.<>u__3;
							this.<>u__3 = default(TaskAwaiter<bool>);
							this.<>1__state = -1;
							goto IL_2AB;
						default:
						{
							this.<owner>5__2 = RpgNet.LocalPlayer();
							if (this.<owner>5__2 == null)
							{
								RpgInventoryScreen.ShowBlockedMessage(this.tooltipHost, SpireLoc.Item("ui.scroll_need_run", "Use scrolls during a run."));
								goto IL_30D;
							}
							targetInstanceId = null;
							this.<kind>5__3 = RpgScrolls.KindOf(this.scroll.Id);
							RpgScrollKind rpgScrollKind = this.<kind>5__3;
							bool flag = rpgScrollKind == RpgScrollKind.Cleanse || rpgScrollKind == RpgScrollKind.Identify;
							if (!flag)
							{
								goto IL_248;
							}
							this.<>8__1 = new RpgInventoryScreen.<>c__DisplayClass40_0();
							RpgInventoryScreen.<>c__DisplayClass40_0 CS$<>8__locals1 = this.<>8__1;
							Func<RpgItemData, bool> filter;
							if (this.<kind>5__3 != RpgScrollKind.Cleanse)
							{
								filter = new Func<RpgItemData, bool>(RpgInventoryScreen.<>c.<>9.<UseScrollFromBag>b__40_1);
							}
							else
							{
								filter = new Func<RpgItemData, bool>(RpgInventoryScreen.<>c.<>9.<UseScrollFromBag>b__40_0);
							}
							CS$<>8__locals1.filter = filter;
							if (!RpgActiveLoadout.AllItems(this.<owner>5__2).Any(new Func<RpgItemData, bool>(this.<>8__1.<UseScrollFromBag>b__2)))
							{
								RpgInventoryScreen.ShowBlockedMessage(this.tooltipHost, (this.<kind>5__3 == RpgScrollKind.Cleanse) ? SpireLoc.Item("ui.scroll_no_cleanse_target", "No cursed or bound item.") : SpireLoc.Item("ui.scroll_no_identify_target", "No unidentified item."));
								goto IL_30D;
							}
							awaiter = RpgInventoryScreen.WaitForMouseIdle().GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgInventoryScreen.<UseScrollFromBag>d__40>(ref awaiter, ref this);
								return;
							}
							break;
						}
						}
						awaiter.GetResult();
						RpgIntensifyScreen.PickChrome value = (this.<kind>5__3 == RpgScrollKind.Cleanse) ? RpgIntensifyScreen.CleanseChrome : RpgIntensifyScreen.IdentifyChrome;
						awaiter2 = RpgIntensifyScreen.OfferPickAsync(this.<owner>5__2, this.<>8__1.filter, new RpgIntensifyScreen.PickChrome?(value), null).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgIntensifyScreen.PickResult>, RpgInventoryScreen.<UseScrollFromBag>d__40>(ref awaiter2, ref this);
							return;
						}
						IL_21D:
						RpgIntensifyScreen.PickResult result = awaiter2.GetResult();
						if (!result.Ok)
						{
							RpgInventoryScreen.Refresh();
							goto IL_30D;
						}
						targetInstanceId = result.InstanceId;
						this.<>8__1 = null;
						IL_248:
						awaiter3 = RpgScrolls.TryUse(this.<owner>5__2, this.scroll, targetInstanceId).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__3 = awaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgInventoryScreen.<UseScrollFromBag>d__40>(ref awaiter3, ref this);
							return;
						}
						IL_2AB:
						if (awaiter3.GetResult())
						{
							RpgItemTooltip.Hide(this.tooltipHost);
							RpgInventoryScreen.Refresh();
						}
						this.<owner>5__2 = null;
					}
					catch (Exception ex)
					{
						SpireTheRpgMainFile.Logger.Info("UseScroll failed: " + ex.Message, 1);
						RpgInventoryScreen.Refresh();
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_30D:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600063E RID: 1598 RVA: 0x0002005C File Offset: 0x0001E25C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400023B RID: 571
			public int <>1__state;

			// Token: 0x0400023C RID: 572
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400023D RID: 573
			[Nullable(0)]
			public Control tooltipHost;

			// Token: 0x0400023E RID: 574
			[Nullable(0)]
			public RpgItemData scroll;

			// Token: 0x0400023F RID: 575
			[Nullable(0)]
			private RpgInventoryScreen.<>c__DisplayClass40_0 <>8__1;

			// Token: 0x04000240 RID: 576
			[Nullable(0)]
			private Player <owner>5__2;

			// Token: 0x04000241 RID: 577
			private RpgScrollKind <kind>5__3;

			// Token: 0x04000242 RID: 578
			private TaskAwaiter <>u__1;

			// Token: 0x04000243 RID: 579
			[Nullable(0)]
			private TaskAwaiter<RpgIntensifyScreen.PickResult> <>u__2;

			// Token: 0x04000244 RID: 580
			[Nullable(0)]
			private TaskAwaiter<bool> <>u__3;
		}

		// Token: 0x020000A6 RID: 166
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WaitForMouseIdle>d__41 : IAsyncStateMachine
		{
			// Token: 0x0600063F RID: 1599 RVA: 0x0002006C File Offset: 0x0001E26C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					TaskAwaiter awaiter;
					if (num != 0)
					{
						if (num != 1)
						{
							this.<i>5__2 = 0;
							goto IL_96;
						}
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter);
						this.<>1__state = -1;
						goto IL_F6;
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter);
						this.<>1__state = -1;
					}
					IL_7F:
					awaiter.GetResult();
					int num2 = this.<i>5__2;
					this.<i>5__2 = num2 + 1;
					IL_96:
					if (this.<i>5__2 < 180 && (Input.IsMouseButtonPressed(2L) || Input.IsMouseButtonPressed(1L)))
					{
						awaiter = RpgInventoryScreen.WaitOneFrame().GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgInventoryScreen.<WaitForMouseIdle>d__41>(ref awaiter, ref this);
							return;
						}
						goto IL_7F;
					}
					else
					{
						awaiter = RpgInventoryScreen.WaitOneFrame().GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgInventoryScreen.<WaitForMouseIdle>d__41>(ref awaiter, ref this);
							return;
						}
					}
					IL_F6:
					awaiter.GetResult();
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

			// Token: 0x06000640 RID: 1600 RVA: 0x000201B4 File Offset: 0x0001E3B4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000245 RID: 581
			public int <>1__state;

			// Token: 0x04000246 RID: 582
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000247 RID: 583
			private int <i>5__2;

			// Token: 0x04000248 RID: 584
			private TaskAwaiter <>u__1;
		}

		// Token: 0x020000A7 RID: 167
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WaitOneFrame>d__42 : IAsyncStateMachine
		{
			// Token: 0x06000641 RID: 1601 RVA: 0x000201C4 File Offset: 0x0001E3C4
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
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgInventoryScreen.<WaitOneFrame>d__42>(ref awaiter, ref this);
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
									this.<>t__builder.AwaitOnCompleted<IAwaiter<Variant[]>, RpgInventoryScreen.<WaitOneFrame>d__42>(ref awaiter2, ref this);
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

			// Token: 0x06000642 RID: 1602 RVA: 0x000202EC File Offset: 0x0001E4EC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000249 RID: 585
			public int <>1__state;

			// Token: 0x0400024A RID: 586
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400024B RID: 587
			private TaskAwaiter <>u__1;

			// Token: 0x0400024C RID: 588
			[Nullable(0)]
			private object <>u__2;
		}
	}
}
