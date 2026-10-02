using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Runs;
using SpireTheRpg.Core;

namespace SpireTheRpg.Relics
{
	// Token: 0x0200001B RID: 27
	[NullableContext(1)]
	[Nullable(0)]
	[Pool(typeof(EventRelicPool))]
	public sealed class RpgRandomItemShopRelic : CustomRelicModel
	{
		// Token: 0x06000147 RID: 327 RVA: 0x0000DBDD File Offset: 0x0000BDDD
		public RpgRandomItemShopRelic() : base(true)
		{
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000DBE6 File Offset: 0x0000BDE6
		// (set) Token: 0x06000149 RID: 329 RVA: 0x0000DBEE File Offset: 0x0000BDEE
		public int PurchaseCount
		{
			[CompilerGenerated]
			get
			{
				return this.<PurchaseCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<PurchaseCount>k__BackingField = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000DBF7 File Offset: 0x0000BDF7
		public override RelicRarity Rarity
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000DBFA File Offset: 0x0000BDFA
		public override bool IsAllowedInShops
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000DBFD File Offset: 0x0000BDFD
		public override int MerchantCost
		{
			get
			{
				return Math.Max(0, RpgEffectiveConfig.RandomItemShopPrice);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000DC0A File Offset: 0x0000BE0A
		public override string PackedIconPath
		{
			get
			{
				return SpireAssets.ItemRelicIconPath;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000DC11 File Offset: 0x0000BE11
		protected override string PackedIconOutlinePath
		{
			get
			{
				return SpireAssets.ItemRelicIconPath;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000DC18 File Offset: 0x0000BE18
		protected override string BigIconPath
		{
			get
			{
				return SpireAssets.ItemRelicIconPath;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000DC1F File Offset: 0x0000BE1F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Price", 100m));
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000DC37 File Offset: 0x0000BE37
		public override bool IsAllowed(IRunState runState)
		{
			return false;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000DC3C File Offset: 0x0000BE3C
		public void RefreshDisplayedPrice()
		{
			DynamicVar dynamicVar = base.DynamicVars["Price"];
			if (dynamicVar != null)
			{
				dynamicVar.BaseValue = this.MerchantCost;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000DC70 File Offset: 0x0000BE70
		public void IncrementPurchaseCount()
		{
			int purchaseCount = this.PurchaseCount;
			this.PurchaseCount = purchaseCount + 1;
		}

		// Token: 0x04000084 RID: 132
		[CompilerGenerated]
		private int <PurchaseCount>k__BackingField;
	}
}
