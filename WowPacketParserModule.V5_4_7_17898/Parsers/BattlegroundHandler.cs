using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.SMSG_PVP_LOG_DATA)]
        public static void HandlePvPLogData(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            var arena = packet.Translator.ReadBit("Arena");
            var arenaStrings = packet.Translator.ReadBit("Has Arena Strings");

            var name1Length = 0U;
            var name2Length = 0U;

            if (arenaStrings)
            {
                name1Length = packet.Translator.ReadBits(7);
                guid2[3] = packet.Translator.ReadBit();
                guid2[2] = packet.Translator.ReadBit();
                guid1[7] = packet.Translator.ReadBit();
                guid1[2] = packet.Translator.ReadBit();
                guid1[3] = packet.Translator.ReadBit();
                guid2[4] = packet.Translator.ReadBit();
                name2Length = packet.Translator.ReadBits(7);
                guid2[7] = packet.Translator.ReadBit();
                guid1[6] = packet.Translator.ReadBit();
                guid1[4] = packet.Translator.ReadBit();
                guid2[6] = packet.Translator.ReadBit();
                guid2[0] = packet.Translator.ReadBit();
                guid1[0] = packet.Translator.ReadBit();
                guid2[5] = packet.Translator.ReadBit();
                guid1[1] = packet.Translator.ReadBit();
                guid1[5] = packet.Translator.ReadBit();
                guid2[1] = packet.Translator.ReadBit();
            }

            var count = (int)packet.Translator.ReadBits(19);

            var guids = new byte[count][];
            var bit1C = new bool[count];
            var bit3C = new bool[count];
            var bit44 = new bool[count];
            var bit34 = new bool[count];
            var bits48 = new uint[count];
            var bit2C = new bool[count];

            for (int i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                guids[i][4] = packet.Translator.ReadBit();
                packet.Translator.ReadBit("Unk Bit 1", i);
                guids[i][0] = packet.Translator.ReadBit();
                bit1C[i] = packet.Translator.ReadBit();
                guids[i][7] = packet.Translator.ReadBit();
                guids[i][5] = packet.Translator.ReadBit();
                bit34[i] = packet.Translator.ReadBit();
                guids[i][1] = packet.Translator.ReadBit();
                bit44[i] = packet.Translator.ReadBit();
                packet.Translator.ReadBit("Unk Bit 2", i);
                bits48[i] = packet.Translator.ReadBits(22);
                guids[i][2] = packet.Translator.ReadBit();
                guids[i][3] = packet.Translator.ReadBit();
                bit3C[i] = packet.Translator.ReadBit();
                bit2C[i] = packet.Translator.ReadBit();
                guids[i][6] = packet.Translator.ReadBit();
            }

            var finished = packet.Translator.ReadBit();

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(guids[i], 5);

                packet.Translator.ReadInt32("Int30", i);
                if (bit1C[i])
                {
                    packet.Translator.ReadInt32("Int30", i);
                    packet.Translator.ReadInt32("Int30", i);
                    packet.Translator.ReadInt32("Int30", i);
                }

                packet.Translator.ReadXORByte(guids[i], 4);
                packet.Translator.ReadXORByte(guids[i], 0);
                if (bit3C[i])
                    packet.Translator.ReadInt32("Int30", i);
                packet.Translator.ReadXORByte(guids[i], 2);
                packet.Translator.ReadInt32("Int30", i);
                packet.Translator.ReadXORByte(guids[i], 6);
                packet.Translator.ReadXORByte(guids[i], 7);
                packet.Translator.ReadInt32("Int30", i);
                packet.Translator.ReadXORByte(guids[i], 3);
                packet.Translator.ReadXORByte(guids[i], 1);
                if (bit2C[i])
                    packet.Translator.ReadInt32("Int30", i);
                if (bit44[i])
                    packet.Translator.ReadInt32("Int30", i);
                for (int j = 0; j < bits48[i]; ++j)
                    packet.Translator.ReadInt32("Int30", i);

                if (bit34[i])
                    packet.Translator.ReadInt32("Int30", i);

                packet.Translator.ReadInt32("Int30", i);

                packet.Translator.WriteGuid("Guids", guids[i], i);
            }

            packet.Translator.ReadByte("Unk Byte 1"); // Team Size
            packet.Translator.ReadByte("Unk Byte 2"); // Team size

            if (arenaStrings)
            {
                packet.Translator.ReadXORByte(guid2, 4);
                packet.Translator.ReadXORByte(guid2, 5);
                packet.Translator.ReadXORByte(guid1, 4);
                packet.Translator.ReadXORByte(guid1, 2);
                packet.Translator.ReadXORByte(guid2, 3);
                packet.Translator.ReadXORByte(guid1, 7);
                packet.Translator.ReadWoWString("Team 1 Name", name1Length);
                packet.Translator.ReadXORByte(guid2, 2);
                packet.Translator.ReadXORByte(guid1, 3);
                packet.Translator.ReadXORByte(guid2, 6);
                packet.Translator.ReadXORByte(guid1, 6);
                packet.Translator.ReadXORByte(guid2, 0);
                packet.Translator.ReadXORByte(guid1, 5);
                packet.Translator.ReadXORByte(guid1, 1);
                packet.Translator.ReadXORByte(guid2, 7);
                packet.Translator.ReadXORByte(guid2, 1);
                packet.Translator.ReadXORByte(guid1, 0);
                packet.Translator.ReadWoWString("Team 2 Name", name2Length);

                packet.Translator.WriteGuid("Guid21", guid1);
                packet.Translator.WriteGuid("Guid22", guid2);
            }

            if (finished)
                packet.Translator.ReadByte("Winner");

            if (arena)
            {
                packet.Translator.ReadInt32("Int10");
                packet.Translator.ReadInt32("Int24");
                packet.Translator.ReadInt32("Int1C");
                packet.Translator.ReadInt32("Int18");
                packet.Translator.ReadInt32("Int14");
                packet.Translator.ReadInt32("Int20");
            }
        }

        [Parser(Opcode.CMSG_REQUEST_INSPECT_RATED_BG_STATS)]
        public static void HandleRequestInspectRBGStats(Packet packet)
        {
            packet.Translator.ReadInt32("Realm Id");

            var guid = packet.Translator.StartBitStream(0, 1, 6, 4, 7, 5, 2, 3);
            packet.Translator.ParseBitStream(guid,      2, 0, 1, 3, 4, 7, 5, 6);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_INSPECT_RATED_BG_STATS)]
        public static void HandleInspectRatedBGStats(Packet packet)
        {
            var guid = new byte[8];

            guid[4] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            var bits10 = (int)packet.Translator.ReadBits(3);
            guid[7] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();

            for (var i = 0; i < bits10; i++)
            {
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadByte("Byte14", i);
                packet.Translator.ReadInt32("Int14", i);
                packet.Translator.ReadInt32("Int14", i);
            }

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}
