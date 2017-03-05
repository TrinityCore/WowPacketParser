using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.CMSG_QUERY_GUILD_INFO)]
        public static void HandleGuildQuery(Packet packet)
        {
            var playerGUID = new byte[8];
            var guildGUID = new byte[8];

            guildGUID[0] = packet.ReadBit();
            guildGUID[4] = packet.ReadBit();
            playerGUID[0] = packet.ReadBit();
            guildGUID[1] = packet.ReadBit();
            guildGUID[7] = packet.ReadBit();
            guildGUID[6] = packet.ReadBit();
            playerGUID[6] = packet.ReadBit();
            playerGUID[2] = packet.ReadBit();
            guildGUID[5] = packet.ReadBit();
            playerGUID[5] = packet.ReadBit();
            playerGUID[7] = packet.ReadBit();
            playerGUID[1] = packet.ReadBit();
            playerGUID[3] = packet.ReadBit();
            playerGUID[4] = packet.ReadBit();
            guildGUID[2] = packet.ReadBit();
            guildGUID[3] = packet.ReadBit();

            packet.ReadXORByte(guildGUID, 7);
            packet.ReadXORByte(playerGUID, 4);
            packet.ReadXORByte(playerGUID, 0);
            packet.ReadXORByte(playerGUID, 3);
            packet.ReadXORByte(playerGUID, 6);
            packet.ReadXORByte(playerGUID, 7);
            packet.ReadXORByte(guildGUID, 2);
            packet.ReadXORByte(guildGUID, 5);
            packet.ReadXORByte(guildGUID, 0);
            packet.ReadXORByte(guildGUID, 3);
            packet.ReadXORByte(playerGUID, 2);
            packet.ReadXORByte(playerGUID, 5);
            packet.ReadXORByte(guildGUID, 1);
            packet.ReadXORByte(playerGUID, 1);
            packet.ReadXORByte(guildGUID, 6);
            packet.ReadXORByte(guildGUID, 4);

            packet.WriteGuid("PlayerGUID", playerGUID);
            packet.WriteGuid("GuildGUID", guildGUID);
        }

        [Parser(Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            var guid2 = new byte[8];
            var guid1 = new byte[8];

            int nameLen = 0;
            int rankCount = 0;
            int[] rankName = null;

            var hasData = packet.ReadBit();
            if (hasData)
            {
                guid1[0] = packet.ReadBit();
                rankCount = (int)packet.ReadBits(21);
                guid1[2] = packet.ReadBit();
                guid1[6] = packet.ReadBit();
                rankName = new int[rankCount];
                for (var i = 0; i < rankCount; ++i)
                    rankName[i] = (int)packet.ReadBits(7);

                guid1[5] = packet.ReadBit();
                guid1[1] = packet.ReadBit();
                guid1[7] = packet.ReadBit();
                nameLen = (int)packet.ReadBits(7);
                guid1[4] = packet.ReadBit();
                guid1[3] = packet.ReadBit();
            }

            guid2[5] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            packet.ReadXORByte(guid2, 6);

            if (hasData)
            {
                packet.ReadWoWString("Guild Name", nameLen);
                packet.ReadInt32("Emblem Style");
                packet.ReadInt32("Emblem Color");
                packet.ReadXORByte(guid1, 1);
                packet.ReadXORByte(guid1, 0);

                for (var j = 0; j < rankCount; j++)
                {
                    packet.ReadInt32("Creation Order", j);
                    packet.ReadInt32("Rights Order", j);
                    packet.ReadWoWString("Rank Name", rankName[j], j);
                }

                packet.ReadXORByte(guid1, 3);
                packet.ReadXORByte(guid1, 4);
                packet.ReadXORByte(guid1, 6);
                packet.ReadXORByte(guid1, 2);
                packet.ReadInt32("Emblem Border Style");
                packet.ReadInt32("Emblem Background Color");
                packet.ReadXORByte(guid1, 5);
                packet.ReadInt32("Realm Id");
                packet.ReadInt32("Emblem Border Color");
                packet.ReadXORByte(guid1, 7);

                packet.WriteGuid("Guid1", guid1);
            }

            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 3);

            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_GUILD_NEWS_TEXT)]
        public static void HandleNewText(Packet packet)
        {
            packet.ReadWoWString("Text", (int)packet.ReadBits(10));
        }

        [Parser(Opcode.CMSG_GUILD_NEWS_UPDATE_STICKY)]
        public static void HandleGuildNewsUpdateSticky(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Int1C");
            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            packet.ReadBit("Sticky");
            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ParseBitStream(guid, 3, 4, 7, 2, 5, 1, 6, 0);

            packet.WriteGuid("Guid2", guid);

        }

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS)]
        public static void HandleGuildAchievementData(Packet packet)
        {
            var count = packet.ReadBits("Criteria count", 20);

            var guid = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                guid[i] = new byte[8];
                packet.StartBitStream(guid[i], 3, 5, 4, 7, 2, 1, 0, 6);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guid[i], 2);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadInt32("Unk 1", i);
                packet.ReadXORByte(guid[i], 5);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadInt32<AchievementId>("Achievement Id", i);
                packet.ReadXORByte(guid[i], 6);
                packet.ReadInt32("Unk 2", i);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 0);
                packet.ReadPackedTime("Time", i);
                packet.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_GET_RANKS)]
        public static void HandleGuildRanks(Packet packet)
        {
            var guid = packet.StartBitStream(2, 7, 0, 5, 6, 3, 4, 1);
            packet.ParseBitStream(guid, 6, 0, 1, 7, 2, 3, 4, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_GUILD_RANKS)]
        public static void HandleGuildRankServer(Packet packet)
        {
            const int guildBankMaxTabs = 8;
            var count = packet.ReadBits("Count", 17);
            var length = new uint[count];

            for (var i = 0; i < count; ++i)
                length[i] = packet.ReadBits(7);

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Creation Order", i);

                packet.ReadWoWString("Name", length[i], i);

                packet.ReadInt32("Unk 1", i);

                for (var j = 0; j < guildBankMaxTabs; ++j)
                {
                    packet.ReadInt32("Tab Slots", i, j);
                    packet.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i, j);
                }

                packet.ReadInt32("Gold Per Day", i);
                packet.ReadInt32("Rights Order", i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_GET_ROSTER)]
        public static void HandleGuildRosterRequest(Packet packet)
        {
            // Seems to have some previous formula, processed GUIDS does not fit any know guid
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[2] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid2[1] = packet.ReadBit();

            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 1);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER)]
        public static void HandleGuildRoster(Packet packet)
        {
            packet.ReadInt32("Unk Uint32 4");
            packet.ReadInt32("Accounts In Guild");
            packet.ReadPackedTime("Time");
            packet.ReadInt32("Weekly Reputation Cap");

            var motdLength = packet.ReadBits(10);
            var infoLength = packet.ReadBits(11);
            var size = packet.ReadBits(17);

            var guid = new byte[size][];
            var nameLength = new uint[size];
            var publicLength = new uint[size];
            var officerLength = new uint[size];

            for (var i = 0; i < size; ++i)
            {
                guid[i] = new byte[8];

                packet.ReadBit("Can SoR", i);
                publicLength[i] = packet.ReadBits(8);
                packet.ReadBit("Has Authenticator", i);
                guid[i][5] = packet.ReadBit();
                guid[i][4] = packet.ReadBit();
                nameLength[i] = packet.ReadBits(6);
                guid[i][6] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                officerLength[i] = packet.ReadBits(8);
                guid[i][1] = packet.ReadBit();
                guid[i][3] = packet.ReadBit();
                guid[i][0] = packet.ReadBit();
            }

            for (var i = 0; i < size; ++i)
            {
                packet.ReadInt32("Guild Reputation", i);
                packet.ReadByteE<Class>("Member Class", i);
                packet.ReadByte("Member Level", i);
                packet.ReadInt32("Unk 3", i);
                packet.ReadInt64("Week activity", i);
                packet.ReadWoWString("Public note", publicLength[i], i);

                packet.ReadXORByte(guid[i], 1);
                packet.ReadSingle("Last online", i);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadInt32("Zone Id", i);
                packet.ReadByteE<Gender>("Gender", i);
                packet.ReadInt32("RealmId", i);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 5);
                packet.ReadInt32("Member Achievement Points", i);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadByteE<GuildMemberFlag>("Member Flags", i);
                packet.ReadXORByte(guid[i], 6);
                packet.ReadInt64("Unk 2", i);
                var name = packet.ReadWoWString("Name", nameLength[i], i);
                packet.ReadWoWString("Officer note", officerLength[i], i);

                for (var j = 0; j < 2; ++j)
                {
                    var rank = packet.ReadUInt32();
                    var value = packet.ReadUInt32();
                    var id = packet.ReadUInt32();

                    packet.AddValue("Profession", string.Format("Id {0} - Value {1} - Rank {2}", id, value, rank), i, j);
                }
                packet.ReadXORByte(guid[i], 0);
                packet.ReadInt32("Member Rank", i);

                packet.WriteGuid("Guid", guid[i], i);
                StoreGetters.AddName(new WowGuid64(BitConverter.ToUInt64(guid[i], 0)), name);
            }

            packet.ReadWoWString("Guild Info", infoLength);
            packet.ReadWoWString("MOTD", motdLength);
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_PARTY_STATE)]
        public static void HandleGuildUpdatePartyState(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 3, 4, 0, 2, 6, 1, 7);
            packet.ParseBitStream(guid, 5, 4, 3, 1, 7, 0, 2, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_GUILD_PARTY_STATE)]
        public static void HandleGuildPartyStateResponse(Packet packet)
        {
            packet.ReadBit("Is guild group");
            packet.ReadSingle("Guild XP multiplier");
            packet.ReadUInt32("Needed guild members");
            packet.ReadUInt32("Current guild members");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_ACTIVATE)]
        public static void HandleGuildBankerActivate(Packet packet)
        {
            var guid = new byte[8];

            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            packet.ReadBit("Full Slot List");
            guid[1] = packet.ReadBit();

            packet.ParseBitStream(guid, 7, 1, 4, 2, 0, 3, 6, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GUILD_BANK_QUERY_TAB)]
        public static void HandleGuildBankQueryTab(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadByte("Tab Id");
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            packet.ReadBit("Full Slot List");
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            packet.ParseBitStream(guid, 0, 6, 1, 7, 5, 2, 3, 4);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_GUILD_NEWS_UPDATE)]
        public static void HandleGuildNewsUpdate(Packet packet)
        {
            var size = packet.ReadBits("Size", 19);

            var guidOut = new byte[size][];
            var guidIn = new byte[size][][];
            var count = new uint[size];

            for (int i = 0; i < size; ++i)
            {
                guidOut[i] = new byte[8];
                packet.StartBitStream(guidOut[i], 1, 5, 0, 7, 3, 4, 6, 2);

                count[i] = packet.ReadBits(24);
                if (count[i] != 0)
                    packet.AddValue("Count", count[i], i);

                guidIn[i] = new byte[count[i]][];
                for (var j = 0; j < count[i]; ++j)
                    guidIn[i][j] = packet.StartBitStream(1, 5, 4, 3, 2, 7, 6, 0);
            }

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < count[i]; ++j)
                {
                    packet.ParseBitStream(guidIn[i][j], 3, 2, 4, 6, 5, 0, 1, 7);
                    packet.WriteGuid("Guid", guidIn[i][j], i, j);
                }

                packet.ReadXORByte(guidOut[i], 5);
                packet.ReadXORByte(guidOut[i], 2);

                packet.ReadPackedTime("Time", i);

                packet.ReadXORByte(guidOut[i], 4);
                packet.ReadXORByte(guidOut[i], 0);
                packet.ReadXORByte(guidOut[i], 6);
                packet.ReadXORByte(guidOut[i], 7);

                packet.ReadInt32("Unk", i); // not 0 for playerachievements and raidencounters, 1 - sticky

                packet.ReadXORByte(guidOut[i], 1);

                packet.ReadInt32("Entry (item/achiev/encounter)", i);
                packet.ReadInt32E<GuildNewsType>("News Type", i);

                packet.ReadXORByte(guidOut[i], 3);

                packet.ReadInt32("News Id", i);
                packet.ReadInt32("Unk Int32 2", i); // always 0

                packet.WriteGuid("Guid", guidOut[i], i);
            }
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_XP)]
        public static void HandleRequestGuildXP(Packet packet)
        {
            var guid = packet.StartBitStream(3, 2, 7, 5, 6, 0, 1, 4);
            packet.ParseBitStream(guid, 3, 7, 2, 1, 5, 4, 0, 6);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_GUILD_XP)]
        public static void HandleGuildXP(Packet packet)
        {
            packet.ReadUInt64("Guild Current XP");
            packet.ReadUInt64("Member Weekly XP");
            packet.ReadUInt64("Guild XP for next Level");
            packet.ReadUInt64("Member Total XP");
        }

        [Parser(Opcode.SMSG_GUILD_REWARD_LIST)]
        public static void HandleGuildRewardsList(Packet packet)
        {
            packet.ReadTime("Time");
            var size = packet.ReadBits("Size", 19);

            var bits4 = new uint[size];

            for (var i = 0; i < size; ++i)
                bits4[i] = packet.ReadBits(22);

            for (var i = 0; i < size; ++i)
            {
                for (var j = 0; j < bits4[i]; ++j)
                    packet.ReadInt32<AchievementId>("Achievement Id", i, j);

                packet.ReadUInt64("Price", i);
                packet.ReadUInt32E<ReputationRank>("Faction Standing", i);
                packet.ReadUInt32E<RaceMask>("Race mask", i);
                packet.ReadUInt32<ItemId>("Item Id", i);
                packet.ReadUInt32("Unk UInt32", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_COMMAND_RESULT)]
        public static void HandleGuildCommandResult(Packet packet)
        {
            packet.ReadUInt32E<GuildCommandError>("Command Result");
            packet.ReadUInt32E<GuildCommandType>("Command Type");

            var paramLen = packet.ReadBits(8);
            packet.ReadWoWString("Param", paramLen);
        }

        [Parser(Opcode.CMSG_GUILD_SET_GUILD_MASTER)]
        public static void HandleGuildSetGuildMaster(Packet packet)
        {
            var nameLength = packet.ReadBits(9);
            packet.ReadWoWString("New GuildMaster name", nameLength);
        }

        [Parser(Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS)]
        public static void HandleGuildRank(Packet packet)
        {
            packet.ReadUInt32("Old Rank Id");

            for (var i = 0; i < 8; ++i)
            {
                packet.ReadUInt32E<GuildBankRightsFlag>("Tab Rights", i);
                packet.ReadUInt32("Tab Slot", i);
            }

            packet.ReadUInt32("Money Per Day");
            packet.ReadUInt32E<GuildRankRightsFlag>("New Rights");
            packet.ReadUInt32("New Rank Id");
            packet.ReadUInt32E<GuildRankRightsFlag>("Old Rights");

            var length = packet.ReadBits(7);
            packet.ReadWoWString("Rank Name", length);
        }

        [Parser(Opcode.CMSG_GUILD_OFFICER_REMOVE_MEMBER)]
        public static void HandleGuildRemove(Packet packet)
        {
            var guid = packet.StartBitStream(3, 1, 6, 0, 7, 2, 5, 4);
            packet.ParseBitStream(guid, 2, 6, 0, 1, 4, 3, 5, 7);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_DEMOTE_MEMBER)]
        public static void HandleGuildDemote(Packet packet)
        {
            var guid = packet.StartBitStream(1, 0, 7, 5, 3, 2, 4, 6);
            packet.ParseBitStream(guid, 3, 4, 1, 0, 7, 2, 5, 6);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_PROMOTE_MEMBER)]
        public static void HandleGuildPromote(Packet packet)
        {
            var guid = packet.StartBitStream(4, 0, 3, 5, 7, 1, 2, 6);
            packet.ParseBitStream(guid, 7, 0, 5, 2, 3, 6, 4, 1);
            packet.WriteGuid("GUID", guid);
        }
    }
}
