using System;
using System.Runtime.CompilerServices;

namespace SpireTheRpg.Core
{
	// Token: 0x02000050 RID: 80
	[NullableContext(1)]
	[Nullable(0)]
	[RequiredMember]
	internal sealed class RpgAbilityInfo
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000321 RID: 801 RVA: 0x000160D7 File Offset: 0x000142D7
		// (set) Token: 0x06000322 RID: 802 RVA: 0x000160DF File Offset: 0x000142DF
		[RequiredMember]
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
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000323 RID: 803 RVA: 0x000160E8 File Offset: 0x000142E8
		// (set) Token: 0x06000324 RID: 804 RVA: 0x000160F0 File Offset: 0x000142F0
		[RequiredMember]
		public string Title
		{
			[CompilerGenerated]
			get
			{
				return this.<Title>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Title>k__BackingField = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000325 RID: 805 RVA: 0x000160F9 File Offset: 0x000142F9
		// (set) Token: 0x06000326 RID: 806 RVA: 0x00016101 File Offset: 0x00014301
		[RequiredMember]
		public string Description
		{
			[CompilerGenerated]
			get
			{
				return this.<Description>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Description>k__BackingField = value;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0001610A File Offset: 0x0001430A
		[Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
		[CompilerFeatureRequired("RequiredMembers")]
		public RpgAbilityInfo()
		{
		}

		// Token: 0x040000FE RID: 254
		[CompilerGenerated]
		private readonly string <Id>k__BackingField;

		// Token: 0x040000FF RID: 255
		[CompilerGenerated]
		private readonly string <Title>k__BackingField;

		// Token: 0x04000100 RID: 256
		[CompilerGenerated]
		private readonly string <Description>k__BackingField;
	}
}
