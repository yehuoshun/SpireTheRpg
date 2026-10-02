using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Godot;
using MegaCrit.Sts2.Core.Nodes.Screens.MainMenu;

namespace SpireTheRpg.Ui
{
	// Token: 0x02000006 RID: 6
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgCodexScreen
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000033C8 File Offset: 0x000015C8
		public static void Show(NCompendiumSubmenu hub)
		{
			RpgCodexScreen.<>c__DisplayClass3_0 CS$<>8__locals1 = new RpgCodexScreen.<>c__DisplayClass3_0();
			CS$<>8__locals1.hub = hub;
			RpgPlayerScreen.Hide(CS$<>8__locals1.hub);
			RpgAbilityScreen.Hide(CS$<>8__locals1.hub);
			RpgInventoryScreen.Hide(CS$<>8__locals1.hub);
			RpgWipScreen.Hide(CS$<>8__locals1.hub);
			RpgCodexScreen.Hide(CS$<>8__locals1.hub);
			SpireLoc.ForceInject();
			ColorRect colorRect = RpgPlayerScreen.MakeDim("SpireRpgCodexOverlay");
			Control control = RpgPlayerScreen.MakePlainPanel(new Vector2(880f, 760f));
			colorRect.AddChild(control, false, 0L);
			VBoxContainer vboxContainer = new VBoxContainer
			{
				MouseFilter = 2L
			};
			vboxContainer.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			vboxContainer.OffsetLeft = 36f;
			vboxContainer.OffsetRight = -36f;
			vboxContainer.OffsetTop = 28f;
			vboxContainer.OffsetBottom = -28f;
			vboxContainer.AddThemeConstantOverride("separation", 10);
			control.AddChild(vboxContainer, false, 0L);
			Label label = new Label
			{
				Text = SpireLoc.T("SPIRE_CODEX.title", "Codex"),
				HorizontalAlignment = 1L,
				MouseFilter = 2L
			};
			label.AddThemeFontSizeOverride("font_size", 28);
			vboxContainer.AddChild(label, false, 0L);
			HBoxContainer hboxContainer = new HBoxContainer
			{
				MouseFilter = 0L
			};
			hboxContainer.AddThemeConstantOverride("separation", 8);
			vboxContainer.AddChild(hboxContainer, false, 0L);
			ScrollContainer scrollContainer = new ScrollContainer
			{
				SizeFlagsVertical = 3L,
				SizeFlagsHorizontal = 3L,
				HorizontalScrollMode = 0L,
				MouseFilter = 0L
			};
			vboxContainer.AddChild(scrollContainer, false, 0L);
			CS$<>8__locals1.list = new VBoxContainer
			{
				SizeFlagsHorizontal = 3L,
				MouseFilter = 2L
			};
			CS$<>8__locals1.list.AddThemeConstantOverride("separation", 8);
			scrollContainer.AddChild(CS$<>8__locals1.list, false, 0L);
			Button button = RpgCodexScreen.MakeTab(SpireLoc.T("SPIRE_CODEX.tab_stats", "Stats"), new Action(CS$<>8__locals1.<Show>g__ShowStats|0));
			Button button2 = RpgCodexScreen.MakeTab(SpireLoc.T("SPIRE_CODEX.tab_affixes", "Affixes"), new Action(CS$<>8__locals1.<Show>g__ShowAffixes|1));
			hboxContainer.AddChild(button, false, 0L);
			hboxContainer.AddChild(button2, false, 0L);
			CS$<>8__locals1.<Show>g__ShowStats|0();
			vboxContainer.AddChild(RpgPlayerScreen.MakeBackButton(delegate
			{
				RpgCodexScreen.Hide(CS$<>8__locals1.hub);
			}), false, 0L);
			CS$<>8__locals1.hub.AddChild(colorRect, false, 0L);
			colorRect.MoveToFront();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003624 File Offset: 0x00001824
		public static void Hide(NCompendiumSubmenu hub)
		{
			RpgPlayerScreen.FreeNamed(hub, "SpireRpgCodexOverlay");
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003631 File Offset: 0x00001831
		private static Button MakeTab(string text, Action onPress)
		{
			Button button = new Button();
			button.Text = text;
			button.CustomMinimumSize = new Vector2(140f, 40f);
			button.SizeFlagsHorizontal = 3L;
			button.MouseFilter = 0L;
			button.Pressed += onPress;
			return button;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000366C File Offset: 0x0000186C
		private static Control MakeSectionHeader(string text)
		{
			Label label = new Label();
			label.Text = text;
			label.MouseFilter = 2L;
			label.AddThemeFontSizeOverride("font_size", 20);
			label.Modulate = new Color(0.85f, 0.78f, 0.45f, 1f);
			return label;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000036C0 File Offset: 0x000018C0
		private static Control MakeEntry(string name, string desc)
		{
			VBoxContainer vboxContainer = new VBoxContainer
			{
				SizeFlagsHorizontal = 3L,
				MouseFilter = 2L
			};
			vboxContainer.AddThemeConstantOverride("separation", 2);
			Label label = new Label
			{
				Text = name,
				AutowrapMode = 3L,
				CustomMinimumSize = new Vector2(780f, 0f),
				MouseFilter = 2L
			};
			label.AddThemeFontSizeOverride("font_size", 18);
			vboxContainer.AddChild(label, false, 0L);
			if (!string.IsNullOrWhiteSpace(desc))
			{
				Label label2 = new Label
				{
					Text = desc,
					AutowrapMode = 3L,
					CustomMinimumSize = new Vector2(780f, 0f),
					MouseFilter = 2L
				};
				label2.AddThemeFontSizeOverride("font_size", 15);
				label2.Modulate = new Color(0.72f, 0.74f, 0.78f, 1f);
				vboxContainer.AddChild(label2, false, 0L);
			}
			ColorRect colorRect = new ColorRect
			{
				CustomMinimumSize = new Vector2(0f, 1f),
				Color = new Color(1f, 1f, 1f, 0.08f),
				MouseFilter = 2L
			};
			vboxContainer.AddChild(colorRect, false, 0L);
			return vboxContainer;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003804 File Offset: 0x00001A04
		private static void ClearChildren(Node node)
		{
			foreach (Node node2 in node.GetChildren(false).ToArray<Node>())
			{
				node.RemoveChild(node2);
				node2.Free();
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003840 File Offset: 0x00001A40
		private static List<RpgCodexScreen.AffixRow> LoadAffixes()
		{
			List<RpgCodexScreen.AffixRow> result;
			try
			{
				string text = SpireAssets.ReadText("assets/affixes/player_buffs.json");
				if (string.IsNullOrWhiteSpace(text))
				{
					result = new List<RpgCodexScreen.AffixRow>();
				}
				else
				{
					result = (JsonSerializer.Deserialize<List<RpgCodexScreen.AffixRow>>(text, new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					}) ?? new List<RpgCodexScreen.AffixRow>());
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("Codex affix load failed: " + ex.Message, 1);
				result = new List<RpgCodexScreen.AffixRow>();
			}
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000038BC File Offset: 0x00001ABC
		// Note: this type is marked as 'beforefieldinit'.
		static RpgCodexScreen()
		{
		}

		// Token: 0x04000011 RID: 17
		private const string OverlayName = "SpireRpgCodexOverlay";

		// Token: 0x04000012 RID: 18
		private static readonly string[] StatIds = new string[]
		{
			"armor",
			"strength",
			"dexterity",
			"intelligence",
			"vitality",
			"all_stats",
			"overpower_damage",
			"damage_reduction",
			"shadow_damage",
			"cooldown_reduction",
			"healing_received",
			"nature_damage",
			"block_chance",
			"critical_strike_damage",
			"damage_while_berserking"
		};

		// Token: 0x02000082 RID: 130
		[Nullable(0)]
		private sealed class AffixRow
		{
			// Token: 0x17000177 RID: 375
			// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001DDD0 File Offset: 0x0001BFD0
			// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0001DDD8 File Offset: 0x0001BFD8
			public string Id
			{
				[CompilerGenerated]
				get
				{
					return this.<Id>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Id>k__BackingField = value;
				}
			} = "";

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001DDE1 File Offset: 0x0001BFE1
			// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0001DDE9 File Offset: 0x0001BFE9
			public string Power
			{
				[CompilerGenerated]
				get
				{
					return this.<Power>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Power>k__BackingField = value;
				}
			} = "";

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x060005AA RID: 1450 RVA: 0x0001DDF2 File Offset: 0x0001BFF2
			// (set) Token: 0x060005AB RID: 1451 RVA: 0x0001DDFA File Offset: 0x0001BFFA
			public string GameName
			{
				[CompilerGenerated]
				get
				{
					return this.<GameName>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<GameName>k__BackingField = value;
				}
			} = "";

			// Token: 0x060005AC RID: 1452 RVA: 0x0001DE03 File Offset: 0x0001C003
			public AffixRow()
			{
			}

			// Token: 0x040001C8 RID: 456
			[CompilerGenerated]
			private string <Id>k__BackingField;

			// Token: 0x040001C9 RID: 457
			[CompilerGenerated]
			private string <Power>k__BackingField;

			// Token: 0x040001CA RID: 458
			[CompilerGenerated]
			private string <GameName>k__BackingField;
		}

		// Token: 0x02000083 RID: 131
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x060005AD RID: 1453 RVA: 0x0001DE2C File Offset: 0x0001C02C
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x060005AE RID: 1454 RVA: 0x0001DE34 File Offset: 0x0001C034
			internal void <Show>g__ShowStats|0()
			{
				RpgCodexScreen.ClearChildren(this.list);
				this.list.AddChild(RpgCodexScreen.MakeSectionHeader(SpireLoc.T("SPIRE_CODEX.stats", "Stats")), false, 0L);
				foreach (string text in RpgCodexScreen.StatIds)
				{
					string name = SpireLoc.Item("stat." + text, text);
					string desc = SpireLoc.Item("stat." + text + ".desc", "");
					this.list.AddChild(RpgCodexScreen.MakeEntry(name, desc), false, 0L);
				}
			}

			// Token: 0x060005AF RID: 1455 RVA: 0x0001DECC File Offset: 0x0001C0CC
			internal void <Show>g__ShowAffixes|1()
			{
				RpgCodexScreen.ClearChildren(this.list);
				this.list.AddChild(RpgCodexScreen.MakeSectionHeader(SpireLoc.T("SPIRE_CODEX.affixes", "Affixes")), false, 0L);
				foreach (RpgCodexScreen.AffixRow affixRow in RpgCodexScreen.LoadAffixes())
				{
					string name = SpireLoc.Item("affix." + affixRow.Id + ".name", affixRow.GameName);
					string text = SpireLoc.Item("affix." + affixRow.Id + ".effect", "");
					string text2 = SpireLoc.Item("affix." + affixRow.Id + ".desc", "");
					string text3 = string.IsNullOrWhiteSpace(text) ? text2 : text;
					if (!string.IsNullOrWhiteSpace(text2) && text2 != text3)
					{
						text3 = text3 + "\n" + text2;
					}
					this.list.AddChild(RpgCodexScreen.MakeEntry(name, text3), false, 0L);
				}
			}

			// Token: 0x060005B0 RID: 1456 RVA: 0x0001DFF8 File Offset: 0x0001C1F8
			internal void <Show>b__2()
			{
				RpgCodexScreen.Hide(this.hub);
			}

			// Token: 0x040001CB RID: 459
			[Nullable(0)]
			public VBoxContainer list;

			// Token: 0x040001CC RID: 460
			[Nullable(0)]
			public NCompendiumSubmenu hub;
		}
	}
}
