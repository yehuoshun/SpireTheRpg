using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Rooms;
using SpireTheRpg.Combat;
using SpireTheRpg.Core;

namespace SpireTheRpg.Relics
{
	// Token: 0x02000019 RID: 25
	[NullableContext(1)]
	[Nullable(0)]
	[Pool(typeof(EventRelicPool))]
	public sealed class RpgExpRelic : CustomRelicModel
	{
		// Token: 0x0600012F RID: 303 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
		public RpgExpRelic() : base(true)
		{
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000D5D1 File Offset: 0x0000B7D1
		public override RelicRarity Rarity
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000D5D4 File Offset: 0x0000B7D4
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000D5D7 File Offset: 0x0000B7D7
		public override int DisplayAmount
		{
			get
			{
				RpgProgress.Ensure();
				return RpgProgress.Xp;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000D5E4 File Offset: 0x0000B7E4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("Amount", 0m),
					new DynamicVar("XpToNext", 100m),
					new DynamicVar("Level", 1m)
				});
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000D634 File Offset: 0x0000B834
		public override string PackedIconPath
		{
			get
			{
				return SpireAssets.ExpRelicIconPath;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000D63B File Offset: 0x0000B83B
		protected override string PackedIconOutlinePath
		{
			get
			{
				return SpireAssets.ExpRelicIconPath;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000D642 File Offset: 0x0000B842
		protected override string BigIconPath
		{
			get
			{
				return SpireAssets.ExpRelicIconPath;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000D649 File Offset: 0x0000B849
		public override Task AfterObtained()
		{
			SpireLoc.ForceInject();
			this.SyncVars();
			return Task.CompletedTask;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000D65C File Offset: 0x0000B85C
		public override Task AfterCombatVictory(CombatRoom room)
		{
			RpgExpRelic.<AfterCombatVictory>d__16 <AfterCombatVictory>d__;
			<AfterCombatVictory>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AfterCombatVictory>d__.<>4__this = this;
			<AfterCombatVictory>d__.room = room;
			<AfterCombatVictory>d__.<>1__state = -1;
			<AfterCombatVictory>d__.<>t__builder.Start<RpgExpRelic.<AfterCombatVictory>d__16>(ref <AfterCombatVictory>d__);
			return <AfterCombatVictory>d__.<>t__builder.Task;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000D6A8 File Offset: 0x0000B8A8
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
				dynamicVar.BaseValue = RpgProgress.Xp;
			}
			DynamicVar dynamicVar2 = base.DynamicVars["XpToNext"];
			if (dynamicVar2 != null)
			{
				dynamicVar2.BaseValue = RpgProgress.XpToNext;
			}
			DynamicVar dynamicVar3 = base.DynamicVars["Level"];
			if (dynamicVar3 != null)
			{
				dynamicVar3.BaseValue = RpgProgress.Level;
			}
			base.InvokeDisplayAmountChanged();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000D735 File Offset: 0x0000B935
		[CompilerGenerated]
		[DebuggerHidden]
		private Task <>n__0(CombatRoom room)
		{
			return base.AfterCombatVictory(room);
		}

		// Token: 0x020000C2 RID: 194
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <AfterCombatVictory>d__16 : IAsyncStateMachine
		{
			// Token: 0x0600068D RID: 1677 RVA: 0x00021A58 File Offset: 0x0001FC58
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				RpgExpRelic rpgExpRelic = this.<>4__this;
				try
				{
					TaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = rpgExpRelic.<>n__0(this.room).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgExpRelic.<AfterCombatVictory>d__16>(ref awaiter, ref this);
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
					if (rpgExpRelic.Owner != null)
					{
						try
						{
							if (LocalContext.NetId == null || LocalContext.IsMe(rpgExpRelic.Owner))
							{
								int num2 = RpgLootTable.RollXp(this.room.RoomType, null);
								int num3 = RpgProgress.AddXp(num2);
								RpgCombatDrop.LastXpGained = num2;
								RpgCombatDrop.LastLevelsGained = num3;
								rpgExpRelic.SyncVars();
								RpgDataRelic relic = rpgExpRelic.Owner.GetRelic<RpgDataRelic>();
								if (relic != null)
								{
									relic.SyncVars();
								}
								Logger logger = SpireTheRpgMainFile.Logger;
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(22, 4);
								defaultInterpolatedStringHandler.AppendLiteral("Rpg XP +");
								defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
								defaultInterpolatedStringHandler.AppendLiteral(" levels=");
								defaultInterpolatedStringHandler.AppendFormatted<int>(num3);
								defaultInterpolatedStringHandler.AppendLiteral(" now=");
								defaultInterpolatedStringHandler.AppendFormatted<int>(RpgProgress.Xp);
								defaultInterpolatedStringHandler.AppendLiteral("/");
								defaultInterpolatedStringHandler.AppendFormatted<int>(RpgProgress.XpToNext);
								logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
							}
						}
						catch (Exception ex)
						{
							SpireTheRpgMainFile.Logger.Info("Rpg XP failed: " + ex.Message, 1);
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

			// Token: 0x0600068E RID: 1678 RVA: 0x00021C44 File Offset: 0x0001FE44
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040002AE RID: 686
			public int <>1__state;

			// Token: 0x040002AF RID: 687
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040002B0 RID: 688
			[Nullable(0)]
			public RpgExpRelic <>4__this;

			// Token: 0x040002B1 RID: 689
			[Nullable(0)]
			public CombatRoom room;

			// Token: 0x040002B2 RID: 690
			private TaskAwaiter <>u__1;
		}
	}
}
