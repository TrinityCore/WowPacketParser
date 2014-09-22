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
            var bits0 = packet.ReadBits(18);

            var guid = new byte[bits0][];

            var bits2084 = new uint[bits0];
            var bit10 = new bool[bits0];
            var bit1C = new bool[bits0];
            var bits144 = new uint[bits0];
            var bits0F = new uint[bits0];

            for (var i = 0; i < bits0; ++i)
            {
                bits2084[i] = packet.ReadBits(17);

                bit10[i] = packet.ReadBit();
                if (bit10[i])
                {
                    guid[i] = new byte[8];
                    packet.StartBitStream(guid[i], 3, 6, 4, 2, 5, 1, 0, 7);
                }

                bit1C[i] = packet.ReadBit();
                bits144[i] = packet.ReadBits(13);
                bits0F[i] = packet.ReadBits(8);

                for (var j = 0; j < bits2084[i]; ++j)
                    packet.ReadBit("bit84", i, j);
            }

            for (var i = 0; i < bits0; ++i)
            {
                for (var j = 0; j < bits2084[i]; ++j)
                {
                    packet.ReadInt32("Int14", i, j);
                    packet.ReadInt32("Int14", i, j);

                    for (var k = 0; k < 8; ++k)
                    {
                        packet.ReadInt32("Int14", i, j, k);
                        packet.ReadInt32("Int14", i, j, k);
                        packet.ReadInt32("Int14", i, j, k);
                    }

                    packet.ReadInt32("Int14", i, j);
                    packet.ReadByte("Byte14", i, j);
                    packet.ReadInt32("Int14", i, j);
                    packet.ReadInt32("Int0", i, j);
                    packet.ReadInt32("Int14", i, j);
                    packet.ReadInt32("Int14", i, j);
                    var len = packet.ReadInt32("len");

                    packet.ReadBytes(len);

                    packet.ReadInt32("Int14", i, j);
                }

                packet.ReadInt32("Int14", i);

                if (bit10[i])
                {
                    packet.ParseBitStream(guid[i], 2, 0, 4, 5, 3, 6, 1, 7);
                    packet.WriteGuid("Guid", guid[i], i);
                }

                if (bits0F[i] > 0)
                    packet.ReadWoWString("String0F", bits0F[i], i);
                packet.ReadInt32("Int14", i);
                packet.ReadSingle("Float14", i);
                packet.ReadInt64("IntED", i);
                packet.ReadInt32("Int14", i);
                packet.ReadInt64("IntED", i);
                packet.ReadInt32("Int14", i);
                if (bits144[i] > 0)
                    packet.ReadWoWString("String144", bits144[i], i);

                if (bit1C[i])
                    packet.ReadInt32("Int14", i);

                packet.ReadByte("Byte14", i);
                packet.ReadInt32("Int14", i);
            }

            packet.ReadInt32("Int20");
        }
    }
}