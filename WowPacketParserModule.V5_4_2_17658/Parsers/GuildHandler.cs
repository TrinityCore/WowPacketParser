using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.SMSG_GUILD_QUERY_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[2] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            
            int nameLen = 0;
            int rankCount = 0;
            int[] rankName = null;
            var hasData = packet.ReadBit();
            if (hasData)
            {
                guid1[5] = packet.ReadBit();
                guid1[4] = packet.ReadBit();
                guid1[1] = packet.ReadBit();
                guid1[3] = packet.ReadBit();
                rankCount = (int)packet.ReadBits(21);
                guid1[0] = packet.ReadBit();

                rankName = new int[rankCount];
                for (var i = 0; i < rankCount; ++i)
                    rankName[i] = (int)packet.ReadBits(7);

                guid1[2] = packet.ReadBit();
                guid1[7] = packet.ReadBit();
                guid1[6] = packet.ReadBit();
                nameLen = (int)packet.ReadBits(7);
            }

            guid2[3] = packet.ReadBit();
            guid2[7] = packet.ReadBit();

            if (hasData)
            {
                packet.ReadXORByte(guid1, 0);
                packet.ReadXORByte(guid1, 1);
                packet.ReadXORByte(guid1, 5);
                packet.ReadInt32("Emblem Border Color");
                for (var j = 0; j < rankCount; j++)
                {
                    packet.ReadInt32("Rights Order", j);
                    packet.ReadInt32("Creation Order", j);
                    packet.ReadWoWString("Rank Name", rankName[j], j);
                }

                packet.ReadWoWString("Guild Name", nameLen);
                packet.ReadInt32("Realm Id");
                packet.ReadInt32("Emblem Background Color");
                packet.ReadXORByte(guid1, 7);
                packet.ReadXORByte(guid1, 6);
                packet.ReadXORByte(guid1, 2);
                packet.ReadInt32("Emblem Style");
                packet.ReadXORByte(guid1, 3);
                packet.ReadXORByte(guid1, 4);
                packet.ReadInt32("Emblem Border Style");
                packet.ReadInt32("Emblem Color");
                packet.WriteGuid("GuildGUID1", guid1);
            }

            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 7);

            packet.WriteGuid("GuildGUID2", guid2);

        }
    }
}
