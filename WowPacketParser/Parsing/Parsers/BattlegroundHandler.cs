using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

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

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldListServer434(Packet packet)
        {
            packet.ReadInt32("Unk Int32"); // Winner Conquest Reward or Random Winner Conquest Reward
            packet.ReadInt32("Unk Int32"); // Winner Conquest Reward or Random Winner Conquest Reward
            packet.ReadInt32("Unk Int32"); // Loser Honor Reward or Random Loser Honor Reward
            packet.ReadEntryWithName<Int32>(StoreNameType.Battleground, "BG type");
            packet.ReadInt32("Unk Int32"); // Loser Honor Reward or Random Loser Honor Reward
            packet.ReadInt32("Unk Int32"); // Winner Honor Reward or Random Winner Honor Reward
            packet.ReadInt32("Unk Int32"); // Winner Honor Reward or Random Winner Honor Reward
            packet.ReadByte("Max level");
            packet.ReadByte("Min level");

            var guidBytes = new byte[8];

            guidBytes[0] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[1] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[7] = (byte)(packet.ReadBit() ? 1 : 0);
            packet.ReadBit("Unk Bit");
            packet.ReadBit("Unk Bit");
            var count = packet.ReadBits("BG Instance count", 24);
            guidBytes[6] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[4] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[2] = (byte)(packet.ReadBit() ? 1 : 0);
            guidBytes[3] = (byte)(packet.ReadBit() ? 1 : 0);
            packet.ReadBit("Unk Bit");
            guidBytes[5] = (byte)(packet.ReadBit() ? 1 : 0);
            packet.ReadBit("Unk Bit");

            if (guidBytes[6] != 0) guidBytes[6] ^= packet.ReadByte();
            if (guidBytes[1] != 0) guidBytes[1] ^= packet.ReadByte();
            if (guidBytes[7] != 0) guidBytes[7] ^= packet.ReadByte();
            if (guidBytes[5] != 0) guidBytes[5] ^= packet.ReadByte();

            for (var i = 0; i < count; i++)
                packet.ReadUInt32("Instance ID", i);

            if (guidBytes[0] != 0) guidBytes[0] ^= packet.ReadByte();
            if (guidBytes[2] != 0) guidBytes[2] ^= packet.ReadByte();
            if (guidBytes[4] != 0) guidBytes[4] ^= packet.ReadByte();
            if (guidBytes[3] != 0) guidBytes[3] ^= packet.ReadByte();

            packet.WriteGuid("Guid", guidBytes);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_2_15211)]
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

            packet.WriteGuid("Guid", guidBytes);
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

            packet.WriteGuid("Guid", guidBytes);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleBattlefieldListServer406(Packet packet)
        {
            packet.ReadEnum<BattlegroundListFlags>("Flags", TypeCode.Byte);
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

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685)) // verify if it wasn't earlier or later
            {
                packet.ReadBoolean("Has Win");
                packet.ReadInt32("Winner Honor Reward");
                packet.ReadInt32("Winner Arena Reward");
                packet.ReadInt32("Loser Honor Reward");

                if (packet.ReadBoolean("Is random"))
                {
                    packet.ReadByte("Random Has Win");
                    packet.ReadInt32("Random Winner Honor Reward");
                    packet.ReadInt32("Random Winner Arena Reward");
                    packet.ReadInt32("Random Loser Honor Reward");
                }
            }

            var count = packet.ReadUInt32("BG Instance count");
            for (var i = 0; i < count; i++)
                packet.ReadUInt32("Instance ID", i);
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
            if (slot >= 2)
            {
                packet.ReadToEnd(); // Client does this too
                return;
            }

            packet.ReadGuid("GUID");

            if (!packet.CanRead())
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

                    packet.ReadUInt32("Instance Expiration");
                    packet.ReadUInt32("Instance Start Time");
                    packet.ReadByte("Arena faction");
                    break;
            }
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_HELLO)]
        [Parser(Opcode.CMSG_AREA_SPIRIT_HEALER_QUERY)]
        [Parser(Opcode.CMSG_AREA_SPIRIT_HEALER_QUEUE)]
        [Parser(Opcode.CMSG_REPORT_PVP_AFK)]
        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_LEFT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED)]
        public static void HandleBattlemasterHello(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_LEFT, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattleGroundPlayerLeft434(Packet packet)
        {
            var guid = packet.StartBitStream(7, 6, 2, 4, 5, 1, 3, 0);
            packet.ParseBitStream(guid, 4, 2, 5, 7, 0, 6, 1, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlemasterJoin(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Battleground, "BGType");
            packet.ReadUInt32("Instance Id");
            packet.ReadBoolean("As group");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlemasterJoin434(Packet packet)
        {
            packet.ReadUInt32("Instance Id");
            var guid = new byte[8];
            guid[2] = packet.ReadBit().ToByte();
            guid[0] = packet.ReadBit().ToByte();
            guid[3] = packet.ReadBit().ToByte();
            guid[1] = packet.ReadBit().ToByte();
            guid[5] = packet.ReadBit().ToByte();
            packet.ReadBit("As Group");
            guid[4] = packet.ReadBit().ToByte();
            guid[6] = packet.ReadBit().ToByte();
            guid[7] = packet.ReadBit().ToByte();

            packet.ParseBitStream(guid, 2, 6, 4, 3, 7, 0, 5, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_ARENA, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlemasterJoinArena(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Slot");
            packet.ReadBoolean("As group");
            packet.ReadBoolean("Rated");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_ARENA, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlemasterJoinArena406(Packet packet)
        {
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.SMSG_REPORT_PVP_AFK_RESULT)]
        public static void HandleReportPvPAFKResult(Packet packet)
        {
            // First three bytes = result, 5 -> enabled, else except 6 -> disabled
            packet.ReadByte("Unk byte");
            packet.ReadByte("Unk byte");
            packet.ReadByte("Unk byte");
            packet.ReadGuid("Unk guid");
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
                packet.WriteGuid("Guid", guidBytes);
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

            packet.WriteGuid("Guid", guidBytes);
        }

        [Parser(Opcode.MSG_PVP_LOG_DATA, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandlePvPLogData(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                return;

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

        [Parser(Opcode.MSG_PVP_LOG_DATA, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandlePvPLogData406(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                return;

            var flags = packet.ReadEnum<BattlegroundUpdateFlags>("Flags", TypeCode.Byte);

            if (flags.HasAnyFlag(BattlegroundUpdateFlags.ArenaNames))
                for (var i = 0; i < 2; ++i)
                    packet.ReadCString("Name", i);

            if (flags.HasAnyFlag(BattlegroundUpdateFlags.ArenaScores))
                for (var i = 0; i < 2; ++i)
                {
                    packet.ReadUInt32("Points Lost", i);
                    packet.ReadUInt32("Points Gained", i);
                    packet.ReadUInt32("Matchmaker Rating", i);
                }

            var count = packet.ReadUInt32("Score count");

            if (flags.HasAnyFlag(BattlegroundUpdateFlags.Finished))
                packet.ReadByte("Team Winner");

            var tempCount = (int)count;
            do
            {
                packet.ReadByte("Player Update Flags", tempCount);
                tempCount -= 2;
            }
            while (tempCount > 0);

            for (var i = 0; i < count; i++)
            {
                packet.ReadUInt32("Damage done", i);

                //if (updateFlags & 128)
                    packet.ReadUInt32("Unk", i);

                var count2 = packet.ReadUInt32("Extra values counter", i);

                //if (???) Depends on read Update Flags
                {
                    packet.ReadUInt32("Honorable Kills", i);
                    packet.ReadUInt32("Deaths", i);
                    packet.ReadUInt32("Bonus Honor", i);
                }

                packet.ReadGuid("Player GUID", i);
                packet.ReadUInt32("Killing Blows", i);
                for (var j = 0; j < count2; j++)
                    packet.ReadUInt32("Value", i, j);

                //if (UpdateFlags & 1)
                    packet.ReadUInt32("Unk", i);

                packet.ReadUInt32("Healing done", i);
            }
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_STATE_CHANGE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrStateChanged434(Packet packet)
        {
            var bytes = new byte[8];
            bytes[5] = packet.ReadBit().ToByte();
            bytes[3] = packet.ReadBit().ToByte();
            bytes[7] = packet.ReadBit().ToByte();
            bytes[2] = packet.ReadBit().ToByte();
            bytes[1] = packet.ReadBit().ToByte();
            bytes[6] = packet.ReadBit().ToByte();
            bytes[0] = packet.ReadBit().ToByte();
            bytes[4] = packet.ReadBit().ToByte();


            if (bytes[1] != 0) bytes[1] ^= packet.ReadByte();
            if (bytes[2] != 0) bytes[2] ^= packet.ReadByte();
            if (bytes[5] != 0) bytes[5] ^= packet.ReadByte();
            packet.ReadEnum<BattlegroundStatus>("status", TypeCode.UInt32);
            if (bytes[4] != 0) bytes[4] ^= packet.ReadByte();
            if (bytes[7] != 0) bytes[7] ^= packet.ReadByte();
            if (bytes[0] != 0) bytes[0] ^= packet.ReadByte();
            if (bytes[3] != 0) bytes[3] ^= packet.ReadByte();
            if (bytes[6] != 0) bytes[6] ^= packet.ReadByte();
            packet.WriteGuid("Guid", bytes);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_STATE_CHANGE, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrStateChanged406(Packet packet)
        {
            packet.ReadEnum<BattlegroundStatus>("status", TypeCode.UInt32);
            packet.ReadGuid("BG Guid");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_STATE_CHANGE, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlefieldMgrStateChanged(Packet packet)
        {
            packet.ReadEnum<BattlegroundStatus>("Old status", TypeCode.UInt32);
            packet.ReadEnum<BattlegroundStatus>("New status", TypeCode.UInt32);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTRY_INVITE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrInviteSend(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id");
            packet.ReadTime("Invite lasts until");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTRY_INVITE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrInviteSend434(Packet packet)
        {
            var bytes = new byte[8];
            bytes[5] = packet.ReadBit().ToByte();
            bytes[3] = packet.ReadBit().ToByte();
            bytes[7] = packet.ReadBit().ToByte();
            bytes[2] = packet.ReadBit().ToByte();
            bytes[6] = packet.ReadBit().ToByte();
            bytes[4] = packet.ReadBit().ToByte();
            bytes[1] = packet.ReadBit().ToByte();
            bytes[0] = packet.ReadBit().ToByte();
            

            if (bytes[6] != 0) bytes[6] ^= packet.ReadByte();
            packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id");
            if (bytes[1] != 0) bytes[1] ^= packet.ReadByte();
            if (bytes[3] != 0) bytes[3] ^= packet.ReadByte();
            if (bytes[4] != 0) bytes[4] ^= packet.ReadByte();
            if (bytes[2] != 0) bytes[2] ^= packet.ReadByte();
            if (bytes[0] != 0) bytes[0] ^= packet.ReadByte();
            packet.ReadTime("Invite lasts until");
            if (bytes[7] != 0) bytes[7] ^= packet.ReadByte();
            if (bytes[5] != 0) bytes[5] ^= packet.ReadByte();
            packet.WriteGuid("Guid", bytes);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_INVITE)]
        public static void HandleBattlefieldMgrQueueInvite(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadByte("Warmup");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_QUEUE_INVITE_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrQueueInviteResponse(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadBoolean("Accepted");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_QUEUE_INVITE_RESPONSE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrQueueInviteResponse434(Packet packet)
        {
            var guid = new byte[8];
            guid[2] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[0] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[4] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[3] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[5] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[7] = (byte)(packet.ReadBit() ? 1 : 0);
            packet.ReadBit("Accepted");
            guid[1] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[6] = (byte)(packet.ReadBit() ? 1 : 0);

            packet.ParseBitStream(guid, 1, 3, 2, 4, 6, 7, 0, 5);
            packet.WriteGuid("Guid", guid);
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

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTERED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrEntered434(Packet packet)
        {
            var guid = new byte[8];
            packet.ReadBit("Unk Bit1");
            packet.ReadBit("Clear AFK");
            guid[1] = packet.ReadBit().ToByte();
            guid[4] = packet.ReadBit().ToByte();
            guid[5] = packet.ReadBit().ToByte();
            guid[0] = packet.ReadBit().ToByte();
            guid[3] = packet.ReadBit().ToByte();
            packet.ReadBit("Unk Bit 3");

            guid[6] = packet.ReadBit().ToByte();
            guid[7] = packet.ReadBit().ToByte();
            guid[2] = packet.ReadBit().ToByte();

            packet.ParseBitStream(guid, 5, 3, 0, 4, 1, 7, 2, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTERED, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrEntered406(Packet packet)
        {
            packet.ReadByte("Unk");
            packet.ReadGuid("BG Guid");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTERED, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlefieldMgrEntered(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadByte("Unk Byte 1");
            packet.ReadByte("Unk Byte 2");
            packet.ReadByte("Clear AFK");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_EJECTED, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrEjected(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadByte("Reason");
            packet.ReadByte("Battle Status");
            packet.ReadByte("Relocated");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_EJECTED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrEjected434(Packet packet)
        {
            var bytes = new byte[8];
            bytes[2] = packet.ReadBit().ToByte();
            bytes[5] = packet.ReadBit().ToByte();
            bytes[1] = packet.ReadBit().ToByte();
            bytes[0] = packet.ReadBit().ToByte();
            bytes[3] = packet.ReadBit().ToByte();
            bytes[6] = packet.ReadBit().ToByte();
            packet.ReadBit("Relocated");
            bytes[7] = packet.ReadBit().ToByte();
            bytes[4] = packet.ReadBit().ToByte();

            packet.ReadByte("Battle Status");

            if (bytes[1] != 0) bytes[1] ^= packet.ReadByte();
            if (bytes[7] != 0) bytes[7] ^= packet.ReadByte();
            if (bytes[4] != 0) bytes[4] ^= packet.ReadByte();
            if (bytes[2] != 0) bytes[2] ^= packet.ReadByte();
            if (bytes[3] != 0) bytes[3] ^= packet.ReadByte();
            packet.ReadByte("Reason");
            if (bytes[6] != 0) bytes[6] ^= packet.ReadByte();
            if (bytes[0] != 0) bytes[0] ^= packet.ReadByte();
            if (bytes[5] != 0) bytes[5] ^= packet.ReadByte();

            packet.WriteGuid("Guid", bytes);


        }

        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_ENTRY_INVITE_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrEntryInviteResponse(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadBoolean("Accepted");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_ENTRY_INVITE_RESPONSE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrEntryInviteResponse434(Packet packet)
        {
            var guid = new byte[8];
            guid[6] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[1] = (byte)(packet.ReadBit() ? 1 : 0);
            packet.ReadBit("Accepted");            
            guid[5] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[3] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[2] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[0] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[7] = (byte)(packet.ReadBit() ? 1 : 0);
            guid[4] = (byte)(packet.ReadBit() ? 1 : 0);


            packet.ParseBitStream(guid, 0, 3, 4, 2, 1, 6, 7, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_EXIT_REQUEST, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrExitRequest(Packet packet)
        {
            packet.ReadInt32("Battle Id");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_EXIT_REQUEST, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrExitRequest434(Packet packet)
        {
            var guid = packet.StartBitStream(2, 0, 3, 7, 4, 5, 6, 1);
            packet.ParseBitStream(guid, 5, 2, 0, 1, 4, 3, 7, 6);
            packet.WriteGuid(guid);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_EJECT_PENDING)]
        public static void HandleBattlefieldMgrEjectPending(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadBoolean("Remote");
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
                var guid = packet.ReadGuid("GUID", i);
                packet.ReadBoolean("Online", i);
                var name = packet.ReadCString("Name", i);
                StoreGetters.AddName(guid, name);
                packet.ReadUInt32("Captain", i);
                packet.ReadByte("Level", i);
                packet.ReadEnum<Class>("Class", TypeCode.Byte, i);
                packet.ReadUInt32("Week Games", i);
                packet.ReadUInt32("Week Win", i);
                packet.ReadUInt32("Seasonal Games", i);
                packet.ReadUInt32("Seasonal Wins", i);
                packet.ReadUInt32("Personal Rating", i);
                if (unk != 0)
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
            packet.ReadUInt32("Team Id");
        }

        [Parser(Opcode.CMSG_ARENA_TEAM_CREATE)]
        public static void HandleArenaTeamCreate(Packet packet)
        {
            packet.ReadUInt32("Background Color");
            packet.ReadUInt32("Icon");
            packet.ReadUInt32("Icon Color");
            packet.ReadUInt32("Border");
            packet.ReadUInt32("Border Color");
            packet.ReadUInt32("Type");
            packet.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_ARENA_TEAM_INVITE)]
        [Parser(Opcode.CMSG_ARENA_TEAM_REMOVE)]
        [Parser(Opcode.CMSG_ARENA_TEAM_LEADER)]
        public static void HandleArenaTeamInvite(Packet packet)
        {
            packet.ReadUInt32("Team Id");
            packet.ReadCString("Name");
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_COMMAND_RESULT, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleArenaTeamCommandResult(Packet packet)
        {
            packet.ReadUInt32("Action"); // FIXME: Use enum
            packet.ReadCString("Team Name");
            packet.ReadCString("Player Name");
            packet.ReadUInt32("ErrorId"); // FIXME: Use enum
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_COMMAND_RESULT, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleArenaTeamCommandResult406(Packet packet)
        {
            packet.ReadEnum<ArenaCommandResult>("Result", TypeCode.UInt32);
            packet.ReadCString("Team Name");
            packet.ReadCString("Player Name");
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_EVENT)]
        public static void HandleArenaTeamEvent(Packet packet)
        {
            packet.ReadEnum<ArenaEvent>("Event", TypeCode.Byte);
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

        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_QUEUE_REQUEST, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattelfieldMgrQueueRequest(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_QUEUE_REQUEST, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattelfieldMgrQueueRequest434(Packet packet)
        {
            var guid = packet.StartBitStream(0, 3, 7, 4, 6, 2, 1, 5);
            packet.ParseBitStream(guid, 6, 3, 2, 4, 7, 1, 5, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_REQUEST_RATED_BG_INFO, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_REQUEST_PVP_OPTIONS_ENABLED)]
        [Parser(Opcode.CMSG_BATTLEGROUND_PLAYER_POSITIONS)]
        [Parser(Opcode.SMSG_BATTLEGROUND_INFO_THROTTLED)]
        [Parser(Opcode.SMSG_BATTLEFIELD_PORT_DENIED)]
        public static void HandleNullBattleground(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_REQUEST_RATED_BG_INFO, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleRequestRatedBGInfo434(Packet packet)
        {
            packet.ReadByte("Unk Byte");
        }
        
        [Parser(Opcode.CMSG_REQUEST_INSPECT_RATED_BG_STATS, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleRequestInspectRBGStats(Packet packet)
        {
            var guid = packet.StartBitStream(1, 4, 6, 5, 0, 2, 7, 3);
            packet.ParseBitStream(guid, 4, 7, 2, 5, 6, 3, 0, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_REQUEST_HONOR_STATS, ClientVersionBuild.V4_3_4_15595)]
        public static void Handle31006(Packet packet)
        {
            var guid = packet.StartBitStream(1, 5, 7, 3, 2, 4, 0, 6);

            packet.ParseBitStream(guid, 4, 7, 0, 5, 1, 6, 2, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_POSITIONS)]
        public static void HandleBattlegroundPlayerPositions(Packet packet)
        {
            var count1 = packet.ReadUInt32("Count 1");
            for (var i = 0; i < count1; ++i)
            {
                packet.ReadGuid("GUID", i);
                packet.ReadVector2("Position", i);
            }

            var count2 = packet.ReadUInt32("Count 2");
            for (var i = 0; i < count2; ++i)
            {
                packet.ReadGuid("GUID", i);
                packet.ReadVector2("Position", i);
            }
        }

        [Parser(Opcode.SMSG_INSPECT_RATED_BG_STATS)]
        public static void HandleInspectRatedBGStats(Packet packet)
        {
            var guid = new byte[8];
            guid[6] = packet.ReadBit().ToByte();
            guid[4] = packet.ReadBit().ToByte();
            guid[5] = packet.ReadBit().ToByte();
            guid[1] = packet.ReadBit().ToByte();
            guid[2] = packet.ReadBit().ToByte();
            guid[7] = packet.ReadBit().ToByte();
            guid[0] = packet.ReadBit().ToByte();
            guid[3] = packet.ReadBit().ToByte();

            if (guid[4] != 0) guid[4] ^= packet.ReadByte();

            packet.ReadInt32("Rating");

            if (guid[1] != 0) guid[1] ^= packet.ReadByte();
            if (guid[7] != 0) guid[7] ^= packet.ReadByte();
            if (guid[3] != 0) guid[3] ^= packet.ReadByte();
            if (guid[6] != 0) guid[6] ^= packet.ReadByte();

            packet.ReadInt32("Won");
            packet.ReadInt32("Played");

            if (guid[2] != 0) guid[2] ^= packet.ReadByte();
            if (guid[5] != 0) guid[5] ^= packet.ReadByte();
            if (guid[0] != 0) guid[0] ^= packet.ReadByte();

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_RATED_INFO)]
        public static void HandleBattlefieldRatedInfo(Packet packet)
        {
            packet.ReadUInt32("Rating");
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32("Unk UInt32 2");
            packet.ReadUInt32("Unk UInt32 3");
            packet.ReadUInt32("Conquest Points Weekly Cap");
            packet.ReadUInt32("Unk UInt32 5");
            packet.ReadUInt32("Unk UInt32 6");
            packet.ReadUInt32("Current Conquest Points");
        }

        [Parser(Opcode.SMSG_RATED_BG_STATS)]
        public static void HandleRatedBGStats(Packet packet)
        {
            for (var i = 0; i < 18; i++)
                packet.ReadUInt32("Unk UInt32", i);
        }

        [Parser(Opcode.SMSG_PVP_OPTIONS_ENABLED)]
        public static void HandlePVPOptionsEnabled(Packet packet)
        {
            for (var i = 0; i < 5; i++)
                packet.ReadBit("Unk Boolean", i);
        }

        [Parser(Opcode.SMSG_REQUEST_PVP_REWARDS_RESPONSE)]
        public static void HandlePVPRewardsResponse(Packet packet)
        {
            packet.ReadUInt32("Conquest Weekly Cap");
            packet.ReadUInt32("Unk Uint32");
            packet.ReadUInt32("Arena Conquest Cap");
            packet.ReadUInt32("Unk Uint32");
            packet.ReadUInt32("Unk Uint32");
            packet.ReadUInt32("Current Conquest Points");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_PORT)]
        public static void HandleBattlefieldPort434(Packet packet)
        {
            packet.ReadTime("Time");
            packet.ReadUInt32("Unk Uint32");
            packet.ReadEntryWithName<Int32>(StoreNameType.Battleground, "BGType");

            var guid = packet.StartBitStream(0, 1, 5, 6, 7, 4, 3, 2);
            packet.ReadBit("Join");

            packet.ParseBitStream(guid, 1, 3, 5, 7, 0, 2, 6, 4);
            packet.WriteGuid("Guid", guid);
        }


        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_FAILED)]
        public static void HandleBattlefieldStatusFailed(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];
            guid1[3] = packet.ReadBit().ToByte();
            guid3[3] = packet.ReadBit().ToByte();
            guid2[3] = packet.ReadBit().ToByte();
            guid3[0] = packet.ReadBit().ToByte();
            guid1[6] = packet.ReadBit().ToByte();
            guid2[5] = packet.ReadBit().ToByte();
            guid2[6] = packet.ReadBit().ToByte();
            guid2[4] = packet.ReadBit().ToByte();

            guid2[2] = packet.ReadBit().ToByte();
            guid3[1] = packet.ReadBit().ToByte();
            guid1[1] = packet.ReadBit().ToByte();
            guid3[5] = packet.ReadBit().ToByte();
            guid3[6] = packet.ReadBit().ToByte();
            guid2[1] = packet.ReadBit().ToByte();
            guid1[7] = packet.ReadBit().ToByte();
            guid3[4] = packet.ReadBit().ToByte();

            guid1[2] = packet.ReadBit().ToByte();
            guid1[5] = packet.ReadBit().ToByte();
            guid3[7] = packet.ReadBit().ToByte();
            guid1[4] = packet.ReadBit().ToByte();
            guid1[0] = packet.ReadBit().ToByte();
            guid2[0] = packet.ReadBit().ToByte();
            guid3[2] = packet.ReadBit().ToByte();
            guid2[7] = packet.ReadBit().ToByte();

            if (guid1[1] != 0) guid1[1] ^= packet.ReadByte();

            packet.ReadEnum<BattlegroundStatus>("Status", TypeCode.UInt32);
            packet.ReadInt32("Unk Int32");

            if (guid2[6] != 0) guid2[6] ^= packet.ReadByte();
            if (guid2[3] != 0) guid2[3] ^= packet.ReadByte();
            if (guid2[7] != 0) guid2[7] ^= packet.ReadByte();
            if (guid2[4] != 0) guid2[4] ^= packet.ReadByte();
            if (guid1[0] != 0) guid1[0] ^= packet.ReadByte();
            if (guid2[5] != 0) guid2[5] ^= packet.ReadByte();
            if (guid1[7] != 0) guid1[7] ^= packet.ReadByte();
            if (guid1[6] != 0) guid1[6] ^= packet.ReadByte();
            if (guid1[2] != 0) guid1[2] ^= packet.ReadByte();
            if (guid3[6] != 0) guid3[6] ^= packet.ReadByte();
            if (guid3[3] != 0) guid3[3] ^= packet.ReadByte();
            if (guid2[1] != 0) guid2[1] ^= packet.ReadByte();
            if (guid1[3] != 0) guid1[3] ^= packet.ReadByte();
            if (guid3[0] != 0) guid3[0] ^= packet.ReadByte();
            if (guid3[1] != 0) guid3[1] ^= packet.ReadByte();
            if (guid3[4] != 0) guid3[4] ^= packet.ReadByte();
            if (guid2[0] != 0) guid2[0] ^= packet.ReadByte();
            if (guid1[5] != 0) guid1[5] ^= packet.ReadByte();
            if (guid3[7] != 0) guid3[7] ^= packet.ReadByte();
            if (guid1[4] != 0) guid1[4] ^= packet.ReadByte();
            if (guid2[2] != 0) guid2[2] ^= packet.ReadByte();

            packet.ReadEntryWithName<Int32>(StoreNameType.Battleground, "BGType");

            if (guid3[2] != 0) guid3[2] ^= packet.ReadByte();

            packet.ReadTime("Time");

            if (guid3[5] != 0) guid3[5] ^= packet.ReadByte();

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
            packet.WriteGuid("Guid3", guid3);
        }

        //[Parser(Opcode.CMSG_BATTLEFIELD_MANAGER_ADVANCE_STATE)]
        //[Parser(Opcode.CMSG_BATTLEFIELD_MANAGER_SET_NEXT_TRANSITION_TIME)]
        //[Parser(Opcode.CMSG_START_BATTLEFIELD_CHEAT)]
        //[Parser(Opcode.CMSG_END_BATTLEFIELD_CHEAT)]
    }
}
