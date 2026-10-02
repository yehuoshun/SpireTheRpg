using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.HoverTips;
using SpireTheRpg.Ui;

namespace SpireTheRpg.Core
{
	// Token: 0x02000052 RID: 82
	[NullableContext(2)]
	[Nullable(0)]
	internal static class RpgPowerLookup
	{
		// Token: 0x0600033D RID: 829 RVA: 0x000163B8 File Offset: 0x000145B8
		public static PowerModel ResolveByAffixId(string affixId)
		{
			if (string.IsNullOrWhiteSpace(affixId))
			{
				return null;
			}
			if (string.Equals(affixId, "armor", StringComparison.OrdinalIgnoreCase))
			{
				return RpgPowerLookup.ResolveByPowerName("Plating");
			}
			RpgAffixDef rpgAffixDef = RpgAffixCatalog.Get(affixId);
			if (rpgAffixDef != null && !string.IsNullOrWhiteSpace(rpgAffixDef.Power))
			{
				return RpgPowerLookup.ResolveByPowerName(rpgAffixDef.Power);
			}
			return RpgPowerLookup.ResolveByPowerName(affixId);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00016414 File Offset: 0x00014614
		public static PowerModel ResolveByPowerName(string powerName)
		{
			if (string.IsNullOrWhiteSpace(powerName))
			{
				return null;
			}
			RpgPowerLookup.EnsureResolved();
			PowerModel result;
			if (RpgPowerLookup.Cache.TryGetValue(powerName, out result))
			{
				return result;
			}
			Type type;
			if (!RpgPowerLookup.PowerTypes.TryGetValue(powerName, out type) && !RpgPowerLookup.PowerTypes.TryGetValue(powerName + "Power", out type))
			{
				RpgPowerLookup.Cache[powerName] = null;
				return null;
			}
			PowerModel powerModel = null;
			try
			{
				if (RpgPowerLookup._modelDbPower != null)
				{
					powerModel = (RpgPowerLookup._modelDbPower.MakeGenericMethod(new Type[]
					{
						type
					}).Invoke(null, null) as PowerModel);
				}
			}
			catch
			{
			}
			if (powerModel == null)
			{
				try
				{
					powerModel = (Activator.CreateInstance(type) as PowerModel);
				}
				catch
				{
				}
			}
			RpgPowerLookup.Cache[powerName] = powerModel;
			return powerModel;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000164EC File Offset: 0x000146EC
		public static Texture2D IconForAffix(string affixId)
		{
			PowerModel powerModel = RpgPowerLookup.ResolveByAffixId(affixId);
			if (powerModel == null)
			{
				return null;
			}
			Texture2D result;
			try
			{
				result = (powerModel.Icon ?? powerModel.BigIcon);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00016530 File Offset: 0x00014730
		public static IHoverTip TipForAffix(string affixId, int? amount = null)
		{
			PowerModel powerModel = RpgPowerLookup.ResolveByAffixId(affixId);
			if (powerModel == null)
			{
				return null;
			}
			IHoverTip result;
			try
			{
				result = HoverTipFactory.FromPower(powerModel, amount);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001656C File Offset: 0x0001476C
		public static void BindPowerHover([Nullable(1)] Control target, string affixId, int? amount = null, Control tipHost = null)
		{
			if (target == null || string.IsNullOrWhiteSpace(affixId))
			{
				return;
			}
			target.MouseFilter = 0L;
			target.MouseEntered += delegate()
			{
				try
				{
					if (tipHost != null && GodotObject.IsInstanceValid(tipHost))
					{
						RpgPowerTip.Show(tipHost, target, affixId, amount);
					}
					else
					{
						IHoverTip hoverTip = RpgPowerLookup.TipForAffix(affixId, amount);
						if (hoverTip != null)
						{
							NHoverTipSet nhoverTipSet = NHoverTipSet.CreateAndShow(target, hoverTip, 2);
							if (nhoverTipSet != null)
							{
								nhoverTipSet.SetAlignment(target, 2);
							}
						}
					}
				}
				catch (Exception ex)
				{
					SpireTheRpgMainFile.Logger.Info("RpgPowerLookup tip show failed: " + ex.Message, 1);
				}
			};
			target.MouseExited += delegate()
			{
				try
				{
					if (tipHost != null && GodotObject.IsInstanceValid(tipHost))
					{
						RpgPowerTip.Hide(tipHost);
					}
					else
					{
						NHoverTipSet.Remove(target);
					}
				}
				catch
				{
				}
			};
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000165EC File Offset: 0x000147EC
		private static void EnsureResolved()
		{
			if (RpgPowerLookup._resolved)
			{
				return;
			}
			RpgPowerLookup._resolved = true;
			try
			{
				RpgPowerLookup._modelDbPower = typeof(ModelDb).GetMethods(BindingFlags.Static | BindingFlags.Public).FirstOrDefault((MethodInfo m) => m.Name == "Power" && m.IsGenericMethodDefinition && m.GetParameters().Length == 0);
				Assembly assembly = typeof(PowerModel).Assembly;
				Type[] array;
				try
				{
					array = assembly.GetTypes();
				}
				catch (ReflectionTypeLoadException ex)
				{
					array = (from t in ex.Types
					where t != null
					select t).Cast<Type>().ToArray<Type>();
				}
				foreach (Type type in array)
				{
					if (!type.IsAbstract && typeof(PowerModel).IsAssignableFrom(type))
					{
						string name = type.Name;
						RpgPowerLookup.PowerTypes[name] = type;
						if (name.EndsWith("Power", StringComparison.Ordinal) && name.Length > 5)
						{
							Dictionary<string, Type> powerTypes = RpgPowerLookup.PowerTypes;
							string text = name;
							powerTypes[text.Substring(0, text.Length - 5)] = type;
						}
					}
				}
			}
			catch (Exception ex2)
			{
				SpireTheRpgMainFile.Logger.Info("RpgPowerLookup resolve failed: " + ex2.Message, 1);
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00016768 File Offset: 0x00014968
		// Note: this type is marked as 'beforefieldinit'.
		static RpgPowerLookup()
		{
		}

		// Token: 0x0400010A RID: 266
		[Nullable(1)]
		private static readonly Dictionary<string, Type> PowerTypes = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400010B RID: 267
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		private static readonly Dictionary<string, PowerModel> Cache = new Dictionary<string, PowerModel>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400010C RID: 268
		private static MethodInfo _modelDbPower;

		// Token: 0x0400010D RID: 269
		private static bool _resolved;

		// Token: 0x02000111 RID: 273
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600079B RID: 1947 RVA: 0x0002791E File Offset: 0x00025B1E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600079C RID: 1948 RVA: 0x0002792A File Offset: 0x00025B2A
			public <>c()
			{
			}

			// Token: 0x0600079D RID: 1949 RVA: 0x00027932 File Offset: 0x00025B32
			[NullableContext(0)]
			internal bool <EnsureResolved>b__9_0(MethodInfo m)
			{
				return m.Name == "Power" && m.IsGenericMethodDefinition && m.GetParameters().Length == 0;
			}

			// Token: 0x0600079E RID: 1950 RVA: 0x0002795A File Offset: 0x00025B5A
			[NullableContext(0)]
			internal bool <EnsureResolved>b__9_1(Type t)
			{
				return t != null;
			}

			// Token: 0x040003EE RID: 1006
			[Nullable(0)]
			public static readonly RpgPowerLookup.<>c <>9 = new RpgPowerLookup.<>c();

			// Token: 0x040003EF RID: 1007
			[Nullable(0)]
			public static Func<MethodInfo, bool> <>9__9_0;

			// Token: 0x040003F0 RID: 1008
			[Nullable(0)]
			public static Func<Type, bool> <>9__9_1;
		}

		// Token: 0x02000112 RID: 274
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x0600079F RID: 1951 RVA: 0x00027963 File Offset: 0x00025B63
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x060007A0 RID: 1952 RVA: 0x0002796C File Offset: 0x00025B6C
			internal void <BindPowerHover>b__0()
			{
				try
				{
					if (this.tipHost != null && GodotObject.IsInstanceValid(this.tipHost))
					{
						RpgPowerTip.Show(this.tipHost, this.target, this.affixId, this.amount);
					}
					else
					{
						IHoverTip hoverTip = RpgPowerLookup.TipForAffix(this.affixId, this.amount);
						if (hoverTip != null)
						{
							NHoverTipSet nhoverTipSet = NHoverTipSet.CreateAndShow(this.target, hoverTip, 2);
							if (nhoverTipSet != null)
							{
								nhoverTipSet.SetAlignment(this.target, 2);
							}
						}
					}
				}
				catch (Exception ex)
				{
					SpireTheRpgMainFile.Logger.Info("RpgPowerLookup tip show failed: " + ex.Message, 1);
				}
			}

			// Token: 0x060007A1 RID: 1953 RVA: 0x00027A14 File Offset: 0x00025C14
			internal void <BindPowerHover>b__1()
			{
				try
				{
					if (this.tipHost != null && GodotObject.IsInstanceValid(this.tipHost))
					{
						RpgPowerTip.Hide(this.tipHost);
					}
					else
					{
						NHoverTipSet.Remove(this.target);
					}
				}
				catch
				{
				}
			}

			// Token: 0x040003F1 RID: 1009
			[Nullable(0)]
			public Control tipHost;

			// Token: 0x040003F2 RID: 1010
			[Nullable(0)]
			public Control target;

			// Token: 0x040003F3 RID: 1011
			[Nullable(0)]
			public string affixId;

			// Token: 0x040003F4 RID: 1012
			public int? amount;
		}
	}
}
