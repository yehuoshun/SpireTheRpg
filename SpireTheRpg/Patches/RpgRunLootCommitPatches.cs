using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Runs;
using SpireTheRpg.Combat;
using SpireTheRpg.Core;
using SpireTheRpg.Ui;

namespace SpireTheRpg.Patches
{
	// Token: 0x02000020 RID: 32
	[HarmonyPatch]
	internal static class RpgRunLootCommitPatches
	{
		// Token: 0x06000173 RID: 371 RVA: 0x0000E228 File Offset: 0x0000C428
		[HarmonyPatch(typeof(RunManager), "OnEnded")]
		[HarmonyPostfix]
		private static void OnEndedPostfix(bool isVictory)
		{
			try
			{
				RpgInRunInventory.Hide(true);
				if (RpgEffectiveConfig.EnableMetaEquipment)
				{
					try
					{
						RpgProgress.Persist();
					}
					catch
					{
					}
					RpgRunLoot.SnapshotForDiscovery();
					if (isVictory || RpgEffectiveConfig.KeepLootOnAbandon)
					{
						Logger logger = SpireTheRpgMainFile.Logger;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 2);
						defaultInterpolatedStringHandler.AppendLiteral("RpgRunLoot commit victory=");
						defaultInterpolatedStringHandler.AppendFormatted<bool>(isVictory);
						defaultInterpolatedStringHandler.AppendLiteral(" abandonKeep=");
						defaultInterpolatedStringHandler.AppendFormatted<bool>(RpgEffectiveConfig.KeepLootOnAbandon);
						logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
						RpgRunLoot.CommitToPermanent();
					}
					else
					{
						SpireTheRpgMainFile.Logger.Info("RpgRunLoot discard on non-victory end", 1);
						RpgRunLoot.Discard();
					}
				}
				else
				{
					RpgRunLoot.SnapshotFromRunLoadout();
					SpireTheRpgMainFile.Logger.Info("RpgRunLoadout cleared (meta equipment off)", 1);
					RpgRunLoadout.Clear();
					RpgRunLoot.Discard();
				}
				RpgHostConfigSync.Clear(null);
			}
			catch (Exception value)
			{
				Logger logger2 = SpireTheRpgMainFile.Logger;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
				defaultInterpolatedStringHandler.AppendLiteral("RpgRunLoot OnEnded failed: ");
				defaultInterpolatedStringHandler.AppendFormatted<Exception>(value);
				logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000E338 File Offset: 0x0000C538
		[HarmonyPatch(typeof(RunManager), "InitializeNewRun")]
		[HarmonyPostfix]
		private static void NewRunPostfix()
		{
			RpgInRunInventory.Hide(true);
			RpgRunLoadout.Clear();
			RpgRunLoot.Clear();
			RpgRunLoot.ClearEndDiscoveries();
			RpgAccessoryRunState.ResetForNewRun();
			RpgLootTable.ResetSharedVictoryCount();
			RpgLoadoutNetSync.ClearCombatSync();
			RpgScrollBag.Clear();
			RpgPendingExtraRewards.Clear();
			if (SpireTheRpgConfig.EnableMetaEquipment)
			{
				try
				{
					List<RpgItemData> scrolls = RpgSaveStore.Load().Scrolls;
					if (scrolls.Count > 0)
					{
						RpgScrollBag.Load(RpgNet.LocalNetId(), scrolls);
					}
				}
				catch (Exception ex)
				{
					SpireTheRpgMainFile.Logger.Info("meta scroll restore failed: " + ex.Message, 1);
				}
			}
			SpireTheRpgMainFile.Logger.Info("RpgRunLoot cleared for new run", 1);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000E3DC File Offset: 0x0000C5DC
		[HarmonyPatch(typeof(RunManager), "InitializeSavedRun")]
		[HarmonyPostfix]
		private static void SavedRunPostfix()
		{
			RpgInRunInventory.Hide(true);
			RpgRunLoadout.RebindLocalNetId();
			RpgRunLoot.RebindLocalNetId();
			RpgPendingExtraRewards.RebindLocalNetId();
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 2);
			defaultInterpolatedStringHandler.AppendLiteral("Rpg run continue bag=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(RpgRunLoadout.BagCount);
			defaultInterpolatedStringHandler.AppendLiteral(" pending=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(RpgRunLoot.Count);
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
		}
	}
}
