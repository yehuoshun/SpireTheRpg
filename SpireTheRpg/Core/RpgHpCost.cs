using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;

namespace SpireTheRpg.Core
{
	// Token: 0x02000042 RID: 66
	internal static class RpgHpCost
	{
		// Token: 0x06000265 RID: 613 RVA: 0x00012094 File Offset: 0x00010294
		[NullableContext(1)]
		public static Task<bool> PayNonLethal(Player owner, int cost)
		{
			RpgHpCost.<PayNonLethal>d__0 <PayNonLethal>d__;
			<PayNonLethal>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<PayNonLethal>d__.owner = owner;
			<PayNonLethal>d__.cost = cost;
			<PayNonLethal>d__.<>1__state = -1;
			<PayNonLethal>d__.<>t__builder.Start<RpgHpCost.<PayNonLethal>d__0>(ref <PayNonLethal>d__);
			return <PayNonLethal>d__.<>t__builder.Task;
		}

		// Token: 0x020000F1 RID: 241
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <PayNonLethal>d__0 : IAsyncStateMachine
		{
			// Token: 0x060006FF RID: 1791 RVA: 0x000259EC File Offset: 0x00023BEC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result;
				try
				{
					Creature creature;
					if (num != 0)
					{
						if (this.cost <= 0)
						{
							result = true;
							goto IL_E8;
						}
						creature = this.owner.Creature;
						if (creature == null)
						{
							result = false;
							goto IL_E8;
						}
					}
					try
					{
						TaskAwaiter awaiter;
						if (num != 0)
						{
							int value = Math.Max(1, creature.CurrentHp - this.cost);
							awaiter = CreatureCmd.SetCurrentHp(creature, value).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgHpCost.<PayNonLethal>d__0>(ref awaiter, ref this);
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
						result = true;
					}
					catch (Exception ex)
					{
						SpireTheRpgMainFile.Logger.Info("RpgHpCost failed: " + ex.Message, 1);
						result = false;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_E8:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000700 RID: 1792 RVA: 0x00025B14 File Offset: 0x00023D14
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine([Nullable(1)] IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000371 RID: 881
			public int <>1__state;

			// Token: 0x04000372 RID: 882
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000373 RID: 883
			public int cost;

			// Token: 0x04000374 RID: 884
			public Player owner;

			// Token: 0x04000375 RID: 885
			private TaskAwaiter <>u__1;
		}
	}
}
