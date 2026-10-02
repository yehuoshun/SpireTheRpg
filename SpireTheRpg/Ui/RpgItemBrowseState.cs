using System;
using System.Runtime.CompilerServices;
using SpireTheRpg.Core;

namespace SpireTheRpg.Ui
{
	// Token: 0x0200000C RID: 12
	internal sealed class RpgItemBrowseState
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00008BD9 File Offset: 0x00006DD9
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00008BE1 File Offset: 0x00006DE1
		public RpgEquipSlot? Slot
		{
			[CompilerGenerated]
			get
			{
				return this.<Slot>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Slot>k__BackingField = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00008BEA File Offset: 0x00006DEA
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00008BF2 File Offset: 0x00006DF2
		public bool OtherOnly
		{
			[CompilerGenerated]
			get
			{
				return this.<OtherOnly>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OtherOnly>k__BackingField = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00008BFB File Offset: 0x00006DFB
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00008C03 File Offset: 0x00006E03
		public RpgItemRarity? Rarity
		{
			[CompilerGenerated]
			get
			{
				return this.<Rarity>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Rarity>k__BackingField = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00008C0C File Offset: 0x00006E0C
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00008C14 File Offset: 0x00006E14
		public RpgItemSortMode Sort
		{
			[CompilerGenerated]
			get
			{
				return this.<Sort>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Sort>k__BackingField = value;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00008C1D File Offset: 0x00006E1D
		public RpgItemBrowseState()
		{
		}

		// Token: 0x0400004B RID: 75
		[CompilerGenerated]
		private RpgEquipSlot? <Slot>k__BackingField;

		// Token: 0x0400004C RID: 76
		[CompilerGenerated]
		private bool <OtherOnly>k__BackingField;

		// Token: 0x0400004D RID: 77
		[CompilerGenerated]
		private RpgItemRarity? <Rarity>k__BackingField;

		// Token: 0x0400004E RID: 78
		[CompilerGenerated]
		private RpgItemSortMode <Sort>k__BackingField;
	}
}
