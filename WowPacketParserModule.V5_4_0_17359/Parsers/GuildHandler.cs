using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.SMSG_GUILD_QUERY_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            var Guild2 = new byte[8];
            var Guild1 = new byte[8];

            int nameLen = 0;
            int rankCount = 0;
            int[] rankName = null;

            packet.StartBitStream(Guild2, 6, 2, 0, 3, 4, 1, 5);

            var hasData = packet.ReadBit();

            Guild1[0] = packet.ReadBit();

            nameLen = (int)packet.ReadBits(7);
            rankCount = (int)packet.ReadBits(21);

            if (hasData)
            {
                rankName = new int[rankCount];
                for (var j = 0; j < rankCount; j++)
                    rankName[j] = (int)packet.ReadBits(7);

                packet.StartBitStream(Guild1, 1, 2, 5, 3, 7, 4, 6);
            }

            Guild2[7] = packet.ReadBit();

            if (hasData)
            {
                packet.ReadInt32("Emblem Style");

                for (var j = 0; j < rankCount; j++)
                {
                    packet.ReadInt32("Rights Order", j);
                    packet.ReadWoWString("Rank Name", rankName[j], j);
                    packet.ReadInt32("Creation Order", j);
                }

                packet.ReadXORByte(Guild1, 1);
                packet.ReadInt32("Realm Id");
                packet.ReadInt32("Emblem Color");
                packet.ReadInt32("Emblem Background Color");
                packet.ReadInt32("Emblem Border Style");
                packet.ReadXORByte(Guild1, 0);
                packet.ReadInt32("Emblem Border Color");
                packet.ReadXORByte(Guild1, 6);

                packet.ReadWoWString("Guild Name", nameLen);

                packet.ReadXORByte(Guild1, 5);
                packet.ReadXORByte(Guild1, 3);
                packet.ReadXORByte(Guild1, 2);
                packet.ReadXORByte(Guild1, 7);
                packet.ReadXORByte(Guild1, 4);

                packet.WriteGuid("Guild1", Guild1);
            }

            packet.ParseBitStream(Guild2, 4, 1, 0, 3, 5, 7, 6, 2);

            packet.WriteGuid("Guild1", Guild2);

        }
    }
}
