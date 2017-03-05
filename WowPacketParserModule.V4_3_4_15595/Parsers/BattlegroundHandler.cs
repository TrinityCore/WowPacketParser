using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_3_4_15595.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.SMSG_BATTLEFIELD_LIST)]
        public static void HandleBattlefieldListServer434(Packet packet)
        {
            packet.ReadInt32("Holiday First Win Arena Currency Bonus");
            packet.ReadInt32("Random First Win Arena Currency Bonus");
            packet.ReadInt32("Holiday Loss Honor Currency Bonus");
            packet.ReadInt32<BgId>("BG type");
            packet.ReadInt32("Random Loss Honor Currency Bonus");
            packet.ReadInt32("Random Win Honor Currency Bonus");
            packet.ReadInt32("Holiday Win Honor Currency Bonus");
            packet.ReadByte("Max level");
            packet.ReadByte("Min level");

            var guidBytes = new byte[8];

            guidBytes[0] = packet.ReadBit();
            guidBytes[1] = packet.ReadBit();
            guidBytes[7] = packet.ReadBit();
            packet.ReadBit("Has Won Holiday Today");
            packet.ReadBit("Has Won Random Today");
            var count = packet.ReadBits("BG Instance count", 24); // Max 64
            guidBytes[6] = packet.ReadBit();
            guidBytes[4] = packet.ReadBit();
            guidBytes[2] = packet.ReadBit();
            guidBytes[3] = packet.ReadBit();
            packet.ReadBit("Unk Bit"); // Unk bit 1
            guidBytes[5] = packet.ReadBit();
            packet.ReadBit("Trigger PVPQUEUE_ANYWHERE_SHOW");

            packet.ReadXORByte(guidBytes, 6);
            packet.ReadXORByte(guidBytes, 1);
            packet.ReadXORByte(guidBytes, 7);
            packet.ReadXORByte(guidBytes, 5);

            for (var i = 0; i < count; i++)
                packet.ReadUInt32("BG Instance ID", i);

            packet.ReadXORByte(guidBytes, 0);
            packet.ReadXORByte(guidBytes, 2);
            packet.ReadXORByte(guidBytes, 4);
            packet.ReadXORByte(guidBytes, 3);

            packet.WriteGuid("Guid", guidBytes);
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED)]
        public static void HandleBattlegroundPlayerJoined434(Packet packet)
        {
            var guid = packet.StartBitStream(0, 4, 3, 5, 7, 6, 2, 1);
            packet.ParseBitStream(guid, 1, 5, 3, 2, 0, 7, 4, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_LEFT)]
        public static void HandleBattleGroundPlayerLeft434(Packet packet)
        {
            var guid = packet.StartBitStream(7, 6, 2, 4, 5, 1, 3, 0);
            packet.ParseBitStream(guid, 4, 2, 5, 7, 0, 6, 1, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN)]
        public static void HandleBattlemasterJoin434(Packet packet)
        {
            packet.ReadUInt32("Instance Id");
            var guid = new byte[8];
            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            packet.ReadBit("As Group");
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[7] = packet.ReadBit();

            packet.ParseBitStream(guid, 2, 6, 4, 3, 7, 0, 5, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS)]
        public static void HandleBattlefieldStatus434(Packet packet)
        {
            var playerGuid = packet.StartBitStream(0, 4, 7, 1, 6, 3, 5, 2);

            packet.ReadXORByte(playerGuid, 5);
            packet.ReadXORByte(playerGuid, 6);
            packet.ReadXORByte(playerGuid, 7);
            packet.ReadXORByte(playerGuid, 2);
            packet.ReadInt32("Join Type");
            packet.ReadXORByte(playerGuid, 3);
            packet.ReadXORByte(playerGuid, 1);
            packet.ReadInt32("Queue Slot");
            packet.ReadInt32("Join Time");
            packet.ReadXORByte(playerGuid, 0);
            packet.ReadXORByte(playerGuid, 4);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_QUEUED)]
        public static void HandleRGroupJoinedBattleground434(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid1[3] = packet.ReadBit();//35
            guid1[0] = packet.ReadBit();//32
            guid2[3] = packet.ReadBit();//59
            guid1[2] = packet.ReadBit();//34
            packet.ReadBit("Eligible In Queue");//21
            packet.ReadBit("Join Failed"); // if true, & statuscode != 0 && bg entry exists, display name in error.
            guid2[2] = packet.ReadBit();//58
            guid1[1] = packet.ReadBit();//33

            guid2[0] = packet.ReadBit();//56
            guid2[6] = packet.ReadBit();//62
            guid2[4] = packet.ReadBit();//60
            guid1[6] = packet.ReadBit();//38
            guid1[7] = packet.ReadBit();//39
            guid2[7] = packet.ReadBit();//63
            guid2[5] = packet.ReadBit();//61
            guid1[4] = packet.ReadBit();//36

            guid1[5] = packet.ReadBit();//37
            packet.ReadBit("Is Rated");//72
            packet.ReadBit("Waiting On Other Activity");//28
            guid2[1] = packet.ReadBit();//57

            packet.ReadXORByte(guid1, 0);

            packet.ReadInt32("Unk Int32");

            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 3);

            packet.ReadInt32("Estimated Wait Time");

            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 2);

            packet.ReadByte("Unk Byte");

            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 2);

            packet.ReadByte("Unk Byte");

            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 0);

            packet.ReadTime("Time");
            packet.ReadInt32("QueueSlot");
            packet.ReadByte("Min Level");
            packet.ReadInt32("Battlefield join time");

            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 5);

            packet.ReadInt32("Unk Int32");

            packet.ReadXORByte(guid1, 4);

            packet.WriteGuid("Player Guid", guid1);
            packet.WriteGuid("BG Guid", guid2);
        }

        [Parser(Opcode.SMSG_PVP_LOG_DATA)]
        public static void HandlePvPLogData434(Packet packet)
        {
            var arena = packet.ReadBit("Is Arena");
            var rated = packet.ReadBit("Is Rated");
            var name1Length = 0u;
            var name2Length = 0u;

            if (arena)
            {
                name1Length = packet.ReadBits(8);
                name2Length = packet.ReadBits(8);
            }

            var count = packet.ReadBits("Score Count", 21);

            var guids = new byte[count][];
            var valuesCount = new uint[count];
            var hasBgRatings = new bool[count]; // 2
            var hasBgScores = new bool[count]; // 3
            var hasPremadeMMR = new bool[count]; // 4
            var hasRatingChange = new bool[count]; // 5
            var hasMmrChange = new bool[count]; // 5

            for (int i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];

                packet.ReadBit(); // Unused
                hasBgRatings[i] = packet.ReadBit("Has BG Rating", i);

                guids[i][2] = packet.ReadBit();

                hasBgScores[i] = packet.ReadBit("Has BG Scores", i);
                hasPremadeMMR[i] = packet.ReadBit("Has premade MMR data", i);
                hasRatingChange[i] = packet.ReadBit("Has Rating change", i);
                hasMmrChange[i] = packet.ReadBit("Has MMR Change", i);

                guids[i][3] = packet.ReadBit();
                guids[i][0] = packet.ReadBit();
                guids[i][5] = packet.ReadBit();
                guids[i][1] = packet.ReadBit();
                guids[i][6] = packet.ReadBit();

                packet.ReadBit("Is " + (arena ? "Gold" : "Alliance") + " team member", i); // 12

                guids[i][7] = packet.ReadBit();

                valuesCount[i] = packet.ReadBits("Value Count", 24, i);

                guids[i][4] = packet.ReadBit();
            }

            var finished = packet.ReadBit("Finished");

            if (rated)
            {
                packet.ReadInt32("[0] Arena MMR");
                packet.ReadInt32("[0] Arena Points Lost");
                packet.ReadInt32("[0] Arena Points Won");
                packet.ReadInt32("[1] Arena MMR");
                packet.ReadInt32("[1] Arena Points Lost");
                packet.ReadInt32("[1] Arena Points Won");
            }

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32("Healing done", i);
                packet.ReadInt32("Damage done", i);

                if (hasBgScores[i])
                {
                    packet.ReadInt32("Bonus Honor", i);
                    packet.ReadInt32("Deaths", i);
                    packet.ReadInt32("Honorable Kills", i);
                }

                packet.ReadXORByte(guids[i], 4);

                packet.ReadInt32("Killing Blows", i);

                if (hasRatingChange[i])
                    packet.ReadInt32("Rating Change", i);

                packet.ReadXORByte(guids[i], 5);

                if (hasMmrChange[i])
                    packet.ReadInt32("MMR Change", i);

                if (hasBgScores[i])
                    packet.ReadInt32("Battleground rating", i);

                packet.ReadXORByte(guids[i], 1);
                packet.ReadXORByte(guids[i], 6);

                packet.ReadInt32("Talent tree", i);

                for (int j = 0; j < valuesCount[i]; ++j)
                    packet.ReadUInt32("Value", i, j);

                packet.ReadXORByte(guids[i], 0);
                packet.ReadXORByte(guids[i], 3);

                if (hasPremadeMMR[i])
                    packet.ReadInt32("Premade MMR", i);

                packet.ReadXORByte(guids[i], 7);
                packet.ReadXORByte(guids[i], 2);

                packet.WriteGuid("Player GUID", guids[i], i);
            }

            if (arena)
            {
                packet.ReadWoWString("Team 2 Name", name1Length);
                packet.ReadWoWString("Team 1 Name", name2Length);
            }

            packet.ReadByte((arena ? "Gold" : "Alliance") + " team size");

            if (finished)
                packet.ReadByte("Winner");

            packet.ReadByte((arena ? "Green" : "Horde") + " team size");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_STATE_CHANGE)]
        public static void HandleBattlefieldMgrStateChanged434(Packet packet)
        {
            var guid = packet.StartBitStream(4, 3, 7, 2, 1, 6, 0, 4);

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadUInt32E<BattlegroundStatus>("Status");
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTRY_INVITE)]
        public static void HandleBattlefieldMgrInviteSend434(Packet packet)
        {
            var guid = packet.StartBitStream(5, 3, 7, 2, 6, 4, 1, 0);

            packet.ReadXORByte(guid, 6);
            packet.ReadInt32<ZoneId>("ZoneID");
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadTime("ExpireTime");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_INVITE)]
        public static void HandleBattlefieldMgrQueueInvite434(Packet packet)
        {
            var guid = new byte[8];

            var v6 = !packet.ReadBit("Unk Bit");
            var hasWarmup = !packet.ReadBit("Has Warmup");
            var v10 = !packet.ReadBit("Unk Bit3");
            guid[0] = packet.ReadBit();
            var v8 = !packet.ReadBit("Unk Bit4");
            guid[2] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();

            var v7 = !packet.ReadBit("Unk Bit5");
            packet.ReadBit("Unk Bit6");
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var v11 = !packet.ReadBit("Unk Bit7");
            guid[7] = packet.ReadBit();

            packet.ReadXORByte(guid, 2);

            if (v10)
                packet.ReadInt32("Unk Int 32");

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);

            if (hasWarmup)
                packet.ReadByte("Warmup");

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);

            if (v6)
                packet.ReadInt32("Unk Int 32");

            if (v11)
                packet.ReadInt32("Unk Int 32");

            if (v8)
                packet.ReadInt32("Unk Int 32");

            packet.ReadXORByte(guid, 4);

            if (v7)
                packet.ReadInt32("Unk Int 32");

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_BF_MGR_QUEUE_INVITE_RESPONSE)]
        public static void HandleBattlefieldMgrQueueInviteResponse434(Packet packet)
        {
            var guid = new byte[8];
            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            packet.ReadBit("Accepted");
            guid[1] = packet.ReadBit();
            guid[6] = packet.ReadBit();

            packet.ParseBitStream(guid, 1, 3, 2, 4, 6, 7, 0, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_REQUEST_RESPONSE)]
        public static void HandleBattlefieldMgrQueueRequestResponse434(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid[1] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            packet.ReadBit("Logging In");
            guid[0] = packet.ReadBit();
            var hasSecondGuid = !packet.ReadBit("Has Second Guid");
            guid[4] = packet.ReadBit();

            if (hasSecondGuid)
                guid2 = packet.StartBitStream(7, 3, 0, 4, 2, 6, 1, 5);

            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            if (hasSecondGuid)
                packet.ParseBitStream(guid2, 2, 5, 3, 0, 4, 6, 1, 7);

            packet.ReadBool("Accepted");

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);

            packet.ReadBool("Warmup");

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);

            packet.ReadInt32<ZoneId>("Zone ID");

            packet.WriteGuid("BG Guid", guid);
            packet.WriteGuid("guid2", guid2);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTERING)]
        public static void HandleBattlefieldMgrEntered434(Packet packet)
        {
            var guid = new byte[8];
            packet.ReadBit("Unk Bit1");
            packet.ReadBit("Clear AFK");
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            packet.ReadBit("Unk Bit 3");

            guid[6] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            packet.ParseBitStream(guid, 5, 3, 0, 4, 1, 7, 2, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_EJECTED)]
        public static void HandleBattlefieldMgrEjected434(Packet packet)
        {
            var bytes = new byte[8];
            bytes[2] = packet.ReadBit();
            bytes[5] = packet.ReadBit();
            bytes[1] = packet.ReadBit();
            bytes[0] = packet.ReadBit();
            bytes[3] = packet.ReadBit();
            bytes[6] = packet.ReadBit();
            packet.ReadBit("Relocated");
            bytes[7] = packet.ReadBit();
            bytes[4] = packet.ReadBit();

            packet.ReadByte("Battle Status");

            packet.ReadXORByte(bytes, 1);
            packet.ReadXORByte(bytes, 7);
            packet.ReadXORByte(bytes, 4);
            packet.ReadXORByte(bytes, 2);
            packet.ReadXORByte(bytes, 3);
            packet.ReadByte("Reason");
            packet.ReadXORByte(bytes, 6);
            packet.ReadXORByte(bytes, 0);
            packet.ReadXORByte(bytes, 5);

            packet.WriteGuid("Guid", bytes);
        }

        [Parser(Opcode.CMSG_BF_MGR_ENTRY_INVITE_RESPONSE)]
        public static void HandleBattlefieldMgrEntryInviteResponse434(Packet packet)
        {
            var guid = new byte[8];
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            packet.ReadBit("Accepted");
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();


            packet.ParseBitStream(guid, 0, 3, 4, 2, 1, 6, 7, 5);
            packet.WriteGuid("Guid", guid);
        }


        [Parser(Opcode.CMSG_BF_MGR_QUEUE_EXIT_REQUEST)]
        public static void HandleBattlefieldMgrExitRequest434(Packet packet)
        {
            var guid = packet.StartBitStream(2, 0, 3, 7, 4, 5, 6, 1);
            packet.ParseBitStream(guid, 5, 2, 0, 1, 4, 3, 7, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_EJECT_PENDING)]
        public static void HandleBattlefieldMgrEjectPending434(Packet packet)
        {
            var guid = new byte[8];

            guid[6] = packet.ReadBit();
            packet.ReadBit("Remove");
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_ARENA_TEAM_COMMAND_RESULT)]
        public static void HandleArenaTeamCommandResult434(Packet packet)
        {
            var playerLength = packet.ReadBits(7);
            var teamLength = packet.ReadBits(8);
            packet.ReadWoWString("Player Name", playerLength);
            packet.ReadUInt32("Action");
            packet.ReadUInt32("ErrorId");
            packet.ReadWoWString("Team Name", teamLength);
        }

        [Parser(Opcode.CMSG_ARENA_TEAM_CREATE)]
        public static void HandleArenaTeamCreate434(Packet packet)
        {
            packet.ReadUInt32("Slot");
            packet.ReadUInt32("Icon Color");
            packet.ReadUInt32("Border Color");
            packet.ReadUInt32("Border");
            packet.ReadUInt32("Background Color");
            packet.ReadUInt32("Icon");
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.CMSG_BF_MGR_QUEUE_REQUEST)]
        public static void HandleBattelfieldMgrQueueRequest434(Packet packet)
        {
            var guid = packet.StartBitStream(0, 3, 7, 4, 6, 2, 1, 5);
            packet.ParseBitStream(guid, 6, 3, 2, 4, 7, 1, 5, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_REQUEST_INSPECT_RATED_BG_STATS)]
        public static void HandleRequestInspectRBGStats(Packet packet)
        {
            var guid = packet.StartBitStream(1, 4, 6, 5, 0, 2, 7, 3);
            packet.ParseBitStream(guid, 4, 7, 2, 5, 6, 3, 0, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_RATED_BATTLEFIELD_INFO)]
        public static void HandleRatedBGStats(Packet packet)
        {
            packet.ReadUInt32("BgWeeklyWins20vs20");
            packet.ReadUInt32("BgWeeklyPlayed20vs20");
            packet.ReadUInt32("BgWeeklyPlayed15vs15");
            packet.ReadUInt32("Unk UInt32 (3)");
            packet.ReadUInt32("BgWeeklyWins10vs10");
            packet.ReadUInt32("Unk UInt32 (3)");
            packet.ReadUInt32("Unk UInt32 (4)");
            packet.ReadUInt32("Unk UInt32");
            packet.ReadUInt32("BgWeeklyWins15vs15");
            packet.ReadUInt32("Unk UInt32");
            packet.ReadUInt32("Unk UInt32 (4)");
            packet.ReadUInt32("Unk UInt32");
            packet.ReadUInt32("Unk UInt32 (3)");
            packet.ReadUInt32("Unk UInt32 (4)");
            packet.ReadUInt32("Unk UInt32");
            packet.ReadUInt32("BgWeeklyPlayed10vs10");
            packet.ReadUInt32("Unk UInt32");
            packet.ReadUInt32("Unk UInt32");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_RATED_INFO)]
        public static void HandleBattlefieldRatedInfo(Packet packet)
        {
            packet.ReadUInt32("Reward");
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32("Unk UInt32 2");
            packet.ReadUInt32("Unk UInt32 3");
            packet.ReadUInt32("Conquest Points Weekly Cap");
            packet.ReadUInt32("Unk UInt32 5");
            packet.ReadUInt32("Unk UInt32 6");
            packet.ReadUInt32("Current Conquest Points");
        }

        [Parser(Opcode.SMSG_PVP_OPTIONS_ENABLED)]
        public static void HandlePVPOptionsEnabled434(Packet packet)
        {
            packet.ReadBit("Unk 1"); // probably debug unused
            packet.ReadBit("WargamesEnabled");
            packet.ReadBit("Unk 1"); // probably debug unused
            packet.ReadBit("RatedBGsEnabled");
            packet.ReadBit("RatedArenasEnabled");
        }

        [Parser(Opcode.SMSG_REQUEST_PVP_REWARDS_RESPONSE)]
        public static void HandlePVPRewardsResponse434(Packet packet)
        {
            packet.ReadUInt32("Conquest Weekly Cap");
            packet.ReadUInt32("Conquest Points This Week");
            packet.ReadUInt32("Arena Conquest Cap");
            packet.ReadUInt32("Conquest Points From Rated Bg");
            packet.ReadUInt32("Conquest Points From Arena");
            packet.ReadUInt32("Current Conquest Points");
        }

        [Parser(Opcode.SMSG_WARGAME_REQUEST_SENT)]
        public static void HandleWargameRequestSent434(Packet packet)
        {
            var guid = packet.StartBitStream(7, 6, 0, 3, 5, 2, 1, 4); // Either 5, 2 or 2, 5, tricky tricky brain tickles
            packet.ParseBitStream(guid, 5, 6, 3, 0, 7, 4, 2, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_WARGAME_CHECK_ENTRY)]
        public static void HandleWargameCheckEntry434(Packet packet)
        {
            var challengerGuid = new byte[8];
            var battlefieldGuid = new byte[8];

            challengerGuid[1] = packet.ReadBit();
            challengerGuid[2] = packet.ReadBit();
            battlefieldGuid[7] = packet.ReadBit();
            battlefieldGuid[4] = packet.ReadBit();
            challengerGuid[4] = packet.ReadBit();
            battlefieldGuid[1] = packet.ReadBit();
            challengerGuid[5] = packet.ReadBit();
            battlefieldGuid[5] = packet.ReadBit();
            challengerGuid[7] = packet.ReadBit();
            battlefieldGuid[6] = packet.ReadBit();
            battlefieldGuid[3] = packet.ReadBit();
            challengerGuid[0] = packet.ReadBit();
            challengerGuid[3] = packet.ReadBit();
            battlefieldGuid[2] = packet.ReadBit();
            challengerGuid[6] = packet.ReadBit();
            battlefieldGuid[0] = packet.ReadBit();

            packet.ReadXORByte(battlefieldGuid, 2);

            packet.ReadUInt32("Battlefield TypeId");

            packet.ReadXORByte(challengerGuid, 0);
            packet.ReadXORByte(challengerGuid, 2);
            packet.ReadXORByte(challengerGuid, 4);
            packet.ReadXORByte(challengerGuid, 6);
            packet.ReadXORByte(battlefieldGuid, 0);
            packet.ReadXORByte(challengerGuid, 5);
            packet.ReadXORByte(challengerGuid, 7);
            packet.ReadXORByte(battlefieldGuid, 3);
            packet.ReadXORByte(battlefieldGuid, 5);
            packet.ReadXORByte(challengerGuid, 1);
            packet.ReadXORByte(battlefieldGuid, 7);
            packet.ReadXORByte(battlefieldGuid, 4);
            packet.ReadXORByte(battlefieldGuid, 1);
            packet.ReadXORByte(battlefieldGuid, 6);
            packet.ReadXORByte(challengerGuid, 3);

            packet.WriteGuid("Challenger GUID", challengerGuid);
            packet.WriteGuid("Battlefield GUID", battlefieldGuid);
        }

        [Parser(Opcode.CMSG_WARGAME_START)]
        public static void HandleWargameStart434(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[0] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[2] = packet.ReadBit();

            guid1[6] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[6] = packet.ReadBit();

            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 4);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_PORT)]
        public static void HandleBattlefieldPort434(Packet packet)
        {
            packet.ReadTime("Time");
            packet.ReadUInt32("Queue Slot");
            packet.ReadInt32<BgId>("BGType");

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
            guid1[3] = packet.ReadBit();
            guid3[3] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid3[0] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[4] = packet.ReadBit();

            guid2[2] = packet.ReadBit();
            guid3[1] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid3[5] = packet.ReadBit();
            guid3[6] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid3[4] = packet.ReadBit();

            guid1[2] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid3[7] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid3[2] = packet.ReadBit();
            guid2[7] = packet.ReadBit();

            packet.ReadXORByte(guid1, 1);

            packet.ReadUInt32E<BattlegroundError4x>("Battleground Error");
            packet.ReadUInt32("Queue slot");

            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid3, 6);
            packet.ReadXORByte(guid3, 3);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid3, 0);
            packet.ReadXORByte(guid3, 1);
            packet.ReadXORByte(guid3, 4);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid3, 7);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 2);

            packet.ReadInt32<BgId>("BGType");

            packet.ReadXORByte(guid3, 2);

            packet.ReadTime("Join Time");

            packet.ReadXORByte(guid3, 5);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
            packet.WriteGuid("Guid3", guid3);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_ACTIVE)]
        public static void HandleBattlefieldStatusActive(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];
            guid1[2] = packet.ReadBit();//26
            guid1[7] = packet.ReadBit();//31
            guid2[7] = packet.ReadBit();//55
            guid2[1] = packet.ReadBit();//49
            guid1[5] = packet.ReadBit();//29
            packet.ReadBit("Battlefield Faction ( 0 horde, 1 alliance )");//76
            guid2[0] = packet.ReadBit();//48
            guid1[1] = packet.ReadBit();//25

            guid2[3] = packet.ReadBit();//51
            guid1[6] = packet.ReadBit();//30
            guid2[5] = packet.ReadBit();//53
            packet.ReadBit("Is Rated"); //64
            guid1[4] = packet.ReadBit();//28
            guid2[6] = packet.ReadBit();//54
            guid2[4] = packet.ReadBit();//52
            guid2[2] = packet.ReadBit();//50

            guid1[3] = packet.ReadBit();//27
            guid1[0] = packet.ReadBit();//24

            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 6);

            packet.ReadTime("Time");
            packet.ReadByte("Teamsize");

            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 1);

            packet.ReadUInt32("Queue slot");
            packet.ReadByte("Max Level");
            packet.ReadUInt32E<BattlegroundStatus>("Status");
            packet.ReadInt32<MapId>("Map Id");
            packet.ReadByte("Min Level");
            packet.ReadUInt32("Elapsed Time");

            packet.ReadXORByte(guid1, 2);

            packet.ReadUInt32("Time To Close");

            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 2);

            packet.ReadUInt32("Client Instance ID");

            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 7);

            packet.WriteGuid("Player Guid", guid1);
            packet.WriteGuid("BG Guid", guid2);

        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_NEED_CONFIRMATION)]
        public static void HandleBattlefieldStatusNeedConfirmation(Packet packet)
        {

            packet.ReadUInt32("Client Instance ID");
            packet.ReadUInt32("Time until closed");
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32("Queue slot");
            packet.ReadTime("Time");
            packet.ReadByte("Min Level");
            packet.ReadUInt32E<BattlegroundStatus>("Status");
            packet.ReadInt32<MapId>("Map Id");
            packet.ReadByte("Unk Byte");

            var guid1 = new byte[8];
            var guid2 = new byte[8];
            guid1[5] = packet.ReadBit();//29
            guid1[2] = packet.ReadBit();//26
            guid1[1] = packet.ReadBit();//25
            guid2[2] = packet.ReadBit();//50
            guid1[4] = packet.ReadBit();//28
            guid2[6] = packet.ReadBit();//54
            guid2[3] = packet.ReadBit();//51
            packet.ReadBit("IsRated");
            guid1[7] = packet.ReadBit();//31
            guid1[3] = packet.ReadBit();//27
            guid2[7] = packet.ReadBit();//55
            guid2[0] = packet.ReadBit();//48
            guid2[4] = packet.ReadBit();//52
            guid1[6] = packet.ReadBit();//30
            guid2[1] = packet.ReadBit();//49
            guid2[5] = packet.ReadBit();//53
            guid1[0] = packet.ReadBit();//24

            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 3);

            packet.WriteGuid("Player Guid", guid1);
            packet.WriteGuid("BG Guid", guid2);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_WAIT_FOR_GROUPS)]
        public static void HandleBattlefieldStatusWaitForGroups(Packet packet)
        {
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32E<BattlegroundStatus>("Status");
            packet.ReadUInt32("Queue slot");
            packet.ReadUInt32("Time until closed");
            packet.ReadUInt32("Unk Int32");
            packet.ReadByte("Unk Byte");
            packet.ReadByte("Unk Byte");
            packet.ReadByte("Min Level");
            packet.ReadByte("Unk Byte");
            packet.ReadByte("Unk Byte");
            packet.ReadInt32<MapId>("Map Id");
            packet.ReadTime("Time");
            packet.ReadByte("Unk Byte");
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[0] = packet.ReadBit();//40
            guid2[1] = packet.ReadBit();//41
            guid2[7] = packet.ReadBit();//47
            guid1[7] = packet.ReadBit();//23
            guid1[0] = packet.ReadBit();//16
            guid2[4] = packet.ReadBit();//44
            guid1[6] = packet.ReadBit();//22
            guid1[2] = packet.ReadBit();//18

            guid1[3] = packet.ReadBit();//19
            guid2[3] = packet.ReadBit();//43
            guid1[4] = packet.ReadBit();//20
            guid2[5] = packet.ReadBit();//45
            guid1[5] = packet.ReadBit();//21
            guid2[2] = packet.ReadBit();//42
            packet.ReadBit("IsRated");//56
            guid1[1] = packet.ReadBit();//17
            guid2[6] = packet.ReadBit();//46

            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 3);

            packet.WriteGuid("Player Guid", guid1);
            packet.WriteGuid("BG Guid", guid2);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_PLAYER_POSITIONS)]
        public static void HandleBattlefieldPlayerPositions(Packet packet)
        {
            var count1 = packet.ReadBits("Count 1", 22);

            var guids1 = new byte[count1][];

            for (int i = 0; i < count1; ++i)
                guids1[i] = packet.StartBitStream(3, 5, 1, 6, 7, 0, 2, 4);

            var count2 = packet.ReadBits("Count 2", 22);

            var guids2 = new byte[count2][];

            for (int i = 0; i < count2; ++i)
                guids2[i] = packet.StartBitStream(6, 5, 4, 7, 2, 1, 0, 3);

            for (int i = 0; i < count2; ++i)
            {
                packet.ReadXORByte(guids2[i], 2);
                packet.ReadXORByte(guids2[i], 1);

                packet.ReadSingle("Y", i);

                packet.ReadXORByte(guids2[i], 5);
                packet.ReadXORByte(guids2[i], 4);
                packet.ReadXORByte(guids2[i], 7);
                packet.ReadXORByte(guids2[i], 0);
                packet.ReadXORByte(guids2[i], 6);
                packet.ReadXORByte(guids2[i], 3);

                packet.ReadSingle("X", i);

                packet.WriteGuid("Guid 2", guids2[i], i);
            }

            for (int i = 0; i < count1; ++i)
            {
                packet.ReadXORByte(guids1[i], 6);

                packet.ReadSingle("X", i);

                packet.ReadXORByte(guids1[i], 5);
                packet.ReadXORByte(guids1[i], 3);

                packet.ReadSingle("Y", i);

                packet.ReadXORByte(guids1[i], 1);
                packet.ReadXORByte(guids1[i], 7);
                packet.ReadXORByte(guids1[i], 0);
                packet.ReadXORByte(guids1[i], 2);
                packet.ReadXORByte(guids1[i], 4);

                packet.WriteGuid("Guid 1", guids1[i], i);
            }
        }
    }
}
