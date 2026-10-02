using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SpireTheRpg.Core
{
	// Token: 0x0200005C RID: 92
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RpgSaveDto
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x000194DD File Offset: 0x000176DD
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x000194E5 File Offset: 0x000176E5
		public int Level
		{
			[CompilerGenerated]
			get
			{
				return this.<Level>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Level>k__BackingField = value;
			}
		} = 1;

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x000194EE File Offset: 0x000176EE
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x000194F6 File Offset: 0x000176F6
		public int Xp
		{
			[CompilerGenerated]
			get
			{
				return this.<Xp>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Xp>k__BackingField = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x000194FF File Offset: 0x000176FF
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00019507 File Offset: 0x00017707
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

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00019510 File Offset: 0x00017710
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x00019518 File Offset: 0x00017718
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

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00019521 File Offset: 0x00017721
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x00019529 File Offset: 0x00017729
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

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00019532 File Offset: 0x00017732
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0001953A File Offset: 0x0001773A
		public List<string> BagIds
		{
			[CompilerGenerated]
			get
			{
				return this.<BagIds>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BagIds>k__BackingField = value;
			}
		} = new List<string>();

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00019543 File Offset: 0x00017743
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x0001954B File Offset: 0x0001774B
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public Dictionary<string, string> Equipped
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
				return this.<Equipped>k__BackingField;
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
				this.<Equipped>k__BackingField = value;
			}
		} = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0600040A RID: 1034 RVA: 0x00019554 File Offset: 0x00017754
		public RpgSaveDto()
		{
		}

		// Token: 0x0400014F RID: 335
		[CompilerGenerated]
		private int <Level>k__BackingField;

		// Token: 0x04000150 RID: 336
		[CompilerGenerated]
		private int <Xp>k__BackingField;

		// Token: 0x04000151 RID: 337
		[CompilerGenerated]
		private List<RpgItemData> <BagItems>k__BackingField;

		// Token: 0x04000152 RID: 338
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		[CompilerGenerated]
		private Dictionary<string, RpgItemData> <EquippedItems>k__BackingField;

		// Token: 0x04000153 RID: 339
		[CompilerGenerated]
		private List<RpgItemData> <Scrolls>k__BackingField;

		// Token: 0x04000154 RID: 340
		[CompilerGenerated]
		private List<string> <BagIds>k__BackingField;

		// Token: 0x04000155 RID: 341
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		[CompilerGenerated]
		private Dictionary<string, string> <Equipped>k__BackingField;
	}
}
