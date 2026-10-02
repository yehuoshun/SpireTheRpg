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
	// Token: 0x02000071 RID: 113
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RpgIntensifyRestSiteOption : CustomRestSiteOption
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x0001BA16 File Offset: 0x00019C16
		public RpgIntensifyRestSiteOption(Player owner) : base(owner)
		{
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0001BA1F File Offset: 0x00019C1F
		public override string OptionId
		{
			get
			{
				return "SPIRE_RPG_INTENSIFY";
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0001BA26 File Offset: 0x00019C26
		[Nullable(2)]
		public override string CustomIconPath
		{
			[NullableContext(2)]
			get
			{
				return SpireAssets.ResAsset("ui/intensify_card_bg.png");
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0001BA32 File Offset: 0x00019C32
		public override IEnumerable<string> AssetPaths
		{
			get
			{
				return base.AssetPaths.Concat(NCardSmithVfx.AssetPaths);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0001BA44 File Offset: 0x00019C44
		public override bool IsEnabled
		{
			get
			{
				return RpgActiveLoadout.AllItems(base.Owner).Any<RpgItemData>();
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001BA56 File Offset: 0x00019C56
		public override Task<bool> OnSelect()
		{
			SpireLoc.ForceInject();
			return RpgRestSiteSync.RunIntensify(base.Owner);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001BA68 File Offset: 0x00019C68
		public override Task DoLocalPostSelectVfx(CancellationToken ct = default(CancellationToken))
		{
			return RpgRestSiteForgeVfx.PlayLocal(RpgRestSiteForgeVfx.TakePendingIcon(), ct);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001BA75 File Offset: 0x00019C75
		public override Task DoRemotePostSelectVfx()
		{
			return RpgRestSiteForgeVfx.PlayRemote(base.Owner);
		}
	}
}
