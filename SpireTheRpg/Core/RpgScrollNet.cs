using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Logging;

namespace SpireTheRpg.Core
{
	// Token: 0x02000063 RID: 99
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgScrollNet
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x0001A010 File Offset: 0x00018210
		public static void BroadcastUse(ulong ownerNetId, string scrollInstanceId, RpgScrollKind kind, int goldAfter, bool magicArmed, [Nullable(2)] RpgItemData changedItem)
		{
			try
			{
				if (RpgNet.CanSendCustom())
				{
					CustomMessageWrapper.Send(new RpgScrollUseMessage
					{
						OwnerNetId = ownerNetId,
						ScrollInstanceId = scrollInstanceId,
						Kind = (int)kind,
						GoldAfter = goldAfter,
						MagicArmed = magicArmed,
						ItemJson = ((changedItem == null) ? "" : JsonSerializer.Serialize<RpgItemData>(changedItem, RpgScrollNet.JsonOptions))
					}, null);
					Logger logger = SpireTheRpgMainFile.Logger;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 3);
					defaultInterpolatedStringHandler.AppendLiteral("RpgScrollNet push kind=");
					defaultInterpolatedStringHandler.AppendFormatted<RpgScrollKind>(kind);
					defaultInterpolatedStringHandler.AppendLiteral(" net=");
					defaultInterpolatedStringHandler.AppendFormatted<ulong>(ownerNetId);
					defaultInterpolatedStringHandler.AppendLiteral(" goldAfter=");
					defaultInterpolatedStringHandler.AppendFormatted<int>(goldAfter);
					logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RpgScrollNet push failed: " + ex.Message, 1);
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001A0FC File Offset: 0x000182FC
		[return: Nullable(2)]
		public static RpgItemData DecodeItem(string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				return null;
			}
			RpgItemData result;
			try
			{
				result = JsonSerializer.Deserialize<RpgItemData>(json, RpgScrollNet.JsonOptions);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001A138 File Offset: 0x00018338
		// Note: this type is marked as 'beforefieldinit'.
		static RpgScrollNet()
		{
		}

		// Token: 0x04000165 RID: 357
		private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			Converters = 
			{
				new JsonStringEnumConverter()
			}
		};
	}
}
