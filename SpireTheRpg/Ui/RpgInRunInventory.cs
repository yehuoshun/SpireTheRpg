using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Runs;
using SpireTheRpg.Core;
using SpireTheRpg.Relics;

namespace SpireTheRpg.Ui
{
	// Token: 0x02000008 RID: 8
	[NullableContext(2)]
	[Nullable(0)]
	internal static class RpgInRunInventory
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00004CA4 File Offset: 0x00002EA4
		public static bool CanOpen()
		{
			if (RpgInRunInventory.TryGetRunState() == null)
			{
				return false;
			}
			try
			{
				NGame instance = NGame.Instance;
				NTransition ntransition = (instance != null) ? instance.Transition : null;
				if (ntransition != null && ntransition.InTransition)
				{
					return false;
				}
			}
			catch
			{
			}
			return true;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004CF4 File Offset: 0x00002EF4
		public static bool IsInCombat()
		{
			try
			{
				CombatManager instance = CombatManager.Instance;
				return instance != null && instance.IsInProgress;
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00004D2C File Offset: 0x00002F2C
		public static bool IsOpen
		{
			get
			{
				Control control = RpgInRunInventory.ResolveHost();
				return control != null && RpgInventoryScreen.IsOpenOn(control);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004D4C File Offset: 0x00002F4C
		public static void Toggle()
		{
			if (!RpgInRunInventory.CanOpen())
			{
				return;
			}
			Control control = RpgInRunInventory.ResolveHost();
			if (control == null)
			{
				return;
			}
			if (RpgInventoryScreen.IsOpenOn(control))
			{
				RpgInventoryScreen.Hide(control);
				return;
			}
			RpgInventoryScreen.Show(control, false);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004D84 File Offset: 0x00002F84
		public static void Hide(bool syncEquipped = true)
		{
			Control control = RpgInRunInventory.ResolveHost();
			if (control != null)
			{
				RpgInventoryScreen.Hide(control);
			}
			if (syncEquipped)
			{
				RpgLoadoutNetSync.PushEquipped(RpgNet.LocalNetId());
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public static void NotifyRelicChanged()
		{
			try
			{
				RunState runState = RpgInRunInventory.TryGetRunState();
				if (runState == null)
				{
					return;
				}
				foreach (Player player in runState.Players)
				{
					RpgItemRelic relic = player.GetRelic<RpgItemRelic>();
					if (relic != null)
					{
						relic.SyncAmountPublic();
					}
				}
			}
			catch
			{
			}
			RpgEquippedGearSync.Request(null);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004E28 File Offset: 0x00003028
		private static RunState TryGetRunState()
		{
			RunState result;
			try
			{
				RunManager instance = RunManager.Instance;
				if (instance == null)
				{
					result = null;
				}
				else
				{
					PropertyInfo propertyInfo = AccessTools.Property(typeof(RunManager), "State");
					result = (((propertyInfo != null) ? propertyInfo.GetValue(instance) : null) as RunState);
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004E84 File Offset: 0x00003084
		private static Control ResolveHost()
		{
			try
			{
				Control instance = NGame.Instance;
				if (instance != null)
				{
					return instance;
				}
			}
			catch
			{
			}
			try
			{
				Control instance2 = NRun.Instance;
				if (instance2 != null)
				{
					return instance2;
				}
			}
			catch
			{
			}
			return null;
		}
	}
}
