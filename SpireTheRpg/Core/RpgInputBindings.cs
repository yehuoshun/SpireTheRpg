using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.Screens.Settings;

namespace SpireTheRpg.Core
{
	// Token: 0x02000043 RID: 67
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgInputBindings
	{
		// Token: 0x06000266 RID: 614 RVA: 0x000120E0 File Offset: 0x000102E0
		public static void EnsureLists()
		{
			if (RpgInputBindings._listsReady)
			{
				return;
			}
			try
			{
				string[] remappableFieldNames = RpgInputBindings.RemappableFieldNames;
				for (int i = 0; i < remappableFieldNames.Length; i++)
				{
					RpgInputBindings.TryAddToRemappableList(remappableFieldNames[i]);
				}
				FieldInfo fieldInfo = AccessTools.Field(typeof(NInputSettingsEntry), "_commandToLocTitle");
				Dictionary<StringName, string> dictionary = ((fieldInfo != null) ? fieldInfo.GetValue(null) : null) as Dictionary<StringName, string>;
				if (dictionary != null)
				{
					dictionary[RpgInputBindings.OpenInventory] = "rpgOpenInventory";
				}
				if (!InputMap.HasAction(RpgInputBindings.OpenInventory))
				{
					InputMap.AddAction(RpgInputBindings.OpenInventory, 0.2f);
				}
				RpgInputBindings._listsReady = true;
				Logger logger = SpireTheRpgMainFile.Logger;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 1);
				defaultInterpolatedStringHandler.AppendLiteral("RpgInputBindings lists ready action=");
				defaultInterpolatedStringHandler.AppendFormatted<StringName>(RpgInputBindings.OpenInventory);
				logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RpgInputBindings EnsureLists failed: " + ex.Message, 1);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000121D4 File Offset: 0x000103D4
		[NullableContext(2)]
		public static void EnsureDefaultKey(NInputManager manager)
		{
			RpgInputBindings.EnsureLists();
			if (manager == null)
			{
				return;
			}
			if (RpgInputBindings.TryGetBoundKey(manager) != null)
			{
				return;
			}
			bool flag = false;
			foreach (string text in RpgInputBindings.ModifyKeyMethodNames)
			{
				MethodInfo methodInfo = AccessTools.Method(typeof(NInputManager), text, new Type[]
				{
					typeof(StringName),
					typeof(Key)
				}, null);
				if (!(methodInfo == null))
				{
					try
					{
						methodInfo.Invoke(manager, new object[]
						{
							RpgInputBindings.OpenInventory,
							73L
						});
						flag = true;
					}
					catch (Exception ex)
					{
						Logger logger = SpireTheRpgMainFile.Logger;
						string str = "RpgInputBindings ";
						string str2 = text;
						string str3 = " failed: ";
						Exception innerException = ex.InnerException;
						logger.Info(str + str2 + str3 + (((innerException != null) ? innerException.Message : null) ?? ex.Message), 1);
					}
				}
			}
			if (RpgInputBindings.TryGetBoundKey(manager) != null)
			{
				return;
			}
			try
			{
				bool flag2 = false;
				foreach (string text2 in RpgInputBindings.MapFieldNames)
				{
					FieldInfo fieldInfo = AccessTools.Field(typeof(NInputManager), text2);
					Dictionary<StringName, Key> dictionary = ((fieldInfo != null) ? fieldInfo.GetValue(manager) : null) as Dictionary<StringName, Key>;
					if (dictionary != null)
					{
						dictionary[RpgInputBindings.OpenInventory] = 73L;
						flag2 = true;
						SpireTheRpgMainFile.Logger.Info("RpgInputBindings map inject field=" + text2 + " key=I", 1);
					}
				}
				if (flag2 || flag)
				{
					RpgInputBindings.TrySaveMappings(manager);
				}
			}
			catch (Exception ex2)
			{
				SpireTheRpgMainFile.Logger.Info("RpgInputBindings map inject failed: " + ex2.Message, 1);
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0001237C File Offset: 0x0001057C
		[NullableContext(2)]
		public static Key GetBoundKey(NInputManager manager = null)
		{
			Key key = RpgInputBindings.TryGetBoundKey(manager ?? NInputManager.Instance);
			if (key != null)
			{
				return key;
			}
			return 73L;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000123A4 File Offset: 0x000105A4
		public static string DisplayHotkeyLabel()
		{
			return RpgInputBindings.GetBoundKey(null).ToString().ToUpperInvariant();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000123CC File Offset: 0x000105CC
		[NullableContext(2)]
		private static Key TryGetBoundKey(NInputManager manager)
		{
			if (manager == null)
			{
				return 0L;
			}
			foreach (string text in RpgInputBindings.GetKeyMethodNames)
			{
				MethodInfo methodInfo = AccessTools.Method(typeof(NInputManager), text, new Type[]
				{
					typeof(StringName)
				}, null);
				if (!(methodInfo == null))
				{
					try
					{
						object obj = methodInfo.Invoke(manager, new object[]
						{
							RpgInputBindings.OpenInventory
						});
						if (obj is Key)
						{
							Key key = (Key)obj;
							if (key != null)
							{
								return key;
							}
						}
					}
					catch
					{
					}
				}
			}
			foreach (string text2 in RpgInputBindings.MapFieldNames)
			{
				FieldInfo fieldInfo = AccessTools.Field(typeof(NInputManager), text2);
				Dictionary<StringName, Key> dictionary = ((fieldInfo != null) ? fieldInfo.GetValue(manager) : null) as Dictionary<StringName, Key>;
				Key key2;
				if (dictionary != null && dictionary.TryGetValue(RpgInputBindings.OpenInventory, out key2) && key2 != null)
				{
					return key2;
				}
			}
			return 0L;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000124CC File Offset: 0x000106CC
		private static void TryAddToRemappableList(string fieldName)
		{
			FieldInfo fieldInfo = AccessTools.Field(typeof(NInputManager), fieldName);
			if (fieldInfo == null)
			{
				return;
			}
			object value = fieldInfo.GetValue(null);
			List<StringName> list = value as List<StringName>;
			if (list != null)
			{
				if (!list.Contains(RpgInputBindings.OpenInventory))
				{
					list.Add(RpgInputBindings.OpenInventory);
				}
				return;
			}
			IList list2 = value as IList;
			if (list2 != null && !list2.IsReadOnly)
			{
				using (IEnumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (object.Equals(enumerator.Current, RpgInputBindings.OpenInventory))
						{
							return;
						}
					}
				}
				list2.Add(RpgInputBindings.OpenInventory);
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0001258C File Offset: 0x0001078C
		private static void TrySaveMappings(NInputManager manager)
		{
			foreach (string text in RpgInputBindings.SaveMethodNames)
			{
				MethodInfo methodInfo = AccessTools.Method(typeof(NInputManager), text, null, null);
				if (!(methodInfo == null))
				{
					try
					{
						methodInfo.Invoke(manager, null);
					}
					catch (Exception ex)
					{
						Logger logger = SpireTheRpgMainFile.Logger;
						string str = "RpgInputBindings ";
						string str2 = text;
						string str3 = " failed: ";
						Exception innerException = ex.InnerException;
						logger.Info(str + str2 + str3 + (((innerException != null) ? innerException.Message : null) ?? ex.Message), 1);
					}
				}
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00012628 File Offset: 0x00010828
		// Note: this type is marked as 'beforefieldinit'.
		static RpgInputBindings()
		{
		}

		// Token: 0x040000B7 RID: 183
		public static readonly StringName OpenInventory = "spire_rpg_open_inventory";

		// Token: 0x040000B8 RID: 184
		private const string LocTitleKey = "rpgOpenInventory";

		// Token: 0x040000B9 RID: 185
		private static bool _listsReady;

		// Token: 0x040000BA RID: 186
		private static readonly string[] RemappableFieldNames = new string[]
		{
			"remappableKeyboardInputs",
			"remappableMKbInputs",
			"remappableKbOnlyInputs"
		};

		// Token: 0x040000BB RID: 187
		private static readonly string[] MapFieldNames = new string[]
		{
			"_keyboardInputMap",
			"_mKbInputMap",
			"_fKbInputMap"
		};

		// Token: 0x040000BC RID: 188
		private static readonly string[] GetKeyMethodNames = new string[]
		{
			"GetCurrentHotkey",
			"GetShortcutKey",
			"GetMKbHotkey",
			"GetKbOnlyHotkey"
		};

		// Token: 0x040000BD RID: 189
		private static readonly string[] ModifyKeyMethodNames = new string[]
		{
			"ModifyShortcutKey",
			"ModifyMKbKey",
			"ModifyKbOnlyKey"
		};

		// Token: 0x040000BE RID: 190
		private static readonly string[] SaveMethodNames = new string[]
		{
			"SaveKeyboardInputMapping",
			"SaveMKbInputMapping",
			"SaveFKbInputMapping"
		};
	}
}
