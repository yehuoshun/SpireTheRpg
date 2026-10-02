using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Godot;

namespace SpireTheRpg
{
	// Token: 0x02000002 RID: 2
	[NullableContext(1)]
	[Nullable(0)]
	internal static class SpireAssets
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static string ModDir
		{
			get
			{
				return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? ".";
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000206A File Offset: 0x0000026A
		public static string ResPath(string relativePath)
		{
			return "res://SpireTheRpg/" + relativePath.Replace('\\', '/').TrimStart('/');
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002087 File Offset: 0x00000287
		public static string ResAsset(string relativePath)
		{
			return SpireAssets.ResPath("assets/" + relativePath.Replace('\\', '/'));
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020A2 File Offset: 0x000002A2
		public static string DataRelicIconPath
		{
			get
			{
				return SpireAssets.ResAsset("relic/data_relic.png");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020AE File Offset: 0x000002AE
		public static string ItemRelicIconPath
		{
			get
			{
				return SpireAssets.ResAsset("relic/item_relic.png");
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020BA File Offset: 0x000002BA
		public static string ExpRelicIconPath
		{
			get
			{
				return SpireAssets.ResAsset("relic/exp_relic.png");
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020C6 File Offset: 0x000002C6
		[NullableContext(2)]
		public static Texture2D PlayerCardBg()
		{
			return SpireAssets.Load("ui/player_card_bg.png");
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020D2 File Offset: 0x000002D2
		[NullableContext(2)]
		public static Texture2D AbilityCardBg()
		{
			return SpireAssets.Load("ui/ability_card_bg.png");
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020DE File Offset: 0x000002DE
		[NullableContext(2)]
		public static Texture2D InventoryCardBg()
		{
			return SpireAssets.Load("ui/inventory_card_bg.png");
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020EA File Offset: 0x000002EA
		[NullableContext(2)]
		public static Texture2D IntensifyCardBg()
		{
			return SpireAssets.Load("ui/intensify_card_bg.png");
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020F6 File Offset: 0x000002F6
		[NullableContext(2)]
		public static Texture2D FusionCardBg()
		{
			return SpireAssets.Load("ui/fusion_card_bg.png");
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002102 File Offset: 0x00000302
		[NullableContext(2)]
		public static Texture2D CleanseCardBg()
		{
			return SpireAssets.Load("ui/cleanse_card_bg.png");
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000210E File Offset: 0x0000030E
		[NullableContext(2)]
		public static Texture2D TradeCardBg()
		{
			return SpireAssets.Load("ui/trade_card_bg.png");
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000211A File Offset: 0x0000031A
		[NullableContext(2)]
		public static Texture2D WipConstruction()
		{
			return SpireAssets.Load("ui/wip_construction.png");
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002126 File Offset: 0x00000326
		[NullableContext(2)]
		public static Texture2D ItemIcon(string relativePath)
		{
			if (!string.IsNullOrWhiteSpace(relativePath))
			{
				return SpireAssets.Load(relativePath);
			}
			return null;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002138 File Offset: 0x00000338
		[return: Nullable(2)]
		public static Texture2D RelicIcon(string relativePath)
		{
			return SpireAssets.Load(relativePath);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002140 File Offset: 0x00000340
		[return: Nullable(2)]
		public static string ReadText(string relativePath)
		{
			string text = relativePath.Replace('\\', '/').TrimStart('/');
			string text2 = SpireAssets.ResPath(text);
			try
			{
				if (FileAccess.FileExists(text2))
				{
					using (FileAccess fileAccess = FileAccess.Open(text2, 1L))
					{
						if (fileAccess != null)
						{
							return fileAccess.GetAsText(false);
						}
					}
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("Text res read failed (" + text2 + "): " + ex.Message, 1);
			}
			string text3 = Path.Combine(SpireAssets.ModDir, text.Replace('/', Path.DirectorySeparatorChar));
			if (!File.Exists(text3))
			{
				return null;
			}
			string result;
			try
			{
				result = File.ReadAllText(text3);
			}
			catch (Exception ex2)
			{
				SpireTheRpgMainFile.Logger.Info("Text disk read failed (" + text3 + "): " + ex2.Message, 1);
				result = null;
			}
			return result;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000223C File Offset: 0x0000043C
		public static IReadOnlyList<string> ListSubdirs(string relativeDir)
		{
			string text = relativeDir.Replace('\\', '/').TrimStart('/');
			string text2 = SpireAssets.ResPath(text);
			List<string> list = new List<string>();
			try
			{
				using (DirAccess dirAccess = DirAccess.Open(text2))
				{
					if (dirAccess != null)
					{
						dirAccess.ListDirBegin();
						for (;;)
						{
							string next = dirAccess.GetNext();
							if (string.IsNullOrEmpty(next))
							{
								break;
							}
							bool flag = next == "." || next == "..";
							if (!flag && dirAccess.CurrentIsDir())
							{
								list.Add(next);
							}
						}
						return list;
					}
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("ListSubdirs res failed (" + text2 + "): " + ex.Message, 1);
			}
			string path = Path.Combine(SpireAssets.ModDir, text.Replace('/', Path.DirectorySeparatorChar));
			if (!Directory.Exists(path))
			{
				return list;
			}
			foreach (string path2 in Directory.GetDirectories(path))
			{
				list.Add(Path.GetFileName(path2));
			}
			return list;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002378 File Offset: 0x00000578
		public static IReadOnlyList<string> ListFiles(string relativeDir, string extension)
		{
			string text = relativeDir.Replace('\\', '/').TrimStart('/');
			string text2 = extension.StartsWith('.') ? extension : ("." + extension);
			string text3 = SpireAssets.ResPath(text);
			List<string> list = new List<string>();
			try
			{
				using (DirAccess dirAccess = DirAccess.Open(text3))
				{
					if (dirAccess != null)
					{
						dirAccess.ListDirBegin();
						for (;;)
						{
							string next = dirAccess.GetNext();
							if (string.IsNullOrEmpty(next))
							{
								break;
							}
							bool flag = next == "." || next == "..";
							if (!flag && !dirAccess.CurrentIsDir() && next.EndsWith(text2, StringComparison.OrdinalIgnoreCase) && !next.EndsWith(".import", StringComparison.OrdinalIgnoreCase))
							{
								list.Add(next);
							}
						}
						return list;
					}
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("ListFiles res failed (" + text3 + "): " + ex.Message, 1);
			}
			string path = Path.Combine(SpireAssets.ModDir, text.Replace('/', Path.DirectorySeparatorChar));
			if (!Directory.Exists(path))
			{
				return list;
			}
			foreach (string path2 in Directory.GetFiles(path, "*" + text2))
			{
				list.Add(Path.GetFileName(path2));
			}
			return list;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024F8 File Offset: 0x000006F8
		[return: Nullable(2)]
		private static Texture2D Load(string relativePath)
		{
			string text = relativePath.Replace('\\', '/');
			Texture2D texture2D;
			if (SpireAssets.Cache.TryGetValue(text, out texture2D) && GodotObject.IsInstanceValid(texture2D))
			{
				return texture2D;
			}
			string text2 = SpireAssets.ResAsset(text);
			try
			{
				if (ResourceLoader.Exists(text2, ""))
				{
					Texture2D texture2D2 = ResourceLoader.Load<Texture2D>(text2, null, 1L);
					if (texture2D2 != null)
					{
						SpireAssets.Cache[text] = texture2D2;
						return texture2D2;
					}
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("Asset res load failed (" + text2 + "): " + ex.Message, 1);
			}
			string text3 = Path.Combine(SpireAssets.ModDir, "assets", text.Replace('/', Path.DirectorySeparatorChar));
			if (!File.Exists(text3))
			{
				SpireTheRpgMainFile.Logger.Info("Asset missing: " + text2 + " / " + text3, 1);
				return null;
			}
			Texture2D result;
			try
			{
				Image image = Image.LoadFromFile(text3);
				if (image == null)
				{
					result = null;
				}
				else
				{
					ImageTexture imageTexture = ImageTexture.CreateFromImage(image);
					SpireAssets.Cache[text] = imageTexture;
					result = imageTexture;
				}
			}
			catch (Exception ex2)
			{
				SpireTheRpgMainFile.Logger.Info("Asset disk load failed (" + text3 + "): " + ex2.Message, 1);
				result = null;
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002644 File Offset: 0x00000844
		// Note: this type is marked as 'beforefieldinit'.
		static SpireAssets()
		{
		}

		// Token: 0x04000001 RID: 1
		public const string ModId = "SpireTheRpg";

		// Token: 0x04000002 RID: 2
		private const string ResRoot = "res://SpireTheRpg";

		// Token: 0x04000003 RID: 3
		private static readonly Dictionary<string, Texture2D> Cache = new Dictionary<string, Texture2D>(StringComparer.OrdinalIgnoreCase);
	}
}
