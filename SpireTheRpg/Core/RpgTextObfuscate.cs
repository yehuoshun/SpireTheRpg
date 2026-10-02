using System;
using System.Runtime.CompilerServices;

namespace SpireTheRpg.Core
{
	// Token: 0x02000065 RID: 101
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgTextObfuscate
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x0001A530 File Offset: 0x00018730
		public static string Line(string real, string seed)
		{
			if (string.IsNullOrWhiteSpace(real))
			{
				return "???";
			}
			Random random = new Random(RpgTextObfuscate.Hash(seed, real));
			char[] array = real.ToCharArray();
			int num = (array.Length <= 2) ? 1 : Math.Clamp(array.Length / 4, 1, 2);
			int num2 = Math.Max(0, array.Length - num);
			for (int i = 0; i < num2; i++)
			{
				char c = array[i];
				if (!char.IsWhiteSpace(c))
				{
					bool flag = char.IsDigit(c);
					if (!flag)
					{
						if (c == '%')
						{
							goto IL_87;
						}
						bool flag2;
						switch (c)
						{
						case '+':
						case ',':
						case '-':
						case '.':
							goto IL_87;
						default:
							flag2 = false;
							break;
						}
						IL_8F:
						flag = flag2;
						goto IL_93;
						IL_87:
						flag2 = true;
						goto IL_8F;
					}
					IL_93:
					if (flag)
					{
						array[i] = '?';
					}
					else
					{
						array[i] = RpgTextObfuscate.Glyphs[random.Next(RpgTextObfuscate.Glyphs.Length)];
					}
				}
			}
			string text = new string(array).Trim();
			if (random.Next(3) != 0)
			{
				text += "???";
			}
			return text;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001A624 File Offset: 0x00018824
		private static int Hash(string seed, string real)
		{
			int num = 17;
			foreach (char c in seed ?? "")
			{
				num = num * 31 + (int)c;
			}
			foreach (char c2 in real)
			{
				num = num * 31 + (int)c2;
			}
			if (num != -2147483648)
			{
				return num;
			}
			return 1;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001A68E File Offset: 0x0001888E
		// Note: this type is marked as 'beforefieldinit'.
		static RpgTextObfuscate()
		{
		}

		// Token: 0x0400016A RID: 362
		private static readonly char[] Glyphs = "ç”±æœˆè|å¥½ä¤©éÔÂòaoÃÑ§Ï°ìó¸Šþø×¿¡¢£¤¦¨«»¬®©".ToCharArray();
	}
}
