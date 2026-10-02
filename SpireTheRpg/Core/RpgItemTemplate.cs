using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SpireTheRpg.Core
{
	// Token: 0x0200004A RID: 74
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RpgItemTemplate
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x000149F6 File Offset: 0x00012BF6
		// (set) Token: 0x060002DA RID: 730 RVA: 0x000149FE File Offset: 0x00012BFE
		public string Id
		{
			[CompilerGenerated]
			get
			{
				return this.<Id>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Id>k__BackingField = value;
			}
		} = "";

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00014A07 File Offset: 0x00012C07
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00014A0F File Offset: 0x00012C0F
		public string IconLetter
		{
			[CompilerGenerated]
			get
			{
				return this.<IconLetter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IconLetter>k__BackingField = value;
			}
		} = "?";

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00014A18 File Offset: 0x00012C18
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00014A20 File Offset: 0x00012C20
		[Nullable(2)]
		public string IconPath
		{
			[NullableContext(2)]
			[CompilerGenerated]
			get
			{
				return this.<IconPath>k__BackingField;
			}
			[NullableContext(2)]
			[CompilerGenerated]
			set
			{
				this.<IconPath>k__BackingField = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00014A29 File Offset: 0x00012C29
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x00014A31 File Offset: 0x00012C31
		public RpgEquipSlot EquipSlot
		{
			[CompilerGenerated]
			get
			{
				return this.<EquipSlot>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EquipSlot>k__BackingField = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00014A3A File Offset: 0x00012C3A
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x00014A42 File Offset: 0x00012C42
		[Nullable(2)]
		public string RequireCharacter
		{
			[NullableContext(2)]
			[CompilerGenerated]
			get
			{
				return this.<RequireCharacter>k__BackingField;
			}
			[NullableContext(2)]
			[CompilerGenerated]
			set
			{
				this.<RequireCharacter>k__BackingField = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00014A4B File Offset: 0x00012C4B
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x00014A53 File Offset: 0x00012C53
		public List<string> RequireSlots
		{
			[CompilerGenerated]
			get
			{
				return this.<RequireSlots>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RequireSlots>k__BackingField = value;
			}
		} = new List<string>();

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00014A5C File Offset: 0x00012C5C
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x00014A64 File Offset: 0x00012C64
		public List<string> RequireItemIds
		{
			[CompilerGenerated]
			get
			{
				return this.<RequireItemIds>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RequireItemIds>k__BackingField = value;
			}
		} = new List<string>();

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00014A6D File Offset: 0x00012C6D
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x00014A75 File Offset: 0x00012C75
		[Nullable(2)]
		public string RequireItemIdPrefix
		{
			[NullableContext(2)]
			[CompilerGenerated]
			get
			{
				return this.<RequireItemIdPrefix>k__BackingField;
			}
			[NullableContext(2)]
			[CompilerGenerated]
			set
			{
				this.<RequireItemIdPrefix>k__BackingField = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00014A7E File Offset: 0x00012C7E
		// (set) Token: 0x060002EA RID: 746 RVA: 0x00014A86 File Offset: 0x00012C86
		public bool RequireAllSlotsFilled
		{
			[CompilerGenerated]
			get
			{
				return this.<RequireAllSlotsFilled>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RequireAllSlotsFilled>k__BackingField = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00014A8F File Offset: 0x00012C8F
		// (set) Token: 0x060002EC RID: 748 RVA: 0x00014A97 File Offset: 0x00012C97
		[Nullable(2)]
		public string RequireGlovesMinRarity
		{
			[NullableContext(2)]
			[CompilerGenerated]
			get
			{
				return this.<RequireGlovesMinRarity>k__BackingField;
			}
			[NullableContext(2)]
			[CompilerGenerated]
			set
			{
				this.<RequireGlovesMinRarity>k__BackingField = value;
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00014AA0 File Offset: 0x00012CA0
		public RpgItemTemplate()
		{
		}

		// Token: 0x040000E5 RID: 229
		[CompilerGenerated]
		private readonly string <Id>k__BackingField;

		// Token: 0x040000E6 RID: 230
		[CompilerGenerated]
		private readonly string <IconLetter>k__BackingField;

		// Token: 0x040000E7 RID: 231
		[Nullable(2)]
		[CompilerGenerated]
		private readonly string <IconPath>k__BackingField;

		// Token: 0x040000E8 RID: 232
		[CompilerGenerated]
		private readonly RpgEquipSlot <EquipSlot>k__BackingField;

		// Token: 0x040000E9 RID: 233
		[Nullable(2)]
		[CompilerGenerated]
		private readonly string <RequireCharacter>k__BackingField;

		// Token: 0x040000EA RID: 234
		[CompilerGenerated]
		private readonly List<string> <RequireSlots>k__BackingField;

		// Token: 0x040000EB RID: 235
		[CompilerGenerated]
		private readonly List<string> <RequireItemIds>k__BackingField;

		// Token: 0x040000EC RID: 236
		[Nullable(2)]
		[CompilerGenerated]
		private readonly string <RequireItemIdPrefix>k__BackingField;

		// Token: 0x040000ED RID: 237
		[CompilerGenerated]
		private readonly bool <RequireAllSlotsFilled>k__BackingField;

		// Token: 0x040000EE RID: 238
		[Nullable(2)]
		[CompilerGenerated]
		private readonly string <RequireGlovesMinRarity>k__BackingField;
	}
}
