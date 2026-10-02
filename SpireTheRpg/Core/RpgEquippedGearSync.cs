using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;
using SpireTheRpg.Relics;

namespace SpireTheRpg.Core
{
	// Token: 0x0200003E RID: 62
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgEquippedGearSync
	{
		// Token: 0x06000252 RID: 594 RVA: 0x00011CA8 File Offset: 0x0000FEA8
		[NullableContext(2)]
		public static void Request(Player player = null)
		{
			if (player == null)
			{
				player = RpgNet.LocalPlayer();
			}
			if (player == null || !RpgNet.IsLocal(player))
			{
				return;
			}
			RpgEquippedGearSync._queued++;
			if (RpgEquippedGearSync._busy > 0)
			{
				return;
			}
			RpgEquippedGearSync.RunAsync(player);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00011CDC File Offset: 0x0000FEDC
		private static Task RunAsync(Player player)
		{
			RpgEquippedGearSync.<RunAsync>d__3 <RunAsync>d__;
			<RunAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RunAsync>d__.player = player;
			<RunAsync>d__.<>1__state = -1;
			<RunAsync>d__.<>t__builder.Start<RpgEquippedGearSync.<RunAsync>d__3>(ref <RunAsync>d__);
			return <RunAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00011D20 File Offset: 0x0000FF20
		private static Task SyncNow(Player player)
		{
			RpgEquippedGearSync.<SyncNow>d__4 <SyncNow>d__;
			<SyncNow>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SyncNow>d__.player = player;
			<SyncNow>d__.<>1__state = -1;
			<SyncNow>d__.<>t__builder.Start<RpgEquippedGearSync.<SyncNow>d__4>(ref <SyncNow>d__);
			return <SyncNow>d__.<>t__builder.Task;
		}

		// Token: 0x040000AE RID: 174
		private static int _busy;

		// Token: 0x040000AF RID: 175
		private static int _queued;

		// Token: 0x020000E6 RID: 230
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006E3 RID: 1763 RVA: 0x000249A6 File Offset: 0x00022BA6
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006E4 RID: 1764 RVA: 0x000249B2 File Offset: 0x00022BB2
			public <>c()
			{
			}

			// Token: 0x060006E5 RID: 1765 RVA: 0x000249BA File Offset: 0x00022BBA
			[NullableContext(0)]
			internal bool <SyncNow>b__4_0(RpgItemData i)
			{
				return i != null && !string.IsNullOrEmpty(i.InstanceId);
			}

			// Token: 0x060006E6 RID: 1766 RVA: 0x000249CF File Offset: 0x00022BCF
			[NullableContext(0)]
			internal string <SyncNow>b__4_1(RpgItemData i)
			{
				return i.InstanceId;
			}

			// Token: 0x060006E7 RID: 1767 RVA: 0x000249D7 File Offset: 0x00022BD7
			[NullableContext(0)]
			internal RpgItemData <SyncNow>b__4_2(IGrouping<string, RpgItemData> g)
			{
				return g.First<RpgItemData>();
			}

			// Token: 0x04000342 RID: 834
			[Nullable(0)]
			public static readonly RpgEquippedGearSync.<>c <>9 = new RpgEquippedGearSync.<>c();

			// Token: 0x04000343 RID: 835
			[Nullable(0)]
			public static Func<RpgItemData, bool> <>9__4_0;

			// Token: 0x04000344 RID: 836
			[Nullable(0)]
			public static Func<RpgItemData, string> <>9__4_1;

			// Token: 0x04000345 RID: 837
			[Nullable(0)]
			public static Func<IGrouping<string, RpgItemData>, RpgItemData> <>9__4_2;
		}

		// Token: 0x020000E7 RID: 231
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x060006E8 RID: 1768 RVA: 0x000249DF File Offset: 0x00022BDF
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x060006E9 RID: 1769 RVA: 0x000249E7 File Offset: 0x00022BE7
			[NullableContext(0)]
			internal bool <SyncNow>b__3(RpgItemData i)
			{
				return i.InstanceId == this.id;
			}

			// Token: 0x04000346 RID: 838
			[Nullable(0)]
			public string id;
		}

		// Token: 0x020000E8 RID: 232
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_1
		{
			// Token: 0x060006EA RID: 1770 RVA: 0x000249FA File Offset: 0x00022BFA
			public <>c__DisplayClass4_1()
			{
			}

			// Token: 0x060006EB RID: 1771 RVA: 0x00024A02 File Offset: 0x00022C02
			[NullableContext(0)]
			internal bool <SyncNow>b__4(RpgEquippedGearRelic r)
			{
				return r.BoundInstanceId == this.item.InstanceId;
			}

			// Token: 0x04000347 RID: 839
			[Nullable(0)]
			public RpgItemData item;
		}

		// Token: 0x020000E9 RID: 233
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <RunAsync>d__3 : IAsyncStateMachine
		{
			// Token: 0x060006EC RID: 1772 RVA: 0x00024A1C File Offset: 0x00022C1C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					if (num != 0)
					{
						RpgEquippedGearSync._busy++;
					}
					try
					{
						TaskAwaiter awaiter;
						if (num == 0)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_79;
						}
						IL_1A:
						RpgEquippedGearSync._queued = 0;
						awaiter = RpgEquippedGearSync.SyncNow(this.player).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgEquippedGearSync.<RunAsync>d__3>(ref awaiter, ref this);
							return;
						}
						IL_79:
						awaiter.GetResult();
						if (RpgEquippedGearSync._queued > 0)
						{
							goto IL_1A;
						}
					}
					catch (Exception ex)
					{
						SpireTheRpgMainFile.Logger.Info("RpgEquippedGearSync failed: " + ex.Message, 1);
					}
					finally
					{
						if (num < 0)
						{
							RpgEquippedGearSync._busy--;
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
				this.<>t__builder.SetResult();
			}

			// Token: 0x060006ED RID: 1773 RVA: 0x00024B38 File Offset: 0x00022D38
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000348 RID: 840
			public int <>1__state;

			// Token: 0x04000349 RID: 841
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400034A RID: 842
			[Nullable(0)]
			public Player player;

			// Token: 0x0400034B RID: 843
			private TaskAwaiter <>u__1;
		}

		// Token: 0x020000EA RID: 234
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SyncNow>d__4 : IAsyncStateMachine
		{
			// Token: 0x060006EE RID: 1774 RVA: 0x00024B48 File Offset: 0x00022D48
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					if (num != 0)
					{
						if (num == 1)
						{
							goto IL_216;
						}
						try
						{
							RunManager instance = RunManager.Instance;
							if (instance == null || !instance.IsInProgress)
							{
								goto IL_35B;
							}
						}
						catch
						{
							goto IL_35B;
						}
						this.<wanted>5__2 = RpgActiveLoadout.EquippedItems(this.player).Where(new Func<RpgItemData, bool>(RpgEquippedGearSync.<>c.<>9.<SyncNow>b__4_0)).GroupBy(new Func<RpgItemData, string>(RpgEquippedGearSync.<>c.<>9.<SyncNow>b__4_1)).Select(new Func<IGrouping<string, RpgItemData>, RpgItemData>(RpgEquippedGearSync.<>c.<>9.<SyncNow>b__4_2)).ToList<RpgItemData>();
						this.<existing>5__3 = this.player.Relics.OfType<RpgEquippedGearRelic>().ToList<RpgEquippedGearRelic>();
						this.<>7__wrap3 = this.<existing>5__3.GetEnumerator();
					}
					try
					{
						if (num != 0)
						{
							goto IL_1B6;
						}
						IL_12B:
						RpgEquippedGearRelic rpgEquippedGearRelic;
						try
						{
							TaskAwaiter awaiter;
							if (num != 0)
							{
								awaiter = RelicCmd.Remove(rpgEquippedGearRelic).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 0);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgEquippedGearSync.<SyncNow>d__4>(ref awaiter, ref this);
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
							goto IL_1B6;
						}
						catch (Exception ex)
						{
							SpireTheRpgMainFile.Logger.Info("RpgEquippedGearSync remove failed: " + ex.Message, 1);
							goto IL_1B6;
						}
						IL_1AF:
						RpgItemData rpgItemData;
						rpgEquippedGearRelic.Bind(rpgItemData);
						IL_1B6:
						if (this.<>7__wrap3.MoveNext())
						{
							rpgEquippedGearRelic = this.<>7__wrap3.Current;
							RpgEquippedGearSync.<>c__DisplayClass4_0 CS$<>8__locals1 = new RpgEquippedGearSync.<>c__DisplayClass4_0();
							CS$<>8__locals1.id = rpgEquippedGearRelic.BoundInstanceId;
							rpgItemData = this.<wanted>5__2.FirstOrDefault(new Func<RpgItemData, bool>(CS$<>8__locals1.<SyncNow>b__3));
							if (rpgItemData == null)
							{
								goto IL_12B;
							}
							goto IL_1AF;
						}
					}
					finally
					{
						if (num < 0)
						{
							((IDisposable)this.<>7__wrap3).Dispose();
						}
					}
					this.<>7__wrap3 = default(List<RpgEquippedGearRelic>.Enumerator);
					this.<existing>5__3 = this.player.Relics.OfType<RpgEquippedGearRelic>().ToList<RpgEquippedGearRelic>();
					this.<>7__wrap4 = this.<wanted>5__2.GetEnumerator();
					IL_216:
					try
					{
						if (num == 1)
						{
							goto IL_256;
						}
						IL_2FE:
						while (this.<>7__wrap4.MoveNext())
						{
							RpgEquippedGearSync.<>c__DisplayClass4_1 CS$<>8__locals2 = new RpgEquippedGearSync.<>c__DisplayClass4_1();
							CS$<>8__locals2.item = this.<>7__wrap4.Current;
							if (!this.<existing>5__3.Any(new Func<RpgEquippedGearRelic, bool>(CS$<>8__locals2.<SyncNow>b__4)))
							{
								goto IL_256;
							}
						}
						goto IL_326;
						IL_256:
						try
						{
							TaskAwaiter<RelicModel> awaiter2;
							if (num != 1)
							{
								RpgEquippedGearRelic rpgEquippedGearRelic2 = (RpgEquippedGearRelic)ModelDb.Relic<RpgEquippedGearRelic>().ToMutable();
								RpgEquippedGearSync.<>c__DisplayClass4_1 CS$<>8__locals2;
								rpgEquippedGearRelic2.Bind(CS$<>8__locals2.item);
								awaiter2 = RelicCmd.Obtain(rpgEquippedGearRelic2, this.player, -1).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (this.<>1__state = 1);
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RelicModel>, RpgEquippedGearSync.<SyncNow>d__4>(ref awaiter2, ref this);
									return;
								}
							}
							else
							{
								awaiter2 = this.<>u__2;
								this.<>u__2 = default(TaskAwaiter<RelicModel>);
								num = (this.<>1__state = -1);
							}
							awaiter2.GetResult();
						}
						catch (Exception ex2)
						{
							SpireTheRpgMainFile.Logger.Info("RpgEquippedGearSync obtain failed: " + ex2.Message, 1);
						}
						goto IL_2FE;
					}
					finally
					{
						if (num < 0)
						{
							((IDisposable)this.<>7__wrap4).Dispose();
						}
					}
					IL_326:
					this.<>7__wrap4 = default(List<RpgItemData>.Enumerator);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<wanted>5__2 = null;
					this.<existing>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_35B:
				this.<>1__state = -2;
				this.<wanted>5__2 = null;
				this.<existing>5__3 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060006EF RID: 1775 RVA: 0x00024F68 File Offset: 0x00023168
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400034C RID: 844
			public int <>1__state;

			// Token: 0x0400034D RID: 845
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400034E RID: 846
			[Nullable(0)]
			public Player player;

			// Token: 0x0400034F RID: 847
			[Nullable(0)]
			private List<RpgItemData> <wanted>5__2;

			// Token: 0x04000350 RID: 848
			[Nullable(0)]
			private List<RpgEquippedGearRelic> <existing>5__3;

			// Token: 0x04000351 RID: 849
			[Nullable(0)]
			private List<RpgEquippedGearRelic>.Enumerator <>7__wrap3;

			// Token: 0x04000352 RID: 850
			private TaskAwaiter <>u__1;

			// Token: 0x04000353 RID: 851
			[Nullable(0)]
			private List<RpgItemData>.Enumerator <>7__wrap4;

			// Token: 0x04000354 RID: 852
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private TaskAwaiter<RelicModel> <>u__2;
		}
	}
}
