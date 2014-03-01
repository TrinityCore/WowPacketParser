using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.CMSG_GUILD_QUERY)]
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

        [Parser(Opcode.SMSG_GUILD_QUERY_RESPONSE)]
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

        [Parser(Opcode.SMSG_GUILD_ACHIEVEMENT_DATA)]
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
                packet.ReadInt32("Achievement Id", i);
                packet.ReadXORByte(guid[i], 6);
                packet.ReadInt32("Unk 2", i);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadXORByte(guid[i], 0);
                packet.ReadPackedTime("Time", i);
                packet.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_GUILD_RANK)]
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
                    packet.ReadEnum<GuildBankRightsFlag>("Tab Rights", TypeCode.Int32, i, j);
                }

                packet.ReadInt32("Gold Per Day", i);
                packet.ReadInt32("Rights Order", i);
            }
        }
    }
}
