using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Platform;
using MegaCrit.Sts2.Core.Saves;

namespace SpireTheRpg.Core
{
	// Token: 0x02000051 RID: 81
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class RpgPlayerProfile
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00016112 File Offset: 0x00014312
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0001611A File Offset: 0x0001431A
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		} = "Hero";

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00016123 File Offset: 0x00014323
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0001612B File Offset: 0x0001432B
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

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00016134 File Offset: 0x00014334
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0001613C File Offset: 0x0001433C
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

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00016145 File Offset: 0x00014345
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0001614D File Offset: 0x0001434D
		public int XpToNext
		{
			[CompilerGenerated]
			get
			{
				return this.<XpToNext>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<XpToNext>k__BackingField = value;
			}
		} = 200;

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00016156 File Offset: 0x00014356
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0001615E File Offset: 0x0001435E
		public int Strength
		{
			[CompilerGenerated]
			get
			{
				return this.<Strength>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Strength>k__BackingField = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00016167 File Offset: 0x00014367
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0001616F File Offset: 0x0001436F
		public int Dexterity
		{
			[CompilerGenerated]
			get
			{
				return this.<Dexterity>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Dexterity>k__BackingField = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00016178 File Offset: 0x00014378
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00016180 File Offset: 0x00014380
		public int Intelligence
		{
			[CompilerGenerated]
			get
			{
				return this.<Intelligence>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Intelligence>k__BackingField = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00016189 File Offset: 0x00014389
		// (set) Token: 0x06000337 RID: 823 RVA: 0x00016191 File Offset: 0x00014391
		public int Vitality
		{
			[CompilerGenerated]
			get
			{
				return this.<Vitality>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Vitality>k__BackingField = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0001619A File Offset: 0x0001439A
		// (set) Token: 0x06000339 RID: 825 RVA: 0x000161A2 File Offset: 0x000143A2
		public IReadOnlyList<RpgAbilityInfo> Abilities
		{
			[CompilerGenerated]
			get
			{
				return this.<Abilities>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Abilities>k__BackingField = value;
			}
		} = Array.Empty<RpgAbilityInfo>();

		// Token: 0x0600033A RID: 826 RVA: 0x000161AC File Offset: 0x000143AC
		public static RpgPlayerProfile Current()
		{
			RpgProgress.Ensure();
			int num = RpgProgress.StatBonus();
			return new RpgPlayerProfile
			{
				Name = RpgPlayerProfile.ResolvePlayerName(),
				Level = RpgProgress.Level,
				Xp = RpgProgress.Xp,
				XpToNext = RpgProgress.XpToNext,
				Strength = 10 + num,
				Dexterity = 10 + num,
				Intelligence = 10 + num,
				Vitality = 12 + num,
				Abilities = new <>z__ReadOnlyArray<RpgAbilityInfo>(new RpgAbilityInfo[]
				{
					new RpgAbilityInfo
					{
						Id = "strike",
						Title = SpireLoc.T("SPIRE_ABILITY.strike.title", "Strike"),
						Description = SpireLoc.T("SPIRE_ABILITY.strike.desc", "Deal damage based on Strength")
					},
					new RpgAbilityInfo
					{
						Id = "guard",
						Title = SpireLoc.T("SPIRE_ABILITY.guard.title", "Guard"),
						Description = SpireLoc.T("SPIRE_ABILITY.guard.desc", "Gain Block based on Vitality")
					},
					new RpgAbilityInfo
					{
						Id = "brew",
						Title = SpireLoc.T("SPIRE_ABILITY.brew.title", "Brew"),
						Description = SpireLoc.T("SPIRE_ABILITY.brew.desc", "Restore resources with alchemy")
					}
				})
			};
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000162E8 File Offset: 0x000144E8
		private static string ResolvePlayerName()
		{
			try
			{
				ulong localPlayerId = PlatformUtil.GetLocalPlayerId(1);
				string playerName = PlatformUtil.GetPlayerName(1, localPlayerId);
				if (!string.IsNullOrWhiteSpace(playerName))
				{
					return playerName;
				}
			}
			catch
			{
			}
			try
			{
				SaveManager instance = SaveManager.Instance;
				int num = (instance != null) ? instance.CurrentProfileId : 0;
				if (num > 0)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Profile ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(num);
					return defaultInterpolatedStringHandler.ToStringAndClear();
				}
			}
			catch
			{
			}
			return SpireLoc.T("SPIRE_PLAYER.default_name", "Hero");
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00016388 File Offset: 0x00014588
		public RpgPlayerProfile()
		{
		}

		// Token: 0x04000101 RID: 257
		[CompilerGenerated]
		private readonly string <Name>k__BackingField;

		// Token: 0x04000102 RID: 258
		[CompilerGenerated]
		private readonly int <Level>k__BackingField;

		// Token: 0x04000103 RID: 259
		[CompilerGenerated]
		private readonly int <Xp>k__BackingField;

		// Token: 0x04000104 RID: 260
		[CompilerGenerated]
		private readonly int <XpToNext>k__BackingField;

		// Token: 0x04000105 RID: 261
		[CompilerGenerated]
		private readonly int <Strength>k__BackingField;

		// Token: 0x04000106 RID: 262
		[CompilerGenerated]
		private readonly int <Dexterity>k__BackingField;

		// Token: 0x04000107 RID: 263
		[CompilerGenerated]
		private readonly int <Intelligence>k__BackingField;

		// Token: 0x04000108 RID: 264
		[CompilerGenerated]
		private readonly int <Vitality>k__BackingField;

		// Token: 0x04000109 RID: 265
		[CompilerGenerated]
		private readonly IReadOnlyList<RpgAbilityInfo> <Abilities>k__BackingField;
	}
}
