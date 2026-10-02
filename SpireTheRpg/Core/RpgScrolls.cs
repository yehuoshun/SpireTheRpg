using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Multiplayer.Game;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;

namespace SpireTheRpg.Core
{
	// Token: 0x02000060 RID: 96
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgScrolls
	{
		// Token: 0x0600041A RID: 1050 RVA: 0x0001995C File Offset: 0x00017B5C
		public static bool IsScrollId(string id)
		{
			return id.StartsWith("scroll_", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001996C File Offset: 0x00017B6C
		public static RpgScrollKind KindOf(string id)
		{
			string a = id.ToLowerInvariant();
			RpgScrollKind result;
			if (!(a == "scroll_cleanse"))
			{
				if (!(a == "scroll_gold"))
				{
					if (!(a == "scroll_identify"))
					{
						result = RpgScrollKind.Magic;
					}
					else
					{
						result = RpgScrollKind.Identify;
					}
				}
				else
				{
					result = RpgScrollKind.Gold;
				}
			}
			else
			{
				result = RpgScrollKind.Cleanse;
			}
			return result;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000199B8 File Offset: 0x00017BB8
		public static RpgItemData Create(RpgScrollKind kind, [Nullable(2)] Rng rng = null)
		{
			string text;
			switch (kind)
			{
			case RpgScrollKind.Cleanse:
				text = "scroll_cleanse";
				break;
			case RpgScrollKind.Gold:
				text = "scroll_gold";
				break;
			case RpgScrollKind.Identify:
				text = "scroll_identify";
				break;
			default:
				text = "scroll_magic";
				break;
			}
			string id = text;
			return new RpgItemData
			{
				InstanceId = Guid.NewGuid().ToString("N"),
				Id = id,
				Rarity = RpgItemRarity.Uncommon,
				ItemPower = 10,
				RequireLevel = 1,
				IconLetter = "S",
				IconPath = "item/other/scroll.png",
				EquipSlot = RpgEquipSlot.Accessory,
				IsScroll = true,
				IsIdentified = true,
				Primary = null,
				Ability = new List<string>(),
				HasLegendaryText = false
			};
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00019A78 File Offset: 0x00017C78
		public static RpgItemData RollRandom([Nullable(2)] Rng rng)
		{
			RpgScrollKind[] values = Enum.GetValues<RpgScrollKind>();
			return RpgScrolls.Create(values[RpgItemRoller.NextInt(rng, values.Length)], rng);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00019A9C File Offset: 0x00017C9C
		public static bool TryDrop(Player owner, RoomType roomType, Rng rng, [Nullable(2)] out RpgItemData scroll)
		{
			scroll = null;
			if (!RpgEffectiveConfig.EnableScrolls)
			{
				return false;
			}
			if (RpgItemRoller.NextDouble(rng) >= RpgEffectiveConfig.ScrollDropChance)
			{
				return false;
			}
			scroll = RpgScrolls.RollRandom(rng);
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 3);
			defaultInterpolatedStringHandler.AppendLiteral("RpgScroll drop ");
			defaultInterpolatedStringHandler.AppendFormatted(scroll.Id);
			defaultInterpolatedStringHandler.AppendLiteral(" net=");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(owner.NetId);
			defaultInterpolatedStringHandler.AppendLiteral(" room=");
			defaultInterpolatedStringHandler.AppendFormatted<RoomType>(roomType);
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			return true;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00019B30 File Offset: 0x00017D30
		public static Task<bool> TryUse(Player owner, RpgItemData scroll, [Nullable(2)] string targetInstanceId)
		{
			RpgScrolls.<TryUse>d__6 <TryUse>d__;
			<TryUse>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<TryUse>d__.owner = owner;
			<TryUse>d__.scroll = scroll;
			<TryUse>d__.targetInstanceId = targetInstanceId;
			<TryUse>d__.<>1__state = -1;
			<TryUse>d__.<>t__builder.Start<RpgScrolls.<TryUse>d__6>(ref <TryUse>d__);
			return <TryUse>d__.<>t__builder.Task;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00019B84 File Offset: 0x00017D84
		private static void TrySyncGoldGained(int amount)
		{
			if (amount <= 0)
			{
				return;
			}
			try
			{
				RunManager instance = RunManager.Instance;
				INetGameService netGameService = (instance != null) ? instance.NetService : null;
				if (instance != null && netGameService != null && netGameService.IsConnected)
				{
					NetGameType type = netGameService.Type;
					bool flag = type - 2 <= 1;
					if (flag)
					{
						instance.RewardSynchronizer.SyncLocalObtainedGold(amount);
					}
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("scroll SyncLocalObtainedGold failed: " + ex.Message, 1);
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00019C10 File Offset: 0x00017E10
		public static void ApplyUseFromNet(RpgScrollUseMessage msg)
		{
			if (msg.OwnerNetId == RpgNet.LocalNetId())
			{
				return;
			}
			try
			{
				RpgScrollBag.TryRemove(msg.OwnerNetId, msg.ScrollInstanceId);
				Player player = RpgNet.PlayerByNetId(msg.OwnerNetId);
				if (player != null && msg.GoldAfter >= 0 && player.Gold != msg.GoldAfter)
				{
					player.Gold = msg.GoldAfter;
				}
				if (msg.MagicArmed)
				{
					RpgMagicScrollPending.Arm();
				}
				RpgItemData rpgItemData = RpgScrollNet.DecodeItem(msg.ItemJson);
				if (rpgItemData != null && !string.IsNullOrWhiteSpace(rpgItemData.InstanceId))
				{
					rpgItemData.SyncVisualFromTemplate();
					if (RpgEffectiveConfig.EnableMetaEquipment)
					{
						RpgLoadout.TryReplaceByInstanceId(rpgItemData.InstanceId, rpgItemData, true);
					}
					else
					{
						RpgRunLoadout.TryReplaceByInstanceId(msg.OwnerNetId, rpgItemData.InstanceId, rpgItemData);
					}
				}
				Logger logger = SpireTheRpgMainFile.Logger;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 3);
				defaultInterpolatedStringHandler.AppendLiteral("RpgScrollNet apply kind=");
				defaultInterpolatedStringHandler.AppendFormatted<RpgScrollKind>((RpgScrollKind)msg.Kind);
				defaultInterpolatedStringHandler.AppendLiteral(" net=");
				defaultInterpolatedStringHandler.AppendFormatted<ulong>(msg.OwnerNetId);
				defaultInterpolatedStringHandler.AppendLiteral(" gold=");
				defaultInterpolatedStringHandler.AppendFormatted<int>(msg.GoldAfter);
				logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RpgScrollNet apply failed: " + ex.Message, 1);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00019D6C File Offset: 0x00017F6C
		[NullableContext(2)]
		private static bool UseCleanse([Nullable(1)] Player owner, string targetInstanceId, out RpgItemData changed)
		{
			changed = null;
			RpgItemData rpgItemData = RpgScrolls.FindItem(owner, targetInstanceId);
			if (rpgItemData == null || !RpgCurseSystem.TryCleanse(rpgItemData) || !RpgScrolls.Replace(owner, rpgItemData))
			{
				return false;
			}
			changed = rpgItemData.Clone();
			return true;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00019DA4 File Offset: 0x00017FA4
		[NullableContext(2)]
		private static bool UseIdentify([Nullable(1)] Player owner, string targetInstanceId, out RpgItemData changed)
		{
			changed = null;
			RpgItemData rpgItemData = RpgScrolls.FindItem(owner, targetInstanceId);
			if (rpgItemData == null || !RpgCurseSystem.TryIdentify(rpgItemData) || !RpgScrolls.Replace(owner, rpgItemData))
			{
				return false;
			}
			changed = rpgItemData.Clone();
			return true;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00019DDC File Offset: 0x00017FDC
		private static Task<bool> UseGold(Player owner)
		{
			RpgScrolls.<UseGold>d__11 <UseGold>d__;
			<UseGold>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<UseGold>d__.owner = owner;
			<UseGold>d__.<>1__state = -1;
			<UseGold>d__.<>t__builder.Start<RpgScrolls.<UseGold>d__11>(ref <UseGold>d__);
			return <UseGold>d__.<>t__builder.Task;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00019E20 File Offset: 0x00018020
		[return: TupleElementNames(new string[]
		{
			"Ok",
			"Armed"
		})]
		[return: Nullable(new byte[]
		{
			1,
			0
		})]
		private static Task<ValueTuple<bool, bool>> UseMagic(Player owner)
		{
			RpgScrolls.<UseMagic>d__12 <UseMagic>d__;
			<UseMagic>d__.<>t__builder = AsyncTaskMethodBuilder<ValueTuple<bool, bool>>.Create();
			<UseMagic>d__.owner = owner;
			<UseMagic>d__.<>1__state = -1;
			<UseMagic>d__.<>t__builder.Start<RpgScrolls.<UseMagic>d__12>(ref <UseMagic>d__);
			return <UseMagic>d__.<>t__builder.Task;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00019E64 File Offset: 0x00018064
		[NullableContext(2)]
		private static RpgItemData FindItem([Nullable(1)] Player owner, string instanceId)
		{
			if (string.IsNullOrWhiteSpace(instanceId))
			{
				return null;
			}
			return RpgActiveLoadout.AllItems(owner).FirstOrDefault((RpgItemData x) => string.Equals(x.InstanceId, instanceId, StringComparison.Ordinal));
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00019EA4 File Offset: 0x000180A4
		private static bool Replace(Player owner, RpgItemData item)
		{
			return RpgActiveLoadout.TryReplaceByInstanceId(item.InstanceId, item);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00019EB2 File Offset: 0x000180B2
		// Note: this type is marked as 'beforefieldinit'.
		static RpgScrolls()
		{
		}

		// Token: 0x0400015D RID: 349
		public static readonly string[] Ids = new string[]
		{
			"scroll_cleanse",
			"scroll_gold",
			"scroll_identify",
			"scroll_magic"
		};

		// Token: 0x02000130 RID: 304
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0
		{
			// Token: 0x06000811 RID: 2065 RVA: 0x0002983A File Offset: 0x00027A3A
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x06000812 RID: 2066 RVA: 0x00029842 File Offset: 0x00027A42
			[NullableContext(0)]
			internal bool <FindItem>b__0(RpgItemData x)
			{
				return string.Equals(x.InstanceId, this.instanceId, StringComparison.Ordinal);
			}

			// Token: 0x04000465 RID: 1125
			[Nullable(0)]
			public string instanceId;
		}

		// Token: 0x02000131 RID: 305
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x06000813 RID: 2067 RVA: 0x00029856 File Offset: 0x00027A56
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06000814 RID: 2068 RVA: 0x0002985E File Offset: 0x00027A5E
			[NullableContext(0)]
			internal bool <TryUse>b__0(RpgItemData x)
			{
				return x.InstanceId == this.scroll.InstanceId;
			}

			// Token: 0x04000466 RID: 1126
			[Nullable(0)]
			public RpgItemData scroll;
		}

		// Token: 0x02000132 RID: 306
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <TryUse>d__6 : IAsyncStateMachine
		{
			// Token: 0x06000815 RID: 2069 RVA: 0x00029878 File Offset: 0x00027A78
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result;
				try
				{
					bool flag;
					TaskAwaiter<bool> awaiter;
					if (num != 0)
					{
						TaskAwaiter<ValueTuple<bool, bool>> awaiter2;
						if (num != 1)
						{
							this.<>8__1 = new RpgScrolls.<>c__DisplayClass6_0();
							this.<>8__1.scroll = this.scroll;
							if (!this.<>8__1.scroll.IsScroll || !RpgEffectiveConfig.EnableScrolls)
							{
								result = false;
								goto IL_29D;
							}
							if (!RpgScrollBag.OfPlayer(this.owner.NetId).Any(new Func<RpgItemData, bool>(this.<>8__1.<TryUse>b__0)))
							{
								result = false;
								goto IL_29D;
							}
							this.<kind>5__2 = RpgScrolls.KindOf(this.<>8__1.scroll.Id);
							this.<changed>5__3 = null;
							this.<magicArmed>5__4 = false;
							flag = false;
							switch (this.<kind>5__2)
							{
							case RpgScrollKind.Cleanse:
								flag = RpgScrolls.UseCleanse(this.owner, this.targetInstanceId, out this.<changed>5__3);
								goto IL_1E8;
							case RpgScrollKind.Gold:
								awaiter = RpgScrolls.UseGold(this.owner).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgScrolls.<TryUse>d__6>(ref awaiter, ref this);
									return;
								}
								goto IL_147;
							case RpgScrollKind.Identify:
								flag = RpgScrolls.UseIdentify(this.owner, this.targetInstanceId, out this.<changed>5__3);
								goto IL_1E8;
							case RpgScrollKind.Magic:
								awaiter2 = RpgScrolls.UseMagic(this.owner).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<ValueTuple<bool, bool>>, RpgScrolls.<TryUse>d__6>(ref awaiter2, ref this);
									return;
								}
								break;
							default:
								goto IL_1E8;
							}
						}
						else
						{
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(TaskAwaiter<ValueTuple<bool, bool>>);
							this.<>1__state = -1;
						}
						ValueTuple<bool, bool> result2 = awaiter2.GetResult();
						flag = result2.Item1;
						this.<magicArmed>5__4 = result2.Item2;
						goto IL_1E8;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(TaskAwaiter<bool>);
					this.<>1__state = -1;
					IL_147:
					flag = awaiter.GetResult();
					IL_1E8:
					if (!flag)
					{
						result = false;
					}
					else
					{
						string instanceId = this.<>8__1.scroll.InstanceId;
						RpgScrollBag.TryRemove(this.owner.NetId, instanceId);
						RpgScrollKind rpgScrollKind = this.<kind>5__2;
						int num2;
						if (rpgScrollKind != RpgScrollKind.Gold)
						{
							if (rpgScrollKind != RpgScrollKind.Magic)
							{
								num2 = 0;
							}
							else
							{
								num2 = RpgEffectiveConfig.ScrollGoldAmount;
							}
						}
						else
						{
							num2 = RpgEffectiveConfig.ScrollGoldAmount;
						}
						int num3 = num2;
						if (num3 > 0)
						{
							RpgScrolls.TrySyncGoldGained(num3);
						}
						RpgScrollNet.BroadcastUse(this.owner.NetId, instanceId, this.<kind>5__2, -1, this.<magicArmed>5__4, this.<changed>5__3);
						result = true;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>8__1 = null;
					this.<changed>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_29D:
				this.<>1__state = -2;
				this.<>8__1 = null;
				this.<changed>5__3 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000816 RID: 2070 RVA: 0x00029B60 File Offset: 0x00027D60
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000467 RID: 1127
			public int <>1__state;

			// Token: 0x04000468 RID: 1128
			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000469 RID: 1129
			[Nullable(0)]
			public RpgItemData scroll;

			// Token: 0x0400046A RID: 1130
			[Nullable(0)]
			public Player owner;

			// Token: 0x0400046B RID: 1131
			[Nullable(0)]
			public string targetInstanceId;

			// Token: 0x0400046C RID: 1132
			[Nullable(0)]
			private RpgScrolls.<>c__DisplayClass6_0 <>8__1;

			// Token: 0x0400046D RID: 1133
			private RpgScrollKind <kind>5__2;

			// Token: 0x0400046E RID: 1134
			[Nullable(0)]
			private RpgItemData <changed>5__3;

			// Token: 0x0400046F RID: 1135
			private bool <magicArmed>5__4;

			// Token: 0x04000470 RID: 1136
			[Nullable(0)]
			private TaskAwaiter<bool> <>u__1;

			// Token: 0x04000471 RID: 1137
			[TupleElementNames(new string[]
			{
				"Ok",
				"Armed"
			})]
			[Nullable(0)]
			private TaskAwaiter<ValueTuple<bool, bool>> <>u__2;
		}

		// Token: 0x02000133 RID: 307
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <UseGold>d__11 : IAsyncStateMachine
		{
			// Token: 0x06000817 RID: 2071 RVA: 0x00029B70 File Offset: 0x00027D70
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result;
				try
				{
					try
					{
						TaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = PlayerCmd.GainGold(RpgEffectiveConfig.ScrollGoldAmount, this.owner, false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgScrolls.<UseGold>d__11>(ref awaiter, ref this);
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
						SpireTheRpgMainFile.Logger.Info("scroll gold failed: " + ex.Message, 1);
						result = false;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000818 RID: 2072 RVA: 0x00029C60 File Offset: 0x00027E60
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000472 RID: 1138
			public int <>1__state;

			// Token: 0x04000473 RID: 1139
			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000474 RID: 1140
			[Nullable(0)]
			public Player owner;

			// Token: 0x04000475 RID: 1141
			private TaskAwaiter <>u__1;
		}

		// Token: 0x02000134 RID: 308
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <UseMagic>d__12 : IAsyncStateMachine
		{
			// Token: 0x06000819 RID: 2073 RVA: 0x00029C70 File Offset: 0x00027E70
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ValueTuple<bool, bool> result;
				try
				{
					try
					{
						TaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = PlayerCmd.GainGold(RpgEffectiveConfig.ScrollGoldAmount, this.owner, false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgScrolls.<UseMagic>d__12>(ref awaiter, ref this);
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
						result = new ValueTuple<bool, bool>(true, false);
					}
					catch (Exception ex)
					{
						SpireTheRpgMainFile.Logger.Info("scroll magic failed: " + ex.Message, 1);
						result = new ValueTuple<bool, bool>(false, false);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600081A RID: 2074 RVA: 0x00029D70 File Offset: 0x00027F70
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000476 RID: 1142
			public int <>1__state;

			// Token: 0x04000477 RID: 1143
			[TupleElementNames(new string[]
			{
				"Ok",
				"Armed"
			})]
			[Nullable(0)]
			public AsyncTaskMethodBuilder<ValueTuple<bool, bool>> <>t__builder;

			// Token: 0x04000478 RID: 1144
			[Nullable(0)]
			public Player owner;

			// Token: 0x04000479 RID: 1145
			private TaskAwaiter <>u__1;
		}
	}
}
