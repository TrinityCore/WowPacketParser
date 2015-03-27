using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.SMSG_QUERY_GUILD_INFO_RESPONSE)]
        public static void HandleGuildQueryResponse(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            var hasData = packet.ReadBit();

            int nameLen = 0;
            int rankCount = 0;
            int[] rankName = null;
            if (hasData)
            {
                packet.StartBitStream(guid2, 1, 7);
                nameLen = (int)packet.ReadBits(7);
                packet.StartBitStream(guid2, 5, 0, 6, 3, 4, 2);
                rankCount = (int)packet.ReadBits(21);
                rankName = new int[rankCount];
                for (var i = 0; i < rankCount; ++i)
                    rankName[i] = (int)packet.ReadBits(7);
            }

            packet.StartBitStream(guid1, 2, 6, 7, 5, 4, 3, 0, 1);
            if (hasData)
            {
                packet.ReadWoWString("Guild Name", nameLen);
                packet.ReadXORByte(guid2, 5);
                for (var i = 0; i < rankCount; ++i)
                {
                    packet.ReadUInt32("Rights Order", i);
                    packet.ReadUInt32("Creation Order", i);
                    packet.ReadWoWString("Rank Name", rankName[i], i);
                }

                packet.ReadInt32("Emblem Border Color");
                packet.ReadXORBytes(guid2, 6, 0);
                packet.ReadInt32("Emblem Border Style");
                packet.ReadInt32("Emblem Style");
                packet.ReadXORByte(guid2, 4);
                packet.ReadInt32("Emblem Color");
                packet.ReadXORBytes(guid2, 7, 2, 3, 1);
                packet.ReadInt32("Emblem Background Color");
                packet.WriteGuid("Guid2", guid2);
            }

            packet.ParseBitStream(guid1, 1, 6, 3, 5, 4, 0, 2, 7);
            packet.WriteGuid("Guid1", guid1);
        }
    }
}
