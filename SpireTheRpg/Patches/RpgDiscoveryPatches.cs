using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Nodes.Screens.GameOverScreen;
using MegaCrit.Sts2.Core.Runs;
using SpireTheRpg.Combat;
using SpireTheRpg.Core;

namespace SpireTheRpg.Patches
{
	// Token: 0x02000021 RID: 33
	[NullableContext(1)]
	[Nullable(0)]
	[HarmonyPatch]
	internal static class RpgDiscoveryPatches
	{
		// Token: 0x06000176 RID: 374 RVA: 0x0000E44A File Offset: 0x0000C64A
		[HarmonyPatch(typeof(NRunSummary), "AnimateInDiscoveries")]
		[HarmonyPostfix]
		private static void AnimateInDiscoveriesPostfix(NRunSummary __instance, RunState runState, CancellationToken ct, ref Task __result)
		{
			__result = RpgDiscoveryPatches.Continue(__instance, ct, __result);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000E458 File Offset: 0x0000C658
		private static Task Continue(NRunSummary summary, CancellationToken ct, Task original)
		{
			RpgDiscoveryPatches.<Continue>d__2 <Continue>d__;
			<Continue>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Continue>d__.summary = summary;
			<Continue>d__.ct = ct;
			<Continue>d__.original = original;
			<Continue>d__.<>1__state = -1;
			<Continue>d__.<>t__builder.Start<RpgDiscoveryPatches.<Continue>d__2>(ref <Continue>d__);
			return <Continue>d__.<>t__builder.Task;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000E4AC File Offset: 0x0000C6AC
		private static Task ShowRpgDiscoveries(NRunSummary summary, CancellationToken ct)
		{
			RpgDiscoveryPatches.<ShowRpgDiscoveries>d__3 <ShowRpgDiscoveries>d__;
			<ShowRpgDiscoveries>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowRpgDiscoveries>d__.summary = summary;
			<ShowRpgDiscoveries>d__.ct = ct;
			<ShowRpgDiscoveries>d__.<>1__state = -1;
			<ShowRpgDiscoveries>d__.<>t__builder.Start<RpgDiscoveryPatches.<ShowRpgDiscoveries>d__3>(ref <ShowRpgDiscoveries>d__);
			return <ShowRpgDiscoveries>d__.<>t__builder.Task;
		}

		// Token: 0x04000089 RID: 137
		private const string RpgNodeName = "DiscoveredRpgLoot";

		// Token: 0x020000C7 RID: 199
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <Continue>d__2 : IAsyncStateMachine
		{
			// Token: 0x06000695 RID: 1685 RVA: 0x00021EC4 File Offset: 0x000200C4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					if (num == 0 || num != 1)
					{
						try
						{
							TaskAwaiter awaiter;
							if (num != 0)
							{
								awaiter = this.original.GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 0);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgDiscoveryPatches.<Continue>d__2>(ref awaiter, ref this);
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
						}
						catch (OperationCanceledException)
						{
							throw;
						}
						catch (Exception value)
						{
							Logger logger = SpireTheRpgMainFile.Logger;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 1);
							defaultInterpolatedStringHandler.AppendLiteral("AnimateInDiscoveries original failed: ");
							defaultInterpolatedStringHandler.AppendFormatted<Exception>(value);
							logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
						}
						if (this.ct.IsCancellationRequested || !GodotObject.IsInstanceValid(this.summary))
						{
							goto IL_185;
						}
					}
					try
					{
						TaskAwaiter awaiter;
						if (num != 1)
						{
							awaiter = RpgDiscoveryPatches.ShowRpgDiscoveries(this.summary, this.ct).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 1);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgDiscoveryPatches.<Continue>d__2>(ref awaiter, ref this);
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
					}
					catch (OperationCanceledException)
					{
						throw;
					}
					catch (Exception value2)
					{
						Logger logger2 = SpireTheRpgMainFile.Logger;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 1);
						defaultInterpolatedStringHandler.AppendLiteral("Rpg discovery UI failed: ");
						defaultInterpolatedStringHandler.AppendFormatted<Exception>(value2);
						logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_185:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000696 RID: 1686 RVA: 0x000220E8 File Offset: 0x000202E8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040002BC RID: 700
			public int <>1__state;

			// Token: 0x040002BD RID: 701
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040002BE RID: 702
			[Nullable(0)]
			public Task original;

			// Token: 0x040002BF RID: 703
			public CancellationToken ct;

			// Token: 0x040002C0 RID: 704
			[Nullable(0)]
			public NRunSummary summary;

			// Token: 0x040002C1 RID: 705
			private TaskAwaiter <>u__1;
		}

		// Token: 0x020000C8 RID: 200
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ShowRpgDiscoveries>d__3 : IAsyncStateMachine
		{
			// Token: 0x06000697 RID: 1687 RVA: 0x000220F8 File Offset: 0x000202F8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					if (num != 0)
					{
						this.<items>5__2 = RpgRunLoot.LastEndDiscoveries;
						if (this.<items>5__2.Count == 0)
						{
							goto IL_2BC;
						}
						SpireLoc.ForceInject();
						Control nodeOrNull = this.summary.GetNodeOrNull<Control>("%DiscoveryHeader");
						Control nodeOrNull2 = this.summary.GetNodeOrNull<Control>("%DiscoveredContents");
						NDiscoveredItem nodeOrNull3 = this.summary.GetNodeOrNull<NDiscoveredItem>("%DiscoveredRelics");
						if (nodeOrNull == null || nodeOrNull2 == null || nodeOrNull3 == null)
						{
							SpireTheRpgMainFile.Logger.Info("Rpg discovery nodes missing", 1);
							goto IL_2BC;
						}
						nodeOrNull.Modulate = new Color(nodeOrNull.Modulate.R, nodeOrNull.Modulate.G, nodeOrNull.Modulate.B, 1f);
						NDiscoveredItem nodeOrNull4 = nodeOrNull2.GetNodeOrNull<NDiscoveredItem>("DiscoveredRpgLoot");
						if (nodeOrNull4 != null)
						{
							nodeOrNull4.QueueFree();
						}
						NDiscoveredItem ndiscoveredItem = nodeOrNull3.Duplicate(15) as NDiscoveredItem;
						if (ndiscoveredItem == null)
						{
							goto IL_2BC;
						}
						ndiscoveredItem.Name = "DiscoveredRpgLoot";
						nodeOrNull2.AddChild(ndiscoveredItem, false, 0L);
						LocString locString = new LocString("main_menu_ui", "SPIRE_LOOT.discovery_header");
						ndiscoveredItem.SetHoverTip(new HoverTip(locString, RpgRunLoot.EndDiscoveryBody(10), null));
						NDiscoveredItem ndiscoveredItem2 = ndiscoveredItem;
						defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
						defaultInterpolatedStringHandler.AppendFormatted<int>(this.<items>5__2.Count);
						ndiscoveredItem2.SetText(defaultInterpolatedStringHandler.ToStringAndClear());
						ndiscoveredItem.Visible = true;
						ndiscoveredItem.Modulate = Colors.White;
						Tween tween = this.summary.CreateTween().SetParallel(true);
						tween.TweenProperty(ndiscoveredItem, "modulate", Colors.White, 0.3);
						tween.TweenProperty(ndiscoveredItem, "position:y", 0f, 0.3).SetEase(1L).SetTrans(10L).From(100f);
					}
					try
					{
						TaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = Task.Delay(350, this.ct).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgDiscoveryPatches.<ShowRpgDiscoveries>d__3>(ref awaiter, ref this);
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
					catch (OperationCanceledException)
					{
						throw;
					}
					Logger logger = SpireTheRpgMainFile.Logger;
					defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Rpg discovery shown count=");
					defaultInterpolatedStringHandler.AppendFormatted<int>(this.<items>5__2.Count);
					logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<items>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_2BC:
				this.<>1__state = -2;
				this.<items>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000698 RID: 1688 RVA: 0x00022410 File Offset: 0x00020610
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040002C2 RID: 706
			public int <>1__state;

			// Token: 0x040002C3 RID: 707
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040002C4 RID: 708
			[Nullable(0)]
			public NRunSummary summary;

			// Token: 0x040002C5 RID: 709
			public CancellationToken ct;

			// Token: 0x040002C6 RID: 710
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private IReadOnlyList<RpgItemData> <items>5__2;

			// Token: 0x040002C7 RID: 711
			private TaskAwaiter <>u__1;
		}
	}
}
