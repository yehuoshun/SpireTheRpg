using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpireTheRpg.Combat
{
	// Token: 0x02000075 RID: 117
	internal static class SharedJson
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x0001CA9B File Offset: 0x0001AC9B
		// Note: this type is marked as 'beforefieldinit'.
		static SharedJson()
		{
		}

		// Token: 0x040001AC RID: 428
		[Nullable(1)]
		public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			Converters = 
			{
				new JsonStringEnumConverter()
			}
		};
	}
}
