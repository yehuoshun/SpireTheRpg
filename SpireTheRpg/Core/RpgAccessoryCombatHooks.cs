using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;
using SpireTheRpg.Combat;

namespace SpireTheRpg.Core
{
	// Token: 0x02000034 RID: 52
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgAccessoryCombatHooks
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00010068 File Offset: 0x0000E268
		public static Task ApplyCombatStart(Creature creature)
		{
			RpgAccessoryCombatHooks.<ApplyCombatStart>d__0 <ApplyCombatStart>d__;
			<ApplyCombatStart>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ApplyCombatStart>d__.creature = creature;
			<ApplyCombatStart>d__.<>1__state = -1;
			<ApplyCombatStart>d__.<>t__builder.Start<RpgAccessoryCombatHooks.<ApplyCombatStart>d__0>(ref <ApplyCombatStart>d__);
			return <ApplyCombatStart>d__.<>t__builder.Task;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000100AC File Offset: 0x0000E2AC
		private static Task<int> ApplyLegendaryStart(PlayerChoiceContext ctx, Creature creature, RpgItemData item)
		{
			RpgAccessoryCombatHooks.<ApplyLegendaryStart>d__1 <ApplyLegendaryStart>d__;
			<ApplyLegendaryStart>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ApplyLegendaryStart>d__.ctx = ctx;
			<ApplyLegendaryStart>d__.creature = creature;
			<ApplyLegendaryStart>d__.item = item;
			<ApplyLegendaryStart>d__.<>1__state = -1;
			<ApplyLegendaryStart>d__.<>t__builder.Start<RpgAccessoryCombatHooks.<ApplyLegendaryStart>d__1>(ref <ApplyLegendaryStart>d__);
			return <ApplyLegendaryStart>d__.<>t__builder.Task;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00010100 File Offset: 0x0000E300
		public static void OnEquipmentGranted(RpgItemData granted, [Nullable(2)] Player owner = null)
		{
			try
			{
				if (!RpgAccessoryRunState.DoubleVisionUsed)
				{
					if (RpgAccessoryCombatHooks.HasActiveAccessory("double_vision_prism", owner))
					{
						RpgAccessoryRunState.DoubleVisionUsed = true;
						RpgItemData rpgItemData = granted.Clone();
						rpgItemData.InstanceId = Guid.NewGuid().ToString("N");
						if (RpgEffectiveConfig.EnableMetaEquipment)
						{
							RpgRunLoot.Add(RpgNet.NetIdOf(owner), rpgItemData);
						}
						else
						{
							RpgRunLoadout.AddToBag(RpgNet.NetIdOf(owner), rpgItemData);
						}
						SpireTheRpgMainFile.Logger.Info("RpgAccessory double_vision copied id=" + granted.Id, 1);
					}
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RpgAccessory double_vision failed: " + ex.Message, 1);
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000101BC File Offset: 0x0000E3BC
		public static bool HasActiveAccessory(string id, [Nullable(2)] Player player = null)
		{
			return RpgActiveLoadout.EquippedItems(player).Any((RpgItemData item) => item.EquipSlot == RpgEquipSlot.Accessory && item.IsIdentified && string.Equals(item.Id, id, StringComparison.OrdinalIgnoreCase) && RpgActiveLoadout.MeetsLevelRequirement(item) && RpgAccessoryGates.IsActive(item, player));
		}

		// Token: 0x020000D5 RID: 213
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x060006B2 RID: 1714 RVA: 0x00022B6E File Offset: 0x00020D6E
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x060006B3 RID: 1715 RVA: 0x00022B76 File Offset: 0x00020D76
			[NullableContext(0)]
			internal bool <HasActiveAccessory>b__0(RpgItemData item)
			{
				return item.EquipSlot == RpgEquipSlot.Accessory && item.IsIdentified && string.Equals(item.Id, this.id, StringComparison.OrdinalIgnoreCase) && RpgActiveLoadout.MeetsLevelRequirement(item) && RpgAccessoryGates.IsActive(item, this.player);
			}

			// Token: 0x040002E7 RID: 743
			[Nullable(0)]
			public string id;

			// Token: 0x040002E8 RID: 744
			[Nullable(0)]
			public Player player;
		}

		// Token: 0x020000D6 RID: 214
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ApplyCombatStart>d__0 : IAsyncStateMachine
		{
			// Token: 0x060006B4 RID: 1716 RVA: 0x00022BB4 File Offset: 0x00020DB4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					int num2;
					if (num != 0)
					{
						if (RpgNet.IsMultiplayerRun())
						{
							goto IL_18D;
						}
						this.<ctx>5__2 = new ThrowingPlayerChoiceContext();
						num2 = 0;
						this.<>7__wrap2 = RpgActiveLoadout.EquippedItems(this.creature.Player).GetEnumerator();
					}
					try
					{
						TaskAwaiter<int> awaiter;
						if (num == 0)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter<int>);
							num = (this.<>1__state = -1);
							goto IL_F5;
						}
						IL_106:
						while (this.<>7__wrap2.MoveNext())
						{
							RpgItemData rpgItemData = this.<>7__wrap2.Current;
							if (rpgItemData.EquipSlot == RpgEquipSlot.Accessory && rpgItemData.IsIdentified && RpgActiveLoadout.MeetsLevelRequirement(rpgItemData) && RpgAccessoryGates.IsActive(rpgItemData, this.creature.Player))
							{
								this.<>7__wrap3 = num2;
								awaiter = RpgAccessoryCombatHooks.ApplyLegendaryStart(this.<ctx>5__2, this.creature, rpgItemData).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 0);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<int>, RpgAccessoryCombatHooks.<ApplyCombatStart>d__0>(ref awaiter, ref this);
									return;
								}
								goto IL_F5;
							}
						}
						goto IL_130;
						IL_F5:
						int result = awaiter.GetResult();
						num2 = this.<>7__wrap3 + result;
						goto IL_106;
					}
					finally
					{
						if (num < 0 && this.<>7__wrap2 != null)
						{
							this.<>7__wrap2.Dispose();
						}
					}
					IL_130:
					this.<>7__wrap2 = null;
					if (num2 > 0)
					{
						Logger logger = SpireTheRpgMainFile.Logger;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 1);
						defaultInterpolatedStringHandler.AppendLiteral("RpgAccessoryCombatHooks start applied=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
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
				IL_18D:
				this.<>1__state = -2;
				this.<ctx>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060006B5 RID: 1717 RVA: 0x00022D9C File Offset: 0x00020F9C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040002E9 RID: 745
			public int <>1__state;

			// Token: 0x040002EA RID: 746
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040002EB RID: 747
			[Nullable(0)]
			public Creature creature;

			// Token: 0x040002EC RID: 748
			[Nullable(0)]
			private ThrowingPlayerChoiceContext <ctx>5__2;

			// Token: 0x040002ED RID: 749
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private IEnumerator<RpgItemData> <>7__wrap2;

			// Token: 0x040002EE RID: 750
			private int <>7__wrap3;

			// Token: 0x040002EF RID: 751
			[Nullable(0)]
			private TaskAwaiter<int> <>u__1;
		}

		// Token: 0x020000D7 RID: 215
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ApplyLegendaryStart>d__1 : IAsyncStateMachine
		{
			// Token: 0x060006B6 RID: 1718 RVA: 0x00022DAC File Offset: 0x00020FAC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				int result;
				try
				{
					TaskAwaiter<bool> awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_190;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_205;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_27A;
					case 4:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_307;
					case 5:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_37C;
					case 6:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_3EE;
					default:
					{
						string id = this.item.Id;
						if (!(id == "armor_pact_charm"))
						{
							if (!(id == "void_resonance_core"))
							{
								if (!(id == "full_set_ritual_ring"))
								{
									if (!(id == "silent_smoke_stride"))
									{
										if (!(id == "regent_coronation_rite"))
										{
											if (!(id == "necrobinder_nightwatch_lantern"))
											{
												result = 0;
												goto IL_418;
											}
											awaiter = RpgCombatBuffApplier.ApplyByPowerName(this.ctx, this.creature, "Thorns", 1).GetAwaiter();
											if (!awaiter.IsCompleted)
											{
												this.<>1__state = 6;
												this.<>u__1 = awaiter;
												this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgAccessoryCombatHooks.<ApplyLegendaryStart>d__1>(ref awaiter, ref this);
												return;
											}
											goto IL_3EE;
										}
										else if (this.creature.CurrentHp >= this.creature.MaxHp)
										{
											awaiter = RpgCombatBuffApplier.ApplyByPowerName(this.ctx, this.creature, "Strength", 2).GetAwaiter();
											if (!awaiter.IsCompleted)
											{
												this.<>1__state = 4;
												this.<>u__1 = awaiter;
												this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgAccessoryCombatHooks.<ApplyLegendaryStart>d__1>(ref awaiter, ref this);
												return;
											}
											goto IL_307;
										}
										else
										{
											awaiter = RpgCombatBuffApplier.ApplyByPowerName(this.ctx, this.creature, "Artifact", 1).GetAwaiter();
											if (!awaiter.IsCompleted)
											{
												this.<>1__state = 5;
												this.<>u__1 = awaiter;
												this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgAccessoryCombatHooks.<ApplyLegendaryStart>d__1>(ref awaiter, ref this);
												return;
											}
											goto IL_37C;
										}
									}
									else
									{
										awaiter = RpgCombatBuffApplier.ApplyByPowerName(this.ctx, this.creature, "Intangible", 1).GetAwaiter();
										if (!awaiter.IsCompleted)
										{
											this.<>1__state = 3;
											this.<>u__1 = awaiter;
											this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgAccessoryCombatHooks.<ApplyLegendaryStart>d__1>(ref awaiter, ref this);
											return;
										}
										goto IL_27A;
									}
								}
								else
								{
									awaiter = RpgCombatBuffApplier.ApplyByPowerName(this.ctx, this.creature, "Buffer", 1).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										this.<>1__state = 2;
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgAccessoryCombatHooks.<ApplyLegendaryStart>d__1>(ref awaiter, ref this);
										return;
									}
									goto IL_205;
								}
							}
							else
							{
								int amount = Math.Clamp(RpgAccessoryGates.CountPrefix("void_", this.creature.Player), 1, 3);
								awaiter = RpgCombatBuffApplier.ApplyByPowerName(this.ctx, this.creature, "Artifact", amount).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgAccessoryCombatHooks.<ApplyLegendaryStart>d__1>(ref awaiter, ref this);
									return;
								}
								goto IL_190;
							}
						}
						else
						{
							awaiter = RpgCombatBuffApplier.ApplyByPowerName(this.ctx, this.creature, "Plating", 4).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgAccessoryCombatHooks.<ApplyLegendaryStart>d__1>(ref awaiter, ref this);
								return;
							}
						}
						break;
					}
					}
					result = ((awaiter.GetResult() > false) ? 1 : 0);
					goto IL_418;
					IL_190:
					result = ((awaiter.GetResult() > false) ? 1 : 0);
					goto IL_418;
					IL_205:
					result = ((awaiter.GetResult() > false) ? 1 : 0);
					goto IL_418;
					IL_27A:
					result = ((awaiter.GetResult() > false) ? 1 : 0);
					goto IL_418;
					IL_307:
					result = ((awaiter.GetResult() > false) ? 1 : 0);
					goto IL_418;
					IL_37C:
					result = ((awaiter.GetResult() > false) ? 1 : 0);
					goto IL_418;
					IL_3EE:
					result = ((awaiter.GetResult() > false) ? 1 : 0);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_418:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060006B7 RID: 1719 RVA: 0x00023204 File Offset: 0x00021404
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040002F0 RID: 752
			public int <>1__state;

			// Token: 0x040002F1 RID: 753
			[Nullable(0)]
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x040002F2 RID: 754
			[Nullable(0)]
			public RpgItemData item;

			// Token: 0x040002F3 RID: 755
			[Nullable(0)]
			public PlayerChoiceContext ctx;

			// Token: 0x040002F4 RID: 756
			[Nullable(0)]
			public Creature creature;

			// Token: 0x040002F5 RID: 757
			[Nullable(0)]
			private TaskAwaiter<bool> <>u__1;
		}
	}
}
