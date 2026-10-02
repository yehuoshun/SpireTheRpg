using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Logging;
using SpireTheRpg.Core;

namespace SpireTheRpg.Combat
{
	// Token: 0x02000078 RID: 120
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgRunLoot
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x0001CE5C File Offset: 0x0001B05C
		private static List<RpgItemData> Of(ulong netId)
		{
			List<RpgItemData> list;
			if (!RpgRunLoot.ByNetId.TryGetValue(netId, out list))
			{
				list = new List<RpgItemData>();
				RpgRunLoot.ByNetId[netId] = list;
			}
			return list;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001CE8B File Offset: 0x0001B08B
		private static List<RpgItemData> Local()
		{
			return RpgRunLoot.Of(RpgNet.LocalNetId());
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0001CE97 File Offset: 0x0001B097
		public static IReadOnlyList<RpgItemData> Current
		{
			get
			{
				return RpgRunLoot.Local();
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001CE9E File Offset: 0x0001B09E
		public static IReadOnlyList<RpgItemData> LastEndDiscoveries
		{
			get
			{
				return RpgRunLoot.EndDiscoveries;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0001CEA5 File Offset: 0x0001B0A5
		public static int Count
		{
			get
			{
				return RpgRunLoot.Local().Count;
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001CEB1 File Offset: 0x0001B0B1
		public static int CountOf(ulong netId)
		{
			return RpgRunLoot.Of(netId).Count;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001CEBE File Offset: 0x0001B0BE
		public static void Clear()
		{
			RpgRunLoot.ByNetId.Clear();
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001CECC File Offset: 0x0001B0CC
		public static void RebindLocalNetId()
		{
			ulong num = RpgNet.LocalNetId();
			if (RpgRunLoot.ByNetId.ContainsKey(num) && RpgRunLoot.CountOf(num) > 0)
			{
				return;
			}
			if (RpgRunLoot.ByNetId.Count != 1)
			{
				return;
			}
			ulong num2;
			List<RpgItemData> list;
			RpgRunLoot.ByNetId.First<KeyValuePair<ulong, List<RpgItemData>>>().Deconstruct(out num2, out list);
			ulong num3 = num2;
			List<RpgItemData> value = list;
			if (num3 == num)
			{
				return;
			}
			RpgRunLoot.ByNetId.Remove(num3);
			RpgRunLoot.ByNetId[num] = value;
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 2);
			defaultInterpolatedStringHandler.AppendLiteral("RpgRunLoot rebound ");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(num3);
			defaultInterpolatedStringHandler.AppendLiteral(" -> ");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(num);
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001CF81 File Offset: 0x0001B181
		public static void LoadFromList([Nullable(new byte[]
		{
			2,
			1
		})] IEnumerable<RpgItemData> items)
		{
			RpgRunLoot.LoadFromList(RpgNet.LocalNetId(), items);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001CF90 File Offset: 0x0001B190
		public static void LoadFromList(ulong netId, [Nullable(new byte[]
		{
			2,
			1
		})] IEnumerable<RpgItemData> items)
		{
			List<RpgItemData> list = RpgRunLoot.Of(netId);
			list.Clear();
			if (items == null)
			{
				return;
			}
			foreach (RpgItemData rpgItemData in items)
			{
				rpgItemData.SyncVisualFromTemplate();
				list.Add(rpgItemData.Clone());
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001CFF4 File Offset: 0x0001B1F4
		public static void Add(RpgItemData item)
		{
			RpgRunLoot.Add(RpgNet.LocalNetId(), item);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001D001 File Offset: 0x0001B201
		public static void Add(ulong netId, RpgItemData item)
		{
			RpgRunLoot.Of(netId).Add(item.Clone());
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001D014 File Offset: 0x0001B214
		public static void SnapshotForDiscovery()
		{
			RpgRunLoot.EndDiscoveries.Clear();
			foreach (List<RpgItemData> list in RpgRunLoot.ByNetId.Values)
			{
				foreach (RpgItemData rpgItemData in list)
				{
					RpgRunLoot.EndDiscoveries.Add(rpgItemData.Clone());
				}
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001D0B4 File Offset: 0x0001B2B4
		public static void SnapshotFromRunLoadout()
		{
			RpgRunLoot.EndDiscoveries.Clear();
			foreach (RpgItemData rpgItemData in RpgRunLoadout.AllForDiscovery())
			{
				RpgRunLoot.EndDiscoveries.Add(rpgItemData.Clone());
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001D114 File Offset: 0x0001B314
		public static void ClearEndDiscoveries()
		{
			RpgRunLoot.EndDiscoveries.Clear();
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001D120 File Offset: 0x0001B320
		public static void CommitToPermanent()
		{
			bool flag = false;
			foreach (List<RpgItemData> list in RpgRunLoot.ByNetId.Values)
			{
				foreach (RpgItemData item in list)
				{
					RpgLoadout.AddToBag(item, false);
					flag = true;
				}
				list.Clear();
			}
			if (!flag)
			{
				return;
			}
			RpgProgress.Persist();
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 1);
			defaultInterpolatedStringHandler.AppendLiteral("RpgRunLoot committed permanent bag=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(RpgLoadout.Bag.Count);
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001D1F8 File Offset: 0x0001B3F8
		public static void Discard()
		{
			int num = RpgRunLoot.ByNetId.Values.Sum((List<RpgItemData> x) => x.Count);
			if (num == 0)
			{
				return;
			}
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 1);
			defaultInterpolatedStringHandler.AppendLiteral("RpgRunLoot discarded count=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(num);
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			RpgRunLoot.ByNetId.Clear();
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001D274 File Offset: 0x0001B474
		public static string NamesSummary(int maxLines = 8)
		{
			List<RpgItemData> list = RpgRunLoot.Local();
			if (list.Count == 0)
			{
				return SpireLoc.T("SPIRE_LOOT.run_empty", "(none this run)");
			}
			return string.Join("\n", from i in list.Take(maxLines)
			select "· " + i.DisplayName);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001D2D4 File Offset: 0x0001B4D4
		public static string EndDiscoveryBody(int maxLines = 10)
		{
			if (RpgRunLoot.EndDiscoveries.Count == 0)
			{
				return "";
			}
			string str = SpireLoc.T("SPIRE_LOOT.discovery_body", "Equipment found this run: {0}").Replace("{0}", RpgRunLoot.EndDiscoveries.Count.ToString());
			string text = string.Join("\n", from i in RpgRunLoot.EndDiscoveries.Take(maxLines)
			select i.DisplayName);
			if (RpgRunLoot.EndDiscoveries.Count > maxLines)
			{
				text += "\n....";
			}
			return str + "\n\n" + text;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001D37C File Offset: 0x0001B57C
		public static Dictionary<ulong, List<RpgItemData>> SnapshotAll()
		{
			Dictionary<ulong, List<RpgItemData>> dictionary = new Dictionary<ulong, List<RpgItemData>>();
			foreach (KeyValuePair<ulong, List<RpgItemData>> keyValuePair in RpgRunLoot.ByNetId)
			{
				ulong num;
				List<RpgItemData> list;
				keyValuePair.Deconstruct(out num, out list);
				ulong key = num;
				List<RpgItemData> source = list;
				dictionary[key] = (from x in source
				select x.Clone()).ToList<RpgItemData>();
			}
			return dictionary;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001D414 File Offset: 0x0001B614
		// Note: this type is marked as 'beforefieldinit'.
		static RpgRunLoot()
		{
		}

		// Token: 0x040001B2 RID: 434
		private static readonly Dictionary<ulong, List<RpgItemData>> ByNetId = new Dictionary<ulong, List<RpgItemData>>();

		// Token: 0x040001B3 RID: 435
		private static readonly List<RpgItemData> EndDiscoveries = new List<RpgItemData>();

		// Token: 0x0200014C RID: 332
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000851 RID: 2129 RVA: 0x0002B99A File Offset: 0x00029B9A
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000852 RID: 2130 RVA: 0x0002B9A6 File Offset: 0x00029BA6
			public <>c()
			{
			}

			// Token: 0x06000853 RID: 2131 RVA: 0x0002B9AE File Offset: 0x00029BAE
			[NullableContext(0)]
			internal int <Discard>b__21_0(List<RpgItemData> x)
			{
				return x.Count;
			}

			// Token: 0x06000854 RID: 2132 RVA: 0x0002B9B6 File Offset: 0x00029BB6
			[NullableContext(0)]
			internal string <NamesSummary>b__22_0(RpgItemData i)
			{
				return "· " + i.DisplayName;
			}

			// Token: 0x06000855 RID: 2133 RVA: 0x0002B9C8 File Offset: 0x00029BC8
			[NullableContext(0)]
			internal string <EndDiscoveryBody>b__23_0(RpgItemData i)
			{
				return i.DisplayName;
			}

			// Token: 0x06000856 RID: 2134 RVA: 0x0002B9D0 File Offset: 0x00029BD0
			[NullableContext(0)]
			internal RpgItemData <SnapshotAll>b__24_0(RpgItemData x)
			{
				return x.Clone();
			}

			// Token: 0x040004D5 RID: 1237
			[Nullable(0)]
			public static readonly RpgRunLoot.<>c <>9 = new RpgRunLoot.<>c();

			// Token: 0x040004D6 RID: 1238
			[Nullable(0)]
			public static Func<List<RpgItemData>, int> <>9__21_0;

			// Token: 0x040004D7 RID: 1239
			[Nullable(0)]
			public static Func<RpgItemData, string> <>9__22_0;

			// Token: 0x040004D8 RID: 1240
			[Nullable(0)]
			public static Func<RpgItemData, string> <>9__23_0;

			// Token: 0x040004D9 RID: 1241
			[Nullable(0)]
			public static Func<RpgItemData, RpgItemData> <>9__24_0;
		}
	}
}
