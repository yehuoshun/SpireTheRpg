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
	// Token: 0x02000070 RID: 112
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RpgFusionRestSiteOption : CustomRestSiteOption
	{
		// Token: 0x060004E5 RID: 1253 RVA: 0x0001B9AA File Offset: 0x00019BAA
		public RpgFusionRestSiteOption(Player owner) : base(owner)
		{
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0001B9B3 File Offset: 0x00019BB3
		public override string OptionId
		{
			get
			{
				return "SPIRE_RPG_FUSION";
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0001B9BA File Offset: 0x00019BBA
		[Nullable(2)]
		public override string CustomIconPath
		{
			[NullableContext(2)]
			get
			{
				return SpireAssets.ResAsset("ui/fusion_card_bg.png");
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0001B9C6 File Offset: 0x00019BC6
		public override IEnumerable<string> AssetPaths
		{
			get
			{
				return base.AssetPaths.Concat(NCardSmithVfx.AssetPaths);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0001B9D8 File Offset: 0x00019BD8
		public override bool IsEnabled
		{
			get
			{
				return RpgItemFusion.HasFusableGroup(RpgActiveLoadout.AllItems(base.Owner));
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001B9EA File Offset: 0x00019BEA
		public override Task<bool> OnSelect()
		{
			SpireLoc.ForceInject();
			return RpgRestSiteSync.RunFusion(base.Owner);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001B9FC File Offset: 0x00019BFC
		public override Task DoLocalPostSelectVfx(CancellationToken ct = default(CancellationToken))
		{
			return RpgRestSiteForgeVfx.PlayLocal(RpgRestSiteForgeVfx.TakePendingIcon(), ct);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001BA09 File Offset: 0x00019C09
		public override Task DoRemotePostSelectVfx()
		{
			return RpgRestSiteForgeVfx.PlayRemote(base.Owner);
		}
	}
}
