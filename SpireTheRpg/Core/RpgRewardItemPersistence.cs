using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using BaseLib.Patches.Saves;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Multiplayer.Serialization;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Saves.Runs;
using SpireTheRpg.Combat;

namespace SpireTheRpg.Core
{
	// Token: 0x02000056 RID: 86
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgRewardItemPersistence
	{
		// Token: 0x06000363 RID: 867 RVA: 0x000170D4 File Offset: 0x000152D4
		public static void Register()
		{
			if (RpgRewardItemPersistence._registered)
			{
				return;
			}
			RpgRewardItemPersistence._registered = true;
			try
			{
				string text = "SpireTheRpg_RewardItem";
				Func<Reward, string> func;
				if ((func = RpgRewardItemPersistence.<>O.<0>__Capture) == null)
				{
					func = (RpgRewardItemPersistence.<>O.<0>__Capture = new Func<Reward, string>(RpgRewardItemPersistence.Capture));
				}
				Action<Reward, string> action;
				if ((action = RpgRewardItemPersistence.<>O.<1>__Apply) == null)
				{
					action = (RpgRewardItemPersistence.<>O.<1>__Apply = new Action<Reward, string>(RpgRewardItemPersistence.Apply));
				}
				ExtendedSaveHandlers<Reward, SerializableReward>.RegisterSave<string>(text, func, action, delegate(string json, [Nullable(1)] PacketWriter writer)
				{
					writer.WriteString(json ?? "");
				}, ([Nullable(1)] PacketReader reader) => reader.ReadString() ?? "");
				SpireTheRpgMainFile.Logger.Info("RpgRewardItemPersistence registered on SerializableReward", 1);
			}
			catch (Exception value)
			{
				Logger logger = SpireTheRpgMainFile.Logger;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 1);
				defaultInterpolatedStringHandler.AppendLiteral("RpgRewardItemPersistence register failed: ");
				defaultInterpolatedStringHandler.AppendFormatted<Exception>(value);
				logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000171C4 File Offset: 0x000153C4
		[return: Nullable(2)]
		private static string Capture(Reward reward)
		{
			RpgEquipmentReward rpgEquipmentReward = reward as RpgEquipmentReward;
			string text;
			if (rpgEquipmentReward == null)
			{
				RpgScrollReward rpgScrollReward = reward as RpgScrollReward;
				if (rpgScrollReward == null)
				{
					text = null;
				}
				else
				{
					text = rpgScrollReward.ExportSaveJson(RpgRewardItemPersistence.JsonOptions);
				}
			}
			else
			{
				text = rpgEquipmentReward.ExportSaveJson(RpgRewardItemPersistence.JsonOptions);
			}
			string text2 = text;
			if (string.IsNullOrWhiteSpace(text2))
			{
				return null;
			}
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(45, 2);
			defaultInterpolatedStringHandler.AppendLiteral("RpgRewardItemPersistence capture type=");
			defaultInterpolatedStringHandler.AppendFormatted(reward.GetType().Name);
			defaultInterpolatedStringHandler.AppendLiteral(" bytes=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(text2.Length);
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			return text2;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00017268 File Offset: 0x00015468
		private static void Apply(Reward reward, [Nullable(2)] string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				bool flag = reward is RpgEquipmentReward || reward is RpgScrollReward;
				if (flag)
				{
					SpireTheRpgMainFile.Logger.Info("RpgRewardItemPersistence apply EMPTY type=" + reward.GetType().Name, 1);
				}
				return;
			}
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 2);
			defaultInterpolatedStringHandler.AppendLiteral("RpgRewardItemPersistence apply type=");
			defaultInterpolatedStringHandler.AppendFormatted(reward.GetType().Name);
			defaultInterpolatedStringHandler.AppendLiteral(" bytes=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(json.Length);
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			RpgEquipmentReward rpgEquipmentReward = reward as RpgEquipmentReward;
			if (rpgEquipmentReward != null)
			{
				rpgEquipmentReward.ImportSaveJson(json, RpgRewardItemPersistence.JsonOptions);
				return;
			}
			RpgScrollReward rpgScrollReward = reward as RpgScrollReward;
			if (rpgScrollReward == null)
			{
				return;
			}
			rpgScrollReward.ImportSaveJson(json, RpgRewardItemPersistence.JsonOptions);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00017338 File Offset: 0x00015538
		// Note: this type is marked as 'beforefieldinit'.
		static RpgRewardItemPersistence()
		{
		}

		// Token: 0x04000116 RID: 278
		private const string SaveId = "SpireTheRpg_RewardItem";

		// Token: 0x04000117 RID: 279
		private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			Converters = 
			{
				new JsonStringEnumConverter()
			}
		};

		// Token: 0x04000118 RID: 280
		private static bool _registered;

		// Token: 0x0200011F RID: 287
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000430 RID: 1072
			[Nullable(new byte[]
			{
				0,
				1,
				2
			})]
			public static Func<Reward, string> <0>__Capture;

			// Token: 0x04000431 RID: 1073
			[Nullable(new byte[]
			{
				0,
				1,
				2
			})]
			public static Action<Reward, string> <1>__Apply;
		}

		// Token: 0x02000120 RID: 288
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060007BF RID: 1983 RVA: 0x00028E06 File Offset: 0x00027006
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060007C0 RID: 1984 RVA: 0x00028E12 File Offset: 0x00027012
			public <>c()
			{
			}

			// Token: 0x060007C1 RID: 1985 RVA: 0x00028E1A File Offset: 0x0002701A
			[NullableContext(0)]
			internal void <Register>b__3_0(string json, [Nullable(1)] PacketWriter writer)
			{
				writer.WriteString(json ?? "");
			}

			// Token: 0x060007C2 RID: 1986 RVA: 0x00028E2C File Offset: 0x0002702C
			[NullableContext(0)]
			internal string <Register>b__3_1([Nullable(1)] PacketReader reader)
			{
				return reader.ReadString() ?? "";
			}

			// Token: 0x04000432 RID: 1074
			[Nullable(0)]
			public static readonly RpgRewardItemPersistence.<>c <>9 = new RpgRewardItemPersistence.<>c();

			// Token: 0x04000433 RID: 1075
			[Nullable(new byte[]
			{
				0,
				0,
				1
			})]
			public static Action<string, PacketWriter> <>9__3_0;

			// Token: 0x04000434 RID: 1076
			[Nullable(new byte[]
			{
				0,
				1,
				0
			})]
			public static Func<PacketReader, string> <>9__3_1;
		}
	}
}
