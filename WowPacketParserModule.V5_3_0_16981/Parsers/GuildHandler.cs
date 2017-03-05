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

            var hasData = packet.Translator.ReadBit();

            int nameLen = 0;
            int rankCount = 0;
            int[] rankName = null;
            if (hasData)
            {
                packet.Translator.StartBitStream(guid2, 1, 7);
                nameLen = (int)packet.Translator.ReadBits(7);
                packet.Translator.StartBitStream(guid2, 5, 0, 6, 3, 4, 2);
                rankCount = (int)packet.Translator.ReadBits(21);
                rankName = new int[rankCount];
                for (var i = 0; i < rankCount; ++i)
                    rankName[i] = (int)packet.Translator.ReadBits(7);
            }

            packet.Translator.StartBitStream(guid1, 2, 6, 7, 5, 4, 3, 0, 1);
            if (hasData)
            {
                packet.Translator.ReadWoWString("Guild Name", nameLen);
                packet.Translator.ReadXORByte(guid2, 5);
                for (var i = 0; i < rankCount; ++i)
                {
                    packet.Translator.ReadUInt32("Rights Order", i);
                    packet.Translator.ReadUInt32("Creation Order", i);
                    packet.Translator.ReadWoWString("Rank Name", rankName[i], i);
                }

                packet.Translator.ReadInt32("Emblem Border Color");
                packet.Translator.ReadXORBytes(guid2, 6, 0);
                packet.Translator.ReadInt32("Emblem Border Style");
                packet.Translator.ReadInt32("Emblem Style");
                packet.Translator.ReadXORByte(guid2, 4);
                packet.Translator.ReadInt32("Emblem Color");
                packet.Translator.ReadXORBytes(guid2, 7, 2, 3, 1);
                packet.Translator.ReadInt32("Emblem Background Color");
                packet.Translator.WriteGuid("Guid2", guid2);
            }

            packet.Translator.ParseBitStream(guid1, 1, 6, 3, 5, 4, 0, 2, 7);
            packet.Translator.WriteGuid("Guid1", guid1);
        }
    }
}
