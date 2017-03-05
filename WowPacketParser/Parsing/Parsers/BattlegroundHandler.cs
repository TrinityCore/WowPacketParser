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
            packet.ReadInt32<MapId>("Map Id");
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
            packet.ReadInt32<MapId>("Map Id");
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
            var guid = new byte[8];

            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            packet.ReadBit("As Group");
            guid[1] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();

            packet.ReadUInt32("Unknown uint32");

            packet.ParseBitStream(guid, 5, 0, 2, 1, 4, 6, 3, 7);
            packet.WriteGuid("BG Guid", guid);
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_JOIN, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleBattlefieldJoin(Packet packet)
        {
            packet.ReadBit("As Group");
            packet.ReadUInt32("Unk1");
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_LIST)]
        public static void HandleBattlefieldListClient(Packet packet)
        {
            packet.ReadInt32<BgId>("BGType");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                return;

            packet.ReadBool("From UI");
            packet.ReadByte("Unk Byte (BattlefieldList)");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_2_15211)]
        public static void HandleBattlefieldListServer430(Packet packet)
        {
            var guidBytes = new byte[8];

            guidBytes[2] = packet.ReadBit();
            guidBytes[7] = packet.ReadBit();
            packet.ReadBit("UnkBit1");
            var count = packet.ReadBits("BG Instance count", 21);
            guidBytes[0] = packet.ReadBit();
            packet.ReadBit("UnkBit2");
            guidBytes[3] = packet.ReadBit();
            guidBytes[1] = packet.ReadBit();
            packet.ReadBit("UnkBit3");
            guidBytes[5] = packet.ReadBit();
            guidBytes[4] = packet.ReadBit();
            packet.ReadBit("Random Has Win");
            guidBytes[6] = packet.ReadBit();

            packet.ReadXORByte(guidBytes, 4);

            packet.ReadInt32("Loser Honor Reward");
            packet.ReadInt32("Winner Honor Reward");
            for (var i = 0; i < count; i++)
                packet.ReadUInt32("Instance ID", i);

            packet.ReadXORByte(guidBytes, 7);

            packet.ReadInt32<BgId>("BG type");

            packet.ReadXORByte(guidBytes, 1);

            packet.ReadInt32("Random Loser Honor Reward");
            packet.ReadInt32("Random Winner Conquest Reward");

            packet.ReadXORByte(guidBytes, 2);

            packet.ReadByte("Max level");

            packet.ReadXORByte(guidBytes, 0);

            packet.ReadInt32("Winner Conquest Reward");
            packet.ReadByte("Min level");

            packet.ReadXORByte(guidBytes, 5);

            packet.ReadInt32("Random Winner Honor Reward");

            packet.ReadXORByte(guidBytes, 3);
            packet.ReadXORByte(guidBytes, 6);

            packet.WriteGuid("Guid", guidBytes);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleBattlefieldListServer422(Packet packet)
        {
            var guidBytes = new byte[8];

            guidBytes[5] = packet.ReadBit();
            guidBytes[0] = packet.ReadBit();
            guidBytes[4] = packet.ReadBit();
            guidBytes[1] = packet.ReadBit();
            guidBytes[3] = packet.ReadBit();
            guidBytes[6] = packet.ReadBit();
            packet.ReadBit("UnkBit1");
            packet.ReadBit("UnkBit2");
            guidBytes[2] = packet.ReadBit();
            packet.ReadBit("UnkBit3");
            packet.ReadBit("UnkBit4");
            guidBytes[7] = packet.ReadBit();

            packet.ReadInt32("Winner Honor Reward");

            packet.ReadXORByte(guidBytes, 3);
            packet.ReadXORByte(guidBytes, 5);

            packet.ReadInt32("Random Winner Honor Reward");

            packet.ReadXORByte(guidBytes, 0);

            packet.ReadByte("Max level");

            packet.ReadXORByte(guidBytes, 7);
            packet.ReadXORByte(guidBytes, 1);
            packet.ReadXORByte(guidBytes, 2);
            packet.ReadXORByte(guidBytes, 4);

            packet.ReadInt32<BgId>("BGType");
            packet.ReadInt32("Random Winner Conquest Reward");
            packet.ReadInt32("Winner Conquest Reward");

            packet.ReadXORByte(guidBytes, 6);

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
            packet.ReadByteE<BattlegroundListFlags>("Flags");
            packet.ReadByte("Min level");
            packet.ReadInt32("Winner Honor Reward");
            packet.ReadGuid("GUID");
            packet.ReadInt32("Random Winner Honor Reward");
            packet.ReadByte("Max level");
            packet.ReadInt32("Random Loser Honor Reward");
            packet.ReadInt32("Random Winner Conquest Reward");
            packet.ReadInt32("Winner Conquest Reward");
            packet.ReadInt32<BgId>("BGType");

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
                packet.ReadBool("From UI");

            packet.ReadInt32<BgId>("BGType");
            packet.ReadByte("Min Level");
            packet.ReadByte("Max Level");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_3_11685)) // verify if it wasn't earlier or later
            {
                packet.ReadBool("Has Win");
                packet.ReadInt32("Winner Honor Reward");
                packet.ReadInt32("Winner Arena Reward");
                packet.ReadInt32("Loser Honor Reward");

                if (packet.ReadBool("Is random"))
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
            packet.ReadBool("Join BG");
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

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_1_13164)]
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
            packet.ReadBool("Rated");
            var status = packet.ReadUInt32E<BattlegroundStatus>("Status");
            switch (status)
            {
                case BattlegroundStatus.WaitQueue:
                    packet.ReadUInt32("Average Wait Time");
                    packet.ReadUInt32("Time in queue");
                    break;
                case BattlegroundStatus.WaitJoin:
                    packet.ReadInt32<MapId>("Map ID");

                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_5_12213))
                        packet.ReadGuid("GUID");

                    packet.ReadUInt32("Time left");
                    break;
                case BattlegroundStatus.InProgress:
                    packet.ReadInt32<MapId>("Map ID");

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
        [Parser(Opcode.CMSG_REPORT_PVP_PLAYER_AFK)]
        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_LEFT, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleBattlemasterHello(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlegroundPlayerJoined430(Packet packet)
        {
            var guid = packet.StartBitStream(2, 3, 5, 4, 6, 7, 1, 0);
            packet.ParseBitStream(guid, 3, 1, 2, 6, 0, 7, 4, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlemasterJoin(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadInt32<BgId>("BGType");
            packet.ReadUInt32("Instance Id");
            packet.ReadBool("As group");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_ARENA, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlemasterJoinArena(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Slot");
            packet.ReadBool("As group");
            packet.ReadBool("Rated");
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

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_QUEUED, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleRGroupJoinedBattleground(Packet packet)
        {
            var val = packet.ReadInt32();
            if (val < 1)
            {
                var result = packet.AddValue("Result", (BattlegroundError)val);
                if (result == BattlegroundError.JoinFailedAsGroup ||
                    result == BattlegroundError.CouldntJoinQueueInTime)
                    packet.ReadGuid("GUID");
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

            guidBytes[1] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();
            field38[2] = packet.ReadBit();
            field10[2] = packet.ReadBit();
            field14[0] = packet.ReadBit();
            field14[1] = packet.ReadBit();
            guidBytes[6] = packet.ReadBit();
            field10[3] = packet.ReadBit();
            field14[2] = packet.ReadBit();
            field3C[0] = packet.ReadBit();
            field3C[2] = packet.ReadBit();
            guidBytes[4] = packet.ReadBit();
            field3C[3] = packet.ReadBit();
            field38[3] = packet.ReadBit();
            guidBytes[0] = packet.ReadBit();
            guidBytes[3] = packet.ReadBit();
            field3C[1] = packet.ReadBit();
            guidBytes[7] = packet.ReadBit();
            field38[0] = packet.ReadBit();
            guidBytes[5] = packet.ReadBit();
            field10[1] = packet.ReadBit();
            field14[3] = packet.ReadBit();
            field38[1] = packet.ReadBit();
            field10[0] = packet.ReadBit();

            packet.ReadXORByte(guidBytes, 4);

            var bgError = packet.ReadInt32E<BattlegroundError4x>("Battleground Error");

            packet.ReadXORByte(guidBytes, 1);

            packet.ReadXORByte(field10, 1);

            packet.ReadXORByte(guidBytes, 6);

            packet.ReadXORByte(field3C, 2);
            packet.ReadXORByte(field14, 1);
            packet.ReadXORByte(field14, 2);

            packet.ReadUInt32("field18");

            packet.ReadXORByte(field38, 0);
            packet.ReadXORByte(field3C, 1);
            packet.ReadXORByte(field10, 0);

            packet.ReadUInt32("BattlegroundId");

            packet.ReadXORByte(field38, 3);
            packet.ReadXORByte(field3C, 3);

            packet.ReadXORByte(guidBytes, 5);

            packet.ReadXORByte(field10, 2);

            packet.ReadXORByte(guidBytes, 0);
            packet.ReadXORByte(guidBytes, 7);

            packet.ReadXORByte(field14, 3);
            packet.ReadXORByte(field10, 3);
            packet.ReadXORByte(field38, 2);

            packet.ReadXORByte(guidBytes, 2);

            packet.ReadXORByte(field38, 1);

            packet.ReadXORByte(guidBytes, 3);

            packet.ReadXORByte(field14, 0);
            packet.ReadXORByte(field3C, 0);

            packet.ReadUInt32("field1C");

            // note: guid is used to identify the player who's unable to join queue when it happens.

            // on id 0xB, 0xC and 8
            if (bgError == BattlegroundError4x.CouldntJoinQueueInTime
                || bgError == BattlegroundError4x.BattlegroundNotInBattleground
                || bgError == BattlegroundError4x.JoinFailedAsGroup)
            {
                packet.WriteGuid("Guid", guidBytes);
            }
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
            var guidBytes = packet.StartBitStream(0, 1, 4, 3, 6, 2, 7, 5);
            packet.ParseBitStream(guidBytes, 2, 6, 3, 4, 5, 7, 1, 0);
            packet.WriteGuid("Guid", guidBytes);
        }

        [Parser(Opcode.MSG_PVP_LOG_DATA, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandlePvPLogData(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                return;

            var arena = packet.ReadBool("Arena");
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

            var finished = packet.ReadBool("Finished");
            if (finished)
                packet.ReadByte("Winner");

            var count = packet.ReadUInt32("Score Count");
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

            var arenaScores = packet.ReadBit("Has Arena Scores");
            var arenaStrings = packet.ReadBit("Has Arena Strings");
            var finished = packet.ReadBit("Is Finished");

            if (arenaStrings)
                for (var i = 0; i < 2; ++i)
                    packet.ReadCString("Name", i);

            if (arenaScores)
            {
                for (var i = 0; i < 2; ++i)
                {
                    packet.ReadUInt32("Points Lost", i);
                    packet.ReadUInt32("Points Gained", i);
                    packet.ReadUInt32("Matchmaker Rating", i);
                }
            }

            var scoreCount = packet.ReadInt32("Score Count");

            if (finished)
                packet.ReadByte("Team Winner");

            var bits = new Bit[scoreCount, 4];

            for (var i = 0; i < scoreCount; ++i)
            {
                bits[i, 0] = packet.ReadBit("Unk Bit 1", i); //  sets *(v23 + v18 + 40)
                bits[i, 1] = packet.ReadBit("Is Battleground", i); //  sets *(v27 + v18 + 48)
                bits[i, 2] = packet.ReadBit("Unk Bit 3", i); // sets *(v2->dword1C + v18 + 4)
                bits[i, 3] = packet.ReadBit("Unk Bit 4", i); // sets *(v32 + v18 + 68)
            }

            for (var i = 0; i < scoreCount; ++i)
            {
                packet.ReadInt32("Damage Done", i);

                if (bits[i, 0])
                    packet.ReadInt32("Unk Int32 1", i);

                var count = packet.ReadInt32("Extra values counter", i);

                if (bits[i, 1])
                {
                    packet.ReadUInt32("Honorable Kills", i);
                    packet.ReadUInt32("Deaths", i);
                    packet.ReadUInt32("Bonus Honor", i);
                }

                packet.ReadGuid("Player GUID", i);
                packet.ReadInt32("Killing Blows");
                for (var j = 0; j < count; ++j)
                    packet.ReadInt32("Extra Value", i, j);

                if (bits[i, 3])
                    packet.ReadInt32("Unk Int32 2", i);

                packet.ReadInt32("Healing Done", i);
            }
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_STATE_CHANGE, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrStateChanged406(Packet packet)
        {
            packet.ReadUInt32E<BattlegroundStatus>("status");
            packet.ReadGuid("BG Guid");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_STATE_CHANGE, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleBattlefieldMgrStateChanged(Packet packet)
        {
            packet.ReadUInt32E<BattlegroundStatus>("Old status");
            packet.ReadUInt32E<BattlegroundStatus>("New status");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTRY_INVITE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrInviteSend(Packet packet)
        {
            packet.ReadInt32("BattleID");
            packet.ReadInt32<ZoneId>("ZoneID");
            packet.ReadTime("ExpireTime");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_INVITE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrQueueInvite(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadByte("Warmup");
        }

        [Parser(Opcode.CMSG_BF_MGR_QUEUE_INVITE_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrQueueInviteResponse(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadBool("Accepted");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_REQUEST_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrQueueRequestResponse(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadInt32<ZoneId>("Zone ID");
            packet.ReadByte("Accepted");
            packet.ReadByte("Logging In");
            packet.ReadByte("Warmup");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTERING, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrEntered406(Packet packet)
        {
            packet.ReadByte("Unk");
            packet.ReadGuid("BG Guid");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTERING, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
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

        [Parser(Opcode.CMSG_BF_MGR_ENTRY_INVITE_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrEntryInviteResponse(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadBool("Accepted");
        }

        [Parser(Opcode.CMSG_BF_MGR_QUEUE_EXIT_REQUEST, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrExitRequest(Packet packet)
        {
            packet.ReadInt32("Battle Id");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_EJECT_PENDING, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrEjectPending(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadBool("Remote");
        }

        [Parser(Opcode.SMSG_ARENA_ERROR)]
        public static void HandleArenaError(Packet packet)
        {
            var error = packet.ReadUInt32E<ArenaError>("Error");
            if (error == ArenaError.NoTeam)
                packet.ReadByte("Arena Type"); // 2, 3, 5
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_ROSTER)]
        public static void HandleArenaTeamRoster(Packet packet)
        {
            packet.ReadUInt32("Team Id");
            var hiddenRating = packet.ReadBool("Send Hidden Rating");
            var count = packet.ReadUInt32("Member count");
            packet.ReadUInt32("Type");

            for (var i = 0; i < count; i++)
            {
                var guid = packet.ReadGuid("GUID", i);
                packet.ReadBool("Online", i);
                var name = packet.ReadCString("Name", i);
                StoreGetters.AddName(guid, name);
                packet.ReadUInt32("Captain", i);
                packet.ReadByte("Level", i);
                packet.ReadByteE<Class>("Class", i);
                packet.ReadUInt32("Week Games", i);
                packet.ReadUInt32("Week Win", i);
                packet.ReadUInt32("Seasonal Games", i);
                packet.ReadUInt32("Seasonal Wins", i);
                packet.ReadUInt32("Personal Rating", i);
                if (hiddenRating)
                {
                    // Hidden rating, see LUA GetArenaTeamGdfInfo - gdf = Gaussian Density Filter
                    // Introduced in Patch 3.0.8
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

        [Parser(Opcode.SMSG_ARENA_TEAM_COMMAND_RESULT, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleArenaTeamCommandResult406(Packet packet)
        {
            packet.ReadUInt32E<ArenaCommandResult>("Result");
            packet.ReadCString("Team Name");
            packet.ReadCString("Player Name");
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_EVENT)]
        public static void HandleArenaTeamEvent(Packet packet)
        {
            packet.ReadByteE<ArenaEvent>("Event");
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

        [Parser(Opcode.CMSG_BF_MGR_QUEUE_REQUEST, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattelfieldMgrQueueRequest(Packet packet)
        {
            packet.ReadUInt32("BattleID");
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
            packet.ReadByte("Unk Byte"); // Arena/BG?
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_POSITIONS, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V4_3_0_15005)]
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
            var guid = packet.StartBitStream(6, 4, 5, 1, 2, 7, 0, 3);

            packet.ReadXORByte(guid, 4);

            packet.ReadInt32("Rating");

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);

            packet.ReadInt32("Won");
            packet.ReadInt32("Played");

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_RATED_INFO, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldRatedInfo430(Packet packet)
        {
            packet.ReadUInt32("Unk UInt32 5");
            packet.ReadUInt32("Current Conquest Points");
            packet.ReadUInt32("Conquest Points Weekly Cap");
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32("Reward");
            packet.ReadUInt32("Unk UInt32 3");
            packet.ReadUInt32("Unk UInt32 6");
            packet.ReadUInt32("Unk UInt32 2");
        }

        [Parser(Opcode.SMSG_RATED_BATTLEFIELD_INFO)]
        public static void HandleRatedBGStats(Packet packet)
        {
            for (var i = 0; i < 18; i++)
                packet.ReadUInt32("Unk UInt32", i);
        }

        [Parser(Opcode.SMSG_PVP_OPTIONS_ENABLED, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandlePVPOptionsEnabled430(Packet packet)
        {
            packet.ReadBit("WargamesEnabled");
            packet.ReadBit("RatedArenasEnabled");
            packet.ReadBit("RatedBGsEnabled");
            packet.ReadBit("Unk 1"); // probably debug unused
            packet.ReadBit("Unk 1"); // probably debug unused
        }

        [Parser(Opcode.SMSG_REQUEST_PVP_REWARDS_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandlePVPRewardsResponse430(Packet packet)
        {
            packet.ReadUInt32("Conquest Points From Arena");
            packet.ReadUInt32("Arena Conquest Cap");
            packet.ReadUInt32("Conquest Points From Rated Bg");
            packet.ReadUInt32("Conquest Weekly Cap");
            packet.ReadUInt32("Current Conquest Points");
            packet.ReadUInt32("Conquest Points This Week");
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
            packet.ReadInt32("Arena Team Id");
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_INVITE)]
        public static void HandleArenaTeamInviteServer(Packet packet)
        {
            packet.ReadCString("Player Name");
            packet.ReadCString("Arena Team Name");
        }
    }
}
