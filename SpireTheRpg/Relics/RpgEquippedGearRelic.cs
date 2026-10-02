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
	// Token: 0x02000018 RID: 24
	[NullableContext(1)]
	[Nullable(0)]
	[Pool(typeof(EventRelicPool))]
	public sealed class RpgEquippedGearRelic : CustomRelicModel
	{
		// Token: 0x06000123 RID: 291 RVA: 0x0000D43C File Offset: 0x0000B63C
		public RpgEquippedGearRelic() : base(true)
		{
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000D445 File Offset: 0x0000B645
		// (set) Token: 0x06000125 RID: 293 RVA: 0x0000D44D File Offset: 0x0000B64D
		[Nullable(2)]
		internal RpgItemData Item
		{
			[NullableContext(2)]
			[CompilerGenerated]
			get
			{
				return this.<Item>k__BackingField;
			}
			[NullableContext(2)]
			[CompilerGenerated]
			private set
			{
				this.<Item>k__BackingField = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000D456 File Offset: 0x0000B656
		public override RelicRarity Rarity
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000D459 File Offset: 0x0000B659
		public override bool ShowCounter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000D45C File Offset: 0x0000B65C
		public override bool IsAllowed(IRunState runState)
		{
			return false;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000D460 File Offset: 0x0000B660
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new StringVar("InstanceId", ""),
					new StringVar("ItemName", ""),
					new StringVar("ItemType", "")
				});
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000D4AE File Offset: 0x0000B6AE
		public override string PackedIconPath
		{
			get
			{
				return SpireAssets.ItemRelicIconPath;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000D4B5 File Offset: 0x0000B6B5
		protected override string PackedIconOutlinePath
		{
			get
			{
				return SpireAssets.ItemRelicIconPath;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000D4BC File Offset: 0x0000B6BC
		protected override string BigIconPath
		{
			get
			{
				return SpireAssets.ItemRelicIconPath;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000D4C4 File Offset: 0x0000B6C4
		internal string BoundInstanceId
		{
			get
			{
				RpgItemData item = this.Item;
				string result;
				if ((result = ((item != null) ? item.InstanceId : null)) == null)
				{
					StringVar stringVar = base.DynamicVars["InstanceId"] as StringVar;
					result = (((stringVar != null) ? stringVar.StringValue : null) ?? "");
				}
				return result;
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000D514 File Offset: 0x0000B714
		internal void Bind(RpgItemData item)
		{
			this.Item = item;
			StringVar stringVar = base.DynamicVars["InstanceId"] as StringVar;
			if (stringVar != null)
			{
				stringVar.StringValue = (item.InstanceId ?? "");
			}
			StringVar stringVar2 = base.DynamicVars["ItemName"] as StringVar;
			if (stringVar2 != null)
			{
				stringVar2.StringValue = (item.DisplayName ?? item.Id);
			}
			StringVar stringVar3 = base.DynamicVars["ItemType"] as StringVar;
			if (stringVar3 != null)
			{
				stringVar3.StringValue = item.LocalizedTypeLine;
			}
			try
			{
				base.InvokeDisplayAmountChanged();
			}
			catch
			{
			}
		}

		// Token: 0x04000083 RID: 131
		[Nullable(2)]
		[CompilerGenerated]
		private RpgItemData <Item>k__BackingField;
	}
}
