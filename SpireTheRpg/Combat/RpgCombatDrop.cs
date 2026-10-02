using System;
using System.Runtime.CompilerServices;

namespace SpireTheRpg.Combat
{
	// Token: 0x0200006C RID: 108
	internal static class RpgCombatDrop
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0001B160 File Offset: 0x00019360
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0001B167 File Offset: 0x00019367
		public static int LastXpGained
		{
			[CompilerGenerated]
			get
			{
				return RpgCombatDrop.<LastXpGained>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				RpgCombatDrop.<LastXpGained>k__BackingField = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0001B16F File Offset: 0x0001936F
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0001B176 File Offset: 0x00019376
		public static int LastLevelsGained
		{
			[CompilerGenerated]
			get
			{
				return RpgCombatDrop.<LastLevelsGained>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				RpgCombatDrop.<LastLevelsGained>k__BackingField = value;
			}
		}

		// Token: 0x0400019A RID: 410
		[CompilerGenerated]
		private static int <LastXpGained>k__BackingField;

		// Token: 0x0400019B RID: 411
		[CompilerGenerated]
		private static int <LastLevelsGained>k__BackingField;
	}
}
