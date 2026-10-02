using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace SpireTheRpg.Core
{
	// Token: 0x0200004F RID: 79
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgPaths
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00016094 File Offset: 0x00014294
		public static string AppDataRoot
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SlayTheSpire2");
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600031F RID: 799 RVA: 0x000160A7 File Offset: 0x000142A7
		public static string ModDataRoot
		{
			get
			{
				string text = Path.Combine(RpgPaths.AppDataRoot, "SpireTheRpg");
				Directory.CreateDirectory(text);
				return text;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000320 RID: 800 RVA: 0x000160BF File Offset: 0x000142BF
		public static string SaveDir
		{
			get
			{
				string text = Path.Combine(RpgPaths.ModDataRoot, "save");
				Directory.CreateDirectory(text);
				return text;
			}
		}
	}
}
