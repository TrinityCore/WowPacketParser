using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class GameStoreHandler
    {
        [Parser(Opcode.CMSG_GAME_STORE_LIST)]
        public static void HandleGameStoreList(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_GAME_STORE_LIST)]
        public static void HandleGameStoreListResponse(Packet packet)
        {
            var bits10 = packet.ReadBits(19);
            var bits38 = packet.ReadBits(20);

            var bits18 = new uint[bits10];
            var bit143C = new bool[bits10][];
            var bit1440 = new bool[bits10][];
            var bits1C = new uint[bits10][];

            var bit1430 = new bool[bits10][];
            var bit20 = new bool[bits10][];
            var bits24 = new uint[bits10][];
            var bit18 = new bool[bits10][];
            var bit142C = new bool[bits10][];
            var bits225 = new uint[bits10][];
            var bit10 = new bool[bits10][];
            var bits426 = new uint[bits10][];
            var bit28 = new bool[bits10][];

            var bits28 = new uint[bits10];

            var bit1454 = new bool[bits10];
            var bits44A = new uint[bits10];
            var bit44 = new bool[bits10];
            var bit34 = new bool[bits10];
            var bit3C = new bool[bits10];
            var bits48 = new uint[bits10];
            var bit1450 = new bool[bits10];
            var bits249 = new uint[bits10];

            for (var i = 0; i < bits10; i++)
            {
                bits18[i] = packet.ReadBits(20);

                bit143C[i] = new bool[bits18[i]];
                bit1440[i] = new bool[bits18[i]];
                bits1C[i] = new uint[bits18[i]];
                bit1430[i] = new bool[bits18[i]];

                bit1430[i] = new bool[bits18[i]];
                bit20[i] = new bool[bits18[i]];
                bits24[i] = new uint[bits18[i]];
                bit18[i] = new bool[bits18[i]];
                bit142C[i] = new bool[bits18[i]];
                bits225[i] = new uint[bits18[i]];
                bit10[i] = new bool[bits18[i]];
                bits426[i] = new uint[bits18[i]];
                bit28[i] = new bool[bits18[i]];

                for (var j = 0; j < bits18[i]; j++)
                {
                    bit143C[i][j] = packet.ReadBit();
                    bit1440[i][j] = packet.ReadBit();

                    if (bit143C[i][j])
                        bits1C[i][j] = packet.ReadBits(4);

                    bit1440[i][j] = packet.ReadBit();
                    if (bit1440[i][j])
                    {
                        bit20[i][j] = packet.ReadBit();
                        bits24[i][j] = packet.ReadBits(10);
                        bit18[i][j] = packet.ReadBit();
                        bit142C[i][j] = packet.ReadBit();
                        bits225[i][j] = packet.ReadBits(10);
                        bit10[i][j] = packet.ReadBit();
                        bits426[i][j] = packet.ReadBits(13);
                    }

                    bit28[i][j] = packet.ReadBit();
                }

                bits28[i] = packet.ReadBits(2);
                bit1454[i] = packet.ReadBit();

                if (bit1454[i])
                {
                    bits44A[i] = packet.ReadBits(13);
                    bit44[i] = packet.ReadBit();
                    bit34[i] = packet.ReadBit();
                    bit3C[i] = packet.ReadBit();
                    bits48[i] = packet.ReadBits(10);
                    bit1450[i] = packet.ReadBit();
                    bits249[i] = packet.ReadBits(10);
                }
            }

            var bits20 = packet.ReadBits(19);

            var bit143 = new bool[bits20];

            var bits30 = new uint[bits20];
            var bits432 = new uint[bits20];
            var bit1C = new bool[bits20];
            var bit24 = new bool[bits20];
            var bit2C = new bool[bits20];
            var bit1438 = new bool[bits20];
            var bits231 = new uint[bits20];

            for (var i = 0; i < bits20; i++)
            {
                bit143[i] = packet.ReadBit();

                if (bit143[i])
                {
                    bits30[i] = packet.ReadBits(10);
                    bits432[i] = packet.ReadBits(13);
                    bit1C[i] = packet.ReadBit();
                    bit24[i] = packet.ReadBit();
                    bit2C[i] = packet.ReadBit();
                    bit1438[i] = packet.ReadBit();
                    bits231[i] = packet.ReadBits(10);
                }
            }

            var bits3C = new uint[bits38];
            for (var i = 0; i < bits38; i++)
                bits3C[i] = packet.ReadBits(8);

            for (var i = 0; i < bits38; i++)
            {
                packet.ReadWoWString("Category", bits3C[i], i);
                packet.ReadInt32("Int3C", i);
                packet.ReadInt32("Int3C", i);
                packet.ReadInt32("Int3C", i);
                packet.ReadByte("Byte3C", i);
            }

            for (var i = 0; i < bits10; i++)
            {
                packet.ReadByte("ByteED", i);

                if (bit1454[i])
                {
                    packet.ReadWoWString("Item Name", bits48[i], i);

                    if (bit34[i])
                        packet.ReadInt32("Display Id", i);

                    if (bit3C[i])
                        packet.ReadInt32("IntED", i);

                    packet.ReadWoWString("Description", bits44A[i], i);

                    if (bit44[i])

                        packet.ReadInt32("IntED", i);

                    if (bit1450[i])
                        packet.ReadInt32("IntED", i);

                    packet.ReadWoWString("StringED", bits249[i], i);
                }

                packet.ReadInt64("Price", i);
                packet.ReadInt32("Int14", i);

                for (var j = 0; j < bits18[i]; j++)
                {
                    if (bit143C[i][j])
                    {
                        packet.ReadWoWString("String225", bits225[i][j], i, j);

                        if (bit142C[i][j])
                            packet.ReadInt32("IntED", i, j);

                        packet.ReadWoWString("StringED", bits426[i][j], i, j);

                        if (bit18[i][j])
                            packet.ReadUInt32<ItemId>("Item Id", i, j);

                        if (bit10[i][j])
                            packet.ReadInt32("IntED", i, j);

                        if (bit20[i][j])
                            packet.ReadInt32("IntED", i, j);

                        packet.ReadWoWString("StringED", bits24[i][j], i, j);
                    }

                    packet.ReadUInt32<ItemId>("Item Id", i, j);
                    packet.ReadInt32("Int14", i, j);
                    packet.ReadInt32("Buy Count", i, j);
                }

                packet.ReadInt64("Price", i);
                packet.ReadInt32("IntED", i);
            }

            for (var i = 0; i < bits20; i++)
            {
                if (bit143[i])
                {
                    if (bit2C[i])
                        packet.ReadInt32("IntED", i);

                    if (bit24[i])
                        packet.ReadInt32("IntED", i);

                    packet.ReadWoWString("StringED", bits432[i], i);
                    packet.ReadWoWString("StringED", bits30[i], i);

                    if (bit1C[i])
                        packet.ReadInt32("IntED", i);

                    packet.ReadWoWString("StringED", bits231[i], i);

                    if (bit1438[i])
                        packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);
                packet.ReadByte("ByteED", i);
                packet.ReadInt32("Int24", i);
            }

            packet.ReadInt32("Int30");
            packet.ReadInt32("Int34");
        }

        [Parser(Opcode.CMSG_GAME_STORE_BUY)]
        public static void HandlGameStoreBuy(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Int18");
            packet.ReadInt32("Int1C");

            packet.StartBitStream(guid, 7, 2, 0, 6, 3, 5, 1, 4);
            packet.ParseBitStream(guid, 1, 5, 4, 6, 7, 2, 3, 0);

            packet.WriteGuid("Guid", guid);

        }

        [Parser(Opcode.SMSG_GAME_STORE_BUY_RESULT)]
        public static void HandlGameStoreBuyResult(Packet packet)
        {
            packet.ReadInt32("Int1C");
            packet.ReadInt32("Int18");
            packet.ReadInt64("Int10");

        }

        [Parser(Opcode.SMSG_GAME_STORE_AUTH_BUY_FAILED)]
        public static void HandleGameStoreAuthBuyFailed(Packet packet)
        {
            var bits10 = packet.ReadBits(19);

            var bits0 = new uint[bits10];

            for (var i = 0; i < bits10; ++i)
                bits0[i] = packet.ReadBits(8);

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadInt32("IntED", i);
                packet.ReadWoWString("StringED", bits0[i], i);
                packet.ReadInt64("IntED", i);
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);
            }
        }

        [Parser(Opcode.SMSG_GAME_STORE_INGAME_BUY_FAILED)]
        public static void HandleGameStoreIngameBuyFailed(Packet packet)
        {
            packet.ReadInt32("Int20");
            var bits10 = packet.ReadBits(19);

            var bits0 = new uint[bits10];

            for (var i = 0; i < bits10; ++i)
                bits0[i] = packet.ReadBits(8);

            for (var i = 0; i < bits10; ++i)
            {
                packet.ReadWoWString("StringED", bits0[i], i);
                packet.ReadInt32("IntED", i);
                packet.ReadInt64("IntED", i);
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);
            }
        }
    }
}