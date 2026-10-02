using System;
using System.Runtime.CompilerServices;
using SpireTheRpg.Core;

namespace SpireTheRpg.Combat
{
	// Token: 0x02000073 RID: 115
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RpgPendingExtraRewardEntry
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0001C02F File Offset: 0x0001A22F
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x0001C037 File Offset: 0x0001A237
		public string Kind
		{
			[CompilerGenerated]
			get
			{
				return this.<Kind>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Kind>k__BackingField = value;
			}
		} = "";

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0001C040 File Offset: 0x0001A240
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x0001C048 File Offset: 0x0001A248
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

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0001C051 File Offset: 0x0001A251
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x0001C059 File Offset: 0x0001A259
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

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0001C062 File Offset: 0x0001A262
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x0001C06A File Offset: 0x0001A26A
		[Nullable(2)]
		public RpgItemData Item
		{
			[NullableContext(2)]
			[CompilerGenerated]
			get
			{
				return this.<Item>k__BackingField;
			}
			[NullableContext(2)]
			[CompilerGenerated]
			set
			{
				this.<Item>k__BackingField = value;
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001C073 File Offset: 0x0001A273
		public RpgPendingExtraRewardEntry()
		{
		}

		// Token: 0x040001A7 RID: 423
		[CompilerGenerated]
		private string <Kind>k__BackingField;

		// Token: 0x040001A8 RID: 424
		[CompilerGenerated]
		private int <RoomType>k__BackingField;

		// Token: 0x040001A9 RID: 425
		[CompilerGenerated]
		private int <ForcedRarity>k__BackingField;

		// Token: 0x040001AA RID: 426
		[Nullable(2)]
		[CompilerGenerated]
		private RpgItemData <Item>k__BackingField;
	}
}
