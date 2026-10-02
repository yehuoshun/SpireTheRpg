using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;

namespace SpireTheRpg.Core
{
	// Token: 0x02000054 RID: 84
	internal static class RpgRerollDie
	{
		// Token: 0x0600034E RID: 846 RVA: 0x000169A9 File Offset: 0x00014BA9
		public static void ResetForNewRun()
		{
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000169AB File Offset: 0x00014BAB
		[NullableContext(2)]
		public static bool CanUse(Player player)
		{
			return RpgAccessoryCombatHooks.HasActiveAccessory("reroll_once_die", player);
		}

		// Token: 0x04000111 RID: 273
		[Nullable(1)]
		public const string AccessoryId = "reroll_once_die";
	}
}
