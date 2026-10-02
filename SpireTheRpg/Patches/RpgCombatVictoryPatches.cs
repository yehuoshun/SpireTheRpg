using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using SpireTheRpg.Combat;
using SpireTheRpg.Core;

namespace SpireTheRpg.Patches
{
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	[Nullable(0)]
	[HarmonyPatch]
	internal static class RpgCombatVictoryPatches
	{
		// Token: 0x06000171 RID: 369 RVA: 0x0000E190 File Offset: 0x0000C390
		[HarmonyPatch(typeof(Hook), "AfterCombatVictory")]
		[HarmonyPostfix]
		private static void AfterCombatVictoryPostfix(IRunState runState, [Nullable(2)] ICombatState combatState, CombatRoom room)
		{
			RpgLootTable.NoteSharedVictory();
			RpgLoadoutNetSync.ClearCombatSync();
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(24, 1);
			defaultInterpolatedStringHandler.AppendLiteral("AfterCombatVictory room=");
			defaultInterpolatedStringHandler.AppendFormatted<RoomType>(room.RoomType);
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			RpgCombatVictoryPatches.AfterVictoryAsync(runState);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000E1E4 File Offset: 0x0000C3E4
		private static Task AfterVictoryAsync(IRunState runState)
		{
			RpgCombatVictoryPatches.<AfterVictoryAsync>d__1 <AfterVictoryAsync>d__;
			<AfterVictoryAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AfterVictoryAsync>d__.runState = runState;
			<AfterVictoryAsync>d__.<>1__state = -1;
			<AfterVictoryAsync>d__.<>t__builder.Start<RpgCombatVictoryPatches.<AfterVictoryAsync>d__1>(ref <AfterVictoryAsync>d__);
			return <AfterVictoryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x020000C6 RID: 198
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <AfterVictoryAsync>d__1 : IAsyncStateMachine
		{
			// Token: 0x06000693 RID: 1683 RVA: 0x00021D2C File Offset: 0x0001FF2C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					TaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							goto IL_84;
						}
						awaiter = RpgCombatRewards.EnsureRelics(this.runState).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgCombatVictoryPatches.<AfterVictoryAsync>d__1>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter);
						num = (this.<>1__state = -1);
					}
					awaiter.GetResult();
					this.<>7__wrap1 = this.runState.Players.GetEnumerator();
					IL_84:
					try
					{
						if (num != 1)
						{
							goto IL_ED;
						}
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter);
						num = (this.<>1__state = -1);
						IL_E6:
						awaiter.GetResult();
						IL_ED:
						if (this.<>7__wrap1.MoveNext())
						{
							awaiter = RpgFinalFormMirror.OnCombatVictory(this.<>7__wrap1.Current).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 1);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgCombatVictoryPatches.<AfterVictoryAsync>d__1>(ref awaiter, ref this);
								return;
							}
							goto IL_E6;
						}
					}
					finally
					{
						if (num < 0 && this.<>7__wrap1 != null)
						{
							this.<>7__wrap1.Dispose();
						}
					}
					this.<>7__wrap1 = null;
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

			// Token: 0x06000694 RID: 1684 RVA: 0x00021EB4 File Offset: 0x000200B4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040002B7 RID: 695
			public int <>1__state;

			// Token: 0x040002B8 RID: 696
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040002B9 RID: 697
			[Nullable(0)]
			public IRunState runState;

			// Token: 0x040002BA RID: 698
			private TaskAwaiter <>u__1;

			// Token: 0x040002BB RID: 699
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private IEnumerator<Player> <>7__wrap1;
		}
	}
}
