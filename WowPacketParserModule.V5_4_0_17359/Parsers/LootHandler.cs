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
            packet.Translator.ReadInt32("Gold");
            packet.Translator.ReadBit("bit14");
        }

        [Parser(Opcode.SMSG_LOOT_RELEASE)]
        public static void HandleLootReleaseResponse(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            var bit45 = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 0, 1);
            var bit30 = !packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid2, 2, 0);
            var bit46 = !packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            var bit54 = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            var bit44 = !packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            var bit20 = !packet.Translator.ReadBit();
            var bits34 = (int)packet.Translator.ReadBits(20);
            packet.Translator.StartBitStream(guid1, 7, 4);
            guid2[6] = packet.Translator.ReadBit();

            for (var i = 0; i < bits34; ++i)
                packet.Translator.ReadBits("bitsED", 3, i);

            guid1[2] = packet.Translator.ReadBit();
            var bit50 = !packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            var bits10 = (int)packet.Translator.ReadBits(19);

            var bit14 = new bool[bits10];
            var bit15 = new bool[bits10];
            var bits18 = new int[bits10];
            var bit20b = new bool[bits10];
            var bits14 = new int[bits10];

            for (var i = 0; i < bits10; ++i)
            {
                bit14[i] = !packet.Translator.ReadBit();
                bit15[i] = !packet.Translator.ReadBit();
                bits18[i] = (int)packet.Translator.ReadBits(2);
                bit20b[i] = packet.Translator.ReadBit();
                bits14[i] = (int)packet.Translator.ReadBits(3);
            }

            guid1[5] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();

            if (bit46)
                packet.Translator.ReadByte("Byte46");

            for (var i = 0; i < bits34; ++i)
            {
                packet.Translator.ReadByte("ByteED", i);
                packet.Translator.ReadInt32("Int38", i);
                packet.Translator.ReadInt32("IntED", i);
            }

            packet.Translator.ReadXORByte(guid1, 3);
            for (var i = 0; i < bits10; ++i)
            {
                if (bit15[i])
                    packet.Translator.ReadByte("Byte14", i);

                packet.Translator.ReadInt32("Int14", i);

                if (bit14[i])
                    packet.Translator.ReadByte("Byte14", i);

                packet.Translator.ReadInt32("Item Display Id", i);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("Int14", i);
                var len = packet.Translator.ReadInt32("Int50", i);

                packet.Translator.ReadBytes(len);

                packet.Translator.ReadInt32("Item Id", i);
            }

            packet.Translator.ReadXORByte(guid1, 0);

            if (bit50)
                packet.Translator.ReadInt32("Int50");

            if (bit44)
                packet.Translator.ReadByte("Byte44");

            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 1);

            if (bit30)
                packet.Translator.ReadByte("Byte30");

            if (bit20)
                packet.Translator.ReadByte("Byte20");

            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 6);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }
    }
}
