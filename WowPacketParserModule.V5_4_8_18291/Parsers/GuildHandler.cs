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

            playerGUID[7] = packet.Translator.ReadBit();
            playerGUID[3] = packet.Translator.ReadBit();
            playerGUID[4] = packet.Translator.ReadBit();
            guildGUID[3] = packet.Translator.ReadBit();
            guildGUID[4] = packet.Translator.ReadBit();
            playerGUID[2] = packet.Translator.ReadBit();
            playerGUID[6] = packet.Translator.ReadBit();
            guildGUID[2] = packet.Translator.ReadBit();
            guildGUID[5] = packet.Translator.ReadBit();
            playerGUID[1] = packet.Translator.ReadBit();
            playerGUID[5] = packet.Translator.ReadBit();
            guildGUID[7] = packet.Translator.ReadBit();
            playerGUID[0] = packet.Translator.ReadBit();
            guildGUID[1] = packet.Translator.ReadBit();
            guildGUID[6] = packet.Translator.ReadBit();
            guildGUID[0] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(playerGUID, 7);
            packet.Translator.ReadXORByte(guildGUID, 2);
            packet.Translator.ReadXORByte(guildGUID, 4);
            packet.Translator.ReadXORByte(guildGUID, 7);
            packet.Translator.ReadXORByte(playerGUID, 6);
            packet.Translator.ReadXORByte(playerGUID, 0);
            packet.Translator.ReadXORByte(guildGUID, 6);
            packet.Translator.ReadXORByte(guildGUID, 0);
            packet.Translator.ReadXORByte(guildGUID, 3);
            packet.Translator.ReadXORByte(playerGUID, 2);
            packet.Translator.ReadXORByte(guildGUID, 5);
            packet.Translator.ReadXORByte(playerGUID, 3);
            packet.Translator.ReadXORByte(guildGUID, 1);
            packet.Translator.ReadXORByte(playerGUID, 4);
            packet.Translator.ReadXORByte(playerGUID, 1);
            packet.Translator.ReadXORByte(playerGUID, 5);

            packet.Translator.WriteGuid("Player GUID", playerGUID);
            packet.Translator.WriteGuid("Guild GUID", guildGUID);
        }

        [Parser(Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            var guid2 = new byte[8];
            var guid1 = new byte[8];

            uint nameLen = 0;
            uint rankCount = 0;
            uint[] rankName = null;

            guid2[5] = packet.Translator.ReadBit();
            var hasData = packet.Translator.ReadBit();
            if (hasData)
            {
                rankCount = packet.Translator.ReadBits(21);

                guid1[5] = packet.Translator.ReadBit();
                guid1[1] = packet.Translator.ReadBit();
                guid1[4] = packet.Translator.ReadBit();
                guid1[7] = packet.Translator.ReadBit();

                rankName = new uint[rankCount];
                for (var i = 0; i < rankCount; ++i)
                    rankName[i] = packet.Translator.ReadBits(7);

                guid1[3] = packet.Translator.ReadBit();
                guid1[2] = packet.Translator.ReadBit();
                guid1[0] = packet.Translator.ReadBit();
                guid1[6] = packet.Translator.ReadBit();

                nameLen = packet.Translator.ReadBits(7);
            }

            guid2[3] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();

            if (hasData)
            {
                packet.Translator.ReadInt32("Emblem Border Style");
                packet.Translator.ReadInt32("Emblem Style");
                packet.Translator.ReadXORByte(guid1, 2);
                packet.Translator.ReadXORByte(guid1, 7);
                packet.Translator.ReadInt32("Emblem Color");
                packet.Translator.ReadInt32("Realm Id");

                for (var j = 0; j < rankCount; j++)
                {
                    packet.Translator.ReadInt32("Rights Order", j);
                    packet.Translator.ReadInt32("Creation Order", j);
                    packet.Translator.ReadWoWString("Rank Name", rankName[j], j);
                }

                packet.Translator.ReadWoWString("Guild Name", nameLen);

                packet.Translator.ReadInt32("Emblem Background Color");
                packet.Translator.ReadXORByte(guid1, 5);
                packet.Translator.ReadXORByte(guid1, 4);
                packet.Translator.ReadInt32("Emblem Border Color");
                packet.Translator.ReadXORByte(guid1, 1);
                packet.Translator.ReadXORByte(guid1, 6);
                packet.Translator.ReadXORByte(guid1, 0);
                packet.Translator.ReadXORByte(guid1, 3);

                packet.Translator.WriteGuid("Guid1", guid1);
            }

            packet.Translator.ParseBitStream(guid2, 2, 6, 4, 0, 7, 3, 5, 1);

            packet.Translator.WriteGuid("Guid2", guid2);
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
                packet.Translator.ReadInt32("Gold Per Day", i);

                for (var j = 0; j < guildBankMaxTabs; ++j)
                {
                    packet.Translator.ReadInt32("Tab Slots", i, j);
                    packet.Translator.ReadInt32E<GuildBankRightsFlag>("Tab Rights", i, j);
                }

                packet.Translator.ReadWoWString("Name", length[i], i);

                packet.Translator.ReadInt32("Rights Order", i);
                packet.Translator.ReadInt32("Unk 1", i);
            }
        }

        [Parser(Opcode.CMSG_GUILD_SET_NOTE)]
        public static void HandleGuildPlayerSetNote(Packet packet)
        {
            var playerGUID = new byte[8];

            playerGUID[1]  = packet.Translator.ReadBit();
            var noteLength = packet.Translator.ReadBits("note length", 8);
            playerGUID[4]  = packet.Translator.ReadBit();
            playerGUID[2]  = packet.Translator.ReadBit();
            var ispublic   = packet.Translator.ReadBit("IsPublic");
            playerGUID[3]  = packet.Translator.ReadBit();
            playerGUID[5]  = packet.Translator.ReadBit();
            playerGUID[0]  = packet.Translator.ReadBit();
            playerGUID[6]  = packet.Translator.ReadBit();
            playerGUID[7]  = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(playerGUID, 5);
            packet.Translator.ReadXORByte(playerGUID, 1);
            packet.Translator.ReadXORByte(playerGUID, 6);
            packet.Translator.ReadWoWString("note", noteLength);
            packet.Translator.ReadXORByte(playerGUID, 0);
            packet.Translator.ReadXORByte(playerGUID, 7);
            packet.Translator.ReadXORByte(playerGUID, 4);
            packet.Translator.ReadXORByte(playerGUID, 3);
            packet.Translator.ReadXORByte(playerGUID, 2);

            packet.Translator.WriteGuid("Player GUID", playerGUID);
        }

        [Parser(Opcode.SMSG_GUILD_NEWS_TEXT)]
        public static void HandleNewText(Packet packet)
        {
            packet.Translator.ReadWoWString("Text", (int)packet.Translator.ReadBits(10));
        }

        [Parser(Opcode.CMSG_GUILD_DEMOTE_MEMBER)]
        public static void HandleGuildDemote(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(3, 6, 0, 2, 7, 5, 4, 1);
            packet.Translator.ParseBitStream(guid, 7, 4, 2, 5, 1, 3, 0, 6);
            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_GUILD_SET_NOTE)]
        public static void HandleGuildClientSetNote(Packet packet)
        {
            var playerGUID = new byte[8];

            playerGUID[1]  = packet.Translator.ReadBit();
            var noteLength = packet.Translator.ReadBits("note length", 8);
            var ispublic   = packet.Translator.ReadBit("IsPublic");
            playerGUID[4]  = packet.Translator.ReadBit();
            playerGUID[2]  = packet.Translator.ReadBit();
            playerGUID[3]  = packet.Translator.ReadBit();
            playerGUID[5]  = packet.Translator.ReadBit();
            playerGUID[0]  = packet.Translator.ReadBit();
            playerGUID[6]  = packet.Translator.ReadBit();
            playerGUID[7]  = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(playerGUID, 5);
            packet.Translator.ReadXORByte(playerGUID, 1);
            packet.Translator.ReadXORByte(playerGUID, 6);
            packet.Translator.ReadXORByte(playerGUID, 0);
            packet.Translator.ReadWoWString("note", noteLength);
            packet.Translator.ReadXORByte(playerGUID, 7);
            packet.Translator.ReadXORByte(playerGUID, 4);
            packet.Translator.ReadXORByte(playerGUID, 3);
            packet.Translator.ReadXORByte(playerGUID, 2);
            packet.Translator.ResetBitReader();

            packet.Translator.WriteGuid("Player GUID", playerGUID);
        }
    }
}
