using System;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Random;

namespace SpireTheRpg.Core
{
	// Token: 0x0200003C RID: 60
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgCurseSystem
	{
		// Token: 0x06000220 RID: 544 RVA: 0x00011794 File Offset: 0x0000F994
		public static void ApplyOnRoll(RpgItemData item, [Nullable(2)] Rng rng)
		{
			if (!RpgEffectiveConfig.EnableCurses)
			{
				return;
			}
			if (item.IsScroll || item.Rarity < RpgItemRarity.Rare)
			{
				return;
			}
			double num;
			switch (item.Rarity)
			{
			case RpgItemRarity.Rare:
				num = RpgEffectiveConfig.CurseChanceRare;
				break;
			case RpgItemRarity.Epic:
				num = RpgEffectiveConfig.CurseChanceEpic;
				break;
			case RpgItemRarity.Legendary:
				num = RpgEffectiveConfig.CurseChanceLegendary;
				break;
			default:
				num = 0.0;
				break;
			}
			double num2 = num;
			if (RpgItemRoller.NextDouble(rng) < num2)
			{
				string text = RpgCurseSystem.CurseIds[RpgItemRoller.NextInt(rng, RpgCurseSystem.CurseIds.Length)];
				if (!item.Curses.Contains(text, StringComparer.OrdinalIgnoreCase))
				{
					item.Curses.Add(text);
				}
			}
			if (RpgItemRoller.NextDouble(rng) < RpgEffectiveConfig.UnidentifiedChanceRarePlus)
			{
				if (item.Ability.Count > 0)
				{
					item.HiddenAbility = item.Ability.ToList<string>();
					item.Ability.Clear();
				}
				item.IsIdentified = false;
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00011876 File Offset: 0x0000FA76
		public static bool TryIdentify(RpgItemData item)
		{
			if (item.IsIdentified)
			{
				return false;
			}
			item.Identify();
			return true;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00011889 File Offset: 0x0000FA89
		public static bool TryCleanse(RpgItemData item)
		{
			if (!item.HasCurse && !item.IsBound)
			{
				return false;
			}
			item.ClearCurses();
			return true;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000118A4 File Offset: 0x0000FAA4
		public static void OnEquipped(RpgItemData item)
		{
			if (!RpgEffectiveConfig.EnableCurses)
			{
				return;
			}
			if (item.HasCurse)
			{
				item.IsBound = true;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000118BD File Offset: 0x0000FABD
		[NullableContext(2)]
		public static bool CanUnequip(RpgItemData item)
		{
			return item == null || !item.IsBound || !RpgEffectiveConfig.EnableCurses;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000118D4 File Offset: 0x0000FAD4
		public static bool CanTrade(RpgItemData item)
		{
			return !item.IsBound && !item.HasCurse;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000118E9 File Offset: 0x0000FAE9
		// Note: this type is marked as 'beforefieldinit'.
		static RpgCurseSystem()
		{
		}

		// Token: 0x040000AC RID: 172
		private static readonly string[] CurseIds = new string[]
		{
			"frail",
			"poison",
			"weak",
			"vulnerable",
			"confused"
		};
	}
}
