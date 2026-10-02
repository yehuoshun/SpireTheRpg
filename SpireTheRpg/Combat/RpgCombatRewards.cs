using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Nodes.Screens.Overlays;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using SpireTheRpg.Core;
using SpireTheRpg.Relics;
using SpireTheRpg.Ui;

namespace SpireTheRpg.Combat
{
	// Token: 0x0200006D RID: 109
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgCombatRewards
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x0001B180 File Offset: 0x00019380
		public static Task EnsureRelics(Player player)
		{
			RpgCombatRewards.<EnsureRelics>d__1 <EnsureRelics>d__;
			<EnsureRelics>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EnsureRelics>d__.player = player;
			<EnsureRelics>d__.<>1__state = -1;
			<EnsureRelics>d__.<>t__builder.Start<RpgCombatRewards.<EnsureRelics>d__1>(ref <EnsureRelics>d__);
			return <EnsureRelics>d__.<>t__builder.Task;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001B1C4 File Offset: 0x000193C4
		public static Task EnsureRelics(IRunState run)
		{
			RpgCombatRewards.<EnsureRelics>d__2 <EnsureRelics>d__;
			<EnsureRelics>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EnsureRelics>d__.run = run;
			<EnsureRelics>d__.<>1__state = -1;
			<EnsureRelics>d__.<>t__builder.Start<RpgCombatRewards.<EnsureRelics>d__2>(ref <EnsureRelics>d__);
			return <EnsureRelics>d__.<>t__builder.Task;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001B207 File Offset: 0x00019407
		public static bool IsLocal(Player player)
		{
			return RpgNet.IsLocal(player);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001B210 File Offset: 0x00019410
		public static Task HandleVictory(Player player, CombatRoom room)
		{
			RpgCombatRewards.<HandleVictory>d__4 <HandleVictory>d__;
			<HandleVictory>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<HandleVictory>d__.player = player;
			<HandleVictory>d__.room = room;
			<HandleVictory>d__.<>1__state = -1;
			<HandleVictory>d__.<>t__builder.Start<RpgCombatRewards.<HandleVictory>d__4>(ref <HandleVictory>d__);
			return <HandleVictory>d__.<>t__builder.Task;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001B25C File Offset: 0x0001945C
		private static Task WaitForRewardsOverlay()
		{
			RpgCombatRewards.<WaitForRewardsOverlay>d__5 <WaitForRewardsOverlay>d__;
			<WaitForRewardsOverlay>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WaitForRewardsOverlay>d__.<>1__state = -1;
			<WaitForRewardsOverlay>d__.<>t__builder.Start<RpgCombatRewards.<WaitForRewardsOverlay>d__5>(ref <WaitForRewardsOverlay>d__);
			return <WaitForRewardsOverlay>d__.<>t__builder.Task;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001B298 File Offset: 0x00019498
		private static Task WaitFrame()
		{
			RpgCombatRewards.<WaitFrame>d__6 <WaitFrame>d__;
			<WaitFrame>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WaitFrame>d__.<>1__state = -1;
			<WaitFrame>d__.<>t__builder.Start<RpgCombatRewards.<WaitFrame>d__6>(ref <WaitFrame>d__);
			return <WaitFrame>d__.<>t__builder.Task;
		}

		// Token: 0x0400019C RID: 412
		private static int _lootGate;

		// Token: 0x0200013F RID: 319
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <EnsureRelics>d__1 : IAsyncStateMachine
		{
			// Token: 0x06000833 RID: 2099 RVA: 0x0002A8A0 File Offset: 0x00028AA0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					try
					{
						TaskAwaiter<RpgDataRelic> awaiter;
						TaskAwaiter<RpgExpRelic> awaiter2;
						TaskAwaiter<RpgItemRelic> awaiter3;
						switch (num)
						{
						case 0:
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter<RpgDataRelic>);
							this.<>1__state = -1;
							break;
						case 1:
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(TaskAwaiter<RpgExpRelic>);
							this.<>1__state = -1;
							goto IL_F2;
						case 2:
							awaiter3 = this.<>u__3;
							this.<>u__3 = default(TaskAwaiter<RpgItemRelic>);
							this.<>1__state = -1;
							goto IL_160;
						default:
							if (this.player.GetRelic<RpgDataRelic>() != null)
							{
								goto IL_8C;
							}
							awaiter = RelicCmd.Obtain<RpgDataRelic>(this.player).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgDataRelic>, RpgCombatRewards.<EnsureRelics>d__1>(ref awaiter, ref this);
								return;
							}
							break;
						}
						awaiter.GetResult();
						IL_8C:
						if (this.player.GetRelic<RpgExpRelic>() != null)
						{
							goto IL_FA;
						}
						awaiter2 = RelicCmd.Obtain<RpgExpRelic>(this.player).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgExpRelic>, RpgCombatRewards.<EnsureRelics>d__1>(ref awaiter2, ref this);
							return;
						}
						IL_F2:
						awaiter2.GetResult();
						IL_FA:
						if (this.player.GetRelic<RpgItemRelic>() != null)
						{
							goto IL_168;
						}
						awaiter3 = RelicCmd.Obtain<RpgItemRelic>(this.player).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__3 = awaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgItemRelic>, RpgCombatRewards.<EnsureRelics>d__1>(ref awaiter3, ref this);
							return;
						}
						IL_160:
						awaiter3.GetResult();
						IL_168:
						RpgDataRelic relic = this.player.GetRelic<RpgDataRelic>();
						if (relic != null)
						{
							relic.SyncVars();
						}
						RpgExpRelic relic2 = this.player.GetRelic<RpgExpRelic>();
						if (relic2 != null)
						{
							relic2.SyncVars();
						}
						if (RpgNet.IsLocal(this.player))
						{
							RpgEquippedGearSync.Request(this.player);
						}
					}
					catch (Exception value)
					{
						Logger logger = SpireTheRpgMainFile.Logger;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 1);
						defaultInterpolatedStringHandler.AppendLiteral("Rpg EnsureRelics failed: ");
						defaultInterpolatedStringHandler.AppendFormatted<Exception>(value);
						logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
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

			// Token: 0x06000834 RID: 2100 RVA: 0x0002AAF4 File Offset: 0x00028CF4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040004A6 RID: 1190
			public int <>1__state;

			// Token: 0x040004A7 RID: 1191
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040004A8 RID: 1192
			[Nullable(0)]
			public Player player;

			// Token: 0x040004A9 RID: 1193
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private TaskAwaiter<RpgDataRelic> <>u__1;

			// Token: 0x040004AA RID: 1194
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private TaskAwaiter<RpgExpRelic> <>u__2;

			// Token: 0x040004AB RID: 1195
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private TaskAwaiter<RpgItemRelic> <>u__3;
		}

		// Token: 0x02000140 RID: 320
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <EnsureRelics>d__2 : IAsyncStateMachine
		{
			// Token: 0x06000835 RID: 2101 RVA: 0x0002AB04 File Offset: 0x00028D04
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					if (num != 0)
					{
						this.<>7__wrap1 = this.run.Players.GetEnumerator();
					}
					try
					{
						if (num != 0)
						{
							goto IL_88;
						}
						TaskAwaiter awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter);
						num = (this.<>1__state = -1);
						IL_81:
						awaiter.GetResult();
						IL_88:
						if (this.<>7__wrap1.MoveNext())
						{
							awaiter = RpgCombatRewards.EnsureRelics(this.<>7__wrap1.Current).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgCombatRewards.<EnsureRelics>d__2>(ref awaiter, ref this);
								return;
							}
							goto IL_81;
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

			// Token: 0x06000836 RID: 2102 RVA: 0x0002AC10 File Offset: 0x00028E10
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040004AC RID: 1196
			public int <>1__state;

			// Token: 0x040004AD RID: 1197
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040004AE RID: 1198
			[Nullable(0)]
			public IRunState run;

			// Token: 0x040004AF RID: 1199
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private IEnumerator<Player> <>7__wrap1;

			// Token: 0x040004B0 RID: 1200
			private TaskAwaiter <>u__1;
		}

		// Token: 0x02000141 RID: 321
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <HandleVictory>d__4 : IAsyncStateMachine
		{
			// Token: 0x06000837 RID: 2103 RVA: 0x0002AC20 File Offset: 0x00028E20
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					if (num > 2)
					{
						if (!RpgCombatRewards.IsLocal(this.player))
						{
							SpireTheRpgMainFile.Logger.Info("HandleVictory skipped: not local player", 1);
							goto IL_485;
						}
						if (Interlocked.CompareExchange(ref RpgCombatRewards._lootGate, 1, 0) != 0)
						{
							SpireTheRpgMainFile.Logger.Info("HandleVictory skipped: gate busy", 1);
							goto IL_485;
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
							goto IL_365;
						case 2:
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_3D1;
						default:
							RpgProgress.Ensure();
							awaiter = RpgCombatRewards.EnsureRelics(this.player).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgCombatRewards.<HandleVictory>d__4>(ref awaiter, ref this);
								return;
							}
							break;
						}
						awaiter.GetResult();
						this.<xp>5__2 = RpgLootTable.RollXp(this.room.RoomType, null);
						this.<levels>5__3 = RpgProgress.AddXp(this.<xp>5__2);
						RpgCombatDrop.LastXpGained = this.<xp>5__2;
						RpgCombatDrop.LastLevelsGained = this.<levels>5__3;
						RpgExpRelic relic = this.player.GetRelic<RpgExpRelic>();
						if (relic != null)
						{
							relic.SyncVars();
						}
						RpgDataRelic relic2 = this.player.GetRelic<RpgDataRelic>();
						if (relic2 != null)
						{
							relic2.SyncVars();
						}
						Logger logger = SpireTheRpgMainFile.Logger;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 5);
						defaultInterpolatedStringHandler.AppendLiteral("Rpg XP +");
						defaultInterpolatedStringHandler.AppendFormatted<int>(this.<xp>5__2);
						defaultInterpolatedStringHandler.AppendLiteral(" levels=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(this.<levels>5__3);
						defaultInterpolatedStringHandler.AppendLiteral(" now=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(RpgProgress.Xp);
						defaultInterpolatedStringHandler.AppendLiteral("/");
						defaultInterpolatedStringHandler.AppendFormatted<int>(RpgProgress.XpToNext);
						defaultInterpolatedStringHandler.AppendLiteral(" lv=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(RpgProgress.Level);
						logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
						double value = RpgLootTable.DropChance(this.room.RoomType);
						if (!RpgLootTable.RollDrop(this.room.RoomType, null))
						{
							Logger logger2 = SpireTheRpgMainFile.Logger;
							defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 2);
							defaultInterpolatedStringHandler.AppendLiteral("Rpg loot miss room=");
							defaultInterpolatedStringHandler.AppendFormatted<RoomType>(this.room.RoomType);
							defaultInterpolatedStringHandler.AppendLiteral(" chance=");
							defaultInterpolatedStringHandler.AppendFormatted<double>(value, "P0");
							logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
							goto IL_485;
						}
						this.<choices>5__4 = RpgLootTable.RollChoices(this.room.RoomType, null, null);
						Logger logger3 = SpireTheRpgMainFile.Logger;
						defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 4);
						defaultInterpolatedStringHandler.AppendLiteral("Rpg loot hit room=");
						defaultInterpolatedStringHandler.AppendFormatted<RoomType>(this.room.RoomType);
						defaultInterpolatedStringHandler.AppendLiteral(" chance=");
						defaultInterpolatedStringHandler.AppendFormatted<double>(value, "P0");
						defaultInterpolatedStringHandler.AppendLiteral(" choices=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(this.<choices>5__4.Count);
						defaultInterpolatedStringHandler.AppendLiteral(" catalog=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(RpgItemCatalog.All.Count);
						logger3.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
						if (this.<choices>5__4.Count == 0)
						{
							SpireTheRpgMainFile.Logger.Info("Rpg loot skipped: empty catalog/choices", 1);
							goto IL_485;
						}
						awaiter = RpgCombatRewards.WaitForRewardsOverlay().GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 1);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgCombatRewards.<HandleVictory>d__4>(ref awaiter, ref this);
							return;
						}
						IL_365:
						awaiter.GetResult();
						awaiter = RpgLootScreen.OfferAsync(this.<choices>5__4, this.<xp>5__2, this.<levels>5__3).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 2);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgCombatRewards.<HandleVictory>d__4>(ref awaiter, ref this);
							return;
						}
						IL_3D1:
						awaiter.GetResult();
						RpgItemRelic relic3 = this.player.GetRelic<RpgItemRelic>();
						if (relic3 != null)
						{
							relic3.SyncAmountPublic();
						}
						RpgDataRelic relic4 = this.player.GetRelic<RpgDataRelic>();
						if (relic4 != null)
						{
							relic4.SyncVars();
						}
						RpgExpRelic relic5 = this.player.GetRelic<RpgExpRelic>();
						if (relic5 != null)
						{
							relic5.SyncVars();
						}
						this.<choices>5__4 = null;
					}
					catch (Exception value2)
					{
						Logger logger4 = SpireTheRpgMainFile.Logger;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 1);
						defaultInterpolatedStringHandler.AppendLiteral("Rpg HandleVictory failed: ");
						defaultInterpolatedStringHandler.AppendFormatted<Exception>(value2);
						logger4.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
					}
					finally
					{
						if (num < 0)
						{
							Interlocked.Exchange(ref RpgCombatRewards._lootGate, 0);
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_485:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000838 RID: 2104 RVA: 0x0002B114 File Offset: 0x00029314
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040004B1 RID: 1201
			public int <>1__state;

			// Token: 0x040004B2 RID: 1202
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040004B3 RID: 1203
			[Nullable(0)]
			public Player player;

			// Token: 0x040004B4 RID: 1204
			[Nullable(0)]
			public CombatRoom room;

			// Token: 0x040004B5 RID: 1205
			private int <xp>5__2;

			// Token: 0x040004B6 RID: 1206
			private int <levels>5__3;

			// Token: 0x040004B7 RID: 1207
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private IReadOnlyList<RpgItemData> <choices>5__4;

			// Token: 0x040004B8 RID: 1208
			private TaskAwaiter <>u__1;
		}

		// Token: 0x02000142 RID: 322
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WaitForRewardsOverlay>d__5 : IAsyncStateMachine
		{
			// Token: 0x06000839 RID: 2105 RVA: 0x0002B124 File Offset: 0x00029324
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					TaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter);
						this.<>1__state = -1;
						goto IL_103;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter);
						this.<>1__state = -1;
						goto IL_15A;
					default:
						this.<i>5__2 = 0;
						goto IL_9E;
					}
					IL_87:
					awaiter.GetResult();
					int num2 = this.<i>5__2;
					this.<i>5__2 = num2 + 1;
					IL_9E:
					if (this.<i>5__2 < 90)
					{
						NOverlayStack instance = NOverlayStack.Instance;
						if (instance == null || instance.ScreenCount <= 0)
						{
							awaiter = RpgCombatRewards.WaitFrame().GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgCombatRewards.<WaitForRewardsOverlay>d__5>(ref awaiter, ref this);
								return;
							}
							goto IL_87;
						}
					}
					awaiter = Task.Delay(200).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgCombatRewards.<WaitForRewardsOverlay>d__5>(ref awaiter, ref this);
						return;
					}
					IL_103:
					awaiter.GetResult();
					awaiter = RpgCombatRewards.WaitFrame().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgCombatRewards.<WaitForRewardsOverlay>d__5>(ref awaiter, ref this);
						return;
					}
					IL_15A:
					awaiter.GetResult();
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

			// Token: 0x0600083A RID: 2106 RVA: 0x0002B2DC File Offset: 0x000294DC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040004B9 RID: 1209
			public int <>1__state;

			// Token: 0x040004BA RID: 1210
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040004BB RID: 1211
			private int <i>5__2;

			// Token: 0x040004BC RID: 1212
			private TaskAwaiter <>u__1;
		}

		// Token: 0x02000143 RID: 323
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WaitFrame>d__6 : IAsyncStateMachine
		{
			// Token: 0x0600083B RID: 2107 RVA: 0x0002B2EC File Offset: 0x000294EC
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
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgCombatRewards.<WaitFrame>d__6>(ref awaiter, ref this);
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
									this.<>t__builder.AwaitOnCompleted<IAwaiter<Variant[]>, RpgCombatRewards.<WaitFrame>d__6>(ref awaiter2, ref this);
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

			// Token: 0x0600083C RID: 2108 RVA: 0x0002B414 File Offset: 0x00029614
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040004BD RID: 1213
			public int <>1__state;

			// Token: 0x040004BE RID: 1214
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040004BF RID: 1215
			private TaskAwaiter <>u__1;

			// Token: 0x040004C0 RID: 1216
			[Nullable(0)]
			private object <>u__2;
		}
	}
}
