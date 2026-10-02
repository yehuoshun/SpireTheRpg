using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Entities.Multiplayer;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Screens.Overlays;
using MegaCrit.Sts2.Core.Nodes.Screens.ScreenContext;
using SpireTheRpg.Core;

namespace SpireTheRpg.Ui
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RpgLootScreen : Control, IOverlayScreen, IScreenContext
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000A437 File Offset: 0x00008637
		public NetScreenType ScreenType
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000A43B File Offset: 0x0000863B
		public bool UseSharedBackstop
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000A43E File Offset: 0x0000863E
		[Nullable(2)]
		public Control DefaultFocusedControl
		{
			[NullableContext(2)]
			get
			{
				return this._cards.FirstOrDefault<Button>();
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000A44B File Offset: 0x0000864B
		[Nullable(2)]
		public Control FocusedControlFromTopBar
		{
			[NullableContext(2)]
			get
			{
				return this.DefaultFocusedControl;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000A454 File Offset: 0x00008654
		private RpgLootScreen(IReadOnlyList<RpgItemData> choices, int xpGained, int levelsGained)
		{
			this._choices = choices;
			this._xpGained = xpGained;
			this._levelsGained = levelsGained;
			base.Name = "SpireRpgLootScreen";
			base.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			base.MouseFilter = 0L;
			base.FocusMode = 2L;
			base.Visible = true;
			this.Build();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000A4CC File Offset: 0x000086CC
		public static Task OfferAsync(IReadOnlyList<RpgItemData> choices, int xpGained, int levelsGained)
		{
			RpgLootScreen.<OfferAsync>d__17 <OfferAsync>d__;
			<OfferAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<OfferAsync>d__.choices = choices;
			<OfferAsync>d__.xpGained = xpGained;
			<OfferAsync>d__.levelsGained = levelsGained;
			<OfferAsync>d__.<>1__state = -1;
			<OfferAsync>d__.<>t__builder.Start<RpgLootScreen.<OfferAsync>d__17>(ref <OfferAsync>d__);
			return <OfferAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000A520 File Offset: 0x00008720
		private static Task AttachAsync(RpgLootScreen screen)
		{
			RpgLootScreen.<AttachAsync>d__18 <AttachAsync>d__;
			<AttachAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AttachAsync>d__.screen = screen;
			<AttachAsync>d__.<>1__state = -1;
			<AttachAsync>d__.<>t__builder.Start<RpgLootScreen.<AttachAsync>d__18>(ref <AttachAsync>d__);
			return <AttachAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000A564 File Offset: 0x00008764
		private static Task WaitOneFrame()
		{
			RpgLootScreen.<WaitOneFrame>d__19 <WaitOneFrame>d__;
			<WaitOneFrame>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WaitOneFrame>d__.<>1__state = -1;
			<WaitOneFrame>d__.<>t__builder.Start<RpgLootScreen.<WaitOneFrame>d__19>(ref <WaitOneFrame>d__);
			return <WaitOneFrame>d__.<>t__builder.Task;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000A59F File Offset: 0x0000879F
		public void AfterOverlayOpened()
		{
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000A5A1 File Offset: 0x000087A1
		public void AfterOverlayClosed()
		{
			if (this._closed)
			{
				return;
			}
			this._closed = true;
			this._done.TrySetResult(null);
			base.QueueFree();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000A5C6 File Offset: 0x000087C6
		public void AfterOverlayShown()
		{
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000A5C8 File Offset: 0x000087C8
		public void AfterOverlayHidden()
		{
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000A5CA File Offset: 0x000087CA
		public override void _ExitTree()
		{
			this._done.TrySetResult(null);
			base._ExitTree();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000A5E0 File Offset: 0x000087E0
		private void Dismiss()
		{
			if (this._closed)
			{
				return;
			}
			if (this._pushedToOverlay && NOverlayStack.Instance != null)
			{
				NOverlayStack.Instance.Remove(this);
				if (!this._closed && GodotObject.IsInstanceValid(this) && base.IsInsideTree())
				{
					base.QueueFree();
				}
				return;
			}
			this._closed = true;
			this._done.TrySetResult(null);
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

		// Token: 0x060000D5 RID: 213 RVA: 0x0000A66C File Offset: 0x0000886C
		private void Build()
		{
			ColorRect colorRect = new ColorRect
			{
				Color = new Color(0.04f, 0.04f, 0.06f, 0.88f),
				MouseFilter = 0L
			};
			colorRect.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			base.AddChild(colorRect, false, 0L);
			Panel panel = new Panel
			{
				CustomMinimumSize = new Vector2(980f, 520f),
				MouseFilter = 0L
			};
			panel.SetAnchorsPreset(8L, false);
			panel.GrowHorizontal = 2L;
			panel.GrowVertical = 2L;
			panel.OffsetLeft = -490f;
			panel.OffsetRight = 490f;
			panel.OffsetTop = -260f;
			panel.OffsetBottom = 260f;
			panel.AddThemeStyleboxOverride("panel", new StyleBoxFlat
			{
				BgColor = new Color(0.11f, 0.11f, 0.13f, 0.98f),
				BorderColor = new Color(1f, 1f, 1f, 0.12f),
				BorderWidthLeft = 1,
				BorderWidthTop = 1,
				BorderWidthRight = 1,
				BorderWidthBottom = 1,
				CornerRadiusTopLeft = 14,
				CornerRadiusTopRight = 14,
				CornerRadiusBottomRight = 14,
				CornerRadiusBottomLeft = 14
			});
			base.AddChild(panel, false, 0L);
			VBoxContainer vboxContainer = new VBoxContainer
			{
				MouseFilter = 2L
			};
			vboxContainer.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			vboxContainer.OffsetLeft = 28f;
			vboxContainer.OffsetRight = -28f;
			vboxContainer.OffsetTop = 24f;
			vboxContainer.OffsetBottom = -24f;
			vboxContainer.AddThemeConstantOverride("separation", 18);
			panel.AddChild(vboxContainer, false, 0L);
			Label label = new Label
			{
				Text = SpireLoc.T("SPIRE_LOOT.title", "Combat Loot"),
				HorizontalAlignment = 1L,
				MouseFilter = 2L
			};
			label.AddThemeFontSizeOverride("font_size", 28);
			vboxContainer.AddChild(label, false, 0L);
			string text = (this._levelsGained > 0) ? SpireLoc.T("SPIRE_LOOT.xp_level", "+{0} XP  (Level up!)") : SpireLoc.T("SPIRE_LOOT.xp", "+{0} XP");
			try
			{
				text = string.Format(text, this._xpGained);
			}
			catch
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
				defaultInterpolatedStringHandler.AppendLiteral("+");
				defaultInterpolatedStringHandler.AppendFormatted<int>(this._xpGained);
				defaultInterpolatedStringHandler.AppendLiteral(" XP");
				text = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			Label label2 = new Label
			{
				Text = text,
				HorizontalAlignment = 1L,
				MouseFilter = 2L
			};
			label2.AddThemeFontSizeOverride("font_size", 18);
			label2.Modulate = new Color(0.85f, 0.9f, 1f, 1f);
			vboxContainer.AddChild(label2, false, 0L);
			Label label3 = new Label
			{
				Text = SpireLoc.T("SPIRE_LOOT.hint", "Choose one item for your outside inventory"),
				HorizontalAlignment = 1L,
				MouseFilter = 2L
			};
			label3.AddThemeFontSizeOverride("font_size", 14);
			label3.Modulate = new Color(1f, 1f, 1f, 0.55f);
			vboxContainer.AddChild(label3, false, 0L);
			HBoxContainer hboxContainer = new HBoxContainer
			{
				SizeFlagsVertical = 3L,
				Alignment = 1L,
				MouseFilter = 2L
			};
			hboxContainer.AddThemeConstantOverride("separation", 16);
			vboxContainer.AddChild(hboxContainer, false, 0L);
			foreach (RpgItemData item in this._choices)
			{
				hboxContainer.AddChild(this.MakeCard(item), false, 0L);
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000AA44 File Offset: 0x00008C44
		private Control MakeCard(RpgItemData item)
		{
			Color color = RpgItemTooltip.RarityColor(item.Rarity);
			Button button = new Button
			{
				CustomMinimumSize = new Vector2(260f, 320f),
				MouseFilter = 0L
			};
			button.AddThemeStyleboxOverride("normal", RpgLootScreen.CardStyle(new Color(0.16f, 0.16f, 0.18f, 1f), color));
			button.AddThemeStyleboxOverride("hover", RpgLootScreen.CardStyle(new Color(0.22f, 0.22f, 0.25f, 1f), color));
			button.AddThemeStyleboxOverride("pressed", RpgLootScreen.CardStyle(new Color(0.14f, 0.14f, 0.16f, 1f), color));
			this._cards.Add(button);
			VBoxContainer vboxContainer = new VBoxContainer
			{
				MouseFilter = 2L
			};
			vboxContainer.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			vboxContainer.OffsetLeft = 14f;
			vboxContainer.OffsetRight = -14f;
			vboxContainer.OffsetTop = 14f;
			vboxContainer.OffsetBottom = -14f;
			vboxContainer.AddThemeConstantOverride("separation", 8);
			button.AddChild(vboxContainer, false, 0L);
			TextureRect textureRect = new TextureRect
			{
				CustomMinimumSize = new Vector2(120f, 120f),
				ExpandMode = 1L,
				StretchMode = 5L,
				SizeFlagsHorizontal = 4L,
				MouseFilter = 2L,
				Texture = SpireAssets.ItemIcon(item.IconPath)
			};
			vboxContainer.AddChild(textureRect, false, 0L);
			Label label = new Label
			{
				Text = item.DisplayName,
				HorizontalAlignment = 1L,
				AutowrapMode = 3L,
				MouseFilter = 2L
			};
			label.AddThemeFontSizeOverride("font_size", 16);
			label.AddThemeColorOverride("font_color", color);
			vboxContainer.AddChild(label, false, 0L);
			Label label2 = new Label
			{
				Text = item.LocalizedTypeLine,
				HorizontalAlignment = 1L,
				MouseFilter = 2L
			};
			label2.AddThemeFontSizeOverride("font_size", 13);
			label2.Modulate = color;
			vboxContainer.AddChild(label2, false, 0L);
			string localizedPrimaryStat = item.LocalizedPrimaryStat;
			if (!string.IsNullOrEmpty(localizedPrimaryStat))
			{
				Label label3 = new Label
				{
					Text = localizedPrimaryStat,
					HorizontalAlignment = 1L,
					MouseFilter = 2L
				};
				label3.AddThemeFontSizeOverride("font_size", 14);
				vboxContainer.AddChild(label3, false, 0L);
			}
			button.Pressed += delegate()
			{
				this.Complete(item);
			};
			return button;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000AD04 File Offset: 0x00008F04
		private void Complete(RpgItemData item)
		{
			if (this._locked)
			{
				return;
			}
			this._locked = true;
			this._done.TrySetResult(item);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000AD24 File Offset: 0x00008F24
		private static StyleBoxFlat CardStyle(Color bg, Color border)
		{
			return new StyleBoxFlat
			{
				BgColor = bg,
				BorderColor = border,
				BorderWidthLeft = 2,
				BorderWidthTop = 2,
				BorderWidthRight = 2,
				BorderWidthBottom = 2,
				CornerRadiusTopLeft = 12,
				CornerRadiusTopRight = 12,
				CornerRadiusBottomRight = 12,
				CornerRadiusBottomLeft = 12,
				ContentMarginLeft = 8f,
				ContentMarginTop = 8f,
				ContentMarginRight = 8f,
				ContentMarginBottom = 8f
			};
		}

		// Token: 0x0400005C RID: 92
		[Nullable(new byte[]
		{
			1,
			2
		})]
		private readonly TaskCompletionSource<RpgItemData> _done = new TaskCompletionSource<RpgItemData>();

		// Token: 0x0400005D RID: 93
		private readonly IReadOnlyList<RpgItemData> _choices;

		// Token: 0x0400005E RID: 94
		private readonly int _xpGained;

		// Token: 0x0400005F RID: 95
		private readonly int _levelsGained;

		// Token: 0x04000060 RID: 96
		private readonly List<Button> _cards = new List<Button>();

		// Token: 0x04000061 RID: 97
		private bool _locked;

		// Token: 0x04000062 RID: 98
		private bool _pushedToOverlay;

		// Token: 0x04000063 RID: 99
		private bool _closed;

		// Token: 0x020000B1 RID: 177
		[CompilerGenerated]
		private sealed class <>c__DisplayClass27_0
		{
			// Token: 0x0600065F RID: 1631 RVA: 0x0002059B File Offset: 0x0001E79B
			public <>c__DisplayClass27_0()
			{
			}

			// Token: 0x06000660 RID: 1632 RVA: 0x000205A3 File Offset: 0x0001E7A3
			internal void <MakeCard>b__0()
			{
				this.<>4__this.Complete(this.item);
			}

			// Token: 0x04000269 RID: 617
			[Nullable(0)]
			public RpgLootScreen <>4__this;

			// Token: 0x0400026A RID: 618
			[Nullable(0)]
			public RpgItemData item;
		}

		// Token: 0x020000B2 RID: 178
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <AttachAsync>d__18 : IAsyncStateMachine
		{
			// Token: 0x06000661 RID: 1633 RVA: 0x000205B8 File Offset: 0x0001E7B8
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
							SpireTheRpgMainFile.Logger.Info("RpgLootScreen pushed to NOverlayStack", 1);
						}
						else
						{
							NGame instance2 = NGame.Instance;
							if (instance2 != null && GodotObject.IsInstanceValid(instance2))
							{
								instance2.AddChild(this.screen, false, 0L);
								this.screen.MoveToFront();
								SpireTheRpgMainFile.Logger.Info("RpgLootScreen attached to NGame fallback", 1);
							}
							else
							{
								this.screen._done.TrySetResult(this.screen._choices[0]);
								SpireTheRpgMainFile.Logger.Info("RpgLootScreen auto-picked (no UI parent)", 1);
							}
						}
					}
					else
					{
						awaiter = RpgLootScreen.WaitOneFrame().GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgLootScreen.<AttachAsync>d__18>(ref awaiter, ref this);
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

			// Token: 0x06000662 RID: 1634 RVA: 0x00020744 File Offset: 0x0001E944
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400026B RID: 619
			public int <>1__state;

			// Token: 0x0400026C RID: 620
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400026D RID: 621
			[Nullable(0)]
			public RpgLootScreen screen;

			// Token: 0x0400026E RID: 622
			private int <i>5__2;

			// Token: 0x0400026F RID: 623
			private TaskAwaiter <>u__1;
		}

		// Token: 0x020000B3 RID: 179
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <OfferAsync>d__17 : IAsyncStateMachine
		{
			// Token: 0x06000663 RID: 1635 RVA: 0x00020754 File Offset: 0x0001E954
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					if (num > 1)
					{
						if (this.choices.Count == 0)
						{
							goto IL_146;
						}
						this.<screen>5__2 = new RpgLootScreen(this.choices, this.xpGained, this.levelsGained);
					}
					try
					{
						TaskAwaiter<RpgItemData> awaiter;
						TaskAwaiter awaiter2;
						if (num != 0)
						{
							if (num == 1)
							{
								awaiter = this.<>u__2;
								this.<>u__2 = default(TaskAwaiter<RpgItemData>);
								num = (this.<>1__state = -1);
								goto IL_100;
							}
							awaiter2 = RpgLootScreen.AttachAsync(this.<screen>5__2).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgLootScreen.<OfferAsync>d__17>(ref awaiter2, ref this);
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
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgItemData>, RpgLootScreen.<OfferAsync>d__17>(ref awaiter, ref this);
							return;
						}
						IL_100:
						RpgItemData result = awaiter.GetResult();
						if (result != null)
						{
							RpgLoadout.AddToBag(result, true);
						}
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
				IL_146:
				this.<>1__state = -2;
				this.<screen>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000664 RID: 1636 RVA: 0x000208F8 File Offset: 0x0001EAF8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000270 RID: 624
			public int <>1__state;

			// Token: 0x04000271 RID: 625
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000272 RID: 626
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public IReadOnlyList<RpgItemData> choices;

			// Token: 0x04000273 RID: 627
			public int xpGained;

			// Token: 0x04000274 RID: 628
			public int levelsGained;

			// Token: 0x04000275 RID: 629
			[Nullable(0)]
			private RpgLootScreen <screen>5__2;

			// Token: 0x04000276 RID: 630
			private TaskAwaiter <>u__1;

			// Token: 0x04000277 RID: 631
			[Nullable(new byte[]
			{
				0,
				2
			})]
			private TaskAwaiter<RpgItemData> <>u__2;
		}

		// Token: 0x020000B4 RID: 180
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WaitOneFrame>d__19 : IAsyncStateMachine
		{
			// Token: 0x06000665 RID: 1637 RVA: 0x00020908 File Offset: 0x0001EB08
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
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgLootScreen.<WaitOneFrame>d__19>(ref awaiter, ref this);
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
									this.<>t__builder.AwaitOnCompleted<IAwaiter<Variant[]>, RpgLootScreen.<WaitOneFrame>d__19>(ref awaiter2, ref this);
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

			// Token: 0x06000666 RID: 1638 RVA: 0x00020A30 File Offset: 0x0001EC30
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000278 RID: 632
			public int <>1__state;

			// Token: 0x04000279 RID: 633
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400027A RID: 634
			private TaskAwaiter <>u__1;

			// Token: 0x0400027B RID: 635
			[Nullable(0)]
			private object <>u__2;
		}
	}
}
