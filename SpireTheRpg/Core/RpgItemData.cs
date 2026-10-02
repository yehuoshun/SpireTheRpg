using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SpireTheRpg.Core
{
	// Token: 0x02000046 RID: 70
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RpgItemData
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00012931 File Offset: 0x00010B31
		// (set) Token: 0x06000273 RID: 627 RVA: 0x00012939 File Offset: 0x00010B39
		public string InstanceId
		{
			[CompilerGenerated]
			get
			{
				return this.<InstanceId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<InstanceId>k__BackingField = value;
			}
		} = Guid.NewGuid().ToString("N");

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00012942 File Offset: 0x00010B42
		// (set) Token: 0x06000275 RID: 629 RVA: 0x0001294A File Offset: 0x00010B4A
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

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00012953 File Offset: 0x00010B53
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0001295B File Offset: 0x00010B5B
		public RpgItemRarity Rarity
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

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00012964 File Offset: 0x00010B64
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0001296C File Offset: 0x00010B6C
		public int ItemPower
		{
			[CompilerGenerated]
			get
			{
				return this.<ItemPower>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ItemPower>k__BackingField = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00012975 File Offset: 0x00010B75
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0001297D File Offset: 0x00010B7D
		public int RequireLevel
		{
			[CompilerGenerated]
			get
			{
				return this.<RequireLevel>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<RequireLevel>k__BackingField = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00012986 File Offset: 0x00010B86
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0001298E File Offset: 0x00010B8E
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00012997 File Offset: 0x00010B97
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0001299F File Offset: 0x00010B9F
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

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000280 RID: 640 RVA: 0x000129A8 File Offset: 0x00010BA8
		// (set) Token: 0x06000281 RID: 641 RVA: 0x000129B0 File Offset: 0x00010BB0
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

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000282 RID: 642 RVA: 0x000129B9 File Offset: 0x00010BB9
		// (set) Token: 0x06000283 RID: 643 RVA: 0x000129C1 File Offset: 0x00010BC1
		[Nullable(2)]
		public string Primary
		{
			[NullableContext(2)]
			[CompilerGenerated]
			get
			{
				return this.<Primary>k__BackingField;
			}
			[NullableContext(2)]
			[CompilerGenerated]
			set
			{
				this.<Primary>k__BackingField = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000284 RID: 644 RVA: 0x000129CA File Offset: 0x00010BCA
		// (set) Token: 0x06000285 RID: 645 RVA: 0x000129D2 File Offset: 0x00010BD2
		public List<string> Details
		{
			[CompilerGenerated]
			get
			{
				return this.<Details>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Details>k__BackingField = value;
			}
		} = new List<string>();

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000286 RID: 646 RVA: 0x000129DB File Offset: 0x00010BDB
		// (set) Token: 0x06000287 RID: 647 RVA: 0x000129E3 File Offset: 0x00010BE3
		public List<string> Ability
		{
			[CompilerGenerated]
			get
			{
				return this.<Ability>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Ability>k__BackingField = value;
			}
		} = new List<string>();

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000288 RID: 648 RVA: 0x000129EC File Offset: 0x00010BEC
		// (set) Token: 0x06000289 RID: 649 RVA: 0x000129F4 File Offset: 0x00010BF4
		public List<string> HiddenAbility
		{
			[CompilerGenerated]
			get
			{
				return this.<HiddenAbility>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HiddenAbility>k__BackingField = value;
			}
		} = new List<string>();

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600028A RID: 650 RVA: 0x000129FD File Offset: 0x00010BFD
		// (set) Token: 0x0600028B RID: 651 RVA: 0x00012A05 File Offset: 0x00010C05
		public List<string> Curses
		{
			[CompilerGenerated]
			get
			{
				return this.<Curses>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Curses>k__BackingField = value;
			}
		} = new List<string>();

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00012A0E File Offset: 0x00010C0E
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00012A16 File Offset: 0x00010C16
		public bool HasLegendaryText
		{
			[CompilerGenerated]
			get
			{
				return this.<HasLegendaryText>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HasLegendaryText>k__BackingField = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00012A1F File Offset: 0x00010C1F
		// (set) Token: 0x0600028F RID: 655 RVA: 0x00012A27 File Offset: 0x00010C27
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00012A30 File Offset: 0x00010C30
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00012A38 File Offset: 0x00010C38
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

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00012A41 File Offset: 0x00010C41
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00012A49 File Offset: 0x00010C49
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00012A52 File Offset: 0x00010C52
		// (set) Token: 0x06000295 RID: 661 RVA: 0x00012A5A File Offset: 0x00010C5A
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

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00012A63 File Offset: 0x00010C63
		// (set) Token: 0x06000297 RID: 663 RVA: 0x00012A6B File Offset: 0x00010C6B
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

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00012A74 File Offset: 0x00010C74
		// (set) Token: 0x06000299 RID: 665 RVA: 0x00012A7C File Offset: 0x00010C7C
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

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00012A85 File Offset: 0x00010C85
		// (set) Token: 0x0600029B RID: 667 RVA: 0x00012A8D File Offset: 0x00010C8D
		public int IntensifyLevel
		{
			[CompilerGenerated]
			get
			{
				return this.<IntensifyLevel>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IntensifyLevel>k__BackingField = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00012A96 File Offset: 0x00010C96
		// (set) Token: 0x0600029D RID: 669 RVA: 0x00012A9E File Offset: 0x00010C9E
		public bool IsIdentified
		{
			[CompilerGenerated]
			get
			{
				return this.<IsIdentified>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsIdentified>k__BackingField = value;
			}
		} = true;

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00012AA7 File Offset: 0x00010CA7
		// (set) Token: 0x0600029F RID: 671 RVA: 0x00012AAF File Offset: 0x00010CAF
		public bool IsBound
		{
			[CompilerGenerated]
			get
			{
				return this.<IsBound>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsBound>k__BackingField = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00012AB8 File Offset: 0x00010CB8
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x00012AC0 File Offset: 0x00010CC0
		public bool IsScroll
		{
			[CompilerGenerated]
			get
			{
				return this.<IsScroll>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsScroll>k__BackingField = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00012AC9 File Offset: 0x00010CC9
		public bool IsRarePlus
		{
			get
			{
				return this.Rarity >= RpgItemRarity.Rare;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00012AD7 File Offset: 0x00010CD7
		public bool HasCurse
		{
			get
			{
				return this.Curses.Count > 0;
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00012AE7 File Offset: 0x00010CE7
		public IEnumerable<string> EffectiveAbilityLines()
		{
			RpgItemData.<EffectiveAbilityLines>d__100 <EffectiveAbilityLines>d__ = new RpgItemData.<EffectiveAbilityLines>d__100(-2);
			<EffectiveAbilityLines>d__.<>4__this = this;
			return <EffectiveAbilityLines>d__;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00012AF8 File Offset: 0x00010CF8
		public string LocalizedName
		{
			get
			{
				if (!this.IsScroll)
				{
					return SpireLoc.Item(this.Id + ".name", this.Id);
				}
				return SpireLoc.Item(this.Id + ".name", this.Id);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00012B44 File Offset: 0x00010D44
		public string DisplayName
		{
			get
			{
				string text = this.LocalizedName;
				if (!this.IsIdentified)
				{
					text = SpireLoc.Item("ui.unidentified", "Unidentified {0}").Replace("{0}", this.LocalizedRarity);
				}
				if (this.IntensifyLevel > 0)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
					defaultInterpolatedStringHandler.AppendFormatted(text);
					defaultInterpolatedStringHandler.AppendLiteral(" +");
					defaultInterpolatedStringHandler.AppendFormatted<int>(this.IntensifyLevel);
					text = defaultInterpolatedStringHandler.ToStringAndClear();
				}
				if (this.IsBound)
				{
					text = text + " [" + SpireLoc.Item("ui.bound", "Bound") + "]";
				}
				return text;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00012BE4 File Offset: 0x00010DE4
		public string LocalizedRarity
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
				defaultInterpolatedStringHandler.AppendLiteral("rarity.");
				defaultInterpolatedStringHandler.AppendFormatted<RpgItemRarity>(this.Rarity);
				return SpireLoc.Item(defaultInterpolatedStringHandler.ToStringAndClear(), this.Rarity.ToString());
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00012C34 File Offset: 0x00010E34
		public string LocalizedItemType
		{
			get
			{
				if (!this.IsScroll)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
					defaultInterpolatedStringHandler.AppendLiteral("type.");
					defaultInterpolatedStringHandler.AppendFormatted<RpgEquipSlot>(this.EquipSlot);
					return SpireLoc.Item(defaultInterpolatedStringHandler.ToStringAndClear(), this.EquipSlot.ToString());
				}
				return SpireLoc.Item("type.Scroll", "Scroll");
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00012C9B File Offset: 0x00010E9B
		public string LocalizedTypeLine
		{
			get
			{
				return SpireLoc.ItemFormat("ui.format.type_line", "{0} {1}", new object[]
				{
					this.LocalizedRarity,
					this.LocalizedItemType
				});
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00012CC4 File Offset: 0x00010EC4
		[Nullable(2)]
		public string LocalizedPrimaryStat
		{
			[NullableContext(2)]
			get
			{
				if (!this.IsIdentified && this.IsRarePlus)
				{
					return this.ObscuredPrimary();
				}
				RpgStatMod rpgStatMod = RpgStatMod.Parse(this.Primary);
				if (rpgStatMod == null)
				{
					return null;
				}
				return rpgStatMod.FormatPrimary();
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00012CF4 File Offset: 0x00010EF4
		public IEnumerable<string> LocalizedDetails()
		{
			if (this.IsIdentified || !this.IsRarePlus)
			{
				IEnumerable<string> details = this.Details;
				Func<string, RpgStatMod> selector;
				if ((selector = RpgItemData.<>O.<0>__Parse) == null)
				{
					selector = (RpgItemData.<>O.<0>__Parse = new Func<string, RpgStatMod>(RpgStatMod.Parse));
				}
				return from x in details.Select(selector)
				where x != null
				select x.FormatDetail();
			}
			return Array.Empty<string>();
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00012D87 File Offset: 0x00010F87
		public IEnumerable<string> LocalizedAffixes()
		{
			RpgItemData.<LocalizedAffixes>d__114 <LocalizedAffixes>d__ = new RpgItemData.<LocalizedAffixes>d__114(-2);
			<LocalizedAffixes>d__.<>4__this = this;
			return <LocalizedAffixes>d__;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00012D97 File Offset: 0x00010F97
		public IEnumerable<string> ObscuredAffixLines()
		{
			RpgItemData.<ObscuredAffixLines>d__115 <ObscuredAffixLines>d__ = new RpgItemData.<ObscuredAffixLines>d__115(-2);
			<ObscuredAffixLines>d__.<>4__this = this;
			return <ObscuredAffixLines>d__;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00012DA8 File Offset: 0x00010FA8
		[NullableContext(2)]
		private string ObscuredPrimary()
		{
			RpgStatMod rpgStatMod = RpgStatMod.Parse(this.Primary);
			string text = (rpgStatMod != null) ? rpgStatMod.FormatPrimary() : null;
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			string real = text;
			string seed;
			if ((seed = this.InstanceId) == null)
			{
				seed = (this.Id ?? "");
			}
			return RpgTextObfuscate.Line(real, seed);
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00012DF6 File Offset: 0x00010FF6
		[Nullable(2)]
		public string LocalizedLegendaryText
		{
			[NullableContext(2)]
			get
			{
				if (!this.HasLegendaryText || !this.IsIdentified)
				{
					return null;
				}
				return SpireLoc.Item(this.Id + ".legendaryText", "");
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00012E24 File Offset: 0x00011024
		public string LocalizedItemPower
		{
			get
			{
				return SpireLoc.ItemFormat("ui.item_power", "{0} Item Power", new object[]
				{
					this.ItemPower
				});
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00012E49 File Offset: 0x00011049
		public string LocalizedRequireLevel
		{
			get
			{
				return SpireLoc.ItemFormat("ui.require_level", "Requires Level {0}", new object[]
				{
					this.RequireLevel
				});
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00012E70 File Offset: 0x00011070
		public RpgItemData Clone()
		{
			return new RpgItemData
			{
				InstanceId = this.InstanceId,
				Id = this.Id,
				Rarity = this.Rarity,
				ItemPower = this.ItemPower,
				RequireLevel = this.RequireLevel,
				IconLetter = this.IconLetter,
				IconPath = this.IconPath,
				EquipSlot = this.EquipSlot,
				Primary = this.Primary,
				Details = this.Details.ToList<string>(),
				Ability = this.Ability.ToList<string>(),
				HiddenAbility = this.HiddenAbility.ToList<string>(),
				Curses = this.Curses.ToList<string>(),
				HasLegendaryText = this.HasLegendaryText,
				RequireCharacter = this.RequireCharacter,
				RequireSlots = this.RequireSlots.ToList<string>(),
				RequireItemIds = this.RequireItemIds.ToList<string>(),
				RequireItemIdPrefix = this.RequireItemIdPrefix,
				RequireAllSlotsFilled = this.RequireAllSlotsFilled,
				RequireGlovesMinRarity = this.RequireGlovesMinRarity,
				IntensifyLevel = this.IntensifyLevel,
				IsIdentified = this.IsIdentified,
				IsBound = this.IsBound,
				IsScroll = this.IsScroll
			};
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00012FC0 File Offset: 0x000111C0
		public void CopyRequirementsFrom(RpgItemTemplate template)
		{
			this.RequireCharacter = template.RequireCharacter;
			List<string> requireSlots = template.RequireSlots;
			this.RequireSlots = (((requireSlots != null) ? requireSlots.ToList<string>() : null) ?? new List<string>());
			List<string> requireItemIds = template.RequireItemIds;
			this.RequireItemIds = (((requireItemIds != null) ? requireItemIds.ToList<string>() : null) ?? new List<string>());
			this.RequireItemIdPrefix = template.RequireItemIdPrefix;
			this.RequireAllSlotsFilled = template.RequireAllSlotsFilled;
			this.RequireGlovesMinRarity = template.RequireGlovesMinRarity;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00013040 File Offset: 0x00011240
		public void SyncVisualFromTemplate()
		{
			if (this.IsScroll)
			{
				if (string.IsNullOrWhiteSpace(this.IconPath))
				{
					this.IconPath = "item/other/scroll.png";
				}
				if (string.IsNullOrWhiteSpace(this.IconLetter))
				{
					this.IconLetter = "S";
				}
				return;
			}
			RpgItemTemplate rpgItemTemplate = RpgItemCatalog.Get(this.Id);
			if (rpgItemTemplate != null)
			{
				if (!string.IsNullOrWhiteSpace(rpgItemTemplate.IconPath))
				{
					this.IconPath = rpgItemTemplate.IconPath;
				}
				if (!string.IsNullOrWhiteSpace(rpgItemTemplate.IconLetter))
				{
					this.IconLetter = rpgItemTemplate.IconLetter;
				}
			}
			this.StripBannedAffixes();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000130D0 File Offset: 0x000112D0
		public void StripBannedAffixes()
		{
			List<string> ability = this.Ability;
			Predicate<string> match;
			if ((match = RpgItemData.<>O.<1>__IsBannedAffixLine) == null)
			{
				match = (RpgItemData.<>O.<1>__IsBannedAffixLine = new Predicate<string>(RpgItemData.IsBannedAffixLine));
			}
			ability.RemoveAll(match);
			List<string> hiddenAbility = this.HiddenAbility;
			Predicate<string> match2;
			if ((match2 = RpgItemData.<>O.<1>__IsBannedAffixLine) == null)
			{
				match2 = (RpgItemData.<>O.<1>__IsBannedAffixLine = new Predicate<string>(RpgItemData.IsBannedAffixLine));
			}
			hiddenAbility.RemoveAll(match2);
			List<string> details = this.Details;
			Predicate<string> match3;
			if ((match3 = RpgItemData.<>O.<1>__IsBannedAffixLine) == null)
			{
				match3 = (RpgItemData.<>O.<1>__IsBannedAffixLine = new Predicate<string>(RpgItemData.IsBannedAffixLine));
			}
			details.RemoveAll(match3);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00013152 File Offset: 0x00011352
		public void Identify()
		{
			if (this.IsIdentified)
			{
				return;
			}
			if (this.HiddenAbility.Count > 0)
			{
				this.Ability = this.HiddenAbility.ToList<string>();
				this.HiddenAbility.Clear();
			}
			this.IsIdentified = true;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0001318E File Offset: 0x0001138E
		public void ClearCurses()
		{
			this.Curses.Clear();
			this.IsBound = false;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000131A4 File Offset: 0x000113A4
		[NullableContext(2)]
		private static bool IsBannedAffixLine(string line)
		{
			RpgStatMod rpgStatMod = RpgStatMod.Parse(line);
			return rpgStatMod != null && RpgAffixCatalog.IsBannedId(rpgStatMod.Id);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000131C8 File Offset: 0x000113C8
		public RpgItemData()
		{
		}

		// Token: 0x040000C8 RID: 200
		[CompilerGenerated]
		private string <InstanceId>k__BackingField;

		// Token: 0x040000C9 RID: 201
		[CompilerGenerated]
		private string <Id>k__BackingField;

		// Token: 0x040000CA RID: 202
		[CompilerGenerated]
		private RpgItemRarity <Rarity>k__BackingField;

		// Token: 0x040000CB RID: 203
		[CompilerGenerated]
		private int <ItemPower>k__BackingField;

		// Token: 0x040000CC RID: 204
		[CompilerGenerated]
		private int <RequireLevel>k__BackingField;

		// Token: 0x040000CD RID: 205
		[CompilerGenerated]
		private string <IconLetter>k__BackingField;

		// Token: 0x040000CE RID: 206
		[Nullable(2)]
		[CompilerGenerated]
		private string <IconPath>k__BackingField;

		// Token: 0x040000CF RID: 207
		[CompilerGenerated]
		private RpgEquipSlot <EquipSlot>k__BackingField;

		// Token: 0x040000D0 RID: 208
		[Nullable(2)]
		[CompilerGenerated]
		private string <Primary>k__BackingField;

		// Token: 0x040000D1 RID: 209
		[CompilerGenerated]
		private List<string> <Details>k__BackingField;

		// Token: 0x040000D2 RID: 210
		[CompilerGenerated]
		private List<string> <Ability>k__BackingField;

		// Token: 0x040000D3 RID: 211
		[CompilerGenerated]
		private List<string> <HiddenAbility>k__BackingField;

		// Token: 0x040000D4 RID: 212
		[CompilerGenerated]
		private List<string> <Curses>k__BackingField;

		// Token: 0x040000D5 RID: 213
		[CompilerGenerated]
		private bool <HasLegendaryText>k__BackingField;

		// Token: 0x040000D6 RID: 214
		[Nullable(2)]
		[CompilerGenerated]
		private string <RequireCharacter>k__BackingField;

		// Token: 0x040000D7 RID: 215
		[CompilerGenerated]
		private List<string> <RequireSlots>k__BackingField;

		// Token: 0x040000D8 RID: 216
		[CompilerGenerated]
		private List<string> <RequireItemIds>k__BackingField;

		// Token: 0x040000D9 RID: 217
		[Nullable(2)]
		[CompilerGenerated]
		private string <RequireItemIdPrefix>k__BackingField;

		// Token: 0x040000DA RID: 218
		[CompilerGenerated]
		private bool <RequireAllSlotsFilled>k__BackingField;

		// Token: 0x040000DB RID: 219
		[Nullable(2)]
		[CompilerGenerated]
		private string <RequireGlovesMinRarity>k__BackingField;

		// Token: 0x040000DC RID: 220
		[CompilerGenerated]
		private int <IntensifyLevel>k__BackingField;

		// Token: 0x040000DD RID: 221
		[CompilerGenerated]
		private bool <IsIdentified>k__BackingField;

		// Token: 0x040000DE RID: 222
		[CompilerGenerated]
		private bool <IsBound>k__BackingField;

		// Token: 0x040000DF RID: 223
		[CompilerGenerated]
		private bool <IsScroll>k__BackingField;

		// Token: 0x020000F3 RID: 243
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400037B RID: 891
			[Nullable(0)]
			public static Func<string, RpgStatMod> <0>__Parse;

			// Token: 0x0400037C RID: 892
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Predicate<string> <1>__IsBannedAffixLine;
		}

		// Token: 0x020000F4 RID: 244
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000707 RID: 1799 RVA: 0x00025B5E File Offset: 0x00023D5E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000708 RID: 1800 RVA: 0x00025B6A File Offset: 0x00023D6A
			public <>c()
			{
			}

			// Token: 0x06000709 RID: 1801 RVA: 0x00025B72 File Offset: 0x00023D72
			[NullableContext(0)]
			internal bool <LocalizedDetails>b__113_0(RpgStatMod x)
			{
				return x != null;
			}

			// Token: 0x0600070A RID: 1802 RVA: 0x00025B78 File Offset: 0x00023D78
			[NullableContext(0)]
			internal string <LocalizedDetails>b__113_1(RpgStatMod x)
			{
				return x.FormatDetail();
			}

			// Token: 0x0600070B RID: 1803 RVA: 0x00025B80 File Offset: 0x00023D80
			[NullableContext(0)]
			internal bool <LocalizedAffixes>b__114_0(RpgStatMod x)
			{
				return x != null;
			}

			// Token: 0x0600070C RID: 1804 RVA: 0x00025B86 File Offset: 0x00023D86
			[NullableContext(0)]
			internal string <LocalizedAffixes>b__114_1(RpgStatMod x)
			{
				return x.FormatAffix();
			}

			// Token: 0x0400037D RID: 893
			[Nullable(0)]
			public static readonly RpgItemData.<>c <>9 = new RpgItemData.<>c();

			// Token: 0x0400037E RID: 894
			[Nullable(0)]
			public static Func<RpgStatMod, bool> <>9__113_0;

			// Token: 0x0400037F RID: 895
			[Nullable(0)]
			public static Func<RpgStatMod, string> <>9__113_1;

			// Token: 0x04000380 RID: 896
			[Nullable(0)]
			public static Func<RpgStatMod, bool> <>9__114_0;

			// Token: 0x04000381 RID: 897
			[Nullable(0)]
			public static Func<RpgStatMod, string> <>9__114_1;
		}

		// Token: 0x020000F5 RID: 245
		[CompilerGenerated]
		private sealed class <EffectiveAbilityLines>d__100 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IEnumerator, IDisposable
		{
			// Token: 0x0600070D RID: 1805 RVA: 0x00025B8E File Offset: 0x00023D8E
			[DebuggerHidden]
			public <EffectiveAbilityLines>d__100(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600070E RID: 1806 RVA: 0x00025BA8 File Offset: 0x00023DA8
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				switch (this.<>1__state)
				{
				case -4:
				case 2:
					break;
				case -3:
				case 1:
					try
					{
						goto IL_42;
					}
					finally
					{
						this.<>m__Finally1();
					}
					break;
				case -2:
				case -1:
				case 0:
					goto IL_42;
				default:
					goto IL_42;
				}
				try
				{
				}
				finally
				{
					this.<>m__Finally2();
				}
				IL_42:
				this.<>7__wrap1 = default(List<string>.Enumerator);
				this.<>1__state = -2;
			}

			// Token: 0x0600070F RID: 1807 RVA: 0x00025C28 File Offset: 0x00023E28
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					RpgItemData rpgItemData = this;
					switch (num)
					{
					case 0:
						this.<>1__state = -1;
						if (!rpgItemData.IsIdentified && rpgItemData.HiddenAbility.Count != 0)
						{
							this.<>7__wrap1 = rpgItemData.HiddenAbility.GetEnumerator();
							this.<>1__state = -4;
							goto IL_ED;
						}
						this.<>7__wrap1 = rpgItemData.Ability.GetEnumerator();
						this.<>1__state = -3;
						break;
					case 1:
						this.<>1__state = -3;
						break;
					case 2:
						this.<>1__state = -4;
						goto IL_ED;
					default:
						return false;
					}
					if (!this.<>7__wrap1.MoveNext())
					{
						this.<>m__Finally1();
						this.<>7__wrap1 = default(List<string>.Enumerator);
						return false;
					}
					string text = this.<>7__wrap1.Current;
					this.<>2__current = text;
					this.<>1__state = 1;
					return true;
					IL_ED:
					if (!this.<>7__wrap1.MoveNext())
					{
						this.<>m__Finally2();
						this.<>7__wrap1 = default(List<string>.Enumerator);
						result = false;
					}
					else
					{
						string text2 = this.<>7__wrap1.Current;
						this.<>2__current = text2;
						this.<>1__state = 2;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06000710 RID: 1808 RVA: 0x00025D6C File Offset: 0x00023F6C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				((IDisposable)this.<>7__wrap1).Dispose();
			}

			// Token: 0x06000711 RID: 1809 RVA: 0x00025D86 File Offset: 0x00023F86
			private void <>m__Finally2()
			{
				this.<>1__state = -1;
				((IDisposable)this.<>7__wrap1).Dispose();
			}

			// Token: 0x1700018E RID: 398
			// (get) Token: 0x06000712 RID: 1810 RVA: 0x00025DA0 File Offset: 0x00023FA0
			string IEnumerator<string>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000713 RID: 1811 RVA: 0x00025DA8 File Offset: 0x00023FA8
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700018F RID: 399
			// (get) Token: 0x06000714 RID: 1812 RVA: 0x00025DAF File Offset: 0x00023FAF
			object IEnumerator.Current
			{
				[DebuggerHidden]
				[return: Nullable(0)]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000715 RID: 1813 RVA: 0x00025DB8 File Offset: 0x00023FB8
			[DebuggerHidden]
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				RpgItemData.<EffectiveAbilityLines>d__100 <EffectiveAbilityLines>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<EffectiveAbilityLines>d__ = this;
				}
				else
				{
					<EffectiveAbilityLines>d__ = new RpgItemData.<EffectiveAbilityLines>d__100(0);
					<EffectiveAbilityLines>d__.<>4__this = this;
				}
				return <EffectiveAbilityLines>d__;
			}

			// Token: 0x06000716 RID: 1814 RVA: 0x00025DFB File Offset: 0x00023FFB
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();
			}

			// Token: 0x04000382 RID: 898
			private int <>1__state;

			// Token: 0x04000383 RID: 899
			private string <>2__current;

			// Token: 0x04000384 RID: 900
			private int <>l__initialThreadId;

			// Token: 0x04000385 RID: 901
			[Nullable(0)]
			public RpgItemData <>4__this;

			// Token: 0x04000386 RID: 902
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private List<string>.Enumerator <>7__wrap1;
		}

		// Token: 0x020000F6 RID: 246
		[CompilerGenerated]
		private sealed class <LocalizedAffixes>d__114 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IEnumerator, IDisposable
		{
			// Token: 0x06000717 RID: 1815 RVA: 0x00025E03 File Offset: 0x00024003
			[DebuggerHidden]
			public <LocalizedAffixes>d__114(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000718 RID: 1816 RVA: 0x00025E20 File Offset: 0x00024020
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				switch (this.<>1__state)
				{
				case -5:
				case 3:
					goto IL_4A;
				case -4:
				case 2:
					break;
				case -3:
				case 1:
					try
					{
						goto IL_54;
					}
					finally
					{
						this.<>m__Finally1();
					}
					break;
				case -2:
				case -1:
				case 0:
					goto IL_54;
				default:
					goto IL_54;
				}
				try
				{
					goto IL_54;
				}
				finally
				{
					this.<>m__Finally2();
				}
				IL_4A:
				try
				{
				}
				finally
				{
					this.<>m__Finally3();
				}
				IL_54:
				this.<>7__wrap1 = null;
				this.<>7__wrap2 = default(List<string>.Enumerator);
				this.<>1__state = -2;
			}

			// Token: 0x06000719 RID: 1817 RVA: 0x00025EC4 File Offset: 0x000240C4
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					RpgItemData rpgItemData = this;
					switch (num)
					{
					case 0:
						this.<>1__state = -1;
						if (rpgItemData.IsIdentified)
						{
							IEnumerable<string> ability = rpgItemData.Ability;
							Func<string, RpgStatMod> selector;
							if ((selector = RpgItemData.<>O.<0>__Parse) == null)
							{
								selector = (RpgItemData.<>O.<0>__Parse = new Func<string, RpgStatMod>(RpgStatMod.Parse));
							}
							this.<>7__wrap1 = ability.Select(selector).Where(new Func<RpgStatMod, bool>(RpgItemData.<>c.<>9.<LocalizedAffixes>b__114_0)).Select(new Func<RpgStatMod, string>(RpgItemData.<>c.<>9.<LocalizedAffixes>b__114_1)).GetEnumerator();
							this.<>1__state = -4;
							goto IL_14D;
						}
						this.<>7__wrap1 = rpgItemData.ObscuredAffixLines().GetEnumerator();
						this.<>1__state = -3;
						break;
					case 1:
						this.<>1__state = -3;
						break;
					case 2:
						this.<>1__state = -4;
						goto IL_14D;
					case 3:
						this.<>1__state = -5;
						goto IL_1D3;
					default:
						return false;
					}
					if (!this.<>7__wrap1.MoveNext())
					{
						this.<>m__Finally1();
						this.<>7__wrap1 = null;
						return false;
					}
					string text = this.<>7__wrap1.Current;
					this.<>2__current = text;
					this.<>1__state = 1;
					return true;
					IL_14D:
					if (this.<>7__wrap1.MoveNext())
					{
						string text2 = this.<>7__wrap1.Current;
						this.<>2__current = text2;
						this.<>1__state = 2;
						return true;
					}
					this.<>m__Finally2();
					this.<>7__wrap1 = null;
					this.<>7__wrap2 = rpgItemData.Curses.GetEnumerator();
					this.<>1__state = -5;
					IL_1D3:
					if (!this.<>7__wrap2.MoveNext())
					{
						this.<>m__Finally3();
						this.<>7__wrap2 = default(List<string>.Enumerator);
						result = false;
					}
					else
					{
						string text3 = this.<>7__wrap2.Current;
						this.<>2__current = SpireLoc.ItemFormat("ui.curse_line", "Curse: {0}", new object[]
						{
							SpireLoc.Item("curse." + text3, text3)
						});
						this.<>1__state = 3;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x0600071A RID: 1818 RVA: 0x000260EC File Offset: 0x000242EC
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (this.<>7__wrap1 != null)
				{
					this.<>7__wrap1.Dispose();
				}
			}

			// Token: 0x0600071B RID: 1819 RVA: 0x00026108 File Offset: 0x00024308
			private void <>m__Finally2()
			{
				this.<>1__state = -1;
				if (this.<>7__wrap1 != null)
				{
					this.<>7__wrap1.Dispose();
				}
			}

			// Token: 0x0600071C RID: 1820 RVA: 0x00026124 File Offset: 0x00024324
			private void <>m__Finally3()
			{
				this.<>1__state = -1;
				((IDisposable)this.<>7__wrap2).Dispose();
			}

			// Token: 0x17000190 RID: 400
			// (get) Token: 0x0600071D RID: 1821 RVA: 0x0002613E File Offset: 0x0002433E
			string IEnumerator<string>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600071E RID: 1822 RVA: 0x00026146 File Offset: 0x00024346
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000191 RID: 401
			// (get) Token: 0x0600071F RID: 1823 RVA: 0x0002614D File Offset: 0x0002434D
			object IEnumerator.Current
			{
				[DebuggerHidden]
				[return: Nullable(0)]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000720 RID: 1824 RVA: 0x00026158 File Offset: 0x00024358
			[DebuggerHidden]
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				RpgItemData.<LocalizedAffixes>d__114 <LocalizedAffixes>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<LocalizedAffixes>d__ = this;
				}
				else
				{
					<LocalizedAffixes>d__ = new RpgItemData.<LocalizedAffixes>d__114(0);
					<LocalizedAffixes>d__.<>4__this = this;
				}
				return <LocalizedAffixes>d__;
			}

			// Token: 0x06000721 RID: 1825 RVA: 0x0002619B File Offset: 0x0002439B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();
			}

			// Token: 0x04000387 RID: 903
			private int <>1__state;

			// Token: 0x04000388 RID: 904
			private string <>2__current;

			// Token: 0x04000389 RID: 905
			private int <>l__initialThreadId;

			// Token: 0x0400038A RID: 906
			[Nullable(0)]
			public RpgItemData <>4__this;

			// Token: 0x0400038B RID: 907
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private IEnumerator<string> <>7__wrap1;

			// Token: 0x0400038C RID: 908
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private List<string>.Enumerator <>7__wrap2;
		}

		// Token: 0x020000F7 RID: 247
		[CompilerGenerated]
		private sealed class <ObscuredAffixLines>d__115 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IEnumerator, IDisposable
		{
			// Token: 0x06000722 RID: 1826 RVA: 0x000261A3 File Offset: 0x000243A3
			[DebuggerHidden]
			public <ObscuredAffixLines>d__115(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000723 RID: 1827 RVA: 0x000261C0 File Offset: 0x000243C0
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				switch (this.<>1__state)
				{
				case -4:
				case 2:
					break;
				case -3:
				case 1:
					try
					{
						goto IL_42;
					}
					finally
					{
						this.<>m__Finally1();
					}
					break;
				case -2:
				case -1:
				case 0:
					goto IL_42;
				default:
					goto IL_42;
				}
				try
				{
				}
				finally
				{
					this.<>m__Finally2();
				}
				IL_42:
				this.<seed>5__2 = null;
				this.<>7__wrap2 = null;
				this.<>7__wrap3 = default(List<string>.Enumerator);
				this.<>1__state = -2;
			}

			// Token: 0x06000724 RID: 1828 RVA: 0x00026250 File Offset: 0x00024450
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					RpgItemData rpgItemData = this;
					switch (num)
					{
					case 0:
					{
						this.<>1__state = -1;
						string text;
						if ((text = rpgItemData.InstanceId) == null)
						{
							text = (rpgItemData.Id ?? "");
						}
						this.<seed>5__2 = text;
						this.<>7__wrap2 = rpgItemData.EffectiveAbilityLines().GetEnumerator();
						this.<>1__state = -3;
						break;
					}
					case 1:
						this.<>1__state = -3;
						break;
					case 2:
						this.<>1__state = -4;
						goto IL_155;
					default:
						return false;
					}
					while (this.<>7__wrap2.MoveNext())
					{
						RpgStatMod rpgStatMod = RpgStatMod.Parse(this.<>7__wrap2.Current);
						if (rpgStatMod != null)
						{
							this.<>2__current = RpgTextObfuscate.Line(rpgStatMod.FormatAffix(), this.<seed>5__2);
							this.<>1__state = 1;
							return true;
						}
					}
					this.<>m__Finally1();
					this.<>7__wrap2 = null;
					if (!rpgItemData.HasCurse)
					{
						goto IL_174;
					}
					this.<>7__wrap3 = rpgItemData.Curses.GetEnumerator();
					this.<>1__state = -4;
					IL_155:
					if (this.<>7__wrap3.MoveNext())
					{
						string text2 = this.<>7__wrap3.Current;
						string real = SpireLoc.ItemFormat("ui.curse_line", "Curse: {0}", new object[]
						{
							SpireLoc.Item("curse." + text2, text2)
						});
						this.<>2__current = RpgTextObfuscate.Line(real, this.<seed>5__2 + ":" + text2);
						this.<>1__state = 2;
						return true;
					}
					this.<>m__Finally2();
					this.<>7__wrap3 = default(List<string>.Enumerator);
					IL_174:
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06000725 RID: 1829 RVA: 0x000263FC File Offset: 0x000245FC
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (this.<>7__wrap2 != null)
				{
					this.<>7__wrap2.Dispose();
				}
			}

			// Token: 0x06000726 RID: 1830 RVA: 0x00026418 File Offset: 0x00024618
			private void <>m__Finally2()
			{
				this.<>1__state = -1;
				((IDisposable)this.<>7__wrap3).Dispose();
			}

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x06000727 RID: 1831 RVA: 0x00026432 File Offset: 0x00024632
			string IEnumerator<string>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000728 RID: 1832 RVA: 0x0002643A File Offset: 0x0002463A
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000193 RID: 403
			// (get) Token: 0x06000729 RID: 1833 RVA: 0x00026441 File Offset: 0x00024641
			object IEnumerator.Current
			{
				[DebuggerHidden]
				[return: Nullable(0)]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600072A RID: 1834 RVA: 0x0002644C File Offset: 0x0002464C
			[DebuggerHidden]
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				RpgItemData.<ObscuredAffixLines>d__115 <ObscuredAffixLines>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<ObscuredAffixLines>d__ = this;
				}
				else
				{
					<ObscuredAffixLines>d__ = new RpgItemData.<ObscuredAffixLines>d__115(0);
					<ObscuredAffixLines>d__.<>4__this = this;
				}
				return <ObscuredAffixLines>d__;
			}

			// Token: 0x0600072B RID: 1835 RVA: 0x0002648F File Offset: 0x0002468F
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();
			}

			// Token: 0x0400038D RID: 909
			private int <>1__state;

			// Token: 0x0400038E RID: 910
			private string <>2__current;

			// Token: 0x0400038F RID: 911
			private int <>l__initialThreadId;

			// Token: 0x04000390 RID: 912
			[Nullable(0)]
			public RpgItemData <>4__this;

			// Token: 0x04000391 RID: 913
			[Nullable(0)]
			private string <seed>5__2;

			// Token: 0x04000392 RID: 914
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private IEnumerator<string> <>7__wrap2;

			// Token: 0x04000393 RID: 915
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private List<string>.Enumerator <>7__wrap3;
		}
	}
}
