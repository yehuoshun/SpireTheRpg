using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.RestSite;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using SpireTheRpg.Core;

namespace SpireTheRpg.Combat
{
	// Token: 0x02000076 RID: 118
	[NullableContext(2)]
	[Nullable(0)]
	internal static class RpgRestSiteForgeVfx
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x0001CABE File Offset: 0x0001ACBE
		public static void RememberItem(RpgItemData item)
		{
			RpgRestSiteForgeVfx._pendingIconPath = ((item != null) ? item.IconPath : null);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001CAD1 File Offset: 0x0001ACD1
		public static Texture2D TakePendingIcon()
		{
			string pendingIconPath = RpgRestSiteForgeVfx._pendingIconPath;
			RpgRestSiteForgeVfx._pendingIconPath = null;
			return SpireAssets.ItemIcon(pendingIconPath);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001CAE4 File Offset: 0x0001ACE4
		[NullableContext(1)]
		public static Task PlayLocal([Nullable(2)] Texture2D icon, CancellationToken ct = default(CancellationToken))
		{
			RpgRestSiteForgeVfx.<PlayLocal>d__4 <PlayLocal>d__;
			<PlayLocal>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<PlayLocal>d__.icon = icon;
			<PlayLocal>d__.ct = ct;
			<PlayLocal>d__.<>1__state = -1;
			<PlayLocal>d__.<>t__builder.Start<RpgRestSiteForgeVfx.<PlayLocal>d__4>(ref <PlayLocal>d__);
			return <PlayLocal>d__.<>t__builder.Task;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001CB30 File Offset: 0x0001AD30
		[NullableContext(1)]
		public static Task PlayRemote(Player owner)
		{
			NRestSiteRoom instance = NRestSiteRoom.Instance;
			NRestSiteCharacter nrestSiteCharacter = (instance != null) ? instance.Characters.FirstOrDefault((NRestSiteCharacter c) => c.Player == owner) : null;
			NCardSmithVfx ncardSmithVfx = NCardSmithVfx.Create();
			if (ncardSmithVfx == null)
			{
				return Task.CompletedTask;
			}
			if (nrestSiteCharacter != null)
			{
				GodotTreeExtensions.AddChildSafely(nrestSiteCharacter, ncardSmithVfx);
				ncardSmithVfx.Position = Vector2.Zero;
			}
			else
			{
				NRestSiteRoom instance2 = NRestSiteRoom.Instance;
				if (instance2 != null)
				{
					GodotTreeExtensions.AddChildSafely(instance2, ncardSmithVfx);
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x040001AD RID: 429
		private const float IconSize = 280f;

		// Token: 0x040001AE RID: 430
		private static string _pendingIconPath;

		// Token: 0x0200014A RID: 330
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x0600084D RID: 2125 RVA: 0x0002B602 File Offset: 0x00029802
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x0600084E RID: 2126 RVA: 0x0002B60A File Offset: 0x0002980A
			[NullableContext(0)]
			internal bool <PlayRemote>b__0(NRestSiteCharacter c)
			{
				return c.Player == this.owner;
			}

			// Token: 0x040004CF RID: 1231
			[Nullable(0)]
			public Player owner;
		}

		// Token: 0x0200014B RID: 331
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <PlayLocal>d__4 : IAsyncStateMachine
		{
			// Token: 0x0600084F RID: 2127 RVA: 0x0002B61C File Offset: 0x0002981C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					TaskAwaiter awaiter;
					if (num != 0)
					{
						if (num != 1)
						{
							NRun instance = NRun.Instance;
							Control control;
							if (instance == null)
							{
								control = null;
							}
							else
							{
								NGlobalUi globalUi = instance.GlobalUi;
								control = ((globalUi != null) ? globalUi.CardPreviewContainer : null);
							}
							Control control2 = control;
							NCardSmithVfx ncardSmithVfx = NCardSmithVfx.Create();
							if (ncardSmithVfx == null)
							{
								awaiter = Cmd.CustomScaledWait(1f, 2f, false, this.ct).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgRestSiteForgeVfx.<PlayLocal>d__4>(ref awaiter, ref this);
									return;
								}
								goto IL_9F;
							}
							else
							{
								TextureRect textureRect = null;
								if (this.icon != null)
								{
									Control nodeOrNull = ncardSmithVfx.GetNodeOrNull<Control>("%CardContainer");
									if (nodeOrNull != null)
									{
										textureRect = new TextureRect
										{
											Texture = this.icon,
											ExpandMode = 1L,
											StretchMode = 5L,
											CustomMinimumSize = new Vector2(280f, 280f),
											Size = new Vector2(280f, 280f),
											PivotOffset = new Vector2(140f, 140f),
											MouseFilter = 2L,
											Scale = Vector2.Zero
										};
										textureRect.SetAnchorsPreset(8L, false);
										textureRect.GrowHorizontal = 2L;
										textureRect.GrowVertical = 2L;
										textureRect.OffsetLeft = -140f;
										textureRect.OffsetTop = -140f;
										textureRect.OffsetRight = 140f;
										textureRect.OffsetBottom = 140f;
										nodeOrNull.AddChild(textureRect, false, 0L);
									}
								}
								if (control2 != null && GodotObject.IsInstanceValid(control2))
								{
									GodotTreeExtensions.AddChildSafely(control2, ncardSmithVfx);
								}
								else
								{
									NRestSiteRoom instance2 = NRestSiteRoom.Instance;
									if (instance2 != null)
									{
										GodotTreeExtensions.AddChildSafely(instance2, ncardSmithVfx);
									}
								}
								if (textureRect != null && GodotObject.IsInstanceValid(textureRect))
								{
									Tween tween = ncardSmithVfx.CreateTween();
									tween.TweenProperty(textureRect, "scale", Vector2.One, 0.25).SetEase(1L).SetTrans(7L);
									tween.Parallel().TweenProperty(textureRect, "rotation_degrees", 12.0, 0.15).SetTrans(6L).SetEase(1L);
									tween.TweenProperty(textureRect, "rotation_degrees", -6.0, 0.15).SetTrans(6L).SetEase(1L);
									tween.TweenProperty(textureRect, "rotation_degrees", 0.0, 0.2).SetTrans(6L).SetEase(1L);
								}
								awaiter = Cmd.CustomScaledWait(1f, 2f, false, this.ct).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgRestSiteForgeVfx.<PlayLocal>d__4>(ref awaiter, ref this);
									return;
								}
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							this.<>1__state = -1;
						}
						awaiter.GetResult();
						goto IL_333;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(TaskAwaiter);
					this.<>1__state = -1;
					IL_9F:
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_333:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000850 RID: 2128 RVA: 0x0002B98C File Offset: 0x00029B8C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine([Nullable(1)] IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040004D0 RID: 1232
			public int <>1__state;

			// Token: 0x040004D1 RID: 1233
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040004D2 RID: 1234
			public CancellationToken ct;

			// Token: 0x040004D3 RID: 1235
			[Nullable(0)]
			public Texture2D icon;

			// Token: 0x040004D4 RID: 1236
			private TaskAwaiter <>u__1;
		}
	}
}
