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
using MegaCrit.Sts2.Core.Nodes.Screens.Overlays;
using MegaCrit.Sts2.Core.Nodes.Screens.ScreenContext;
using SpireTheRpg.Core;

namespace SpireTheRpg.Ui
{
	// Token: 0x02000014 RID: 20
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RpgTradeScreen : Control, IOverlayScreen, IScreenContext
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000C292 File Offset: 0x0000A492
		public NetScreenType ScreenType
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000C296 File Offset: 0x0000A496
		public bool UseSharedBackstop
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000C299 File Offset: 0x0000A499
		[Nullable(2)]
		public Control DefaultFocusedControl
		{
			[NullableContext(2)]
			get
			{
				return this._confirm;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000C2A1 File Offset: 0x0000A4A1
		[Nullable(2)]
		public Control FocusedControlFromTopBar
		{
			[NullableContext(2)]
			get
			{
				return this.DefaultFocusedControl;
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000C2AC File Offset: 0x0000A4AC
		private RpgTradeScreen(Player owner, Player partner)
		{
			this._owner = owner;
			this._partner = partner;
			base.Name = "SpireRpgTradeScreen";
			base.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			base.MouseFilter = 0L;
			this.Build();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000C318 File Offset: 0x0000A518
		public static Task<RpgTradeScreen.PickResult> OfferAsync(Player owner, Player partner)
		{
			RpgTradeScreen.<OfferAsync>d__17 <OfferAsync>d__;
			<OfferAsync>d__.<>t__builder = AsyncTaskMethodBuilder<RpgTradeScreen.PickResult>.Create();
			<OfferAsync>d__.owner = owner;
			<OfferAsync>d__.partner = partner;
			<OfferAsync>d__.<>1__state = -1;
			<OfferAsync>d__.<>t__builder.Start<RpgTradeScreen.<OfferAsync>d__17>(ref <OfferAsync>d__);
			return <OfferAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000C364 File Offset: 0x0000A564
		private void Dismiss()
		{
			if (this._closed)
			{
				return;
			}
			this._closed = true;
			try
			{
				NOverlayStack instance = NOverlayStack.Instance;
				if (instance != null)
				{
					instance.Remove(this);
				}
			}
			catch
			{
			}
			base.QueueFree();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000C3B0 File Offset: 0x0000A5B0
		public void AfterOverlayOpened()
		{
			base.MoveToFront();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
		public void AfterOverlayClosed()
		{
			if (this._closed)
			{
				return;
			}
			this._closed = true;
			this._done.TrySetResult(new RpgTradeScreen.PickResult(false, "", ""));
			base.QueueFree();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000C3EC File Offset: 0x0000A5EC
		public void AfterOverlayShown()
		{
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000C3EE File Offset: 0x0000A5EE
		public void AfterOverlayHidden()
		{
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
		public override void _ExitTree()
		{
			this._done.TrySetResult(new RpgTradeScreen.PickResult(false, "", ""));
			base._ExitTree();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000C414 File Offset: 0x0000A614
		private void Build()
		{
			ColorRect colorRect = new ColorRect
			{
				Color = new Color(0f, 0f, 0f, 0.72f),
				MouseFilter = 0L
			};
			colorRect.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			base.AddChild(colorRect, false, 0L);
			PanelContainer panelContainer = new PanelContainer();
			panelContainer.SetAnchorsPreset(8L, false);
			panelContainer.GrowHorizontal = 2L;
			panelContainer.GrowVertical = 2L;
			panelContainer.CustomMinimumSize = new Vector2(720f, 480f);
			base.AddChild(panelContainer, false, 0L);
			VBoxContainer vboxContainer = new VBoxContainer
			{
				SizeFlagsHorizontal = 3L
			};
			vboxContainer.AddThemeConstantOverride("separation", 10);
			panelContainer.AddChild(vboxContainer, false, 0L);
			vboxContainer.AddChild(new Label
			{
				Text = SpireLoc.Item("ui.trade.title", "Trade Equipment"),
				HorizontalAlignment = 1L
			}, false, 0L);
			vboxContainer.AddChild(new Label
			{
				Text = SpireLoc.Item("ui.trade.hint", "Pick one of yours and one of theirs."),
				HorizontalAlignment = 1L
			}, false, 0L);
			HBoxContainer hboxContainer = new HBoxContainer
			{
				SizeFlagsVertical = 3L
			};
			hboxContainer.AddThemeConstantOverride("separation", 16);
			vboxContainer.AddChild(hboxContainer, false, 0L);
			Node node = hboxContainer;
			string title = SpireLoc.Item("ui.trade.yours", "Yours");
			IEnumerable<RpgItemData> source = RpgActiveLoadout.AllItems(this._owner);
			Func<RpgItemData, bool> predicate;
			if ((predicate = RpgTradeScreen.<>O.<0>__CanTrade) == null)
			{
				predicate = (RpgTradeScreen.<>O.<0>__CanTrade = new Func<RpgItemData, bool>(RpgCurseSystem.CanTrade));
			}
			node.AddChild(this.MakeList(title, from x in source.Where(predicate)
			where !x.IsScroll
			select x, true), false, 0L);
			Node node2 = hboxContainer;
			string title2 = SpireLoc.Item("ui.trade.theirs", "Theirs");
			IEnumerable<RpgItemData> source2 = RpgActiveLoadout.AllItems(this._partner);
			Func<RpgItemData, bool> predicate2;
			if ((predicate2 = RpgTradeScreen.<>O.<0>__CanTrade) == null)
			{
				predicate2 = (RpgTradeScreen.<>O.<0>__CanTrade = new Func<RpgItemData, bool>(RpgCurseSystem.CanTrade));
			}
			node2.AddChild(this.MakeList(title2, from x in source2.Where(predicate2)
			where !x.IsScroll
			select x, false), false, 0L);
			HBoxContainer hboxContainer2 = new HBoxContainer
			{
				Alignment = 1L
			};
			vboxContainer.AddChild(hboxContainer2, false, 0L);
			this._confirm = new Button
			{
				Text = SpireLoc.Item("ui.trade.confirm", "Trade"),
				Disabled = true
			};
			this._confirm.Pressed += delegate()
			{
				if (string.IsNullOrEmpty(this._mineId) || string.IsNullOrEmpty(this._theirsId))
				{
					return;
				}
				this._done.TrySetResult(new RpgTradeScreen.PickResult(true, this._mineId, this._theirsId));
			};
			hboxContainer2.AddChild(this._confirm, false, 0L);
			Button button = new Button
			{
				Text = SpireLoc.Item("ui.trade.cancel", "Cancel")
			};
			button.Pressed += delegate()
			{
				this._done.TrySetResult(new RpgTradeScreen.PickResult(false, "", ""));
			};
			hboxContainer2.AddChild(button, false, 0L);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000C6CC File Offset: 0x0000A8CC
		private Control MakeList(string title, IEnumerable<RpgItemData> items, bool mine)
		{
			VBoxContainer vboxContainer = new VBoxContainer
			{
				SizeFlagsHorizontal = 3L
			};
			vboxContainer.AddChild(new Label
			{
				Text = title
			}, false, 0L);
			ScrollContainer scrollContainer = new ScrollContainer
			{
				CustomMinimumSize = new Vector2(320f, 300f),
				SizeFlagsVertical = 3L
			};
			vboxContainer.AddChild(scrollContainer, false, 0L);
			VBoxContainer vboxContainer2 = new VBoxContainer();
			scrollContainer.AddChild(vboxContainer2, false, 0L);
			foreach (RpgItemData capture2 in items)
			{
				RpgItemData capture = capture2;
				Button button = new Button
				{
					Text = capture.DisplayName + " [" + capture.LocalizedRarity + "]",
					SizeFlagsHorizontal = 3L
				};
				button.Pressed += delegate()
				{
					if (mine)
					{
						this._mineId = capture.InstanceId;
					}
					else
					{
						this._theirsId = capture.InstanceId;
					}
					if (this._confirm != null)
					{
						this._confirm.Disabled = (string.IsNullOrEmpty(this._mineId) || string.IsNullOrEmpty(this._theirsId));
					}
				};
				vboxContainer2.AddChild(button, false, 0L);
			}
			return vboxContainer;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000C800 File Offset: 0x0000AA00
		[CompilerGenerated]
		private void <Build>b__24_2()
		{
			if (string.IsNullOrEmpty(this._mineId) || string.IsNullOrEmpty(this._theirsId))
			{
				return;
			}
			this._done.TrySetResult(new RpgTradeScreen.PickResult(true, this._mineId, this._theirsId));
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000C83B File Offset: 0x0000AA3B
		[CompilerGenerated]
		private void <Build>b__24_3()
		{
			this._done.TrySetResult(new RpgTradeScreen.PickResult(false, "", ""));
		}

		// Token: 0x04000075 RID: 117
		private readonly TaskCompletionSource<RpgTradeScreen.PickResult> _done = new TaskCompletionSource<RpgTradeScreen.PickResult>();

		// Token: 0x04000076 RID: 118
		private readonly Player _owner;

		// Token: 0x04000077 RID: 119
		private readonly Player _partner;

		// Token: 0x04000078 RID: 120
		private string _mineId = "";

		// Token: 0x04000079 RID: 121
		private string _theirsId = "";

		// Token: 0x0400007A RID: 122
		[Nullable(2)]
		private Button _confirm;

		// Token: 0x0400007B RID: 123
		private bool _closed;

		// Token: 0x020000B7 RID: 183
		[Nullable(0)]
		internal readonly struct PickResult : IEquatable<RpgTradeScreen.PickResult>
		{
			// Token: 0x0600066B RID: 1643 RVA: 0x00020AC4 File Offset: 0x0001ECC4
			public PickResult(bool Ok, string MineId, string TheirsId)
			{
				this.Ok = Ok;
				this.MineId = MineId;
				this.TheirsId = TheirsId;
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x0600066C RID: 1644 RVA: 0x00020ADB File Offset: 0x0001ECDB
			// (set) Token: 0x0600066D RID: 1645 RVA: 0x00020AE3 File Offset: 0x0001ECE3
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

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x0600066E RID: 1646 RVA: 0x00020AEC File Offset: 0x0001ECEC
			// (set) Token: 0x0600066F RID: 1647 RVA: 0x00020AF4 File Offset: 0x0001ECF4
			public string MineId
			{
				[CompilerGenerated]
				get
				{
					return this.<MineId>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<MineId>k__BackingField = value;
				}
			}

			// Token: 0x1700018B RID: 395
			// (get) Token: 0x06000670 RID: 1648 RVA: 0x00020AFD File Offset: 0x0001ECFD
			// (set) Token: 0x06000671 RID: 1649 RVA: 0x00020B05 File Offset: 0x0001ED05
			public string TheirsId
			{
				[CompilerGenerated]
				get
				{
					return this.<TheirsId>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<TheirsId>k__BackingField = value;
				}
			}

			// Token: 0x06000672 RID: 1650 RVA: 0x00020B10 File Offset: 0x0001ED10
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

			// Token: 0x06000673 RID: 1651 RVA: 0x00020B5C File Offset: 0x0001ED5C
			[NullableContext(0)]
			[CompilerGenerated]
			private bool PrintMembers(StringBuilder builder)
			{
				builder.Append("Ok = ");
				builder.Append(this.Ok.ToString());
				builder.Append(", MineId = ");
				builder.Append(this.MineId);
				builder.Append(", TheirsId = ");
				builder.Append(this.TheirsId);
				return true;
			}

			// Token: 0x06000674 RID: 1652 RVA: 0x00020BC3 File Offset: 0x0001EDC3
			[CompilerGenerated]
			public static bool operator !=(RpgTradeScreen.PickResult left, RpgTradeScreen.PickResult right)
			{
				return !(left == right);
			}

			// Token: 0x06000675 RID: 1653 RVA: 0x00020BCF File Offset: 0x0001EDCF
			[CompilerGenerated]
			public static bool operator ==(RpgTradeScreen.PickResult left, RpgTradeScreen.PickResult right)
			{
				return left.Equals(right);
			}

			// Token: 0x06000676 RID: 1654 RVA: 0x00020BD9 File Offset: 0x0001EDD9
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return (EqualityComparer<bool>.Default.GetHashCode(this.<Ok>k__BackingField) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<MineId>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<TheirsId>k__BackingField);
			}

			// Token: 0x06000677 RID: 1655 RVA: 0x00020C19 File Offset: 0x0001EE19
			[NullableContext(0)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return obj is RpgTradeScreen.PickResult && this.Equals((RpgTradeScreen.PickResult)obj);
			}

			// Token: 0x06000678 RID: 1656 RVA: 0x00020C34 File Offset: 0x0001EE34
			[CompilerGenerated]
			public bool Equals(RpgTradeScreen.PickResult other)
			{
				return EqualityComparer<bool>.Default.Equals(this.<Ok>k__BackingField, other.<Ok>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<MineId>k__BackingField, other.<MineId>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<TheirsId>k__BackingField, other.<TheirsId>k__BackingField);
			}

			// Token: 0x06000679 RID: 1657 RVA: 0x00020C89 File Offset: 0x0001EE89
			[CompilerGenerated]
			public void Deconstruct(out bool Ok, out string MineId, out string TheirsId)
			{
				Ok = this.Ok;
				MineId = this.MineId;
				TheirsId = this.TheirsId;
			}

			// Token: 0x04000280 RID: 640
			[CompilerGenerated]
			private readonly bool <Ok>k__BackingField;

			// Token: 0x04000281 RID: 641
			[CompilerGenerated]
			private readonly string <MineId>k__BackingField;

			// Token: 0x04000282 RID: 642
			[CompilerGenerated]
			private readonly string <TheirsId>k__BackingField;
		}

		// Token: 0x020000B8 RID: 184
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000283 RID: 643
			[Nullable(0)]
			public static Func<RpgItemData, bool> <0>__CanTrade;
		}

		// Token: 0x020000B9 RID: 185
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600067A RID: 1658 RVA: 0x00020CA3 File Offset: 0x0001EEA3
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600067B RID: 1659 RVA: 0x00020CAF File Offset: 0x0001EEAF
			public <>c()
			{
			}

			// Token: 0x0600067C RID: 1660 RVA: 0x00020CB7 File Offset: 0x0001EEB7
			[NullableContext(0)]
			internal bool <Build>b__24_0(RpgItemData x)
			{
				return !x.IsScroll;
			}

			// Token: 0x0600067D RID: 1661 RVA: 0x00020CC2 File Offset: 0x0001EEC2
			[NullableContext(0)]
			internal bool <Build>b__24_1(RpgItemData x)
			{
				return !x.IsScroll;
			}

			// Token: 0x04000284 RID: 644
			[Nullable(0)]
			public static readonly RpgTradeScreen.<>c <>9 = new RpgTradeScreen.<>c();

			// Token: 0x04000285 RID: 645
			[Nullable(0)]
			public static Func<RpgItemData, bool> <>9__24_0;

			// Token: 0x04000286 RID: 646
			[Nullable(0)]
			public static Func<RpgItemData, bool> <>9__24_1;
		}

		// Token: 0x020000BA RID: 186
		[CompilerGenerated]
		private sealed class <>c__DisplayClass25_0
		{
			// Token: 0x0600067E RID: 1662 RVA: 0x00020CCD File Offset: 0x0001EECD
			public <>c__DisplayClass25_0()
			{
			}

			// Token: 0x04000287 RID: 647
			public bool mine;

			// Token: 0x04000288 RID: 648
			[Nullable(0)]
			public RpgTradeScreen <>4__this;
		}

		// Token: 0x020000BB RID: 187
		[CompilerGenerated]
		private sealed class <>c__DisplayClass25_1
		{
			// Token: 0x0600067F RID: 1663 RVA: 0x00020CD5 File Offset: 0x0001EED5
			public <>c__DisplayClass25_1()
			{
			}

			// Token: 0x06000680 RID: 1664 RVA: 0x00020CE0 File Offset: 0x0001EEE0
			internal void <MakeList>b__0()
			{
				if (this.CS$<>8__locals1.mine)
				{
					this.CS$<>8__locals1.<>4__this._mineId = this.capture.InstanceId;
				}
				else
				{
					this.CS$<>8__locals1.<>4__this._theirsId = this.capture.InstanceId;
				}
				if (this.CS$<>8__locals1.<>4__this._confirm != null)
				{
					this.CS$<>8__locals1.<>4__this._confirm.Disabled = (string.IsNullOrEmpty(this.CS$<>8__locals1.<>4__this._mineId) || string.IsNullOrEmpty(this.CS$<>8__locals1.<>4__this._theirsId));
				}
			}

			// Token: 0x04000289 RID: 649
			[Nullable(0)]
			public RpgItemData capture;

			// Token: 0x0400028A RID: 650
			[Nullable(0)]
			public RpgTradeScreen.<>c__DisplayClass25_0 CS$<>8__locals1;
		}

		// Token: 0x020000BC RID: 188
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <OfferAsync>d__17 : IAsyncStateMachine
		{
			// Token: 0x06000681 RID: 1665 RVA: 0x00020D88 File Offset: 0x0001EF88
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				RpgTradeScreen.PickResult result;
				try
				{
					if (num > 1)
					{
						this.<screen>5__2 = new RpgTradeScreen(this.owner, this.partner);
					}
					try
					{
						TaskAwaiter<RpgTradeScreen.PickResult> awaiter;
						TaskAwaiter awaiter2;
						if (num != 0)
						{
							if (num != 1)
							{
								this.<i>5__3 = 0;
								goto IL_A2;
							}
							awaiter = this.<>u__2;
							this.<>u__2 = default(TaskAwaiter<RpgTradeScreen.PickResult>);
							num = (this.<>1__state = -1);
							goto IL_127;
						}
						else
						{
							awaiter2 = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							num = (this.<>1__state = -1);
						}
						IL_8B:
						awaiter2.GetResult();
						int num2 = this.<i>5__3;
						this.<i>5__3 = num2 + 1;
						IL_A2:
						if (this.<i>5__3 >= 120 || NOverlayStack.Instance != null)
						{
							NOverlayStack instance = NOverlayStack.Instance;
							if (instance != null)
							{
								instance.Push(this.<screen>5__2);
							}
							awaiter = this.<screen>5__2._done.Task.GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 1);
								this.<>u__2 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgTradeScreen.PickResult>, RpgTradeScreen.<OfferAsync>d__17>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = Task.Delay(16).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgTradeScreen.<OfferAsync>d__17>(ref awaiter2, ref this);
								return;
							}
							goto IL_8B;
						}
						IL_127:
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
				this.<>1__state = -2;
				this.<screen>5__2 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000682 RID: 1666 RVA: 0x00020F48 File Offset: 0x0001F148
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400028B RID: 651
			public int <>1__state;

			// Token: 0x0400028C RID: 652
			[Nullable(0)]
			public AsyncTaskMethodBuilder<RpgTradeScreen.PickResult> <>t__builder;

			// Token: 0x0400028D RID: 653
			[Nullable(0)]
			public Player owner;

			// Token: 0x0400028E RID: 654
			[Nullable(0)]
			public Player partner;

			// Token: 0x0400028F RID: 655
			[Nullable(0)]
			private RpgTradeScreen <screen>5__2;

			// Token: 0x04000290 RID: 656
			private int <i>5__3;

			// Token: 0x04000291 RID: 657
			private TaskAwaiter <>u__1;

			// Token: 0x04000292 RID: 658
			[Nullable(0)]
			private TaskAwaiter<RpgTradeScreen.PickResult> <>u__2;
		}
	}
}
