using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Runs;
using SpireTheRpg.Combat;
using SpireTheRpg.Core;

namespace SpireTheRpg.Patches
{
	// Token: 0x02000029 RID: 41
	[NullableContext(1)]
	[Nullable(0)]
	[HarmonyPatch]
	internal static class RpgRelicGrantPatches
	{
		// Token: 0x06000192 RID: 402 RVA: 0x0000F0AC File Offset: 0x0000D2AC
		[HarmonyPatch(typeof(RunManager), "InitializeNewRun")]
		[HarmonyPostfix]
		private static void NewRunPostfix()
		{
			RpgRelicGrantPatches.TrySubscribe();
			RpgRelicGrantPatches.TryEnsureCurrent();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000F0B8 File Offset: 0x0000D2B8
		[HarmonyPatch(typeof(RunManager), "InitializeSavedRun")]
		[HarmonyPostfix]
		private static void SavedRunPostfix()
		{
			RpgRelicGrantPatches.TrySubscribe();
			RpgRelicGrantPatches.TryEnsureCurrent();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
		[HarmonyPatch(typeof(RunManager), "set_State")]
		[HarmonyPostfix]
		private static void StateSetPostfix(RunState value)
		{
			if (value == null)
			{
				return;
			}
			RpgProgress.Ensure();
			RpgRelicGrantPatches.EnsureRun(value);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000F0D8 File Offset: 0x0000D2D8
		private static void TrySubscribe()
		{
			if (RpgRelicGrantPatches._subscribed)
			{
				return;
			}
			try
			{
				RunManager instance = RunManager.Instance;
				if (instance != null)
				{
					RunManager runManager = instance;
					Action<RunState> action;
					if ((action = RpgRelicGrantPatches.<>O.<0>__OnRunStarted) == null)
					{
						action = (RpgRelicGrantPatches.<>O.<0>__OnRunStarted = new Action<RunState>(RpgRelicGrantPatches.OnRunStarted));
					}
					runManager.RunStarted += action;
					RpgRelicGrantPatches._subscribed = true;
					SpireTheRpgMainFile.Logger.Info("Subscribed to RunManager.RunStarted", 1);
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RunStarted subscribe failed: " + ex.Message, 1);
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000F160 File Offset: 0x0000D360
		private static void OnRunStarted(RunState state)
		{
			RpgRelicGrantPatches.EnsureRun(state);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000F16C File Offset: 0x0000D36C
		private static void TryEnsureCurrent()
		{
			try
			{
				PropertyInfo propertyInfo = AccessTools.Property(typeof(RunManager), "State");
				RunState runState = ((propertyInfo != null) ? propertyInfo.GetValue(RunManager.Instance) : null) as RunState;
				if (runState != null)
				{
					RpgRelicGrantPatches.EnsureRun(runState);
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("TryEnsureCurrent failed: " + ex.Message, 1);
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000F1E0 File Offset: 0x0000D3E0
		private static Task EnsureRun(RunState run)
		{
			RpgRelicGrantPatches.<EnsureRun>d__9 <EnsureRun>d__;
			<EnsureRun>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EnsureRun>d__.run = run;
			<EnsureRun>d__.<>1__state = -1;
			<EnsureRun>d__.<>t__builder.Start<RpgRelicGrantPatches.<EnsureRun>d__9>(ref <EnsureRun>d__);
			return <EnsureRun>d__.<>t__builder.Task;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000F223 File Offset: 0x0000D423
		// Note: this type is marked as 'beforefieldinit'.
		static RpgRelicGrantPatches()
		{
		}

		// Token: 0x0400008F RID: 143
		private static readonly object Gate = new object();

		// Token: 0x04000090 RID: 144
		private static readonly HashSet<RunState> Ensured = new HashSet<RunState>(ReferenceEqualityComparer.Instance);

		// Token: 0x04000091 RID: 145
		private static bool _subscribed;

		// Token: 0x020000CD RID: 205
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040002D3 RID: 723
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Action<RunState> <0>__OnRunStarted;
		}

		// Token: 0x020000CE RID: 206
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <EnsureRun>d__9 : IAsyncStateMachine
		{
			// Token: 0x060006A4 RID: 1700 RVA: 0x000225D4 File Offset: 0x000207D4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					if (num > 2)
					{
						object gate = RpgRelicGrantPatches.Gate;
						bool flag = false;
						try
						{
							Monitor.Enter(gate, ref flag);
							if (!RpgRelicGrantPatches.Ensured.Add(this.run))
							{
								goto IL_25D;
							}
						}
						finally
						{
							if (num < 0 && flag)
							{
								Monitor.Exit(gate);
							}
						}
					}
					try
					{
						TaskAwaiter awaiter;
						switch (num)
						{
						case 0:
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							num = (this.<>1__state = -1);
							break;
						case 1:
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_10F;
						case 2:
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_16F;
						default:
							awaiter = RpgHostConfigSync.EnsureSynced(this.run).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgRelicGrantPatches.<EnsureRun>d__9>(ref awaiter, ref this);
								return;
							}
							break;
						}
						awaiter.GetResult();
						RpgProgress.Ensure();
						awaiter = Task.Delay(80).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 1);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgRelicGrantPatches.<EnsureRun>d__9>(ref awaiter, ref this);
							return;
						}
						IL_10F:
						awaiter.GetResult();
						awaiter = RpgCombatRewards.EnsureRelics(this.run).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 2);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgRelicGrantPatches.<EnsureRun>d__9>(ref awaiter, ref this);
							return;
						}
						IL_16F:
						awaiter.GetResult();
						Logger logger = SpireTheRpgMainFile.Logger;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 3);
						defaultInterpolatedStringHandler.AppendLiteral("Granted RPG relics level=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(RpgProgress.Level);
						defaultInterpolatedStringHandler.AppendLiteral(" xp=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(RpgProgress.Xp);
						defaultInterpolatedStringHandler.AppendLiteral(" meta=");
						defaultInterpolatedStringHandler.AppendFormatted<bool>(RpgEffectiveConfig.EnableMetaEquipment);
						logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
					}
					catch (Exception value)
					{
						object gate = RpgRelicGrantPatches.Gate;
						bool flag = false;
						try
						{
							Monitor.Enter(gate, ref flag);
							RpgRelicGrantPatches.Ensured.Remove(this.run);
						}
						finally
						{
							if (num < 0 && flag)
							{
								Monitor.Exit(gate);
							}
						}
						Logger logger2 = SpireTheRpgMainFile.Logger;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(24, 1);
						defaultInterpolatedStringHandler.AppendLiteral("Rpg relic grant failed: ");
						defaultInterpolatedStringHandler.AppendFormatted<Exception>(value);
						logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_25D:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060006A5 RID: 1701 RVA: 0x000228B8 File Offset: 0x00020AB8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040002D4 RID: 724
			public int <>1__state;

			// Token: 0x040002D5 RID: 725
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040002D6 RID: 726
			[Nullable(0)]
			public RunState run;

			// Token: 0x040002D7 RID: 727
			private TaskAwaiter <>u__1;
		}
	}
}
