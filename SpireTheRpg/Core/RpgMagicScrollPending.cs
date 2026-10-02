using System;

namespace SpireTheRpg.Core
{
	// Token: 0x02000061 RID: 97
	internal static class RpgMagicScrollPending
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x00019EDF File Offset: 0x000180DF
		public static void Arm()
		{
			RpgMagicScrollPending._armed = true;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00019EE7 File Offset: 0x000180E7
		public static void Clear()
		{
			RpgMagicScrollPending._armed = false;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00019EEF File Offset: 0x000180EF
		public static bool Consume()
		{
			if (!RpgMagicScrollPending._armed)
			{
				return false;
			}
			RpgMagicScrollPending._armed = false;
			return true;
		}

		// Token: 0x0400015E RID: 350
		private static bool _armed;
	}
}
