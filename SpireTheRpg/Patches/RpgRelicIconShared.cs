using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Relics;
using SpireTheRpg.Core;
using SpireTheRpg.Relics;

namespace SpireTheRpg.Patches
{
	// Token: 0x0200002A RID: 42
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgRelicIconShared
	{
		// Token: 0x0600019A RID: 410 RVA: 0x0000F240 File Offset: 0x0000D440
		[NullableContext(2)]
		public static bool IsOurRelic(RelicModel model)
		{
			return model is RpgDataRelic || model is RpgExpRelic || model is RpgItemRelic || model is RpgRandomItemShopRelic || model is RpgEquippedGearRelic;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000F27C File Offset: 0x0000D47C
		[return: Nullable(2)]
		public static RelicModel TryGetModel(NRelic node)
		{
			try
			{
				FieldInfo modelField = RpgRelicIconShared.ModelField;
				RelicModel relicModel = ((modelField != null) ? modelField.GetValue(node) : null) as RelicModel;
				if (relicModel != null)
				{
					return relicModel;
				}
			}
			catch
			{
			}
			RelicModel result;
			try
			{
				result = node.Model;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000F2DC File Offset: 0x0000D4DC
		[return: Nullable(2)]
		public static Texture2D TextureFor(RelicModel model)
		{
			RpgEquippedGearRelic rpgEquippedGearRelic = model as RpgEquippedGearRelic;
			if (rpgEquippedGearRelic != null)
			{
				RpgItemData item = rpgEquippedGearRelic.Item;
				if (item != null)
				{
					return SpireAssets.ItemIcon(item.IconPath) ?? SpireAssets.RelicIcon("relic/item_relic.png");
				}
			}
			else
			{
				if (model is RpgDataRelic)
				{
					return SpireAssets.RelicIcon("relic/data_relic.png");
				}
				if (model is RpgExpRelic)
				{
					return SpireAssets.RelicIcon("relic/exp_relic.png");
				}
				if (model is RpgItemRelic || model is RpgRandomItemShopRelic)
				{
					return SpireAssets.RelicIcon("relic/item_relic.png");
				}
			}
			return null;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000F368 File Offset: 0x0000D568
		public static bool TryOverride(RelicModel instance, ref Texture2D result)
		{
			if (!RpgRelicIconShared.IsOurRelic(instance))
			{
				return false;
			}
			Texture2D texture2D = RpgRelicIconShared.TextureFor(instance);
			if (texture2D == null)
			{
				return false;
			}
			result = texture2D;
			return true;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000F390 File Offset: 0x0000D590
		public static void ApplyToNode(NRelic node)
		{
			RelicModel relicModel = RpgRelicIconShared.TryGetModel(node);
			if (!RpgRelicIconShared.IsOurRelic(relicModel) || relicModel == null)
			{
				return;
			}
			Texture2D texture2D = RpgRelicIconShared.TextureFor(relicModel);
			if (texture2D == null)
			{
				return;
			}
			if (node.Icon != null)
			{
				node.Icon.Texture = texture2D;
			}
			if (node.Outline != null)
			{
				node.Outline.Texture = texture2D;
				node.Outline.Visible = false;
			}
			RpgItemRelicClickWiring.Wire(node, relicModel);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000F3F6 File Offset: 0x0000D5F6
		// Note: this type is marked as 'beforefieldinit'.
		static RpgRelicIconShared()
		{
		}

		// Token: 0x04000092 RID: 146
		[Nullable(2)]
		private static readonly FieldInfo ModelField = typeof(NRelic).GetField("_model", BindingFlags.Instance | BindingFlags.NonPublic);
	}
}
