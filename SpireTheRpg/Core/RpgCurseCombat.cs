using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;

namespace SpireTheRpg.Core
{
	// Token: 0x0200003B RID: 59
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgCurseCombat
	{
		// Token: 0x0600021E RID: 542 RVA: 0x000116FC File Offset: 0x0000F8FC
		public static Task ApplyEquipped(Creature creature)
		{
			RpgCurseCombat.<ApplyEquipped>d__0 <ApplyEquipped>d__;
			<ApplyEquipped>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ApplyEquipped>d__.creature = creature;
			<ApplyEquipped>d__.<>1__state = -1;
			<ApplyEquipped>d__.<>t__builder.Start<RpgCurseCombat.<ApplyEquipped>d__0>(ref <ApplyEquipped>d__);
			return <ApplyEquipped>d__.<>t__builder.Task;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00011740 File Offset: 0x0000F940
		private static Task<bool> ApplyCurse(PlayerChoiceContext ctx, Creature creature, string curse)
		{
			RpgCurseCombat.<ApplyCurse>d__1 <ApplyCurse>d__;
			<ApplyCurse>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ApplyCurse>d__.ctx = ctx;
			<ApplyCurse>d__.creature = creature;
			<ApplyCurse>d__.curse = curse;
			<ApplyCurse>d__.<>1__state = -1;
			<ApplyCurse>d__.<>t__builder.Start<RpgCurseCombat.<ApplyCurse>d__1>(ref <ApplyCurse>d__);
			return <ApplyCurse>d__.<>t__builder.Task;
		}

		// Token: 0x020000E4 RID: 228
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ApplyCurse>d__1 : IAsyncStateMachine
		{
			// Token: 0x060006DF RID: 1759 RVA: 0x000245E8 File Offset: 0x000227E8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result;
				try
				{
					TaskAwaiter<bool> awaiter;
					if (num != 0)
					{
						string a = this.curse.ToLowerInvariant();
						string text;
						if (!(a == "frail"))
						{
							if (!(a == "poison"))
							{
								if (!(a == "weak"))
								{
									if (!(a == "vulnerable"))
									{
										if (!(a == "confused"))
										{
											text = null;
										}
										else
										{
											text = null;
										}
									}
									else
									{
										text = "Vulnerable";
									}
								}
								else
								{
									text = "Weak";
								}
							}
							else
							{
								text = "Poison";
							}
						}
						else
						{
							text = "Frail";
						}
						string text2 = text;
						if (text2 == null)
						{
							result = false;
							goto IL_118;
						}
						awaiter = RpgCombatBuffApplier.ApplyByPowerName(this.ctx, this.creature, text2, 1).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgCurseCombat.<ApplyCurse>d__1>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
					}
					result = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_118:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060006E0 RID: 1760 RVA: 0x00024734 File Offset: 0x00022934
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000334 RID: 820
			public int <>1__state;

			// Token: 0x04000335 RID: 821
			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000336 RID: 822
			[Nullable(0)]
			public string curse;

			// Token: 0x04000337 RID: 823
			[Nullable(0)]
			public PlayerChoiceContext ctx;

			// Token: 0x04000338 RID: 824
			[Nullable(0)]
			public Creature creature;

			// Token: 0x04000339 RID: 825
			[Nullable(0)]
			private TaskAwaiter<bool> <>u__1;
		}

		// Token: 0x020000E5 RID: 229
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ApplyEquipped>d__0 : IAsyncStateMachine
		{
			// Token: 0x060006E1 RID: 1761 RVA: 0x00024744 File Offset: 0x00022944
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					if (num != 0)
					{
						if (!RpgEffectiveConfig.EnableCurses)
						{
							goto IL_1E0;
						}
						if (RpgNet.IsMultiplayerRun())
						{
							goto IL_1E0;
						}
						this.<ctx>5__2 = new ThrowingPlayerChoiceContext();
						this.<applied>5__3 = 0;
						this.<>7__wrap3 = RpgActiveLoadout.EquippedItems(this.creature.Player).GetEnumerator();
					}
					try
					{
						if (num == 0)
						{
							goto IL_8B;
						}
						IL_14F:
						while (this.<>7__wrap3.MoveNext())
						{
							RpgItemData rpgItemData = this.<>7__wrap3.Current;
							if (rpgItemData.IsIdentified && rpgItemData.HasCurse)
							{
								this.<>7__wrap4 = rpgItemData.Curses.GetEnumerator();
								goto IL_8B;
							}
						}
						goto IL_179;
						IL_8B:
						try
						{
							if (num != 0)
							{
								goto IL_11B;
							}
							TaskAwaiter<bool> awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter<bool>);
							num = (this.<>1__state = -1);
							IL_100:
							if (awaiter.GetResult())
							{
								int num2 = this.<applied>5__3;
								this.<applied>5__3 = num2 + 1;
							}
							IL_11B:
							if (this.<>7__wrap4.MoveNext())
							{
								string curse = this.<>7__wrap4.Current;
								awaiter = RpgCurseCombat.ApplyCurse(this.<ctx>5__2, this.creature, curse).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 0);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgCurseCombat.<ApplyEquipped>d__0>(ref awaiter, ref this);
									return;
								}
								goto IL_100;
							}
						}
						finally
						{
							if (num < 0)
							{
								((IDisposable)this.<>7__wrap4).Dispose();
							}
						}
						this.<>7__wrap4 = default(List<string>.Enumerator);
						goto IL_14F;
					}
					finally
					{
						if (num < 0 && this.<>7__wrap3 != null)
						{
							this.<>7__wrap3.Dispose();
						}
					}
					IL_179:
					this.<>7__wrap3 = null;
					if (this.<applied>5__3 > 0)
					{
						Logger logger = SpireTheRpgMainFile.Logger;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 1);
						defaultInterpolatedStringHandler.AppendLiteral("RpgCurseCombat applied=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(this.<applied>5__3);
						logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<ctx>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1E0:
				this.<>1__state = -2;
				this.<ctx>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060006E2 RID: 1762 RVA: 0x00024998 File Offset: 0x00022B98
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400033A RID: 826
			public int <>1__state;

			// Token: 0x0400033B RID: 827
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400033C RID: 828
			[Nullable(0)]
			public Creature creature;

			// Token: 0x0400033D RID: 829
			[Nullable(0)]
			private ThrowingPlayerChoiceContext <ctx>5__2;

			// Token: 0x0400033E RID: 830
			private int <applied>5__3;

			// Token: 0x0400033F RID: 831
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private IEnumerator<RpgItemData> <>7__wrap3;

			// Token: 0x04000340 RID: 832
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private List<string>.Enumerator <>7__wrap4;

			// Token: 0x04000341 RID: 833
			[Nullable(0)]
			private TaskAwaiter<bool> <>u__1;
		}
	}
}
