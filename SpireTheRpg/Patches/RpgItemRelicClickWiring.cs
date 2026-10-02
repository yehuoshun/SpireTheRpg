using System;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Relics;
using SpireTheRpg.Relics;
using SpireTheRpg.Ui;

namespace SpireTheRpg.Patches
{
	// Token: 0x02000026 RID: 38
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgItemRelicClickWiring
	{
		// Token: 0x0600018E RID: 398 RVA: 0x0000EF84 File Offset: 0x0000D184
		public static void Wire(NRelic node, [Nullable(2)] RelicModel model)
		{
			if (!(model is RpgItemRelic))
			{
				return;
			}
			if (node.HasMeta("spire_rpg_inv_click"))
			{
				return;
			}
			node.SetMeta("spire_rpg_inv_click", true);
			node.GuiInput += delegate(InputEvent e)
			{
				InputEventMouseButton inputEventMouseButton = e as InputEventMouseButton;
				if (inputEventMouseButton == null || !inputEventMouseButton.Pressed || inputEventMouseButton.ButtonIndex != 1L)
				{
					return;
				}
				if (!RpgInRunInventory.CanOpen())
				{
					return;
				}
				RpgInRunInventory.Toggle();
				node.AcceptEvent();
			};
		}

		// Token: 0x0400008E RID: 142
		private const string MetaKey = "spire_rpg_inv_click";

		// Token: 0x020000CB RID: 203
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x0600069E RID: 1694 RVA: 0x00022556 File Offset: 0x00020756
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x0600069F RID: 1695 RVA: 0x00022560 File Offset: 0x00020760
			[NullableContext(0)]
			internal void <Wire>b__0(InputEvent e)
			{
				InputEventMouseButton inputEventMouseButton = e as InputEventMouseButton;
				if (inputEventMouseButton == null || !inputEventMouseButton.Pressed || inputEventMouseButton.ButtonIndex != 1L)
				{
					return;
				}
				if (!RpgInRunInventory.CanOpen())
				{
					return;
				}
				RpgInRunInventory.Toggle();
				this.node.AcceptEvent();
			}

			// Token: 0x040002CF RID: 719
			[Nullable(0)]
			public NRelic node;
		}
	}
}
