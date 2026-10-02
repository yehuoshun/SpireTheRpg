using System;
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
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Saves.Runs;
using SpireTheRpg.Core;
using SpireTheRpg.Relics;

namespace SpireTheRpg.Combat
{
	// Token: 0x02000079 RID: 121
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RpgScrollReward : CustomReward
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001D42A File Offset: 0x0001B62A
		protected override RewardType RewardType
		{
			get
			{
				return RpgScrollReward.RpgScrollRewardType;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0001D431 File Offset: 0x0001B631
		protected override string IconPath
		{
			get
			{
				if (this._scroll == null || string.IsNullOrWhiteSpace(this._scroll.IconPath))
				{
					return SpireAssets.ItemRelicIconPath;
				}
				return SpireAssets.ResAsset(this._scroll.IconPath);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001D463 File Offset: 0x0001B663
		public override LocString Description
		{
			get
			{
				if (this._scroll == null)
				{
					return new LocString("main_menu_ui", "SPIRE_LOOT.scroll_reward");
				}
				LocString locString = new LocString("main_menu_ui", "SPIRE_LOOT.reward_named");
				locString.Add("Name", this._scroll.DisplayName);
				return locString;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0001D4A2 File Offset: 0x0001B6A2
		public override bool IsPopulated
		{
			get
			{
				return this._scroll != null;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001D4AD File Offset: 0x0001B6AD
		public override CreateRewardFromSave<CustomReward> DeserializeMethod
		{
			get
			{
				CreateRewardFromSave<CustomReward> result;
				if ((result = RpgScrollReward.<>O.<0>__Deserialize) == null)
				{
					result = (RpgScrollReward.<>O.<0>__Deserialize = new CreateRewardFromSave<CustomReward>(RpgScrollReward.Deserialize));
				}
				return result;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0001D4CA File Offset: 0x0001B6CA
		public override Vector2 IconPosition
		{
			get
			{
				return new Vector2(0f, -4f);
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001D4DB File Offset: 0x0001B6DB
		public RpgScrollReward(Player player) : base(player)
		{
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001D4E4 File Offset: 0x0001B6E4
		internal static RpgScrollReward Of(Player player, RpgItemData scroll)
		{
			return new RpgScrollReward(player)
			{
				_scroll = scroll.Clone()
			};
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001D4F8 File Offset: 0x0001B6F8
		public override void Populate()
		{
			if (this._scroll != null)
			{
				return;
			}
			RpgItemData scroll;
			if (RpgPendingExtraRewards.TryApplyScroll(base.Player.NetId, out scroll))
			{
				this._scroll = scroll;
				SpireTheRpgMainFile.Logger.Info("RPG_SC_PENDING " + this._scroll.Id, 1);
				RpgPendingExtraRewards.RememberScroll(base.Player.NetId, this._scroll);
				return;
			}
			this._scroll = RpgScrolls.RollRandom(null);
			Logger logger = SpireTheRpgMainFile.Logger;
			string str = "RPG_SC_FRESH ";
			RpgItemData scroll2 = this._scroll;
			logger.Info(str + (((scroll2 != null) ? scroll2.Id : null) ?? "none"), 1);
			if (this._scroll != null)
			{
				RpgPendingExtraRewards.RememberScroll(base.Player.NetId, this._scroll);
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001D5BA File Offset: 0x0001B7BA
		internal string ExportSaveJson(JsonSerializerOptions options)
		{
			if (this._scroll == null)
			{
				return "";
			}
			return JsonSerializer.Serialize<RpgItemData>(this._scroll.Clone(), options);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001D5DC File Offset: 0x0001B7DC
		internal void ImportSaveJson(string json, JsonSerializerOptions options)
		{
			try
			{
				RpgItemData rpgItemData = JsonSerializer.Deserialize<RpgItemData>(json, options);
				if (rpgItemData != null)
				{
					this._scroll = rpgItemData;
					this._scroll.IsScroll = true;
					this._scroll.SyncVisualFromTemplate();
					SpireTheRpgMainFile.Logger.Info("RpgScrollReward restored id=" + this._scroll.Id, 1);
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RpgScrollReward import failed: " + ex.Message, 1);
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001D664 File Offset: 0x0001B864
		[NullableContext(2)]
		public override Control CreateIcon()
		{
			Texture2D texture2D = null;
			if (this._scroll != null && !string.IsNullOrWhiteSpace(this._scroll.IconPath))
			{
				texture2D = SpireAssets.ItemIcon(this._scroll.IconPath);
			}
			if (texture2D == null)
			{
				texture2D = SpireAssets.RelicIcon("relic/item_relic.png");
			}
			if (texture2D == null)
			{
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

		// Token: 0x0600054E RID: 1358 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
		protected override Task<bool> OnSelect()
		{
			if (this._scroll == null)
			{
				this.Populate();
			}
			if (this._scroll == null)
			{
				return Task.FromResult<bool>(true);
			}
			RpgPendingExtraRewards.RemoveScroll(base.Player.NetId, this._scroll.InstanceId);
			RpgScrollBag.Add(base.Player.NetId, this._scroll);
			if (!RpgNet.IsLocal(base.Player))
			{
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
				SpireTheRpgMainFile.Logger.Info("RpgScrollReward VFX failed: " + ex.Message, 1);
			}
			Logger logger = SpireTheRpgMainFile.Logger;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 2);
			defaultInterpolatedStringHandler.AppendLiteral("RpgScrollReward granted ");
			defaultInterpolatedStringHandler.AppendFormatted(this._scroll.Id);
			defaultInterpolatedStringHandler.AppendLiteral(" net=");
			defaultInterpolatedStringHandler.AppendFormatted<ulong>(base.Player.NetId);
			logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			return Task.FromResult<bool>(true);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001D850 File Offset: 0x0001BA50
		public override void MarkContentAsSeen()
		{
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0001D852 File Offset: 0x0001BA52
		public override SerializableReward ToSerializable()
		{
			return base.ToSerializable();
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001D85A File Offset: 0x0001BA5A
		private static CustomReward Deserialize(SerializableReward save, Player player)
		{
			return new RpgScrollReward(player);
		}

		// Token: 0x040001B4 RID: 436
		[CustomEnum(null)]
		public static RewardType RpgScrollRewardType;

		// Token: 0x040001B5 RID: 437
		[Nullable(2)]
		private RpgItemData _scroll;

		// Token: 0x0200014D RID: 333
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040004DA RID: 1242
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static CreateRewardFromSave<CustomReward> <0>__Deserialize;
		}
	}
}
