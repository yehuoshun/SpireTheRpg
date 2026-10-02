using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Random;

namespace SpireTheRpg.Core
{
	// Token: 0x02000049 RID: 73
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgItemRoller
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x00013B76 File Offset: 0x00011D76
		[NullableContext(2)]
		public static int NextInt(Rng rng, int maxExclusive)
		{
			if (rng == null)
			{
				return Random.Shared.Next(maxExclusive);
			}
			return rng.NextInt(maxExclusive);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00013B8E File Offset: 0x00011D8E
		[NullableContext(2)]
		public static int NextInt(Rng rng, int minInclusive, int maxExclusive)
		{
			if (rng == null)
			{
				return Random.Shared.Next(minInclusive, maxExclusive);
			}
			return rng.NextInt(minInclusive, maxExclusive);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00013BA8 File Offset: 0x00011DA8
		[NullableContext(2)]
		public static double NextDouble(Rng rng)
		{
			if (rng == null)
			{
				return Random.Shared.NextDouble();
			}
			return rng.NextDouble();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00013BC0 File Offset: 0x00011DC0
		private static string MakeInstanceId(string templateId, RpgItemRarity rarity, [Nullable(2)] Rng rng)
		{
			if (rng == null)
			{
				return Guid.NewGuid().ToString("N");
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 5);
			defaultInterpolatedStringHandler.AppendFormatted(templateId);
			defaultInterpolatedStringHandler.AppendLiteral("_");
			defaultInterpolatedStringHandler.AppendFormatted<int>((int)rarity);
			defaultInterpolatedStringHandler.AppendLiteral("_");
			defaultInterpolatedStringHandler.AppendFormatted<int>(RpgItemRoller.NextInt(rng, 65536), "x4");
			defaultInterpolatedStringHandler.AppendLiteral("_");
			defaultInterpolatedStringHandler.AppendFormatted<int>(RpgItemRoller.NextInt(rng, 65536), "x4");
			defaultInterpolatedStringHandler.AppendLiteral("_");
			defaultInterpolatedStringHandler.AppendFormatted<int>(RpgItemRoller.NextInt(rng, 65536), "x4");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00013C78 File Offset: 0x00011E78
		public static void EnsureUniqueInstanceIds(IEnumerable<RpgItemData> items)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			foreach (RpgItemData rpgItemData in items)
			{
				if (string.IsNullOrWhiteSpace(rpgItemData.InstanceId) || !hashSet.Add(rpgItemData.InstanceId))
				{
					rpgItemData.InstanceId = Guid.NewGuid().ToString("N");
					hashSet.Add(rpgItemData.InstanceId);
				}
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00013D04 File Offset: 0x00011F04
		public static void SelfCheck()
		{
			try
			{
				RpgItemTemplate rpgItemTemplate = new RpgItemTemplate();
				rpgItemTemplate.Id = "self_check";
				rpgItemTemplate.IconLetter = "?";
				rpgItemTemplate.EquipSlot = RpgEquipSlot.Boots;
				RpgItemData rpgItemData = RpgItemRoller.Roll(rpgItemTemplate, RpgItemRarity.Rare, null);
				if (rpgItemData.Primary == null || !rpgItemData.Primary.StartsWith("armor:", StringComparison.OrdinalIgnoreCase))
				{
					throw new Exception("bad primary=" + rpgItemData.Primary);
				}
				List<string> list = RpgItemRoller.Roll(new RpgItemTemplate
				{
					Id = "self_check_acc",
					IconLetter = "A",
					EquipSlot = RpgEquipSlot.Accessory,
					RequireCharacter = "Ironclad"
				}, RpgItemRarity.Rare, null).EffectiveAbilityLines().ToList<string>();
				int count = list.Count;
				bool flag = count < 1 || count > 2;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				if (flag)
				{
					defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(18, 1);
					defaultInterpolatedStringHandler.AppendLiteral("acc ability count=");
					defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
					throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				if (list.Count == 2)
				{
					IEnumerable<string> source = list;
					Func<string, RpgStatMod> selector;
					if ((selector = RpgItemRoller.<>O.<0>__Parse) == null)
					{
						selector = (RpgItemRoller.<>O.<0>__Parse = new Func<string, RpgStatMod>(RpgStatMod.Parse));
					}
					if ((from x in source.Select(selector)
					where x != null
					select x).ToList<RpgStatMod>().Count((RpgStatMod m) => m.IsDebuff) != 1)
					{
						throw new Exception("acc dual must include exactly one debuff");
					}
				}
				RpgItemData rpgItemData2 = RpgItemRoller.Roll(rpgItemTemplate, RpgItemRarity.Common, null);
				if (rpgItemData2.Ability.Count != 0 || rpgItemData2.HiddenAbility.Count != 0)
				{
					throw new Exception("common should have no affixes");
				}
				if (RpgAffixCatalog.All.Count < 20)
				{
					defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(25, 1);
					defaultInterpolatedStringHandler.AppendLiteral("affix catalog too small: ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(RpgAffixCatalog.All.Count);
					throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				if (RpgAffixCatalog.Debuffs.Count < 10)
				{
					defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(26, 1);
					defaultInterpolatedStringHandler.AppendLiteral("debuff catalog too small: ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(RpgAffixCatalog.Debuffs.Count);
					throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				Logger logger = SpireTheRpgMainFile.Logger;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(77, 5);
				defaultInterpolatedStringHandler.AppendLiteral("RpgItemRoller self-check ok primary=");
				defaultInterpolatedStringHandler.AppendFormatted(rpgItemData.Primary);
				defaultInterpolatedStringHandler.AppendLiteral(" affixes=");
				defaultInterpolatedStringHandler.AppendFormatted<int>(rpgItemData.EffectiveAbilityLines().Count<string>());
				defaultInterpolatedStringHandler.AppendLiteral(" catalog=");
				defaultInterpolatedStringHandler.AppendFormatted<int>(RpgAffixCatalog.All.Count);
				defaultInterpolatedStringHandler.AppendLiteral(" debuffs=");
				defaultInterpolatedStringHandler.AppendFormatted<int>(RpgAffixCatalog.Debuffs.Count);
				defaultInterpolatedStringHandler.AppendLiteral(" accAbilities=");
				defaultInterpolatedStringHandler.AppendFormatted<int>(list.Count);
				logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RpgItemRoller self-check FAIL: " + ex.Message, 1);
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00014014 File Offset: 0x00012214
		public static RpgItemData Roll(RpgItemTemplate template, RpgItemRarity rarity, [Nullable(2)] Rng rng = null)
		{
			if (template.EquipSlot == RpgEquipSlot.Accessory)
			{
				return RpgItemRoller.RollAccessory(template, rarity, rng);
			}
			ValueTuple<int, int, int, int> valueTuple = RpgItemRoller.Meta(rarity);
			int item = valueTuple.Item1;
			int item2 = valueTuple.Item2;
			int item3 = valueTuple.Item3;
			int item4 = valueTuple.Item4;
			string primary = RpgItemRoller.RollPrimary(template.EquipSlot, rarity, rng);
			int num = RpgItemRoller.AffixCount(rarity, rng);
			List<string> list = new List<string>();
			List<string> details = new List<string>();
			HashSet<string> used = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			List<RpgAffixDef> list2 = RpgAffixCatalog.Buffs.ToList<RpgAffixDef>();
			int num2 = 0;
			Func<RpgAffixDef, bool> <>9__0;
			while (num2 < num && list2.Count > 0)
			{
				IEnumerable<RpgAffixDef> source = list2;
				Func<RpgAffixDef, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((RpgAffixDef a) => !used.Contains(a.Id)));
				}
				List<RpgAffixDef> list3 = source.Where(predicate).ToList<RpgAffixDef>();
				if (list3.Count == 0)
				{
					break;
				}
				RpgAffixDef rpgAffixDef = list3[RpgItemRoller.NextInt(rng, list3.Count)];
				used.Add(rpgAffixDef.Id);
				list.Add(RpgItemRoller.FormatCatalogAffix(rpgAffixDef, rarity, rng));
				num2++;
			}
			if (rarity >= RpgItemRarity.Rare && RpgItemRoller.NextDouble(rng) < 0.45)
			{
				RpgItemRoller.ExtraAffix extraAffix = RpgItemRoller.ExtraPercentAffixes[RpgItemRoller.NextInt(rng, RpgItemRoller.ExtraPercentAffixes.Length)];
				if (used.Add(extraAffix.Id))
				{
					list.Add(RpgItemRoller.FormatExtra(extraAffix, rarity, rng));
				}
			}
			if (RpgItemRoller.NextDouble(rng) < RpgItemRoller.DebuffChance(rarity))
			{
				List<RpgAffixDef> list4 = (from a in RpgAffixCatalog.Debuffs
				where !used.Contains(a.Id)
				select a).ToList<RpgAffixDef>();
				if (list4.Count > 0)
				{
					RpgAffixDef rpgAffixDef2 = list4[RpgItemRoller.NextInt(rng, list4.Count)];
					used.Add(rpgAffixDef2.Id);
					list.Add(RpgItemRoller.FormatCatalogAffix(rpgAffixDef2, rarity, rng));
				}
			}
			RpgItemData rpgItemData = new RpgItemData();
			rpgItemData.InstanceId = RpgItemRoller.MakeInstanceId(template.Id, rarity, rng);
			rpgItemData.Id = template.Id;
			rpgItemData.Rarity = rarity;
			rpgItemData.ItemPower = RpgItemRoller.NextInt(rng, item, item2 + 1);
			rpgItemData.RequireLevel = RpgItemRoller.NextInt(rng, item3, item4 + 1);
			rpgItemData.IconLetter = template.IconLetter;
			rpgItemData.IconPath = template.IconPath;
			rpgItemData.EquipSlot = template.EquipSlot;
			rpgItemData.Primary = primary;
			rpgItemData.Details = details;
			rpgItemData.Ability = list;
			rpgItemData.HasLegendaryText = (rarity == RpgItemRarity.Legendary);
			rpgItemData.CopyRequirementsFrom(template);
			RpgCurseSystem.ApplyOnRoll(rpgItemData, rng);
			return rpgItemData;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00014288 File Offset: 0x00012488
		private static RpgItemData RollAccessory(RpgItemTemplate template, RpgItemRarity rarity, [Nullable(2)] Rng rng)
		{
			ValueTuple<int, int, int, int> valueTuple = RpgItemRoller.Meta(rarity);
			int item = valueTuple.Item1;
			int item2 = valueTuple.Item2;
			int item3 = valueTuple.Item3;
			int item4 = valueTuple.Item4;
			string primary = RpgItemRoller.RollPrimary(RpgEquipSlot.Accessory, rarity, rng);
			List<string> list = new List<string>();
			HashSet<string> used = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			if (rarity >= RpgItemRarity.Rare)
			{
				int num = (RpgItemRoller.NextInt(rng, 0, 2) == 0) ? 1 : 2;
				List<RpgAffixDef> list2 = (from a in RpgAffixCatalog.Buffs
				where !used.Contains(a.Id)
				select a).ToList<RpgAffixDef>();
				if (list2.Count == 0)
				{
					list2 = RpgAffixCatalog.Buffs.ToList<RpgAffixDef>();
				}
				RpgAffixDef rpgAffixDef = list2[RpgItemRoller.NextInt(rng, list2.Count)];
				used.Add(rpgAffixDef.Id);
				list.Add(RpgItemRoller.FormatCatalogAffix(rpgAffixDef, rarity, rng));
				if (num == 2)
				{
					List<RpgAffixDef> list3 = (from a in RpgAffixCatalog.Debuffs
					where !used.Contains(a.Id)
					select a).ToList<RpgAffixDef>();
					if (list3.Count > 0)
					{
						RpgAffixDef rpgAffixDef2 = list3[RpgItemRoller.NextInt(rng, list3.Count)];
						used.Add(rpgAffixDef2.Id);
						list.Add(RpgItemRoller.FormatCatalogAffix(rpgAffixDef2, rarity, rng));
						if (RpgItemRoller.NextInt(rng, 2) == 0)
						{
							list.Reverse();
						}
					}
				}
			}
			RpgItemData rpgItemData = new RpgItemData();
			rpgItemData.InstanceId = RpgItemRoller.MakeInstanceId(template.Id, rarity, rng);
			rpgItemData.Id = template.Id;
			rpgItemData.Rarity = rarity;
			rpgItemData.ItemPower = RpgItemRoller.NextInt(rng, item, item2 + 1);
			rpgItemData.RequireLevel = RpgItemRoller.NextInt(rng, item3, item4 + 1);
			rpgItemData.IconLetter = template.IconLetter;
			rpgItemData.IconPath = template.IconPath;
			rpgItemData.EquipSlot = RpgEquipSlot.Accessory;
			rpgItemData.Primary = primary;
			rpgItemData.Details = new List<string>();
			rpgItemData.Ability = list;
			rpgItemData.HasLegendaryText = (rarity >= RpgItemRarity.Rare);
			rpgItemData.CopyRequirementsFrom(template);
			RpgCurseSystem.ApplyOnRoll(rpgItemData, rng);
			return rpgItemData;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00014474 File Offset: 0x00012674
		private static string FormatCatalogAffix(RpgAffixDef def, RpgItemRarity rarity, [Nullable(2)] Rng rng)
		{
			double num = def.IsDebuff ? 1.0 : RpgItemRoller.RarityMult(rarity);
			int num2 = Math.Max(1, (int)Math.Round((double)def.Min * num));
			int num3 = Math.Max(num2, (int)Math.Round((double)def.Max * num));
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted(def.Id);
			defaultInterpolatedStringHandler.AppendLiteral(":");
			defaultInterpolatedStringHandler.AppendFormatted<int>(RpgItemRoller.NextInt(rng, num2, num3 + 1));
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00014504 File Offset: 0x00012704
		private static string FormatExtra(RpgItemRoller.ExtraAffix pool, RpgItemRarity rarity, [Nullable(2)] Rng rng)
		{
			double num = RpgItemRoller.RarityMult(rarity);
			double num2 = pool.Min * num;
			double num3 = pool.Max * num;
			if (!pool.Percent)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
				defaultInterpolatedStringHandler.AppendFormatted(pool.Id);
				defaultInterpolatedStringHandler.AppendLiteral(":");
				defaultInterpolatedStringHandler.AppendFormatted<int>(RpgItemRoller.NextInt(rng, (int)Math.Round(num2), (int)Math.Round(num3) + 1));
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
			double num4 = Math.Round(num2 + RpgItemRoller.NextDouble(rng) * (num3 - num2), 1);
			if (Math.Abs(num4 % 1.0) < 0.05)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
				defaultInterpolatedStringHandler.AppendFormatted(pool.Id);
				defaultInterpolatedStringHandler.AppendLiteral(":");
				defaultInterpolatedStringHandler.AppendFormatted<int>((int)num4);
				defaultInterpolatedStringHandler.AppendLiteral("%");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
			return pool.Id + ":" + num4.ToString("0.0", CultureInfo.InvariantCulture) + "%";
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00014610 File Offset: 0x00012810
		private static double DebuffChance(RpgItemRarity rarity)
		{
			double result;
			switch (rarity)
			{
			case RpgItemRarity.Common:
				result = 0.0;
				break;
			case RpgItemRarity.Uncommon:
				result = 0.0;
				break;
			case RpgItemRarity.Rare:
				result = 0.22;
				break;
			case RpgItemRarity.Epic:
				result = 0.2;
				break;
			case RpgItemRarity.Legendary:
				result = 0.18;
				break;
			default:
				result = 0.0;
				break;
			}
			return result;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00014680 File Offset: 0x00012880
		private static double RarityMult(RpgItemRarity rarity)
		{
			double result;
			switch (rarity)
			{
			case RpgItemRarity.Common:
				result = 1.0;
				break;
			case RpgItemRarity.Uncommon:
				result = 1.25;
				break;
			case RpgItemRarity.Rare:
				result = 1.5;
				break;
			case RpgItemRarity.Epic:
				result = 1.85;
				break;
			case RpgItemRarity.Legendary:
				result = 2.2;
				break;
			default:
				result = 1.0;
				break;
			}
			return result;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000146F0 File Offset: 0x000128F0
		private static string RollPrimary(RpgEquipSlot slot, RpgItemRarity rarity, [Nullable(2)] Rng rng)
		{
			ValueTuple<int, int> valueTuple;
			switch (slot)
			{
			case RpgEquipSlot.Gloves:
				valueTuple = RpgItemRoller.WeaponPrimaryRange(rarity);
				goto IL_40;
			case RpgEquipSlot.Weapon:
				valueTuple = RpgItemRoller.WeaponPrimaryRange(rarity);
				goto IL_40;
			case RpgEquipSlot.Accessory:
				valueTuple = RpgItemRoller.WeaponPrimaryRange(rarity);
				goto IL_40;
			}
			valueTuple = RpgItemRoller.ArmorPrimaryRange(rarity);
			IL_40:
			ValueTuple<int, int> valueTuple2 = valueTuple;
			int item = valueTuple2.Item1;
			int item2 = valueTuple2.Item2;
			int value = RpgItemRoller.NextInt(rng, item, item2 + 1);
			string text;
			switch (slot)
			{
			case RpgEquipSlot.Gloves:
				text = "dexterity";
				goto IL_BF;
			case RpgEquipSlot.Weapon:
				text = "strength";
				goto IL_BF;
			case RpgEquipSlot.Accessory:
				text = (new string[]
				{
					"strength",
					"dexterity",
					"intelligence",
					"vitality"
				})[RpgItemRoller.NextInt(rng, 4)];
				goto IL_BF;
			}
			text = "armor";
			IL_BF:
			string value2 = text;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted(value2);
			defaultInterpolatedStringHandler.AppendLiteral(":");
			defaultInterpolatedStringHandler.AppendFormatted<int>(value);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000147EC File Offset: 0x000129EC
		[NullableContext(0)]
		[return: TupleElementNames(new string[]
		{
			"Min",
			"Max"
		})]
		private static ValueTuple<int, int> WeaponPrimaryRange(RpgItemRarity rarity)
		{
			ValueTuple<int, int> result;
			switch (rarity)
			{
			case RpgItemRarity.Common:
				result = new ValueTuple<int, int>(1, 1);
				break;
			case RpgItemRarity.Uncommon:
				result = new ValueTuple<int, int>(1, 2);
				break;
			case RpgItemRarity.Rare:
				result = new ValueTuple<int, int>(1, 3);
				break;
			case RpgItemRarity.Epic:
				result = new ValueTuple<int, int>(2, 4);
				break;
			case RpgItemRarity.Legendary:
				result = new ValueTuple<int, int>(3, 5);
				break;
			default:
				result = new ValueTuple<int, int>(1, 1);
				break;
			}
			return result;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00014850 File Offset: 0x00012A50
		[NullableContext(0)]
		[return: TupleElementNames(new string[]
		{
			"Min",
			"Max"
		})]
		private static ValueTuple<int, int> ArmorPrimaryRange(RpgItemRarity rarity)
		{
			ValueTuple<int, int> result;
			switch (rarity)
			{
			case RpgItemRarity.Common:
				result = new ValueTuple<int, int>(0, 1);
				break;
			case RpgItemRarity.Uncommon:
				result = new ValueTuple<int, int>(1, 1);
				break;
			case RpgItemRarity.Rare:
				result = new ValueTuple<int, int>(1, 2);
				break;
			case RpgItemRarity.Epic:
				result = new ValueTuple<int, int>(2, 4);
				break;
			case RpgItemRarity.Legendary:
				result = new ValueTuple<int, int>(4, 6);
				break;
			default:
				result = new ValueTuple<int, int>(0, 1);
				break;
			}
			return result;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000148B4 File Offset: 0x00012AB4
		[NullableContext(2)]
		private static int AffixCount(RpgItemRarity rarity, Rng rng)
		{
			int result;
			switch (rarity)
			{
			case RpgItemRarity.Common:
				result = 0;
				break;
			case RpgItemRarity.Uncommon:
				result = 0;
				break;
			case RpgItemRarity.Rare:
				result = RpgItemRoller.NextInt(rng, 1, 3);
				break;
			case RpgItemRarity.Epic:
				result = RpgItemRoller.NextInt(rng, 2, 4);
				break;
			case RpgItemRarity.Legendary:
				result = RpgItemRoller.NextInt(rng, 3, 5);
				break;
			default:
				result = 0;
				break;
			}
			return result;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0001490C File Offset: 0x00012B0C
		[NullableContext(0)]
		[return: TupleElementNames(new string[]
		{
			"PowerMin",
			"PowerMax",
			"LevelMin",
			"LevelMax"
		})]
		private static ValueTuple<int, int, int, int> Meta(RpgItemRarity rarity)
		{
			ValueTuple<int, int, int, int> result;
			switch (rarity)
			{
			case RpgItemRarity.Common:
				result = new ValueTuple<int, int, int, int>(28, 48, 1, 3);
				break;
			case RpgItemRarity.Uncommon:
				result = new ValueTuple<int, int, int, int>(55, 90, 4, 7);
				break;
			case RpgItemRarity.Rare:
				result = new ValueTuple<int, int, int, int>(100, 150, 8, 12);
				break;
			case RpgItemRarity.Epic:
				result = new ValueTuple<int, int, int, int>(170, 240, 13, 16);
				break;
			case RpgItemRarity.Legendary:
				result = new ValueTuple<int, int, int, int>(250, 330, 16, 20);
				break;
			default:
				result = new ValueTuple<int, int, int, int>(28, 48, 1, 3);
				break;
			}
			return result;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0001499C File Offset: 0x00012B9C
		// Note: this type is marked as 'beforefieldinit'.
		static RpgItemRoller()
		{
		}

		// Token: 0x040000E4 RID: 228
		private static readonly RpgItemRoller.ExtraAffix[] ExtraPercentAffixes = new RpgItemRoller.ExtraAffix[]
		{
			new RpgItemRoller.ExtraAffix("shadow_damage", true, 4.0, 14.0, false),
			new RpgItemRoller.ExtraAffix("critical_strike_damage", true, 5.0, 15.0, false)
		};

		// Token: 0x020000FE RID: 254
		[Nullable(0)]
		private sealed class ExtraAffix : IEquatable<RpgItemRoller.ExtraAffix>
		{
			// Token: 0x06000746 RID: 1862 RVA: 0x000265F9 File Offset: 0x000247F9
			public ExtraAffix(string Id, bool Percent, double Min, double Max, bool AsDetail = false)
			{
				this.Id = Id;
				this.Percent = Percent;
				this.Min = Min;
				this.Max = Max;
				this.AsDetail = AsDetail;
				base..ctor();
			}

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x06000747 RID: 1863 RVA: 0x00026626 File Offset: 0x00024826
			[CompilerGenerated]
			private Type EqualityContract
			{
				[CompilerGenerated]
				get
				{
					return typeof(RpgItemRoller.ExtraAffix);
				}
			}

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x06000748 RID: 1864 RVA: 0x00026632 File Offset: 0x00024832
			// (set) Token: 0x06000749 RID: 1865 RVA: 0x0002663A File Offset: 0x0002483A
			public string Id
			{
				[CompilerGenerated]
				get
				{
					return this.<Id>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Id>k__BackingField = value;
				}
			}

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x0600074A RID: 1866 RVA: 0x00026643 File Offset: 0x00024843
			// (set) Token: 0x0600074B RID: 1867 RVA: 0x0002664B File Offset: 0x0002484B
			public bool Percent
			{
				[CompilerGenerated]
				get
				{
					return this.<Percent>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Percent>k__BackingField = value;
				}
			}

			// Token: 0x17000197 RID: 407
			// (get) Token: 0x0600074C RID: 1868 RVA: 0x00026654 File Offset: 0x00024854
			// (set) Token: 0x0600074D RID: 1869 RVA: 0x0002665C File Offset: 0x0002485C
			public double Min
			{
				[CompilerGenerated]
				get
				{
					return this.<Min>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Min>k__BackingField = value;
				}
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x0600074E RID: 1870 RVA: 0x00026665 File Offset: 0x00024865
			// (set) Token: 0x0600074F RID: 1871 RVA: 0x0002666D File Offset: 0x0002486D
			public double Max
			{
				[CompilerGenerated]
				get
				{
					return this.<Max>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Max>k__BackingField = value;
				}
			}

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x06000750 RID: 1872 RVA: 0x00026676 File Offset: 0x00024876
			// (set) Token: 0x06000751 RID: 1873 RVA: 0x0002667E File Offset: 0x0002487E
			public bool AsDetail
			{
				[CompilerGenerated]
				get
				{
					return this.<AsDetail>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<AsDetail>k__BackingField = value;
				}
			}

			// Token: 0x06000752 RID: 1874 RVA: 0x00026688 File Offset: 0x00024888
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("ExtraAffix");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x06000753 RID: 1875 RVA: 0x000266D4 File Offset: 0x000248D4
			[CompilerGenerated]
			private bool PrintMembers(StringBuilder builder)
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				builder.Append("Id = ");
				builder.Append(this.Id);
				builder.Append(", Percent = ");
				builder.Append(this.Percent.ToString());
				builder.Append(", Min = ");
				builder.Append(this.Min.ToString());
				builder.Append(", Max = ");
				builder.Append(this.Max.ToString());
				builder.Append(", AsDetail = ");
				builder.Append(this.AsDetail.ToString());
				return true;
			}

			// Token: 0x06000754 RID: 1876 RVA: 0x0002679C File Offset: 0x0002499C
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator !=(RpgItemRoller.ExtraAffix left, RpgItemRoller.ExtraAffix right)
			{
				return !(left == right);
			}

			// Token: 0x06000755 RID: 1877 RVA: 0x000267A8 File Offset: 0x000249A8
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator ==(RpgItemRoller.ExtraAffix left, RpgItemRoller.ExtraAffix right)
			{
				return left == right || (left != null && left.Equals(right));
			}

			// Token: 0x06000756 RID: 1878 RVA: 0x000267BC File Offset: 0x000249BC
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Id>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Percent>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Min>k__BackingField)) * -1521134295 + EqualityComparer<double>.Default.GetHashCode(this.<Max>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<AsDetail>k__BackingField);
			}

			// Token: 0x06000757 RID: 1879 RVA: 0x0002684C File Offset: 0x00024A4C
			[NullableContext(2)]
			[CompilerGenerated]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as RpgItemRoller.ExtraAffix);
			}

			// Token: 0x06000758 RID: 1880 RVA: 0x0002685C File Offset: 0x00024A5C
			[NullableContext(2)]
			[CompilerGenerated]
			public bool Equals(RpgItemRoller.ExtraAffix other)
			{
				return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Id>k__BackingField, other.<Id>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Percent>k__BackingField, other.<Percent>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Min>k__BackingField, other.<Min>k__BackingField) && EqualityComparer<double>.Default.Equals(this.<Max>k__BackingField, other.<Max>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<AsDetail>k__BackingField, other.<AsDetail>k__BackingField));
			}

			// Token: 0x06000759 RID: 1881 RVA: 0x00026903 File Offset: 0x00024B03
			[CompilerGenerated]
			public RpgItemRoller.ExtraAffix <Clone>$()
			{
				return new RpgItemRoller.ExtraAffix(this);
			}

			// Token: 0x0600075A RID: 1882 RVA: 0x0002690C File Offset: 0x00024B0C
			[CompilerGenerated]
			private ExtraAffix(RpgItemRoller.ExtraAffix original)
			{
				this.Id = original.<Id>k__BackingField;
				this.Percent = original.<Percent>k__BackingField;
				this.Min = original.<Min>k__BackingField;
				this.Max = original.<Max>k__BackingField;
				this.AsDetail = original.<AsDetail>k__BackingField;
			}

			// Token: 0x0600075B RID: 1883 RVA: 0x0002695B File Offset: 0x00024B5B
			[CompilerGenerated]
			public void Deconstruct(out string Id, out bool Percent, out double Min, out double Max, out bool AsDetail)
			{
				Id = this.Id;
				Percent = this.Percent;
				Min = this.Min;
				Max = this.Max;
				AsDetail = this.AsDetail;
			}

			// Token: 0x040003AA RID: 938
			[CompilerGenerated]
			private readonly string <Id>k__BackingField;

			// Token: 0x040003AB RID: 939
			[CompilerGenerated]
			private readonly bool <Percent>k__BackingField;

			// Token: 0x040003AC RID: 940
			[CompilerGenerated]
			private readonly double <Min>k__BackingField;

			// Token: 0x040003AD RID: 941
			[CompilerGenerated]
			private readonly double <Max>k__BackingField;

			// Token: 0x040003AE RID: 942
			[CompilerGenerated]
			private readonly bool <AsDetail>k__BackingField;
		}

		// Token: 0x020000FF RID: 255
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040003AF RID: 943
			[Nullable(0)]
			public static Func<string, RpgStatMod> <0>__Parse;
		}

		// Token: 0x02000100 RID: 256
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600075C RID: 1884 RVA: 0x00026987 File Offset: 0x00024B87
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600075D RID: 1885 RVA: 0x00026993 File Offset: 0x00024B93
			public <>c()
			{
			}

			// Token: 0x0600075E RID: 1886 RVA: 0x0002699B File Offset: 0x00024B9B
			[NullableContext(0)]
			internal bool <SelfCheck>b__7_0(RpgStatMod x)
			{
				return x != null;
			}

			// Token: 0x0600075F RID: 1887 RVA: 0x000269A1 File Offset: 0x00024BA1
			[NullableContext(0)]
			internal bool <SelfCheck>b__7_1(RpgStatMod m)
			{
				return m.IsDebuff;
			}

			// Token: 0x040003B0 RID: 944
			[Nullable(0)]
			public static readonly RpgItemRoller.<>c <>9 = new RpgItemRoller.<>c();

			// Token: 0x040003B1 RID: 945
			[Nullable(0)]
			public static Func<RpgStatMod, bool> <>9__7_0;

			// Token: 0x040003B2 RID: 946
			[Nullable(0)]
			public static Func<RpgStatMod, bool> <>9__7_1;
		}

		// Token: 0x02000101 RID: 257
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x06000760 RID: 1888 RVA: 0x000269A9 File Offset: 0x00024BA9
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x06000761 RID: 1889 RVA: 0x000269B1 File Offset: 0x00024BB1
			[NullableContext(0)]
			internal bool <Roll>b__0(RpgAffixDef a)
			{
				return !this.used.Contains(a.Id);
			}

			// Token: 0x06000762 RID: 1890 RVA: 0x000269C7 File Offset: 0x00024BC7
			[NullableContext(0)]
			internal bool <Roll>b__1(RpgAffixDef a)
			{
				return !this.used.Contains(a.Id);
			}

			// Token: 0x040003B3 RID: 947
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public HashSet<string> used;

			// Token: 0x040003B4 RID: 948
			[Nullable(0)]
			public Func<RpgAffixDef, bool> <>9__0;
		}

		// Token: 0x02000102 RID: 258
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0
		{
			// Token: 0x06000763 RID: 1891 RVA: 0x000269DD File Offset: 0x00024BDD
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x06000764 RID: 1892 RVA: 0x000269E5 File Offset: 0x00024BE5
			[NullableContext(0)]
			internal bool <RollAccessory>b__0(RpgAffixDef a)
			{
				return !this.used.Contains(a.Id);
			}

			// Token: 0x06000765 RID: 1893 RVA: 0x000269FB File Offset: 0x00024BFB
			[NullableContext(0)]
			internal bool <RollAccessory>b__1(RpgAffixDef a)
			{
				return !this.used.Contains(a.Id);
			}

			// Token: 0x040003B5 RID: 949
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public HashSet<string> used;
		}
	}
}
