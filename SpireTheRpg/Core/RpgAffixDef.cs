using System;
using System.Runtime.CompilerServices;

namespace SpireTheRpg.Core
{
	// Token: 0x02000038 RID: 56
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RpgAffixDef
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00010D0D File Offset: 0x0000EF0D
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00010D15 File Offset: 0x0000EF15
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00010D1E File Offset: 0x0000EF1E
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00010D26 File Offset: 0x0000EF26
		public string Power
		{
			[CompilerGenerated]
			get
			{
				return this.<Power>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Power>k__BackingField = value;
			}
		} = "";

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00010D2F File Offset: 0x0000EF2F
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00010D37 File Offset: 0x0000EF37
		public string GameName
		{
			[CompilerGenerated]
			get
			{
				return this.<GameName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<GameName>k__BackingField = value;
			}
		} = "";

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00010D40 File Offset: 0x0000EF40
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00010D48 File Offset: 0x0000EF48
		public string Apply
		{
			[CompilerGenerated]
			get
			{
				return this.<Apply>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Apply>k__BackingField = value;
			}
		} = "special";

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00010D51 File Offset: 0x0000EF51
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00010D59 File Offset: 0x0000EF59
		public bool Rollable
		{
			[CompilerGenerated]
			get
			{
				return this.<Rollable>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Rollable>k__BackingField = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00010D62 File Offset: 0x0000EF62
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00010D6A File Offset: 0x0000EF6A
		public bool IsDebuff
		{
			[CompilerGenerated]
			get
			{
				return this.<IsDebuff>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsDebuff>k__BackingField = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00010D73 File Offset: 0x0000EF73
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00010D7B File Offset: 0x0000EF7B
		public string ValueKind
		{
			[CompilerGenerated]
			get
			{
				return this.<ValueKind>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ValueKind>k__BackingField = value;
			}
		} = "stacks";

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00010D84 File Offset: 0x0000EF84
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00010D8C File Offset: 0x0000EF8C
		public int Min
		{
			[CompilerGenerated]
			get
			{
				return this.<Min>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Min>k__BackingField = value;
			}
		} = 1;

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00010D95 File Offset: 0x0000EF95
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00010D9D File Offset: 0x0000EF9D
		public int Max
		{
			[CompilerGenerated]
			get
			{
				return this.<Max>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Max>k__BackingField = value;
			}
		} = 5;

		// Token: 0x06000208 RID: 520 RVA: 0x00010DA8 File Offset: 0x0000EFA8
		public RpgAffixDef()
		{
		}

		// Token: 0x0400009C RID: 156
		[CompilerGenerated]
		private string <Id>k__BackingField;

		// Token: 0x0400009D RID: 157
		[CompilerGenerated]
		private string <Power>k__BackingField;

		// Token: 0x0400009E RID: 158
		[CompilerGenerated]
		private string <GameName>k__BackingField;

		// Token: 0x0400009F RID: 159
		[CompilerGenerated]
		private string <Apply>k__BackingField;

		// Token: 0x040000A0 RID: 160
		[CompilerGenerated]
		private bool <Rollable>k__BackingField;

		// Token: 0x040000A1 RID: 161
		[CompilerGenerated]
		private bool <IsDebuff>k__BackingField;

		// Token: 0x040000A2 RID: 162
		[CompilerGenerated]
		private string <ValueKind>k__BackingField;

		// Token: 0x040000A3 RID: 163
		[CompilerGenerated]
		private int <Min>k__BackingField;

		// Token: 0x040000A4 RID: 164
		[CompilerGenerated]
		private int <Max>k__BackingField;
	}
}
