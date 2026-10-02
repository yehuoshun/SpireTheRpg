using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Runs;

namespace SpireTheRpg.Combat
{
	// Token: 0x02000077 RID: 119
	[NullableContext(2)]
	[Nullable(0)]
	internal static class RpgRng
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x0001CBAC File Offset: 0x0001ADAC
		static RpgRng()
		{
			foreach (ConstructorInfo constructorInfo in typeof(Rng).GetConstructors())
			{
				ParameterInfo[] parameters = constructorInfo.GetParameters();
				if (parameters.Length == 1 && parameters[0].ParameterType == typeof(ulong))
				{
					RpgRng.CtorULong = constructorInfo;
				}
				else if (parameters.Length == 1 && parameters[0].ParameterType == typeof(uint))
				{
					RpgRng.CtorUInt = constructorInfo;
				}
				else if (parameters.Length == 2 && parameters[0].ParameterType == typeof(uint) && parameters[1].ParameterType == typeof(int))
				{
					RpgRng.CtorUIntCounter = constructorInfo;
				}
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0001CC74 File Offset: 0x0001AE74
		public static ulong SeedOf(object rngSet)
		{
			if (rngSet == null)
			{
				return 0UL;
			}
			ulong result;
			try
			{
				PropertyInfo property = rngSet.GetType().GetProperty("Seed");
				object obj = (property != null) ? property.GetValue(rngSet) : null;
				ulong num2;
				if (obj is ulong)
				{
					ulong num = (ulong)obj;
					num2 = num;
				}
				else if (obj is uint)
				{
					uint num3 = (uint)obj;
					num2 = (ulong)num3;
				}
				else if (obj is int)
				{
					int num4 = (int)obj;
					num2 = (ulong)num4;
				}
				else if (obj is long)
				{
					long num5 = (long)obj;
					num2 = (ulong)num5;
				}
				else
				{
					num2 = 0UL;
				}
				result = num2;
			}
			catch
			{
				result = 0UL;
			}
			return result;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001CD28 File Offset: 0x0001AF28
		[NullableContext(1)]
		public static ulong RunSeedOf(Player player)
		{
			try
			{
				IRunState runState = player.RunState;
				ulong num = RpgRng.SeedOf((runState != null) ? runState.Rng : null);
				if (num != 0UL)
				{
					return num;
				}
			}
			catch
			{
			}
			ulong result;
			try
			{
				result = RpgRng.SeedOf(player.PlayerRng);
			}
			catch
			{
				result = 0UL;
			}
			return result;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001CD8C File Offset: 0x0001AF8C
		[NullableContext(1)]
		public static Rng FromSeed(ulong seed)
		{
			if (RpgRng.CtorULong != null)
			{
				return (Rng)RpgRng.CtorULong.Invoke(new object[]
				{
					seed
				});
			}
			if (RpgRng.CtorUIntCounter != null)
			{
				return (Rng)RpgRng.CtorUIntCounter.Invoke(new object[]
				{
					(uint)seed,
					0
				});
			}
			if (RpgRng.CtorUInt != null)
			{
				return (Rng)RpgRng.CtorUInt.Invoke(new object[]
				{
					(uint)seed
				});
			}
			throw new MissingMethodException("Rng seed constructor not found");
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001CE32 File Offset: 0x0001B032
		public static ulong Mix(ulong a, ulong b, ulong c = 0UL, ulong d = 0UL, ulong e = 0UL)
		{
			return (((a * 374761393UL + b) * 668265263UL + c) * (ulong)-2048144777 + d) * (ulong)-1028477379 + e;
		}

		// Token: 0x040001AF RID: 431
		private static readonly ConstructorInfo CtorULong;

		// Token: 0x040001B0 RID: 432
		private static readonly ConstructorInfo CtorUInt;

		// Token: 0x040001B1 RID: 433
		private static readonly ConstructorInfo CtorUIntCounter;
	}
}
