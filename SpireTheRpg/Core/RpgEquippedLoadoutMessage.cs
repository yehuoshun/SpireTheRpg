using System;
using System.Runtime.CompilerServices;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Multiplayer.Serialization;

namespace SpireTheRpg.Core
{
	// Token: 0x0200003F RID: 63
	public sealed class RpgEquippedLoadoutMessage : ICustomMessage, IPacketSerializable
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00011D63 File Offset: 0x0000FF63
		public bool ShouldBroadcast
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00011D66 File Offset: 0x0000FF66
		public void HandleMessage(ulong senderId)
		{
			RpgLoadoutNetSync.ApplyEquippedFromNet(this.OwnerNetId, this.Json);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00011D79 File Offset: 0x0000FF79
		[NullableContext(1)]
		public void Serialize(PacketWriter writer)
		{
			writer.WriteULong(this.OwnerNetId, 64);
			writer.WriteString(this.Json ?? "");
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00011D9E File Offset: 0x0000FF9E
		[NullableContext(1)]
		public void Deserialize(PacketReader reader)
		{
			this.OwnerNetId = reader.ReadULong(64);
			this.Json = (reader.ReadString() ?? "");
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00011DC3 File Offset: 0x0000FFC3
		public RpgEquippedLoadoutMessage()
		{
		}

		// Token: 0x040000B0 RID: 176
		public ulong OwnerNetId;

		// Token: 0x040000B1 RID: 177
		[Nullable(1)]
		public string Json = "";
	}
}
