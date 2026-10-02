using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using BaseLib.Patches.Content;
using Godot;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;
using SpireTheRpg.Core;
using SpireTheRpg.Relics;

namespace SpireTheRpg.Combat
{
	// Token: 0x0200006E RID: 110
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RpgEquipmentReward : CustomReward
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0001B2D3 File Offset: 0x000194D3
		protected override RewardType RewardType
		{
			get
			{
				return RpgEquipmentReward.RpgEquipmentRewardType;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x0001B2DA File Offset: 0x000194DA
		protected override string IconPath
		{
			get
			{
				if (this._item == null || string.IsNullOrWhiteSpace(this._item.IconPath))
				{
					return SpireAssets.ItemRelicIconPath;
				}
				return SpireAssets.ResAsset(this._item.IconPath);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0001B30C File Offset: 0x0001950C
		public override LocString Description
		{
			get
			{
				if (this._item == null)
				{
					return new LocString("main_menu_ui", "SPIRE_LOOT.reward");
				}
				LocString locString = new LocString("main_menu_ui", "SPIRE_LOOT.reward_named");
				locString.Add("Name", this._item.DisplayName);
				return locString;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0001B34B File Offset: 0x0001954B
		public override bool IsPopulated
		{
			get
			{
				return this._item != null;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001B356 File Offset: 0x00019556
		public override CreateRewardFromSave<CustomReward> DeserializeMethod
		{
			get
			{
				CreateRewardFromSave<CustomReward> result;
				if ((result = RpgEquipmentReward.<>O.<0>__Deserialize) == null)
				{
					result = (RpgEquipmentReward.<>O.<0>__Deserialize = new CreateRewardFromSave<CustomReward>(RpgEquipmentReward.Deserialize));
				}
				return result;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0001B373 File Offset: 0x00019573
		public override Vector2 IconPosition
		{
			get
			{
				return new Vector2(0f, -4f);
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001B384 File Offset: 0x00019584
		public RpgEquipmentReward(Player player, RoomType roomType) : base(player)
		{
			this._roomType = roomType;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001B39B File Offset: 0x0001959B
		internal static RpgEquipmentReward WithRarity(Player player, RoomType roomType, RpgItemRarity rarity)
		{
			return new RpgEquipmentReward(player, roomType)
			{
				_forcedRarity = (int)rarity
			};
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001B3AC File Offset: 0x000195AC
		public override void Populate()
		{
			if (this._item != null)
			{
				return;
			}
			int roomType;
			int forcedRarity;
			RpgItemData item;
			if (RpgPendingExtraRewards.TryApplyEquipment(base.Player.NetId, out roomType, out forcedRarity, out item))
			{
				this._roomType = roomType;
				this._forcedRarity = forcedRarity;
				this._item = item;
				SpireTheRpgMainFile.Logger.Info("RPG_EQ_BUILD_MARKER_v3", 1);
				SpireTheRpgMainFile.Logger.Info("RPG_EQ_PENDING " + this._item.Id, 1);
				RpgPendingExtraRewards.RememberEquipment(base.Player.NetId, this._roomType, this._forcedRarity, this._item);
				return;
			}
			Rng rng = RpgLootTable.LootRng(base.Player, this._roomType, (this._forcedRarity >= 0) ? (this._forcedRarity + 1) : 0);
			this._item = ((this._forcedRarity >= 0) ? RpgLootTable.RollOne((RpgItemRarity)this._forcedRarity, rng) : RpgLootTable.RollChoices(this._roomType, new int?(1), rng).FirstOrDefault<RpgItemData>());
			Logger logger = SpireTheRpgMainFile.Logger;
			string str = "RPG_EQ_FRESH ";
			RpgItemData item2 = this._item;
			logger.Info(str + (((item2 != null) ? item2.Id : null) ?? "none"), 1);
			if (this._item != null)
			{
				RpgPendingExtraRewards.RememberEquipment(base.Player.NetId, this._roomType, this._forcedRarity, this._item);
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001B4F6 File Offset: 0x000196F6
		[NullableContext(2)]
		internal string PeekItemId()
		{
			RpgItemData item = this._item;
			if (item == null)
			{
				return null;
			}
			return item.Id;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001B50C File Offset: 0x0001970C
		internal string ExportSaveJson(JsonSerializerOptions options)
		{
			if (this._item == null)
			{
				return "";
			}
			return JsonSerializer.Serialize<RpgEquipmentRewardSave>(new RpgEquipmentRewardSave
			{
				RoomType = this._roomType,
				ForcedRarity = this._forcedRarity,
				Item = this._item.Clone()
			}, options);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001B55C File Offset: 0x0001975C
		internal void ImportSaveJson(string json, JsonSerializerOptions options)
		{
			try
			{
				RpgEquipmentRewardSave rpgEquipmentRewardSave = JsonSerializer.Deserialize<RpgEquipmentRewardSave>(json, options);
				if (((rpgEquipmentRewardSave != null) ? rpgEquipmentRewardSave.Item : null) != null)
				{
					this._roomType = rpgEquipmentRewardSave.RoomType;
					this._forcedRarity = rpgEquipmentRewardSave.ForcedRarity;
					this._item = rpgEquipmentRewardSave.Item;
					this._item.SyncVisualFromTemplate();
					Logger logger = SpireTheRpgMainFile.Logger;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(51, 3);
					defaultInterpolatedStringHandler.AppendLiteral("RpgEquipmentReward restored id=");
					defaultInterpolatedStringHandler.AppendFormatted(this._item.Id);
					defaultInterpolatedStringHandler.AppendLiteral(" rarity=");
					defaultInterpolatedStringHandler.AppendFormatted<RpgItemRarity>(this._item.Rarity);
					defaultInterpolatedStringHandler.AppendLiteral(" identified=");
					defaultInterpolatedStringHandler.AppendFormatted<bool>(this._item.IsIdentified);
					logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RpgEquipmentReward import failed: " + ex.Message, 1);
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001B658 File Offset: 0x00019858
		[NullableContext(2)]
		public override Control CreateIcon()
		{
			Texture2D texture2D = null;
			if (this._item != null && !string.IsNullOrWhiteSpace(this._item.IconPath))
			{
				texture2D = SpireAssets.ItemIcon(this._item.IconPath);
			}
			if (texture2D == null)
			{
				texture2D = SpireAssets.RelicIcon("relic/item_relic.png");
			}
			if (texture2D == null)
			{
				SpireTheRpgMainFile.Logger.Info("RpgEquipmentReward icon missing, falling back", 1);
				return base.CreateIcon();
			}
			TextureRect textureRect = new TextureRect();
			textureRect.Texture = texture2D;
			textureRect.ExpandMode = 1L;
			textureRect.StretchMode = 5L;
			textureRect.MouseFilter = 2L;
			textureRect.SetAnchorsAndOffsetsPreset(15L, 0L, 0);
			return textureRect;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001B6EC File Offset: 0x000198EC
		protected override Task<bool> OnSelect()
		{
			if (this._item == null)
			{
				this.Populate();
			}
			if (this._item == null)
			{
				SpireTheRpgMainFile.Logger.Info("RpgEquipmentReward OnSelect: no item", 1);
				return Task.FromResult<bool>(true);
			}
			RpgPendingExtraRewards.RemoveEquipment(base.Player.NetId, this._item.InstanceId);
			if (RpgEffectiveConfig.EnableMetaEquipment)
			{
				RpgRunLoot.Add(base.Player.NetId, this._item);
			}
			else
			{
				RpgRunLoadout.AddToBag(base.Player.NetId, this._item);
			}
			RpgAccessoryCombatHooks.OnEquipmentGranted(this._item, base.Player);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			if (!RpgNet.IsLocal(base.Player))
			{
				Logger logger = SpireTheRpgMainFile.Logger;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 2);
				defaultInterpolatedStringHandler.AppendLiteral("RpgEquipmentReward OnSelect remote bag net=");
				defaultInterpolatedStringHandler.AppendFormatted<ulong>(base.Player.NetId);
				defaultInterpolatedStringHandler.AppendLiteral(" id=");
				defaultInterpolatedStringHandler.AppendFormatted(this._item.Id);
				logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
				return Task.FromResult<bool>(true);
			}
			RpgItemRelic relic = base.Player.GetRelic<RpgItemRelic>();
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
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RpgEquipmentReward VFX failed: " + ex.Message, 1);
			}
			int value = RpgEffectiveConfig.EnableMetaEquipment ? RpgRunLoot.CountOf(base.Player.NetId) : RpgRunLoadout.BagCountOf(base.Player.NetId);
			Logger logger2 = SpireTheRpgMainFile.Logger;
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(45, 4);
			defaultInterpolatedStringHandler.AppendLiteral("RpgEquipmentReward granted ");
			defaultInterpolatedStringHandler.AppendFormatted(this._item.Id);
			defaultInterpolatedStringHandler.AppendLiteral(" total=");
			defaultInterpolatedStringHandler.AppendFormatted<int>(value);
			defaultInterpolatedStringHandler.AppendLiteral(" meta=");
			defaultInterpolatedStringHandler.AppendFormatted<bool>(RpgEffectiveConfig.EnableMetaEquipment);
			defaultInterpolatedStringHandler.AppendLiteral(" net=");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(base.Player.NetId);
			logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			return Task.FromResult<bool>(true);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001B944 File Offset: 0x00019B44
		public override void MarkContentAsSeen()
		{
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001B946 File Offset: 0x00019B46
		public override SerializableReward ToSerializable()
		{
			SerializableReward serializableReward = base.ToSerializable();
			serializableReward.OptionCount = this._roomType;
			return serializableReward;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001B95A File Offset: 0x00019B5A
		private static CustomReward Deserialize(SerializableReward save, Player player)
		{
			return new RpgEquipmentReward(player, save.OptionCount);
		}

		// Token: 0x0400019D RID: 413
		public const string BuildMarker = "RPG_EQ_BUILD_MARKER_v3";

		// Token: 0x0400019E RID: 414
		[CustomEnum(null)]
		public static RewardType RpgEquipmentRewardType;

		// Token: 0x0400019F RID: 415
		private RoomType _roomType;

		// Token: 0x040001A0 RID: 416
		private int _forcedRarity = -1;

		// Token: 0x040001A1 RID: 417
		[Nullable(2)]
		private RpgItemData _item;

		// Token: 0x02000144 RID: 324
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040004C1 RID: 1217
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static CreateRewardFromSave<CustomReward> <0>__Deserialize;
		}
	}
}
