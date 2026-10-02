using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;
using SpireTheRpg.Cards;

namespace SpireTheRpg.Core
{
	// Token: 0x02000040 RID: 64
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgFinalFormMirror
	{
		// Token: 0x0600025A RID: 602 RVA: 0x00011DD6 File Offset: 0x0000FFD6
		public static void ResetForNewRun()
		{
			RpgFinalFormMirror.WinsByNet.Clear();
			RpgFinalFormMirror.Granted.Clear();
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00011DEC File Offset: 0x0000FFEC
		public static Task OnCombatVictory(Player player)
		{
			RpgFinalFormMirror.<OnCombatVictory>d__5 <OnCombatVictory>d__;
			<OnCombatVictory>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<OnCombatVictory>d__.player = player;
			<OnCombatVictory>d__.<>1__state = -1;
			<OnCombatVictory>d__.<>t__builder.Start<RpgFinalFormMirror.<OnCombatVictory>d__5>(ref <OnCombatVictory>d__);
			return <OnCombatVictory>d__.<>t__builder.Task;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00011E2F File Offset: 0x0001002F
		// Note: this type is marked as 'beforefieldinit'.
		static RpgFinalFormMirror()
		{
		}

		// Token: 0x040000B2 RID: 178
		public const string AccessoryId = "ironclad_final_form_mirror";

		// Token: 0x040000B3 RID: 179
		public const int WinsRequired = 3;

		// Token: 0x040000B4 RID: 180
		private static readonly Dictionary<int, int> WinsByNet = new Dictionary<int, int>();

		// Token: 0x040000B5 RID: 181
		private static readonly HashSet<int> Granted = new HashSet<int>();

		// Token: 0x020000EB RID: 235
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006F0 RID: 1776 RVA: 0x00024F76 File Offset: 0x00023176
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006F1 RID: 1777 RVA: 0x00024F82 File Offset: 0x00023182
			public <>c()
			{
			}

			// Token: 0x060006F2 RID: 1778 RVA: 0x00024F8A File Offset: 0x0002318A
			[NullableContext(0)]
			internal bool <OnCombatVictory>b__5_0(CardModel c)
			{
				return c.Type == 1 && c.Tags.Contains(1);
			}

			// Token: 0x060006F3 RID: 1779 RVA: 0x00024FA3 File Offset: 0x000231A3
			[NullableContext(0)]
			internal int <OnCombatVictory>b__5_1(CardModel c)
			{
				return (c.IsUpgraded > false) ? 1 : 0;
			}

			// Token: 0x060006F4 RID: 1780 RVA: 0x00024FAE File Offset: 0x000231AE
			[NullableContext(0)]
			internal int <OnCombatVictory>b__5_2(CardModel c)
			{
				return c.EnergyCost.GetResolved();
			}

			// Token: 0x04000355 RID: 853
			[Nullable(0)]
			public static readonly RpgFinalFormMirror.<>c <>9 = new RpgFinalFormMirror.<>c();

			// Token: 0x04000356 RID: 854
			[Nullable(0)]
			public static Func<CardModel, bool> <>9__5_0;

			// Token: 0x04000357 RID: 855
			[Nullable(0)]
			public static Func<CardModel, int> <>9__5_1;

			// Token: 0x04000358 RID: 856
			[Nullable(0)]
			public static Func<CardModel, int> <>9__5_2;
		}

		// Token: 0x020000EC RID: 236
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <OnCombatVictory>d__5 : IAsyncStateMachine
		{
			// Token: 0x060006F5 RID: 1781 RVA: 0x00024FBC File Offset: 0x000231BC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					try
					{
						TaskAwaiter<CardPileAddResult> awaiter;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
						TaskAwaiter awaiter2;
						if (num != 0)
						{
							if (num == 1)
							{
								awaiter = this.<>u__2;
								this.<>u__2 = default(TaskAwaiter<CardPileAddResult>);
								this.<>1__state = -1;
								goto IL_2B2;
							}
							IRunState runState = this.player.RunState;
							this.<run>5__2 = (runState as RunState);
							if (this.<run>5__2 == null)
							{
								goto IL_368;
							}
							this.<netId>5__3 = (int)this.player.NetId;
							if (RpgFinalFormMirror.Granted.Contains(this.<netId>5__3))
							{
								goto IL_368;
							}
							if (!RpgAccessoryCombatHooks.HasActiveAccessory("ironclad_final_form_mirror", this.player))
							{
								goto IL_368;
							}
							int num2;
							RpgFinalFormMirror.WinsByNet.TryGetValue(this.<netId>5__3, out num2);
							num2++;
							RpgFinalFormMirror.WinsByNet[this.<netId>5__3] = num2;
							RpgAccessoryRunState.CombatWinsThisRun = num2;
							Logger logger = SpireTheRpgMainFile.Logger;
							defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 3);
							defaultInterpolatedStringHandler.AppendLiteral("RpgFinalFormMirror wins=");
							defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
							defaultInterpolatedStringHandler.AppendLiteral("/");
							defaultInterpolatedStringHandler.AppendFormatted<int>(3);
							defaultInterpolatedStringHandler.AppendLiteral(" net=");
							defaultInterpolatedStringHandler.AppendFormatted<int>(this.<netId>5__3);
							logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
							if (num2 < 3)
							{
								goto IL_368;
							}
							this.<strike>5__4 = this.player.Deck.Cards.Where(new Func<CardModel, bool>(RpgFinalFormMirror.<>c.<>9.<OnCombatVictory>b__5_0)).OrderBy(new Func<CardModel, int>(RpgFinalFormMirror.<>c.<>9.<OnCombatVictory>b__5_1)).ThenBy(new Func<CardModel, int>(RpgFinalFormMirror.<>c.<>9.<OnCombatVictory>b__5_2)).FirstOrDefault<CardModel>();
							if (this.<strike>5__4 == null)
							{
								Logger logger2 = SpireTheRpgMainFile.Logger;
								defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(41, 1);
								defaultInterpolatedStringHandler.AppendLiteral("RpgFinalFormMirror no Strike in deck net=");
								defaultInterpolatedStringHandler.AppendFormatted<int>(this.<netId>5__3);
								logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
								goto IL_368;
							}
							awaiter2 = CardPileCmd.RemoveFromDeck(this.<strike>5__4, false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgFinalFormMirror.<OnCombatVictory>d__5>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							this.<>1__state = -1;
						}
						awaiter2.GetResult();
						awaiter = CardPileCmd.Add(this.<run>5__2.CreateCard<StrikeForm>(this.player), this.player.Deck, 1, null, false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__2 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<CardPileAddResult>, RpgFinalFormMirror.<OnCombatVictory>d__5>(ref awaiter, ref this);
							return;
						}
						IL_2B2:
						awaiter.GetResult();
						RpgFinalFormMirror.Granted.Add(this.<netId>5__3);
						Logger logger3 = SpireTheRpgMainFile.Logger;
						defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(52, 2);
						defaultInterpolatedStringHandler.AppendLiteral("RpgFinalFormMirror granted StrikeForm replaced=");
						defaultInterpolatedStringHandler.AppendFormatted<ModelId>(this.<strike>5__4.Id);
						defaultInterpolatedStringHandler.AppendLiteral(" net=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(this.<netId>5__3);
						logger3.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
						this.<run>5__2 = null;
						this.<strike>5__4 = null;
					}
					catch (Exception ex)
					{
						SpireTheRpgMainFile.Logger.Info("RpgFinalFormMirror failed: " + ex.Message, 1);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_368:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060006F6 RID: 1782 RVA: 0x00025378 File Offset: 0x00023578
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000359 RID: 857
			public int <>1__state;

			// Token: 0x0400035A RID: 858
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400035B RID: 859
			[Nullable(0)]
			public Player player;

			// Token: 0x0400035C RID: 860
			[Nullable(0)]
			private RunState <run>5__2;

			// Token: 0x0400035D RID: 861
			private int <netId>5__3;

			// Token: 0x0400035E RID: 862
			[Nullable(0)]
			private CardModel <strike>5__4;

			// Token: 0x0400035F RID: 863
			private TaskAwaiter <>u__1;

			// Token: 0x04000360 RID: 864
			[Nullable(0)]
			private TaskAwaiter<CardPileAddResult> <>u__2;
		}
	}
}
