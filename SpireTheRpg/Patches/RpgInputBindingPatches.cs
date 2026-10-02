using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using SpireTheRpg.Core;

namespace SpireTheRpg.Patches
{
	// Token: 0x02000024 RID: 36
	[NullableContext(1)]
	[Nullable(0)]
	[HarmonyPatch]
	internal static class RpgInputBindingPatches
	{
		// Token: 0x06000185 RID: 389 RVA: 0x0000ECE6 File Offset: 0x0000CEE6
		[HarmonyPatch(typeof(NInputManager), "_Ready")]
		[HarmonyPrefix]
		private static void ReadyPrefix()
		{
			RpgInputBindings.EnsureLists();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000ECED File Offset: 0x0000CEED
		[HarmonyPatch(typeof(NInputManager), "Init")]
		[HarmonyPostfix]
		private static void InitPostfix(NInputManager __instance, ref Task __result)
		{
			__result = RpgInputBindingPatches.ContinueAfterInit(__instance, __result);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000ECFC File Offset: 0x0000CEFC
		private static Task ContinueAfterInit(NInputManager manager, Task original)
		{
			RpgInputBindingPatches.<ContinueAfterInit>d__2 <ContinueAfterInit>d__;
			<ContinueAfterInit>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ContinueAfterInit>d__.manager = manager;
			<ContinueAfterInit>d__.original = original;
			<ContinueAfterInit>d__.<>1__state = -1;
			<ContinueAfterInit>d__.<>t__builder.Start<RpgInputBindingPatches.<ContinueAfterInit>d__2>(ref <ContinueAfterInit>d__);
			return <ContinueAfterInit>d__.<>t__builder.Task;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000ED47 File Offset: 0x0000CF47
		[HarmonyPatch(typeof(NInputManager), "ResetToDefaults")]
		[HarmonyPostfix]
		private static void ResetPostfix(NInputManager __instance)
		{
			RpgInputBindings.EnsureDefaultKey(__instance);
		}

		// Token: 0x020000CA RID: 202
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ContinueAfterInit>d__2 : IAsyncStateMachine
		{
			// Token: 0x0600069C RID: 1692 RVA: 0x00022440 File Offset: 0x00020640
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					try
					{
						TaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = this.original.GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgInputBindingPatches.<ContinueAfterInit>d__2>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							this.<>1__state = -1;
						}
						awaiter.GetResult();
					}
					catch
					{
					}
					RpgInputBindings.EnsureDefaultKey(this.manager);
					Logger logger = SpireTheRpgMainFile.Logger;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
					defaultInterpolatedStringHandler.AppendLiteral("RpgInputBindings ready key=");
					defaultInterpolatedStringHandler.AppendFormatted<Key>(RpgInputBindings.GetBoundKey(this.manager));
					logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
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

			// Token: 0x0600069D RID: 1693 RVA: 0x00022548 File Offset: 0x00020748
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040002CA RID: 714
			public int <>1__state;

			// Token: 0x040002CB RID: 715
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040002CC RID: 716
			[Nullable(0)]
			public Task original;

			// Token: 0x040002CD RID: 717
			[Nullable(0)]
			public NInputManager manager;

			// Token: 0x040002CE RID: 718
			private TaskAwaiter <>u__1;
		}
	}
}
