using System;
using System.Runtime.CompilerServices;
using SpireTheRpg.Core;

namespace SpireTheRpg.Combat
{
	// Token: 0x0200006F RID: 111
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class RpgEquipmentRewardSave
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0001B968 File Offset: 0x00019B68
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x0001B970 File Offset: 0x00019B70
		public int RoomType
		{
			[CompilerGenerated]
			get
			{
				return this.<RoomType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RoomType>k__BackingField = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0001B979 File Offset: 0x00019B79
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x0001B981 File Offset: 0x00019B81
		public int ForcedRarity
		{
			[CompilerGenerated]
			get
			{
				return this.<ForcedRarity>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ForcedRarity>k__BackingField = value;
			}
		} = -1;

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0001B98A File Offset: 0x00019B8A
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x0001B992 File Offset: 0x00019B92
		public RpgItemData Item
		{
			[CompilerGenerated]
			get
			{
				return this.<Item>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Item>k__BackingField = value;
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001B99B File Offset: 0x00019B9B
		public RpgEquipmentRewardSave()
		{
		}

		// Token: 0x040001A2 RID: 418
		[CompilerGenerated]
		private int <RoomType>k__BackingField;

		// Token: 0x040001A3 RID: 419
		[CompilerGenerated]
		private int <ForcedRarity>k__BackingField;

		// Token: 0x040001A4 RID: 420
		[CompilerGenerated]
		private RpgItemData <Item>k__BackingField;
	}
}
