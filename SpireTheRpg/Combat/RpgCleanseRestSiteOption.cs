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
	// Token: 0x0200006A RID: 106
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RpgCleanseRestSiteOption : CustomRestSiteOption
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x0001B01E File Offset: 0x0001921E
		public RpgCleanseRestSiteOption(Player owner) : base(owner)
		{
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0001B027 File Offset: 0x00019227
		public override string OptionId
		{
			get
			{
				return "SPIRE_RPG_CLEANSE";
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0001B02E File Offset: 0x0001922E
		[Nullable(2)]
		public override string CustomIconPath
		{
			[NullableContext(2)]
			get
			{
				return SpireAssets.ResAsset("ui/cleanse_card_bg.png");
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0001B03A File Offset: 0x0001923A
		public override IEnumerable<string> AssetPaths
		{
			get
			{
				return base.AssetPaths.Concat(NCardSmithVfx.AssetPaths);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0001B04C File Offset: 0x0001924C
		public override bool IsEnabled
		{
			get
			{
				if (RpgEffectiveConfig.EnableCurses)
				{
					return RpgActiveLoadout.AllItems(base.Owner).Any((RpgItemData x) => x.HasCurse || x.IsBound);
				}
				return false;
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001B086 File Offset: 0x00019286
		public override Task<bool> OnSelect()
		{
			SpireLoc.ForceInject();
			return RpgRestSiteSync.RunCleanse(base.Owner);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001B098 File Offset: 0x00019298
		public override Task DoLocalPostSelectVfx(CancellationToken ct = default(CancellationToken))
		{
			return RpgRestSiteForgeVfx.PlayLocal(RpgRestSiteForgeVfx.TakePendingIcon(), ct);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001B0A5 File Offset: 0x000192A5
		public override Task DoRemotePostSelectVfx()
		{
			return RpgRestSiteForgeVfx.PlayRemote(base.Owner);
		}

		// Token: 0x0200013D RID: 317
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000830 RID: 2096 RVA: 0x0002A87A File Offset: 0x00028A7A
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000831 RID: 2097 RVA: 0x0002A886 File Offset: 0x00028A86
			public <>c()
			{
			}

			// Token: 0x06000832 RID: 2098 RVA: 0x0002A88E File Offset: 0x00028A8E
			[NullableContext(0)]
			internal bool <get_IsEnabled>b__8_0(RpgItemData x)
			{
				return x.HasCurse || x.IsBound;
			}

			// Token: 0x040004A3 RID: 1187
			[Nullable(0)]
			public static readonly RpgCleanseRestSiteOption.<>c <>9 = new RpgCleanseRestSiteOption.<>c();

			// Token: 0x040004A4 RID: 1188
			[Nullable(0)]
			public static Func<RpgItemData, bool> <>9__8_0;
		}
	}
}
