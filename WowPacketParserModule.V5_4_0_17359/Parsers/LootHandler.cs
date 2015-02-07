using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class LootHandler
    {
        [Parser(Opcode.SMSG_LOOT_MONEY_NOTIFY)]
        public static void HandleLootMoneyNotify(Packet packet)
        {
            packet.ReadInt32("Gold");
            packet.ReadBit("bit14");
        }

        [Parser(Opcode.SMSG_LOOT_RELEASE)]
        public static void HandleLootReleaseResponse(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            var bit45 = packet.ReadBit();
            packet.StartBitStream(guid1, 0, 1);
            var bit30 = !packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            packet.StartBitStream(guid2, 2, 0);
            var bit46 = !packet.ReadBit();
            guid2[1] = packet.ReadBit();
            var bit54 = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            var bit44 = !packet.ReadBit();
            guid2[3] = packet.ReadBit();
            var bit20 = !packet.ReadBit();
            var bits34 = (int)packet.ReadBits(20);
            packet.StartBitStream(guid1, 7, 4);
            guid2[6] = packet.ReadBit();

            for (var i = 0; i < bits34; ++i)
                packet.ReadBits("bitsED", 3, i);

            guid1[2] = packet.ReadBit();
            var bit50 = !packet.ReadBit();
            guid2[4] = packet.ReadBit();
            var bits10 = (int)packet.ReadBits(19);

            var bit14 = new bool[bits10];
            var bit15 = new bool[bits10];
            var bits18 = new int[bits10];
            var bit20b = new bool[bits10];
            var bits14 = new int[bits10];

            for (var i = 0; i < bits10; ++i)
            {
                bit14[i] = !packet.ReadBit();
                bit15[i] = !packet.ReadBit();
                bits18[i] = (int)packet.ReadBits(2);
                bit20b[i] = packet.ReadBit();
                bits14[i] = (int)packet.ReadBits(3);
            }

            guid1[5] = packet.ReadBit();
            guid2[5] = packet.ReadBit();

            if (bit46)
                packet.ReadByte("Byte46");

            for (var i = 0; i < bits34; ++i)
            {
                packet.ReadByte("ByteED", i);
                packet.ReadInt32("Int38", i);
                packet.ReadInt32("IntED", i);
            }

            packet.ReadXORByte(guid1, 3);
            for (var i = 0; i < bits10; ++i)
            {
                if (bit15[i])
                    packet.ReadByte("Byte14", i);

                packet.ReadInt32("Int14", i);

                if (bit14[i])
                    packet.ReadByte("Byte14", i);

                packet.ReadInt32("Item Display Id", i);
                packet.ReadInt32("Int14", i);
                packet.ReadInt32("Int14", i);
                var len = packet.ReadInt32("Int50", i);

                packet.ReadBytes(len);

                packet.ReadInt32("Item Id", i);
            }

            packet.ReadXORByte(guid1, 0);

            if (bit50)
                packet.ReadInt32("Int50");

            if (bit44)
                packet.ReadByte("Byte44");

            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 1);

            if (bit30)
                packet.ReadByte("Byte30");

            if (bit20)
                packet.ReadByte("Byte20");

            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 6);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }
    }
}
