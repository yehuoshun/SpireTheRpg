using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using SpireTheRpg.Core;

namespace SpireTheRpg.Combat
{
	// Token: 0x0200006B RID: 107
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RpgTradeRestSiteOption : CustomRestSiteOption
	{
		// Token: 0x060004BB RID: 1211 RVA: 0x0001B0B2 File Offset: 0x000192B2
		public RpgTradeRestSiteOption(Player owner) : base(owner)
		{
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0001B0BB File Offset: 0x000192BB
		public override string OptionId
		{
			get
			{
				return "SPIRE_RPG_TRADE";
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0001B0C2 File Offset: 0x000192C2
		[Nullable(2)]
		public override string CustomIconPath
		{
			[NullableContext(2)]
			get
			{
				return SpireAssets.ResAsset("ui/trade_card_bg.png");
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0001B0CE File Offset: 0x000192CE
		public override IEnumerable<string> AssetPaths
		{
			get
			{
				return base.AssetPaths.Concat(NCardSmithVfx.AssetPaths);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0001B0E0 File Offset: 0x000192E0
		public override bool IsEnabled
		{
			get
			{
				if (RpgEffectiveConfig.EnableTrade && !RpgEffectiveConfig.EnableMetaEquipment && base.Owner.RunState.Players.Count > 1)
				{
					IEnumerable<RpgItemData> source = RpgActiveLoadout.AllItems(base.Owner);
					Func<RpgItemData, bool> predicate;
					if ((predicate = RpgTradeRestSiteOption.<>O.<0>__CanTrade) == null)
					{
						predicate = (RpgTradeRestSiteOption.<>O.<0>__CanTrade = new Func<RpgItemData, bool>(RpgCurseSystem.CanTrade));
					}
					return source.Any(predicate);
				}
				return false;
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001B140 File Offset: 0x00019340
		public override Task<bool> OnSelect()
		{
			SpireLoc.ForceInject();
			return RpgTradeSync.Run(base.Owner);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001B152 File Offset: 0x00019352
		public override Task DoLocalPostSelectVfx(CancellationToken ct = default(CancellationToken))
		{
			return Task.CompletedTask;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001B159 File Offset: 0x00019359
		public override Task DoRemotePostSelectVfx()
		{
			return Task.CompletedTask;
		}

		// Token: 0x0200013E RID: 318
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040004A5 RID: 1189
			[Nullable(0)]
			public static Func<RpgItemData, bool> <0>__CanTrade;
		}
	}
}
