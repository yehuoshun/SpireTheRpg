using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace SpireTheRpg.Core
{
	// Token: 0x0200003A RID: 58
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgCombatBuffApplier
	{
		// Token: 0x06000213 RID: 531 RVA: 0x00011208 File Offset: 0x0000F408
		public static Task ApplyEquipped(Creature creature)
		{
			RpgCombatBuffApplier.<ApplyEquipped>d__3 <ApplyEquipped>d__;
			<ApplyEquipped>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ApplyEquipped>d__.creature = creature;
			<ApplyEquipped>d__.<>1__state = -1;
			<ApplyEquipped>d__.<>t__builder.Start<RpgCombatBuffApplier.<ApplyEquipped>d__3>(ref <ApplyEquipped>d__);
			return <ApplyEquipped>d__.<>t__builder.Task;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0001124C File Offset: 0x0000F44C
		private static IEnumerable<RpgStatMod> CollectMods(RpgItemData item)
		{
			RpgCombatBuffApplier.<>c__DisplayClass4_0 CS$<>8__locals1;
			CS$<>8__locals1.list = new List<RpgStatMod>();
			RpgCombatBuffApplier.<CollectMods>g__Add|4_0(item.Primary, ref CS$<>8__locals1);
			foreach (string s in item.Details)
			{
				RpgCombatBuffApplier.<CollectMods>g__Add|4_0(s, ref CS$<>8__locals1);
			}
			foreach (string s2 in item.EffectiveAbilityLines())
			{
				RpgCombatBuffApplier.<CollectMods>g__Add|4_0(s2, ref CS$<>8__locals1);
			}
			return CS$<>8__locals1.list;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000112F8 File Offset: 0x0000F4F8
		private static bool IsAttackStat(string id)
		{
			return id.Equals("strength", StringComparison.OrdinalIgnoreCase) || id.Equals("overpower_damage", StringComparison.OrdinalIgnoreCase) || id.Equals("damage_while_berserking", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00011324 File Offset: 0x0000F524
		private static bool IsSafeCombatDebuff(string id)
		{
			return id.Equals("weak", StringComparison.OrdinalIgnoreCase) || id.Equals("frail", StringComparison.OrdinalIgnoreCase) || id.Equals("vulnerable", StringComparison.OrdinalIgnoreCase) || id.Equals("poison", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00011360 File Offset: 0x0000F560
		public static Task<bool> ApplyByPowerName(PlayerChoiceContext ctx, Creature creature, string powerName, int amount)
		{
			RpgCombatBuffApplier.<ApplyByPowerName>d__7 <ApplyByPowerName>d__;
			<ApplyByPowerName>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ApplyByPowerName>d__.ctx = ctx;
			<ApplyByPowerName>d__.creature = creature;
			<ApplyByPowerName>d__.powerName = powerName;
			<ApplyByPowerName>d__.amount = amount;
			<ApplyByPowerName>d__.<>1__state = -1;
			<ApplyByPowerName>d__.<>t__builder.Start<RpgCombatBuffApplier.<ApplyByPowerName>d__7>(ref <ApplyByPowerName>d__);
			return <ApplyByPowerName>d__.<>t__builder.Task;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000113BC File Offset: 0x0000F5BC
		private static Task<bool> TryApplyLegacy(PlayerChoiceContext ctx, Creature creature, string id, int amount)
		{
			RpgCombatBuffApplier.<TryApplyLegacy>d__8 <TryApplyLegacy>d__;
			<TryApplyLegacy>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<TryApplyLegacy>d__.ctx = ctx;
			<TryApplyLegacy>d__.creature = creature;
			<TryApplyLegacy>d__.id = id;
			<TryApplyLegacy>d__.amount = amount;
			<TryApplyLegacy>d__.<>1__state = -1;
			<TryApplyLegacy>d__.<>t__builder.Start<RpgCombatBuffApplier.<TryApplyLegacy>d__8>(ref <TryApplyLegacy>d__);
			return <TryApplyLegacy>d__.<>t__builder.Task;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00011418 File Offset: 0x0000F618
		private static Task<bool> ApplyTyped<[Nullable(0)] T>(PlayerChoiceContext ctx, Creature creature, int amount) where T : PowerModel
		{
			RpgCombatBuffApplier.<ApplyTyped>d__9<T> <ApplyTyped>d__;
			<ApplyTyped>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ApplyTyped>d__.ctx = ctx;
			<ApplyTyped>d__.creature = creature;
			<ApplyTyped>d__.amount = amount;
			<ApplyTyped>d__.<>1__state = -1;
			<ApplyTyped>d__.<>t__builder.Start<RpgCombatBuffApplier.<ApplyTyped>d__9<T>>(ref <ApplyTyped>d__);
			return <ApplyTyped>d__.<>t__builder.Task;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0001146C File Offset: 0x0000F66C
		private static Task<bool> TryApplyNamed(PlayerChoiceContext ctx, Creature creature, string powerName, int amount)
		{
			RpgCombatBuffApplier.<TryApplyNamed>d__10 <TryApplyNamed>d__;
			<TryApplyNamed>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<TryApplyNamed>d__.ctx = ctx;
			<TryApplyNamed>d__.creature = creature;
			<TryApplyNamed>d__.powerName = powerName;
			<TryApplyNamed>d__.amount = amount;
			<TryApplyNamed>d__.<>1__state = -1;
			<TryApplyNamed>d__.<>t__builder.Start<RpgCombatBuffApplier.<TryApplyNamed>d__10>(ref <TryApplyNamed>d__);
			return <TryApplyNamed>d__.<>t__builder.Task;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000114C8 File Offset: 0x0000F6C8
		private static void EnsureResolved()
		{
			if (RpgCombatBuffApplier._resolved)
			{
				return;
			}
			RpgCombatBuffApplier._resolved = true;
			try
			{
				RpgCombatBuffApplier._applyMethod = (from m in typeof(PowerCmd).GetMethods(BindingFlags.Static | BindingFlags.Public)
				where m.Name == "Apply" && m.IsGenericMethodDefinition
				select m).FirstOrDefault(delegate(MethodInfo m)
				{
					ParameterInfo[] parameters = m.GetParameters();
					return parameters.Length == 6 && parameters[1].ParameterType == typeof(Creature);
				});
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
						RpgCombatBuffApplier.PowerTypes[name] = type;
						if (name.EndsWith("Power", StringComparison.Ordinal) && name.Length > 5)
						{
							Dictionary<string, Type> powerTypes = RpgCombatBuffApplier.PowerTypes;
							string text = name;
							powerTypes[text.Substring(0, text.Length - 5)] = type;
						}
					}
				}
				Logger logger = SpireTheRpgMainFile.Logger;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(41, 2);
				defaultInterpolatedStringHandler.AppendLiteral("RpgCombatBuffApplier ready powers=");
				defaultInterpolatedStringHandler.AppendFormatted<int>(RpgCombatBuffApplier.PowerTypes.Count / 2);
				defaultInterpolatedStringHandler.AppendLiteral(" apply=");
				defaultInterpolatedStringHandler.AppendFormatted<bool>(RpgCombatBuffApplier._applyMethod != null);
				logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			}
			catch (Exception ex2)
			{
				SpireTheRpgMainFile.Logger.Info("RpgCombatBuffApplier resolve failed: " + ex2.Message, 1);
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000116C4 File Offset: 0x0000F8C4
		// Note: this type is marked as 'beforefieldinit'.
		static RpgCombatBuffApplier()
		{
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000116D8 File Offset: 0x0000F8D8
		[NullableContext(2)]
		[CompilerGenerated]
		internal static void <CollectMods>g__Add|4_0(string s, ref RpgCombatBuffApplier.<>c__DisplayClass4_0 A_1)
		{
			RpgStatMod rpgStatMod = RpgStatMod.Parse(s);
			if (rpgStatMod != null)
			{
				A_1.list.Add(rpgStatMod);
			}
		}

		// Token: 0x040000A9 RID: 169
		private static readonly Dictionary<string, Type> PowerTypes = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040000AA RID: 170
		[Nullable(2)]
		private static MethodInfo _applyMethod;

		// Token: 0x040000AB RID: 171
		private static bool _resolved;

		// Token: 0x020000DD RID: 221
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006D0 RID: 1744 RVA: 0x00023557 File Offset: 0x00021757
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006D1 RID: 1745 RVA: 0x00023563 File Offset: 0x00021763
			public <>c()
			{
			}

			// Token: 0x060006D2 RID: 1746 RVA: 0x0002356B File Offset: 0x0002176B
			[NullableContext(0)]
			internal bool <EnsureResolved>b__11_0(MethodInfo m)
			{
				return m.Name == "Apply" && m.IsGenericMethodDefinition;
			}

			// Token: 0x060006D3 RID: 1747 RVA: 0x00023588 File Offset: 0x00021788
			[NullableContext(0)]
			internal bool <EnsureResolved>b__11_1(MethodInfo m)
			{
				ParameterInfo[] parameters = m.GetParameters();
				return parameters.Length == 6 && parameters[1].ParameterType == typeof(Creature);
			}

			// Token: 0x060006D4 RID: 1748 RVA: 0x000235BB File Offset: 0x000217BB
			[NullableContext(0)]
			internal bool <EnsureResolved>b__11_2(Type t)
			{
				return t != null;
			}

			// Token: 0x0400030A RID: 778
			[Nullable(0)]
			public static readonly RpgCombatBuffApplier.<>c <>9 = new RpgCombatBuffApplier.<>c();

			// Token: 0x0400030B RID: 779
			[Nullable(0)]
			public static Func<MethodInfo, bool> <>9__11_0;

			// Token: 0x0400030C RID: 780
			[Nullable(0)]
			public static Func<MethodInfo, bool> <>9__11_1;

			// Token: 0x0400030D RID: 781
			[Nullable(0)]
			public static Func<Type, bool> <>9__11_2;
		}

		// Token: 0x020000DE RID: 222
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass4_0
		{
			// Token: 0x0400030E RID: 782
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public List<RpgStatMod> list;
		}

		// Token: 0x020000DF RID: 223
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ApplyByPowerName>d__7 : IAsyncStateMachine
		{
			// Token: 0x060006D5 RID: 1749 RVA: 0x000235C4 File Offset: 0x000217C4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result;
				try
				{
					TaskAwaiter<bool> awaiter;
					if (num != 0)
					{
						RpgCombatBuffApplier.EnsureResolved();
						awaiter = RpgCombatBuffApplier.TryApplyNamed(this.ctx, this.creature, this.powerName, this.amount).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgCombatBuffApplier.<ApplyByPowerName>d__7>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
					}
					result = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060006D6 RID: 1750 RVA: 0x00023690 File Offset: 0x00021890
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400030F RID: 783
			public int <>1__state;

			// Token: 0x04000310 RID: 784
			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000311 RID: 785
			[Nullable(0)]
			public PlayerChoiceContext ctx;

			// Token: 0x04000312 RID: 786
			[Nullable(0)]
			public Creature creature;

			// Token: 0x04000313 RID: 787
			[Nullable(0)]
			public string powerName;

			// Token: 0x04000314 RID: 788
			public int amount;

			// Token: 0x04000315 RID: 789
			[Nullable(0)]
			private TaskAwaiter<bool> <>u__1;
		}

		// Token: 0x020000E0 RID: 224
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ApplyEquipped>d__3 : IAsyncStateMachine
		{
			// Token: 0x060006D7 RID: 1751 RVA: 0x000236A0 File Offset: 0x000218A0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					bool flag;
					TaskAwaiter<bool> awaiter;
					if (num != 0)
					{
						if (num - 1 <= 1)
						{
							goto IL_153;
						}
						if (RpgNet.IsMultiplayerRun())
						{
							Logger logger = SpireTheRpgMainFile.Logger;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(45, 1);
							defaultInterpolatedStringHandler.AppendLiteral("RpgCombatBuffApplier skip PowerCmd in MP net=");
							Player player = this.creature.Player;
							defaultInterpolatedStringHandler.AppendFormatted<ulong?>((player != null) ? new ulong?(player.NetId) : null);
							logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
							goto IL_47F;
						}
						RpgCombatBuffApplier.EnsureResolved();
						this.<ctx>5__2 = new ThrowingPlayerChoiceContext();
						this.<applied>5__3 = 0;
						this.<earlyTurns>5__4 = RpgEffectiveConfig.EarlyCombatBonusTurns;
						int num2 = RpgActiveLoadout.FlatStat("armor", this.creature.Player);
						flag = (num2 > 0);
						if (!flag)
						{
							goto IL_122;
						}
						awaiter = RpgCombatBuffApplier.ApplyTyped<PlatingPower>(this.<ctx>5__2, this.creature, num2).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgCombatBuffApplier.<ApplyEquipped>d__3>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						num = (this.<>1__state = -1);
					}
					flag = awaiter.GetResult();
					IL_122:
					if (flag)
					{
						int num3 = this.<applied>5__3;
						this.<applied>5__3 = num3 + 1;
					}
					this.<>7__wrap4 = RpgActiveLoadout.EquippedItems(this.creature.Player).GetEnumerator();
					IL_153:
					try
					{
						if (num - 1 <= 1)
						{
							goto IL_1C1;
						}
						IL_3EF:
						while (this.<>7__wrap4.MoveNext())
						{
							this.<item>5__6 = this.<>7__wrap4.Current;
							if (this.<item>5__6.IsIdentified && RpgActiveLoadout.MeetsLevelRequirement(this.<item>5__6) && RpgAccessoryGates.ContributesStats(this.<item>5__6, this.creature.Player))
							{
								this.<>7__wrap6 = RpgCombatBuffApplier.CollectMods(this.<item>5__6).GetEnumerator();
								goto IL_1C1;
							}
						}
						goto IL_419;
						IL_1C1:
						try
						{
							if (num == 1)
							{
								awaiter = this.<>u__1;
								this.<>u__1 = default(TaskAwaiter<bool>);
								num = (this.<>1__state = -1);
								goto IL_30E;
							}
							if (num == 2)
							{
								awaiter = this.<>u__1;
								this.<>u__1 = default(TaskAwaiter<bool>);
								num = (this.<>1__state = -1);
								goto IL_39C;
							}
							IL_3B7:
							while (this.<>7__wrap6.MoveNext())
							{
								RpgStatMod rpgStatMod = this.<>7__wrap6.Current;
								if (!rpgStatMod.Percent && !string.Equals(rpgStatMod.Id, "armor", StringComparison.OrdinalIgnoreCase) && !string.Equals(rpgStatMod.Id, "all_stats", StringComparison.OrdinalIgnoreCase) && !RpgAffixCatalog.IsBannedId(rpgStatMod.Id))
								{
									int num4 = (int)Math.Round(rpgStatMod.Value);
									if (num4 > 0)
									{
										if (this.<earlyTurns>5__4 > 1 && this.<item>5__6.Rarity < RpgItemRarity.Rare && RpgCombatBuffApplier.IsAttackStat(rpgStatMod.Id))
										{
											num4 *= this.<earlyTurns>5__4;
										}
										RpgAffixDef rpgAffixDef = RpgAffixCatalog.Get(rpgStatMod.Id);
										if (rpgAffixDef != null)
										{
											if (!rpgAffixDef.IsDebuff || RpgCombatBuffApplier.IsSafeCombatDebuff(rpgAffixDef.Id))
											{
												awaiter = RpgCombatBuffApplier.TryApplyNamed(this.<ctx>5__2, this.creature, rpgAffixDef.Power, num4).GetAwaiter();
												if (!awaiter.IsCompleted)
												{
													num = (this.<>1__state = 1);
													this.<>u__1 = awaiter;
													this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgCombatBuffApplier.<ApplyEquipped>d__3>(ref awaiter, ref this);
													return;
												}
												goto IL_30E;
											}
										}
										else
										{
											awaiter = RpgCombatBuffApplier.TryApplyLegacy(this.<ctx>5__2, this.creature, rpgStatMod.Id, num4).GetAwaiter();
											if (!awaiter.IsCompleted)
											{
												num = (this.<>1__state = 2);
												this.<>u__1 = awaiter;
												this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgCombatBuffApplier.<ApplyEquipped>d__3>(ref awaiter, ref this);
												return;
											}
											goto IL_39C;
										}
									}
								}
							}
							goto IL_3E1;
							IL_30E:
							if (awaiter.GetResult())
							{
								int num3 = this.<applied>5__3;
								this.<applied>5__3 = num3 + 1;
								goto IL_3B7;
							}
							goto IL_3B7;
							IL_39C:
							if (awaiter.GetResult())
							{
								int num3 = this.<applied>5__3;
								this.<applied>5__3 = num3 + 1;
								goto IL_3B7;
							}
							goto IL_3B7;
						}
						finally
						{
							if (num < 0 && this.<>7__wrap6 != null)
							{
								this.<>7__wrap6.Dispose();
							}
						}
						IL_3E1:
						this.<>7__wrap6 = null;
						this.<item>5__6 = null;
						goto IL_3EF;
					}
					finally
					{
						if (num < 0 && this.<>7__wrap4 != null)
						{
							this.<>7__wrap4.Dispose();
						}
					}
					IL_419:
					this.<>7__wrap4 = null;
					if (this.<applied>5__3 > 0)
					{
						Logger logger2 = SpireTheRpgMainFile.Logger;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 1);
						defaultInterpolatedStringHandler.AppendLiteral("RpgCombatBuffApplier applied=");
						defaultInterpolatedStringHandler.AppendFormatted<int>(this.<applied>5__3);
						logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<ctx>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_47F:
				this.<>1__state = -2;
				this.<ctx>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060006D8 RID: 1752 RVA: 0x00023B94 File Offset: 0x00021D94
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000316 RID: 790
			public int <>1__state;

			// Token: 0x04000317 RID: 791
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000318 RID: 792
			[Nullable(0)]
			public Creature creature;

			// Token: 0x04000319 RID: 793
			[Nullable(0)]
			private ThrowingPlayerChoiceContext <ctx>5__2;

			// Token: 0x0400031A RID: 794
			private int <applied>5__3;

			// Token: 0x0400031B RID: 795
			private int <earlyTurns>5__4;

			// Token: 0x0400031C RID: 796
			[Nullable(0)]
			private TaskAwaiter<bool> <>u__1;

			// Token: 0x0400031D RID: 797
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private IEnumerator<RpgItemData> <>7__wrap4;

			// Token: 0x0400031E RID: 798
			[Nullable(0)]
			private RpgItemData <item>5__6;

			// Token: 0x0400031F RID: 799
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private IEnumerator<RpgStatMod> <>7__wrap6;
		}

		// Token: 0x020000E1 RID: 225
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ApplyTyped>d__9<T> : IAsyncStateMachine where T : PowerModel
		{
			// Token: 0x060006D9 RID: 1753 RVA: 0x00023BA4 File Offset: 0x00021DA4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result;
				try
				{
					try
					{
						TaskAwaiter<T> awaiter;
						if (num != 0)
						{
							awaiter = PowerCmd.Apply<T>(this.ctx, this.creature, this.amount, null, null, true).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<T>, RpgCombatBuffApplier.<ApplyTyped>d__9<T>>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter<T>);
							this.<>1__state = -1;
						}
						awaiter.GetResult();
						result = true;
					}
					catch (Exception ex)
					{
						Logger logger = SpireTheRpgMainFile.Logger;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 3);
						defaultInterpolatedStringHandler.AppendLiteral("Apply ");
						defaultInterpolatedStringHandler.AppendFormatted(typeof(T).Name);
						defaultInterpolatedStringHandler.AppendLiteral("x");
						defaultInterpolatedStringHandler.AppendFormatted<int>(this.amount);
						defaultInterpolatedStringHandler.AppendLiteral(" failed: ");
						defaultInterpolatedStringHandler.AppendFormatted(ex.GetBaseException().Message);
						logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
						result = false;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060006DA RID: 1754 RVA: 0x00023D00 File Offset: 0x00021F00
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000320 RID: 800
			public int <>1__state;

			// Token: 0x04000321 RID: 801
			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000322 RID: 802
			[Nullable(0)]
			public PlayerChoiceContext ctx;

			// Token: 0x04000323 RID: 803
			[Nullable(0)]
			public Creature creature;

			// Token: 0x04000324 RID: 804
			public int amount;

			// Token: 0x04000325 RID: 805
			[Nullable(new byte[]
			{
				0,
				2
			})]
			private TaskAwaiter<T> <>u__1;
		}

		// Token: 0x020000E2 RID: 226
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <TryApplyLegacy>d__8 : IAsyncStateMachine
		{
			// Token: 0x060006DB RID: 1755 RVA: 0x00023D10 File Offset: 0x00021F10
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result;
				try
				{
					TaskAwaiter<bool> awaiter;
					bool flag;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_2B6;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_32B;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_3A0;
					case 4:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_415;
					case 5:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_48A;
					case 6:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_4FF;
					case 7:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_574;
					case 8:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_5E9;
					case 9:
						awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<bool>);
						this.<>1__state = -1;
						goto IL_659;
					default:
					{
						string text = this.id.ToLowerInvariant();
						if (text != null)
						{
							int length = text.Length;
							switch (length)
							{
							case 8:
							{
								char c = text[0];
								if (c != 's')
								{
									if (c != 'v')
									{
										goto IL_663;
									}
									if (!(text == "vitality"))
									{
										goto IL_663;
									}
								}
								else
								{
									if (!(text == "strength"))
									{
										goto IL_663;
									}
									awaiter = RpgCombatBuffApplier.ApplyTyped<StrengthPower>(this.ctx, this.creature, this.amount).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										this.<>1__state = 0;
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgCombatBuffApplier.<TryApplyLegacy>d__8>(ref awaiter, ref this);
										return;
									}
									goto IL_241;
								}
								break;
							}
							case 9:
								if (!(text == "dexterity"))
								{
									goto IL_663;
								}
								awaiter = RpgCombatBuffApplier.ApplyTyped<DexterityPower>(this.ctx, this.creature, this.amount).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgCombatBuffApplier.<TryApplyLegacy>d__8>(ref awaiter, ref this);
									return;
								}
								goto IL_2B6;
							case 10:
							case 11:
							case 14:
							case 15:
							case 17:
								goto IL_663;
							case 12:
							{
								char c = text[0];
								if (c != 'b')
								{
									if (c != 'i')
									{
										goto IL_663;
									}
									if (!(text == "intelligence"))
									{
										goto IL_663;
									}
									awaiter = RpgCombatBuffApplier.ApplyTyped<ArtifactPower>(this.ctx, this.creature, this.amount).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										this.<>1__state = 2;
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgCombatBuffApplier.<TryApplyLegacy>d__8>(ref awaiter, ref this);
										return;
									}
									goto IL_32B;
								}
								else
								{
									if (!(text == "block_chance"))
									{
										goto IL_663;
									}
									awaiter = RpgCombatBuffApplier.ApplyTyped<BlurPower>(this.ctx, this.creature, this.amount).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										this.<>1__state = 8;
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgCombatBuffApplier.<TryApplyLegacy>d__8>(ref awaiter, ref this);
										return;
									}
									goto IL_5E9;
								}
								break;
							}
							case 13:
								if (!(text == "nature_damage"))
								{
									goto IL_663;
								}
								awaiter = RpgCombatBuffApplier.ApplyTyped<NoxiousFumesPower>(this.ctx, this.creature, this.amount).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 5;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgCombatBuffApplier.<TryApplyLegacy>d__8>(ref awaiter, ref this);
									return;
								}
								goto IL_48A;
							case 16:
							{
								char c = text[0];
								if (c != 'd')
								{
									if (c != 'h')
									{
										if (c != 'o')
										{
											goto IL_663;
										}
										if (!(text == "overpower_damage"))
										{
											goto IL_663;
										}
										awaiter = RpgCombatBuffApplier.ApplyTyped<VigorPower>(this.ctx, this.creature, this.amount).GetAwaiter();
										if (!awaiter.IsCompleted)
										{
											this.<>1__state = 6;
											this.<>u__1 = awaiter;
											this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgCombatBuffApplier.<TryApplyLegacy>d__8>(ref awaiter, ref this);
											return;
										}
										goto IL_4FF;
									}
									else
									{
										if (!(text == "healing_received"))
										{
											goto IL_663;
										}
										awaiter = RpgCombatBuffApplier.ApplyTyped<RegenPower>(this.ctx, this.creature, this.amount).GetAwaiter();
										if (!awaiter.IsCompleted)
										{
											this.<>1__state = 4;
											this.<>u__1 = awaiter;
											this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgCombatBuffApplier.<TryApplyLegacy>d__8>(ref awaiter, ref this);
											return;
										}
										goto IL_415;
									}
								}
								else if (!(text == "damage_reduction"))
								{
									goto IL_663;
								}
								break;
							}
							case 18:
								if (!(text == "cooldown_reduction"))
								{
									goto IL_663;
								}
								awaiter = RpgCombatBuffApplier.ApplyTyped<EnergyNextTurnPower>(this.ctx, this.creature, this.amount).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 7;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgCombatBuffApplier.<TryApplyLegacy>d__8>(ref awaiter, ref this);
									return;
								}
								goto IL_574;
							default:
								if (length != 23)
								{
									goto IL_663;
								}
								if (!(text == "damage_while_berserking"))
								{
									goto IL_663;
								}
								awaiter = RpgCombatBuffApplier.ApplyTyped<AggressionPower>(this.ctx, this.creature, this.amount).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 9;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgCombatBuffApplier.<TryApplyLegacy>d__8>(ref awaiter, ref this);
									return;
								}
								goto IL_659;
							}
							awaiter = RpgCombatBuffApplier.ApplyTyped<BufferPower>(this.ctx, this.creature, this.amount).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 3;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<bool>, RpgCombatBuffApplier.<TryApplyLegacy>d__8>(ref awaiter, ref this);
								return;
							}
							goto IL_3A0;
						}
						IL_663:
						flag = false;
						goto IL_665;
					}
					}
					IL_241:
					flag = awaiter.GetResult();
					goto IL_665;
					IL_2B6:
					flag = awaiter.GetResult();
					goto IL_665;
					IL_32B:
					flag = awaiter.GetResult();
					goto IL_665;
					IL_3A0:
					flag = awaiter.GetResult();
					goto IL_665;
					IL_415:
					flag = awaiter.GetResult();
					goto IL_665;
					IL_48A:
					flag = awaiter.GetResult();
					goto IL_665;
					IL_4FF:
					flag = awaiter.GetResult();
					goto IL_665;
					IL_574:
					flag = awaiter.GetResult();
					goto IL_665;
					IL_5E9:
					flag = awaiter.GetResult();
					goto IL_665;
					IL_659:
					flag = awaiter.GetResult();
					IL_665:
					result = flag;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060006DC RID: 1756 RVA: 0x000243D0 File Offset: 0x000225D0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000326 RID: 806
			public int <>1__state;

			// Token: 0x04000327 RID: 807
			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000328 RID: 808
			[Nullable(0)]
			public string id;

			// Token: 0x04000329 RID: 809
			[Nullable(0)]
			public PlayerChoiceContext ctx;

			// Token: 0x0400032A RID: 810
			[Nullable(0)]
			public Creature creature;

			// Token: 0x0400032B RID: 811
			public int amount;

			// Token: 0x0400032C RID: 812
			[Nullable(0)]
			private TaskAwaiter<bool> <>u__1;
		}

		// Token: 0x020000E3 RID: 227
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <TryApplyNamed>d__10 : IAsyncStateMachine
		{
			// Token: 0x060006DD RID: 1757 RVA: 0x000243E0 File Offset: 0x000225E0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result;
				try
				{
					Type type;
					if (num != 0)
					{
						if (RpgCombatBuffApplier._applyMethod == null)
						{
							result = false;
							goto IL_1A1;
						}
						if (!RpgCombatBuffApplier.PowerTypes.TryGetValue(this.powerName, out type) && !RpgCombatBuffApplier.PowerTypes.TryGetValue(this.powerName + "Power", out type))
						{
							result = false;
							goto IL_1A1;
						}
					}
					try
					{
						TaskAwaiter awaiter;
						if (num != 0)
						{
							Task task = RpgCombatBuffApplier._applyMethod.MakeGenericMethod(new Type[]
							{
								type
							}).Invoke(null, new object[]
							{
								this.ctx,
								this.creature,
								this.amount,
								null,
								null,
								true
							}) as Task;
							if (task == null)
							{
								goto IL_10E;
							}
							awaiter = task.GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgCombatBuffApplier.<TryApplyNamed>d__10>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							this.<>1__state = -1;
						}
						awaiter.GetResult();
						IL_10E:
						result = true;
					}
					catch (Exception ex)
					{
						Logger logger = SpireTheRpgMainFile.Logger;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(16, 3);
						defaultInterpolatedStringHandler.AppendLiteral("Apply ");
						defaultInterpolatedStringHandler.AppendFormatted(this.powerName);
						defaultInterpolatedStringHandler.AppendLiteral("x");
						defaultInterpolatedStringHandler.AppendFormatted<int>(this.amount);
						defaultInterpolatedStringHandler.AppendLiteral(" failed: ");
						defaultInterpolatedStringHandler.AppendFormatted(ex.GetBaseException().Message);
						logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
						result = false;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1A1:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060006DE RID: 1758 RVA: 0x000245D8 File Offset: 0x000227D8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400032D RID: 813
			public int <>1__state;

			// Token: 0x0400032E RID: 814
			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x0400032F RID: 815
			[Nullable(0)]
			public string powerName;

			// Token: 0x04000330 RID: 816
			[Nullable(0)]
			public PlayerChoiceContext ctx;

			// Token: 0x04000331 RID: 817
			[Nullable(0)]
			public Creature creature;

			// Token: 0x04000332 RID: 818
			public int amount;

			// Token: 0x04000333 RID: 819
			private TaskAwaiter <>u__1;
		}
	}
}
