using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.RestSite;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace SpireTheRpg.Patches
{
	// Token: 0x02000030 RID: 48
	[NullableContext(1)]
	[Nullable(0)]
	[HarmonyPatch(typeof(NRestSiteRoom), "UpdateRestSiteOptions")]
	internal static class RpgRestSiteOptionsLayoutPatch
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x0000F460 File Offset: 0x0000D660
		private static void Postfix(NRestSiteRoom __instance)
		{
			try
			{
				Control control = RpgRestSiteOptionsLayoutPatch.ResolveContainer(__instance);
				if (control != null && GodotObject.IsInstanceValid(control))
				{
					List<NRestSiteButton> list = control.GetChildren(false).OfType<NRestSiteButton>().ToList<NRestSiteButton>();
					if (list.Count != 0)
					{
						if (list.Count < 5)
						{
							HBoxContainer hboxContainer = control as HBoxContainer;
							if (hboxContainer != null)
							{
								hboxContainer.AddThemeConstantOverride("separation", 56);
							}
							RpgRestSiteOptionsLayoutPatch.EnableMouse(list);
							RpgRestSiteOptionsLayoutPatch.WireFocus(list, list.Count);
						}
						else
						{
							GridContainer gridContainer = (control as GridContainer) ?? RpgRestSiteOptionsLayoutPatch.CreateGridReplacing(__instance, control, list);
							if (gridContainer != null)
							{
								list = gridContainer.GetChildren(false).OfType<NRestSiteButton>().ToList<NRestSiteButton>();
								gridContainer.Columns = ((list.Count <= 6) ? 3 : 4);
								gridContainer.AddThemeConstantOverride("h_separation", 28);
								gridContainer.AddThemeConstantOverride("v_separation", 56);
								RpgRestSiteOptionsLayoutPatch.ExpandBounds(gridContainer, gridContainer.Columns, list.Count);
								RpgRestSiteOptionsLayoutPatch.PushDescriptionDown(__instance, gridContainer);
								RpgRestSiteOptionsLayoutPatch.EnableMouse(list);
								RpgRestSiteOptionsLayoutPatch.WireFocus(list, gridContainer.Columns);
								gridContainer.MoveToFront();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RpgRestSiteOptionsLayoutPatch failed: " + ex.Message, 1);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000F5B4 File Offset: 0x0000D7B4
		[return: Nullable(2)]
		private static Control ResolveContainer(NRestSiteRoom room)
		{
			FieldInfo fieldInfo = AccessTools.Field(typeof(NRestSiteRoom), "_choicesContainer");
			Control control = ((fieldInfo != null) ? fieldInfo.GetValue(room) : null) as Control;
			if (control != null && GodotObject.IsInstanceValid(control))
			{
				return control;
			}
			return room.GetNodeOrNull<Control>("%ChoicesContainer");
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000F608 File Offset: 0x0000D808
		private static void EnableMouse(IEnumerable<NRestSiteButton> buttons)
		{
			foreach (NRestSiteButton nrestSiteButton in buttons)
			{
				if (GodotObject.IsInstanceValid(nrestSiteButton))
				{
					nrestSiteButton.MouseFilter = 0L;
					nrestSiteButton.Modulate = Colors.White;
				}
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000F664 File Offset: 0x0000D864
		private static void PushDescriptionDown(NRestSiteRoom room, Control grid)
		{
			Control nodeOrNull = room.GetNodeOrNull<Control>("%Description");
			if (nodeOrNull == null || !GodotObject.IsInstanceValid(nodeOrNull))
			{
				return;
			}
			float num = grid.OffsetBottom + 48f;
			float num2 = Math.Max(280f, nodeOrNull.OffsetBottom - nodeOrNull.OffsetTop);
			nodeOrNull.OffsetTop = num;
			nodeOrNull.OffsetBottom = num + num2;
			FieldInfo fieldInfo = AccessTools.Field(typeof(NRestSiteRoom), "_originalDescriptionYPos");
			if (fieldInfo != null)
			{
				fieldInfo.SetValue(room, nodeOrNull.Position.Y);
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		[return: Nullable(2)]
		private static GridContainer CreateGridReplacing(NRestSiteRoom room, Control oldContainer, List<NRestSiteButton> buttons)
		{
			Node parent = oldContainer.GetParent();
			if (parent == null)
			{
				return null;
			}
			int index = oldContainer.GetIndex(false);
			GridContainer gridContainer = new GridContainer
			{
				Name = "ChoicesContainer",
				UniqueNameInOwner = true,
				MouseFilter = 2L,
				LayoutMode = oldContainer.LayoutMode,
				AnchorsPreset = oldContainer.AnchorsPreset,
				AnchorLeft = 0.5f,
				AnchorTop = 0.5f,
				AnchorRight = 0.5f,
				AnchorBottom = 0.5f,
				GrowHorizontal = 2L,
				GrowVertical = 2L
			};
			foreach (NRestSiteButton nrestSiteButton in buttons)
			{
				oldContainer.RemoveChild(nrestSiteButton);
				gridContainer.AddChild(nrestSiteButton, false, 0L);
			}
			oldContainer.UniqueNameInOwner = false;
			oldContainer.Name = "ChoicesContainer_Old";
			parent.AddChild(gridContainer, false, 0L);
			parent.MoveChild(gridContainer, index);
			FieldInfo fieldInfo = AccessTools.Field(typeof(NRestSiteRoom), "_choicesContainer");
			if (fieldInfo != null)
			{
				fieldInfo.SetValue(room, gridContainer);
			}
			oldContainer.QueueFree();
			return gridContainer;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000F834 File Offset: 0x0000DA34
		private static void ExpandBounds(Control grid, int columns, int count)
		{
			int num = (count + columns - 1) / columns;
			float num2 = Math.Max(480f, (float)columns * 145f);
			float num3 = (float)num * 160f + (float)(Math.Max(0, num - 1) * 56);
			grid.OffsetLeft = -num2;
			grid.OffsetRight = num2;
			grid.OffsetTop = -260f;
			grid.OffsetBottom = grid.OffsetTop + num3;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000F89C File Offset: 0x0000DA9C
		private static void WireFocus(IReadOnlyList<NRestSiteButton> buttons, int columns)
		{
			if (buttons.Count == 0 || columns <= 0)
			{
				return;
			}
			for (int i = 0; i < buttons.Count; i++)
			{
				NRestSiteButton nrestSiteButton = buttons[i];
				int num = i % columns;
				int num2 = i / columns * columns;
				int num3 = Math.Min(columns, buttons.Count - num2);
				int index = num2 + (num - 1 + num3) % num3;
				int index2 = num2 + (num + 1) % num3;
				int num4 = i - columns;
				if (num4 < 0)
				{
					num4 = i;
				}
				int num5 = i + columns;
				if (num5 >= buttons.Count)
				{
					num5 = i;
				}
				nrestSiteButton.FocusNeighborLeft = buttons[index].GetPath();
				nrestSiteButton.FocusNeighborRight = buttons[index2].GetPath();
				nrestSiteButton.FocusNeighborTop = buttons[num4].GetPath();
				nrestSiteButton.FocusNeighborBottom = buttons[num5].GetPath();
			}
		}

		// Token: 0x04000093 RID: 147
		private const int WrapAt = 5;

		// Token: 0x04000094 RID: 148
		private const int Columns = 3;

		// Token: 0x04000095 RID: 149
		private const int RowGap = 56;
	}
}
