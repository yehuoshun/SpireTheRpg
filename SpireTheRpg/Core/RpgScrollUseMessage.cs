using System;
using System.Runtime.CompilerServices;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Multiplayer.Serialization;

namespace SpireTheRpg.Core
{
	// Token: 0x02000062 RID: 98
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RpgScrollUseMessage : ICustomMessage, IPacketSerializable
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00019F01 File Offset: 0x00018101
		public bool ShouldBroadcast
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00019F04 File Offset: 0x00018104
		public void HandleMessage(ulong senderId)
		{
			RpgScrolls.ApplyUseFromNet(this);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00019F0C File Offset: 0x0001810C
		public void Serialize(PacketWriter writer)
		{
			writer.WriteULong(this.OwnerNetId, 64);
			writer.WriteString(this.ScrollInstanceId ?? "");
			writer.WriteInt(this.Kind, 32);
			writer.WriteInt(this.GoldAfter, 32);
			writer.WriteBool(this.MagicArmed);
			writer.WriteString(this.ItemJson ?? "");
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00019F7C File Offset: 0x0001817C
		public void Deserialize(PacketReader reader)
		{
			this.OwnerNetId = reader.ReadULong(64);
			this.ScrollInstanceId = (reader.ReadString() ?? "");
			this.Kind = reader.ReadInt(32);
			this.GoldAfter = reader.ReadInt(32);
			this.MagicArmed = reader.ReadBool();
			this.ItemJson = (reader.ReadString() ?? "");
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00019FE9 File Offset: 0x000181E9
		public RpgScrollUseMessage()
		{
		}

		// Token: 0x0400015F RID: 351
		public ulong OwnerNetId;

		// Token: 0x04000160 RID: 352
		public string ScrollInstanceId = "";

		// Token: 0x04000161 RID: 353
		public int Kind;

		// Token: 0x04000162 RID: 354
		public int GoldAfter = -1;

		// Token: 0x04000163 RID: 355
		public bool MagicArmed;

		// Token: 0x04000164 RID: 356
		public string ItemJson = "";
	}
}
