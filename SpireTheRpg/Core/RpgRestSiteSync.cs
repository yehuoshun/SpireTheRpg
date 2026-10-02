using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Runs;
using SpireTheRpg.Combat;
using SpireTheRpg.Ui;

namespace SpireTheRpg.Core
{
	// Token: 0x02000055 RID: 85
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgRestSiteSync
	{
		// Token: 0x06000350 RID: 848 RVA: 0x000169B8 File Offset: 0x00014BB8
		[return: TupleElementNames(new string[]
		{
			"Ok",
			"Result"
		})]
		[return: Nullable(new byte[]
		{
			1,
			0,
			2
		})]
		private static Task<ValueTuple<bool, RpgItemData>> ApplyIntensifyAsync(Player owner, [Nullable(2)] string instanceId)
		{
			RpgRestSiteSync.<ApplyIntensifyAsync>d__4 <ApplyIntensifyAsync>d__;
			<ApplyIntensifyAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ValueTuple<bool, RpgItemData>>.Create();
			<ApplyIntensifyAsync>d__.owner = owner;
			<ApplyIntensifyAsync>d__.instanceId = instanceId;
			<ApplyIntensifyAsync>d__.<>1__state = -1;
			<ApplyIntensifyAsync>d__.<>t__builder.Start<RpgRestSiteSync.<ApplyIntensifyAsync>d__4>(ref <ApplyIntensifyAsync>d__);
			return <ApplyIntensifyAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00016A04 File Offset: 0x00014C04
		private static Task<bool> ApplyIntensifiedItemAsync(Player owner, RpgItemData upgraded)
		{
			RpgRestSiteSync.<ApplyIntensifiedItemAsync>d__5 <ApplyIntensifiedItemAsync>d__;
			<ApplyIntensifiedItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ApplyIntensifiedItemAsync>d__.owner = owner;
			<ApplyIntensifiedItemAsync>d__.upgraded = upgraded;
			<ApplyIntensifiedItemAsync>d__.<>1__state = -1;
			<ApplyIntensifiedItemAsync>d__.<>t__builder.Start<RpgRestSiteSync.<ApplyIntensifiedItemAsync>d__5>(ref <ApplyIntensifiedItemAsync>d__);
			return <ApplyIntensifiedItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00016A50 File Offset: 0x00014C50
		public static Task<bool> RunIntensify(Player owner)
		{
			RpgRestSiteSync.<RunIntensify>d__6 <RunIntensify>d__;
			<RunIntensify>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<RunIntensify>d__.owner = owner;
			<RunIntensify>d__.<>1__state = -1;
			<RunIntensify>d__.<>t__builder.Start<RpgRestSiteSync.<RunIntensify>d__6>(ref <RunIntensify>d__);
			return <RunIntensify>d__.<>t__builder.Task;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00016A94 File Offset: 0x00014C94
		public static Task<bool> RunFusion(Player owner)
		{
			RpgRestSiteSync.<RunFusion>d__7 <RunFusion>d__;
			<RunFusion>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<RunFusion>d__.owner = owner;
			<RunFusion>d__.<>1__state = -1;
			<RunFusion>d__.<>t__builder.Start<RpgRestSiteSync.<RunFusion>d__7>(ref <RunFusion>d__);
			return <RunFusion>d__.<>t__builder.Task;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00016AD8 File Offset: 0x00014CD8
		public static Task<bool> RunCleanse(Player owner)
		{
			RpgRestSiteSync.<RunCleanse>d__8 <RunCleanse>d__;
			<RunCleanse>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<RunCleanse>d__.owner = owner;
			<RunCleanse>d__.<>1__state = -1;
			<RunCleanse>d__.<>t__builder.Start<RpgRestSiteSync.<RunCleanse>d__8>(ref <RunCleanse>d__);
			return <RunCleanse>d__.<>t__builder.Task;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00016B1C File Offset: 0x00014D1C
		[return: TupleElementNames(new string[]
		{
			"Ok",
			"Result"
		})]
		[return: Nullable(new byte[]
		{
			1,
			0,
			2
		})]
		private static Task<ValueTuple<bool, RpgItemData>> ApplyCleanseAsync(Player owner, [Nullable(2)] string instanceId)
		{
			RpgRestSiteSync.<ApplyCleanseAsync>d__9 <ApplyCleanseAsync>d__;
			<ApplyCleanseAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ValueTuple<bool, RpgItemData>>.Create();
			<ApplyCleanseAsync>d__.owner = owner;
			<ApplyCleanseAsync>d__.instanceId = instanceId;
			<ApplyCleanseAsync>d__.<>1__state = -1;
			<ApplyCleanseAsync>d__.<>t__builder.Start<RpgRestSiteSync.<ApplyCleanseAsync>d__9>(ref <ApplyCleanseAsync>d__);
			return <ApplyCleanseAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00016B68 File Offset: 0x00014D68
		private static Task<bool> ApplyCleansedItemAsync(Player owner, RpgItemData item)
		{
			RpgRestSiteSync.<ApplyCleansedItemAsync>d__10 <ApplyCleansedItemAsync>d__;
			<ApplyCleansedItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ApplyCleansedItemAsync>d__.owner = owner;
			<ApplyCleansedItemAsync>d__.item = item;
			<ApplyCleansedItemAsync>d__.<>1__state = -1;
			<ApplyCleansedItemAsync>d__.<>t__builder.Start<RpgRestSiteSync.<ApplyCleansedItemAsync>d__10>(ref <ApplyCleansedItemAsync>d__);
			return <ApplyCleansedItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00016BB4 File Offset: 0x00014DB4
		private static bool ApplyFusion(Player owner, IReadOnlyList<string> materialIds, int seed)
		{
			if (materialIds.Count != 5)
			{
				return false;
			}
			ulong netId = owner.NetId;
			List<RpgItemData> list = new List<RpgItemData>();
			using (IEnumerator<string> enumerator = materialIds.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string id = enumerator.Current;
					RpgItemData rpgItemData = RpgActiveLoadout.AllItems(owner).FirstOrDefault((RpgItemData x) => string.Equals(x.InstanceId, id, StringComparison.Ordinal));
					if (rpgItemData == null)
					{
						return false;
					}
					list.Add(rpgItemData);
				}
			}
			RpgItemData rpgItemData2;
			if (!RpgItemFusion.TryFuse(list, RpgRng.FromSeed((ulong)seed), out rpgItemData2) || rpgItemData2 == null)
			{
				return false;
			}
			rpgItemData2.ClearCurses();
			rpgItemData2.IsIdentified = true;
			rpgItemData2.HiddenAbility.Clear();
			if (RpgEffectiveConfig.EnableMetaEquipment)
			{
				if (!RpgLoadout.TryRemoveManyByInstanceIds(materialIds, true))
				{
					return false;
				}
				RpgLoadout.AddToBag(rpgItemData2, true);
				RpgRestSiteForgeVfx.RememberItem(rpgItemData2);
				return true;
			}
			else
			{
				if (!RpgRunLoadout.TryRemoveManyByInstanceIds(netId, materialIds))
				{
					return false;
				}
				RpgRunLoadout.AddToBag(netId, rpgItemData2);
				RpgRestSiteForgeVfx.RememberItem(rpgItemData2);
				return true;
			}
			bool result;
			return result;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00016CB4 File Offset: 0x00014EB4
		[NullableContext(2)]
		private static bool IsMultiplayer(out PlayerChoiceSynchronizer sync)
		{
			sync = null;
			bool result;
			try
			{
				RunManager instance = RunManager.Instance;
				if (((instance != null) ? instance.NetService : null) == null || instance.NetService.Type == 1)
				{
					result = false;
				}
				else
				{
					sync = instance.PlayerChoiceSynchronizer;
					result = (sync != null);
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00016D10 File Offset: 0x00014F10
		private static PlayerChoiceResult EncodeItemResult(int magic, bool ok, [Nullable(2)] RpgItemData item)
		{
			List<int> list = new List<int>();
			list.Add(magic);
			list.Add((ok > false) ? 1 : 0);
			string value = (ok && item != null) ? JsonSerializer.Serialize<RpgItemData>(item, RpgRestSiteSync.JsonOptions) : "";
			RpgRestSiteSync.EncodePackedString(list, value);
			return PlayerChoiceResult.FromIndexes(list);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00016D58 File Offset: 0x00014F58
		private static bool TryDecodeItemResult(PlayerChoiceResult remote, int magic, out bool ok, [Nullable(2)] out RpgItemData item)
		{
			ok = false;
			item = null;
			List<int> list = remote.AsIndexes();
			if (list == null || list.Count < 3 || list[0] != magic)
			{
				return false;
			}
			ok = (list[1] != 0);
			string text;
			if (!RpgRestSiteSync.TryDecodePackedString(list, 2, out text))
			{
				string value;
				return RpgRestSiteSync.TryDecodeString(list, 2, out value) && (!ok || string.IsNullOrWhiteSpace(value));
			}
			if (!ok || string.IsNullOrWhiteSpace(text))
			{
				return true;
			}
			item = JsonSerializer.Deserialize<RpgItemData>(text, RpgRestSiteSync.JsonOptions);
			return item != null;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00016DE0 File Offset: 0x00014FE0
		private static PlayerChoiceResult EncodeFusion(bool ok, int seed, IReadOnlyList<string> ids)
		{
			List<int> list = new List<int>
			{
				1380992838,
				(ok > false) ? 1 : 0,
				seed,
				ids.Count
			};
			foreach (string value in ids)
			{
				RpgRestSiteSync.EncodeString(list, value);
			}
			return PlayerChoiceResult.FromIndexes(list);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00016E5C File Offset: 0x0001505C
		private static bool TryDecodeFusion(PlayerChoiceResult remote, out bool ok, out int seed, out List<string> ids)
		{
			ok = false;
			seed = 0;
			ids = new List<string>();
			List<int> list = remote.AsIndexes();
			if (list == null || list.Count < 4 || list[0] != 1380992838)
			{
				return false;
			}
			ok = (list[1] != 0);
			seed = list[2];
			int num = list[3];
			int start = 4;
			for (int i = 0; i < num; i++)
			{
				string item;
				if (!RpgRestSiteSync.TryDecodeString(list, start, out item, out start))
				{
					return false;
				}
				ids.Add(item);
			}
			return true;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00016EE0 File Offset: 0x000150E0
		private static void EncodePackedString(List<int> list, string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			list.Add(bytes.Length);
			for (int i = 0; i < bytes.Length; i += 4)
			{
				int num = (int)bytes[i];
				if (i + 1 < bytes.Length)
				{
					num |= (int)bytes[i + 1] << 8;
				}
				if (i + 2 < bytes.Length)
				{
					num |= (int)bytes[i + 2] << 16;
				}
				if (i + 3 < bytes.Length)
				{
					num |= (int)bytes[i + 3] << 24;
				}
				list.Add(num);
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00016F54 File Offset: 0x00015154
		private static bool TryDecodePackedString(IReadOnlyList<int> indexes, int start, out string value)
		{
			value = "";
			if (start >= indexes.Count)
			{
				return false;
			}
			int num = indexes[start];
			if (num < 0 || num > 2097152)
			{
				return false;
			}
			int num2 = (num + 3) / 4;
			if (start + 1 + num2 > indexes.Count)
			{
				return false;
			}
			byte[] array = new byte[num];
			for (int i = 0; i < num; i++)
			{
				int num3 = indexes[start + 1 + i / 4];
				array[i] = (byte)(num3 >> i % 4 * 8 & 255);
			}
			value = Encoding.UTF8.GetString(array);
			return true;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00016FE4 File Offset: 0x000151E4
		private static void EncodeString(List<int> list, string value)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			list.Add(bytes.Length);
			foreach (byte item in bytes)
			{
				list.Add((int)item);
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00017024 File Offset: 0x00015224
		private static bool TryDecodeString(IReadOnlyList<int> indexes, int start, out string value)
		{
			int num;
			return RpgRestSiteSync.TryDecodeString(indexes, start, out value, out num);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001703C File Offset: 0x0001523C
		private static bool TryDecodeString(IReadOnlyList<int> indexes, int start, out string value, out int next)
		{
			value = "";
			next = start;
			if (start >= indexes.Count)
			{
				return false;
			}
			int num = indexes[start];
			if (num < 0 || start + 1 + num > indexes.Count)
			{
				return false;
			}
			byte[] array = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = (byte)indexes[start + 1 + i];
			}
			value = Encoding.UTF8.GetString(array);
			next = start + 1 + num;
			return true;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000170B0 File Offset: 0x000152B0
		// Note: this type is marked as 'beforefieldinit'.
		static RpgRestSiteSync()
		{
		}

		// Token: 0x04000112 RID: 274
		private const int IntensifyMagic = 1380992841;

		// Token: 0x04000113 RID: 275
		private const int FusionMagic = 1380992838;

		// Token: 0x04000114 RID: 276
		private const int CleanseMagic = 1380992835;

		// Token: 0x04000115 RID: 277
		private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			Converters = 
			{
				new JsonStringEnumConverter()
			}
		};

		// Token: 0x02000114 RID: 276
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060007A7 RID: 1959 RVA: 0x00027AA2 File Offset: 0x00025CA2
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060007A8 RID: 1960 RVA: 0x00027AAE File Offset: 0x00025CAE
			public <>c()
			{
			}

			// Token: 0x060007A9 RID: 1961 RVA: 0x00027AB6 File Offset: 0x00025CB6
			internal bool <RunCleanse>b__8_0(RpgItemData x)
			{
				return x.HasCurse || x.IsBound;
			}

			// Token: 0x060007AA RID: 1962 RVA: 0x00027AC8 File Offset: 0x00025CC8
			internal bool <RunCleanse>b__8_1(RpgItemData x)
			{
				return x.HasCurse || x.IsBound;
			}

			// Token: 0x040003F9 RID: 1017
			[Nullable(0)]
			public static readonly RpgRestSiteSync.<>c <>9 = new RpgRestSiteSync.<>c();

			// Token: 0x040003FA RID: 1018
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Func<RpgItemData, bool> <>9__8_0;

			// Token: 0x040003FB RID: 1019
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Func<RpgItemData, bool> <>9__8_1;
		}

		// Token: 0x02000115 RID: 277
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0
		{
			// Token: 0x060007AB RID: 1963 RVA: 0x00027ADA File Offset: 0x00025CDA
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x060007AC RID: 1964 RVA: 0x00027AE2 File Offset: 0x00025CE2
			[NullableContext(0)]
			internal bool <ApplyFusion>b__0(RpgItemData x)
			{
				return string.Equals(x.InstanceId, this.id, StringComparison.Ordinal);
			}

			// Token: 0x040003FC RID: 1020
			[Nullable(0)]
			public string id;
		}

		// Token: 0x02000116 RID: 278
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x060007AD RID: 1965 RVA: 0x00027AF6 File Offset: 0x00025CF6
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x060007AE RID: 1966 RVA: 0x00027AFE File Offset: 0x00025CFE
			[NullableContext(0)]
			internal bool <ApplyIntensifyAsync>b__0(RpgItemData x)
			{
				return string.Equals(x.InstanceId, this.instanceId, StringComparison.Ordinal);
			}

			// Token: 0x040003FD RID: 1021
			[Nullable(0)]
			public string instanceId;
		}

		// Token: 0x02000117 RID: 279
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0
		{
			// Token: 0x060007AF RID: 1967 RVA: 0x00027B12 File Offset: 0x00025D12
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x060007B0 RID: 1968 RVA: 0x00027B1A File Offset: 0x00025D1A
			[NullableContext(0)]
			internal bool <ApplyCleanseAsync>b__0(RpgItemData x)
			{
				return string.Equals(x.InstanceId, this.instanceId, StringComparison.Ordinal);
			}

			// Token: 0x040003FE RID: 1022
			[Nullable(0)]
			public string instanceId;
		}

		// Token: 0x02000118 RID: 280
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ApplyCleanseAsync>d__9 : IAsyncStateMachine
		{
			// Token: 0x060007B1 RID: 1969 RVA: 0x00027B30 File Offset: 0x00025D30
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ValueTuple<bool, RpgItemData> result;
				try
				{
					TaskAwaiter<bool> awaiter;
					if (num != 0)
					{
						RpgRestSiteSync.<>c__DisplayClass9_0 CS$<>8__locals1 = new RpgRestSiteSync.<>c__DisplayClass9_0();
						CS$<>8__locals1.instanceId = this.instanceId;
						if (string.IsNullOrWhiteSpace(CS$<>8__locals1.instanceId))
						{
							result = new ValueTuple<bool, RpgItemData>(false, null);
							goto IL_16B;
						}
						this.<item>5__2 = RpgActiveLoadout.AllItems(this.owner).FirstOrDefault(new Func<RpgItemData, bool>(CS$<>8__locals1.<ApplyCleanseAsync>b__0));
						if (this.<item>5__2 == null || (!this.<item>5__2.HasCurse && !this.<item>5__2.IsBound))
						{
							result = new ValueTuple<bool, RpgItemData>(false, null);
							goto IL_16B;
						}
						if (!RpgCurseSystem.TryCleanse(this.<item>5__2))
						{
							result = new ValueTuple<bool, RpgItemData>(false, null);
							goto IL_16B;
						}
						if (!RpgActiveLoadout.TryReplaceByInstanceId(this.<item>5__2.InstanceId, this.<item>5__2))
						{
							result = new ValueTuple<bool, RpgItemData>(false, null);
							goto IL_16B;
						}
						awaiter = RpgHpCost.PayNonLethal(this.owner, RpgEffectiveConfig.CleanseHpCost).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgRestSiteSync.<ApplyCleanseAsync>d__9>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					RpgRestSiteForgeVfx.RememberItem(this.<item>5__2);
					result = new ValueTuple<bool, RpgItemData>(true, this.<item>5__2.Clone());
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<item>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_16B:
				this.<>1__state = -2;
				this.<item>5__2 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060007B2 RID: 1970 RVA: 0x00027CE0 File Offset: 0x00025EE0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040003FF RID: 1023
			public int <>1__state;

			// Token: 0x04000400 RID: 1024
			[TupleElementNames(new string[]
			{
				"Ok",
				"Result"
			})]
			[Nullable(new byte[]
			{
				0,
				0,
				2
			})]
			public AsyncTaskMethodBuilder<ValueTuple<bool, RpgItemData>> <>t__builder;

			// Token: 0x04000401 RID: 1025
			[Nullable(0)]
			public string instanceId;

			// Token: 0x04000402 RID: 1026
			[Nullable(0)]
			public Player owner;

			// Token: 0x04000403 RID: 1027
			[Nullable(0)]
			private RpgItemData <item>5__2;

			// Token: 0x04000404 RID: 1028
			[Nullable(0)]
			private TaskAwaiter<bool> <>u__1;
		}

		// Token: 0x02000119 RID: 281
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ApplyCleansedItemAsync>d__10 : IAsyncStateMachine
		{
			// Token: 0x060007B3 RID: 1971 RVA: 0x00027CF0 File Offset: 0x00025EF0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result;
				try
				{
					TaskAwaiter<bool> awaiter;
					if (num != 0)
					{
						if (string.IsNullOrWhiteSpace(this.item.InstanceId))
						{
							result = false;
							goto IL_CB;
						}
						if (!RpgActiveLoadout.TryReplaceByInstanceId(this.item.InstanceId, this.item))
						{
							result = false;
							goto IL_CB;
						}
						awaiter = RpgHpCost.PayNonLethal(this.owner, RpgEffectiveConfig.CleanseHpCost).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgRestSiteSync.<ApplyCleansedItemAsync>d__10>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					RpgRestSiteForgeVfx.RememberItem(this.item);
					result = true;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_CB:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060007B4 RID: 1972 RVA: 0x00027DEC File Offset: 0x00025FEC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000405 RID: 1029
			public int <>1__state;

			// Token: 0x04000406 RID: 1030
			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000407 RID: 1031
			[Nullable(0)]
			public RpgItemData item;

			// Token: 0x04000408 RID: 1032
			[Nullable(0)]
			public Player owner;

			// Token: 0x04000409 RID: 1033
			[Nullable(0)]
			private TaskAwaiter<bool> <>u__1;
		}

		// Token: 0x0200011A RID: 282
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ApplyIntensifiedItemAsync>d__5 : IAsyncStateMachine
		{
			// Token: 0x060007B5 RID: 1973 RVA: 0x00027DFC File Offset: 0x00025FFC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result;
				try
				{
					TaskAwaiter<bool> awaiter;
					if (num != 0)
					{
						if (string.IsNullOrWhiteSpace(this.upgraded.InstanceId))
						{
							result = false;
							goto IL_F9;
						}
						if (!(RpgEffectiveConfig.EnableMetaEquipment ? RpgLoadout.TryReplaceByInstanceId(this.upgraded.InstanceId, this.upgraded, true) : RpgRunLoadout.TryReplaceByInstanceId(this.owner.NetId, this.upgraded.InstanceId, this.upgraded)))
						{
							result = false;
							goto IL_F9;
						}
						awaiter = RpgHpCost.PayNonLethal(this.owner, RpgEffectiveConfig.IntensifyHpCost).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgRestSiteSync.<ApplyIntensifiedItemAsync>d__5>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					RpgRestSiteForgeVfx.RememberItem(this.upgraded);
					result = true;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_F9:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060007B6 RID: 1974 RVA: 0x00027F28 File Offset: 0x00026128
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400040A RID: 1034
			public int <>1__state;

			// Token: 0x0400040B RID: 1035
			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x0400040C RID: 1036
			[Nullable(0)]
			public RpgItemData upgraded;

			// Token: 0x0400040D RID: 1037
			[Nullable(0)]
			public Player owner;

			// Token: 0x0400040E RID: 1038
			[Nullable(0)]
			private TaskAwaiter<bool> <>u__1;
		}

		// Token: 0x0200011B RID: 283
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ApplyIntensifyAsync>d__4 : IAsyncStateMachine
		{
			// Token: 0x060007B7 RID: 1975 RVA: 0x00027F38 File Offset: 0x00026138
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ValueTuple<bool, RpgItemData> result;
				try
				{
					TaskAwaiter<bool> awaiter;
					if (num != 0)
					{
						RpgRestSiteSync.<>c__DisplayClass4_0 CS$<>8__locals1 = new RpgRestSiteSync.<>c__DisplayClass4_0();
						CS$<>8__locals1.instanceId = this.instanceId;
						if (string.IsNullOrWhiteSpace(CS$<>8__locals1.instanceId))
						{
							result = new ValueTuple<bool, RpgItemData>(false, null);
							goto IL_16D;
						}
						ulong netId = this.owner.NetId;
						RpgItemData rpgItemData = RpgActiveLoadout.AllItems(this.owner).FirstOrDefault(new Func<RpgItemData, bool>(CS$<>8__locals1.<ApplyIntensifyAsync>b__0));
						if (rpgItemData == null)
						{
							result = new ValueTuple<bool, RpgItemData>(false, null);
							goto IL_16D;
						}
						if (!rpgItemData.IsIdentified)
						{
							rpgItemData.Identify();
						}
						this.<upgraded>5__2 = RpgItemIntensify.Apply(rpgItemData);
						if (!(RpgEffectiveConfig.EnableMetaEquipment ? RpgLoadout.TryReplaceByInstanceId(CS$<>8__locals1.instanceId, this.<upgraded>5__2, true) : RpgRunLoadout.TryReplaceByInstanceId(netId, CS$<>8__locals1.instanceId, this.<upgraded>5__2)))
						{
							result = new ValueTuple<bool, RpgItemData>(false, null);
							goto IL_16D;
						}
						awaiter = RpgHpCost.PayNonLethal(this.owner, RpgEffectiveConfig.IntensifyHpCost).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgRestSiteSync.<ApplyIntensifyAsync>d__4>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					RpgRestSiteForgeVfx.RememberItem(this.<upgraded>5__2);
					result = new ValueTuple<bool, RpgItemData>(true, this.<upgraded>5__2);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<upgraded>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_16D:
				this.<>1__state = -2;
				this.<upgraded>5__2 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060007B8 RID: 1976 RVA: 0x000280EC File Offset: 0x000262EC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400040F RID: 1039
			public int <>1__state;

			// Token: 0x04000410 RID: 1040
			[TupleElementNames(new string[]
			{
				"Ok",
				"Result"
			})]
			[Nullable(new byte[]
			{
				0,
				0,
				2
			})]
			public AsyncTaskMethodBuilder<ValueTuple<bool, RpgItemData>> <>t__builder;

			// Token: 0x04000411 RID: 1041
			[Nullable(0)]
			public string instanceId;

			// Token: 0x04000412 RID: 1042
			[Nullable(0)]
			public Player owner;

			// Token: 0x04000413 RID: 1043
			[Nullable(0)]
			private RpgItemData <upgraded>5__2;

			// Token: 0x04000414 RID: 1044
			[Nullable(0)]
			private TaskAwaiter<bool> <>u__1;
		}

		// Token: 0x0200011C RID: 284
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <RunCleanse>d__8 : IAsyncStateMachine
		{
			// Token: 0x060007B9 RID: 1977 RVA: 0x000280FC File Offset: 0x000262FC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result2;
				try
				{
					TaskAwaiter<RpgIntensifyScreen.PickResult> awaiter;
					TaskAwaiter<ValueTuple<bool, RpgItemData>> awaiter2;
					TaskAwaiter<PlayerChoiceResult> awaiter3;
					TaskAwaiter<bool> awaiter4;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<RpgIntensifyScreen.PickResult>);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(TaskAwaiter<ValueTuple<bool, RpgItemData>>);
						this.<>1__state = -1;
						goto IL_13F;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<RpgIntensifyScreen.PickResult>);
						this.<>1__state = -1;
						goto IL_1FE;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(TaskAwaiter<ValueTuple<bool, RpgItemData>>);
						this.<>1__state = -1;
						goto IL_279;
					case 4:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(TaskAwaiter<PlayerChoiceResult>);
						this.<>1__state = -1;
						goto IL_324;
					case 5:
						awaiter4 = this.<>u__4;
						this.<>u__4 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_3BC;
					default:
						if (!RpgRestSiteSync.IsMultiplayer(out this.<sync>5__2) || this.<sync>5__2 == null)
						{
							awaiter = RpgIntensifyScreen.OfferPickAsync(this.owner, new Func<RpgItemData, bool>(RpgRestSiteSync.<>c.<>9.<RunCleanse>b__8_0), new RpgIntensifyScreen.PickChrome?(RpgIntensifyScreen.CleanseChrome), null).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgIntensifyScreen.PickResult>, RpgRestSiteSync.<RunCleanse>d__8>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							this.<choiceId>5__3 = this.<sync>5__2.ReserveChoiceId(this.owner);
							if (RpgNet.IsLocal(this.owner))
							{
								awaiter = RpgIntensifyScreen.OfferPickAsync(this.owner, new Func<RpgItemData, bool>(RpgRestSiteSync.<>c.<>9.<RunCleanse>b__8_1), new RpgIntensifyScreen.PickChrome?(RpgIntensifyScreen.CleanseChrome), null).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 2;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgIntensifyScreen.PickResult>, RpgRestSiteSync.<RunCleanse>d__8>(ref awaiter, ref this);
									return;
								}
								goto IL_1FE;
							}
							else
							{
								awaiter3 = this.<sync>5__2.WaitForRemoteChoice(this.owner, this.<choiceId>5__3).GetAwaiter();
								if (!awaiter3.IsCompleted)
								{
									this.<>1__state = 4;
									this.<>u__3 = awaiter3;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<PlayerChoiceResult>, RpgRestSiteSync.<RunCleanse>d__8>(ref awaiter3, ref this);
									return;
								}
								goto IL_324;
							}
						}
						break;
					}
					RpgIntensifyScreen.PickResult result = awaiter.GetResult();
					if (!result.Ok)
					{
						result2 = false;
						goto IL_3EA;
					}
					awaiter2 = RpgRestSiteSync.ApplyCleanseAsync(this.owner, result.InstanceId).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<ValueTuple<bool, RpgItemData>>, RpgRestSiteSync.<RunCleanse>d__8>(ref awaiter2, ref this);
						return;
					}
					IL_13F:
					result2 = awaiter2.GetResult().Item1;
					goto IL_3EA;
					IL_1FE:
					RpgIntensifyScreen.PickResult result3 = awaiter.GetResult();
					RpgItemData item = null;
					bool flag = false;
					if (!result3.Ok)
					{
						goto IL_28F;
					}
					awaiter2 = RpgRestSiteSync.ApplyCleanseAsync(this.owner, result3.InstanceId).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<ValueTuple<bool, RpgItemData>>, RpgRestSiteSync.<RunCleanse>d__8>(ref awaiter2, ref this);
						return;
					}
					IL_279:
					ValueTuple<bool, RpgItemData> result4 = awaiter2.GetResult();
					flag = result4.Item1;
					item = result4.Item2;
					IL_28F:
					this.<sync>5__2.SyncLocalChoice(this.owner, this.<choiceId>5__3, RpgRestSiteSync.EncodeItemResult(1380992835, flag, item));
					result2 = flag;
					goto IL_3EA;
					IL_324:
					bool flag2;
					RpgItemData rpgItemData;
					if (!RpgRestSiteSync.TryDecodeItemResult(awaiter3.GetResult(), 1380992835, out flag2, out rpgItemData))
					{
						SpireTheRpgMainFile.Logger.Info("RpgRestSiteSync cleanse remote decode failed", 1);
						result2 = false;
						goto IL_3EA;
					}
					bool flag3 = flag2 && rpgItemData != null;
					if (!flag3)
					{
						goto IL_3C5;
					}
					awaiter4 = RpgRestSiteSync.ApplyCleansedItemAsync(this.owner, rpgItemData).GetAwaiter();
					if (!awaiter4.IsCompleted)
					{
						this.<>1__state = 5;
						this.<>u__4 = awaiter4;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgRestSiteSync.<RunCleanse>d__8>(ref awaiter4, ref this);
						return;
					}
					IL_3BC:
					flag3 = awaiter4.GetResult();
					IL_3C5:
					result2 = flag3;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<sync>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_3EA:
				this.<>1__state = -2;
				this.<sync>5__2 = null;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x060007BA RID: 1978 RVA: 0x0002852C File Offset: 0x0002672C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000415 RID: 1045
			public int <>1__state;

			// Token: 0x04000416 RID: 1046
			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000417 RID: 1047
			[Nullable(0)]
			public Player owner;

			// Token: 0x04000418 RID: 1048
			[Nullable(0)]
			private PlayerChoiceSynchronizer <sync>5__2;

			// Token: 0x04000419 RID: 1049
			private uint <choiceId>5__3;

			// Token: 0x0400041A RID: 1050
			[Nullable(0)]
			private TaskAwaiter<RpgIntensifyScreen.PickResult> <>u__1;

			// Token: 0x0400041B RID: 1051
			[TupleElementNames(new string[]
			{
				"Ok",
				"Result"
			})]
			[Nullable(new byte[]
			{
				0,
				0,
				2
			})]
			private TaskAwaiter<ValueTuple<bool, RpgItemData>> <>u__2;

			// Token: 0x0400041C RID: 1052
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private TaskAwaiter<PlayerChoiceResult> <>u__3;

			// Token: 0x0400041D RID: 1053
			[Nullable(0)]
			private TaskAwaiter<bool> <>u__4;
		}

		// Token: 0x0200011D RID: 285
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <RunFusion>d__7 : IAsyncStateMachine
		{
			// Token: 0x060007BB RID: 1979 RVA: 0x0002853C File Offset: 0x0002673C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result2;
				try
				{
					TaskAwaiter<RpgFusionScreen.PickResult> awaiter;
					TaskAwaiter<PlayerChoiceResult> awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<RpgFusionScreen.PickResult>);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<RpgFusionScreen.PickResult>);
						this.<>1__state = -1;
						goto IL_144;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(TaskAwaiter<PlayerChoiceResult>);
						this.<>1__state = -1;
						goto IL_27E;
					default:
						if (!RpgRestSiteSync.IsMultiplayer(out this.<sync>5__2) || this.<sync>5__2 == null)
						{
							awaiter = RpgFusionScreen.OfferPickAsync(this.owner).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgFusionScreen.PickResult>, RpgRestSiteSync.<RunFusion>d__7>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							this.<choiceId>5__3 = this.<sync>5__2.ReserveChoiceId(this.owner);
							if (RpgNet.IsLocal(this.owner))
							{
								awaiter = RpgFusionScreen.OfferPickAsync(this.owner).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgFusionScreen.PickResult>, RpgRestSiteSync.<RunFusion>d__7>(ref awaiter, ref this);
									return;
								}
								goto IL_144;
							}
							else
							{
								awaiter2 = this.<sync>5__2.WaitForRemoteChoice(this.owner, this.<choiceId>5__3).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									this.<>1__state = 2;
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<PlayerChoiceResult>, RpgRestSiteSync.<RunFusion>d__7>(ref awaiter2, ref this);
									return;
								}
								goto IL_27E;
							}
						}
						break;
					}
					RpgFusionScreen.PickResult result = awaiter.GetResult();
					result2 = (result.Ok && RpgRestSiteSync.ApplyFusion(this.owner, result.MaterialIds, result.Seed));
					goto IL_34D;
					IL_144:
					RpgFusionScreen.PickResult result3 = awaiter.GetResult();
					bool flag = result3.Ok && RpgRestSiteSync.ApplyFusion(this.owner, result3.MaterialIds, result3.Seed);
					this.<sync>5__2.SyncLocalChoice(this.owner, this.<choiceId>5__3, RpgRestSiteSync.EncodeFusion(flag, result3.Seed, result3.MaterialIds));
					Logger logger = SpireTheRpgMainFile.Logger;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 3);
					defaultInterpolatedStringHandler.AppendLiteral("RpgRestSiteSync fusion local ok=");
					defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
					defaultInterpolatedStringHandler.AppendLiteral(" mats=");
					defaultInterpolatedStringHandler.AppendFormatted<int>(result3.MaterialIds.Count);
					defaultInterpolatedStringHandler.AppendLiteral(" net=");
					defaultInterpolatedStringHandler.AppendFormatted<ulong>(this.owner.NetId);
					logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
					result2 = flag;
					goto IL_34D;
					IL_27E:
					bool flag2;
					int seed;
					List<string> list;
					if (!RpgRestSiteSync.TryDecodeFusion(awaiter2.GetResult(), out flag2, out seed, out list))
					{
						SpireTheRpgMainFile.Logger.Info("RpgRestSiteSync fusion remote decode failed", 1);
						result2 = false;
					}
					else
					{
						bool flag3 = flag2 && RpgRestSiteSync.ApplyFusion(this.owner, list, seed);
						Logger logger2 = SpireTheRpgMainFile.Logger;
						defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 3);
						defaultInterpolatedStringHandler.AppendLiteral("RpgRestSiteSync fusion remote ok=");
						defaultInterpolatedStringHandler.AppendFormatted<bool>(flag3);
						defaultInterpolatedStringHandler.AppendLiteral(" mats=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
						defaultInterpolatedStringHandler.AppendLiteral(" net=");
						defaultInterpolatedStringHandler.AppendFormatted<ulong>(this.owner.NetId);
						logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
						result2 = flag3;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<sync>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_34D:
				this.<>1__state = -2;
				this.<sync>5__2 = null;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x060007BC RID: 1980 RVA: 0x000288D0 File Offset: 0x00026AD0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400041E RID: 1054
			public int <>1__state;

			// Token: 0x0400041F RID: 1055
			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000420 RID: 1056
			[Nullable(0)]
			public Player owner;

			// Token: 0x04000421 RID: 1057
			[Nullable(0)]
			private PlayerChoiceSynchronizer <sync>5__2;

			// Token: 0x04000422 RID: 1058
			private uint <choiceId>5__3;

			// Token: 0x04000423 RID: 1059
			[Nullable(0)]
			private TaskAwaiter<RpgFusionScreen.PickResult> <>u__1;

			// Token: 0x04000424 RID: 1060
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private TaskAwaiter<PlayerChoiceResult> <>u__2;
		}

		// Token: 0x0200011E RID: 286
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <RunIntensify>d__6 : IAsyncStateMachine
		{
			// Token: 0x060007BD RID: 1981 RVA: 0x000288E0 File Offset: 0x00026AE0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result2;
				try
				{
					TaskAwaiter<RpgIntensifyScreen.PickResult> awaiter;
					TaskAwaiter<ValueTuple<bool, RpgItemData>> awaiter2;
					TaskAwaiter<PlayerChoiceResult> awaiter3;
					TaskAwaiter<bool> awaiter4;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<RpgIntensifyScreen.PickResult>);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(TaskAwaiter<ValueTuple<bool, RpgItemData>>);
						this.<>1__state = -1;
						goto IL_121;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<RpgIntensifyScreen.PickResult>);
						this.<>1__state = -1;
						goto IL_1C2;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(TaskAwaiter<ValueTuple<bool, RpgItemData>>);
						this.<>1__state = -1;
						goto IL_24D;
					case 4:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(TaskAwaiter<PlayerChoiceResult>);
						this.<>1__state = -1;
						goto IL_377;
					case 5:
						awaiter4 = this.<>u__4;
						this.<>u__4 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_420;
					default:
						if (!RpgRestSiteSync.IsMultiplayer(out this.<sync>5__2) || this.<sync>5__2 == null)
						{
							awaiter = RpgIntensifyScreen.OfferPickAsync(this.owner, null, null, null).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgIntensifyScreen.PickResult>, RpgRestSiteSync.<RunIntensify>d__6>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							this.<choiceId>5__3 = this.<sync>5__2.ReserveChoiceId(this.owner);
							if (RpgNet.IsLocal(this.owner))
							{
								awaiter = RpgIntensifyScreen.OfferPickAsync(this.owner, null, null, null).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 2;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<RpgIntensifyScreen.PickResult>, RpgRestSiteSync.<RunIntensify>d__6>(ref awaiter, ref this);
									return;
								}
								goto IL_1C2;
							}
							else
							{
								awaiter3 = this.<sync>5__2.WaitForRemoteChoice(this.owner, this.<choiceId>5__3).GetAwaiter();
								if (!awaiter3.IsCompleted)
								{
									this.<>1__state = 4;
									this.<>u__3 = awaiter3;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<PlayerChoiceResult>, RpgRestSiteSync.<RunIntensify>d__6>(ref awaiter3, ref this);
									return;
								}
								goto IL_377;
							}
						}
						break;
					}
					RpgIntensifyScreen.PickResult result = awaiter.GetResult();
					if (!result.Ok)
					{
						result2 = false;
						goto IL_4CA;
					}
					awaiter2 = RpgRestSiteSync.ApplyIntensifyAsync(this.owner, result.InstanceId).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<ValueTuple<bool, RpgItemData>>, RpgRestSiteSync.<RunIntensify>d__6>(ref awaiter2, ref this);
						return;
					}
					IL_121:
					result2 = awaiter2.GetResult().Item1;
					goto IL_4CA;
					IL_1C2:
					RpgIntensifyScreen.PickResult result3 = awaiter.GetResult();
					this.<pick>5__5 = result3;
					RpgItemData rpgItemData = null;
					bool flag = false;
					if (!this.<pick>5__5.Ok)
					{
						goto IL_263;
					}
					awaiter2 = RpgRestSiteSync.ApplyIntensifyAsync(this.owner, this.<pick>5__5.InstanceId).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<ValueTuple<bool, RpgItemData>>, RpgRestSiteSync.<RunIntensify>d__6>(ref awaiter2, ref this);
						return;
					}
					IL_24D:
					ValueTuple<bool, RpgItemData> result4 = awaiter2.GetResult();
					flag = result4.Item1;
					rpgItemData = result4.Item2;
					IL_263:
					this.<sync>5__2.SyncLocalChoice(this.owner, this.<choiceId>5__3, RpgRestSiteSync.EncodeItemResult(1380992841, flag, rpgItemData));
					Logger logger = SpireTheRpgMainFile.Logger;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(44, 3);
					defaultInterpolatedStringHandler.AppendLiteral("RpgRestSiteSync intensify local ok=");
					defaultInterpolatedStringHandler.AppendFormatted<bool>(flag);
					defaultInterpolatedStringHandler.AppendLiteral(" id=");
					defaultInterpolatedStringHandler.AppendFormatted(((rpgItemData != null) ? rpgItemData.InstanceId : null) ?? this.<pick>5__5.InstanceId);
					defaultInterpolatedStringHandler.AppendLiteral(" net=");
					defaultInterpolatedStringHandler.AppendFormatted<ulong>(this.owner.NetId);
					logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
					result2 = flag;
					goto IL_4CA;
					IL_377:
					bool flag2;
					if (!RpgRestSiteSync.TryDecodeItemResult(awaiter3.GetResult(), 1380992841, out flag2, out this.<remoteItem>5__4))
					{
						SpireTheRpgMainFile.Logger.Info("RpgRestSiteSync intensify remote decode failed", 1);
						result2 = false;
						goto IL_4CA;
					}
					bool flag3 = flag2 && this.<remoteItem>5__4 != null;
					if (!flag3)
					{
						goto IL_429;
					}
					awaiter4 = RpgRestSiteSync.ApplyIntensifiedItemAsync(this.owner, this.<remoteItem>5__4).GetAwaiter();
					if (!awaiter4.IsCompleted)
					{
						this.<>1__state = 5;
						this.<>u__4 = awaiter4;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgRestSiteSync.<RunIntensify>d__6>(ref awaiter4, ref this);
						return;
					}
					IL_420:
					flag3 = awaiter4.GetResult();
					IL_429:
					bool flag4 = flag3;
					Logger logger2 = SpireTheRpgMainFile.Logger;
					defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(45, 3);
					defaultInterpolatedStringHandler.AppendLiteral("RpgRestSiteSync intensify remote ok=");
					defaultInterpolatedStringHandler.AppendFormatted<bool>(flag4);
					defaultInterpolatedStringHandler.AppendLiteral(" id=");
					RpgItemData rpgItemData2 = this.<remoteItem>5__4;
					defaultInterpolatedStringHandler.AppendFormatted((rpgItemData2 != null) ? rpgItemData2.InstanceId : null);
					defaultInterpolatedStringHandler.AppendLiteral(" net=");
					defaultInterpolatedStringHandler.AppendFormatted<ulong>(this.owner.NetId);
					logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
					result2 = flag4;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<sync>5__2 = null;
					this.<remoteItem>5__4 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_4CA:
				this.<>1__state = -2;
				this.<sync>5__2 = null;
				this.<remoteItem>5__4 = null;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x060007BE RID: 1982 RVA: 0x00028DF8 File Offset: 0x00026FF8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000425 RID: 1061
			public int <>1__state;

			// Token: 0x04000426 RID: 1062
			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000427 RID: 1063
			[Nullable(0)]
			public Player owner;

			// Token: 0x04000428 RID: 1064
			[Nullable(0)]
			private PlayerChoiceSynchronizer <sync>5__2;

			// Token: 0x04000429 RID: 1065
			private uint <choiceId>5__3;

			// Token: 0x0400042A RID: 1066
			[Nullable(0)]
			private RpgItemData <remoteItem>5__4;

			// Token: 0x0400042B RID: 1067
			[Nullable(0)]
			private TaskAwaiter<RpgIntensifyScreen.PickResult> <>u__1;

			// Token: 0x0400042C RID: 1068
			[TupleElementNames(new string[]
			{
				"Ok",
				"Result"
			})]
			[Nullable(new byte[]
			{
				0,
				0,
				2
			})]
			private TaskAwaiter<ValueTuple<bool, RpgItemData>> <>u__2;

			// Token: 0x0400042D RID: 1069
			private RpgIntensifyScreen.PickResult <pick>5__5;

			// Token: 0x0400042E RID: 1070
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private TaskAwaiter<PlayerChoiceResult> <>u__3;

			// Token: 0x0400042F RID: 1071
			[Nullable(0)]
			private TaskAwaiter<bool> <>u__4;
		}
	}
}
