using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.CMSG_GUILD_QUERY)]
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

        [Parser(Opcode.SMSG_GUILD_QUERY_RESPONSE)]
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
    }
}
