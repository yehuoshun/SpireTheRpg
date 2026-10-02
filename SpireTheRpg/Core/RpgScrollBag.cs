using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SpireTheRpg.Core
{
	// Token: 0x0200005F RID: 95
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgScrollBag
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x000197E0 File Offset: 0x000179E0
		private static List<RpgItemData> Of(ulong netId)
		{
			List<RpgItemData> list;
			if (!RpgScrollBag.ByNetId.TryGetValue(netId, out list))
			{
				list = new List<RpgItemData>();
				RpgScrollBag.ByNetId[netId] = list;
			}
			return list;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001980F File Offset: 0x00017A0F
		public static void Clear()
		{
			RpgScrollBag.ByNetId.Clear();
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001981B File Offset: 0x00017A1B
		public static IReadOnlyList<RpgItemData> OfPlayer(ulong netId)
		{
			return RpgScrollBag.Of(netId);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00019823 File Offset: 0x00017A23
		public static IReadOnlyList<RpgItemData> Local()
		{
			return RpgScrollBag.Of(RpgNet.LocalNetId());
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001982F File Offset: 0x00017A2F
		public static void Add(ulong netId, RpgItemData scroll)
		{
			RpgScrollBag.Of(netId).Add(scroll.Clone());
			RpgScrollBag.PersistMeta();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00019848 File Offset: 0x00017A48
		public static bool TryRemove(ulong netId, string instanceId)
		{
			List<RpgItemData> list = RpgScrollBag.Of(netId);
			int num = list.FindIndex((RpgItemData x) => x.InstanceId == instanceId);
			if (num < 0)
			{
				return false;
			}
			list.RemoveAt(num);
			RpgScrollBag.PersistMeta();
			return true;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001988F File Offset: 0x00017A8F
		public static List<RpgItemData> Snapshot(ulong netId)
		{
			return (from x in RpgScrollBag.Of(netId)
			select x.Clone()).ToList<RpgItemData>();
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000198C0 File Offset: 0x00017AC0
		public static void Load(ulong netId, [Nullable(new byte[]
		{
			2,
			1
		})] IEnumerable<RpgItemData> scrolls)
		{
			List<RpgItemData> list = RpgScrollBag.Of(netId);
			list.Clear();
			if (scrolls == null)
			{
				return;
			}
			foreach (RpgItemData rpgItemData in scrolls)
			{
				list.Add(rpgItemData.Clone());
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00019920 File Offset: 0x00017B20
		private static void PersistMeta()
		{
			if (!RpgEffectiveConfig.EnableMetaEquipment)
			{
				return;
			}
			try
			{
				RpgProgress.Persist();
			}
			catch
			{
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00019950 File Offset: 0x00017B50
		// Note: this type is marked as 'beforefieldinit'.
		static RpgScrollBag()
		{
		}

		// Token: 0x0400015C RID: 348
		private static readonly Dictionary<ulong, List<RpgItemData>> ByNetId = new Dictionary<ulong, List<RpgItemData>>();

		// Token: 0x0200012E RID: 302
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600080C RID: 2060 RVA: 0x00029803 File Offset: 0x00027A03
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600080D RID: 2061 RVA: 0x0002980F File Offset: 0x00027A0F
			public <>c()
			{
			}

			// Token: 0x0600080E RID: 2062 RVA: 0x00029817 File Offset: 0x00027A17
			[NullableContext(0)]
			internal RpgItemData <Snapshot>b__7_0(RpgItemData x)
			{
				return x.Clone();
			}

			// Token: 0x04000462 RID: 1122
			[Nullable(0)]
			public static readonly RpgScrollBag.<>c <>9 = new RpgScrollBag.<>c();

			// Token: 0x04000463 RID: 1123
			[Nullable(0)]
			public static Func<RpgItemData, RpgItemData> <>9__7_0;
		}

		// Token: 0x0200012F RID: 303
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x0600080F RID: 2063 RVA: 0x0002981F File Offset: 0x00027A1F
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06000810 RID: 2064 RVA: 0x00029827 File Offset: 0x00027A27
			internal bool <TryRemove>b__0(RpgItemData x)
			{
				return x.InstanceId == this.instanceId;
			}

			// Token: 0x04000464 RID: 1124
			[Nullable(0)]
			public string instanceId;
		}
	}
}
