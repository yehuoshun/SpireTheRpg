using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using SpireTheRpg.Powers;

namespace SpireTheRpg.Cards
{
	// Token: 0x0200007A RID: 122
	[NullableContext(1)]
	[Nullable(0)]
	[Pool(typeof(ColorlessCardPool))]
	public sealed class StrikeForm : CustomCardModel
	{
		// Token: 0x06000552 RID: 1362 RVA: 0x0001D862 File Offset: 0x0001BA62
		public StrikeForm() : base(3, 3, 4, 1, true, true)
		{
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0001D870 File Offset: 0x0001BA70
		[Nullable(2)]
		public override Texture2D CustomPortrait
		{
			[NullableContext(2)]
			get
			{
				return SpireAssets.ItemIcon("card/strike_form.png");
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0001D87C File Offset: 0x0001BA7C
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				if (!base.IsUpgraded)
				{
					return new <>z__ReadOnlySingleElementList<CardKeyword>(2);
				}
				return Array.Empty<CardKeyword>();
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0001D8A1 File Offset: 0x0001BAA1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<StrikeFormPower>(1m),
					new DynamicVar("StrikeDamage", 0m),
					new DynamicVar("ExtraPlays", 1m)
				});
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0001D8E0 File Offset: 0x0001BAE0
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<StrikeFormPower>(null),
					HoverTipFactory.Static(15, Array.Empty<DynamicVar>())
				});
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001D918 File Offset: 0x0001BB18
		protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			StrikeForm.<OnPlay>d__9 <OnPlay>d__;
			<OnPlay>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<OnPlay>d__.<>4__this = this;
			<OnPlay>d__.choiceContext = choiceContext;
			<OnPlay>d__.<>1__state = -1;
			<OnPlay>d__.<>t__builder.Start<StrikeForm.<OnPlay>d__9>(ref <OnPlay>d__);
			return <OnPlay>d__.<>t__builder.Task;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001D964 File Offset: 0x0001BB64
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
			base.RemoveKeyword(2);
			base.DynamicVars["ExtraPlays"].UpgradeValueBy(1m);
			base.DynamicVars["StrikeFormPower"].UpgradeValueBy(1m);
			base.DynamicVars["StrikeDamage"].UpgradeValueBy(5m);
		}

		// Token: 0x0200014E RID: 334
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <OnPlay>d__9 : IAsyncStateMachine
		{
			// Token: 0x06000857 RID: 2135 RVA: 0x0002B9D8 File Offset: 0x00029BD8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				StrikeForm strikeForm = this.<>4__this;
				try
				{
					TaskAwaiter<StrikeFormPower> awaiter;
					TaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(TaskAwaiter<StrikeFormPower>);
							this.<>1__state = -1;
							goto IL_133;
						}
						Player owner = strikeForm.Owner;
						if (((owner != null) ? owner.Creature : null) == null)
						{
							goto IL_17A;
						}
						awaiter2 = CreatureCmd.TriggerAnim(strikeForm.Owner.Creature, "PowerUp", strikeForm.Owner.Character.PowerUpAnimDelay).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, StrikeForm.<OnPlay>d__9>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter2.GetResult();
					decimal baseValue = strikeForm.DynamicVars["ExtraPlays"].BaseValue;
					awaiter = PowerCmd.Apply<StrikeFormPower>(this.choiceContext, strikeForm.Owner.Creature, baseValue, strikeForm.Owner.Creature, strikeForm, false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<StrikeFormPower>, StrikeForm.<OnPlay>d__9>(ref awaiter, ref this);
						return;
					}
					IL_133:
					StrikeFormPower result = awaiter.GetResult();
					if (result != null)
					{
						result.Configure((int)strikeForm.DynamicVars["StrikeDamage"].BaseValue);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_17A:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000858 RID: 2136 RVA: 0x0002BB90 File Offset: 0x00029D90
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040004DB RID: 1243
			public int <>1__state;

			// Token: 0x040004DC RID: 1244
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040004DD RID: 1245
			[Nullable(0)]
			public StrikeForm <>4__this;

			// Token: 0x040004DE RID: 1246
			[Nullable(0)]
			public PlayerChoiceContext choiceContext;

			// Token: 0x040004DF RID: 1247
			private TaskAwaiter <>u__1;

			// Token: 0x040004E0 RID: 1248
			[Nullable(new byte[]
			{
				0,
				2
			})]
			private TaskAwaiter<StrikeFormPower> <>u__2;
		}
	}
}
