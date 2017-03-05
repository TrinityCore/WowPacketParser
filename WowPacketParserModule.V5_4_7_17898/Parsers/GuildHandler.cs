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

            guildGUID[0] = packet.Translator.ReadBit();
            guildGUID[4] = packet.Translator.ReadBit();
            playerGUID[0] = packet.Translator.ReadBit();
            guildGUID[1] = packet.Translator.ReadBit();
            guildGUID[7] = packet.Translator.ReadBit();
            guildGUID[6] = packet.Translator.ReadBit();
            playerGUID[6] = packet.Translator.ReadBit();
            playerGUID[2] = packet.Translator.ReadBit();
            guildGUID[5] = packet.Translator.ReadBit();
            playerGUID[5] = packet.Translator.ReadBit();
            playerGUID[7] = packet.Translator.ReadBit();
            playerGUID[1] = packet.Translator.ReadBit();
            playerGUID[3] = packet.Translator.ReadBit();
            playerGUID[4] = packet.Translator.ReadBit();
            guildGUID[2] = packet.Translator.ReadBit();
            guildGUID[3] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guildGUID, 7);
            packet.Translator.ReadXORByte(playerGUID, 4);
            packet.Translator.ReadXORByte(playerGUID, 0);
            packet.Translator.ReadXORByte(playerGUID, 3);
            packet.Translator.ReadXORByte(playerGUID, 6);
            packet.Translator.ReadXORByte(playerGUID, 7);
            packet.Translator.ReadXORByte(guildGUID, 2);
            packet.Translator.ReadXORByte(guildGUID, 5);
            packet.Translator.ReadXORByte(guildGUID, 0);
            packet.Translator.ReadXORByte(guildGUID, 3);
            packet.Translator.ReadXORByte(playerGUID, 2);
            packet.Translator.ReadXORByte(playerGUID, 5);
            packet.Translator.ReadXORByte(guildGUID, 1);
            packet.Translator.ReadXORByte(playerGUID, 1);
            packet.Translator.ReadXORByte(guildGUID, 6);
            packet.Translator.ReadXORByte(guildGUID, 4);

            packet.Translator.WriteGuid("PlayerGUID", playerGUID);
            packet.Translator.WriteGuid("GuildGUID", guildGUID);
        }

        [Parser(Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            var guid2 = new byte[8];
            var guid1 = new byte[8];

            int nameLen = 0;
            int rankCount = 0;
            int[] rankName = null;

            var hasData = packet.Translator.ReadBit();
            if (hasData)
            {
                guid1[0] = packet.Translator.ReadBit();
                rankCount = (int)packet.Translator.ReadBits(21);
                guid1[2] = packet.Translator.ReadBit();
                guid1[6] = packet.Translator.ReadBit();
                rankName = new int[rankCount];
                for (var i = 0; i < rankCount; ++i)
                    rankName[i] = (int)packet.Translator.ReadBits(7);

                guid1[5] = packet.Translator.ReadBit();
                guid1[1] = packet.Translator.ReadBit();
                guid1[7] = packet.Translator.ReadBit();
                nameLen = (int)packet.Translator.ReadBits(7);
                guid1[4] = packet.Translator.ReadBit();
                guid1[3] = packet.Translator.ReadBit();
            }

            guid2[5] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid2, 6);

            if (hasData)
            {
                packet.Translator.ReadWoWString("Guild Name", nameLen);
                packet.Translator.ReadInt32("Emblem Style");
                packet.Translator.ReadInt32("Emblem Color");
                packet.Translator.ReadXORByte(guid1, 1);
                packet.Translator.ReadXORByte(guid1, 0);

                for (var j = 0; j < rankCount; j++)
                {
                    packet.Translator.ReadInt32("Creation Order", j);
                    packet.Translator.ReadInt32("Rights Order", j);
                    packet.Translator.ReadWoWString("Rank Name", rankName[j], j);
                }

                packet.Translator.ReadXORByte(guid1, 3);
                packet.Translator.ReadXORByte(guid1, 4);
                packet.Translator.ReadXORByte(guid1, 6);
                packet.Translator.ReadXORByte(guid1, 2);
                packet.Translator.ReadInt32("Emblem Border Style");
                packet.Translator.ReadInt32("Emblem Background Color");
                packet.Translator.ReadXORByte(guid1, 5);
                packet.Translator.ReadInt32("Realm Id");
                packet.Translator.ReadInt32("Emblem Border Color");
                packet.Translator.ReadXORByte(guid1, 7);

                packet.Translator.WriteGuid("Guid1", guid1);
            }

            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 3);

            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_GUILD_NEWS_TEXT)]
        public static void HandleNewText(Packet packet)
        {
            packet.Translator.ReadWoWString("Text", (int)packet.Translator.ReadBits(10));
        }

        [Parser(Opcode.CMSG_GUILD_NEWS_UPDATE_STICKY)]
        public static void HandleGuildNewsUpdateSticky(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadInt32("Int1C");
            guid[3] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Sticky");
            guid[5] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();

            packet.Translator.ParseBitStream(guid, 3, 4, 7, 2, 5, 1, 6, 0);

            packet.Translator.WriteGuid("Guid2", guid);

        }

        [Parser(Opcode.SMSG_ALL_GUILD_ACHIEVEMENTS)]
        public static void HandleGuildAchievementData(Packet packet)
        {
            var count = packet.Translator.ReadBits("Criteria count", 20);

            var guid = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                guid[i] = new byte[8];
                packet.Translator.StartBitStream(guid[i], 3, 5, 4, 7, 2, 1, 0, 6);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(guid[i], 2);
                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.ReadInt32("Unk 1", i);
                packet.Translator.ReadXORByte(guid[i], 5);
                packet.Translator.ReadXORByte(guid[i], 3);
                packet.Translator.ReadXORByte(guid[i], 1);
                packet.Translator.ReadInt32<AchievementId>("Achievement Id", i);
                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadInt32("Unk 2", i);
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadXORByte(guid[i], 0);
                packet.Translator.ReadPackedTime("Time", i);
                packet.Translator.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_GET_RANKS)]
        public static void HandleGuildRanks(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(2, 7, 0, 5, 6, 3, 4, 1);
            packet.Translator.ParseBitStream(guid, 6, 0, 1, 7, 2, 3, 4, 5);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_GUILD_RANKS)]
        public static void HandleGuildRankServer(Packet packet)
        {
            const int guildBankMaxTabs = 8;
            var count = packet.Translator.ReadBits("Count", 17);
            var length = new uint[count];

            for (var i = 0; i < count; ++i)
                length[i] = packet.Translator.ReadBits(7);

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Creation Order", i);

                packet.Translator.ReadWoWString("Name", length[i], i);

                packet.Translator.ReadInt32("Unk 1", i);

                for (var j = 0; j < guildBankMaxTabs; ++j)
                {
                    packet.Translator.ReadInt32("Tab Slots", i, j);
                    packet.Translator.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i, j);
                }

                packet.Translator.ReadInt32("Gold Per Day", i);
                packet.Translator.ReadInt32("Rights Order", i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_GET_ROSTER)]
        public static void HandleGuildRosterRequest(Packet packet)
        {
            // Seems to have some previous formula, processed GUIDS does not fit any know guid
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[2] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 1);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_GUILD_ROSTER)]
        public static void HandleGuildRoster(Packet packet)
        {
            packet.Translator.ReadInt32("Unk Uint32 4");
            packet.Translator.ReadInt32("Accounts In Guild");
            packet.Translator.ReadPackedTime("Time");
            packet.Translator.ReadInt32("Weekly Reputation Cap");

            var motdLength = packet.Translator.ReadBits(10);
            var infoLength = packet.Translator.ReadBits(11);
            var size = packet.Translator.ReadBits(17);

            var guid = new byte[size][];
            var nameLength = new uint[size];
            var publicLength = new uint[size];
            var officerLength = new uint[size];

            for (var i = 0; i < size; ++i)
            {
                guid[i] = new byte[8];

                packet.Translator.ReadBit("Can SoR", i);
                publicLength[i] = packet.Translator.ReadBits(8);
                packet.Translator.ReadBit("Has Authenticator", i);
                guid[i][5] = packet.Translator.ReadBit();
                guid[i][4] = packet.Translator.ReadBit();
                nameLength[i] = packet.Translator.ReadBits(6);
                guid[i][6] = packet.Translator.ReadBit();
                guid[i][2] = packet.Translator.ReadBit();
                guid[i][7] = packet.Translator.ReadBit();
                officerLength[i] = packet.Translator.ReadBits(8);
                guid[i][1] = packet.Translator.ReadBit();
                guid[i][3] = packet.Translator.ReadBit();
                guid[i][0] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < size; ++i)
            {
                packet.Translator.ReadInt32("Guild Reputation", i);
                packet.Translator.ReadByteE<Class>("Member Class", i);
                packet.Translator.ReadByte("Member Level", i);
                packet.Translator.ReadInt32("Unk 3", i);
                packet.Translator.ReadInt64("Week activity", i);
                packet.Translator.ReadWoWString("Public note", publicLength[i], i);

                packet.Translator.ReadXORByte(guid[i], 1);
                packet.Translator.ReadSingle("Last online", i);
                packet.Translator.ReadXORByte(guid[i], 2);
                packet.Translator.ReadXORByte(guid[i], 4);
                packet.Translator.ReadInt32("Zone Id", i);
                packet.Translator.ReadByteE<Gender>("Gender", i);
                packet.Translator.ReadInt32("RealmId", i);
                packet.Translator.ReadXORByte(guid[i], 7);
                packet.Translator.ReadXORByte(guid[i], 5);
                packet.Translator.ReadInt32("Member Achievement Points", i);
                packet.Translator.ReadXORByte(guid[i], 3);
                packet.Translator.ReadByteE<GuildMemberFlag>("Member Flags", i);
                packet.Translator.ReadXORByte(guid[i], 6);
                packet.Translator.ReadInt64("Unk 2", i);
                var name = packet.Translator.ReadWoWString("Name", nameLength[i], i);
                packet.Translator.ReadWoWString("Officer note", officerLength[i], i);

                for (var j = 0; j < 2; ++j)
                {
                    var rank = packet.Translator.ReadUInt32();
                    var value = packet.Translator.ReadUInt32();
                    var id = packet.Translator.ReadUInt32();

                    packet.AddValue("Profession", string.Format("Id {0} - Value {1} - Rank {2}", id, value, rank), i, j);
                }
                packet.Translator.ReadXORByte(guid[i], 0);
                packet.Translator.ReadInt32("Member Rank", i);

                packet.Translator.WriteGuid("Guid", guid[i], i);
                StoreGetters.AddName(new WowGuid64(BitConverter.ToUInt64(guid[i], 0)), name);
            }

            packet.Translator.ReadWoWString("Guild Info", infoLength);
            packet.Translator.ReadWoWString("MOTD", motdLength);
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_PARTY_STATE)]
        public static void HandleGuildUpdatePartyState(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 3, 4, 0, 2, 6, 1, 7);
            packet.Translator.ParseBitStream(guid, 5, 4, 3, 1, 7, 0, 2, 6);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_GUILD_PARTY_STATE)]
        public static void HandleGuildPartyStateResponse(Packet packet)
        {
            packet.Translator.ReadBit("Is guild group");
            packet.Translator.ReadSingle("Guild XP multiplier");
            packet.Translator.ReadUInt32("Needed guild members");
            packet.Translator.ReadUInt32("Current guild members");
        }

        [Parser(Opcode.CMSG_GUILD_BANK_ACTIVATE)]
        public static void HandleGuildBankerActivate(Packet packet)
        {
            var guid = new byte[8];

            guid[3] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Full Slot List");
            guid[1] = packet.Translator.ReadBit();

            packet.Translator.ParseBitStream(guid, 7, 1, 4, 2, 0, 3, 6, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GUILD_BANK_QUERY_TAB)]
        public static void HandleGuildBankQueryTab(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadByte("Tab Id");
            guid[4] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Full Slot List");
            guid[3] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();

            packet.Translator.ParseBitStream(guid, 0, 6, 1, 7, 5, 2, 3, 4);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_GUILD_NEWS_UPDATE)]
        public static void HandleGuildNewsUpdate(Packet packet)
        {
            var size = packet.Translator.ReadBits("Size", 19);

            var guidOut = new byte[size][];
            var guidIn = new byte[size][][];
            var count = new uint[size];

            for (int i = 0; i < size; ++i)
            {
                guidOut[i] = new byte[8];
                packet.Translator.StartBitStream(guidOut[i], 1, 5, 0, 7, 3, 4, 6, 2);

                count[i] = packet.Translator.ReadBits(24);
                if (count[i] != 0)
                    packet.AddValue("Count", count[i], i);

                guidIn[i] = new byte[count[i]][];
                for (var j = 0; j < count[i]; ++j)
                    guidIn[i][j] = packet.Translator.StartBitStream(1, 5, 4, 3, 2, 7, 6, 0);
            }

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < count[i]; ++j)
                {
                    packet.Translator.ParseBitStream(guidIn[i][j], 3, 2, 4, 6, 5, 0, 1, 7);
                    packet.Translator.WriteGuid("Guid", guidIn[i][j], i, j);
                }

                packet.Translator.ReadXORByte(guidOut[i], 5);
                packet.Translator.ReadXORByte(guidOut[i], 2);

                packet.Translator.ReadPackedTime("Time", i);

                packet.Translator.ReadXORByte(guidOut[i], 4);
                packet.Translator.ReadXORByte(guidOut[i], 0);
                packet.Translator.ReadXORByte(guidOut[i], 6);
                packet.Translator.ReadXORByte(guidOut[i], 7);

                packet.Translator.ReadInt32("Unk", i); // not 0 for playerachievements and raidencounters, 1 - sticky

                packet.Translator.ReadXORByte(guidOut[i], 1);

                packet.Translator.ReadInt32("Entry (item/achiev/encounter)", i);
                packet.Translator.ReadInt32E<GuildNewsType>("News Type", i);

                packet.Translator.ReadXORByte(guidOut[i], 3);

                packet.Translator.ReadInt32("News Id", i);
                packet.Translator.ReadInt32("Unk Int32 2", i); // always 0

                packet.Translator.WriteGuid("Guid", guidOut[i], i);
            }
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_XP)]
        public static void HandleRequestGuildXP(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(3, 2, 7, 5, 6, 0, 1, 4);
            packet.Translator.ParseBitStream(guid, 3, 7, 2, 1, 5, 4, 0, 6);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_GUILD_XP)]
        public static void HandleGuildXP(Packet packet)
        {
            packet.Translator.ReadUInt64("Guild Current XP");
            packet.Translator.ReadUInt64("Member Weekly XP");
            packet.Translator.ReadUInt64("Guild XP for next Level");
            packet.Translator.ReadUInt64("Member Total XP");
        }

        [Parser(Opcode.SMSG_GUILD_REWARD_LIST)]
        public static void HandleGuildRewardsList(Packet packet)
        {
            packet.Translator.ReadTime("Time");
            var size = packet.Translator.ReadBits("Size", 19);

            var bits4 = new uint[size];

            for (var i = 0; i < size; ++i)
                bits4[i] = packet.Translator.ReadBits(22);

            for (var i = 0; i < size; ++i)
            {
                for (var j = 0; j < bits4[i]; ++j)
                    packet.Translator.ReadInt32<AchievementId>("Achievement Id", i, j);

                packet.Translator.ReadUInt64("Price", i);
                packet.Translator.ReadUInt32E<ReputationRank>("Faction Standing", i);
                packet.Translator.ReadUInt32E<RaceMask>("Race mask", i);
                packet.Translator.ReadUInt32<ItemId>("Item Id", i);
                packet.Translator.ReadUInt32("Unk UInt32", i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_COMMAND_RESULT)]
        public static void HandleGuildCommandResult(Packet packet)
        {
            packet.Translator.ReadUInt32E<GuildCommandError>("Command Result");
            packet.Translator.ReadUInt32E<GuildCommandType>("Command Type");

            var paramLen = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("Param", paramLen);
        }

        [Parser(Opcode.CMSG_GUILD_SET_GUILD_MASTER)]
        public static void HandleGuildSetGuildMaster(Packet packet)
        {
            var nameLength = packet.Translator.ReadBits(9);
            packet.Translator.ReadWoWString("New GuildMaster name", nameLength);
        }

        [Parser(Opcode.CMSG_GUILD_SET_RANK_PERMISSIONS)]
        public static void HandleGuildRank(Packet packet)
        {
            packet.Translator.ReadUInt32("Old Rank Id");

            for (var i = 0; i < 8; ++i)
            {
                packet.Translator.ReadUInt32E<GuildBankRightsFlag>("Tab Rights", i);
                packet.Translator.ReadUInt32("Tab Slot", i);
            }

            packet.Translator.ReadUInt32("Money Per Day");
            packet.Translator.ReadUInt32E<GuildRankRightsFlag>("New Rights");
            packet.Translator.ReadUInt32("New Rank Id");
            packet.Translator.ReadUInt32E<GuildRankRightsFlag>("Old Rights");

            var length = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("Rank Name", length);
        }

        [Parser(Opcode.CMSG_GUILD_OFFICER_REMOVE_MEMBER)]
        public static void HandleGuildRemove(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(3, 1, 6, 0, 7, 2, 5, 4);
            packet.Translator.ParseBitStream(guid, 2, 6, 0, 1, 4, 3, 5, 7);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_DEMOTE_MEMBER)]
        public static void HandleGuildDemote(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(1, 0, 7, 5, 3, 2, 4, 6);
            packet.Translator.ParseBitStream(guid, 3, 4, 1, 0, 7, 2, 5, 6);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_PROMOTE_MEMBER)]
        public static void HandleGuildPromote(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(4, 0, 3, 5, 7, 1, 2, 6);
            packet.Translator.ParseBitStream(guid, 7, 0, 5, 2, 3, 6, 4, 1);
            packet.Translator.WriteGuid("GUID", guid);
        }
    }
}
