using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SpireTheRpg.Core
{
	// Token: 0x02000053 RID: 83
	internal static class RpgProgress
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00016788 File Offset: 0x00014988
		public static int Level
		{
			get
			{
				RpgProgress.Ensure();
				return RpgProgress._level;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00016794 File Offset: 0x00014994
		public static int Xp
		{
			get
			{
				RpgProgress.Ensure();
				return RpgProgress._xp;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000346 RID: 838 RVA: 0x000167A0 File Offset: 0x000149A0
		public static int XpToNext
		{
			get
			{
				RpgProgress.Ensure();
				return RpgProgress.XpNeededFor(RpgProgress._level);
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000167B4 File Offset: 0x000149B4
		public static void Ensure()
		{
			if (RpgProgress._loaded)
			{
				return;
			}
			RpgSaveDto rpgSaveDto = RpgSaveStore.Load();
			RpgProgress._level = Math.Max(1, rpgSaveDto.Level);
			RpgProgress._xp = Math.Max(0, rpgSaveDto.Xp);
			RpgProgress.NormalizeLevel();
			RpgProgress._loaded = true;
			RpgLoadout.LoadFromSave(rpgSaveDto);
			if (rpgSaveDto.Scrolls.Count > 0)
			{
				RpgScrollBag.Load(RpgNet.LocalNetId(), rpgSaveDto.Scrolls);
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00016820 File Offset: 0x00014A20
		public static int AddXp(int amount)
		{
			RpgProgress.Ensure();
			if (amount <= 0)
			{
				return 0;
			}
			RpgProgress._xp += amount;
			int num = 0;
			while (RpgProgress._xp >= RpgProgress.XpNeededFor(RpgProgress._level))
			{
				RpgProgress._xp -= RpgProgress.XpNeededFor(RpgProgress._level);
				RpgProgress._level++;
				num++;
			}
			RpgProgress.Persist();
			return num;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00016884 File Offset: 0x00014A84
		public static void Persist()
		{
			RpgProgress.Ensure();
			RpgSaveDto rpgSaveDto = new RpgSaveDto();
			rpgSaveDto.Level = RpgProgress._level;
			rpgSaveDto.Xp = RpgProgress._xp;
			rpgSaveDto.BagItems = (from x in RpgLoadout.Bag
			select x.Clone()).ToList<RpgItemData>();
			rpgSaveDto.EquippedItems = Enum.GetValues<RpgEquipSlot>().ToDictionary((RpgEquipSlot s) => s.ToString(), delegate(RpgEquipSlot s)
			{
				RpgItemData equipped = RpgLoadout.GetEquipped(s);
				if (equipped == null)
				{
					return null;
				}
				return equipped.Clone();
			}, StringComparer.OrdinalIgnoreCase);
			rpgSaveDto.Scrolls = RpgScrollBag.Snapshot(RpgNet.LocalNetId());
			RpgSaveStore.Save(rpgSaveDto);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0001694D File Offset: 0x00014B4D
		public static int StatBonus()
		{
			return Math.Max(0, RpgProgress.Level - 1);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001695C File Offset: 0x00014B5C
		private static void NormalizeLevel()
		{
			while (RpgProgress._xp >= RpgProgress.XpNeededFor(RpgProgress._level))
			{
				RpgProgress._xp -= RpgProgress.XpNeededFor(RpgProgress._level);
				RpgProgress._level++;
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00016992 File Offset: 0x00014B92
		private static int XpNeededFor(int level)
		{
			return 100 + Math.Max(1, level) * 20;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000169A1 File Offset: 0x00014BA1
		// Note: this type is marked as 'beforefieldinit'.
		static RpgProgress()
		{
		}

		// Token: 0x0400010E RID: 270
		private static int _level = 1;

		// Token: 0x0400010F RID: 271
		private static int _xp;

		// Token: 0x04000110 RID: 272
		private static bool _loaded;

		// Token: 0x02000113 RID: 275
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060007A2 RID: 1954 RVA: 0x00027A64 File Offset: 0x00025C64
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060007A3 RID: 1955 RVA: 0x00027A70 File Offset: 0x00025C70
			public <>c()
			{
			}

			// Token: 0x060007A4 RID: 1956 RVA: 0x00027A78 File Offset: 0x00025C78
			internal RpgItemData <Persist>b__11_0(RpgItemData x)
			{
				return x.Clone();
			}

			// Token: 0x060007A5 RID: 1957 RVA: 0x00027A80 File Offset: 0x00025C80
			[NullableContext(1)]
			internal string <Persist>b__11_1(RpgEquipSlot s)
			{
				return s.ToString();
			}

			// Token: 0x060007A6 RID: 1958 RVA: 0x00027A8F File Offset: 0x00025C8F
			internal RpgItemData <Persist>b__11_2(RpgEquipSlot s)
			{
				RpgItemData equipped = RpgLoadout.GetEquipped(s);
				if (equipped == null)
				{
					return null;
				}
				return equipped.Clone();
			}

			// Token: 0x040003F5 RID: 1013
			public static readonly RpgProgress.<>c <>9 = new RpgProgress.<>c();

			// Token: 0x040003F6 RID: 1014
			public static Func<RpgItemData, RpgItemData> <>9__11_0;

			// Token: 0x040003F7 RID: 1015
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Func<RpgEquipSlot, string> <>9__11_1;

			// Token: 0x040003F8 RID: 1016
			public static Func<RpgEquipSlot, RpgItemData> <>9__11_2;
		}
	}
}
