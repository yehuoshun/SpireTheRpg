using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace SpireTheRpg.Core
{
	// Token: 0x02000064 RID: 100
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RpgStatMod
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0001A15B File Offset: 0x0001835B
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0001A163 File Offset: 0x00018363
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

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0001A16C File Offset: 0x0001836C
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0001A174 File Offset: 0x00018374
		public double Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Value>k__BackingField = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0001A17D File Offset: 0x0001837D
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x0001A185 File Offset: 0x00018385
		public bool Percent
		{
			[CompilerGenerated]
			get
			{
				return this.<Percent>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Percent>k__BackingField = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0001A190 File Offset: 0x00018390
		public string LocalizedName
		{
			get
			{
				string text = SpireLoc.Item("affix." + this.Id + ".name", "");
				if (!string.IsNullOrWhiteSpace(text))
				{
					return text;
				}
				return SpireLoc.Item("stat." + this.Id, this.Id);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0001A1E4 File Offset: 0x000183E4
		public string LocalizedDesc
		{
			get
			{
				string text = SpireLoc.Item("affix." + this.Id + ".effect", "");
				if (!string.IsNullOrWhiteSpace(text))
				{
					return text;
				}
				return SpireLoc.Item("stat." + this.Id + ".desc", "");
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001A23C File Offset: 0x0001843C
		public string FormatPrimary()
		{
			if (!this.Percent)
			{
				return SpireLoc.ItemFormat("ui.format.primary", "{0} {1}", new object[]
				{
					this.FormatValue(),
					this.LocalizedName
				});
			}
			return SpireLoc.ItemFormat("ui.format.primary_percent", "{0}% {1}", new object[]
			{
				this.FormatValue(),
				this.LocalizedName
			});
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001A2A0 File Offset: 0x000184A0
		public string FormatAffix()
		{
			if (this.IsDebuff)
			{
				if (!this.Percent)
				{
					return SpireLoc.ItemFormat("ui.format.debuff", "-{0} {1}", new object[]
					{
						this.FormatValue(),
						this.LocalizedName
					});
				}
				return SpireLoc.ItemFormat("ui.format.debuff_percent", "-{0}% {1}", new object[]
				{
					this.FormatValue(),
					this.LocalizedName
				});
			}
			else
			{
				if (!this.Percent)
				{
					return SpireLoc.ItemFormat("ui.format.affix", "+{0} {1}", new object[]
					{
						this.FormatValue(),
						this.LocalizedName
					});
				}
				return SpireLoc.ItemFormat("ui.format.affix_percent", "+{0}% {1}", new object[]
				{
					this.FormatValue(),
					this.LocalizedName
				});
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0001A364 File Offset: 0x00018564
		public bool IsDebuff
		{
			get
			{
				return RpgAffixCatalog.IsDebuffId(this.Id);
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001A374 File Offset: 0x00018574
		public string FormatDetail()
		{
			if (!this.Percent)
			{
				return SpireLoc.ItemFormat("ui.format.detail", "{0} {1}", new object[]
				{
					this.FormatValue(),
					this.LocalizedName
				});
			}
			return SpireLoc.ItemFormat("ui.format.detail_percent", "{1} {0}%", new object[]
			{
				this.FormatValue(),
				this.LocalizedName
			});
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001A3D8 File Offset: 0x000185D8
		public string FormatValue()
		{
			double num = Math.Abs(this.Value);
			if (Math.Abs(num % 1.0) < 0.0001)
			{
				return ((int)num).ToString(CultureInfo.InvariantCulture);
			}
			return num.ToString("0.0", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001A42D File Offset: 0x0001862D
		public string ToShorthand()
		{
			if (!this.Percent)
			{
				return this.Id + ":" + this.FormatValue();
			}
			return this.Id + ":" + this.FormatValue() + "%";
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001A46C File Offset: 0x0001866C
		[NullableContext(2)]
		public static RpgStatMod Parse(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				return null;
			}
			Match match = RpgStatMod.Shorthand.Match(text.Trim());
			if (!match.Success)
			{
				return null;
			}
			double value;
			if (!double.TryParse(match.Groups["value"].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
			{
				return null;
			}
			return new RpgStatMod
			{
				Id = match.Groups["id"].Value,
				Value = value,
				Percent = match.Groups["percent"].Success
			};
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001A50A File Offset: 0x0001870A
		public RpgStatMod()
		{
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001A51D File Offset: 0x0001871D
		// Note: this type is marked as 'beforefieldinit'.
		static RpgStatMod()
		{
		}

		// Token: 0x04000166 RID: 358
		private static readonly Regex Shorthand = new Regex("^(?<id>[a-zA-Z0-9_]+)\\s*:\\s*(?<value>-?\\d+(?:\\.\\d+)?)(?<percent>%)?\\s*$", RegexOptions.Compiled);

		// Token: 0x04000167 RID: 359
		[CompilerGenerated]
		private readonly string <Id>k__BackingField;

		// Token: 0x04000168 RID: 360
		[CompilerGenerated]
		private readonly double <Value>k__BackingField;

		// Token: 0x04000169 RID: 361
		[CompilerGenerated]
		private readonly bool <Percent>k__BackingField;
	}
}
