using System;
using System.Runtime.CompilerServices;

namespace SpireTheRpg.Core
{
	// Token: 0x02000036 RID: 54
	internal static class RpgAccessoryRunState
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000106CC File Offset: 0x0000E8CC
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x000106D3 File Offset: 0x0000E8D3
		public static bool DoubleVisionUsed
		{
			[CompilerGenerated]
			get
			{
				return RpgAccessoryRunState.<DoubleVisionUsed>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				RpgAccessoryRunState.<DoubleVisionUsed>k__BackingField = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000106DB File Offset: 0x0000E8DB
		// (set) Token: 0x060001DA RID: 474 RVA: 0x000106E2 File Offset: 0x0000E8E2
		public static int CombatWinsThisRun
		{
			[CompilerGenerated]
			get
			{
				return RpgAccessoryRunState.<CombatWinsThisRun>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				RpgAccessoryRunState.<CombatWinsThisRun>k__BackingField = value;
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000106EA File Offset: 0x0000E8EA
		public static void ResetForNewRun()
		{
			RpgAccessoryRunState.DoubleVisionUsed = false;
			RpgAccessoryRunState.CombatWinsThisRun = 0;
			RpgTwinExistence.ClearAll();
			RpgRerollDie.ResetForNewRun();
			RpgFinalFormMirror.ResetForNewRun();
		}

		// Token: 0x0400009A RID: 154
		[CompilerGenerated]
		private static bool <DoubleVisionUsed>k__BackingField;

		// Token: 0x0400009B RID: 155
		[CompilerGenerated]
		private static int <CombatWinsThisRun>k__BackingField;
	}
}
