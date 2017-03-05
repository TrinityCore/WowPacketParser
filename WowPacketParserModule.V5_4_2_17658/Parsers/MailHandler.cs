using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class MailHandler
    {
        [Parser(Opcode.SMSG_MAIL_LIST_RESULT)]
        public static void HandleMailListResult(Packet packet)
        {
            packet.Translator.ReadUInt32("Total Mails");

            var count = packet.Translator.ReadBits(18);

            var guid = new byte[count][];

            var bit1C = new bool[count];
            var bit24 = new bool[count];
            var bit10 = new bool[count];
            var bit2C = new bool[count];

            var subjectLength = new uint[count];
            var itemCount = new uint[count];
            var bodyLength = new uint[count];

            for (var i = 0; i < count; ++i)
            {
                bit1C[i] = packet.Translator.ReadBit();
                bit24[i] = packet.Translator.ReadBit();
                subjectLength[i] = packet.Translator.ReadBits(8);
                itemCount[i] = packet.Translator.ReadBits(17);
                bodyLength[i] = packet.Translator.ReadBits(13);

                for (var j = 0; j < itemCount[i]; ++j)
                    packet.Translator.ReadBit("bit84", i, j);

                bit10[i] = packet.Translator.ReadBit();

                if (bit10[i])
                {
                    guid[i] = new byte[8];
                    packet.Translator.StartBitStream(guid[i], 2, 0, 4, 5, 6, 3, 1, 7);
                }

                bit2C[i] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                if (bit10[i])
                {
                    packet.Translator.ParseBitStream(guid[i], 6, 4, 2, 7, 3, 1, 0, 5);
                    packet.Translator.WriteGuid("Guid", guid[i]);
                }

                for (var j = 0; j < itemCount[i]; ++j)
                {
                    packet.Translator.ReadInt32("Int14", i, j);

                    for (var k = 0; k < 8; ++k)
                    {
                        packet.Translator.ReadInt32("Int14", i, j, k);
                        packet.Translator.ReadInt32("Int14", i, j, k);
                        packet.Translator.ReadInt32("Int14", i, j, k);
                    }

                    packet.Translator.ReadUInt32<ItemId>("Item Id", i, j);

                    var len = packet.Translator.ReadInt32();

                    packet.Translator.ReadBytes(len);

                    packet.Translator.ReadInt32("IntED", i, j);
                    packet.Translator.ReadInt32("IntED", i, j);
                    packet.Translator.ReadInt32("IntED", i, j);
                    packet.Translator.ReadInt32("IntED", i, j);
                    packet.Translator.ReadInt32("IntED", i, j);
                    packet.Translator.ReadInt32("IntED", i, j);
                    packet.Translator.ReadByte("ByteED", i, j);
                }

                packet.Translator.ReadWoWString("Subject", subjectLength[i], i);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadWoWString("Body", bodyLength[i], i);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadSingle("Time", i);
                packet.Translator.ReadByte("ByteED", i);
                packet.Translator.ReadInt64("Int30", i);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt64("Int40", i);

                if (bit1C[i])
                    packet.Translator.ReadInt32("IntED", i);

                if (bit24[i])
                    packet.Translator.ReadInt32("IntED", i);

                if (bit2C[i])
                    packet.Translator.ReadInt32("IntED", i);
            }

        }
    }
}