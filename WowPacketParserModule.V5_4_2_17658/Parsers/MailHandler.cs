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
            packet.ReadUInt32("Total Mails");

            var count = packet.ReadBits(18);

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
                bit1C[i] = packet.ReadBit();
                bit24[i] = packet.ReadBit();
                subjectLength[i] = packet.ReadBits(8);
                itemCount[i] = packet.ReadBits(17);
                bodyLength[i] = packet.ReadBits(13);

                for (var j = 0; j < itemCount[i]; ++j)
                    packet.ReadBit("bit84", i, j);

                bit10[i] = packet.ReadBit();

                if (bit10[i])
                {
                    guid[i] = new byte[8];
                    packet.StartBitStream(guid[i], 2, 0, 4, 5, 6, 3, 1, 7);
                }

                bit2C[i] = packet.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                if (bit10[i])
                {
                    packet.ParseBitStream(guid[i], 6, 4, 2, 7, 3, 1, 0, 5);
                    packet.WriteGuid("Guid", guid[i]);
                }

                for (var j = 0; j < itemCount[i]; ++j)
                {
                    packet.ReadInt32("Int14", i, j);

                    for (var k = 0; k < 8; ++k)
                    {
                        packet.ReadInt32("Int14", i, j, k);
                        packet.ReadInt32("Int14", i, j, k);
                        packet.ReadInt32("Int14", i, j, k);
                    }

                    packet.ReadUInt32<ItemId>("Item Id", i, j);

                    var len = packet.ReadInt32();

                    packet.ReadBytes(len);

                    packet.ReadInt32("IntED", i, j);
                    packet.ReadInt32("IntED", i, j);
                    packet.ReadInt32("IntED", i, j);
                    packet.ReadInt32("IntED", i, j);
                    packet.ReadInt32("IntED", i, j);
                    packet.ReadInt32("IntED", i, j);
                    packet.ReadByte("ByteED", i, j);
                }

                packet.ReadWoWString("Subject", subjectLength[i], i);
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);
                packet.ReadWoWString("Body", bodyLength[i], i);
                packet.ReadInt32("IntED", i);
                packet.ReadSingle("Time", i);
                packet.ReadByte("ByteED", i);
                packet.ReadInt64("Int30", i);
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("Int14", i);
                packet.ReadInt64("Int40", i);

                if (bit1C[i])
                    packet.ReadInt32("IntED", i);

                if (bit24[i])
                    packet.ReadInt32("IntED", i);

                if (bit2C[i])
                    packet.ReadInt32("IntED", i);
            }

        }
    }
}