using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SpireTheRpg.Core
{
	// Token: 0x02000059 RID: 89
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RpgRunEquipmentAllDto
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0001811B File Offset: 0x0001631B
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00018123 File Offset: 0x00016323
		public Dictionary<string, RpgRunEquipmentDto> Players
		{
			[CompilerGenerated]
			get
			{
				return this.<Players>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Players>k__BackingField = value;
			}
		} = new Dictionary<string, RpgRunEquipmentDto>(StringComparer.Ordinal);

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0001812C File Offset: 0x0001632C
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00018134 File Offset: 0x00016334
		public List<RpgItemData> BagItems
		{
			[CompilerGenerated]
			get
			{
				return this.<BagItems>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BagItems>k__BackingField = value;
			}
		} = new List<RpgItemData>();

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0001813D File Offset: 0x0001633D
		// (set) Token: 0x060003CD RID: 973 RVA: 0x00018145 File Offset: 0x00016345
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public Dictionary<string, RpgItemData> EquippedItems
		{
			[CompilerGenerated]
			[return: Nullable(new byte[]
			{
				1,
				1,
				2
			})]
			get
			{
				return this.<EquippedItems>k__BackingField;
			}
			[CompilerGenerated]
			[param: Nullable(new byte[]
			{
				1,
				1,
				2
			})]
			set
			{
				this.<EquippedItems>k__BackingField = value;
			}
		} = new Dictionary<string, RpgItemData>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0001814E File Offset: 0x0001634E
		// (set) Token: 0x060003CF RID: 975 RVA: 0x00018156 File Offset: 0x00016356
		public List<RpgItemData> PendingLoot
		{
			[CompilerGenerated]
			get
			{
				return this.<PendingLoot>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PendingLoot>k__BackingField = value;
			}
		} = new List<RpgItemData>();

		// Token: 0x060003D0 RID: 976 RVA: 0x0001815F File Offset: 0x0001635F
		public RpgRunEquipmentAllDto()
		{
		}

		// Token: 0x04000147 RID: 327
		[CompilerGenerated]
		private Dictionary<string, RpgRunEquipmentDto> <Players>k__BackingField;

		// Token: 0x04000148 RID: 328
		[CompilerGenerated]
		private List<RpgItemData> <BagItems>k__BackingField;

		// Token: 0x04000149 RID: 329
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		[CompilerGenerated]
		private Dictionary<string, RpgItemData> <EquippedItems>k__BackingField;

		// Token: 0x0400014A RID: 330
		[CompilerGenerated]
		private List<RpgItemData> <PendingLoot>k__BackingField;
	}
}
