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

            var arena = packet.ReadBit("Arena");
            var arenaStrings = packet.ReadBit("Has Arena Strings");

            var name1Length = 0U;
            var name2Length = 0U;

            if (arenaStrings)
            {
                name1Length = packet.ReadBits(7);
                guid2[3] = packet.ReadBit();
                guid2[2] = packet.ReadBit();
                guid1[7] = packet.ReadBit();
                guid1[2] = packet.ReadBit();
                guid1[3] = packet.ReadBit();
                guid2[4] = packet.ReadBit();
                name2Length = packet.ReadBits(7);
                guid2[7] = packet.ReadBit();
                guid1[6] = packet.ReadBit();
                guid1[4] = packet.ReadBit();
                guid2[6] = packet.ReadBit();
                guid2[0] = packet.ReadBit();
                guid1[0] = packet.ReadBit();
                guid2[5] = packet.ReadBit();
                guid1[1] = packet.ReadBit();
                guid1[5] = packet.ReadBit();
                guid2[1] = packet.ReadBit();
            }

            var count = (int)packet.ReadBits(19);

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
                guids[i][4] = packet.ReadBit();
                packet.ReadBit("Unk Bit 1", i);
                guids[i][0] = packet.ReadBit();
                bit1C[i] = packet.ReadBit();
                guids[i][7] = packet.ReadBit();
                guids[i][5] = packet.ReadBit();
                bit34[i] = packet.ReadBit();
                guids[i][1] = packet.ReadBit();
                bit44[i] = packet.ReadBit();
                packet.ReadBit("Unk Bit 2", i);
                bits48[i] = packet.ReadBits(22);
                guids[i][2] = packet.ReadBit();
                guids[i][3] = packet.ReadBit();
                bit3C[i] = packet.ReadBit();
                bit2C[i] = packet.ReadBit();
                guids[i][6] = packet.ReadBit();
            }

            var finished = packet.ReadBit();

            for (int i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guids[i], 5);

                packet.ReadInt32("Int30", i);
                if (bit1C[i])
                {
                    packet.ReadInt32("Int30", i);
                    packet.ReadInt32("Int30", i);
                    packet.ReadInt32("Int30", i);
                }

                packet.ReadXORByte(guids[i], 4);
                packet.ReadXORByte(guids[i], 0);
                if (bit3C[i])
                    packet.ReadInt32("Int30", i);
                packet.ReadXORByte(guids[i], 2);
                packet.ReadInt32("Int30", i);
                packet.ReadXORByte(guids[i], 6);
                packet.ReadXORByte(guids[i], 7);
                packet.ReadInt32("Int30", i);
                packet.ReadXORByte(guids[i], 3);
                packet.ReadXORByte(guids[i], 1);
                if (bit2C[i])
                    packet.ReadInt32("Int30", i);
                if (bit44[i])
                    packet.ReadInt32("Int30", i);
                for (int j = 0; j < bits48[i]; ++j)
                    packet.ReadInt32("Int30", i);

                if (bit34[i])
                    packet.ReadInt32("Int30", i);

                packet.ReadInt32("Int30", i);

                packet.WriteGuid("Guids", guids[i], i);
            }

            packet.ReadByte("Unk Byte 1"); // Team Size
            packet.ReadByte("Unk Byte 2"); // Team size

            if (arenaStrings)
            {
                packet.ReadXORByte(guid2, 4);
                packet.ReadXORByte(guid2, 5);
                packet.ReadXORByte(guid1, 4);
                packet.ReadXORByte(guid1, 2);
                packet.ReadXORByte(guid2, 3);
                packet.ReadXORByte(guid1, 7);
                packet.ReadWoWString("Team 1 Name", name1Length);
                packet.ReadXORByte(guid2, 2);
                packet.ReadXORByte(guid1, 3);
                packet.ReadXORByte(guid2, 6);
                packet.ReadXORByte(guid1, 6);
                packet.ReadXORByte(guid2, 0);
                packet.ReadXORByte(guid1, 5);
                packet.ReadXORByte(guid1, 1);
                packet.ReadXORByte(guid2, 7);
                packet.ReadXORByte(guid2, 1);
                packet.ReadXORByte(guid1, 0);
                packet.ReadWoWString("Team 2 Name", name2Length);

                packet.WriteGuid("Guid21", guid1);
                packet.WriteGuid("Guid22", guid2);
            }

            if (finished)
                packet.ReadByte("Winner");

            if (arena)
            {
                packet.ReadInt32("Int10");
                packet.ReadInt32("Int24");
                packet.ReadInt32("Int1C");
                packet.ReadInt32("Int18");
                packet.ReadInt32("Int14");
                packet.ReadInt32("Int20");
            }
        }

        [Parser(Opcode.CMSG_REQUEST_INSPECT_RATED_BG_STATS)]
        public static void HandleRequestInspectRBGStats(Packet packet)
        {
            packet.ReadInt32("Realm Id");

            var guid = packet.StartBitStream(0, 1, 6, 4, 7, 5, 2, 3);
            packet.ParseBitStream(guid,      2, 0, 1, 3, 4, 7, 5, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_INSPECT_RATED_BG_STATS)]
        public static void HandleInspectRatedBGStats(Packet packet)
        {
            var guid = new byte[8];

            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            var bits10 = (int)packet.ReadBits(3);
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            for (var i = 0; i < bits10; i++)
            {
                packet.ReadInt32("Int14", i);
                packet.ReadInt32("Int14", i);
                packet.ReadInt32("Int14", i);
                packet.ReadInt32("Int14", i);
                packet.ReadInt32("Int14", i);
                packet.ReadByte("Byte14", i);
                packet.ReadInt32("Int14", i);
                packet.ReadInt32("Int14", i);
            }

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid", guid);
        }
    }
}
