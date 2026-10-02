using System;
using System.Runtime.CompilerServices;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using SpireTheRpg.Core;
using SpireTheRpg.Ui;

namespace SpireTheRpg.Patches
{
	// Token: 0x02000025 RID: 37
	[NullableContext(1)]
	[Nullable(0)]
	[HarmonyPatch]
	internal static class RpgInventoryHotkeyPatches
	{
		// Token: 0x06000189 RID: 393 RVA: 0x0000ED50 File Offset: 0x0000CF50
		[HarmonyPatch(typeof(NInputManager), "_UnhandledKeyInput")]
		[HarmonyPostfix]
		private static void OnUnhandledKey(InputEvent inputEvent)
		{
			InputEventKey inputEventKey = inputEvent as InputEventKey;
			if (inputEventKey == null || !inputEventKey.Pressed || inputEventKey.Echo)
			{
				return;
			}
			RpgInputBindings.EnsureLists();
			RpgInputBindings.EnsureDefaultKey(NInputManager.Instance);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			if (!RpgInventoryHotkeyPatches._loggedBind)
			{
				RpgInventoryHotkeyPatches._loggedBind = true;
				Key value = RpgInventoryHotkeyPatches.BoundKey();
				Logger logger = SpireTheRpgMainFile.Logger;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 2);
				defaultInterpolatedStringHandler.AppendLiteral("RpgInventoryHotkey listening bound=");
				defaultInterpolatedStringHandler.AppendFormatted<Key>(value);
				defaultInterpolatedStringHandler.AppendLiteral(" canOpen=");
				defaultInterpolatedStringHandler.AppendFormatted<bool>(RpgInRunInventory.CanOpen());
				logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			}
			if (!RpgInventoryHotkeyPatches.MatchesBoundKey(inputEventKey))
			{
				return;
			}
			Logger logger2 = SpireTheRpgMainFile.Logger;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(58, 4);
			defaultInterpolatedStringHandler.AppendLiteral("RpgInventoryHotkey key hit code=");
			defaultInterpolatedStringHandler.AppendFormatted<Key>(inputEventKey.Keycode);
			defaultInterpolatedStringHandler.AppendLiteral(" physical=");
			defaultInterpolatedStringHandler.AppendFormatted<Key>(inputEventKey.PhysicalKeycode);
			defaultInterpolatedStringHandler.AppendLiteral(" label=");
			defaultInterpolatedStringHandler.AppendFormatted<Key>(inputEventKey.KeyLabel);
			defaultInterpolatedStringHandler.AppendLiteral(" canOpen=");
			defaultInterpolatedStringHandler.AppendFormatted<bool>(RpgInRunInventory.CanOpen());
			logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			if (!RpgInRunInventory.CanOpen())
			{
				return;
			}
			ulong ticksMsec = Time.GetTicksMsec();
			if (ticksMsec - RpgInventoryHotkeyPatches._lastToggleMs < 200UL)
			{
				return;
			}
			RpgInventoryHotkeyPatches._lastToggleMs = ticksMsec;
			RpgInRunInventory.Toggle();
			Logger logger3 = SpireTheRpgMainFile.Logger;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(32, 1);
			defaultInterpolatedStringHandler.AppendLiteral("RpgInventoryHotkey toggled open=");
			defaultInterpolatedStringHandler.AppendFormatted<bool>(RpgInRunInventory.IsOpen);
			logger3.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000EEC7 File Offset: 0x0000D0C7
		[HarmonyPatch(typeof(NGame), "_Ready")]
		[HarmonyPostfix]
		private static void NGameReady(NGame __instance)
		{
			RpgInputBindings.EnsureLists();
			RpgInputBindings.EnsureDefaultKey(NInputManager.Instance);
			SpireTheRpgMainFile.Logger.Info("RpgInventoryHotkey NGame ready", 1);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		[HarmonyPatch(typeof(NRun), "_Ready")]
		[HarmonyPostfix]
		private static void NRunReady(NRun __instance)
		{
			RpgInputBindings.EnsureDefaultKey(NInputManager.Instance);
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 2);
			defaultInterpolatedStringHandler.AppendLiteral("RpgInventoryHotkey NRun ready key=");
			defaultInterpolatedStringHandler.AppendFormatted<Key>(RpgInventoryHotkeyPatches.BoundKey());
			defaultInterpolatedStringHandler.AppendLiteral(" canOpen=");
			defaultInterpolatedStringHandler.AppendFormatted<bool>(RpgInRunInventory.CanOpen());
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000EF4B File Offset: 0x0000D14B
		private static Key BoundKey()
		{
			return RpgInputBindings.GetBoundKey(null);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000EF54 File Offset: 0x0000D154
		private static bool MatchesBoundKey(InputEventKey key)
		{
			Key key2 = RpgInventoryHotkeyPatches.BoundKey();
			return key.Keycode == key2 || key.PhysicalKeycode == key2 || key.KeyLabel == key2;
		}

		// Token: 0x0400008C RID: 140
		private static ulong _lastToggleMs;

		// Token: 0x0400008D RID: 141
		private static bool _loggedBind;
	}
}
