using System;
using System.Linq;
using System.Runtime.CompilerServices;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;

namespace SpireTheRpg.Powers
{
	// Token: 0x0200001C RID: 28
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class StrikeFormPower : CustomPowerModel
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000DC8D File Offset: 0x0000BE8D
		public override PowerType Type
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000DC90 File Offset: 0x0000BE90
		public override PowerStackType StackType
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000DC93 File Offset: 0x0000BE93
		public override string CustomPackedIconPath
		{
			get
			{
				return SpireAssets.ResAsset("power/strike_from.png");
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000DC9F File Offset: 0x0000BE9F
		public override string CustomBigIconPath
		{
			get
			{
				return SpireAssets.ResAsset("power/strike_from.png");
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000DCAB File Offset: 0x0000BEAB
		[NullableContext(1)]
		protected override object InitInternalData()
		{
			return new StrikeFormPower.Data();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000DCB2 File Offset: 0x0000BEB2
		public void Configure(int strikeDamageBonus)
		{
			base.GetInternalData<StrikeFormPower.Data>().StrikeDamageBonus = Math.Max(base.GetInternalData<StrikeFormPower.Data>().StrikeDamageBonus, Math.Max(0, strikeDamageBonus));
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000DCD8 File Offset: 0x0000BED8
		[NullableContext(1)]
		public override int ModifyCardPlayCount(CardModel card, [Nullable(2)] Creature target, int playCount)
		{
			Player owner = card.Owner;
			if (((owner != null) ? owner.Creature : null) != base.Owner)
			{
				return playCount;
			}
			if (card.Type != 1)
			{
				return playCount;
			}
			if (!card.Tags.Contains(1))
			{
				return playCount;
			}
			base.Flash();
			return playCount + base.Amount;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000DD2C File Offset: 0x0000BF2C
		public decimal CalcStrikeDamageBonus(Creature dealer, CardModel cardSource)
		{
			if (dealer != base.Owner || cardSource == null)
			{
				return 0m;
			}
			if (cardSource.Type != 1)
			{
				return 0m;
			}
			if (!cardSource.Tags.Contains(1))
			{
				return 0m;
			}
			int strikeDamageBonus = base.GetInternalData<StrikeFormPower.Data>().StrikeDamageBonus;
			if (strikeDamageBonus <= 0)
			{
				return 0m;
			}
			return strikeDamageBonus;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000DD8A File Offset: 0x0000BF8A
		public StrikeFormPower()
		{
		}

		// Token: 0x020000C3 RID: 195
		[NullableContext(0)]
		private sealed class Data
		{
			// Token: 0x0600068F RID: 1679 RVA: 0x00021C52 File Offset: 0x0001FE52
			public Data()
			{
			}

			// Token: 0x040002B3 RID: 691
			public int StrikeDamageBonus;
		}
	}
}
