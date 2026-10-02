using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Runs;

namespace SpireTheRpg.Core
{
	// Token: 0x02000041 RID: 65
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgHostConfigSync
	{
		// Token: 0x0600025D RID: 605 RVA: 0x00011E48 File Offset: 0x00010048
		public static Task EnsureSynced(RunState run)
		{
			RpgHostConfigSync.<EnsureSynced>d__1 <EnsureSynced>d__;
			<EnsureSynced>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EnsureSynced>d__.run = run;
			<EnsureSynced>d__.<>1__state = -1;
			<EnsureSynced>d__.<>t__builder.Start<RpgHostConfigSync.<EnsureSynced>d__1>(ref <EnsureSynced>d__);
			return <EnsureSynced>d__.<>t__builder.Task;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00011E8C File Offset: 0x0001008C
		[NullableContext(2)]
		public static void Clear(RunState run = null)
		{
			RpgEffectiveConfig.ClearRunSnapshot();
			HashSet<RunState> synced = RpgHostConfigSync.Synced;
			lock (synced)
			{
				if (run != null)
				{
					RpgHostConfigSync.Synced.Remove(run);
				}
				else
				{
					RpgHostConfigSync.Synced.Clear();
				}
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00011EE8 File Offset: 0x000100E8
		private static bool LocalContextIsAuthority(RunManager runManager, Player authority)
		{
			bool result;
			try
			{
				result = (authority.NetId == runManager.NetService.NetId);
			}
			catch
			{
				result = (runManager.NetService.Type == 2);
			}
			return result;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00011F30 File Offset: 0x00010130
		private static void TrySync(PlayerChoiceSynchronizer sync, Player player, uint choiceId, PlayerChoiceResult result)
		{
			try
			{
				sync.SyncLocalChoice(player, choiceId, result);
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RpgHostConfigSync sync failed: " + ex.Message, 1);
				try
				{
					uint num = sync.ReserveChoiceId(player);
					sync.SyncLocalChoice(player, num, result);
				}
				catch (Exception ex2)
				{
					SpireTheRpgMainFile.Logger.Info("RpgHostConfigSync sync retry failed: " + ex2.Message, 1);
				}
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00011FB4 File Offset: 0x000101B4
		[return: Nullable(2)]
		private static Player GetAuthorityPlayer(RunManager runManager, IRunState runState)
		{
			if (runManager.NetService.Type == 2)
			{
				return runState.Players.FirstOrDefault((Player p) => p.NetId == runManager.NetService.NetId);
			}
			return runState.Players.FirstOrDefault<Player>();
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00012004 File Offset: 0x00010204
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		private static Task<PlayerChoiceSynchronizer> WaitForSynchronizer(RunManager runManager)
		{
			RpgHostConfigSync.<WaitForSynchronizer>d__6 <WaitForSynchronizer>d__;
			<WaitForSynchronizer>d__.<>t__builder = AsyncTaskMethodBuilder<PlayerChoiceSynchronizer>.Create();
			<WaitForSynchronizer>d__.runManager = runManager;
			<WaitForSynchronizer>d__.<>1__state = -1;
			<WaitForSynchronizer>d__.<>t__builder.Start<RpgHostConfigSync.<WaitForSynchronizer>d__6>(ref <WaitForSynchronizer>d__);
			return <WaitForSynchronizer>d__.<>t__builder.Task;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00012048 File Offset: 0x00010248
		private static Task WaitFrame()
		{
			RpgHostConfigSync.<WaitFrame>d__7 <WaitFrame>d__;
			<WaitFrame>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WaitFrame>d__.<>1__state = -1;
			<WaitFrame>d__.<>t__builder.Start<RpgHostConfigSync.<WaitFrame>d__7>(ref <WaitFrame>d__);
			return <WaitFrame>d__.<>t__builder.Task;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00012083 File Offset: 0x00010283
		// Note: this type is marked as 'beforefieldinit'.
		static RpgHostConfigSync()
		{
		}

		// Token: 0x040000B6 RID: 182
		private static readonly HashSet<RunState> Synced = new HashSet<RunState>(ReferenceEqualityComparer.Instance);

		// Token: 0x020000ED RID: 237
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x060006F7 RID: 1783 RVA: 0x00025386 File Offset: 0x00023586
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x060006F8 RID: 1784 RVA: 0x0002538E File Offset: 0x0002358E
			[NullableContext(0)]
			internal bool <GetAuthorityPlayer>b__0(Player p)
			{
				return p.NetId == this.runManager.NetService.NetId;
			}

			// Token: 0x04000361 RID: 865
			[Nullable(0)]
			public RunManager runManager;
		}

		// Token: 0x020000EE RID: 238
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <EnsureSynced>d__1 : IAsyncStateMachine
		{
			// Token: 0x060006F9 RID: 1785 RVA: 0x000253A8 File Offset: 0x000235A8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					if (num > 1)
					{
						HashSet<RunState> synced = RpgHostConfigSync.Synced;
						bool flag = false;
						try
						{
							Monitor.Enter(synced, ref flag);
							if (!RpgHostConfigSync.Synced.Add(this.run))
							{
								goto IL_363;
							}
						}
						finally
						{
							if (num < 0 && flag)
							{
								Monitor.Exit(synced);
							}
						}
					}
					try
					{
						TaskAwaiter<PlayerChoiceResult> awaiter;
						TaskAwaiter<PlayerChoiceSynchronizer> awaiter2;
						if (num != 0)
						{
							if (num == 1)
							{
								awaiter = this.<>u__2;
								this.<>u__2 = default(TaskAwaiter<PlayerChoiceResult>);
								num = (this.<>1__state = -1);
								goto IL_24F;
							}
							this.<runManager>5__2 = RunManager.Instance;
							if (this.<runManager>5__2 == null || this.<runManager>5__2.NetService.Type == 1)
							{
								RpgEffectiveConfig.SetRunSnapshot(RpgRunConfigSnapshot.FromLocal());
								Logger logger = SpireTheRpgMainFile.Logger;
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 1);
								defaultInterpolatedStringHandler.AppendLiteral("RpgHostConfigSync local meta=");
								defaultInterpolatedStringHandler.AppendFormatted<bool>(RpgEffectiveConfig.EnableMetaEquipment);
								logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
								goto IL_363;
							}
							awaiter2 = RpgHostConfigSync.WaitForSynchronizer(this.<runManager>5__2).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<PlayerChoiceSynchronizer>, RpgHostConfigSync.<EnsureSynced>d__1>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter<PlayerChoiceSynchronizer>);
							num = (this.<>1__state = -1);
						}
						PlayerChoiceSynchronizer result = awaiter2.GetResult();
						Player authorityPlayer = RpgHostConfigSync.GetAuthorityPlayer(this.<runManager>5__2, this.run);
						if (result == null || authorityPlayer == null)
						{
							RpgEffectiveConfig.SetRunSnapshot(RpgRunConfigSnapshot.FromLocal());
							SpireTheRpgMainFile.Logger.Info("RpgHostConfigSync fallback: no sync/authority", 1);
							goto IL_363;
						}
						uint num2 = result.ReserveChoiceId(authorityPlayer);
						if (RpgHostConfigSync.LocalContextIsAuthority(this.<runManager>5__2, authorityPlayer))
						{
							RpgRunConfigSnapshot rpgRunConfigSnapshot = RpgRunConfigSnapshot.FromLocal();
							RpgEffectiveConfig.SetRunSnapshot(rpgRunConfigSnapshot);
							PlayerChoiceResult result2 = PlayerChoiceResult.FromIndexes(rpgRunConfigSnapshot.Encode());
							RpgHostConfigSync.TrySync(result, authorityPlayer, num2, result2);
							Logger logger2 = SpireTheRpgMainFile.Logger;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(41, 2);
							defaultInterpolatedStringHandler.AppendLiteral("RpgHostConfigSync host pushed meta=");
							defaultInterpolatedStringHandler.AppendFormatted<bool>(rpgRunConfigSnapshot.EnableMetaEquipment);
							defaultInterpolatedStringHandler.AppendLiteral(" drop=");
							defaultInterpolatedStringHandler.AppendFormatted<double>(rpgRunConfigSnapshot.MonsterDropChance, "P0");
							logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
							goto IL_363;
						}
						awaiter = result.WaitForRemoteChoice(authorityPlayer, num2).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 1);
							this.<>u__2 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<PlayerChoiceResult>, RpgHostConfigSync.<EnsureSynced>d__1>(ref awaiter, ref this);
							return;
						}
						IL_24F:
						List<int> list = awaiter.GetResult().AsIndexes();
						RpgRunConfigSnapshot rpgRunConfigSnapshot2;
						if (list != null && RpgRunConfigSnapshot.TryDecode(list, out rpgRunConfigSnapshot2))
						{
							RpgEffectiveConfig.SetRunSnapshot(rpgRunConfigSnapshot2);
							Logger logger3 = SpireTheRpgMainFile.Logger;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 2);
							defaultInterpolatedStringHandler.AppendLiteral("RpgHostConfigSync client applied meta=");
							defaultInterpolatedStringHandler.AppendFormatted<bool>(rpgRunConfigSnapshot2.EnableMetaEquipment);
							defaultInterpolatedStringHandler.AppendLiteral(" drop=");
							defaultInterpolatedStringHandler.AppendFormatted<double>(rpgRunConfigSnapshot2.MonsterDropChance, "P0");
							logger3.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
						}
						else
						{
							RpgEffectiveConfig.SetRunSnapshot(RpgRunConfigSnapshot.FromLocal());
							SpireTheRpgMainFile.Logger.Info("RpgHostConfigSync client decode failed, using local", 1);
						}
						this.<runManager>5__2 = null;
					}
					catch (Exception ex)
					{
						HashSet<RunState> synced = RpgHostConfigSync.Synced;
						bool flag = false;
						try
						{
							Monitor.Enter(synced, ref flag);
							RpgHostConfigSync.Synced.Remove(this.run);
						}
						finally
						{
							if (num < 0 && flag)
							{
								Monitor.Exit(synced);
							}
						}
						RpgEffectiveConfig.SetRunSnapshot(RpgRunConfigSnapshot.FromLocal());
						SpireTheRpgMainFile.Logger.Info("RpgHostConfigSync failed: " + ex.Message, 1);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_363:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060006FA RID: 1786 RVA: 0x00025790 File Offset: 0x00023990
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000362 RID: 866
			public int <>1__state;

			// Token: 0x04000363 RID: 867
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000364 RID: 868
			[Nullable(0)]
			public RunState run;

			// Token: 0x04000365 RID: 869
			[Nullable(0)]
			private RunManager <runManager>5__2;

			// Token: 0x04000366 RID: 870
			[Nullable(new byte[]
			{
				0,
				2
			})]
			private TaskAwaiter<PlayerChoiceSynchronizer> <>u__1;

			// Token: 0x04000367 RID: 871
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private TaskAwaiter<PlayerChoiceResult> <>u__2;
		}

		// Token: 0x020000EF RID: 239
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WaitForSynchronizer>d__6 : IAsyncStateMachine
		{
			// Token: 0x060006FB RID: 1787 RVA: 0x000257A0 File Offset: 0x000239A0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				PlayerChoiceSynchronizer playerChoiceSynchronizer;
				try
				{
					if (num != 0)
					{
						this.<i>5__2 = 0;
						goto IL_9B;
					}
					TaskAwaiter awaiter = this.<>u__1;
					this.<>u__1 = default(TaskAwaiter);
					this.<>1__state = -1;
					IL_84:
					awaiter.GetResult();
					int num2 = this.<i>5__2;
					this.<i>5__2 = num2 + 1;
					IL_9B:
					if (this.<i>5__2 >= 180)
					{
						playerChoiceSynchronizer = this.runManager.PlayerChoiceSynchronizer;
					}
					else if (this.runManager.PlayerChoiceSynchronizer != null)
					{
						playerChoiceSynchronizer = this.runManager.PlayerChoiceSynchronizer;
					}
					else
					{
						awaiter = RpgHostConfigSync.WaitFrame().GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgHostConfigSync.<WaitForSynchronizer>d__6>(ref awaiter, ref this);
							return;
						}
						goto IL_84;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(playerChoiceSynchronizer);
			}

			// Token: 0x060006FC RID: 1788 RVA: 0x000258A4 File Offset: 0x00023AA4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000368 RID: 872
			public int <>1__state;

			// Token: 0x04000369 RID: 873
			[Nullable(0)]
			public AsyncTaskMethodBuilder<PlayerChoiceSynchronizer> <>t__builder;

			// Token: 0x0400036A RID: 874
			[Nullable(0)]
			public RunManager runManager;

			// Token: 0x0400036B RID: 875
			private int <i>5__2;

			// Token: 0x0400036C RID: 876
			private TaskAwaiter <>u__1;
		}

		// Token: 0x020000F0 RID: 240
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WaitFrame>d__7 : IAsyncStateMachine
		{
			// Token: 0x060006FD RID: 1789 RVA: 0x000258B4 File Offset: 0x00023AB4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					IAwaiter<Variant[]> awaiter;
					if (num != 0)
					{
						TaskAwaiter awaiter2;
						if (num != 1)
						{
							SceneTree sceneTree = Engine.GetMainLoop() as SceneTree;
							if (sceneTree != null)
							{
								awaiter = sceneTree.ToSignal(sceneTree, SceneTree.SignalName.ProcessFrame).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitOnCompleted<IAwaiter<Variant[]>, RpgHostConfigSync.<WaitFrame>d__7>(ref awaiter, ref this);
									return;
								}
								goto IL_78;
							}
							else
							{
								awaiter2 = Task.Delay(16).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgHostConfigSync.<WaitFrame>d__7>(ref awaiter2, ref this);
									return;
								}
							}
						}
						else
						{
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(TaskAwaiter);
							this.<>1__state = -1;
						}
						awaiter2.GetResult();
						goto IL_F5;
					}
					awaiter = (IAwaiter<Variant[]>)this.<>u__1;
					this.<>u__1 = null;
					this.<>1__state = -1;
					IL_78:
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

			// Token: 0x060006FE RID: 1790 RVA: 0x000259DC File Offset: 0x00023BDC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400036D RID: 877
			public int <>1__state;

			// Token: 0x0400036E RID: 878
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400036F RID: 879
			[Nullable(0)]
			private object <>u__1;

			// Token: 0x04000370 RID: 880
			private TaskAwaiter <>u__2;
		}
	}
}
