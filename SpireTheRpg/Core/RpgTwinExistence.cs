using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Saves;

namespace SpireTheRpg.Core
{
	// Token: 0x02000067 RID: 103
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgTwinExistence
	{
		// Token: 0x06000450 RID: 1104 RVA: 0x0001AA54 File Offset: 0x00018C54
		[return: Nullable(2)]
		public static object Begin(Player player)
		{
			if (!RpgAccessoryCombatHooks.HasActiveAccessory("twin_existence_ring", player))
			{
				return null;
			}
			if (player.Creature.IsDead)
			{
				return null;
			}
			RpgTwinExistence.Tracker tracker = new RpgTwinExistence.Tracker
			{
				Player = player,
				Previous = RpgTwinExistence.Current.Value
			};
			RpgTwinExistence.Current.Value = tracker;
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
			defaultInterpolatedStringHandler.AppendLiteral("RpgTwinExistence begin net=");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(player.NetId);
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			return tracker;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001AAE0 File Offset: 0x00018CE0
		public static Task<bool> Complete(Task<bool> original, [Nullable(2)] object state)
		{
			RpgTwinExistence.Tracker tracker = state as RpgTwinExistence.Tracker;
			if (tracker == null)
			{
				return original;
			}
			return RpgTwinExistence.CompleteAsync(original, tracker);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001AB00 File Offset: 0x00018D00
		private static Task<bool> CompleteAsync(Task<bool> original, RpgTwinExistence.Tracker scope)
		{
			RpgTwinExistence.<CompleteAsync>d__5 <CompleteAsync>d__;
			<CompleteAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<CompleteAsync>d__.original = original;
			<CompleteAsync>d__.scope = scope;
			<CompleteAsync>d__.<>1__state = -1;
			<CompleteAsync>d__.<>t__builder.Start<RpgTwinExistence.<CompleteAsync>d__5>(ref <CompleteAsync>d__);
			return <CompleteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001AB4C File Offset: 0x00018D4C
		public static void TrackAdd(CardModel card, PileType newPileType, [Nullable(2)] AbstractModel clonedBy, ref Task<CardPileAddResult> result)
		{
			RpgTwinExistence.Tracker value = RpgTwinExistence.Current.Value;
			if (value == null)
			{
				return;
			}
			if (RpgTwinExistence.SuppressDepth.Value > 0)
			{
				return;
			}
			if (clonedBy != null)
			{
				return;
			}
			if (newPileType != 6)
			{
				return;
			}
			if (card.Owner != value.Player)
			{
				return;
			}
			result = RpgTwinExistence.TrackAddAsync(result, value);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001AB98 File Offset: 0x00018D98
		private static Task<CardPileAddResult> TrackAddAsync(Task<CardPileAddResult> original, RpgTwinExistence.Tracker tracker)
		{
			RpgTwinExistence.<TrackAddAsync>d__7 <TrackAddAsync>d__;
			<TrackAddAsync>d__.<>t__builder = AsyncTaskMethodBuilder<CardPileAddResult>.Create();
			<TrackAddAsync>d__.original = original;
			<TrackAddAsync>d__.tracker = tracker;
			<TrackAddAsync>d__.<>1__state = -1;
			<TrackAddAsync>d__.<>t__builder.Start<RpgTwinExistence.<TrackAddAsync>d__7>(ref <TrackAddAsync>d__);
			return <TrackAddAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001ABE4 File Offset: 0x00018DE4
		private static Task Duplicate(Player player, IReadOnlyList<CardModel> sourceCards)
		{
			RpgTwinExistence.<Duplicate>d__8 <Duplicate>d__;
			<Duplicate>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Duplicate>d__.player = player;
			<Duplicate>d__.sourceCards = sourceCards;
			<Duplicate>d__.<>1__state = -1;
			<Duplicate>d__.<>t__builder.Start<RpgTwinExistence.<Duplicate>d__8>(ref <Duplicate>d__);
			return <Duplicate>d__.<>t__builder.Task;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001AC2F File Offset: 0x00018E2F
		public static void ClearAll()
		{
			RpgTwinExistence.Current.Value = null;
			RpgTwinExistence.SuppressDepth.Value = 0;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001AC47 File Offset: 0x00018E47
		// Note: this type is marked as 'beforefieldinit'.
		static RpgTwinExistence()
		{
		}

		// Token: 0x0400016C RID: 364
		[Nullable(new byte[]
		{
			1,
			2
		})]
		private static readonly AsyncLocal<RpgTwinExistence.Tracker> Current = new AsyncLocal<RpgTwinExistence.Tracker>();

		// Token: 0x0400016D RID: 365
		private static readonly AsyncLocal<int> SuppressDepth = new AsyncLocal<int>();

		// Token: 0x02000139 RID: 313
		[Nullable(0)]
		[RequiredMember]
		private sealed class Tracker
		{
			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x06000824 RID: 2084 RVA: 0x0002A2CE File Offset: 0x000284CE
			// (set) Token: 0x06000825 RID: 2085 RVA: 0x0002A2D6 File Offset: 0x000284D6
			[RequiredMember]
			public Player Player
			{
				[CompilerGenerated]
				get
				{
					return this.<Player>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Player>k__BackingField = value;
				}
			}

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x06000826 RID: 2086 RVA: 0x0002A2DF File Offset: 0x000284DF
			// (set) Token: 0x06000827 RID: 2087 RVA: 0x0002A2E7 File Offset: 0x000284E7
			[Nullable(2)]
			public RpgTwinExistence.Tracker Previous
			{
				[NullableContext(2)]
				[CompilerGenerated]
				get
				{
					return this.<Previous>k__BackingField;
				}
				[NullableContext(2)]
				[CompilerGenerated]
				set
				{
					this.<Previous>k__BackingField = value;
				}
			}

			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x06000828 RID: 2088 RVA: 0x0002A2F0 File Offset: 0x000284F0
			public List<CardModel> AddedCards
			{
				[CompilerGenerated]
				get
				{
					return this.<AddedCards>k__BackingField;
				}
			} = new List<CardModel>();

			// Token: 0x06000829 RID: 2089 RVA: 0x0002A2F8 File Offset: 0x000284F8
			[Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
			[CompilerFeatureRequired("RequiredMembers")]
			public Tracker()
			{
			}

			// Token: 0x0400048D RID: 1165
			[CompilerGenerated]
			private readonly Player <Player>k__BackingField;

			// Token: 0x0400048E RID: 1166
			[Nullable(2)]
			[CompilerGenerated]
			private readonly RpgTwinExistence.Tracker <Previous>k__BackingField;

			// Token: 0x0400048F RID: 1167
			[CompilerGenerated]
			private readonly List<CardModel> <AddedCards>k__BackingField;
		}

		// Token: 0x0200013A RID: 314
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <CompleteAsync>d__5 : IAsyncStateMachine
		{
			// Token: 0x0600082A RID: 2090 RVA: 0x0002A30C File Offset: 0x0002850C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result2;
				try
				{
					try
					{
						TaskAwaiter awaiter;
						TaskAwaiter<bool> awaiter2;
						if (num != 0)
						{
							if (num == 1)
							{
								awaiter = this.<>u__2;
								this.<>u__2 = default(TaskAwaiter);
								num = (this.<>1__state = -1);
								goto IL_100;
							}
							awaiter2 = this.original.GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgTwinExistence.<CompleteAsync>d__5>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter<bool>);
							num = (this.<>1__state = -1);
						}
						bool result = awaiter2.GetResult();
						this.<ok>5__2 = result;
						if (!this.<ok>5__2 || this.scope.AddedCards.Count <= 0)
						{
							goto IL_107;
						}
						awaiter = RpgTwinExistence.Duplicate(this.scope.Player, this.scope.AddedCards).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 1);
							this.<>u__2 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgTwinExistence.<CompleteAsync>d__5>(ref awaiter, ref this);
							return;
						}
						IL_100:
						awaiter.GetResult();
						IL_107:
						result2 = this.<ok>5__2;
					}
					finally
					{
						if (num < 0)
						{
							RpgTwinExistence.Current.Value = this.scope.Previous;
						}
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

			// Token: 0x0600082B RID: 2091 RVA: 0x0002A4A4 File Offset: 0x000286A4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000490 RID: 1168
			public int <>1__state;

			// Token: 0x04000491 RID: 1169
			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000492 RID: 1170
			[Nullable(0)]
			public Task<bool> original;

			// Token: 0x04000493 RID: 1171
			[Nullable(0)]
			public RpgTwinExistence.Tracker scope;

			// Token: 0x04000494 RID: 1172
			private bool <ok>5__2;

			// Token: 0x04000495 RID: 1173
			[Nullable(0)]
			private TaskAwaiter<bool> <>u__1;

			// Token: 0x04000496 RID: 1174
			private TaskAwaiter <>u__2;
		}

		// Token: 0x0200013B RID: 315
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <Duplicate>d__8 : IAsyncStateMachine
		{
			// Token: 0x0600082C RID: 2092 RVA: 0x0002A4B4 File Offset: 0x000286B4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					if (num != 0)
					{
						if (this.player.Creature.IsDead)
						{
							goto IL_21B;
						}
						AsyncLocal<int> suppressDepth = RpgTwinExistence.SuppressDepth;
						int value = suppressDepth.Value;
						suppressDepth.Value = value + 1;
					}
					try
					{
						if (num != 0)
						{
							this.<results>5__2 = new List<CardPileAddResult>();
							this.<>7__wrap2 = this.sourceCards.GetEnumerator();
						}
						try
						{
							TaskAwaiter<CardPileAddResult> awaiter;
							if (num == 0)
							{
								awaiter = this.<>u__1;
								this.<>u__1 = default(TaskAwaiter<CardPileAddResult>);
								num = (this.<>1__state = -1);
								goto IL_FF;
							}
							IL_18E:
							while (this.<>7__wrap2.MoveNext())
							{
								CardModel cardModel = this.<>7__wrap2.Current;
								if (cardModel.Owner == this.player && this.player.RunState.ContainsCard(cardModel))
								{
									awaiter = CardPileCmd.Add(this.player.RunState.CloneCard(cardModel), 6, 1, null, false).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										num = (this.<>1__state = 0);
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<CardPileAddResult>, RpgTwinExistence.<Duplicate>d__8>(ref awaiter, ref this);
										return;
									}
									goto IL_FF;
								}
							}
							goto IL_1B8;
							IL_FF:
							CardPileAddResult result = awaiter.GetResult();
							if (result.success)
							{
								try
								{
									SaveManager.Instance.MarkCardAsSeen(result.cardAdded);
								}
								catch
								{
								}
								this.<results>5__2.Add(result);
								Logger logger = SpireTheRpgMainFile.Logger;
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 2);
								defaultInterpolatedStringHandler.AppendLiteral("RpgTwinExistence duplicated net=");
								defaultInterpolatedStringHandler.AppendFormatted<ulong>(this.player.NetId);
								defaultInterpolatedStringHandler.AppendLiteral(" card=");
								defaultInterpolatedStringHandler.AppendFormatted(result.cardAdded.Id.Entry);
								logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
								goto IL_18E;
							}
							goto IL_18E;
						}
						finally
						{
							if (num < 0 && this.<>7__wrap2 != null)
							{
								this.<>7__wrap2.Dispose();
							}
						}
						IL_1B8:
						this.<>7__wrap2 = null;
						if (this.<results>5__2.Count > 0)
						{
							CardCmd.PreviewCardPileAdd(this.<results>5__2, 2f, 1);
						}
						this.<results>5__2 = null;
					}
					finally
					{
						if (num < 0)
						{
							AsyncLocal<int> suppressDepth2 = RpgTwinExistence.SuppressDepth;
							int value = suppressDepth2.Value;
							suppressDepth2.Value = value - 1;
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_21B:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600082D RID: 2093 RVA: 0x0002A754 File Offset: 0x00028954
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000497 RID: 1175
			public int <>1__state;

			// Token: 0x04000498 RID: 1176
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000499 RID: 1177
			[Nullable(0)]
			public Player player;

			// Token: 0x0400049A RID: 1178
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public IReadOnlyList<CardModel> sourceCards;

			// Token: 0x0400049B RID: 1179
			[Nullable(0)]
			private List<CardPileAddResult> <results>5__2;

			// Token: 0x0400049C RID: 1180
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private IEnumerator<CardModel> <>7__wrap2;

			// Token: 0x0400049D RID: 1181
			[Nullable(0)]
			private TaskAwaiter<CardPileAddResult> <>u__1;
		}

		// Token: 0x0200013C RID: 316
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <TrackAddAsync>d__7 : IAsyncStateMachine
		{
			// Token: 0x0600082E RID: 2094 RVA: 0x0002A764 File Offset: 0x00028964
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				CardPileAddResult result2;
				try
				{
					TaskAwaiter<CardPileAddResult> awaiter;
					if (num != 0)
					{
						awaiter = this.original.GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<CardPileAddResult>, RpgTwinExistence.<TrackAddAsync>d__7>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<CardPileAddResult>);
						this.<>1__state = -1;
					}
					CardPileAddResult result = awaiter.GetResult();
					if (result.success && result.cardAdded.Owner == this.tracker.Player)
					{
						CardPile pile = result.cardAdded.Pile;
						if (pile != null && pile.Type == 6)
						{
							this.tracker.AddedCards.Add(result.cardAdded);
						}
					}
					result2 = result;
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

			// Token: 0x0600082F RID: 2095 RVA: 0x0002A86C File Offset: 0x00028A6C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400049E RID: 1182
			public int <>1__state;

			// Token: 0x0400049F RID: 1183
			[Nullable(0)]
			public AsyncTaskMethodBuilder<CardPileAddResult> <>t__builder;

			// Token: 0x040004A0 RID: 1184
			[Nullable(0)]
			public Task<CardPileAddResult> original;

			// Token: 0x040004A1 RID: 1185
			[Nullable(0)]
			public RpgTwinExistence.Tracker tracker;

			// Token: 0x040004A2 RID: 1186
			[Nullable(0)]
			private TaskAwaiter<CardPileAddResult> <>u__1;
		}
	}
}
