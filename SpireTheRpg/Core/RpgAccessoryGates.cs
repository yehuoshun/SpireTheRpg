using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;

namespace SpireTheRpg.Core
{
	// Token: 0x02000035 RID: 53
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgAccessoryGates
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x000101F9 File Offset: 0x0000E3F9
		public static bool ContributesStats(RpgItemData item, [Nullable(2)] Player player = null)
		{
			return item.EquipSlot != RpgEquipSlot.Accessory || RpgAccessoryGates.IsActive(item, player);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0001020D File Offset: 0x0000E40D
		public static bool MeetsCharacterRequirement(RpgItemData item, [Nullable(2)] Player player = null)
		{
			return string.IsNullOrWhiteSpace(item.RequireCharacter) || RpgAccessoryGates.CharacterMatches(item.RequireCharacter, player);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0001022C File Offset: 0x0000E42C
		public static bool MeetsSlotRequirements(RpgItemData item, [Nullable(2)] Player player = null)
		{
			if (item.EquipSlot != RpgEquipSlot.Accessory)
			{
				return true;
			}
			if (item.RequireAllSlotsFilled && !RpgAccessoryGates.AllNonAccessoryFilled(player))
			{
				return false;
			}
			using (List<string>.Enumerator enumerator = item.RequireSlots.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RpgEquipSlot rpgEquipSlot;
					if (Enum.TryParse<RpgEquipSlot>(enumerator.Current, true, out rpgEquipSlot) && rpgEquipSlot != RpgEquipSlot.Accessory && RpgActiveLoadout.GetEquipped(player, rpgEquipSlot) == null)
					{
						return false;
					}
				}
			}
			return (string.IsNullOrWhiteSpace(item.RequireItemIdPrefix) || RpgAccessoryGates.CountPrefix(item.RequireItemIdPrefix, player) > 0) && ((item.RequireItemIds.Count <= 0 && string.IsNullOrWhiteSpace(item.RequireGlovesMinRarity)) || RpgAccessoryGates.GlovesRequirementMet(item, player));
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000102F8 File Offset: 0x0000E4F8
		public static bool MeetsEquipRequirements(RpgItemData item, [Nullable(2)] Player player = null)
		{
			return RpgAccessoryGates.MeetsCharacterRequirement(item, player) && RpgAccessoryGates.MeetsSlotRequirements(item, player);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0001030C File Offset: 0x0000E50C
		public static bool IsActive(RpgItemData item, [Nullable(2)] Player player = null)
		{
			return item.EquipSlot != RpgEquipSlot.Accessory || RpgAccessoryGates.MeetsEquipRequirements(item, player);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00010320 File Offset: 0x0000E520
		public static int CountPrefix(string prefix, [Nullable(2)] Player player = null)
		{
			int num = 0;
			using (IEnumerator<RpgItemData> enumerator = RpgActiveLoadout.EquippedItems(player).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Id.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0001037C File Offset: 0x0000E57C
		public static IEnumerable<string> RequirementLines(RpgItemData item)
		{
			RpgAccessoryGates.<RequirementLines>d__7 <RequirementLines>d__ = new RpgAccessoryGates.<RequirementLines>d__7(-2);
			<RequirementLines>d__.<>3__item = item;
			return <RequirementLines>d__;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0001038C File Offset: 0x0000E58C
		[NullableContext(2)]
		public static string CurrentCharacterKey(Player player = null)
		{
			try
			{
				if (player == null)
				{
					player = RpgAccessoryGates.TryLocalPlayer();
				}
				if (player == null)
				{
					return null;
				}
				CharacterModel character = player.Character;
				if (character != null)
				{
					string name = character.GetType().Name;
					foreach (string text in new string[]
					{
						"Ironclad",
						"Silent",
						"Regent",
						"Necrobinder",
						"Defect"
					})
					{
						if (name.Contains(text, StringComparison.OrdinalIgnoreCase))
						{
							return text;
						}
					}
				}
				object obj = RpgAccessoryGates.AccessToolsLikeGet(player, "CharacterId");
				string text2 = ((obj != null) ? obj.ToString() : null) ?? "";
				foreach (string text3 in new string[]
				{
					"Ironclad",
					"Silent",
					"Regent",
					"Necrobinder",
					"Defect"
				})
				{
					if (text2.Contains(text3, StringComparison.OrdinalIgnoreCase))
					{
						return text3;
					}
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000104C0 File Offset: 0x0000E6C0
		private static bool CharacterMatches(string required, [Nullable(2)] Player player)
		{
			string text = RpgAccessoryGates.CurrentCharacterKey(player);
			return text != null && string.Equals(text, required, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000104E4 File Offset: 0x0000E6E4
		[NullableContext(2)]
		private static bool AllNonAccessoryFilled(Player player)
		{
			foreach (RpgEquipSlot rpgEquipSlot in RpgAccessoryGates.AllGearSlots)
			{
				if (rpgEquipSlot != RpgEquipSlot.Accessory && RpgActiveLoadout.GetEquipped(player, rpgEquipSlot) == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0001051C File Offset: 0x0000E71C
		private static bool GlovesRequirementMet(RpgItemData item, [Nullable(2)] Player player)
		{
			RpgItemData gloves = RpgActiveLoadout.GetEquipped(player, RpgEquipSlot.Gloves);
			if (gloves == null)
			{
				return false;
			}
			if (item.RequireItemIds.Count > 0 && item.RequireItemIds.Any((string id) => string.Equals(id, gloves.Id, StringComparison.OrdinalIgnoreCase)))
			{
				return true;
			}
			if (string.IsNullOrWhiteSpace(item.RequireGlovesMinRarity))
			{
				return item.RequireItemIds.Count == 0;
			}
			RpgItemRarity rpgItemRarity;
			return Enum.TryParse<RpgItemRarity>(item.RequireGlovesMinRarity, true, out rpgItemRarity) && gloves.Rarity >= rpgItemRarity;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000105B0 File Offset: 0x0000E7B0
		private static string SlotLoc(string slotName)
		{
			RpgEquipSlot value;
			if (!Enum.TryParse<RpgEquipSlot>(slotName, true, out value))
			{
				return slotName;
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
			defaultInterpolatedStringHandler.AppendLiteral("type.");
			defaultInterpolatedStringHandler.AppendFormatted<RpgEquipSlot>(value);
			return SpireLoc.Item(defaultInterpolatedStringHandler.ToStringAndClear(), value.ToString());
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00010600 File Offset: 0x0000E800
		[NullableContext(2)]
		private static Player TryLocalPlayer()
		{
			Player result;
			try
			{
				RunManager instance = RunManager.Instance;
				if (instance == null)
				{
					result = null;
				}
				else
				{
					PropertyInfo property = typeof(RunManager).GetProperty("State", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					RunState runState = ((property != null) ? property.GetValue(instance) : null) as RunState;
					if (runState == null)
					{
						result = null;
					}
					else
					{
						IEnumerable<Player> players = runState.Players;
						Func<Player, bool> predicate;
						if ((predicate = RpgAccessoryGates.<>O.<1>__IsLocal) == null)
						{
							predicate = (RpgAccessoryGates.<>O.<1>__IsLocal = new Func<Player, bool>(RpgNet.IsLocal));
						}
						result = (players.FirstOrDefault(predicate) ?? runState.Players.FirstOrDefault<Player>());
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00010698 File Offset: 0x0000E898
		[return: Nullable(2)]
		private static object AccessToolsLikeGet(object obj, string propName)
		{
			PropertyInfo property = obj.GetType().GetProperty(propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (property == null)
			{
				return null;
			}
			return property.GetValue(obj);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000106B4 File Offset: 0x0000E8B4
		// Note: this type is marked as 'beforefieldinit'.
		static RpgAccessoryGates()
		{
		}

		// Token: 0x04000099 RID: 153
		private static readonly RpgEquipSlot[] AllGearSlots = new RpgEquipSlot[]
		{
			RpgEquipSlot.Helmet,
			RpgEquipSlot.Chest,
			RpgEquipSlot.Gloves,
			RpgEquipSlot.Boots,
			RpgEquipSlot.Weapon,
			RpgEquipSlot.Accessory
		};

		// Token: 0x020000D8 RID: 216
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040002F6 RID: 758
			[Nullable(0)]
			public static Func<string, string> <0>__SlotLoc;

			// Token: 0x040002F7 RID: 759
			[Nullable(0)]
			public static Func<Player, bool> <1>__IsLocal;
		}

		// Token: 0x020000D9 RID: 217
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0
		{
			// Token: 0x060006B8 RID: 1720 RVA: 0x00023212 File Offset: 0x00021412
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x060006B9 RID: 1721 RVA: 0x0002321A File Offset: 0x0002141A
			[NullableContext(0)]
			internal bool <GlovesRequirementMet>b__0(string id)
			{
				return string.Equals(id, this.gloves.Id, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x040002F8 RID: 760
			[Nullable(0)]
			public RpgItemData gloves;
		}

		// Token: 0x020000DA RID: 218
		[CompilerGenerated]
		private sealed class <RequirementLines>d__7 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IEnumerator, IDisposable
		{
			// Token: 0x060006BA RID: 1722 RVA: 0x0002322E File Offset: 0x0002142E
			[DebuggerHidden]
			public <RequirementLines>d__7(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060006BB RID: 1723 RVA: 0x00023248 File Offset: 0x00021448
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				this.<>1__state = -2;
			}

			// Token: 0x060006BC RID: 1724 RVA: 0x00023254 File Offset: 0x00021454
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					if (item.EquipSlot != RpgEquipSlot.Accessory)
					{
						return false;
					}
					if (!string.IsNullOrWhiteSpace(item.RequireCharacter))
					{
						string text = SpireLoc.Item("character." + item.RequireCharacter, item.RequireCharacter);
						this.<>2__current = SpireLoc.ItemFormat("ui.require_character", "Requires {0}", new object[]
						{
							text
						});
						this.<>1__state = 1;
						return true;
					}
					break;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					goto IL_11D;
				case 3:
					this.<>1__state = -1;
					goto IL_168;
				case 4:
					this.<>1__state = -1;
					goto IL_19A;
				case 5:
					this.<>1__state = -1;
					return false;
				default:
					return false;
				}
				if (item.RequireSlots.Count > 0)
				{
					string separator = ", ";
					IEnumerable<string> requireSlots = item.RequireSlots;
					Func<string, string> selector;
					if ((selector = RpgAccessoryGates.<>O.<0>__SlotLoc) == null)
					{
						selector = (RpgAccessoryGates.<>O.<0>__SlotLoc = new Func<string, string>(RpgAccessoryGates.SlotLoc));
					}
					string text2 = string.Join(separator, requireSlots.Select(selector));
					this.<>2__current = SpireLoc.ItemFormat("ui.require_slots", "Requires equipped: {0}", new object[]
					{
						text2
					});
					this.<>1__state = 2;
					return true;
				}
				IL_11D:
				if (!string.IsNullOrWhiteSpace(item.RequireItemIdPrefix))
				{
					this.<>2__current = SpireLoc.ItemFormat("ui.require_item_prefix", "Requires {0}* gear", new object[]
					{
						item.RequireItemIdPrefix
					});
					this.<>1__state = 3;
					return true;
				}
				IL_168:
				if (item.RequireAllSlotsFilled)
				{
					this.<>2__current = SpireLoc.Item("ui.require_all_slots", "Requires all gear slots filled");
					this.<>1__state = 4;
					return true;
				}
				IL_19A:
				if (!string.IsNullOrWhiteSpace(item.RequireGlovesMinRarity))
				{
					this.<>2__current = SpireLoc.ItemFormat("ui.require_gloves_rarity", "Requires {0}+ gloves (or listed gloves)", new object[]
					{
						item.RequireGlovesMinRarity
					});
					this.<>1__state = 5;
					return true;
				}
				return false;
			}

			// Token: 0x1700018C RID: 396
			// (get) Token: 0x060006BD RID: 1725 RVA: 0x00023447 File Offset: 0x00021647
			string IEnumerator<string>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060006BE RID: 1726 RVA: 0x0002344F File Offset: 0x0002164F
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700018D RID: 397
			// (get) Token: 0x060006BF RID: 1727 RVA: 0x00023456 File Offset: 0x00021656
			object IEnumerator.Current
			{
				[DebuggerHidden]
				[return: Nullable(0)]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060006C0 RID: 1728 RVA: 0x00023460 File Offset: 0x00021660
			[DebuggerHidden]
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				RpgAccessoryGates.<RequirementLines>d__7 <RequirementLines>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<RequirementLines>d__ = this;
				}
				else
				{
					<RequirementLines>d__ = new RpgAccessoryGates.<RequirementLines>d__7(0);
				}
				<RequirementLines>d__.item = item;
				return <RequirementLines>d__;
			}

			// Token: 0x060006C1 RID: 1729 RVA: 0x000234A3 File Offset: 0x000216A3
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();
			}

			// Token: 0x040002F9 RID: 761
			private int <>1__state;

			// Token: 0x040002FA RID: 762
			private string <>2__current;

			// Token: 0x040002FB RID: 763
			private int <>l__initialThreadId;

			// Token: 0x040002FC RID: 764
			[Nullable(0)]
			private RpgItemData item;

			// Token: 0x040002FD RID: 765
			[Nullable(0)]
			public RpgItemData <>3__item;
		}
	}
}
