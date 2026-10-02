using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using SpireTheRpg.Core;
using SpireTheRpg.Ui;

namespace SpireTheRpg.Relics
{
	// Token: 0x02000017 RID: 23
	[NullableContext(1)]
	[Nullable(0)]
	[Pool(typeof(EventRelicPool))]
	public sealed class RpgDataRelic : CustomRelicModel
	{
		// Token: 0x06000115 RID: 277 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		public RpgDataRelic() : base(true)
		{
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000D1B1 File Offset: 0x0000B3B1
		public override RelicRarity Rarity
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000D1B4 File Offset: 0x0000B3B4
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000D1B7 File Offset: 0x0000B3B7
		public override int DisplayAmount
		{
			get
			{
				RpgProgress.Ensure();
				return RpgProgress.Level;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000D1C3 File Offset: 0x0000B3C3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("Amount", 1m),
					new StringVar("Effects", "")
				});
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000D1F4 File Offset: 0x0000B3F4
		public override string PackedIconPath
		{
			get
			{
				return SpireAssets.DataRelicIconPath;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000D1FB File Offset: 0x0000B3FB
		protected override string PackedIconOutlinePath
		{
			get
			{
				return SpireAssets.DataRelicIconPath;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000D202 File Offset: 0x0000B402
		protected override string BigIconPath
		{
			get
			{
				return SpireAssets.DataRelicIconPath;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000D209 File Offset: 0x0000B409
		public override Task AfterObtained()
		{
			SpireLoc.ForceInject();
			this.SyncVars();
			return Task.CompletedTask;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000D21C File Offset: 0x0000B41C
		public override Task BeforeCombatStart()
		{
			RpgDataRelic.<BeforeCombatStart>d__16 <BeforeCombatStart>d__;
			<BeforeCombatStart>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<BeforeCombatStart>d__.<>4__this = this;
			<BeforeCombatStart>d__.<>1__state = -1;
			<BeforeCombatStart>d__.<>t__builder.Start<RpgDataRelic.<BeforeCombatStart>d__16>(ref <BeforeCombatStart>d__);
			return <BeforeCombatStart>d__.<>t__builder.Task;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000D260 File Offset: 0x0000B460
		public override bool TryModifyRewardsLate(Player player, List<Reward> rewards, [Nullable(2)] AbstractRoom room)
		{
			if (player != base.Owner)
			{
				return false;
			}
			if (!RpgRerollDie.CanUse(player))
			{
				return false;
			}
			bool result = false;
			foreach (CardReward cardReward in rewards.OfType<CardReward>())
			{
				cardReward.CanReroll = true;
				result = true;
			}
			return result;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000D2C8 File Offset: 0x0000B4C8
		[NullableContext(2)]
		internal decimal CalcDamageMult(Creature dealer, CardModel cardSource)
		{
			if (cardSource != null && dealer != null)
			{
				Player owner = base.Owner;
				if (((owner != null) ? owner.Creature : null) != null)
				{
					if (dealer != base.Owner.Creature)
					{
						return 1m;
					}
					decimal num = 1m;
					double num2 = RpgActiveLoadout.PercentStat("shadow_damage", base.Owner);
					if (num2 > 0.0 && cardSource.IsUpgraded && cardSource.Tags.Contains(1))
					{
						num += (decimal)(num2 / 100.0);
					}
					double num3 = RpgActiveLoadout.PercentStat("critical_strike_damage", base.Owner);
					if (num3 > 0.0 && cardSource.Type == 1)
					{
						num += (decimal)(num3 / 100.0);
					}
					if (cardSource.Type == 1 && RpgAccessoryCombatHooks.HasActiveAccessory("glass_and_iron", base.Owner))
					{
						num += 0.15m;
					}
					return num;
				}
			}
			return 1m;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000D3C4 File Offset: 0x0000B5C4
		public void SyncVars()
		{
			if (base.IsCanonical)
			{
				return;
			}
			RpgProgress.Ensure();
			DynamicVar dynamicVar = base.DynamicVars["Amount"];
			if (dynamicVar != null)
			{
				dynamicVar.BaseValue = RpgProgress.Level;
			}
			StringVar stringVar = base.DynamicVars["Effects"] as StringVar;
			if (stringVar != null)
			{
				stringVar.StringValue = RpgActiveLoadout.EffectsSummary(8, base.Owner);
			}
			base.InvokeDisplayAmountChanged();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000D434 File Offset: 0x0000B634
		[CompilerGenerated]
		[DebuggerHidden]
		private Task <>n__0()
		{
			return base.BeforeCombatStart();
		}

		// Token: 0x020000C1 RID: 193
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <BeforeCombatStart>d__16 : IAsyncStateMachine
		{
			// Token: 0x0600068B RID: 1675 RVA: 0x0002172C File Offset: 0x0001F92C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				RpgDataRelic rpgDataRelic = this.<>4__this;
				try
				{
					TaskAwaiter awaiter;
					if (num != 0)
					{
						if (num - 1 <= 2)
						{
							goto IL_72;
						}
						awaiter = rpgDataRelic.<>n__0().GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgDataRelic.<BeforeCombatStart>d__16>(ref awaiter, ref this);
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
					IL_72:
					try
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
						switch (num)
						{
						case 1:
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							this.<>1__state = -1;
							break;
						case 2:
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							this.<>1__state = -1;
							goto IL_1BD;
						case 3:
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							this.<>1__state = -1;
							goto IL_21D;
						default:
						{
							RpgInRunInventory.Hide(false);
							RpgActiveLoadout.UnequipUnidentified(rpgDataRelic.Owner);
							rpgDataRelic.SyncVars();
							Player owner = rpgDataRelic.Owner;
							this.<creature>5__2 = ((owner != null) ? owner.Creature : null);
							if (this.<creature>5__2 == null || rpgDataRelic.Owner == null)
							{
								goto IL_2C6;
							}
							Logger logger = SpireTheRpgMainFile.Logger;
							defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(41, 1);
							defaultInterpolatedStringHandler.AppendLiteral("RpgDataRelic BeforeCombatStart enter net=");
							defaultInterpolatedStringHandler.AppendFormatted<ulong>(rpgDataRelic.Owner.NetId);
							logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
							awaiter = RpgCombatBuffApplier.ApplyEquipped(this.<creature>5__2).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgDataRelic.<BeforeCombatStart>d__16>(ref awaiter, ref this);
								return;
							}
							break;
						}
						}
						awaiter.GetResult();
						awaiter = RpgAccessoryCombatHooks.ApplyCombatStart(this.<creature>5__2).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgDataRelic.<BeforeCombatStart>d__16>(ref awaiter, ref this);
							return;
						}
						IL_1BD:
						awaiter.GetResult();
						awaiter = RpgCurseCombat.ApplyEquipped(this.<creature>5__2).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 3;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgDataRelic.<BeforeCombatStart>d__16>(ref awaiter, ref this);
							return;
						}
						IL_21D:
						awaiter.GetResult();
						rpgDataRelic.Flash();
						RpgEquippedGearSync.Request(rpgDataRelic.Owner);
						Logger logger2 = SpireTheRpgMainFile.Logger;
						defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(40, 1);
						defaultInterpolatedStringHandler.AppendLiteral("RpgDataRelic BeforeCombatStart done net=");
						defaultInterpolatedStringHandler.AppendFormatted<ulong>(rpgDataRelic.Owner.NetId);
						logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
						this.<creature>5__2 = null;
					}
					catch (Exception value)
					{
						Logger logger3 = SpireTheRpgMainFile.Logger;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 1);
						defaultInterpolatedStringHandler.AppendLiteral("RpgDataRelic BeforeCombatStart failed: ");
						defaultInterpolatedStringHandler.AppendFormatted<Exception>(value);
						logger3.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_2C6:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600068C RID: 1676 RVA: 0x00021A48 File Offset: 0x0001FC48
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040002A9 RID: 681
			public int <>1__state;

			// Token: 0x040002AA RID: 682
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040002AB RID: 683
			[Nullable(0)]
			public RpgDataRelic <>4__this;

			// Token: 0x040002AC RID: 684
			private TaskAwaiter <>u__1;

			// Token: 0x040002AD RID: 685
			[Nullable(0)]
			private Creature <creature>5__2;
		}
	}
}
