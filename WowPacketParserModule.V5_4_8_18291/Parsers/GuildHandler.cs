using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.CMSG_QUERY_GUILD_INFO)]
        public static void HandleGuildQuery(Packet packet)
        {
            var playerGUID = new byte[8];
            var guildGUID = new byte[8];

            playerGUID[7] = packet.ReadBit();
            playerGUID[3] = packet.ReadBit();
            playerGUID[4] = packet.ReadBit();
            guildGUID[3] = packet.ReadBit();
            guildGUID[4] = packet.ReadBit();
            playerGUID[2] = packet.ReadBit();
            playerGUID[6] = packet.ReadBit();
            guildGUID[2] = packet.ReadBit();
            guildGUID[5] = packet.ReadBit();
            playerGUID[1] = packet.ReadBit();
            playerGUID[5] = packet.ReadBit();
            guildGUID[7] = packet.ReadBit();
            playerGUID[0] = packet.ReadBit();
            guildGUID[1] = packet.ReadBit();
            guildGUID[6] = packet.ReadBit();
            guildGUID[0] = packet.ReadBit();

            packet.ReadXORByte(playerGUID, 7);
            packet.ReadXORByte(guildGUID, 2);
            packet.ReadXORByte(guildGUID, 4);
            packet.ReadXORByte(guildGUID, 7);
            packet.ReadXORByte(playerGUID, 6);
            packet.ReadXORByte(playerGUID, 0);
            packet.ReadXORByte(guildGUID, 6);
            packet.ReadXORByte(guildGUID, 0);
            packet.ReadXORByte(guildGUID, 3);
            packet.ReadXORByte(playerGUID, 2);
            packet.ReadXORByte(guildGUID, 5);
            packet.ReadXORByte(playerGUID, 3);
            packet.ReadXORByte(guildGUID, 1);
            packet.ReadXORByte(playerGUID, 4);
            packet.ReadXORByte(playerGUID, 1);
            packet.ReadXORByte(playerGUID, 5);

            packet.WriteGuid("Player GUID", playerGUID);
            packet.WriteGuid("Guild GUID", guildGUID);
        }

        [Parser(Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            var guid2 = new byte[8];
            var guid1 = new byte[8];

            uint nameLen = 0;
            uint rankCount = 0;
            uint[] rankName = null;

            guid2[5] = packet.ReadBit();
            var hasData = packet.ReadBit();
            if (hasData)
            {
                rankCount = packet.ReadBits(21);

                guid1[5] = packet.ReadBit();
                guid1[1] = packet.ReadBit();
                guid1[4] = packet.ReadBit();
                guid1[7] = packet.ReadBit();

                rankName = new uint[rankCount];
                for (var i = 0; i < rankCount; ++i)
                    rankName[i] = packet.ReadBits(7);

                guid1[3] = packet.ReadBit();
                guid1[2] = packet.ReadBit();
                guid1[0] = packet.ReadBit();
                guid1[6] = packet.ReadBit();

                nameLen = packet.ReadBits(7);
            }

            guid2[3] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[6] = packet.ReadBit();

            if (hasData)
            {
                packet.ReadInt32("Emblem Border Style");
                packet.ReadInt32("Emblem Style");
                packet.ReadXORByte(guid1, 2);
                packet.ReadXORByte(guid1, 7);
                packet.ReadInt32("Emblem Color");
                packet.ReadInt32("Realm Id");

                for (var j = 0; j < rankCount; j++)
                {
                    packet.ReadInt32("Rights Order", j);
                    packet.ReadInt32("Creation Order", j);
                    packet.ReadWoWString("Rank Name", rankName[j], j);
                }

                packet.ReadWoWString("Guild Name", nameLen);

                packet.ReadInt32("Emblem Background Color");
                packet.ReadXORByte(guid1, 5);
                packet.ReadXORByte(guid1, 4);
                packet.ReadInt32("Emblem Border Color");
                packet.ReadXORByte(guid1, 1);
                packet.ReadXORByte(guid1, 6);
                packet.ReadXORByte(guid1, 0);
                packet.ReadXORByte(guid1, 3);

                packet.WriteGuid("Guid1", guid1);
            }

            packet.ParseBitStream(guid2, 2, 6, 4, 0, 7, 3, 5, 1);

            packet.WriteGuid("Guid2", guid2);
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
                packet.ReadInt32("Gold Per Day", i);

                for (var j = 0; j < guildBankMaxTabs; ++j)
                {
                    packet.ReadInt32("Tab Slots", i, j);
                    packet.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i, j);
                }

                packet.ReadWoWString("Name", length[i], i);

                packet.ReadInt32("Rights Order", i);
                packet.ReadInt32("Unk 1", i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_SET_NOTE)]
        public static void HandleGuildPlayerSetNote(Packet packet)
        {
            var playerGUID = new byte[8];

            playerGUID[1]  = packet.ReadBit();
            var noteLength = packet.ReadBits("note length", 8);
            playerGUID[4]  = packet.ReadBit();
            playerGUID[2]  = packet.ReadBit();
            var ispublic   = packet.ReadBit("IsPublic");
            playerGUID[3]  = packet.ReadBit();
            playerGUID[5]  = packet.ReadBit();
            playerGUID[0]  = packet.ReadBit();
            playerGUID[6]  = packet.ReadBit();
            playerGUID[7]  = packet.ReadBit();

            packet.ReadXORByte(playerGUID, 5);
            packet.ReadXORByte(playerGUID, 1);
            packet.ReadXORByte(playerGUID, 6);
            packet.ReadWoWString("note", noteLength);
            packet.ReadXORByte(playerGUID, 0);
            packet.ReadXORByte(playerGUID, 7);
            packet.ReadXORByte(playerGUID, 4);
            packet.ReadXORByte(playerGUID, 3);
            packet.ReadXORByte(playerGUID, 2);

            packet.WriteGuid("Player GUID", playerGUID);
        }

        [Parser(Opcode.SMSG_GUILD_NEWS_TEXT)]
        public static void HandleNewText(Packet packet)
        {
            packet.ReadWoWString("Text", (int)packet.ReadBits(10));
        }

        [Parser(Opcode.CMSG_GUILD_DEMOTE_MEMBER)]
        public static void HandleGuildDemote(Packet packet)
        {
            var guid = packet.StartBitStream(3, 6, 0, 2, 7, 5, 4, 1);
            packet.ParseBitStream(guid, 7, 4, 2, 5, 1, 3, 0, 6);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_GUILD_SET_NOTE)]
        public static void HandleGuildClientSetNote(Packet packet)
        {
            var playerGUID = new byte[8];

            playerGUID[1]  = packet.ReadBit();
            var noteLength = packet.ReadBits("note length", 8);
            var ispublic   = packet.ReadBit("IsPublic");
            playerGUID[4]  = packet.ReadBit();
            playerGUID[2]  = packet.ReadBit();
            playerGUID[3]  = packet.ReadBit();
            playerGUID[5]  = packet.ReadBit();
            playerGUID[0]  = packet.ReadBit();
            playerGUID[6]  = packet.ReadBit();
            playerGUID[7]  = packet.ReadBit();

            packet.ReadXORByte(playerGUID, 5);
            packet.ReadXORByte(playerGUID, 1);
            packet.ReadXORByte(playerGUID, 6);
            packet.ReadXORByte(playerGUID, 0);
            packet.ReadWoWString("note", noteLength);
            packet.ReadXORByte(playerGUID, 7);
            packet.ReadXORByte(playerGUID, 4);
            packet.ReadXORByte(playerGUID, 3);
            packet.ReadXORByte(playerGUID, 2);
            packet.ResetBitReader();

            packet.WriteGuid("Player GUID", playerGUID);
        }
    }
}
