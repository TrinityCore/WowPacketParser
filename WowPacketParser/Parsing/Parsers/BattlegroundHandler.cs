using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.SMSG_BATTLEGROUND_EXIT_QUEUE)]
        public static void HandleBattlegroundExitQueue(Packet packet)
        {
            packet.Translator.ReadUInt32("Queue slot");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_IN_PROGRESS)]
        public static void HandleBattlegroundInProgress(Packet packet)
        {
            packet.Translator.ReadBit("IsRated");
            packet.Translator.ReadUInt32("Time since started");
            packet.Translator.ReadUInt32("Queue slot");
            packet.Translator.ReadInt32<MapId>("Map Id");
            packet.Translator.ReadGuid("BG Guid");
            packet.Translator.ReadUInt32("Time until closed");
            packet.Translator.ReadByte("Teamsize");
            packet.Translator.ReadByte("Max Level");
            packet.Translator.ReadUInt32("Client Instance ID");
            packet.Translator.ReadByte("Min Level");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_WAIT_JOIN)]
        public static void HandleBattlegroundWaitJoin(Packet packet)
        {
            packet.Translator.ReadBit("IsArena");
            packet.Translator.ReadByte("Min Level");
            packet.Translator.ReadUInt32("Client Instance ID");
            packet.Translator.ReadGuid("BG Guid");
            packet.Translator.ReadInt32("Queue slot");
            packet.Translator.ReadByte("Teamsize");
            packet.Translator.ReadUInt32("Expire Time");
            packet.Translator.ReadInt32<MapId>("Map Id");
            packet.Translator.ReadByte("Max Level");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_WAIT_LEAVE)]
        public static void HandleBattlegroundWaitLeave(Packet packet)
        {
            packet.Translator.ReadByte("Unk");
            packet.Translator.ReadUInt32("Time left");
            packet.Translator.ReadByte("Min Level");
            packet.Translator.ReadByte("Unk2");
            packet.Translator.ReadByte("Unk3");
            packet.Translator.ReadInt32("Queue slot");
            packet.Translator.ReadByte("Max Level");
            packet.Translator.ReadUInt32("Time2");
            packet.Translator.ReadByte("Teamsize");
            packet.Translator.ReadUInt32("Client Instance ID");
            packet.Translator.ReadByte("Unk4");
            packet.Translator.ReadGuid("BG Guid");
            packet.Translator.ReadByte("Unk5");
        }

        [Parser(Opcode.SMSG_AREA_SPIRIT_HEALER_TIME)]
        public static void HandleAreaSpiritHealerTime(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32("Timer");
        }

        [Parser(Opcode.MSG_BATTLEGROUND_PLAYER_POSITIONS)]
        public static void HandleBattlegrounPlayerPositions(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                return;

            var count1 = packet.Translator.ReadInt32("Count1");
            for (var i = 0; i < count1; i++)
            {
                packet.Translator.ReadGuid("Player GUID", i);
                packet.Translator.ReadVector2("Position", i);
            }

            var count2 = packet.Translator.ReadInt32("Count2");
            for (var i = 0; i < count2; i++)
            {
                packet.Translator.ReadGuid("Player GUID", i);
                packet.Translator.ReadVector2("Position", i);
            }
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_JOIN, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleBattlefieldJoin422(Packet packet)
        {
            var guid = new byte[8];

            guid[0] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("As Group");
            guid[1] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();

            packet.Translator.ReadUInt32("Unknown uint32");

            packet.Translator.ParseBitStream(guid, 5, 0, 2, 1, 4, 6, 3, 7);
            packet.Translator.WriteGuid("BG Guid", guid);
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_JOIN, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleBattlefieldJoin(Packet packet)
        {
            packet.Translator.ReadBit("As Group");
            packet.Translator.ReadUInt32("Unk1");
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_LIST)]
        public static void HandleBattlefieldListClient(Packet packet)
        {
            packet.Translator.ReadInt32<BgId>("BGType");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                return;

            packet.Translator.ReadBool("From UI");
            packet.Translator.ReadByte("Unk Byte (BattlefieldList)");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_2_15211)]
        public static void HandleBattlefieldListServer430(Packet packet)
        {
            var guidBytes = new byte[8];

            guidBytes[2] = packet.Translator.ReadBit();
            guidBytes[7] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("UnkBit1");
            var count = packet.Translator.ReadBits("BG Instance count", 21);
            guidBytes[0] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("UnkBit2");
            guidBytes[3] = packet.Translator.ReadBit();
            guidBytes[1] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("UnkBit3");
            guidBytes[5] = packet.Translator.ReadBit();
            guidBytes[4] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Random Has Win");
            guidBytes[6] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guidBytes, 4);

            packet.Translator.ReadInt32("Loser Honor Reward");
            packet.Translator.ReadInt32("Winner Honor Reward");
            for (var i = 0; i < count; i++)
                packet.Translator.ReadUInt32("Instance ID", i);

            packet.Translator.ReadXORByte(guidBytes, 7);

            packet.Translator.ReadInt32<BgId>("BG type");

            packet.Translator.ReadXORByte(guidBytes, 1);

            packet.Translator.ReadInt32("Random Loser Honor Reward");
            packet.Translator.ReadInt32("Random Winner Conquest Reward");

            packet.Translator.ReadXORByte(guidBytes, 2);

            packet.Translator.ReadByte("Max level");

            packet.Translator.ReadXORByte(guidBytes, 0);

            packet.Translator.ReadInt32("Winner Conquest Reward");
            packet.Translator.ReadByte("Min level");

            packet.Translator.ReadXORByte(guidBytes, 5);

            packet.Translator.ReadInt32("Random Winner Honor Reward");

            packet.Translator.ReadXORByte(guidBytes, 3);
            packet.Translator.ReadXORByte(guidBytes, 6);

            packet.Translator.WriteGuid("Guid", guidBytes);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleBattlefieldListServer422(Packet packet)
        {
            var guidBytes = new byte[8];

            guidBytes[5] = packet.Translator.ReadBit();
            guidBytes[0] = packet.Translator.ReadBit();
            guidBytes[4] = packet.Translator.ReadBit();
            guidBytes[1] = packet.Translator.ReadBit();
            guidBytes[3] = packet.Translator.ReadBit();
            guidBytes[6] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("UnkBit1");
            packet.Translator.ReadBit("UnkBit2");
            guidBytes[2] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("UnkBit3");
            packet.Translator.ReadBit("UnkBit4");
            guidBytes[7] = packet.Translator.ReadBit();

            packet.Translator.ReadInt32("Winner Honor Reward");

            packet.Translator.ReadXORByte(guidBytes, 3);
            packet.Translator.ReadXORByte(guidBytes, 5);

            packet.Translator.ReadInt32("Random Winner Honor Reward");

            packet.Translator.ReadXORByte(guidBytes, 0);

            packet.Translator.ReadByte("Max level");

            packet.Translator.ReadXORByte(guidBytes, 7);
            packet.Translator.ReadXORByte(guidBytes, 1);
            packet.Translator.ReadXORByte(guidBytes, 2);
            packet.Translator.ReadXORByte(guidBytes, 4);

            packet.Translator.ReadInt32<BgId>("BGType");
            packet.Translator.ReadInt32("Random Winner Conquest Reward");
            packet.Translator.ReadInt32("Winner Conquest Reward");

            packet.Translator.ReadXORByte(guidBytes, 6);

            packet.Translator.ReadInt32("Random Loser Honor Reward");
            packet.Translator.ReadByte("Min level");
            packet.Translator.ReadInt32("Loser Honor Reward");

            var count = packet.Translator.ReadUInt32("BG Instance count");
            for (var i = 0; i < count; i++)
                packet.Translator.ReadUInt32("Instance ID", i);

            packet.Translator.WriteGuid("Guid", guidBytes);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleBattlefieldListServer406(Packet packet)
        {
            packet.Translator.ReadByteE<BattlegroundListFlags>("Flags");
            packet.Translator.ReadByte("Min level");
            packet.Translator.ReadInt32("Winner Honor Reward");
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadInt32("Random Winner Honor Reward");
            packet.Translator.ReadByte("Max level");
            packet.Translator.ReadInt32("Random Loser Honor Reward");
            packet.Translator.ReadInt32("Random Winner Conquest Reward");
            packet.Translator.ReadInt32("Winner Conquest Reward");
            packet.Translator.ReadInt32<BgId>("BGType");

            var count = packet.Translator.ReadUInt32("BG Instance count");
            for (var i = 0; i < count; i++)
                packet.Translator.ReadUInt32("Instance ID", i);

            packet.Translator.ReadInt32("Loser Honor Reward");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlefieldListServer(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_0_14333))
                packet.Translator.ReadBool("From UI");

            packet.Translator.ReadInt32<BgId>("BGType");
            packet.Translator.ReadByte("Min Level");
            packet.Translator.ReadByte("Max Level");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685)) // verify if it wasn't earlier or later
            {
                packet.Translator.ReadBool("Has Win");
                packet.Translator.ReadInt32("Winner Honor Reward");
                packet.Translator.ReadInt32("Winner Arena Reward");
                packet.Translator.ReadInt32("Loser Honor Reward");

                if (packet.Translator.ReadBool("Is random"))
                {
                    packet.Translator.ReadByte("Random Has Win");
                    packet.Translator.ReadInt32("Random Winner Honor Reward");
                    packet.Translator.ReadInt32("Random Winner Arena Reward");
                    packet.Translator.ReadInt32("Random Loser Honor Reward");
                }
            }

            var count = packet.Translator.ReadUInt32("BG Instance count");
            for (var i = 0; i < count; i++)
                packet.Translator.ReadUInt32("Instance ID", i);
        }

        [Parser(Opcode.CMSG_BATTLEGROUND_PORT_AND_LEAVE, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlefieldPort406(Packet packet)
        {
            packet.Translator.ReadBit("Join");
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_BATTLEGROUND_PORT_AND_LEAVE, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlefieldPort(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadBool("Join BG");
        }

        [Parser(Opcode.CMSG_LEAVE_BATTLEFIELD)]
        public static void HandleBattlefieldLeave(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_LEAVE)] // Differences from above packet?
        [Parser(Opcode.CMSG_BATTLEFIELD_STATUS)]
        public static void HandleBGZeroLengthPackets(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_1_13164)]
        public static void HandleBattlefieldStatusServer(Packet packet)
        {
            var slot = packet.Translator.ReadUInt32("Queue Slot");
            if (slot >= 2)
            {
                packet.ReadToEnd(); // Client does this too
                return;
            }

            packet.Translator.ReadGuid("GUID");

            if (!packet.CanRead())
                return;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
            {
                packet.Translator.ReadByte("Min Level");
                packet.Translator.ReadByte("Max Level");
            }

            if (!packet.CanRead())
                return;

            packet.Translator.ReadUInt32("Client Instance ID");
            packet.Translator.ReadBool("Rated");
            var status = packet.Translator.ReadUInt32E<BattlegroundStatus>("Status");
            switch (status)
            {
                case BattlegroundStatus.WaitQueue:
                    packet.Translator.ReadUInt32("Average Wait Time");
                    packet.Translator.ReadUInt32("Time in queue");
                    break;
                case BattlegroundStatus.WaitJoin:
                    packet.Translator.ReadInt32<MapId>("Map ID");

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_5_12213))
                        packet.Translator.ReadGuid("GUID");

                    packet.Translator.ReadUInt32("Time left");
                    break;
                case BattlegroundStatus.InProgress:
                    packet.Translator.ReadInt32<MapId>("Map ID");

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_5_12213))
                        packet.Translator.ReadGuid("GUID");

                    packet.Translator.ReadUInt32("Instance Expiration");
                    packet.Translator.ReadUInt32("Instance Start Time");
                    packet.Translator.ReadByte("Arena faction");
                    break;
            }
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_HELLO)]
        [Parser(Opcode.CMSG_AREA_SPIRIT_HEALER_QUERY)]
        [Parser(Opcode.CMSG_AREA_SPIRIT_HEALER_QUEUE)]
        [Parser(Opcode.CMSG_REPORT_PVP_PLAYER_AFK)]
        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_LEFT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleBattlemasterHello(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlegroundPlayerJoined430(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(2, 3, 5, 4, 6, 7, 1, 0);
            packet.Translator.ParseBitStream(guid, 3, 1, 2, 6, 0, 7, 4, 5);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlemasterJoin(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadInt32<BgId>("BGType");
            packet.Translator.ReadUInt32("Instance Id");
            packet.Translator.ReadBool("As group");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_ARENA, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlemasterJoinArena(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadBool("As group");
            packet.Translator.ReadBool("Rated");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_ARENA, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlemasterJoinArena406(Packet packet)
        {
            packet.Translator.ReadByte("Slot");
        }

        [Parser(Opcode.SMSG_REPORT_PVP_AFK_RESULT)]
        public static void HandleReportPvPAFKResult(Packet packet)
        {
            // First three bytes = result, 5 -> enabled, else except 6 -> disabled
            packet.Translator.ReadByte("Unk byte");
            packet.Translator.ReadByte("Unk byte");
            packet.Translator.ReadByte("Unk byte");
            packet.Translator.ReadGuid("Unk guid");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_QUEUED, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleRGroupJoinedBattleground(Packet packet)
        {
            var val = packet.Translator.ReadInt32();
            if (val < 1)
            {
                var result = packet.AddValue("Result", (BattlegroundError)val);
                if (result == BattlegroundError.JoinFailedAsGroup ||
                    result == BattlegroundError.CouldntJoinQueueInTime)
                    packet.Translator.ReadGuid("GUID");
            }
            else
                packet.AddValue("Result", "Joined (BGType: " + StoreGetters.GetName(StoreNameType.Battleground, val) + ")");
        }

        [Parser(Opcode.SMSG_JOINED_BATTLEGROUND_QUEUE, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleJoinedBattlegroundQueue422(Packet packet)
        {
            var guidBytes = new byte[8];
            var field14 = new byte[4];
            var field10 = new byte[4];
            var field38 = new byte[4];
            var field3C = new byte[4];

            guidBytes[1] = packet.Translator.ReadBit();
            guidBytes[2] = packet.Translator.ReadBit();
            field38[2] = packet.Translator.ReadBit();
            field10[2] = packet.Translator.ReadBit();
            field14[0] = packet.Translator.ReadBit();
            field14[1] = packet.Translator.ReadBit();
            guidBytes[6] = packet.Translator.ReadBit();
            field10[3] = packet.Translator.ReadBit();
            field14[2] = packet.Translator.ReadBit();
            field3C[0] = packet.Translator.ReadBit();
            field3C[2] = packet.Translator.ReadBit();
            guidBytes[4] = packet.Translator.ReadBit();
            field3C[3] = packet.Translator.ReadBit();
            field38[3] = packet.Translator.ReadBit();
            guidBytes[0] = packet.Translator.ReadBit();
            guidBytes[3] = packet.Translator.ReadBit();
            field3C[1] = packet.Translator.ReadBit();
            guidBytes[7] = packet.Translator.ReadBit();
            field38[0] = packet.Translator.ReadBit();
            guidBytes[5] = packet.Translator.ReadBit();
            field10[1] = packet.Translator.ReadBit();
            field14[3] = packet.Translator.ReadBit();
            field38[1] = packet.Translator.ReadBit();
            field10[0] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guidBytes, 4);

            var bgError = packet.Translator.ReadInt32E<BattlegroundError4x>("Battleground Error");

            packet.Translator.ReadXORByte(guidBytes, 1);

            packet.Translator.ReadXORByte(field10, 1);

            packet.Translator.ReadXORByte(guidBytes, 6);

            packet.Translator.ReadXORByte(field3C, 2);
            packet.Translator.ReadXORByte(field14, 1);
            packet.Translator.ReadXORByte(field14, 2);

            packet.Translator.ReadUInt32("field18");

            packet.Translator.ReadXORByte(field38, 0);
            packet.Translator.ReadXORByte(field3C, 1);
            packet.Translator.ReadXORByte(field10, 0);

            packet.Translator.ReadUInt32("BattlegroundId");

            packet.Translator.ReadXORByte(field38, 3);
            packet.Translator.ReadXORByte(field3C, 3);

            packet.Translator.ReadXORByte(guidBytes, 5);

            packet.Translator.ReadXORByte(field10, 2);

            packet.Translator.ReadXORByte(guidBytes, 0);
            packet.Translator.ReadXORByte(guidBytes, 7);

            packet.Translator.ReadXORByte(field14, 3);
            packet.Translator.ReadXORByte(field10, 3);
            packet.Translator.ReadXORByte(field38, 2);

            packet.Translator.ReadXORByte(guidBytes, 2);

            packet.Translator.ReadXORByte(field38, 1);

            packet.Translator.ReadXORByte(guidBytes, 3);

            packet.Translator.ReadXORByte(field14, 0);
            packet.Translator.ReadXORByte(field3C, 0);

            packet.Translator.ReadUInt32("field1C");

            // note: guid is used to identify the player who's unable to join queue when it happens.

            // on id 0xB, 0xC and 8
            if (bgError == BattlegroundError4x.CouldntJoinQueueInTime
                || bgError == BattlegroundError4x.BattlegroundNotInBattleground
                || bgError == BattlegroundError4x.JoinFailedAsGroup)
            {
                packet.Translator.WriteGuid("Guid", guidBytes);
            }
        }

        [Parser(Opcode.SMSG_JOINED_BATTLEGROUND_QUEUE, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleJoinedBattlegroundQueue(Packet packet)
        {
            packet.Translator.ReadByte("Flags");
            packet.Translator.ReadByte("Max Level");
            packet.Translator.ReadInt32("Avg Wait Time");
            packet.Translator.ReadInt32("Queue Slot");
            packet.Translator.ReadInt32("Instance ID");
            packet.Translator.ReadByte("Min Level");
            packet.Translator.ReadGuid("BG Guid");
            packet.Translator.ReadByte("Team Size");
            packet.Translator.ReadInt32("Time in queue");
        }

        [Parser(Opcode.TEST_422_265C, ClientVersionBuild.V4_2_2_14545)] // SMSG
        public static void HandleRGroupJoinedBattleground422(Packet packet)
        {
            var guidBytes = packet.Translator.StartBitStream(0, 1, 4, 3, 6, 2, 7, 5);
            packet.Translator.ParseBitStream(guidBytes, 2, 6, 3, 4, 5, 7, 1, 0);
            packet.Translator.WriteGuid("Guid", guidBytes);
        }

        [Parser(Opcode.MSG_PVP_LOG_DATA, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandlePvPLogData(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                return;

            var arena = packet.Translator.ReadBool("Arena");
            if (arena)
            {
                packet.Translator.ReadUInt32("[1] Points Lost");
                packet.Translator.ReadUInt32("[1] Points Gained");
                packet.Translator.ReadUInt32("[1] Matchmaker Rating");
                packet.Translator.ReadUInt32("[0] Points Lost");
                packet.Translator.ReadUInt32("[0] Points Gained");
                packet.Translator.ReadUInt32("[0] Matchmaker Rating");
                packet.Translator.ReadCString("[1] Name");
                packet.Translator.ReadCString("[0] Name");
            }

            var finished = packet.Translator.ReadBool("Finished");
            if (finished)
                packet.Translator.ReadByte("Winner");

            var count = packet.Translator.ReadUInt32("Score Count");
            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadGuid("Player GUID", i);
                packet.Translator.ReadUInt32("Killing Blows", i);
                if (!arena)
                {
                    packet.Translator.ReadUInt32("Honorable Kills", i);
                    packet.Translator.ReadUInt32("Deaths", i);
                    packet.Translator.ReadUInt32("Bonus Honor", i);
                }
                else
                    packet.Translator.ReadByte("BG Team", i);

                packet.Translator.ReadUInt32("Damage done", i);
                packet.Translator.ReadUInt32("Healing done", i);

                var count2 = packet.Translator.ReadUInt32("Extra values counter", i);

                for (var j = 0; j < count2; j++)
                    packet.Translator.ReadUInt32("Value", i, j);
            }
        }

        [Parser(Opcode.MSG_PVP_LOG_DATA, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandlePvPLogData406(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                return;

            var arenaScores = packet.Translator.ReadBit("Has Arena Scores");
            var arenaStrings = packet.Translator.ReadBit("Has Arena Strings");
            var finished = packet.Translator.ReadBit("Is Finished");

            if (arenaStrings)
                for (var i = 0; i < 2; ++i)
                    packet.Translator.ReadCString("Name", i);

            if (arenaScores)
            {
                for (var i = 0; i < 2; ++i)
                {
                    packet.Translator.ReadUInt32("Points Lost", i);
                    packet.Translator.ReadUInt32("Points Gained", i);
                    packet.Translator.ReadUInt32("Matchmaker Rating", i);
                }
            }

            var scoreCount = packet.Translator.ReadInt32("Score Count");

            if (finished)
                packet.Translator.ReadByte("Team Winner");

            var bits = new Bit[scoreCount, 4];

            for (var i = 0; i < scoreCount; ++i)
            {
                bits[i, 0] = packet.Translator.ReadBit("Unk Bit 1", i); //  sets *(v23 + v18 + 40)
                bits[i, 1] = packet.Translator.ReadBit("Is Battleground", i); //  sets *(v27 + v18 + 48)
                bits[i, 2] = packet.Translator.ReadBit("Unk Bit 3", i); // sets *(v2->dword1C + v18 + 4)
                bits[i, 3] = packet.Translator.ReadBit("Unk Bit 4", i); // sets *(v32 + v18 + 68)
            }

            for (var i = 0; i < scoreCount; ++i)
            {
                packet.Translator.ReadInt32("Damage Done", i);

                if (bits[i, 0])
                    packet.Translator.ReadInt32("Unk Int32 1", i);

                var count = packet.Translator.ReadInt32("Extra values counter", i);

                if (bits[i, 1])
                {
                    packet.Translator.ReadUInt32("Honorable Kills", i);
                    packet.Translator.ReadUInt32("Deaths", i);
                    packet.Translator.ReadUInt32("Bonus Honor", i);
                }

                packet.Translator.ReadGuid("Player GUID", i);
                packet.Translator.ReadInt32("Killing Blows");
                for (var j = 0; j < count; ++j)
                    packet.Translator.ReadInt32("Extra Value", i, j);

                if (bits[i, 3])
                    packet.Translator.ReadInt32("Unk Int32 2", i);

                packet.Translator.ReadInt32("Healing Done", i);
            }
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_STATE_CHANGE, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrStateChanged406(Packet packet)
        {
            packet.Translator.ReadUInt32E<BattlegroundStatus>("status");
            packet.Translator.ReadGuid("BG Guid");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_STATE_CHANGE, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlefieldMgrStateChanged(Packet packet)
        {
            packet.Translator.ReadUInt32E<BattlegroundStatus>("Old status");
            packet.Translator.ReadUInt32E<BattlegroundStatus>("New status");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTRY_INVITE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrInviteSend(Packet packet)
        {
            packet.Translator.ReadInt32("BattleID");
            packet.Translator.ReadInt32<ZoneId>("ZoneID");
            packet.Translator.ReadTime("ExpireTime");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_INVITE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrQueueInvite(Packet packet)
        {
            packet.Translator.ReadInt32("Battle Id");
            packet.Translator.ReadByte("Warmup");
        }

        [Parser(Opcode.CMSG_BF_MGR_QUEUE_INVITE_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrQueueInviteResponse(Packet packet)
        {
            packet.Translator.ReadInt32("Battle Id");
            packet.Translator.ReadBool("Accepted");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_REQUEST_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrQueueRequestResponse(Packet packet)
        {
            packet.Translator.ReadInt32("Battle Id");
            packet.Translator.ReadInt32<ZoneId>("Zone ID");
            packet.Translator.ReadByte("Accepted");
            packet.Translator.ReadByte("Logging In");
            packet.Translator.ReadByte("Warmup");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTERING, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrEntered406(Packet packet)
        {
            packet.Translator.ReadByte("Unk");
            packet.Translator.ReadGuid("BG Guid");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTERING, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlefieldMgrEntered(Packet packet)
        {
            packet.Translator.ReadInt32("Battle Id");
            packet.Translator.ReadByte("Unk Byte 1");
            packet.Translator.ReadByte("Unk Byte 2");
            packet.Translator.ReadByte("Clear AFK");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_EJECTED, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrEjected(Packet packet)
        {
            packet.Translator.ReadInt32("Battle Id");
            packet.Translator.ReadByte("Reason");
            packet.Translator.ReadByte("Battle Status");
            packet.Translator.ReadByte("Relocated");
        }

        [Parser(Opcode.CMSG_BF_MGR_ENTRY_INVITE_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrEntryInviteResponse(Packet packet)
        {
            packet.Translator.ReadInt32("Battle Id");
            packet.Translator.ReadBool("Accepted");
        }

        [Parser(Opcode.CMSG_BF_MGR_QUEUE_EXIT_REQUEST, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrExitRequest(Packet packet)
        {
            packet.Translator.ReadInt32("Battle Id");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_EJECT_PENDING, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrEjectPending(Packet packet)
        {
            packet.Translator.ReadInt32("Battle Id");
            packet.Translator.ReadBool("Remote");
        }

        [Parser(Opcode.SMSG_ARENA_ERROR)]
        public static void HandleArenaError(Packet packet)
        {
            var error = packet.Translator.ReadUInt32E<ArenaError>("Error");
            if (error == ArenaError.NoTeam)
                packet.Translator.ReadByte("Arena Type"); // 2, 3, 5
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_ROSTER)]
        public static void HandleArenaTeamRoster(Packet packet)
        {
            packet.Translator.ReadUInt32("Team Id");
            var hiddenRating = packet.Translator.ReadBool("Send Hidden Rating");
            var count = packet.Translator.ReadUInt32("Member count");
            packet.Translator.ReadUInt32("Type");

            for (var i = 0; i < count; i++)
            {
                var guid = packet.Translator.ReadGuid("GUID", i);
                packet.Translator.ReadBool("Online", i);
                var name = packet.Translator.ReadCString("Name", i);
                StoreGetters.AddName(guid, name);
                packet.Translator.ReadUInt32("Captain", i);
                packet.Translator.ReadByte("Level", i);
                packet.Translator.ReadByteE<Class>("Class", i);
                packet.Translator.ReadUInt32("Week Games", i);
                packet.Translator.ReadUInt32("Week Win", i);
                packet.Translator.ReadUInt32("Seasonal Games", i);
                packet.Translator.ReadUInt32("Seasonal Wins", i);
                packet.Translator.ReadUInt32("Personal Rating", i);
                if (hiddenRating)
                {
                    // Hidden rating, see LUA GetArenaTeamGdfInfo - gdf = Gaussian Density Filter
                    // Introduced in Patch 3.0.8
                    packet.Translator.ReadSingle("Unk float 1", i);
                    packet.Translator.ReadSingle("Unk float 2", i);
                }
            }
        }

        [Parser(Opcode.CMSG_ARENA_TEAM_ROSTER)]
        [Parser(Opcode.CMSG_ARENA_TEAM_QUERY)]
        public static void HandleArenaTeamQuery(Packet packet)
        {
            packet.Translator.ReadUInt32("Team Id");
        }

        [Parser(Opcode.CMSG_ARENA_TEAM_CREATE)]
        public static void HandleArenaTeamCreate(Packet packet)
        {
            packet.Translator.ReadUInt32("Background Color");
            packet.Translator.ReadUInt32("Icon");
            packet.Translator.ReadUInt32("Icon Color");
            packet.Translator.ReadUInt32("Border");
            packet.Translator.ReadUInt32("Border Color");
            packet.Translator.ReadUInt32("Type");
            packet.Translator.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_ARENA_TEAM_INVITE)]
        [Parser(Opcode.CMSG_ARENA_TEAM_REMOVE)]
        [Parser(Opcode.CMSG_ARENA_TEAM_LEADER)]
        public static void HandleArenaTeamInvite(Packet packet)
        {
            packet.Translator.ReadUInt32("Team Id");
            packet.Translator.ReadCString("Name");
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_COMMAND_RESULT, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleArenaTeamCommandResult(Packet packet)
        {
            packet.Translator.ReadUInt32("Action"); // FIXME: Use enum
            packet.Translator.ReadCString("Team Name");
            packet.Translator.ReadCString("Player Name");
            packet.Translator.ReadUInt32("ErrorId"); // FIXME: Use enum
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_COMMAND_RESULT, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleArenaTeamCommandResult406(Packet packet)
        {
            packet.Translator.ReadUInt32E<ArenaCommandResult>("Result");
            packet.Translator.ReadCString("Team Name");
            packet.Translator.ReadCString("Player Name");
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_EVENT)]
        public static void HandleArenaTeamEvent(Packet packet)
        {
            packet.Translator.ReadByteE<ArenaEvent>("Event");
            var count = packet.Translator.ReadByte("Count");
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadCString("Param", i);

            if (packet.CanRead())
                packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_QUERY_RESPONSE)]
        public static void HandleArenaTeamQueryResponse(Packet packet)
        {
            packet.Translator.ReadUInt32("Team ID");
            packet.Translator.ReadCString("Team Name");
            packet.Translator.ReadUInt32("Type");
            packet.Translator.ReadUInt32("Background Color");
            packet.Translator.ReadUInt32("Emblem Style");
            packet.Translator.ReadUInt32("Emblem Color");
            packet.Translator.ReadUInt32("Border Style");
            packet.Translator.ReadUInt32("Border Color");
        }

        [Parser(Opcode.SMSG_ARENA_OPPONENT_UPDATE)]
        public static void HandleArenaOpponentUpdate(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.MSG_INSPECT_ARENA_TEAMS)]
        public static void HandleInspectArenaTeams(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            if (packet.Direction == Direction.ClientToServer)
                return;

            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadUInt32("Team Id");
            packet.Translator.ReadUInt32("Team Rating");
            packet.Translator.ReadUInt32("Team Season Games");
            packet.Translator.ReadUInt32("Team Season Wins");
            packet.Translator.ReadUInt32("Player Season Games");
            packet.Translator.ReadUInt32("Player Personal Rating");
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_STATS)]
        public static void HandleArenaTeamStats(Packet packet)
        {
            packet.Translator.ReadUInt32("Team Id");
            packet.Translator.ReadUInt32("Rating");
            packet.Translator.ReadUInt32("Week Games");
            packet.Translator.ReadUInt32("Week Win");
            packet.Translator.ReadUInt32("Seasonal Games");
            packet.Translator.ReadUInt32("Seasonal Wins");
            packet.Translator.ReadUInt32("Rank");
        }

        [Parser(Opcode.CMSG_BF_MGR_QUEUE_REQUEST, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattelfieldMgrQueueRequest(Packet packet)
        {
            packet.Translator.ReadUInt32("BattleID");
        }

        [Parser(Opcode.CMSG_REQUEST_RATED_BG_INFO, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_GET_PVP_OPTIONS_ENABLED)]
        [Parser(Opcode.CMSG_BATTLEGROUND_PLAYER_POSITIONS)]
        [Parser(Opcode.SMSG_BATTLEGROUND_INFO_THROTTLED)]
        [Parser(Opcode.SMSG_BATTLEFIELD_PORT_DENIED)]
        public static void HandleNullBattleground(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_REQUEST_RATED_BG_INFO, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleRequestRatedBGInfo434(Packet packet)
        {
            packet.Translator.ReadByte("Unk Byte"); // Arena/BG?
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_POSITIONS, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleBattlegroundPlayerPositions(Packet packet)
        {
            var count1 = packet.Translator.ReadUInt32("Count 1");
            for (var i = 0; i < count1; ++i)
            {
                packet.Translator.ReadGuid("GUID", i);
                packet.Translator.ReadVector2("Position", i);
            }

            var count2 = packet.Translator.ReadUInt32("Count 2");
            for (var i = 0; i < count2; ++i)
            {
                packet.Translator.ReadGuid("GUID", i);
                packet.Translator.ReadVector2("Position", i);
            }
        }

        [Parser(Opcode.SMSG_INSPECT_RATED_BG_STATS)]
        public static void HandleInspectRatedBGStats(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(6, 4, 5, 1, 2, 7, 0, 3);

            packet.Translator.ReadXORByte(guid, 4);

            packet.Translator.ReadInt32("Rating");

            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.ReadInt32("Won");
            packet.Translator.ReadInt32("Played");

            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_RATED_INFO, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldRatedInfo430(Packet packet)
        {
            packet.Translator.ReadUInt32("Unk UInt32 5");
            packet.Translator.ReadUInt32("Current Conquest Points");
            packet.Translator.ReadUInt32("Conquest Points Weekly Cap");
            packet.Translator.ReadByte("Unk Byte");
            packet.Translator.ReadUInt32("Reward");
            packet.Translator.ReadUInt32("Unk UInt32 3");
            packet.Translator.ReadUInt32("Unk UInt32 6");
            packet.Translator.ReadUInt32("Unk UInt32 2");
        }

        [Parser(Opcode.SMSG_RATED_BATTLEFIELD_INFO)]
        public static void HandleRatedBGStats(Packet packet)
        {
            for (var i = 0; i < 18; i++)
                packet.Translator.ReadUInt32("Unk UInt32", i);
        }

        [Parser(Opcode.SMSG_PVP_OPTIONS_ENABLED, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandlePVPOptionsEnabled430(Packet packet)
        {
            packet.Translator.ReadBit("WargamesEnabled");
            packet.Translator.ReadBit("RatedArenasEnabled");
            packet.Translator.ReadBit("RatedBGsEnabled");
            packet.Translator.ReadBit("Unk 1"); // probably debug unused
            packet.Translator.ReadBit("Unk 1"); // probably debug unused
        }

        [Parser(Opcode.SMSG_REQUEST_PVP_REWARDS_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandlePVPRewardsResponse430(Packet packet)
        {
            packet.Translator.ReadUInt32("Conquest Points From Arena");
            packet.Translator.ReadUInt32("Arena Conquest Cap");
            packet.Translator.ReadUInt32("Conquest Points From Rated Bg");
            packet.Translator.ReadUInt32("Conquest Weekly Cap");
            packet.Translator.ReadUInt32("Current Conquest Points");
            packet.Translator.ReadUInt32("Conquest Points This Week");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_REQUEST_SCORE_DATA)]
        [Parser(Opcode.CMSG_QUERY_BATTLEFIELD_STATE)]
        [Parser(Opcode.CMSG_REQUEST_RATED_BG_STATS)]
        [Parser(Opcode.CMSG_PVP_LOG_DATA)]
        [Parser(Opcode.CMSG_REQUEST_PVP_REWARDS)]
        [Parser(Opcode.CMSG_ARENA_TEAM_ACCEPT)]
        [Parser(Opcode.CMSG_ARENA_TEAM_DECLINE)]
        public static void HandleBattlegroundNull(Packet packet)
        {
        }

        //[Parser(Opcode.CMSG_BATTLEFIELD_MANAGER_ADVANCE_STATE)]
        //[Parser(Opcode.CMSG_BATTLEFIELD_MANAGER_SET_NEXT_TRANSITION_TIME)]
        //[Parser(Opcode.CMSG_START_BATTLEFIELD_CHEAT)]
        //[Parser(Opcode.CMSG_END_BATTLEFIELD_CHEAT)]

        [Parser(Opcode.CMSG_ARENA_TEAM_DISBAND)]
        [Parser(Opcode.CMSG_ARENA_TEAM_LEAVE)]
        public static void HandleArenaTeamIdMisc(Packet packet)
        {
            packet.Translator.ReadInt32("Arena Team Id");
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_INVITE)]
        public static void HandleArenaTeamInviteServer(Packet packet)
        {
            packet.Translator.ReadCString("Player Name");
            packet.Translator.ReadCString("Arena Team Name");
        }
    }
}
