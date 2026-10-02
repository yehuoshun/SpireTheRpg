using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SpireTheRpg.Combat;

namespace SpireTheRpg.Core
{
	// Token: 0x02000058 RID: 88
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RpgRunEquipmentDto
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00018077 File Offset: 0x00016277
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0001807F File Offset: 0x0001627F
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

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00018088 File Offset: 0x00016288
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x00018090 File Offset: 0x00016290
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

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00018099 File Offset: 0x00016299
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x000180A1 File Offset: 0x000162A1
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

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x000180AA File Offset: 0x000162AA
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x000180B2 File Offset: 0x000162B2
		public List<RpgItemData> Scrolls
		{
			[CompilerGenerated]
			get
			{
				return this.<Scrolls>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Scrolls>k__BackingField = value;
			}
		} = new List<RpgItemData>();

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x000180BB File Offset: 0x000162BB
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x000180C3 File Offset: 0x000162C3
		public List<RpgPendingExtraRewardEntry> PendingExtraRewards
		{
			[CompilerGenerated]
			get
			{
				return this.<PendingExtraRewards>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PendingExtraRewards>k__BackingField = value;
			}
		} = new List<RpgPendingExtraRewardEntry>();

		// Token: 0x060003C7 RID: 967 RVA: 0x000180CC File Offset: 0x000162CC
		public RpgRunEquipmentDto()
		{
		}

		// Token: 0x04000142 RID: 322
		[CompilerGenerated]
		private List<RpgItemData> <BagItems>k__BackingField;

		// Token: 0x04000143 RID: 323
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		[CompilerGenerated]
		private Dictionary<string, RpgItemData> <EquippedItems>k__BackingField;

		// Token: 0x04000144 RID: 324
		[CompilerGenerated]
		private List<RpgItemData> <PendingLoot>k__BackingField;

		// Token: 0x04000145 RID: 325
		[CompilerGenerated]
		private List<RpgItemData> <Scrolls>k__BackingField;

		// Token: 0x04000146 RID: 326
		[CompilerGenerated]
		private List<RpgPendingExtraRewardEntry> <PendingExtraRewards>k__BackingField;
	}
}
