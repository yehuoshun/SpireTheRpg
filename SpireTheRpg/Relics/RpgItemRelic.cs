using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rooms;
using SpireTheRpg.Combat;
using SpireTheRpg.Core;

namespace SpireTheRpg.Relics
{
	// Token: 0x0200001A RID: 26
	[NullableContext(1)]
	[Nullable(0)]
	[Pool(typeof(EventRelicPool))]
	public sealed class RpgItemRelic : CustomRelicModel
	{
		// Token: 0x0600013B RID: 315 RVA: 0x0000D73E File Offset: 0x0000B93E
		public RpgItemRelic() : base(true)
		{
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000D747 File Offset: 0x0000B947
		public override RelicRarity Rarity
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000D74A File Offset: 0x0000B94A
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000D74D File Offset: 0x0000B94D
		public override int DisplayAmount
		{
			get
			{
				if (!RpgEffectiveConfig.EnableMetaEquipment)
				{
					return RpgRunLoadout.BagCountOf(RpgNet.NetIdOf(base.Owner));
				}
				return RpgRunLoot.CountOf(RpgNet.NetIdOf(base.Owner));
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000D778 File Offset: 0x0000B978
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("Amount", 0m),
					new StringVar("LootList", ""),
					new StringVar("Hotkey", "I"),
					new StringVar("OpenHint", "")
				});
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000D7D8 File Offset: 0x0000B9D8
		public override string PackedIconPath
		{
			get
			{
				return SpireAssets.ItemRelicIconPath;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000D7DF File Offset: 0x0000B9DF
		protected override string PackedIconOutlinePath
		{
			get
			{
				return SpireAssets.ItemRelicIconPath;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000D7E6 File Offset: 0x0000B9E6
		protected override string BigIconPath
		{
			get
			{
				return SpireAssets.ItemRelicIconPath;
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
		public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
		{
			if (base.Owner == null || base.Owner != player)
			{
				return false;
			}
			SpireLoc.ForceInject();
			options.Add(new RpgIntensifyRestSiteOption(player));
			options.Add(new RpgFusionRestSiteOption(player));
			if (RpgEffectiveConfig.EnableCurses)
			{
				options.Add(new RpgCleanseRestSiteOption(player));
			}
			if (RpgEffectiveConfig.EnableTrade)
			{
				options.Add(new RpgTradeRestSiteOption(player));
			}
			return true;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000D854 File Offset: 0x0000BA54
		public override Task AfterObtained()
		{
			SpireLoc.ForceInject();
			this.SyncAmountPublic();
			return Task.CompletedTask;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000D868 File Offset: 0x0000BA68
		public override Task AfterCombatVictory(CombatRoom room)
		{
			if (base.Owner == null)
			{
				return Task.CompletedTask;
			}
			try
			{
				RpgPendingExtraRewards.Clear(base.Owner.NetId);
				Rng rng = RpgLootTable.DropRng(base.Owner, room.RoomType);
				if (!RpgLootTable.RollDrop(room.RoomType, rng))
				{
					Logger logger = SpireTheRpgMainFile.Logger;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 3);
					defaultInterpolatedStringHandler.AppendLiteral("Rpg ExtraReward miss room=");
					defaultInterpolatedStringHandler.AppendFormatted<RoomType>(room.RoomType);
					defaultInterpolatedStringHandler.AppendLiteral(" chance=");
					defaultInterpolatedStringHandler.AppendFormatted<double>(RpgLootTable.DropChance(room.RoomType), "P0");
					defaultInterpolatedStringHandler.AppendLiteral(" net=");
					defaultInterpolatedStringHandler.AppendFormatted<ulong>(base.Owner.NetId);
					logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
					return Task.CompletedTask;
				}
				if (RpgLootTable.IsMonsterAllRarities(room.RoomType))
				{
					foreach (RpgItemRarity rarity in RpgLootTable.AllRarities)
					{
						RpgEquipmentReward rpgEquipmentReward = RpgEquipmentReward.WithRarity(base.Owner, room.RoomType, rarity);
						rpgEquipmentReward.Populate();
						room.AddExtraReward(base.Owner, rpgEquipmentReward);
					}
					Logger logger2 = SpireTheRpgMainFile.Logger;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 3);
					defaultInterpolatedStringHandler.AppendLiteral("Rpg ExtraReward all-rarities count=");
					defaultInterpolatedStringHandler.AppendFormatted<int>(RpgLootTable.AllRarities.Length);
					defaultInterpolatedStringHandler.AppendLiteral(" room=");
					defaultInterpolatedStringHandler.AppendFormatted<RoomType>(room.RoomType);
					defaultInterpolatedStringHandler.AppendLiteral(" net=");
					defaultInterpolatedStringHandler.AppendFormatted<ulong>(base.Owner.NetId);
					logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
				}
				else
				{
					RpgEquipmentReward rpgEquipmentReward2 = new RpgEquipmentReward(base.Owner, room.RoomType);
					rpgEquipmentReward2.Populate();
					room.AddExtraReward(base.Owner, rpgEquipmentReward2);
					SpireTheRpgMainFile.Logger.Info("RPG_EQ_ADDED " + (rpgEquipmentReward2.PeekItemId() ?? "none"), 1);
				}
				RpgItemData rpgItemData;
				if (RpgScrolls.TryDrop(base.Owner, room.RoomType, rng, out rpgItemData) && rpgItemData != null)
				{
					room.AddExtraReward(base.Owner, RpgScrollReward.Of(base.Owner, rpgItemData));
				}
				RpgPendingExtraRewards.CaptureFromRoom(base.Owner, room);
				SpireTheRpgMainFile.Logger.Info("RPG_PENDING_CAPTURED", 1);
			}
			catch (Exception value)
			{
				Logger logger3 = SpireTheRpgMainFile.Logger;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(24, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Rpg ExtraReward failed: ");
				defaultInterpolatedStringHandler.AppendFormatted<Exception>(value);
				logger3.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			}
			return Task.CompletedTask;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000DAF4 File Offset: 0x0000BCF4
		public void SyncAmountPublic()
		{
			if (base.IsCanonical)
			{
				return;
			}
			int displayAmount = this.DisplayAmount;
			DynamicVar dynamicVar = base.DynamicVars["Amount"];
			if (dynamicVar != null)
			{
				dynamicVar.BaseValue = displayAmount;
			}
			StringVar stringVar = base.DynamicVars["LootList"] as StringVar;
			if (stringVar != null)
			{
				stringVar.StringValue = (RpgEffectiveConfig.EnableMetaEquipment ? RpgRunLoot.NamesSummary(8) : "");
			}
			string text = RpgInputBindings.DisplayHotkeyLabel();
			StringVar stringVar2 = base.DynamicVars["Hotkey"] as StringVar;
			if (stringVar2 != null)
			{
				stringVar2.StringValue = text;
			}
			StringVar stringVar3 = base.DynamicVars["OpenHint"] as StringVar;
			if (stringVar3 != null)
			{
				stringVar3.StringValue = (RpgEffectiveConfig.EnableMetaEquipment ? "" : SpireLoc.T("SPIRE_INVENTORY.open_hint", "Click or press {0} to open run inventory (cleared at run end).").Replace("{0}", text));
			}
			base.InvokeDisplayAmountChanged();
		}
	}
}
