using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Runs;
using SpireTheRpg.Ui;

namespace SpireTheRpg.Core
{
	// Token: 0x02000066 RID: 102
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgTradeSync
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x0001A6A0 File Offset: 0x000188A0
		public static Task<bool> Run(Player owner)
		{
			RpgTradeSync.<Run>d__1 <Run>d__;
			<Run>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<Run>d__.owner = owner;
			<Run>d__.<>1__state = -1;
			<Run>d__.<>t__builder.Start<RpgTradeSync.<Run>d__1>(ref <Run>d__);
			return <Run>d__.<>t__builder.Task;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001A6E4 File Offset: 0x000188E4
		private static Task<bool> RunLocal(Player owner, Player partner)
		{
			RpgTradeSync.<RunLocal>d__2 <RunLocal>d__;
			<RunLocal>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<RunLocal>d__.owner = owner;
			<RunLocal>d__.partner = partner;
			<RunLocal>d__.<>1__state = -1;
			<RunLocal>d__.<>t__builder.Start<RpgTradeSync.<RunLocal>d__2>(ref <RunLocal>d__);
			return <RunLocal>d__.<>t__builder.Task;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001A730 File Offset: 0x00018930
		private static bool ApplyTrade(Player owner, Player partner, string mineId, string theirsId)
		{
			if (string.IsNullOrWhiteSpace(mineId) || string.IsNullOrWhiteSpace(theirsId))
			{
				return false;
			}
			RpgItemData rpgItemData = RpgActiveLoadout.AllItems(owner).FirstOrDefault((RpgItemData x) => string.Equals(x.InstanceId, mineId, StringComparison.Ordinal));
			RpgItemData rpgItemData2 = RpgActiveLoadout.AllItems(partner).FirstOrDefault((RpgItemData x) => string.Equals(x.InstanceId, theirsId, StringComparison.Ordinal));
			if (rpgItemData == null || rpgItemData2 == null)
			{
				return false;
			}
			if (!RpgCurseSystem.CanTrade(rpgItemData) || !RpgCurseSystem.CanTrade(rpgItemData2))
			{
				return false;
			}
			if (rpgItemData.IsScroll || rpgItemData2.IsScroll)
			{
				return false;
			}
			RpgItemData rpgItemData3 = rpgItemData.Clone();
			RpgItemData rpgItemData4 = rpgItemData2.Clone();
			rpgItemData3.InstanceId = Guid.NewGuid().ToString("N");
			rpgItemData4.InstanceId = Guid.NewGuid().ToString("N");
			if (!RpgTradeSync.RemoveFromPlayer(owner, mineId))
			{
				return false;
			}
			if (!RpgTradeSync.RemoveFromPlayer(partner, theirsId))
			{
				RpgActiveLoadout.AddToBag(owner, rpgItemData);
				return false;
			}
			RpgActiveLoadout.AddToBag(partner, rpgItemData3);
			RpgActiveLoadout.AddToBag(owner, rpgItemData4);
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(19, 4);
			defaultInterpolatedStringHandler.AppendLiteral("RpgTrade ok ");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(owner.NetId);
			defaultInterpolatedStringHandler.AppendLiteral(":");
			defaultInterpolatedStringHandler.AppendFormatted(rpgItemData.Id);
			defaultInterpolatedStringHandler.AppendLiteral(" <-> ");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(partner.NetId);
			defaultInterpolatedStringHandler.AppendLiteral(":");
			defaultInterpolatedStringHandler.AppendFormatted(rpgItemData2.Id);
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			return true;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001A8C2 File Offset: 0x00018AC2
		private static bool RemoveFromPlayer(Player player, string instanceId)
		{
			if (RpgEffectiveConfig.EnableMetaEquipment)
			{
				return RpgLoadout.TryRemoveManyByInstanceIds(new <>z__ReadOnlySingleElementList<string>(instanceId), true);
			}
			return RpgRunLoadout.TryRemoveManyByInstanceIds(player.NetId, new <>z__ReadOnlySingleElementList<string>(instanceId));
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001A8EC File Offset: 0x00018AEC
		private static PlayerChoiceResult Encode(bool ok, string mine, string theirs)
		{
			List<int> list = new List<int>();
			list.Add(1380992852);
			list.Add((ok > false) ? 1 : 0);
			RpgTradeSync.EncodeString(list, mine ?? "");
			RpgTradeSync.EncodeString(list, theirs ?? "");
			return PlayerChoiceResult.FromIndexes(list);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001A938 File Offset: 0x00018B38
		private static bool TryDecode(PlayerChoiceResult remote, out bool ok, out string mine, out string theirs)
		{
			ok = false;
			mine = "";
			theirs = "";
			List<int> list = remote.AsIndexes();
			if (list == null || list.Count < 4 || list[0] != 1380992852)
			{
				return false;
			}
			ok = (list[1] != 0);
			int start;
			int num;
			return RpgTradeSync.TryDecodeString(list, 2, out mine, out start) && RpgTradeSync.TryDecodeString(list, start, out theirs, out num);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001A9A0 File Offset: 0x00018BA0
		private static void EncodeString(List<int> list, string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			list.Add(bytes.Length);
			foreach (byte item in bytes)
			{
				list.Add((int)item);
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001A9E0 File Offset: 0x00018BE0
		private static bool TryDecodeString(IReadOnlyList<int> indexes, int start, out string value, out int next)
		{
			value = "";
			next = start;
			if (start >= indexes.Count)
			{
				return false;
			}
			int num = indexes[start];
			if (num < 0 || start + 1 + num > indexes.Count)
			{
				return false;
			}
			byte[] array = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (byte)indexes[start + 1 + i];
			}
			value = Encoding.UTF8.GetString(array);
			next = start + 1 + num;
			return true;
		}

		// Token: 0x0400016B RID: 363
		private const int Magic = 1380992852;

		// Token: 0x02000135 RID: 309
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x0600081B RID: 2075 RVA: 0x00029D7E File Offset: 0x00027F7E
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x0600081C RID: 2076 RVA: 0x00029D86 File Offset: 0x00027F86
			[NullableContext(0)]
			internal bool <Run>b__0(Player p)
			{
				return p.NetId != this.owner.NetId;
			}

			// Token: 0x0400047A RID: 1146
			[Nullable(0)]
			public Player owner;
		}

		// Token: 0x02000136 RID: 310
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x0600081D RID: 2077 RVA: 0x00029D9E File Offset: 0x00027F9E
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x0600081E RID: 2078 RVA: 0x00029DA6 File Offset: 0x00027FA6
			[NullableContext(0)]
			internal bool <ApplyTrade>b__0(RpgItemData x)
			{
				return string.Equals(x.InstanceId, this.mineId, StringComparison.Ordinal);
			}

			// Token: 0x0600081F RID: 2079 RVA: 0x00029DBA File Offset: 0x00027FBA
			[NullableContext(0)]
			internal bool <ApplyTrade>b__1(RpgItemData x)
			{
				return string.Equals(x.InstanceId, this.theirsId, StringComparison.Ordinal);
			}

			// Token: 0x0400047B RID: 1147
			[Nullable(0)]
			public string mineId;

			// Token: 0x0400047C RID: 1148
			[Nullable(0)]
			public string theirsId;
		}

		// Token: 0x02000137 RID: 311
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <Run>d__1 : IAsyncStateMachine
		{
			// Token: 0x06000820 RID: 2080 RVA: 0x00029DD0 File Offset: 0x00027FD0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result;
				try
				{
					TaskAwaiter awaiter;
					TaskAwaiter<bool> awaiter2;
					TaskAwaiter<RpgTradeScreen.PickResult> awaiter3;
					TaskAwaiter<PlayerChoiceResult> awaiter4;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_1B6;
					case 2:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(TaskAwaiter<RpgTradeScreen.PickResult>);
						this.<>1__state = -1;
						goto IL_25B;
					case 3:
						awaiter4 = this.<>u__4;
						this.<>u__4 = default(TaskAwaiter<PlayerChoiceResult>);
						this.<>1__state = -1;
						goto IL_33C;
					default:
						this.<>8__1 = new RpgTradeSync.<>c__DisplayClass1_0();
						this.<>8__1.owner = this.owner;
						if (this.<>8__1.owner.RunState.Players.Count <= 1)
						{
							result = false;
							goto IL_3A5;
						}
						awaiter = RpgLoadoutNetSync.SyncAllBeforeCombat(this.<>8__1.owner.RunState, null).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgTradeSync.<Run>d__1>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					this.<partner>5__2 = this.<>8__1.owner.RunState.Players.FirstOrDefault(new Func<Player, bool>(this.<>8__1.<Run>b__0));
					if (this.<partner>5__2 == null)
					{
						result = false;
						goto IL_3A5;
					}
					RunManager instance = RunManager.Instance;
					bool flag = ((instance != null) ? instance.NetService : null) != null && instance.NetService.Type != 1;
					this.<sync>5__3 = ((instance != null) ? instance.PlayerChoiceSynchronizer : null);
					if (!flag || this.<sync>5__3 == null)
					{
						awaiter2 = RpgTradeSync.RunLocal(this.<>8__1.owner, this.<partner>5__2).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgTradeSync.<Run>d__1>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						this.<choiceId>5__4 = this.<sync>5__3.ReserveChoiceId(this.<>8__1.owner);
						if (RpgNet.IsLocal(this.<>8__1.owner))
						{
							awaiter3 = RpgTradeScreen.OfferAsync(this.<>8__1.owner, this.<partner>5__2).GetAwaiter();
							if (!awaiter3.IsCompleted)
							{
								this.<>1__state = 2;
								this.<>u__3 = awaiter3;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgTradeScreen.PickResult>, RpgTradeSync.<Run>d__1>(ref awaiter3, ref this);
								return;
							}
							goto IL_25B;
						}
						else
						{
							awaiter4 = this.<sync>5__3.WaitForRemoteChoice(this.<>8__1.owner, this.<choiceId>5__4).GetAwaiter();
							if (!awaiter4.IsCompleted)
							{
								this.<>1__state = 3;
								this.<>u__4 = awaiter4;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<PlayerChoiceResult>, RpgTradeSync.<Run>d__1>(ref awaiter4, ref this);
								return;
							}
							goto IL_33C;
						}
					}
					IL_1B6:
					result = awaiter2.GetResult();
					goto IL_3A5;
					IL_25B:
					RpgTradeScreen.PickResult result2 = awaiter3.GetResult();
					bool flag2 = result2.Ok && RpgTradeSync.ApplyTrade(this.<>8__1.owner, this.<partner>5__2, result2.MineId, result2.TheirsId);
					this.<sync>5__3.SyncLocalChoice(this.<>8__1.owner, this.<choiceId>5__4, RpgTradeSync.Encode(flag2, result2.MineId, result2.TheirsId));
					result = flag2;
					goto IL_3A5;
					IL_33C:
					bool flag3;
					string mineId;
					string theirsId;
					if (!RpgTradeSync.TryDecode(awaiter4.GetResult(), out flag3, out mineId, out theirsId))
					{
						result = false;
					}
					else
					{
						result = (flag3 && RpgTradeSync.ApplyTrade(this.<>8__1.owner, this.<partner>5__2, mineId, theirsId));
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>8__1 = null;
					this.<partner>5__2 = null;
					this.<sync>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_3A5:
				this.<>1__state = -2;
				this.<>8__1 = null;
				this.<partner>5__2 = null;
				this.<sync>5__3 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000821 RID: 2081 RVA: 0x0002A1C8 File Offset: 0x000283C8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400047D RID: 1149
			public int <>1__state;

			// Token: 0x0400047E RID: 1150
			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x0400047F RID: 1151
			[Nullable(0)]
			public Player owner;

			// Token: 0x04000480 RID: 1152
			[Nullable(0)]
			private RpgTradeSync.<>c__DisplayClass1_0 <>8__1;

			// Token: 0x04000481 RID: 1153
			[Nullable(0)]
			private Player <partner>5__2;

			// Token: 0x04000482 RID: 1154
			[Nullable(0)]
			private PlayerChoiceSynchronizer <sync>5__3;

			// Token: 0x04000483 RID: 1155
			private uint <choiceId>5__4;

			// Token: 0x04000484 RID: 1156
			private TaskAwaiter <>u__1;

			// Token: 0x04000485 RID: 1157
			[Nullable(0)]
			private TaskAwaiter<bool> <>u__2;

			// Token: 0x04000486 RID: 1158
			[Nullable(0)]
			private TaskAwaiter<RpgTradeScreen.PickResult> <>u__3;

			// Token: 0x04000487 RID: 1159
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private TaskAwaiter<PlayerChoiceResult> <>u__4;
		}

		// Token: 0x02000138 RID: 312
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <RunLocal>d__2 : IAsyncStateMachine
		{
			// Token: 0x06000822 RID: 2082 RVA: 0x0002A1D8 File Offset: 0x000283D8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result2;
				try
				{
					TaskAwaiter<RpgTradeScreen.PickResult> awaiter;
					if (num != 0)
					{
						awaiter = RpgTradeScreen.OfferAsync(this.owner, this.partner).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgTradeScreen.PickResult>, RpgTradeSync.<RunLocal>d__2>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<RpgTradeScreen.PickResult>);
						this.<>1__state = -1;
					}
					RpgTradeScreen.PickResult result = awaiter.GetResult();
					result2 = (result.Ok && RpgTradeSync.ApplyTrade(this.owner, this.partner, result.MineId, result.TheirsId));
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06000823 RID: 2083 RVA: 0x0002A2C0 File Offset: 0x000284C0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000488 RID: 1160
			public int <>1__state;

			// Token: 0x04000489 RID: 1161
			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x0400048A RID: 1162
			[Nullable(0)]
			public Player owner;

			// Token: 0x0400048B RID: 1163
			[Nullable(0)]
			public Player partner;

			// Token: 0x0400048C RID: 1164
			[Nullable(0)]
			private TaskAwaiter<RpgTradeScreen.PickResult> <>u__1;
		}
	}
}
