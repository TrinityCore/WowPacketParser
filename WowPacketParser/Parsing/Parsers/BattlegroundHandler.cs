using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.SMSG_BATTLEGROUND_EXIT_QUEUE)]
        public static void HandleBattlegroundExitQueue(Packet packet)
        {
            packet.ReadUInt32("Queue slot");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_IN_PROGRESS)]
        public static void HandleBattlegroundInProgress(Packet packet)
        {
            packet.ReadBit("IsRated");
            packet.ReadUInt32("Time since started");
            packet.ReadUInt32("Queue slot");
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map Id");
            packet.ReadGuid("BG Guid");
            packet.ReadUInt32("Time until closed");
            packet.ReadByte("Teamsize");
            packet.ReadByte("Max Level");
            packet.ReadUInt32("Client Instance ID");
            packet.ReadByte("Min Level");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_WAIT_JOIN)]
        public static void HandleBattlegroundWaitJoin(Packet packet)
        {
            packet.ReadBit("IsArena");
            packet.ReadByte("Min Level");
            packet.ReadUInt32("Client Instance ID");
            packet.ReadGuid("BG Guid");
            packet.ReadInt32("Queue slot");
            packet.ReadByte("Teamsize");
            packet.ReadUInt32("Expire Time");
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map Id");
            packet.ReadByte("Max Level");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_WAIT_LEAVE)]
        public static void HandleBattlegroundWaitLeave(Packet packet)
        {
            packet.ReadByte("Unk");
            packet.ReadUInt32("Time left");
            packet.ReadByte("Min Level");
            packet.ReadByte("Unk2");
            packet.ReadByte("Unk3");
            packet.ReadInt32("Queue slot");
            packet.ReadByte("Max Level");
            packet.ReadUInt32("Time2");
            packet.ReadByte("Teamsize");
            packet.ReadUInt32("Client Instance ID");
            packet.ReadByte("Unk4");
            packet.ReadGuid("BG Guid");
            packet.ReadByte("Unk5");
        }

        [Parser(Opcode.SMSG_AREA_SPIRIT_HEALER_TIME)]
        public static void HandleAreaSpiritHealerTime(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Timer");
        }

        [Parser(Opcode.MSG_BATTLEGROUND_PLAYER_POSITIONS)]
        public static void HandleBattlegrounPlayerPositions(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                return;

            var count1 = packet.ReadInt32("Count1");
            for (var i = 0; i < count1; i++)
            {
                packet.ReadGuid("Player GUID", i);
                packet.ReadVector2("Position", i);
            }

            var count2 = packet.ReadInt32("Count2");
            for (var i = 0; i < count2; i++)
            {
                packet.ReadGuid("Player GUID", i);
                packet.ReadVector2("Position", i);
            }
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_JOIN, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleBattlefieldJoin422(Packet packet)
        {
            var bytes = new byte[8];

            bytes[0] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes[4] = (byte)(packet.ReadBit() ? 1 : 0);
            //uint somebyte = guidBytes[4]; // unsure which one it goes with, but it is used around here.
            bytes[1] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes[6] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes[7] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes[5] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes[2] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes[3] = (byte)(packet.ReadBit() ? 1 : 0);

            packet.ReadUInt32("Unknown uint32");

            if (bytes[5] != 0)
                bytes[5] ^= packet.ReadByte("unk5");

            if (bytes[0] != 0)
                bytes[0] ^= packet.ReadByte("unk0");

            if (bytes[2] != 0)
                bytes[2] ^= packet.ReadByte("unk2");

            if (bytes[1] != 0)
                bytes[1] ^= packet.ReadByte("unk1");

            if (bytes[4] != 0)
                bytes[4] ^= packet.ReadByte("BattlefieldId");

            if (bytes[6] != 0)
                bytes[6] ^= packet.ReadByte("unk6");

            if (bytes[3] != 0)
                bytes[3] ^= packet.ReadByte("unk3");

            if (bytes[7] != 0)
                bytes[7] ^= packet.ReadByte("unk7");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_JOIN, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleBattlefieldJoin(Packet packet)
        {
            packet.ReadBit("asGroup");
            packet.ReadUInt32("Unk1");
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_LIST)]
        public static void HandleBattlefieldListClient(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Battleground, "BGType");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                return;

            packet.ReadBoolean("From UI");
            packet.ReadByte("Unk Byte (BattlefieldList)");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleBattlefieldListServer430(Packet packet)
        {
            var guidBytes = new byte[8];

            guidBytes[2] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[7] = (byte)(packet.ReadBit() ? 1 : 0);
            packet.ReadBit("UnkBit1");
            var count = packet.ReadBits("BG Instance count", 21);
            guidBytes[0] = (byte)(packet.ReadBit() ? 1 : 0);
            packet.ReadBit("UnkBit2");
            guidBytes[3] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[1] = (byte)(packet.ReadBit() ? 1 : 0);
            packet.ReadBit("UnkBit3");
            guidBytes[5] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[4] = (byte)(packet.ReadBit() ? 1 : 0);
            packet.ReadBit("Random Has Win");
            guidBytes[6] = (byte)(packet.ReadBit() ? 1 : 0);

            if (guidBytes[4] != 0) guidBytes[4] ^= packet.ReadByte();

            packet.ReadInt32("Loser Honor Reward");
            packet.ReadInt32("Winner Honor Reward");
            for (var i = 0; i < count; i++)
                packet.ReadUInt32("Instance ID", i);

            if (guidBytes[7] != 0) guidBytes[7] ^= packet.ReadByte();

            packet.ReadEntryWithName<Int32>(StoreNameType.Battleground, "BG type");

            if (guidBytes[1] != 0) guidBytes[1] ^= packet.ReadByte();

            packet.ReadInt32("Random Loser Honor Reward");
            packet.ReadInt32("Random Winner Conquest Reward");

            if (guidBytes[2] != 0) guidBytes[2] ^= packet.ReadByte();

            packet.ReadByte("Max level");

            if (guidBytes[0] != 0) guidBytes[0] ^= packet.ReadByte();

            packet.ReadInt32("Winner Conquest Reward");
            packet.ReadByte("Min level");

            if (guidBytes[5] != 0) guidBytes[5] ^= packet.ReadByte();

            packet.ReadInt32("Random Winner Honor Reward");

            if (guidBytes[3] != 0) guidBytes[3] ^= packet.ReadByte();
            if (guidBytes[6] != 0) guidBytes[6] ^= packet.ReadByte();

            packet.WriteLine("Guid: {0}", new Guid(BitConverter.ToUInt64(guidBytes, 0)));
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleBattlefieldListServer422(Packet packet)
        {
            var guidBytes = new byte[8];

            guidBytes[5] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[0] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[4] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[1] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[3] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[6] = (byte)(packet.ReadBit() ? 1 : 0);
            packet.ReadBit("UnkBit1");
            packet.ReadBit("UnkBit2");
            guidBytes[2] = (byte)(packet.ReadBit() ? 1 : 0);
            packet.ReadBit("UnkBit3");
            packet.ReadBit("UnkBit4");
            guidBytes[7] = (byte)(packet.ReadBit() ? 1 : 0);

            packet.ReadInt32("Winner Honor Reward");

            if (guidBytes[3] != 0) guidBytes[3] ^= packet.ReadByte();
            if (guidBytes[5] != 0) guidBytes[5] ^= packet.ReadByte();

            packet.ReadInt32("Random Winner Honor Reward");

            if (guidBytes[0] != 0) guidBytes[0] ^= packet.ReadByte();

            packet.ReadByte("Max level");

            if (guidBytes[7] != 0) guidBytes[7] ^= packet.ReadByte();
            if (guidBytes[1] != 0) guidBytes[1] ^= packet.ReadByte();
            if (guidBytes[2] != 0) guidBytes[2] ^= packet.ReadByte();
            if (guidBytes[4] != 0) guidBytes[4] ^= packet.ReadByte();

            packet.ReadEntryWithName<Int32>(StoreNameType.Battleground, "BGType");
            packet.ReadInt32("Random Winner Conquest Reward");
            packet.ReadInt32("Winner Conquest Reward");

            if (guidBytes[6] != 0) guidBytes[6] ^= packet.ReadByte();

            packet.ReadInt32("Random Loser Honor Reward");
            packet.ReadByte("Min level");
            packet.ReadInt32("Loser Honor Reward");

            var count = packet.ReadUInt32("BG Instance count");
            for (var i = 0; i < count; i++)
                packet.ReadUInt32("Instance ID", i);

            packet.WriteLine("Guid: {0}", new Guid(BitConverter.ToUInt64(guidBytes, 0)));
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleBattlefieldListServer406(Packet packet)
        {
            packet.ReadEnum<UnknownFlags>("Flags", TypeCode.Byte); // 0x10 Already won, 0x20 all cases, 0x80 From UI
            packet.ReadByte("Min level");
            packet.ReadInt32("Winner Honor Reward");
            packet.ReadGuid("GUID");
            packet.ReadInt32("Random Winner Honor Reward");
            packet.ReadByte("Max level");
            packet.ReadInt32("Random Loser Honor Reward");
            packet.ReadInt32("Random Winner Conquest Reward");
            packet.ReadInt32("Winner Conquest Reward");
            packet.ReadEntryWithName<Int32>(StoreNameType.Battleground, "BGType");

            var count = packet.ReadUInt32("BG Instance count");
            for (var i = 0; i < count; i++)
                packet.ReadUInt32("Instance ID", i);

            packet.ReadInt32("Loser Honor Reward");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlefieldListServer(Packet packet)
        {
            packet.ReadGuid("GUID");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_0_14333))
                packet.ReadBoolean("From UI");

            packet.ReadEntryWithName<Int32>(StoreNameType.Battleground, "BGType");
            packet.ReadByte("Min Level");
            packet.ReadByte("Max Level");
            packet.ReadBoolean("Has Win");
            packet.ReadInt32("Winner Honor Reward");
            packet.ReadInt32("Winner Arena Reward");
            packet.ReadInt32("Loser Honor Reward");

            var random = packet.ReadBoolean("Is random");
            if (random)
            {
                packet.ReadByte("Random Has Win");
                packet.ReadInt32("Random Winner Honor Reward");
                packet.ReadInt32("Random Winner Arena Reward");
                packet.ReadInt32("Random Loser Honor Reward");
            }

            var count = packet.ReadUInt32("BG Instance count");
            for (var i = 0; i < count; i++)
                packet.ReadUInt32("[" + i + "] Instance ID");
        }

        [Parser(Opcode.CMSG_BATTLEGROUND_PORT_AND_LEAVE, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlefieldPort406(Packet packet)
        {
            packet.ReadBit("Join");
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_BATTLEGROUND_PORT_AND_LEAVE, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlefieldPort(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadBoolean("Join BG");
        }

        [Parser(Opcode.CMSG_LEAVE_BATTLEFIELD)]
        public static void HandleBattlefieldLeave(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_LEAVE)] // Differences from above packet?
        [Parser(Opcode.CMSG_BATTLEFIELD_STATUS)]
        public static void HandleBGZeroLengthPackets(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS)]
        public static void HandleBattlefieldStatusServer(Packet packet)
        {
            var slot = packet.ReadUInt32("Queue Slot");
            var guid = packet.ReadGuid("GUID");

            if (slot == 1 && guid.GetLow() == 0)
                return;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
            {
                packet.ReadByte("Min Level");
                packet.ReadByte("Max Level");
            }

            if (!packet.CanRead())
                return;

            packet.ReadUInt32("Client Instance ID");
            packet.ReadBoolean("Rated");
            var status = packet.ReadEnum<BattlegroundStatus>("Status", TypeCode.UInt32);
            switch (status)
            {
                case BattlegroundStatus.WaitQueue:
                    packet.ReadUInt32("Average Wait Time");
                    packet.ReadUInt32("Time in queue");
                    break;
                case BattlegroundStatus.WaitJoin:
                    packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_5_12213))
                        packet.ReadGuid("GUID");

                    packet.ReadUInt32("Time left");
                    break;
                case BattlegroundStatus.InProgress:
                    packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_5_12213))
                        packet.ReadGuid("GUID");

                    packet.ReadUInt32("Auto Leave Time");
                    packet.ReadUInt32("Time in BG");
                    packet.ReadByte("Is BG?");
                    break;
            }
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_HELLO)]
        [Parser(Opcode.CMSG_AREA_SPIRIT_HEALER_QUERY)]
        [Parser(Opcode.CMSG_AREA_SPIRIT_HEALER_QUEUE)]
        [Parser(Opcode.CMSG_REPORT_PVP_AFK)]
        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_LEFT)]
        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED)]
        public static void HandleBattlemasterHello(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN)]
        public static void HandleBattlemasterJoin(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Battleground, "BGType");
            packet.ReadUInt32("Instance Id");
            packet.ReadBoolean("As group");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_ARENA)]
        public static void HandleBattlemasterJoinArena(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Slot");
            packet.ReadBoolean("As group");
            packet.ReadBoolean("Rated");
        }

        [Parser(Opcode.CMSG_REPORT_PVP_AFK)]
        public static void HandleReportPvPAFK(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        // Reading 11 Bytes in random order. Only 1st Byte seems to have data (Only seen '5')
        [Parser(Opcode.SMSG_REPORT_PVP_AFK_RESULT)]
        public static void HandleReportPvPAFKResult(Packet packet)
        {
            packet.ReadByte("Unk Byte (afkResult)");
            packet.ReadUInt64("Unk Uint64 (afkResult)");
            packet.ReadUInt16("Unk Uint16 (afkResult)");
        }

        [Parser(Opcode.SMSG_GROUP_JOINED_BATTLEGROUND, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleRGroupJoinedBattleground(Packet packet)
        {
            var val = packet.ReadInt32();
            if (val < 1)
            {
                var result = (BattlegroundError)val;
                packet.WriteLine("Result: " + result);
                if (result == BattlegroundError.JoinFailedAsGroup ||
                    result == BattlegroundError.CouldntJoinQueueInTime)
                    packet.ReadGuid("GUID");
            }
            else
                packet.WriteLine("Result: Joined (BGType: " + StoreGetters.GetName(StoreNameType.Battleground, val) + ")");
        }

        [Parser(Opcode.SMSG_JOINED_BATTLEGROUND_QUEUE, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleJoinedBattlegroundQueue422(Packet packet)
        {
            var guidBytes = new byte[8];
            var field14 = new byte[4];
            var field10 = new byte[4];
            var field38 = new byte[4];
            var field3C = new byte[4];

            guidBytes[1] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[2] = (byte)(packet.ReadBit() ? 1 : 0);
            field38[2] = (byte)(packet.ReadBit() ? 1 : 0);
            field10[2] = (byte)(packet.ReadBit() ? 1 : 0);
            field14[0] = (byte)(packet.ReadBit() ? 1 : 0);
            field14[1] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[6] = (byte)(packet.ReadBit() ? 1 : 0);
            field10[3] = (byte)(packet.ReadBit() ? 1 : 0);
            field14[2] = (byte)(packet.ReadBit() ? 1 : 0);
            field3C[0] = (byte)(packet.ReadBit() ? 1 : 0);
            field3C[2] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[4] = (byte)(packet.ReadBit() ? 1 : 0);
            field3C[3] = (byte)(packet.ReadBit() ? 1 : 0);
            field38[3] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[0] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[3] = (byte)(packet.ReadBit() ? 1 : 0);
            field3C[1] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[7] = (byte)(packet.ReadBit() ? 1 : 0);
            field38[0] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[5] = (byte)(packet.ReadBit() ? 1 : 0);
            field10[1] = (byte)(packet.ReadBit() ? 1 : 0);
            field14[3] = (byte)(packet.ReadBit() ? 1 : 0);
            field38[1] = (byte)(packet.ReadBit() ? 1 : 0);
            field10[0] = (byte)(packet.ReadBit() ? 1 : 0);

            if (guidBytes[4] != 0) guidBytes[4] ^= packet.ReadByte();

            var bgError = packet.ReadEnum<BattlegroundError430>("Battleground Error", TypeCode.Int32);

            if (guidBytes[1] != 0) guidBytes[1] ^= packet.ReadByte();

            if (field10[1] != 0) field10[1] ^= packet.ReadByte();

            if (guidBytes[6] != 0) guidBytes[6] ^= packet.ReadByte();

            if (field3C[2] != 0) field3C[2] ^= packet.ReadByte();
            if (field14[1] != 0) field14[1] ^= packet.ReadByte();
            if (field14[2] != 0) field14[2] ^= packet.ReadByte();

            packet.ReadUInt32("field18");

            if (field38[0] != 0) field38[0] ^= packet.ReadByte();
            if (field3C[1] != 0) field3C[1] ^= packet.ReadByte();
            if (field10[0] != 0) field10[0] ^= packet.ReadByte();

            packet.ReadUInt32("BattlegroundId");

            if (field38[3] != 0) field38[3] ^= packet.ReadByte();
            if (field3C[3] != 0) field3C[3] ^= packet.ReadByte();

            if (guidBytes[5] != 0) guidBytes[5] ^= packet.ReadByte();

            if (field10[2] != 0) field10[2] ^= packet.ReadByte();

            if (guidBytes[0] != 0) guidBytes[0] ^= packet.ReadByte();
            if (guidBytes[7] != 0) guidBytes[7] ^= packet.ReadByte();

            if (field14[3] != 0) field14[3] ^= packet.ReadByte();
            if (field10[3] != 0) field10[3] ^= packet.ReadByte();
            if (field38[2] != 0) field38[2] ^= packet.ReadByte();

            if (guidBytes[2] != 0) guidBytes[2] ^= packet.ReadByte();

            if (field38[1] != 0) field38[1] ^= packet.ReadByte();

            if (guidBytes[3] != 0) guidBytes[3] ^= packet.ReadByte();

            if (field14[0] != 0) field14[0] ^= packet.ReadByte();
            if (field3C[0] != 0) field3C[0] ^= packet.ReadByte();

            packet.ReadUInt32("field1C");

            // note: guid is used to identify the player who's unable to join queue when it happens.

            // on id 0xB, 0xC and 8
            if (bgError == BattlegroundError430.CouldntJoinQueueInTime
                || bgError == BattlegroundError430.NotAllowedInBattleground
                || bgError == BattlegroundError430.JoinFailedAsGroup)
            {
                packet.WriteLine("GUID: {0:X16}", BitConverter.ToUInt64(guidBytes, 0));
            }

            packet.WriteLine("BGError: {0}", bgError);
        }

        [Parser(Opcode.SMSG_JOINED_BATTLEGROUND_QUEUE, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleJoinedBattlegroundQueue(Packet packet)
        {
            packet.ReadByte("Flags");
            packet.ReadByte("Max Level");
            packet.ReadInt32("Avg Wait Time");
            packet.ReadInt32("Queue Slot");
            packet.ReadInt32("Instance ID");
            packet.ReadByte("Min Level");
            packet.ReadGuid("BG Guid");
            packet.ReadByte("Team Size");
            packet.ReadInt32("Time in queue");
        }

        [Parser(Opcode.TEST_422_265C, ClientVersionBuild.V4_2_2_14545)] // SMSG
        public static void HandleRGroupJoinedBattleground422(Packet packet)
        {
            var guidBytes = new byte[8];

            guidBytes[0] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[1] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[4] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[3] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[6] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[2] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[7] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[5] = (byte)(packet.ReadBit() ? 1 : 0);

            if (guidBytes[2] != 0) guidBytes[2] ^= packet.ReadByte();
            if (guidBytes[6] != 0) guidBytes[6] ^= packet.ReadByte();
            if (guidBytes[3] != 0) guidBytes[3] ^= packet.ReadByte();
            if (guidBytes[4] != 0) guidBytes[4] ^= packet.ReadByte();
            if (guidBytes[5] != 0) guidBytes[5] ^= packet.ReadByte();
            if (guidBytes[7] != 0) guidBytes[7] ^= packet.ReadByte();
            if (guidBytes[1] != 0) guidBytes[1] ^= packet.ReadByte();
            if (guidBytes[0] != 0) guidBytes[0] ^= packet.ReadByte();

            packet.WriteLine("Guid: {0}", new Guid(BitConverter.ToUInt64(guidBytes, 0)));
        }

        [Parser(Opcode.MSG_PVP_LOG_DATA)]
        public static void HandlePvPLogData(Packet packet)
        {
            var arena = packet.ReadBoolean("Arena");
            if (arena)
            {
                packet.ReadUInt32("[1] Points Lost");
                packet.ReadUInt32("[1] Points Gained");
                packet.ReadUInt32("[1] Matchmaker Rating");
                packet.ReadUInt32("[0] Points Lost");
                packet.ReadUInt32("[0] Points Gained");
                packet.ReadUInt32("[0] Matchmaker Rating");
                packet.ReadCString("[1] Name");
                packet.ReadCString("[0] Name");
            }

            var finished = packet.ReadBoolean("Finished");
            if (finished)
                packet.ReadByte("Winner");

            var count = packet.ReadUInt32("Score count");
            for (var i = 0; i < count; i++)
            {
                packet.ReadGuid("Player GUID", i);
                packet.ReadUInt32("Killing Blows", i);
                if (!arena)
                {
                    packet.ReadUInt32("Honorable Kills", i);
                    packet.ReadUInt32("Deaths", i);
                    packet.ReadUInt32("Bonus Honor", i);
                }
                else
                    packet.ReadByte("BG Team", i);

                packet.ReadUInt32("Damage done", i);
                packet.ReadUInt32("Healing done", i);

                var count2 = packet.ReadUInt32("Extra values counter", i);

                for (var j = 0; j < count2; j++)
                    packet.ReadUInt32("Value", i, j);
            }
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_STATE_CHANGE, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlefieldMgrStateChanged406(Packet packet)
        {
            packet.ReadEnum<BattlegroundStatus>("status", TypeCode.UInt32);
            packet.ReadGuid("BG Guid");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_STATE_CHANGE)]
        public static void HandleBattlefieldMgrStateChanged(Packet packet)
        {
            packet.ReadEnum<BattlegroundStatus>("Old status", TypeCode.UInt32);
            packet.ReadEnum<BattlegroundStatus>("New status", TypeCode.UInt32);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTRY_INVITE)]
        public static void HandleBattlefieldMgrInviteSend(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id");
            packet.ReadTime("Invite lasts until");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_INVITE)]
        public static void HandleBattlefieldMgrQueueInvite(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_QUEUE_INVITE_RESPONSE)]
        public static void HandleBattlefieldMgrQueueInviteResponse(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadBoolean("Accepted");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_REQUEST_RESPONSE)]
        public static void HandleBattlefieldMgrQueueRequestResponse(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone ID");
            packet.ReadByte("Accepted");
            packet.ReadByte("Logging In");
            packet.ReadByte("Warmup");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTERED, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlefieldMgrEntered406(Packet packet)
        {
            packet.ReadByte("Unk");
            packet.ReadGuid("BG Guid");

        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTERED)]
        public static void HandleBattlefieldMgrEntered(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadByte("Unk Byte 1");
            packet.ReadByte("Unk Byte 2");
            packet.ReadByte("Clear AFK");

        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_EJECTED)]
        public static void HandleBattlefieldMgrEjected(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadByte("Reason");
            packet.ReadByte("Battle Status");
            packet.ReadByte("Relocated");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_ENTRY_INVITE_RESPONSE)]
        public static void HandleBattlefieldMgrEntryInviteResponse(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadBoolean("Accepted");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_EXIT_REQUEST)]
        public static void HandleBattlefieldMgrExitRequest(Packet packet)
        {
            packet.ReadInt32("Battle Id");
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_ROSTER)]
        public static void HandleArenaTeamRoster(Packet packet)
        {
            packet.ReadUInt32("Team Id");
            var unk = packet.ReadByte("Unk Byte");
            var count = packet.ReadUInt32("Member count");
            packet.ReadUInt32("Type");

            for (var i = 0; i < count; i++)
            {
                packet.ReadGuid(" GUID", i);
                packet.ReadBoolean("Online", i);
                packet.ReadCString("Name", i);
                packet.ReadUInt32("Captain", i);
                packet.ReadByte("Level", i);
                packet.ReadByte("Class", i);
                packet.ReadUInt32("Week Games", i);
                packet.ReadUInt32("Week Win", i);
                packet.ReadUInt32("Seasonal Games", i);
                packet.ReadUInt32("Seasonal Wins", i);
                packet.ReadUInt32("Personal Rating", i);
                if (unk > 0)
                {
                    packet.ReadSingle("Unk float 1", i);
                    packet.ReadSingle("Unk float 2", i);
                }
            }
        }

        [Parser(Opcode.CMSG_ARENA_TEAM_ROSTER)]
        [Parser(Opcode.CMSG_ARENA_TEAM_QUERY)]
        public static void HandleArenaTeamQuery(Packet packet)
        {
            packet.ReadUInt32("Team ID");
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_COMMAND_RESULT)]
        public static void HandleArenaTeamCommandResult(Packet packet)
        {
            packet.ReadUInt32("Action"); // FIXME: Use enum
            packet.ReadCString("Team Name");
            packet.ReadCString("Player Name");
            packet.ReadUInt32("ErrorId"); // FIXME: Use enum
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_EVENT)]
        public static void HandleArenaTeamEvent(Packet packet)
        {
            packet.ReadByte("Event"); // FIXME: Use enum
            var count = packet.ReadByte("Count");
            for (var i = 0; i < count; ++i)
                packet.ReadCString("Param", i);

            if (packet.CanRead())
                packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_QUERY_RESPONSE)]
        public static void HandleArenaTeamQueryResponse(Packet packet)
        {
            packet.ReadUInt32("Team ID");
            packet.ReadCString("Team Name");
            packet.ReadUInt32("Type");
            packet.ReadUInt32("Background Color");
            packet.ReadUInt32("Emblem Style");
            packet.ReadUInt32("Emblem Color");
            packet.ReadUInt32("Border Style");
            packet.ReadUInt32("Border Color");
        }

        [Parser(Opcode.SMSG_ARENA_OPPONENT_UPDATE)]
        public static void HandleArenaOpponentUpdate(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.MSG_INSPECT_ARENA_TEAMS)]
        public static void HandleInspectArenaTeams(Packet packet)
        {
            packet.ReadGuid("GUID");
            if (packet.Direction == Direction.ClientToServer)
                return;

            packet.ReadByte("Slot");
            packet.ReadUInt32("Team Id");
            packet.ReadUInt32("Team Rating");
            packet.ReadUInt32("Team Season Games");
            packet.ReadUInt32("Team Season Wins");
            packet.ReadUInt32("Player Season Games");
            packet.ReadUInt32("Player Personal Rating");
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_STATS)]
        public static void HandleArenaTeamStats(Packet packet)
        {
            packet.ReadUInt32("Team Id");
            packet.ReadUInt32("Rating");
            packet.ReadUInt32("Week Games");
            packet.ReadUInt32("Week Win");
            packet.ReadUInt32("Seasonal Games");
            packet.ReadUInt32("Seasonal Wins");
            packet.ReadUInt32("Rank");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_QUEUE_REQUEST)]
        public static void HandleBattelfieldMgrQueueRequest(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        //[Parser(Opcode.SMSG_BATTLEFIELD_MGR_EJECT_PENDING)]
        //[Parser(Opcode.CMSG_BATTLEFIELD_MANAGER_ADVANCE_STATE)]
        //[Parser(Opcode.CMSG_BATTLEFIELD_MANAGER_SET_NEXT_TRANSITION_TIME)]
        //[Parser(Opcode.SMSG_JOINED_BATTLEGROUND_QUEUE)]
        //[Parser(Opcode.SMSG_BATTLEGROUND_INFO_THROTTLED)]
        //[Parser(Opcode.SMSG_BATTLEFIELD_PORT_DENIED)]
        //[Parser(Opcode.CMSG_START_BATTLEFIELD_CHEAT)]
        //[Parser(Opcode.CMSG_END_BATTLEFIELD_CHEAT)]
    }
}
