using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Merchant;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Multiplayer.Game;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Screens.Shops;
using MegaCrit.Sts2.Core.Runs;
using SpireTheRpg.Combat;
using SpireTheRpg.Core;
using SpireTheRpg.Relics;

namespace SpireTheRpg.Patches
{
	// Token: 0x02000031 RID: 49
	[NullableContext(1)]
	[Nullable(0)]
	[HarmonyPatch]
	internal static class RpgShopRandomItemPatches
	{
		// Token: 0x060001AD RID: 429 RVA: 0x0000F96B File Offset: 0x0000DB6B
		[HarmonyPatch(typeof(MerchantInventory), "CreateForNormalMerchant")]
		[HarmonyPostfix]
		private static void CreateForNormalMerchantPostfix(Player player, MerchantInventory __result)
		{
			RpgShopRandomItemPatches.InstallEntry(__result, player);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000F974 File Offset: 0x0000DB74
		[HarmonyPatch(typeof(Hook), "ModifyMerchantPrice")]
		[HarmonyPostfix]
		private static void ModifyMerchantPricePostfix(MerchantEntry entry, ref decimal __result)
		{
			RpgRandomItemShopRelic rpgRandomItemShopRelic;
			if (!RpgShopRandomItemPatches.TryGetShopRelic(entry, out rpgRandomItemShopRelic))
			{
				return;
			}
			rpgRandomItemShopRelic.RefreshDisplayedPrice();
			__result = Math.Max(0, RpgEffectiveConfig.RandomItemShopPrice);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000F9A8 File Offset: 0x0000DBA8
		[HarmonyPatch(typeof(Hook), "ShouldRefillMerchantEntry")]
		[HarmonyPrefix]
		private static bool ShouldRefillMerchantEntryPrefix(MerchantEntry entry, ref bool __result)
		{
			if (!RpgShopRandomItemPatches.IsOurEntry(entry))
			{
				return true;
			}
			__result = true;
			return false;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000F9B8 File Offset: 0x0000DBB8
		[HarmonyPatch(typeof(NMerchantInventory), "Initialize")]
		[HarmonyPrefix]
		private static void MerchantInventoryInitializePrefix(NMerchantInventory __instance, MerchantInventory inventory)
		{
			if (__instance is NFakeMerchantInventory)
			{
				RpgShopRandomItemPatches.RemoveOurEntries(inventory);
				return;
			}
			RpgShopRandomItemPatches.InstallEntry(inventory, inventory.Player);
			RpgShopRandomItemPatches.EnsureRelicSlot(__instance, inventory);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000F9DC File Offset: 0x0000DBDC
		[HarmonyPatch(typeof(NMerchantInventory), "Initialize")]
		[HarmonyPostfix]
		private static void MerchantInventoryInitializePostfix(NMerchantInventory __instance, MerchantInventory inventory)
		{
			if (__instance is NFakeMerchantInventory)
			{
				return;
			}
			RpgShopRandomItemPatches.MoveCardRemoval(__instance, inventory);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		[return: Nullable(new byte[]
		{
			1,
			0
		})]
		private static Task<ValueTuple<bool, int>> Purchase(MerchantRelicEntry entry, MerchantInventory inventory, bool ignoreCost)
		{
			RpgShopRandomItemPatches.<Purchase>d__11 <Purchase>d__;
			<Purchase>d__.<>t__builder = AsyncTaskMethodBuilder<ValueTuple<bool, int>>.Create();
			<Purchase>d__.entry = entry;
			<Purchase>d__.inventory = inventory;
			<Purchase>d__.ignoreCost = ignoreCost;
			<Purchase>d__.<>1__state = -1;
			<Purchase>d__.<>t__builder.Start<RpgShopRandomItemPatches.<Purchase>d__11>(ref <Purchase>d__);
			return <Purchase>d__.<>t__builder.Task;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000FA44 File Offset: 0x0000DC44
		[return: Nullable(2)]
		private static RpgItemData RollItem(Player player, int purchaseCount)
		{
			IRunState runState = player.RunState;
			ulong c = (ulong)((runState != null) ? runState.TotalFloor : 0);
			ulong seed = RpgRng.Mix(RpgLootTable.RunSeedOf(player), player.NetId, c, (ulong)purchaseCount, RpgLootTable.TimeSalt(player) ^ 1397247824UL);
			return RpgLootTable.RollChoices(1, new int?(1), RpgRng.FromSeed(seed)).FirstOrDefault<RpgItemData>();
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000FAA0 File Offset: 0x0000DCA0
		private static void GrantItem(Player player, RpgItemData item)
		{
			if (RpgEffectiveConfig.EnableMetaEquipment)
			{
				RpgRunLoot.Add(player.NetId, item);
			}
			else
			{
				RpgRunLoadout.AddToBag(player.NetId, item);
			}
			RpgAccessoryCombatHooks.OnEquipmentGranted(item, player);
			if (!RpgNet.IsLocal(player))
			{
				return;
			}
			RpgItemRelic relic = player.GetRelic<RpgItemRelic>();
			if (relic != null)
			{
				relic.SyncAmountPublic();
			}
			if (relic != null)
			{
				relic.Flash();
			}
			try
			{
				NDebugAudioManager instance = NDebugAudioManager.Instance;
				if (instance != null)
				{
					instance.Play("relic_get.mp3", 1f, 0);
				}
				if (relic != null)
				{
					NRun instance2 = NRun.Instance;
					if (instance2 != null)
					{
						instance2.GlobalUi.RelicInventory.AnimateRelic(relic, null, null);
					}
				}
			}
			catch
			{
			}
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 3);
			defaultInterpolatedStringHandler.AppendLiteral("RpgShopRandomItem granted ");
			defaultInterpolatedStringHandler.AppendFormatted(item.Id);
			defaultInterpolatedStringHandler.AppendLiteral(" rarity=");
			defaultInterpolatedStringHandler.AppendFormatted<RpgItemRarity>(item.Rarity);
			defaultInterpolatedStringHandler.AppendLiteral(" net=");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(player.NetId);
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000FBC0 File Offset: 0x0000DDC0
		private static void TrySyncGoldLost(int cost)
		{
			try
			{
				RunManager instance = RunManager.Instance;
				INetGameService netGameService = (instance != null) ? instance.NetService : null;
				if (instance != null && netGameService != null && netGameService.IsConnected)
				{
					NetGameType type = netGameService.Type;
					bool flag = type - 2 <= 1;
					if (flag)
					{
						instance.RewardSynchronizer.SyncLocalGoldLost(cost);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000FC28 File Offset: 0x0000DE28
		private static void InstallEntry(MerchantInventory inventory, Player player)
		{
			IEnumerable<MerchantRelicEntry> relicEntries = inventory.RelicEntries;
			Func<MerchantRelicEntry, bool> predicate;
			if ((predicate = RpgShopRandomItemPatches.<>O.<0>__IsOurEntry) == null)
			{
				predicate = (RpgShopRandomItemPatches.<>O.<0>__IsOurEntry = new Func<MerchantRelicEntry, bool>(RpgShopRandomItemPatches.IsOurEntry));
			}
			if (relicEntries.Any(predicate))
			{
				return;
			}
			RpgRandomItemShopRelic rpgRandomItemShopRelic = (RpgRandomItemShopRelic)ModelDb.Relic<RpgRandomItemShopRelic>().ToMutable();
			rpgRandomItemShopRelic.RefreshDisplayedPrice();
			inventory.AddRelicEntry(new MerchantRelicEntry(rpgRandomItemShopRelic, player));
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000FC84 File Offset: 0x0000DE84
		private static void RemoveOurEntries(MerchantInventory inventory)
		{
			FieldInfo relicEntriesField = RpgShopRandomItemPatches.RelicEntriesField;
			List<MerchantRelicEntry> list = ((relicEntriesField != null) ? relicEntriesField.GetValue(inventory) : null) as List<MerchantRelicEntry>;
			if (list == null)
			{
				return;
			}
			List<MerchantRelicEntry> list2 = list;
			Predicate<MerchantRelicEntry> match;
			if ((match = RpgShopRandomItemPatches.<>O.<1>__IsOurEntry) == null)
			{
				match = (RpgShopRandomItemPatches.<>O.<1>__IsOurEntry = new Predicate<MerchantRelicEntry>(RpgShopRandomItemPatches.IsOurEntry));
			}
			list2.RemoveAll(match);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000FCD0 File Offset: 0x0000DED0
		[NullableContext(2)]
		private static bool IsOurEntry(MerchantEntry entry)
		{
			MerchantRelicEntry merchantRelicEntry = entry as MerchantRelicEntry;
			return merchantRelicEntry != null && merchantRelicEntry.Model is RpgRandomItemShopRelic;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000FCF8 File Offset: 0x0000DEF8
		private static bool TryGetShopRelic(MerchantEntry entry, out RpgRandomItemShopRelic shop)
		{
			shop = null;
			MerchantRelicEntry merchantRelicEntry = entry as MerchantRelicEntry;
			if (merchantRelicEntry != null)
			{
				RpgRandomItemShopRelic rpgRandomItemShopRelic = merchantRelicEntry.Model as RpgRandomItemShopRelic;
				if (rpgRandomItemShopRelic != null)
				{
					shop = rpgRandomItemShopRelic;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000FD28 File Offset: 0x0000DF28
		private static void EnsureRelicSlot(NMerchantInventory merchantInventory, MerchantInventory inventory)
		{
			IEnumerable<MerchantRelicEntry> relicEntries = inventory.RelicEntries;
			Func<MerchantRelicEntry, bool> predicate;
			if ((predicate = RpgShopRandomItemPatches.<>O.<0>__IsOurEntry) == null)
			{
				predicate = (RpgShopRandomItemPatches.<>O.<0>__IsOurEntry = new Func<MerchantRelicEntry, bool>(RpgShopRandomItemPatches.IsOurEntry));
			}
			if (!relicEntries.Any(predicate))
			{
				return;
			}
			Control nodeOrNull = merchantInventory.GetNodeOrNull<Control>("%Relics");
			if (nodeOrNull == null)
			{
				return;
			}
			List<NMerchantRelic> list = nodeOrNull.GetChildren(false).OfType<NMerchantRelic>().ToList<NMerchantRelic>();
			while (list.Count < inventory.RelicEntries.Count)
			{
				NMerchantRelic nmerchantRelic = list.LastOrDefault<NMerchantRelic>();
				if (nmerchantRelic == null)
				{
					break;
				}
				NMerchantRelic nmerchantRelic2 = nmerchantRelic.Duplicate(15) as NMerchantRelic;
				if (nmerchantRelic2 == null)
				{
					break;
				}
				Node node = nmerchantRelic2;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 2);
				defaultInterpolatedStringHandler.AppendFormatted<StringName>(nmerchantRelic.Name);
				defaultInterpolatedStringHandler.AppendLiteral("_RpgExtra");
				defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
				node.Name = defaultInterpolatedStringHandler.ToStringAndClear();
				nmerchantRelic2.Position = nmerchantRelic.Position + RpgShopRandomItemPatches.NextSlotOffset(list);
				nodeOrNull.AddChild(nmerchantRelic2, false, 0L);
				list.Add(nmerchantRelic2);
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000FE2C File Offset: 0x0000E02C
		private static Vector2 NextSlotOffset(IReadOnlyList<NMerchantRelic> slots)
		{
			if (slots.Count >= 2)
			{
				Vector2 result = slots[slots.Count - 1].Position - slots[slots.Count - 2].Position;
				if (result.LengthSquared() > 1f)
				{
					return result;
				}
			}
			return new Vector2(160f, 0f);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000FE90 File Offset: 0x0000E090
		private static void MoveCardRemoval(NMerchantInventory merchantInventory, MerchantInventory inventory)
		{
			IEnumerable<MerchantRelicEntry> relicEntries = inventory.RelicEntries;
			Func<MerchantRelicEntry, bool> predicate;
			if ((predicate = RpgShopRandomItemPatches.<>O.<0>__IsOurEntry) == null)
			{
				predicate = (RpgShopRandomItemPatches.<>O.<0>__IsOurEntry = new Func<MerchantRelicEntry, bool>(RpgShopRandomItemPatches.IsOurEntry));
			}
			if (!relicEntries.Any(predicate))
			{
				return;
			}
			Node nodeOrNull = merchantInventory.GetNodeOrNull("%MerchantCardRemoval");
			Control control = nodeOrNull as Control;
			if (control != null)
			{
				control.Position = RpgShopRandomItemPatches.OriginOf(control, control.Position) + new Vector2(0f, 60f);
				return;
			}
			Node2D node2D = nodeOrNull as Node2D;
			if (node2D != null)
			{
				node2D.Position = RpgShopRandomItemPatches.OriginOf(node2D, node2D.Position) + new Vector2(0f, 60f);
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000FF38 File Offset: 0x0000E138
		private static Vector2 OriginOf(GodotObject node, Vector2 current)
		{
			ulong instanceId = node.GetInstanceId();
			Vector2 vector;
			if (!RpgShopRandomItemPatches.CardRemovalOrigins.TryGetValue(instanceId, out vector))
			{
				vector = current;
				RpgShopRandomItemPatches.CardRemovalOrigins[instanceId] = vector;
			}
			return vector;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000FF6A File Offset: 0x0000E16A
		// Note: this type is marked as 'beforefieldinit'.
		static RpgShopRandomItemPatches()
		{
		}

		// Token: 0x04000096 RID: 150
		private const float CardRemovalOffsetY = 60f;

		// Token: 0x04000097 RID: 151
		[Nullable(2)]
		private static readonly FieldInfo RelicEntriesField = AccessTools.Field(typeof(MerchantInventory), "_relicEntries");

		// Token: 0x04000098 RID: 152
		private static readonly Dictionary<ulong, Vector2> CardRemovalOrigins = new Dictionary<ulong, Vector2>();

		// Token: 0x020000CF RID: 207
		[Nullable(0)]
		[HarmonyPatch]
		private static class PurchasePatch
		{
			// Token: 0x060006A6 RID: 1702 RVA: 0x000228C6 File Offset: 0x00020AC6
			private static MethodBase TargetMethod()
			{
				return AccessTools.DeclaredMethod(typeof(MerchantRelicEntry), "OnTryPurchase", null, null);
			}

			// Token: 0x060006A7 RID: 1703 RVA: 0x000228DE File Offset: 0x00020ADE
			private static bool Prefix(MerchantRelicEntry __instance, [Nullable(2)] MerchantInventory inventory, bool ignoreCost, [Nullable(new byte[]
			{
				1,
				0
			})] ref Task<ValueTuple<bool, int>> __result)
			{
				if (!RpgShopRandomItemPatches.IsOurEntry(__instance) || inventory == null)
				{
					return true;
				}
				__result = RpgShopRandomItemPatches.Purchase(__instance, inventory, ignoreCost);
				return false;
			}
		}

		// Token: 0x020000D0 RID: 208
		[Nullable(0)]
		[HarmonyPatch]
		private static class RestockPatch
		{
			// Token: 0x060006A8 RID: 1704 RVA: 0x000228F8 File Offset: 0x00020AF8
			private static MethodBase TargetMethod()
			{
				return AccessTools.DeclaredMethod(typeof(MerchantRelicEntry), "RestockAfterPurchase", null, null);
			}

			// Token: 0x060006A9 RID: 1705 RVA: 0x00022910 File Offset: 0x00020B10
			private static bool Prefix(MerchantRelicEntry __instance)
			{
				return !RpgShopRandomItemPatches.IsOurEntry(__instance);
			}
		}

		// Token: 0x020000D1 RID: 209
		[Nullable(0)]
		[HarmonyPatch]
		private static class SuccessfulPurchaseAnimPatch
		{
			// Token: 0x060006AA RID: 1706 RVA: 0x0002291B File Offset: 0x00020B1B
			private static MethodBase TargetMethod()
			{
				return AccessTools.DeclaredMethod(typeof(NMerchantRelic), "OnSuccessfulPurchase", null, null);
			}

			// Token: 0x060006AB RID: 1707 RVA: 0x00022933 File Offset: 0x00020B33
			private static bool Prefix(NMerchantRelic __instance)
			{
				if (!RpgShopRandomItemPatches.IsOurEntry(__instance.Entry))
				{
					return true;
				}
				__instance.Entry.OnMerchantInventoryUpdated();
				return false;
			}
		}

		// Token: 0x020000D2 RID: 210
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040002D8 RID: 728
			[Nullable(0)]
			public static Func<MerchantRelicEntry, bool> <0>__IsOurEntry;

			// Token: 0x040002D9 RID: 729
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Predicate<MerchantRelicEntry> <1>__IsOurEntry;
		}

		// Token: 0x020000D3 RID: 211
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <Purchase>d__11 : IAsyncStateMachine
		{
			// Token: 0x060006AC RID: 1708 RVA: 0x00022950 File Offset: 0x00020B50
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ValueTuple<bool, int> result;
				try
				{
					TaskAwaiter awaiter;
					if (num != 0)
					{
						this.<player>5__2 = this.inventory.Player;
						if (!RpgShopRandomItemPatches.TryGetShopRelic(this.entry, out this.<shop>5__3))
						{
							result = new ValueTuple<bool, int>(false, 0);
							goto IL_18E;
						}
						this.<shop>5__3.RefreshDisplayedPrice();
						this.<cost>5__4 = (this.ignoreCost ? 0 : this.entry.Cost);
						this.<item>5__5 = RpgShopRandomItemPatches.RollItem(this.<player>5__2, this.<shop>5__3.PurchaseCount);
						if (this.<item>5__5 == null)
						{
							this.entry.InvokePurchaseFailed(4);
							result = new ValueTuple<bool, int>(false, 0);
							goto IL_18E;
						}
						if (this.ignoreCost)
						{
							goto IL_121;
						}
						awaiter = PlayerCmd.LoseGold(this.<cost>5__4, this.<player>5__2, 1).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgShopRandomItemPatches.<Purchase>d__11>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					RpgShopRandomItemPatches.TrySyncGoldLost(this.<cost>5__4);
					IL_121:
					RpgShopRandomItemPatches.GrantItem(this.<player>5__2, this.<item>5__5);
					this.<shop>5__3.IncrementPurchaseCount();
					this.<shop>5__3.RefreshDisplayedPrice();
					this.entry.OnMerchantInventoryUpdated();
					result = new ValueTuple<bool, int>(true, this.<cost>5__4);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<player>5__2 = null;
					this.<shop>5__3 = null;
					this.<item>5__5 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_18E:
				this.<>1__state = -2;
				this.<player>5__2 = null;
				this.<shop>5__3 = null;
				this.<item>5__5 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060006AD RID: 1709 RVA: 0x00022B30 File Offset: 0x00020D30
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040002DA RID: 730
			public int <>1__state;

			// Token: 0x040002DB RID: 731
			[Nullable(0)]
			public AsyncTaskMethodBuilder<ValueTuple<bool, int>> <>t__builder;

			// Token: 0x040002DC RID: 732
			[Nullable(0)]
			public MerchantInventory inventory;

			// Token: 0x040002DD RID: 733
			[Nullable(0)]
			public MerchantRelicEntry entry;

			// Token: 0x040002DE RID: 734
			public bool ignoreCost;

			// Token: 0x040002DF RID: 735
			[Nullable(0)]
			private Player <player>5__2;

			// Token: 0x040002E0 RID: 736
			[Nullable(0)]
			private RpgRandomItemShopRelic <shop>5__3;

			// Token: 0x040002E1 RID: 737
			private int <cost>5__4;

			// Token: 0x040002E2 RID: 738
			[Nullable(0)]
			private RpgItemData <item>5__5;

			// Token: 0x040002E3 RID: 739
			private TaskAwaiter <>u__1;
		}
	}
}
