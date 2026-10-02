using System;
using System.Runtime.CompilerServices;
using BaseLib.Config;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using SpireTheRpg.Core;

namespace SpireTheRpg
{
	// Token: 0x02000004 RID: 4
	[NullableContext(1)]
	[Nullable(0)]
	[ModInitializer("Initialize")]
	public static class SpireTheRpgMainFile
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002F2C File Offset: 0x0000112C
		public static Logger Logger
		{
			[CompilerGenerated]
			get
			{
				return SpireTheRpgMainFile.<Logger>k__BackingField;
			}
		} = new Logger("SpireTheRpg", 0);

		// Token: 0x06000028 RID: 40 RVA: 0x00002F34 File Offset: 0x00001134
		public static void Initialize()
		{
			SpireTheRpgMainFile.Logger.Info("SpireTheRpg loaded", 1);
			SpireTheRpgMainFile.Logger.Info("Rpg data root: " + RpgPaths.ModDataRoot, 1);
			SpireLoc.Inject();
			RpgInputBindings.EnsureLists();
			RpgRunEquipmentPersistence.Register();
			RpgRewardItemPersistence.Register();
			try
			{
				ModConfigRegistry.Register("SpireTheRpg", new SpireTheRpgConfig());
			}
			catch (Exception value)
			{
				Logger logger = SpireTheRpgMainFile.Logger;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
				defaultInterpolatedStringHandler.AppendLiteral("ModConfig register failed: ");
				defaultInterpolatedStringHandler.AppendFormatted<Exception>(value);
				logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			}
			new Harmony("SpireTheRpg").PatchAll();
			RpgItemRoller.SelfCheck();
			RpgItemIntensify.SelfCheck();
			RpgItemFusion.SelfCheck();
			SpireTheRpgMainFile.Logger.Info("SpireTheRpg Harmony patches applied", 1);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003004 File Offset: 0x00001204
		// Note: this type is marked as 'beforefieldinit'.
		static SpireTheRpgMainFile()
		{
		}

		// Token: 0x0400000B RID: 11
		public const string ModId = "SpireTheRpg";

		// Token: 0x0400000C RID: 12
		[CompilerGenerated]
		private static readonly Logger <Logger>k__BackingField;
	}
}
