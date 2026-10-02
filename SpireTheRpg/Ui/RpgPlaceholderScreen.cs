using System;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Nodes.Screens.MainMenu;

namespace SpireTheRpg.Ui
{
	// Token: 0x02000010 RID: 16
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgPlaceholderScreen
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x0000ADAC File Offset: 0x00008FAC
		public static void Hide(NCompendiumSubmenu hub)
		{
			Node nodeOrNull = hub.GetNodeOrNull<Node>("SpireRpgPlaceholderOverlay");
			if (nodeOrNull == null)
			{
				return;
			}
			nodeOrNull.QueueFree();
		}

		// Token: 0x04000064 RID: 100
		private const string OverlayName = "SpireRpgPlaceholderOverlay";
	}
}
