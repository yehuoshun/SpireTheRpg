using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.addons.mega_text;
using MegaCrit.Sts2.Core.Nodes.Screens.MainMenu;
using SpireTheRpg.Core;

namespace SpireTheRpg.Ui
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgUpdateNotice
	{
		// Token: 0x06000105 RID: 261 RVA: 0x0000C859 File Offset: 0x0000AA59
		public static void OnMainMenuReady(NMainMenu menu)
		{
			if (!SpireTheRpgConfig.ShowUpdateNotice)
			{
				return;
			}
			RpgUpdateNotice.AttachWhenReadyAsync(menu);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000C86C File Offset: 0x0000AA6C
		private static Task AttachWhenReadyAsync(NMainMenu menu)
		{
			RpgUpdateNotice.<AttachWhenReadyAsync>d__7 <AttachWhenReadyAsync>d__;
			<AttachWhenReadyAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AttachWhenReadyAsync>d__.menu = menu;
			<AttachWhenReadyAsync>d__.<>1__state = -1;
			<AttachWhenReadyAsync>d__.<>t__builder.Start<RpgUpdateNotice.<AttachWhenReadyAsync>d__7>(ref <AttachWhenReadyAsync>d__);
			return <AttachWhenReadyAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000C8B0 File Offset: 0x0000AAB0
		private static bool TryAttach(NMainMenu menu)
		{
			Label label = menu.GetNodeOrNull<Label>("%ModdedWarning") ?? RpgUpdateNotice.FindModStatusLabel(menu);
			Node node = (label != null) ? label.GetParent() : null;
			if (label == null || node == null)
			{
				return false;
			}
			RpgUpdateNotice.RemoveExisting(menu);
			Label label2 = RpgUpdateNotice.CreateLabel(label);
			node.AddChild(label2, false, 0L);
			node.MoveChild(label2, Math.Min(label.GetIndex(false) + 1, node.GetChildCount(false) - 1));
			object @lock = RpgUpdateNotice.Lock;
			lock (@lock)
			{
				if (RpgUpdateNotice._cachedText != null)
				{
					RpgUpdateNotice.SetText(label2, RpgUpdateNotice._cachedText);
					return true;
				}
				if (RpgUpdateNotice._checkTask == null)
				{
					RpgUpdateNotice._checkTask = RpgUpdateNotice.CheckAsync();
				}
			}
			RpgUpdateNotice.ApplyAsync(label2, RpgUpdateNotice._checkTask);
			return true;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000C988 File Offset: 0x0000AB88
		private static Label CreateLabel(Label template)
		{
			Label label = (template is MegaLabel) ? new MegaLabel() : new Label();
			label.Name = "SpireTheRpgUpdateNotice";
			label.MouseFilter = 2L;
			label.ZIndex = template.ZIndex;
			label.Modulate = template.Modulate;
			label.SelfModulate = template.SelfModulate;
			label.Theme = template.Theme;
			label.ThemeTypeVariation = template.ThemeTypeVariation;
			Font themeFont = template.GetThemeFont("font", null);
			if (themeFont != null)
			{
				label.AddThemeFontOverride("font", themeFont);
			}
			int themeFontSize = template.GetThemeFontSize("font_size", null);
			if (themeFontSize > 0)
			{
				label.AddThemeFontSizeOverride("font_size", themeFontSize);
			}
			label.AddThemeColorOverride("font_color", template.GetThemeColor("font_color", null));
			label.AddThemeColorOverride("font_outline_color", template.GetThemeColor("font_outline_color", null));
			label.AddThemeConstantOverride("outline_size", template.GetThemeConstant("outline_size", null));
			label.HorizontalAlignment = 0L;
			label.VerticalAlignment = 1L;
			label.AnchorLeft = 0f;
			label.AnchorTop = 1f;
			label.AnchorRight = 0f;
			label.AnchorBottom = 1f;
			label.OffsetLeft = 16f;
			label.OffsetTop = -74f;
			label.OffsetRight = 820f;
			label.OffsetBottom = -44f;
			RpgUpdateNotice.SetText(label, SpireLoc.T("SPIRE_UPDATE.checking", "Spire The RPG: checking version…"));
			return label;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000CB2C File Offset: 0x0000AD2C
		private static Task ApplyAsync(Label label, Task<string> task)
		{
			RpgUpdateNotice.<ApplyAsync>d__10 <ApplyAsync>d__;
			<ApplyAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ApplyAsync>d__.label = label;
			<ApplyAsync>d__.task = task;
			<ApplyAsync>d__.<>1__state = -1;
			<ApplyAsync>d__.<>t__builder.Start<RpgUpdateNotice.<ApplyAsync>d__10>(ref <ApplyAsync>d__);
			return <ApplyAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000CB78 File Offset: 0x0000AD78
		private static Task<string> CheckAsync()
		{
			RpgUpdateNotice.<CheckAsync>d__11 <CheckAsync>d__;
			<CheckAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<CheckAsync>d__.<>1__state = -1;
			<CheckAsync>d__.<>t__builder.Start<RpgUpdateNotice.<CheckAsync>d__11>(ref <CheckAsync>d__);
			return <CheckAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000CBB4 File Offset: 0x0000ADB4
		[return: Nullable(2)]
		private static string ReadVersion(JsonElement root, string key)
		{
			if (root.ValueKind != JsonValueKind.Object)
			{
				return null;
			}
			JsonElement jsonElement;
			if (!root.TryGetProperty(key, out jsonElement) || jsonElement.ValueKind != JsonValueKind.String)
			{
				return null;
			}
			return jsonElement.GetString();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000CBEC File Offset: 0x0000ADEC
		private static string ReadLocalVersion()
		{
			try
			{
				string path = Path.Combine(SpireAssets.ModDir, "SpireTheRpg.json");
				if (!File.Exists(path))
				{
					return "?";
				}
				using (JsonDocument jsonDocument = JsonDocument.Parse(File.ReadAllText(path), default(JsonDocumentOptions)))
				{
					JsonElement jsonElement;
					if (jsonDocument.RootElement.TryGetProperty("version", out jsonElement) && jsonElement.ValueKind == JsonValueKind.String)
					{
						string @string = jsonElement.GetString();
						return ((@string != null) ? @string.Trim() : null) ?? "?";
					}
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RpgUpdateNotice local version failed: " + ex.Message, 1);
			}
			return "?";
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000CCC4 File Offset: 0x0000AEC4
		private static int CompareVersions(string left, string right)
		{
			List<int> list = RpgUpdateNotice.ParseParts(left);
			List<int> list2 = RpgUpdateNotice.ParseParts(right);
			int num = Math.Max(list.Count, list2.Count);
			for (int i = 0; i < num; i++)
			{
				int num2 = (i < list.Count) ? list[i] : 0;
				int value = (i < list2.Count) ? list2[i] : 0;
				int num3 = num2.CompareTo(value);
				if (num3 != 0)
				{
					return num3;
				}
			}
			return 0;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000CD3C File Offset: 0x0000AF3C
		private static List<int> ParseParts(string version)
		{
			string input = version.Trim().TrimStart(new char[]
			{
				'v',
				'V'
			});
			List<int> list = new List<int>();
			foreach (string text in Regex.Split(input, "[.+\\-]"))
			{
				if (!string.IsNullOrEmpty(text))
				{
					int num = 0;
					bool flag = false;
					foreach (char c in text)
					{
						if (!char.IsDigit(c))
						{
							break;
						}
						flag = true;
						num = num * 10 + (int)(c - '0');
					}
					if (flag)
					{
						list.Add(num);
					}
				}
			}
			return list;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000CDE0 File Offset: 0x0000AFE0
		private static void SetText(Label label, string text)
		{
			MegaLabel megaLabel = label as MegaLabel;
			if (megaLabel != null)
			{
				megaLabel.SetTextAutoSize(text);
				return;
			}
			label.Text = text;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000CE08 File Offset: 0x0000B008
		[return: Nullable(2)]
		private static Label FindModStatusLabel(Node root)
		{
			foreach (Node node in root.GetChildren(false))
			{
				if (!(node.Name == "SpireTheRpgUpdateNotice") && !(node.Name == "HextechRunesUpdateNotice"))
				{
					Label label = node as Label;
					if (label != null)
					{
						string a = node.Name.ToString();
						string text = label.Text ?? "";
						if (a == "ModdedWarning" || (text.Contains("mod", StringComparison.OrdinalIgnoreCase) && text.Contains("loaded", StringComparison.OrdinalIgnoreCase)))
						{
							return label;
						}
					}
					Label label2 = RpgUpdateNotice.FindModStatusLabel(node);
					if (label2 != null)
					{
						return label2;
					}
				}
			}
			return null;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000CEEC File Offset: 0x0000B0EC
		private static void RemoveExisting(Node root)
		{
			foreach (Node node in root.GetChildren(false))
			{
				if (node.Name == "SpireTheRpgUpdateNotice")
				{
					node.QueueFree();
				}
				else
				{
					RpgUpdateNotice.RemoveExisting(node);
				}
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000CF58 File Offset: 0x0000B158
		// Note: this type is marked as 'beforefieldinit'.
		static RpgUpdateNotice()
		{
		}

		// Token: 0x0400007C RID: 124
		private const string NoticeName = "SpireTheRpgUpdateNotice";

		// Token: 0x0400007D RID: 125
		private const string VersionUrl = "https://vvv.aimaxx.app/latest-version.json";

		// Token: 0x0400007E RID: 126
		private static readonly HttpClient Http = new HttpClient
		{
			Timeout = TimeSpan.FromSeconds(12L)
		};

		// Token: 0x0400007F RID: 127
		private static readonly object Lock = new object();

		// Token: 0x04000080 RID: 128
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private static Task<string> _checkTask;

		// Token: 0x04000081 RID: 129
		[Nullable(2)]
		private static string _cachedText;

		// Token: 0x020000BD RID: 189
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ApplyAsync>d__10 : IAsyncStateMachine
		{
			// Token: 0x06000683 RID: 1667 RVA: 0x00020F58 File Offset: 0x0001F158
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					string text;
					try
					{
						ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = this.task.ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, RpgUpdateNotice.<ApplyAsync>d__10>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						text = awaiter.GetResult();
					}
					catch (Exception ex)
					{
						SpireTheRpgMainFile.Logger.Info("RpgUpdateNotice failed: " + ex.Message, 1);
						text = SpireLoc.T("SPIRE_UPDATE.failed", "Spire The RPG: version check unavailable");
						object @lock = RpgUpdateNotice.Lock;
						bool flag = false;
						try
						{
							Monitor.Enter(@lock, ref flag);
							if (RpgUpdateNotice._checkTask == this.task)
							{
								RpgUpdateNotice._checkTask = null;
							}
						}
						finally
						{
							if (num < 0 && flag)
							{
								Monitor.Exit(@lock);
							}
						}
					}
					if (GodotObject.IsInstanceValid(this.label))
					{
						if (this.label is MegaLabel)
						{
							this.label.CallDeferred("SetTextAutoSize", new Variant[]
							{
								text
							});
						}
						else
						{
							this.label.CallDeferred("set", new Variant[]
							{
								"text",
								text
							});
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000684 RID: 1668 RVA: 0x00021140 File Offset: 0x0001F340
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000293 RID: 659
			public int <>1__state;

			// Token: 0x04000294 RID: 660
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000295 RID: 661
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public Task<string> task;

			// Token: 0x04000296 RID: 662
			[Nullable(0)]
			public Label label;

			// Token: 0x04000297 RID: 663
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000BE RID: 190
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <AttachWhenReadyAsync>d__7 : IAsyncStateMachine
		{
			// Token: 0x06000685 RID: 1669 RVA: 0x00021150 File Offset: 0x0001F350
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					if (num != 0)
					{
						this.<i>5__2 = 0;
						goto IL_BB;
					}
					IAwaiter<Variant[]> awaiter = (IAwaiter<Variant[]>)this.<>u__1;
					this.<>u__1 = null;
					this.<>1__state = -1;
					IL_A4:
					awaiter.GetResult();
					int num2 = this.<i>5__2;
					this.<i>5__2 = num2 + 1;
					IL_BB:
					if (this.<i>5__2 < 30)
					{
						if (GodotObject.IsInstanceValid(this.menu))
						{
							if (!RpgUpdateNotice.TryAttach(this.menu))
							{
								SceneTree tree = this.menu.GetTree();
								if (tree != null)
								{
									awaiter = tree.ToSignal(tree, SceneTree.SignalName.ProcessFrame).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										this.<>1__state = 0;
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitOnCompleted<IAwaiter<Variant[]>, RpgUpdateNotice.<AttachWhenReadyAsync>d__7>(ref awaiter, ref this);
										return;
									}
									goto IL_A4;
								}
							}
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000686 RID: 1670 RVA: 0x00021264 File Offset: 0x0001F464
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000298 RID: 664
			public int <>1__state;

			// Token: 0x04000299 RID: 665
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400029A RID: 666
			[Nullable(0)]
			public NMainMenu menu;

			// Token: 0x0400029B RID: 667
			private int <i>5__2;

			// Token: 0x0400029C RID: 668
			[Nullable(0)]
			private object <>u__1;
		}

		// Token: 0x020000BF RID: 191
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <CheckAsync>d__11 : IAsyncStateMachine
		{
			// Token: 0x06000687 RID: 1671 RVA: 0x00021274 File Offset: 0x0001F474
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				string result4;
				try
				{
					ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num - 1 <= 2)
						{
							goto IL_8B;
						}
						awaiter = RpgUpdateNotice.Http.GetAsync("https://vvv.aimaxx.app/latest-version.json").ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter, RpgUpdateNotice.<CheckAsync>d__11>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
					}
					HttpResponseMessage result = awaiter.GetResult();
					this.<response>5__2 = result;
					IL_8B:
					try
					{
						ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter awaiter2;
						object @lock;
						ValueTaskAwaiter awaiter4;
						switch (num)
						{
						case 1:
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							break;
						case 2:
							IL_136:
							try
							{
								ConfiguredTaskAwaitable<JsonDocument>.ConfiguredTaskAwaiter awaiter3;
								if (num != 2)
								{
									awaiter3 = JsonDocument.ParseAsync(this.<stream>5__3, default(JsonDocumentOptions), default(CancellationToken)).ConfigureAwait(false).GetAwaiter();
									if (!awaiter3.IsCompleted)
									{
										num = (this.<>1__state = 2);
										this.<>u__3 = awaiter3;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<JsonDocument>.ConfiguredTaskAwaiter, RpgUpdateNotice.<CheckAsync>d__11>(ref awaiter3, ref this);
										return;
									}
								}
								else
								{
									awaiter3 = this.<>u__3;
									this.<>u__3 = default(ConfiguredTaskAwaitable<JsonDocument>.ConfiguredTaskAwaiter);
									num = (this.<>1__state = -1);
								}
								JsonDocument result2 = awaiter3.GetResult();
								try
								{
									JsonElement rootElement = result2.RootElement;
									string text;
									if ((text = RpgUpdateNotice.ReadVersion(rootElement, "latestVersion")) == null && (text = RpgUpdateNotice.ReadVersion(rootElement, "version")) == null)
									{
										text = (RpgUpdateNotice.ReadVersion(rootElement, "latest") ?? "");
									}
									string text2 = text;
									if (string.IsNullOrWhiteSpace(text2))
									{
										throw new Exception("missing latestVersion");
									}
									string text3 = RpgUpdateNotice.ReadLocalVersion();
									string cachedText = (RpgUpdateNotice.CompareVersions(text2, text3) > 0) ? SpireLoc.T("SPIRE_UPDATE.outdated", "Spire The RPG update available: {0} (you have {1})").Replace("{0}", text2.Trim()).Replace("{1}", text3) : SpireLoc.T("SPIRE_UPDATE.latest", "Spire The RPG is up to date ({0})").Replace("{0}", text3);
									@lock = RpgUpdateNotice.Lock;
									bool flag = false;
									try
									{
										Monitor.Enter(@lock, ref flag);
										RpgUpdateNotice._cachedText = cachedText;
									}
									finally
									{
										if (num < 0 && flag)
										{
											Monitor.Exit(@lock);
										}
									}
									SpireTheRpgMainFile.Logger.Info("RpgUpdateNotice latest=" + text2 + " current=" + text3, 1);
									this.<>7__wrap5 = cachedText;
								}
								finally
								{
									if (num < 0 && result2 != null)
									{
										((IDisposable)result2).Dispose();
									}
								}
								this.<>7__wrap4 = 1;
							}
							catch (object @lock)
							{
								this.<>7__wrap3 = @lock;
							}
							if (this.<stream>5__3 == null)
							{
								goto IL_350;
							}
							awaiter4 = this.<stream>5__3.DisposeAsync().GetAwaiter();
							if (!awaiter4.IsCompleted)
							{
								num = (this.<>1__state = 3);
								this.<>u__4 = awaiter4;
								this.<>t__builder.AwaitUnsafeOnCompleted<ValueTaskAwaiter, RpgUpdateNotice.<CheckAsync>d__11>(ref awaiter4, ref this);
								return;
							}
							goto IL_349;
						case 3:
							awaiter4 = this.<>u__4;
							this.<>u__4 = default(ValueTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_349;
						default:
							this.<response>5__2.EnsureSuccessStatusCode();
							awaiter2 = this.<response>5__2.Content.ReadAsStreamAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 1);
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter, RpgUpdateNotice.<CheckAsync>d__11>(ref awaiter2, ref this);
								return;
							}
							break;
						}
						Stream result3 = awaiter2.GetResult();
						this.<stream>5__3 = result3;
						this.<>7__wrap3 = null;
						this.<>7__wrap4 = 0;
						goto IL_136;
						IL_349:
						awaiter4.GetResult();
						IL_350:
						@lock = this.<>7__wrap3;
						if (@lock != null)
						{
							Exception ex = @lock as Exception;
							if (ex == null)
							{
								throw @lock;
							}
							ExceptionDispatchInfo.Capture(ex).Throw();
						}
						int num2 = this.<>7__wrap4;
						if (num2 == 1)
						{
							result4 = this.<>7__wrap5;
							goto IL_3E8;
						}
						this.<>7__wrap3 = null;
						this.<>7__wrap5 = null;
					}
					finally
					{
						if (num < 0 && this.<response>5__2 != null)
						{
							((IDisposable)this.<response>5__2).Dispose();
						}
					}
					this.<response>5__2 = null;
					this.<stream>5__3 = null;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<response>5__2 = null;
					this.<stream>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_3E8:
				this.<>1__state = -2;
				this.<response>5__2 = null;
				this.<stream>5__3 = null;
				this.<>t__builder.SetResult(result4);
			}

			// Token: 0x06000688 RID: 1672 RVA: 0x00021708 File Offset: 0x0001F908
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400029D RID: 669
			public int <>1__state;

			// Token: 0x0400029E RID: 670
			[Nullable(0)]
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x0400029F RID: 671
			[Nullable(0)]
			private HttpResponseMessage <response>5__2;

			// Token: 0x040002A0 RID: 672
			[Nullable(0)]
			private Stream <stream>5__3;

			// Token: 0x040002A1 RID: 673
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private ConfiguredTaskAwaitable<HttpResponseMessage>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040002A2 RID: 674
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private ConfiguredTaskAwaitable<Stream>.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x040002A3 RID: 675
			[Nullable(0)]
			private object <>7__wrap3;

			// Token: 0x040002A4 RID: 676
			private int <>7__wrap4;

			// Token: 0x040002A5 RID: 677
			[Nullable(0)]
			private string <>7__wrap5;

			// Token: 0x040002A6 RID: 678
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private ConfiguredTaskAwaitable<JsonDocument>.ConfiguredTaskAwaiter <>u__3;

			// Token: 0x040002A7 RID: 679
			private ValueTaskAwaiter <>u__4;
		}
	}
}
