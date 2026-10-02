using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Localization;

namespace SpireTheRpg
{
	// Token: 0x02000003 RID: 3
	[NullableContext(1)]
	[Nullable(0)]
	internal static class SpireLoc
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002658 File Offset: 0x00000858
		public static void Inject()
		{
			try
			{
				SpireLoc.RegisterCustomTables();
				LocManager instance = LocManager.Instance;
				if (instance != null)
				{
					SpireLoc.EnsureLocaleSubscription(instance);
					string text = SpireLoc.ResolveLanguage(instance.Language);
					if (!(SpireLoc._injectedLanguage == text))
					{
						SpireLoc.InjectTable(instance, text, "main_menu_ui", "main_menu_ui", null);
						LocManager lm = instance;
						string lang = text;
						string fileTable = "items";
						string gameTable = "items";
						Func<string, Dictionary<string, string>> loader;
						if ((loader = SpireLoc.<>O.<0>__LoadItemEntries) == null)
						{
							loader = (SpireLoc.<>O.<0>__LoadItemEntries = new Func<string, Dictionary<string, string>>(SpireLoc.LoadItemEntries));
						}
						SpireLoc.InjectTable(lm, lang, fileTable, gameTable, loader);
						SpireLoc.InjectTable(instance, text, "relics", "relics", null);
						SpireLoc.InjectTable(instance, text, "settings_ui", "settings_ui", null);
						SpireLoc.InjectTable(instance, text, "rest_site_ui", "rest_site_ui", null);
						SpireLoc._injectedLanguage = text;
						SpireTheRpgMainFile.Logger.Info("Spire loc injected lang=" + text, 1);
					}
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("Spire loc inject failed: " + ex.Message, 1);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000275C File Offset: 0x0000095C
		public static void ForceInject()
		{
			SpireLoc._injectedLanguage = null;
			SpireLoc.Inject();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002769 File Offset: 0x00000969
		public static string T(string key, string fallback)
		{
			return SpireLoc.T("main_menu_ui", key, fallback);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002777 File Offset: 0x00000977
		public static string Item(string key, string fallback)
		{
			return SpireLoc.T("items", key, fallback);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002788 File Offset: 0x00000988
		public static string ItemFormat(string key, string fallback, params object[] args)
		{
			string result;
			try
			{
				SpireLoc.Inject();
				LocString locString = new LocString("items", key);
				string text = locString.Exists() ? locString.GetRawText() : fallback;
				if (string.IsNullOrWhiteSpace(text))
				{
					text = fallback;
				}
				result = string.Format(text, args);
			}
			catch
			{
				try
				{
					result = string.Format(fallback, args);
				}
				catch
				{
					result = fallback;
				}
			}
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000027FC File Offset: 0x000009FC
		public static string T(string table, string key, string fallback)
		{
			try
			{
				SpireLoc.Inject();
				LocString locString = new LocString(table, key);
				if (locString.Exists())
				{
					string rawText = locString.GetRawText();
					if (!string.IsNullOrWhiteSpace(rawText))
					{
						return rawText;
					}
				}
			}
			catch
			{
			}
			return fallback;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000284C File Offset: 0x00000A4C
		private static void RegisterCustomTables()
		{
			if (SpireLoc._registeredCustomTables)
			{
				return;
			}
			try
			{
				CustomLocTableManager.Register("items");
				CustomLocTableManager.Register("settings_ui");
				SpireLoc._registeredCustomTables = true;
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("Spire loc register failed: " + ex.Message, 1);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000028AC File Offset: 0x00000AAC
		private static void InjectTable(LocManager lm, string lang, string fileTable, string gameTable, [Nullable(new byte[]
		{
			2,
			1,
			1,
			1,
			1
		})] Func<string, Dictionary<string, string>> loader = null)
		{
			Dictionary<string, string> dictionary = ((loader != null) ? loader(lang) : null) ?? SpireLoc.LoadEntries(lang, fileTable);
			Dictionary<string, string> dictionary2 = ((loader != null) ? loader("eng") : null) ?? SpireLoc.LoadEntries("eng", fileTable);
			if (dictionary.Count == 0 && dictionary2.Count == 0)
			{
				return;
			}
			Dictionary<string, LocTable> field = SpireLoc.GetField<Dictionary<string, LocTable>>(lm, "_tables");
			Dictionary<string, LocTable> field2 = SpireLoc.GetField<Dictionary<string, LocTable>>(lm, "_engTables");
			if (field == null)
			{
				return;
			}
			SpireLoc.MergeIntoTable(field, gameTable, (dictionary.Count > 0) ? dictionary : dictionary2);
			if (field2 != null)
			{
				SpireLoc.MergeIntoTable(field2, gameTable, dictionary2);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002944 File Offset: 0x00000B44
		private static Dictionary<string, string> LoadItemEntries(string lang)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			SpireLoc.MergeItemDir(dictionary, "eng");
			if (!string.Equals(lang, "eng", StringComparison.OrdinalIgnoreCase))
			{
				SpireLoc.MergeItemDir(dictionary, lang);
			}
			return dictionary;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002980 File Offset: 0x00000B80
		private static void MergeItemDir(Dictionary<string, string> merged, string lang)
		{
			string text = "localization/" + lang + "/item";
			foreach (string str in SpireAssets.ListFiles(text, ".json"))
			{
				string text2 = text + "/" + str;
				try
				{
					string text3 = SpireAssets.ReadText(text2);
					if (!string.IsNullOrWhiteSpace(text3))
					{
						Dictionary<string, string> dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(text3, null);
						if (dictionary != null)
						{
							foreach (KeyValuePair<string, string> keyValuePair in dictionary)
							{
								string text4;
								string text5;
								keyValuePair.Deconstruct(out text4, out text5);
								string key = text4;
								string value = text5;
								merged[key] = value;
							}
						}
					}
				}
				catch (Exception ex)
				{
					SpireTheRpgMainFile.Logger.Info("Spire item loc read failed (" + text2 + "): " + ex.Message, 1);
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002AA0 File Offset: 0x00000CA0
		private static void EnsureLocaleSubscription(LocManager lm)
		{
			if (SpireLoc._subscribed)
			{
				return;
			}
			try
			{
				lm.SubscribeToLocaleChange(delegate()
				{
					SpireLoc._injectedLanguage = null;
					SpireLoc.Inject();
				});
				SpireLoc._subscribed = true;
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("Spire loc subscribe failed: " + ex.Message, 1);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002B14 File Offset: 0x00000D14
		private static string ResolveLanguage([Nullable(2)] string language)
		{
			if (string.IsNullOrWhiteSpace(language))
			{
				return "eng";
			}
			string text = language.Trim().ToLowerInvariant();
			if (text != null)
			{
				switch (text.Length)
				{
				case 2:
				{
					char c = text[0];
					if (c <= 'e')
					{
						if (c != 'c')
						{
							if (c != 'e')
							{
								goto IL_218;
							}
							if (!(text == "en"))
							{
								goto IL_218;
							}
							goto IL_210;
						}
						else if (!(text == "cn"))
						{
							goto IL_218;
						}
					}
					else if (c != 'h')
					{
						if (c != 't')
						{
							if (c != 'z')
							{
								goto IL_218;
							}
							if (!(text == "zh"))
							{
								goto IL_218;
							}
						}
						else
						{
							if (!(text == "tw"))
							{
								goto IL_218;
							}
							goto IL_208;
						}
					}
					else
					{
						if (!(text == "hk"))
						{
							goto IL_218;
						}
						goto IL_208;
					}
					break;
				}
				case 3:
				{
					char c = text[2];
					if (c != 's')
					{
						if (c != 't')
						{
							goto IL_218;
						}
						if (!(text == "cht"))
						{
							goto IL_218;
						}
						goto IL_208;
					}
					else if (!(text == "chs"))
					{
						goto IL_218;
					}
					break;
				}
				case 4:
				case 6:
					goto IL_218;
				case 5:
				{
					char c = text[3];
					if (c != 'c')
					{
						if (c != 't')
						{
							if (c != 'u')
							{
								goto IL_218;
							}
							if (!(text == "en-us") && !(text == "en_us"))
							{
								goto IL_218;
							}
							goto IL_210;
						}
						else
						{
							if (!(text == "zh-tw") && !(text == "zh_tw"))
							{
								goto IL_218;
							}
							goto IL_208;
						}
					}
					else if (!(text == "zh-cn") && !(text == "zh_cn"))
					{
						goto IL_218;
					}
					break;
				}
				case 7:
				{
					char c = text[6];
					if (c != 'h')
					{
						if (c != 's')
						{
							if (c != 't')
							{
								goto IL_218;
							}
							if (!(text == "zh-hant"))
							{
								goto IL_218;
							}
							goto IL_208;
						}
						else if (!(text == "zh-hans"))
						{
							goto IL_218;
						}
					}
					else
					{
						if (!(text == "english"))
						{
							goto IL_218;
						}
						goto IL_210;
					}
					break;
				}
				default:
					goto IL_218;
				}
				return "zhs";
				IL_208:
				return "zht";
				IL_210:
				return "eng";
			}
			IL_218:
			return text;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002D3C File Offset: 0x00000F3C
		private static Dictionary<string, string> LoadEntries(string lang, string table)
		{
			foreach (string value in SpireLoc.DistinctLangs(lang))
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(19, 2);
				defaultInterpolatedStringHandler.AppendLiteral("localization/");
				defaultInterpolatedStringHandler.AppendFormatted(value);
				defaultInterpolatedStringHandler.AppendLiteral("/");
				defaultInterpolatedStringHandler.AppendFormatted(table);
				defaultInterpolatedStringHandler.AppendLiteral(".json");
				string text = defaultInterpolatedStringHandler.ToStringAndClear();
				try
				{
					string text2 = SpireAssets.ReadText(text);
					if (!string.IsNullOrWhiteSpace(text2))
					{
						Dictionary<string, string> dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(text2, null);
						if (dictionary != null && dictionary.Count > 0)
						{
							return dictionary;
						}
					}
				}
				catch (Exception ex)
				{
					SpireTheRpgMainFile.Logger.Info("Spire loc read failed (" + text + "): " + ex.Message, 1);
				}
			}
			return new Dictionary<string, string>();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002E3C File Offset: 0x0000103C
		private static IEnumerable<string> DistinctLangs(string lang)
		{
			SpireLoc.<DistinctLangs>d__20 <DistinctLangs>d__ = new SpireLoc.<DistinctLangs>d__20(-2);
			<DistinctLangs>d__.<>3__lang = lang;
			return <DistinctLangs>d__;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002E4C File Offset: 0x0000104C
		private static void MergeIntoTable(Dictionary<string, LocTable> tables, string tableName, Dictionary<string, string> entries)
		{
			if (entries.Count == 0)
			{
				return;
			}
			LocTable locTable;
			if (!tables.TryGetValue(tableName, out locTable))
			{
				locTable = new LocTable(tableName, new Dictionary<string, string>(), null);
				tables[tableName] = locTable;
			}
			locTable.MergeWith(entries);
			Dictionary<string, string> field = SpireLoc.GetField<Dictionary<string, string>>(locTable, "_translations");
			if (field != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in entries)
				{
					string text;
					string text2;
					keyValuePair.Deconstruct(out text, out text2);
					string key = text;
					string value = text2;
					field[key] = value;
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002EF0 File Offset: 0x000010F0
		[return: Nullable(2)]
		private static T GetField<T>(object instance, string name) where T : class
		{
			FieldInfo field = instance.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
			return ((field != null) ? field.GetValue(instance) : null) as T;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002F17 File Offset: 0x00001117
		// Note: this type is marked as 'beforefieldinit'.
		static SpireLoc()
		{
		}

		// Token: 0x04000004 RID: 4
		private static readonly string[] LanguageFallback = new string[]
		{
			"eng"
		};

		// Token: 0x04000005 RID: 5
		private const string UiTable = "main_menu_ui";

		// Token: 0x04000006 RID: 6
		private const string ItemsTable = "items";

		// Token: 0x04000007 RID: 7
		private const string SettingsTable = "settings_ui";

		// Token: 0x04000008 RID: 8
		private static bool _subscribed;

		// Token: 0x04000009 RID: 9
		private static bool _registeredCustomTables;

		// Token: 0x0400000A RID: 10
		[Nullable(2)]
		private static string _injectedLanguage;

		// Token: 0x0200007E RID: 126
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040001BD RID: 445
			[Nullable(new byte[]
			{
				0,
				1,
				1,
				1,
				1
			})]
			public static Func<string, Dictionary<string, string>> <0>__LoadItemEntries;
		}

		// Token: 0x0200007F RID: 127
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000599 RID: 1433 RVA: 0x0001DC52 File Offset: 0x0001BE52
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600059A RID: 1434 RVA: 0x0001DC5E File Offset: 0x0001BE5E
			public <>c()
			{
			}

			// Token: 0x0600059B RID: 1435 RVA: 0x0001DC66 File Offset: 0x0001BE66
			internal void <EnsureLocaleSubscription>b__17_0()
			{
				SpireLoc._injectedLanguage = null;
				SpireLoc.Inject();
			}

			// Token: 0x040001BE RID: 446
			[Nullable(0)]
			public static readonly SpireLoc.<>c <>9 = new SpireLoc.<>c();

			// Token: 0x040001BF RID: 447
			[Nullable(0)]
			public static LocManager.LocaleChangeCallback <>9__17_0;
		}

		// Token: 0x02000080 RID: 128
		[CompilerGenerated]
		private sealed class <DistinctLangs>d__20 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IEnumerator, IDisposable
		{
			// Token: 0x0600059C RID: 1436 RVA: 0x0001DC73 File Offset: 0x0001BE73
			[DebuggerHidden]
			public <DistinctLangs>d__20(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600059D RID: 1437 RVA: 0x0001DC8D File Offset: 0x0001BE8D
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				this.<>7__wrap1 = null;
				this.<>1__state = -2;
			}

			// Token: 0x0600059E RID: 1438 RVA: 0x0001DCA0 File Offset: 0x0001BEA0
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					this.<>2__current = lang;
					this.<>1__state = 1;
					return true;
				case 1:
					this.<>1__state = -1;
					this.<>7__wrap1 = SpireLoc.LanguageFallback;
					this.<>7__wrap2 = 0;
					goto IL_94;
				case 2:
					this.<>1__state = -1;
					break;
				default:
					return false;
				}
				IL_86:
				this.<>7__wrap2++;
				IL_94:
				if (this.<>7__wrap2 >= this.<>7__wrap1.Length)
				{
					this.<>7__wrap1 = null;
					return false;
				}
				string a = this.<>7__wrap1[this.<>7__wrap2];
				if (!string.Equals(a, lang, StringComparison.OrdinalIgnoreCase))
				{
					this.<>2__current = a;
					this.<>1__state = 2;
					return true;
				}
				goto IL_86;
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x0600059F RID: 1439 RVA: 0x0001DD59 File Offset: 0x0001BF59
			string IEnumerator<string>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060005A0 RID: 1440 RVA: 0x0001DD61 File Offset: 0x0001BF61
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0001DD68 File Offset: 0x0001BF68
			object IEnumerator.Current
			{
				[DebuggerHidden]
				[return: Nullable(0)]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060005A2 RID: 1442 RVA: 0x0001DD70 File Offset: 0x0001BF70
			[DebuggerHidden]
			[return: Nullable(new byte[]
			{
				1,
				0
			})]
			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				SpireLoc.<DistinctLangs>d__20 <DistinctLangs>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<DistinctLangs>d__ = this;
				}
				else
				{
					<DistinctLangs>d__ = new SpireLoc.<DistinctLangs>d__20(0);
				}
				<DistinctLangs>d__.lang = lang;
				return <DistinctLangs>d__;
			}

			// Token: 0x060005A3 RID: 1443 RVA: 0x0001DDB3 File Offset: 0x0001BFB3
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();
			}

			// Token: 0x040001C0 RID: 448
			private int <>1__state;

			// Token: 0x040001C1 RID: 449
			private string <>2__current;

			// Token: 0x040001C2 RID: 450
			private int <>l__initialThreadId;

			// Token: 0x040001C3 RID: 451
			[Nullable(0)]
			private string lang;

			// Token: 0x040001C4 RID: 452
			[Nullable(0)]
			public string <>3__lang;

			// Token: 0x040001C5 RID: 453
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private string[] <>7__wrap1;

			// Token: 0x040001C6 RID: 454
			private int <>7__wrap2;
		}
	}
}
