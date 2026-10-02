using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using MegaCrit.Sts2.Core.Saves;

namespace SpireTheRpg.Core
{
	// Token: 0x0200005D RID: 93
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgSaveStore
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x000195B0 File Offset: 0x000177B0
		public static string SavePath
		{
			get
			{
				int val = 0;
				try
				{
					SaveManager instance = SaveManager.Instance;
					val = ((instance != null) ? instance.CurrentProfileId : 0);
				}
				catch
				{
				}
				string saveDir = RpgPaths.SaveDir;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(17, 1);
				defaultInterpolatedStringHandler.AppendLiteral("rpg_profile_");
				defaultInterpolatedStringHandler.AppendFormatted<int>(Math.Max(0, val));
				defaultInterpolatedStringHandler.AppendLiteral(".json");
				return Path.Combine(saveDir, defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00019628 File Offset: 0x00017828
		public static RpgSaveDto Load()
		{
			RpgSaveDto result;
			try
			{
				string savePath = RpgSaveStore.SavePath;
				RpgSaveStore.TryMigrateLegacy(savePath);
				if (!File.Exists(savePath))
				{
					result = new RpgSaveDto();
				}
				else
				{
					result = (JsonSerializer.Deserialize<RpgSaveDto>(File.ReadAllText(savePath), RpgSaveStore.JsonOptions) ?? new RpgSaveDto());
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("Rpg save load failed: " + ex.Message, 1);
				result = new RpgSaveDto();
			}
			return result;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000196A4 File Offset: 0x000178A4
		public static void Save(RpgSaveDto dto)
		{
			try
			{
				string savePath = RpgSaveStore.SavePath;
				Directory.CreateDirectory(Path.GetDirectoryName(savePath));
				dto.BagIds = new List<string>();
				dto.Equipped = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
				File.WriteAllText(savePath, JsonSerializer.Serialize<RpgSaveDto>(dto, RpgSaveStore.JsonOptions));
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("Rpg save write failed: " + ex.Message, 1);
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00019720 File Offset: 0x00017920
		private static void TryMigrateLegacy(string newPath)
		{
			if (File.Exists(newPath))
			{
				return;
			}
			try
			{
				string fileName = Path.GetFileName(newPath);
				string text = Path.Combine(SpireAssets.ModDir, "save", fileName);
				if (File.Exists(text))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(newPath));
					File.Copy(text, newPath);
					SpireTheRpgMainFile.Logger.Info("Migrated RPG save to " + newPath, 1);
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("Rpg save migrate failed: " + ex.Message, 1);
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000197B4 File Offset: 0x000179B4
		// Note: this type is marked as 'beforefieldinit'.
		static RpgSaveStore()
		{
		}

		// Token: 0x04000156 RID: 342
		private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
		{
			WriteIndented = true,
			PropertyNameCaseInsensitive = true,
			Converters = 
			{
				new JsonStringEnumConverter()
			}
		};
	}
}
