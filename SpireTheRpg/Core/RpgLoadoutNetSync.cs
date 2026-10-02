using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Runs;
using SpireTheRpg.Combat;

namespace SpireTheRpg.Core
{
	// Token: 0x0200004D RID: 77
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RpgLoadoutNetSync
	{
		// Token: 0x06000307 RID: 775 RVA: 0x000156E4 File Offset: 0x000138E4
		public static void PushEquipped(ulong netId)
		{
			if (RpgLoadoutNetSync._applyingRemote > 0)
			{
				return;
			}
			if (netId != RpgNet.LocalNetId())
			{
				return;
			}
			try
			{
				if (RpgNet.CanSendCustom())
				{
					string text = RpgLoadoutNetSync.CaptureEquipped(netId);
					CustomMessageWrapper.Send(new RpgEquippedLoadoutMessage
					{
						OwnerNetId = netId,
						Json = text
					}, null);
					Logger logger = SpireTheRpgMainFile.Logger;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 2);
					defaultInterpolatedStringHandler.AppendLiteral("RpgLoadoutNetSync equipped push-msg net=");
					defaultInterpolatedStringHandler.AppendFormatted<ulong>(netId);
					defaultInterpolatedStringHandler.AppendLiteral(" bytes=");
					defaultInterpolatedStringHandler.AppendFormatted<int>(text.Length);
					logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
					RpgEquippedGearSync.Request(null);
				}
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RpgLoadoutNetSync equipped push-msg failed: " + ex.Message, 1);
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000157B0 File Offset: 0x000139B0
		[NullableContext(2)]
		public static void ApplyEquippedFromNet(ulong netId, string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				return;
			}
			if (netId == RpgNet.LocalNetId())
			{
				return;
			}
			RpgLoadoutNetSync._applyingRemote++;
			try
			{
				RpgLoadoutNetSync.ApplyEquipped(netId, json);
				Logger logger = SpireTheRpgMainFile.Logger;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 2);
				defaultInterpolatedStringHandler.AppendLiteral("RpgLoadoutNetSync equipped from-msg net=");
				defaultInterpolatedStringHandler.AppendFormatted<ulong>(netId);
				defaultInterpolatedStringHandler.AppendLiteral(" bytes=");
				defaultInterpolatedStringHandler.AppendFormatted<int>(json.Length);
				logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
			}
			catch (Exception ex)
			{
				SpireTheRpgMainFile.Logger.Info("RpgLoadoutNetSync equipped from-msg failed: " + ex.Message, 1);
			}
			finally
			{
				RpgLoadoutNetSync._applyingRemote--;
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00015874 File Offset: 0x00013A74
		public static Task SyncEquippedBeforeMove(IRunState run)
		{
			RpgLoadoutNetSync.<SyncEquippedBeforeMove>d__8 <SyncEquippedBeforeMove>d__;
			<SyncEquippedBeforeMove>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SyncEquippedBeforeMove>d__.run = run;
			<SyncEquippedBeforeMove>d__.<>1__state = -1;
			<SyncEquippedBeforeMove>d__.<>t__builder.Start<RpgLoadoutNetSync.<SyncEquippedBeforeMove>d__8>(ref <SyncEquippedBeforeMove>d__);
			return <SyncEquippedBeforeMove>d__.<>t__builder.Task;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000158B8 File Offset: 0x00013AB8
		public static Task SyncAllBeforeCombat(IRunState run, [Nullable(2)] object combatKey = null)
		{
			RpgLoadoutNetSync.<SyncAllBeforeCombat>d__9 <SyncAllBeforeCombat>d__;
			<SyncAllBeforeCombat>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SyncAllBeforeCombat>d__.run = run;
			<SyncAllBeforeCombat>d__.combatKey = combatKey;
			<SyncAllBeforeCombat>d__.<>1__state = -1;
			<SyncAllBeforeCombat>d__.<>t__builder.Start<RpgLoadoutNetSync.<SyncAllBeforeCombat>d__9>(ref <SyncAllBeforeCombat>d__);
			return <SyncAllBeforeCombat>d__.<>t__builder.Task;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00015903 File Offset: 0x00013B03
		public static Task SyncAllAsync([Nullable(2)] IRunState run = null)
		{
			if (run == null)
			{
				return Task.CompletedTask;
			}
			return RpgLoadoutNetSync.SyncAllBeforeCombat(run, null);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00015915 File Offset: 0x00013B15
		public static void ClearCombatSync()
		{
			RpgLoadoutNetSync._syncedCombat = null;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00015920 File Offset: 0x00013B20
		public static Task SyncPlayerAsync(Player owner)
		{
			RpgLoadoutNetSync.<SyncPlayerAsync>d__12 <SyncPlayerAsync>d__;
			<SyncPlayerAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SyncPlayerAsync>d__.owner = owner;
			<SyncPlayerAsync>d__.<>1__state = -1;
			<SyncPlayerAsync>d__.<>t__builder.Start<RpgLoadoutNetSync.<SyncPlayerAsync>d__12>(ref <SyncPlayerAsync>d__);
			return <SyncPlayerAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00015964 File Offset: 0x00013B64
		private static Task SyncEquippedPlayerAsync(Player owner, PlayerChoiceSynchronizer sync)
		{
			RpgLoadoutNetSync.<SyncEquippedPlayerAsync>d__13 <SyncEquippedPlayerAsync>d__;
			<SyncEquippedPlayerAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SyncEquippedPlayerAsync>d__.owner = owner;
			<SyncEquippedPlayerAsync>d__.sync = sync;
			<SyncEquippedPlayerAsync>d__.<>1__state = -1;
			<SyncEquippedPlayerAsync>d__.<>t__builder.Start<RpgLoadoutNetSync.<SyncEquippedPlayerAsync>d__13>(ref <SyncEquippedPlayerAsync>d__);
			return <SyncEquippedPlayerAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000159B0 File Offset: 0x00013BB0
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		private static Task<string> WaitDecode(PlayerChoiceSynchronizer sync, Player owner, uint choiceId)
		{
			RpgLoadoutNetSync.<WaitDecode>d__14 <WaitDecode>d__;
			<WaitDecode>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<WaitDecode>d__.sync = sync;
			<WaitDecode>d__.owner = owner;
			<WaitDecode>d__.choiceId = choiceId;
			<WaitDecode>d__.<>1__state = -1;
			<WaitDecode>d__.<>t__builder.Start<RpgLoadoutNetSync.<WaitDecode>d__14>(ref <WaitDecode>d__);
			return <WaitDecode>d__.<>t__builder.Task;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00015A04 File Offset: 0x00013C04
		private static string CaptureFull(ulong netId)
		{
			RpgRunEquipmentDto rpgRunEquipmentDto = RpgRunLoadout.CaptureDto(netId);
			List<RpgItemData> valueOrDefault = RpgRunLoot.SnapshotAll().GetValueOrDefault(netId);
			List<RpgItemData> list;
			if (valueOrDefault == null)
			{
				list = null;
			}
			else
			{
				list = (from x in valueOrDefault
				select x.Clone()).ToList<RpgItemData>();
			}
			rpgRunEquipmentDto.PendingLoot = (list ?? new List<RpgItemData>());
			rpgRunEquipmentDto.Scrolls = RpgScrollBag.Snapshot(netId);
			return JsonSerializer.Serialize<RpgRunEquipmentDto>(rpgRunEquipmentDto, RpgLoadoutNetSync.JsonOptions);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00015A78 File Offset: 0x00013C78
		private static string CaptureEquipped(ulong netId)
		{
			if (netId == RpgNet.LocalNetId() && RpgEffectiveConfig.EnableMetaEquipment)
			{
				RpgRunEquipmentDto rpgRunEquipmentDto = new RpgRunEquipmentDto();
				rpgRunEquipmentDto.BagItems = new List<RpgItemData>();
				rpgRunEquipmentDto.EquippedItems = Enum.GetValues<RpgEquipSlot>().ToDictionary((RpgEquipSlot s) => s.ToString(), delegate(RpgEquipSlot s)
				{
					RpgItemData equipped = RpgLoadout.GetEquipped(s);
					if (equipped == null)
					{
						return null;
					}
					return equipped.Clone();
				}, StringComparer.OrdinalIgnoreCase);
				rpgRunEquipmentDto.PendingLoot = new List<RpgItemData>();
				rpgRunEquipmentDto.Scrolls = new List<RpgItemData>();
				return JsonSerializer.Serialize<RpgRunEquipmentDto>(rpgRunEquipmentDto, RpgLoadoutNetSync.JsonOptions);
			}
			return JsonSerializer.Serialize<RpgRunEquipmentDto>(RpgRunLoadout.CaptureEquippedDto(netId), RpgLoadoutNetSync.JsonOptions);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00015B30 File Offset: 0x00013D30
		private static void ApplyFull(ulong netId, string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				return;
			}
			RpgRunEquipmentDto rpgRunEquipmentDto = JsonSerializer.Deserialize<RpgRunEquipmentDto>(json, RpgLoadoutNetSync.JsonOptions);
			if (rpgRunEquipmentDto == null)
			{
				return;
			}
			RpgRunLoadout.LoadFromDto(netId, rpgRunEquipmentDto);
			RpgRunLoot.LoadFromList(netId, rpgRunEquipmentDto.PendingLoot);
			RpgScrollBag.Load(netId, rpgRunEquipmentDto.Scrolls);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00015B78 File Offset: 0x00013D78
		private static void ApplyEquipped(ulong netId, string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				return;
			}
			RpgRunEquipmentDto rpgRunEquipmentDto = JsonSerializer.Deserialize<RpgRunEquipmentDto>(json, RpgLoadoutNetSync.JsonOptions);
			if (rpgRunEquipmentDto == null)
			{
				return;
			}
			RpgRunLoadout.LoadEquippedFromDto(netId, rpgRunEquipmentDto);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00015BA8 File Offset: 0x00013DA8
		private static PlayerChoiceResult Encode(int magic, string json)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(json);
			List<int> list = new List<int>
			{
				magic,
				bytes.Length
			};
			for (int i = 0; i < bytes.Length; i += 4)
			{
				int num = (int)bytes[i];
				if (i + 1 < bytes.Length)
				{
					num |= (int)bytes[i + 1] << 8;
				}
				if (i + 2 < bytes.Length)
				{
					num |= (int)bytes[i + 2] << 16;
				}
				if (i + 3 < bytes.Length)
				{
					num |= (int)bytes[i + 3] << 24;
				}
				list.Add(num);
			}
			return PlayerChoiceResult.FromIndexes(list);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00015C30 File Offset: 0x00013E30
		private static bool TryDecode(PlayerChoiceResult remote, out string json)
		{
			json = "";
			List<int> list = remote.AsIndexes();
			if (list == null || list.Count < 2)
			{
				return false;
			}
			if (list[0] != 1380732737 && list[0] != 1380730193)
			{
				return false;
			}
			int num = list[1];
			if (num < 0 || num > 8388608)
			{
				return false;
			}
			int num2 = (num + 3) / 4;
			if (list.Count >= 2 + num2)
			{
				byte[] array = new byte[num];
				for (int i = 0; i < num; i++)
				{
					int num3 = list[2 + i / 4];
					array[i] = (byte)(num3 >> i % 4 * 8 & 255);
				}
				json = Encoding.UTF8.GetString(array);
				return true;
			}
			if (list.Count < 2 + num)
			{
				return false;
			}
			byte[] array2 = new byte[num];
			for (int j = 0; j < num; j++)
			{
				array2[j] = (byte)list[2 + j];
			}
			json = Encoding.UTF8.GetString(array2);
			return true;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00015D2D File Offset: 0x00013F2D
		// Note: this type is marked as 'beforefieldinit'.
		static RpgLoadoutNetSync()
		{
		}

		// Token: 0x040000F8 RID: 248
		private const int MagicFull = 1380732737;

		// Token: 0x040000F9 RID: 249
		private const int MagicEquipped = 1380730193;

		// Token: 0x040000FA RID: 250
		private const int TimeoutMs = 12000;

		// Token: 0x040000FB RID: 251
		private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			Converters = 
			{
				new JsonStringEnumConverter()
			}
		};

		// Token: 0x040000FC RID: 252
		[Nullable(2)]
		private static object _syncedCombat;

		// Token: 0x040000FD RID: 253
		private static int _applyingRemote;

		// Token: 0x02000109 RID: 265
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000786 RID: 1926 RVA: 0x00026E73 File Offset: 0x00025073
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000787 RID: 1927 RVA: 0x00026E7F File Offset: 0x0002507F
			public <>c()
			{
			}

			// Token: 0x06000788 RID: 1928 RVA: 0x00026E87 File Offset: 0x00025087
			[NullableContext(0)]
			internal ulong <SyncEquippedBeforeMove>b__8_0(Player p)
			{
				return p.NetId;
			}

			// Token: 0x06000789 RID: 1929 RVA: 0x00026E8F File Offset: 0x0002508F
			[NullableContext(0)]
			internal ulong <SyncAllBeforeCombat>b__9_0(Player p)
			{
				return p.NetId;
			}

			// Token: 0x0600078A RID: 1930 RVA: 0x00026E97 File Offset: 0x00025097
			[NullableContext(0)]
			internal RpgItemData <CaptureFull>b__15_0(RpgItemData x)
			{
				return x.Clone();
			}

			// Token: 0x0600078B RID: 1931 RVA: 0x00026E9F File Offset: 0x0002509F
			internal string <CaptureEquipped>b__16_0(RpgEquipSlot s)
			{
				return s.ToString();
			}

			// Token: 0x0600078C RID: 1932 RVA: 0x00026EAE File Offset: 0x000250AE
			[NullableContext(0)]
			internal RpgItemData <CaptureEquipped>b__16_1(RpgEquipSlot s)
			{
				RpgItemData equipped = RpgLoadout.GetEquipped(s);
				if (equipped == null)
				{
					return null;
				}
				return equipped.Clone();
			}

			// Token: 0x040003C9 RID: 969
			[Nullable(0)]
			public static readonly RpgLoadoutNetSync.<>c <>9 = new RpgLoadoutNetSync.<>c();

			// Token: 0x040003CA RID: 970
			[Nullable(0)]
			public static Func<Player, ulong> <>9__8_0;

			// Token: 0x040003CB RID: 971
			[Nullable(0)]
			public static Func<Player, ulong> <>9__9_0;

			// Token: 0x040003CC RID: 972
			[Nullable(0)]
			public static Func<RpgItemData, RpgItemData> <>9__15_0;

			// Token: 0x040003CD RID: 973
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public static Func<RpgEquipSlot, string> <>9__16_0;

			// Token: 0x040003CE RID: 974
			[Nullable(0)]
			public static Func<RpgEquipSlot, RpgItemData> <>9__16_1;
		}

		// Token: 0x0200010A RID: 266
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SyncAllBeforeCombat>d__9 : IAsyncStateMachine
		{
			// Token: 0x0600078D RID: 1933 RVA: 0x00026EC4 File Offset: 0x000250C4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					if (num != 0)
					{
						if (this.run.Players.Count <= 1)
						{
							goto IL_130;
						}
						if (this.combatKey != null)
						{
							if (RpgLoadoutNetSync._syncedCombat == this.combatKey)
							{
								goto IL_130;
							}
							RpgLoadoutNetSync._syncedCombat = this.combatKey;
						}
						this.<>7__wrap1 = this.run.Players.OrderBy(new Func<Player, ulong>(RpgLoadoutNetSync.<>c.<>9.<SyncAllBeforeCombat>b__9_0)).GetEnumerator();
					}
					try
					{
						if (num != 0)
						{
							goto IL_E9;
						}
						TaskAwaiter awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter);
						num = (this.<>1__state = -1);
						IL_E2:
						awaiter.GetResult();
						IL_E9:
						if (this.<>7__wrap1.MoveNext())
						{
							awaiter = RpgLoadoutNetSync.SyncPlayerAsync(this.<>7__wrap1.Current).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgLoadoutNetSync.<SyncAllBeforeCombat>d__9>(ref awaiter, ref this);
								return;
							}
							goto IL_E2;
						}
					}
					finally
					{
						if (num < 0 && this.<>7__wrap1 != null)
						{
							this.<>7__wrap1.Dispose();
						}
					}
					this.<>7__wrap1 = null;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_130:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600078E RID: 1934 RVA: 0x00027048 File Offset: 0x00025248
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040003CF RID: 975
			public int <>1__state;

			// Token: 0x040003D0 RID: 976
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040003D1 RID: 977
			[Nullable(0)]
			public IRunState run;

			// Token: 0x040003D2 RID: 978
			[Nullable(0)]
			public object combatKey;

			// Token: 0x040003D3 RID: 979
			[Nullable(0)]
			private IEnumerator<Player> <>7__wrap1;

			// Token: 0x040003D4 RID: 980
			private TaskAwaiter <>u__1;
		}

		// Token: 0x0200010B RID: 267
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SyncEquippedBeforeMove>d__8 : IAsyncStateMachine
		{
			// Token: 0x0600078F RID: 1935 RVA: 0x00027058 File Offset: 0x00025258
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					if (num != 0)
					{
						if (this.run.Players.Count <= 1)
						{
							goto IL_17E;
						}
						RunManager instance = RunManager.Instance;
						if (((instance != null) ? instance.NetService : null) == null || instance.NetService.Type == 1)
						{
							goto IL_17E;
						}
						this.<sync>5__2 = instance.PlayerChoiceSynchronizer;
						if (this.<sync>5__2 == null)
						{
							goto IL_17E;
						}
						SpireTheRpgMainFile.Logger.Info("RpgLoadoutNetSync equipped-before-move start", 1);
						this.<>7__wrap2 = this.run.Players.OrderBy(new Func<Player, ulong>(RpgLoadoutNetSync.<>c.<>9.<SyncEquippedBeforeMove>b__8_0)).GetEnumerator();
					}
					try
					{
						if (num != 0)
						{
							goto IL_120;
						}
						TaskAwaiter awaiter = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter);
						num = (this.<>1__state = -1);
						IL_119:
						awaiter.GetResult();
						IL_120:
						if (this.<>7__wrap2.MoveNext())
						{
							awaiter = RpgLoadoutNetSync.SyncEquippedPlayerAsync(this.<>7__wrap2.Current, this.<sync>5__2).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, RpgLoadoutNetSync.<SyncEquippedBeforeMove>d__8>(ref awaiter, ref this);
								return;
							}
							goto IL_119;
						}
					}
					finally
					{
						if (num < 0 && this.<>7__wrap2 != null)
						{
							this.<>7__wrap2.Dispose();
						}
					}
					this.<>7__wrap2 = null;
					SpireTheRpgMainFile.Logger.Info("RpgLoadoutNetSync equipped-before-move done", 1);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<sync>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_17E:
				this.<>1__state = -2;
				this.<sync>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000790 RID: 1936 RVA: 0x00027234 File Offset: 0x00025434
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040003D5 RID: 981
			public int <>1__state;

			// Token: 0x040003D6 RID: 982
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040003D7 RID: 983
			[Nullable(0)]
			public IRunState run;

			// Token: 0x040003D8 RID: 984
			[Nullable(0)]
			private PlayerChoiceSynchronizer <sync>5__2;

			// Token: 0x040003D9 RID: 985
			[Nullable(0)]
			private IEnumerator<Player> <>7__wrap2;

			// Token: 0x040003DA RID: 986
			private TaskAwaiter <>u__1;
		}

		// Token: 0x0200010C RID: 268
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SyncEquippedPlayerAsync>d__13 : IAsyncStateMachine
		{
			// Token: 0x06000791 RID: 1937 RVA: 0x00027244 File Offset: 0x00025444
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					try
					{
						TaskAwaiter<string> awaiter;
						if (num != 0)
						{
							uint num2 = this.sync.ReserveChoiceId(this.owner);
							if (RpgNet.IsLocal(this.owner))
							{
								string text = RpgLoadoutNetSync.CaptureEquipped(this.owner.NetId);
								this.sync.SyncLocalChoice(this.owner, num2, RpgLoadoutNetSync.Encode(1380730193, text));
								Logger logger = SpireTheRpgMainFile.Logger;
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 2);
								defaultInterpolatedStringHandler.AppendLiteral("RpgLoadoutNetSync equipped push net=");
								defaultInterpolatedStringHandler.AppendFormatted<ulong>(this.owner.NetId);
								defaultInterpolatedStringHandler.AppendLiteral(" bytes=");
								defaultInterpolatedStringHandler.AppendFormatted<int>(text.Length);
								logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
								goto IL_1CC;
							}
							awaiter = RpgLoadoutNetSync.WaitDecode(this.sync, this.owner, num2).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, RpgLoadoutNetSync.<SyncEquippedPlayerAsync>d__13>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter<string>);
							this.<>1__state = -1;
						}
						string result = awaiter.GetResult();
						if (result != null)
						{
							RpgLoadoutNetSync.ApplyEquipped(this.owner.NetId, result);
							Logger logger2 = SpireTheRpgMainFile.Logger;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(46, 2);
							defaultInterpolatedStringHandler.AppendLiteral("RpgLoadoutNetSync equipped applied net=");
							defaultInterpolatedStringHandler.AppendFormatted<ulong>(this.owner.NetId);
							defaultInterpolatedStringHandler.AppendLiteral(" bytes=");
							defaultInterpolatedStringHandler.AppendFormatted<int>(result.Length);
							logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
						}
					}
					catch (Exception ex)
					{
						SpireTheRpgMainFile.Logger.Info("RpgLoadoutNetSync equipped failed: " + ex.Message, 1);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1CC:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000792 RID: 1938 RVA: 0x00027464 File Offset: 0x00025664
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040003DB RID: 987
			public int <>1__state;

			// Token: 0x040003DC RID: 988
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040003DD RID: 989
			[Nullable(0)]
			public PlayerChoiceSynchronizer sync;

			// Token: 0x040003DE RID: 990
			[Nullable(0)]
			public Player owner;

			// Token: 0x040003DF RID: 991
			[Nullable(new byte[]
			{
				0,
				2
			})]
			private TaskAwaiter<string> <>u__1;
		}

		// Token: 0x0200010D RID: 269
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SyncPlayerAsync>d__12 : IAsyncStateMachine
		{
			// Token: 0x06000793 RID: 1939 RVA: 0x00027474 File Offset: 0x00025674
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					try
					{
						TaskAwaiter<string> awaiter;
						if (num != 0)
						{
							if (this.owner.RunState.Players.Count <= 1)
							{
								goto IL_217;
							}
							RunManager instance = RunManager.Instance;
							if (((instance != null) ? instance.NetService : null) == null || instance.NetService.Type == 1)
							{
								goto IL_217;
							}
							PlayerChoiceSynchronizer playerChoiceSynchronizer = instance.PlayerChoiceSynchronizer;
							if (playerChoiceSynchronizer == null)
							{
								goto IL_217;
							}
							uint num2 = playerChoiceSynchronizer.ReserveChoiceId(this.owner);
							if (RpgNet.IsLocal(this.owner))
							{
								string text = RpgLoadoutNetSync.CaptureFull(this.owner.NetId);
								playerChoiceSynchronizer.SyncLocalChoice(this.owner, num2, RpgLoadoutNetSync.Encode(1380732737, text));
								Logger logger = SpireTheRpgMainFile.Logger;
								DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(39, 2);
								defaultInterpolatedStringHandler.AppendLiteral("RpgLoadoutNetSync full push net=");
								defaultInterpolatedStringHandler.AppendFormatted<ulong>(this.owner.NetId);
								defaultInterpolatedStringHandler.AppendLiteral(" bytes=");
								defaultInterpolatedStringHandler.AppendFormatted<int>(text.Length);
								logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
								goto IL_217;
							}
							awaiter = RpgLoadoutNetSync.WaitDecode(playerChoiceSynchronizer, this.owner, num2).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, RpgLoadoutNetSync.<SyncPlayerAsync>d__12>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter<string>);
							this.<>1__state = -1;
						}
						string result = awaiter.GetResult();
						if (result != null)
						{
							RpgLoadoutNetSync.ApplyFull(this.owner.NetId, result);
							Logger logger2 = SpireTheRpgMainFile.Logger;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 2);
							defaultInterpolatedStringHandler.AppendLiteral("RpgLoadoutNetSync full applied net=");
							defaultInterpolatedStringHandler.AppendFormatted<ulong>(this.owner.NetId);
							defaultInterpolatedStringHandler.AppendLiteral(" bytes=");
							defaultInterpolatedStringHandler.AppendFormatted<int>(result.Length);
							logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
						}
					}
					catch (Exception ex)
					{
						SpireTheRpgMainFile.Logger.Info("RpgLoadoutNetSync full failed: " + ex.Message, 1);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_217:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000794 RID: 1940 RVA: 0x000276E0 File Offset: 0x000258E0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040003E0 RID: 992
			public int <>1__state;

			// Token: 0x040003E1 RID: 993
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040003E2 RID: 994
			[Nullable(0)]
			public Player owner;

			// Token: 0x040003E3 RID: 995
			[Nullable(new byte[]
			{
				0,
				2
			})]
			private TaskAwaiter<string> <>u__1;
		}

		// Token: 0x0200010E RID: 270
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WaitDecode>d__14 : IAsyncStateMachine
		{
			// Token: 0x06000795 RID: 1941 RVA: 0x000276F0 File Offset: 0x000258F0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				string result;
				try
				{
					TaskAwaiter<PlayerChoiceResult> awaiter;
					TaskAwaiter<Task> awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(TaskAwaiter<PlayerChoiceResult>);
							this.<>1__state = -1;
							goto IL_138;
						}
						this.<wait>5__2 = this.sync.WaitForRemoteChoice(this.owner, this.choiceId);
						awaiter2 = Task.WhenAny(this.<wait>5__2, Task.Delay(12000)).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<Task>, RpgLoadoutNetSync.<WaitDecode>d__14>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(TaskAwaiter<Task>);
						this.<>1__state = -1;
					}
					if (awaiter2.GetResult() != this.<wait>5__2)
					{
						Logger logger = SpireTheRpgMainFile.Logger;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(30, 1);
						defaultInterpolatedStringHandler.AppendLiteral("RpgLoadoutNetSync timeout net=");
						defaultInterpolatedStringHandler.AppendFormatted<ulong>(this.owner.NetId);
						logger.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
						result = null;
						goto IL_1AA;
					}
					awaiter = this.<wait>5__2.GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<PlayerChoiceResult>, RpgLoadoutNetSync.<WaitDecode>d__14>(ref awaiter, ref this);
						return;
					}
					IL_138:
					string text;
					if (!RpgLoadoutNetSync.TryDecode(awaiter.GetResult(), out text))
					{
						Logger logger2 = SpireTheRpgMainFile.Logger;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 1);
						defaultInterpolatedStringHandler.AppendLiteral("RpgLoadoutNetSync decode fail net=");
						defaultInterpolatedStringHandler.AppendFormatted<ulong>(this.owner.NetId);
						logger2.Info(defaultInterpolatedStringHandler.ToStringAndClear(), 1);
						result = null;
					}
					else
					{
						result = text;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<wait>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1AA:
				this.<>1__state = -2;
				this.<wait>5__2 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000796 RID: 1942 RVA: 0x000278E0 File Offset: 0x00025AE0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040003E4 RID: 996
			public int <>1__state;

			// Token: 0x040003E5 RID: 997
			[Nullable(0)]
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x040003E6 RID: 998
			[Nullable(0)]
			public PlayerChoiceSynchronizer sync;

			// Token: 0x040003E7 RID: 999
			[Nullable(0)]
			public Player owner;

			// Token: 0x040003E8 RID: 1000
			public uint choiceId;

			// Token: 0x040003E9 RID: 1001
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private Task<PlayerChoiceResult> <wait>5__2;

			// Token: 0x040003EA RID: 1002
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private TaskAwaiter<Task> <>u__1;

			// Token: 0x040003EB RID: 1003
			[Nullable(new byte[]
			{
				0,
				1
			})]
			private TaskAwaiter<PlayerChoiceResult> <>u__2;
		}
	}
}
