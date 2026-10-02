using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Multiplayer.Game;
using MegaCrit.Sts2.Core.Runs;

namespace SpireTheRpg.Core
{
	// Token: 0x0200004E RID: 78
	[NullableContext(2)]
	[Nullable(0)]
	internal static class RpgNet
	{
		// Token: 0x06000317 RID: 791 RVA: 0x00015D50 File Offset: 0x00013F50
		public static bool IsMultiplayerRun()
		{
			bool result;
			try
			{
				RunManager instance = RunManager.Instance;
				INetGameService netGameService = (instance != null) ? instance.NetService : null;
				if (netGameService == null || !netGameService.IsConnected || netGameService.Type == 1)
				{
					result = false;
				}
				else
				{
					PropertyInfo propertyInfo = AccessTools.Property(typeof(RunManager), "State");
					RunState runState = ((propertyInfo != null) ? propertyInfo.GetValue(instance) : null) as RunState;
					result = (runState != null && runState.Players.Count > 1);
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00015DDC File Offset: 0x00013FDC
		public static bool CanSendCustom()
		{
			bool result;
			try
			{
				RunManager instance = RunManager.Instance;
				INetGameService netGameService = (instance != null) ? instance.NetService : null;
				if (netGameService == null || !netGameService.IsConnected || netGameService.Type == 1)
				{
					result = false;
				}
				else
				{
					PropertyInfo propertyInfo = AccessTools.Property(typeof(RunManager), "State");
					RunState runState = ((propertyInfo != null) ? propertyInfo.GetValue(instance) : null) as RunState;
					result = (runState != null && runState.Players.Count > 1);
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00015E68 File Offset: 0x00014068
		public static ulong LocalNetId()
		{
			try
			{
				ulong? netId = LocalContext.NetId;
				if (netId != null)
				{
					return netId.GetValueOrDefault();
				}
			}
			catch
			{
			}
			try
			{
				RunManager instance = RunManager.Instance;
				if (((instance != null) ? instance.NetService : null) != null)
				{
					return instance.NetService.NetId;
				}
			}
			catch
			{
			}
			return 0UL;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00015EE0 File Offset: 0x000140E0
		public static ulong NetIdOf(Player player)
		{
			if (player == null)
			{
				return RpgNet.LocalNetId();
			}
			return player.NetId;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00015EF4 File Offset: 0x000140F4
		public static bool IsLocal(Player player)
		{
			if (player == null)
			{
				return false;
			}
			try
			{
				if (LocalContext.NetId != null)
				{
					return LocalContext.IsMe(player);
				}
			}
			catch
			{
			}
			bool result;
			try
			{
				RunManager instance = RunManager.Instance;
				if (((instance != null) ? instance.NetService : null) != null && instance.NetService.Type != 1)
				{
					result = (player.NetId == instance.NetService.NetId);
				}
				else
				{
					result = true;
				}
			}
			catch
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00015F84 File Offset: 0x00014184
		public static Player LocalPlayer()
		{
			Player result;
			try
			{
				RunManager instance = RunManager.Instance;
				PropertyInfo propertyInfo = AccessTools.Property(typeof(RunManager), "State");
				RunState runState = ((propertyInfo != null) ? propertyInfo.GetValue(instance) : null) as RunState;
				if (runState == null)
				{
					result = null;
				}
				else
				{
					ulong local = RpgNet.LocalNetId();
					result = (runState.Players.FirstOrDefault((Player p) => p.NetId == local) ?? runState.Players.FirstOrDefault<Player>());
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00016018 File Offset: 0x00014218
		public static Player PlayerByNetId(ulong netId)
		{
			Player result;
			try
			{
				RunManager instance = RunManager.Instance;
				PropertyInfo propertyInfo = AccessTools.Property(typeof(RunManager), "State");
				RunState runState = ((propertyInfo != null) ? propertyInfo.GetValue(instance) : null) as RunState;
				result = ((runState != null) ? runState.Players.FirstOrDefault((Player p) => p.NetId == netId) : null);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0200010F RID: 271
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x06000797 RID: 1943 RVA: 0x000278EE File Offset: 0x00025AEE
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x06000798 RID: 1944 RVA: 0x000278F6 File Offset: 0x00025AF6
			[NullableContext(0)]
			internal bool <LocalPlayer>b__0(Player p)
			{
				return p.NetId == this.local;
			}

			// Token: 0x040003EC RID: 1004
			public ulong local;
		}

		// Token: 0x02000110 RID: 272
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x06000799 RID: 1945 RVA: 0x00027906 File Offset: 0x00025B06
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x0600079A RID: 1946 RVA: 0x0002790E File Offset: 0x00025B0E
			[NullableContext(0)]
			internal bool <PlayerByNetId>b__0(Player p)
			{
				return p.NetId == this.netId;
			}

			// Token: 0x040003ED RID: 1005
			public ulong netId;
		}
	}
}
