using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class BattlePetHandler
    {
        [Parser(Opcode.SMSG_BATTLE_PET_DELETED)]
        public static void HandleBattlePetDeleted(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 2, 3, 4, 7, 1, 0, 5, 6);
            packet.ParseBitStream(guid, 7, 3, 0, 4, 2, 5, 1, 6);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_PET_BATTLE_ROUND_RESULT)]
        public static void HandleBattlePetRoundResult(Packet packet)
        {

            var bits18 = 0;
            var bits28 = 0;
            var bits38 = 0;

            for (var i = 0; i < 2; ++i)
            {
                packet.ReadByte("Byte4A", i);
                packet.ReadByte("Byte4A", i);
                packet.ReadUInt16("Int53", i);
            }

            packet.ReadInt32("Int10"); // v3+4
            bits38 = (int)packet.ReadBits(20); // v3+56
            bits28 = (int)packet.ReadBits(3); // v3+40

            var bit9 = new bool[bits38];
            var bit14 = false;

            for (var i = 0; i < bits38; ++i)
            {
                bit9[i] = !packet.ReadBit(); // v3+15
            }

            bit14 = !packet.ReadBit(); //v3+20
            bits18 = (int)packet.ReadBits(22); // v3+24

            var bit4 = new bool[bits18];
            var bit6 = new bool[bits18];
            var bit8 = new bool[bits18];
            var bit80 = new bool[bits18][];
            var bitA = new bool[bits18];
            var bitB = new bool[bits18];
            var bitC = new bool[bits18];
            var bitCC = new bool[bits18][];
            var bit10 = new bool[bits18];
            var bit100 = new bool[bits18][];
            var bit18 = new bool[bits18][];
            var bit140 = new bool[bits18][];
            var bit1C = new bool[bits18][];
            var bit20 = new bool[bits18][];
            var bit24 = new bool[bits18][];
            var bit28 = new bool[bits18][];
            var bit2C = new bool[bits18][];
            var bit30 = new bool[bits18][];
            var bit34 = new bool[bits18][];
            var bit38 = new bool[bits18][];
            var bitEB = new bool[bits18];

            var bits10 = new uint[bits18];
            var bitsED = new uint[bits18][];

            for (var i = 0; i < bits18; ++i)
            {
                bitB[i] = !packet.ReadBit();//v3+11
                bit4[i] = !packet.ReadBit();//v3+4
                bitC[i] = !packet.ReadBit();//v3+12
                bit8[i] = !packet.ReadBit();//v3+8
                bitA[i] = !packet.ReadBit();//v3+10
                bit6[i] = !packet.ReadBit();//v3+6
                bitEB[i] = !packet.ReadBit();//v3+0
                bits10[i] = packet.ReadBits(25);//v3 + 7 + 16

                //case 0
                bit28[i] = new bool[bits10[i]];
                //case 1
                bitCC[i] = new bool[bits10[i]];
                bit140[i] = new bool[bits10[i]];
                bit100[i] = new bool[bits10[i]];
                bit80[i] = new bool[bits10[i]];
                //case 2
                bit34[i] = new bool[bits10[i]];
                bit2C[i] = new bool[bits10[i]];
                bit30[i] = new bool[bits10[i]];
                //case 3
                bit18[i] = new bool[bits10[i]];
                bit1C[i] = new bool[bits10[i]];
                //case 4
                bit24[i] = new bool[bits10[i]];
                //case 5
                bit38[i] = new bool[bits10[i]];
                //case 7
                bit20[i] = new bool[bits10[i]];
                bitsED[i] = new uint[bits10[i]];

                for (var j = 0; j < bits10[i]; ++j)
                {
                    bit1C[i][j] = !packet.ReadBit(); // v3 + 7 + 20 + 4
                    bitsED[i][j] = packet.ReadBits(3); //v3 + 7 + 20

                    switch (bitsED[i][j])
                    {
                        case 0:
                            bit28[i][j] = !packet.ReadBit(); break; //40
                        case 1:
                            {
                                bit80[i][j] = !packet.ReadBit();//8
                                bit140[i][j] = !packet.ReadBit(); // 20
                                bitCC[i][j] = !packet.ReadBit();//12
                                bit100[i][j] = !packet.ReadBit();// 16
                                break;
                            }
                        case 2:
                            {
                                bit2C[i][j] = !packet.ReadBit(); //44
                                bit34[i][j] = !packet.ReadBit(); //52
                                bit30[i][j] = !packet.ReadBit(); //48
                                break;
                            }
                        case 3:
                            {
                                bit18[i][j] = !packet.ReadBit(); //24
                                bit1C[i][j] = !packet.ReadBit(); //28
                                break;
                            }
                        case 4:
                            bit24[i][j] = !packet.ReadBit(); break; //36
                        case 5:
                            bit38[i][j] = !packet.ReadBit(); break; //32
                        case 7:
                            bit20[i][j] = !packet.ReadBit(); break; //56
                    }
                }
            }

            packet.ResetBitReader();

            for (var i = 0; i < bits18; ++i)
            {
                for (var j = 0; j < bits10[i]; ++j)
                {
                    if (bit1C[i][j])
                        packet.ReadByte("Byte14", i, j);

                    switch (bitsED[i][j])
                    {
                        case 0:
                            {
                                if (bit28[i][j])
                                    packet.ReadInt32("Int28", i, j);
                                break;
                            }
                        case 1:
                            {
                                if (bit140[i][j])
                                    packet.ReadInt32("Int14", i, j);
                                if (bit100[i][j])
                                    packet.ReadInt32("Int10", i, j);
                                if (bitCC[i][j])
                                    packet.ReadInt32("IntC", i, j);
                                if (bit80[i][j])
                                    packet.ReadInt32("Int8", i, j);
                                break;
                            }
                        case 2:
                            {
                                if (bit30[i][j])
                                    packet.ReadInt32("Int30", i, j);
                                if (bit2C[i][j])
                                    packet.ReadInt32("Int2C", i, j);
                                if (bit34[i][j])
                                    packet.ReadInt32("Int34", i, j);
                                break;
                            }
                        case 3:
                            {
                                if (bit18[i][j])
                                    packet.ReadInt32("Int18", i, j);
                                if (bit1C[i][j])
                                    packet.ReadInt32("Int1C", i, j);
                                break;
                            }
                        case 4:
                            {
                                if (bit24[i][j])
                                    packet.ReadInt32("Int24", i, j);
                                break;
                            }
                        case 5:
                            {
                                if (bit38[i][j])
                                    packet.ReadInt32("Int38", i, j);
                                break;
                            }
                        case 7:
                            {
                                if (bit20[i][j])
                                    packet.ReadInt32("Int20", i, j);
                                break;
                            }
                    }
                    if (bit4[i])
                        packet.ReadInt16("Int4", i);
                    if (bitB[i])
                        packet.ReadByte("ByteB", i);
                    if (bit6[i])
                        packet.ReadInt16("Int6", i);
                    if (bitA[i])
                        packet.ReadByte("ByteA", i);
                    if (bitEB[i])
                        packet.ReadInt32("IntEB", i);
                    if (bit8[i])
                        packet.ReadInt16("Int8", i);
                    if (bitC[i])
                        packet.ReadByte("ByteC", i);
                }
            }

            for (var i = 0; i < bits38; ++i)
            {
                packet.ReadInt16("Int6", i);
                packet.ReadInt16("Int4", i);
                packet.ReadInt32("Int15", i);
                if (bit9[i])
                    packet.ReadByte("Byte24", i);
                packet.ReadByte("Byte8", i);
            }

            if (bit14)
                packet.ReadByte("Byte20");

            for (var i = 0; i < bits28; ++i)
            {
                packet.ReadByte("Byte2C", i);
            }
        }

        [Parser(Opcode.SMSG_PET_BATTLE_MAX_GAME_LENGTH_WARNING)]
        public static void HandleBattlePetMaxGameLengthWarning(Packet packet)
        {
            var bit14 = !packet.ReadBit();
            packet.ReadInt32("Int10");
            if (bit14)
                packet.ReadInt32("Int14");
        }

        [Parser(Opcode.SMSG_PET_BATTLE_PVP_CHALLENGE)]
        public static void HandleBattlePetPvpChallenge(Packet packet)
        {
            var guid = new byte[8];

            guid[4] = packet.ReadBit();
            var bit10 = !packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var bitA = !packet.ReadBit();

            if (bit10)
                packet.ReadSingle("Float20");

            packet.ReadXORByte(guid, 1);
            packet.ReadSingle("Float18");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadSingle("Float1C");
            if (bit10)
                packet.ReadInt32("Int10");
            packet.ReadXORByte(guid, 2);
            for (var i = 0; i < 2; ++i)
            {
                packet.ReadSingle("Float4");
                packet.ReadSingle("Float28");
                packet.ReadSingle("Float28");
            }

            packet.ReadSingle("Float14");
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_PET_BATTLE_FIRST_ROUND)]
        public static void HandleBattlePetFirstRound(Packet packet)
        {

            var bits18 = 0;
            bits18 = (int)packet.ReadBits(22);
            var bits10 = new uint[bits18];

            var bits28 = 0;
            var bits38 = 0;
            var bitsED = new uint[bits18][];


            var bit4 = new bool[bits18];
            var bit6 = new bool[bits18];
            var bit8 = new bool[bits18];
            var bit80 = new bool[bits18][];
            var bit9 = new bool[bits38];
            var bitA = new bool[bits18];
            var bitB = new bool[bits18];
            var bitC = new bool[bits18];
            var bitCC = new bool[bits18][];
            var bit10 = new bool[bits18];
            var bit100 = new bool[bits18][];
            var bit14 = false;
            var bit140 = new bool[bits18][];
            var bit18 = new bool[bits18][];
            var bit1C = new bool[bits18][];
            var bit20 = new bool[bits18][];
            var bit24 = new bool[bits18][];
            var bit28 = new bool[bits18][];
            var bit2C = new bool[bits18][];
            var bit30 = new bool[bits18][];
            var bit34 = new bool[bits18][];
            var bit38 = new bool[bits18][];
            var bitEB = new bool[bits18];

            for (var i = 0; i < bits18; ++i)
            {
                bitEB[i] = !packet.ReadBit();
                bitA[i] = !packet.ReadBit();
                bit4[i] = !packet.ReadBit();
                bitB[i] = !packet.ReadBit();
                bit6[i] = !packet.ReadBit();
                bitC[i] = !packet.ReadBit();
                bit8[i] = !packet.ReadBit();
                bits10[i] = packet.ReadBits(25);

                //case 1
                bitCC[i] = new bool[bits10[i]];
                bit140[i] = new bool[bits10[i]];
                bit100[i] = new bool[bits10[i]];
                bit80[i] = new bool[bits10[i]];
                //case 2
                bit34[i] = new bool[bits10[i]];
                bit2C[i] = new bool[bits10[i]];
                bit30[i] = new bool[bits10[i]];
                //case 3
                bit18[i] = new bool[bits10[i]];
                bit1C[i] = new bool[bits10[i]];
                //case 4
                bit24[i] = new bool[bits10[i]];
                //case 5
                bit38[i] = new bool[bits10[i]];
                //case 7
                bit20[i] = new bool[bits10[i]];
                bitsED[i] = new uint[bits10[i]];

                for (var j = 0u; j < bits10[i]; ++j)
                {
                    bit1C[i][j] = !packet.ReadBit();
                    bitsED[i][j] = packet.ReadBits(3);

                    switch (bitsED[i][j])
                    {
                        case 0:
                            bit28[i][j] = !packet.ReadBit(); break;
                        case 1:
                            {
                                bitCC[i][j] = !packet.ReadBit();
                                bit140[i][j] = !packet.ReadBit();
                                bit100[i][j] = !packet.ReadBit();
                                bit80[i][j] = !packet.ReadBit();
                                break;
                            }
                        case 2:
                            {
                                bit34[i][j] = !packet.ReadBit();
                                bit2C[i][j] = !packet.ReadBit();
                                bit30[i][j] = !packet.ReadBit();
                                break;
                            }
                        case 3:
                            {
                                bit18[i][j] = !packet.ReadBit();
                                bit1C[i][j] = !packet.ReadBit();
                                break;
                            }
                        case 4:
                            bit24[i][j] = !packet.ReadBit(); break;
                        case 5:
                            bit38[i][j] = !packet.ReadBit(); break;
                        case 7:
                            bit20[i][j] = !packet.ReadBit(); break;
                    }
                }
            }

            bits28 = (int)packet.ReadBits(3);
            bit14 = !packet.ReadBit();
            bits38 = (int)packet.ReadBits(20);

            for (var i = 0; i < bits38; ++i)
            {
                bit9[i] = !packet.ReadBit();
            }

            packet.ResetBitReader();

            for (var i = 0; i < bits38; ++i)
            {
                if (bit9[i])
                {
                    packet.ReadByte("Byte9", i);
                }
                packet.ReadInt16("IntED", i);
                packet.ReadByte("ByteED", i);
                packet.ReadInt32("Int3C", i);
                packet.ReadInt16("IntED", i);
            }

            for (var i = 0; i < bits18; ++i)
            {
                if (bit8[i])
                    packet.ReadInt16("Int8", i);

                for (var j = 0; j < bits10[i]; ++j)
                {
                    switch (bitsED[i][j])
                    {
                        case 0:
                            {
                                if (bit28[i][j])
                                    packet.ReadInt32("Int28", i, j);
                                break;
                            }
                        case 1:
                            {
                                if (bit140[i][j])
                                    packet.ReadInt32("Int14", i, j);
                                if (bit100[i][j])
                                    packet.ReadInt32("Int10", i, j);
                                if (bitCC[i][j])
                                    packet.ReadInt32("IntC", i, j);
                                if (bit80[i][j])
                                    packet.ReadInt32("Int8", i, j);
                                break;
                            }
                        case 2:
                            {
                                if (bit30[i][j])
                                    packet.ReadInt32("Int30", i, j);
                                if (bit2C[i][j])
                                    packet.ReadInt32("Int2C", i, j);
                                if (bit34[i][j])
                                    packet.ReadInt32("Int34", i, j);
                                break;
                            }
                        case 3:
                            {
                                if (bit18[i][j])
                                    packet.ReadInt32("Int18", i, j);
                                if (bit1C[i][j])
                                    packet.ReadInt32("Int1C", i, j);
                                break;
                            }
                        case 4:
                            {
                                if (bit24[i][j])
                                    packet.ReadInt32("Int24", i, j);
                                break;
                            }
                        case 5:
                            {
                                if (bit38[i][j])
                                    packet.ReadInt32("Int38", i, j);
                                break;
                            }
                        case 7:
                            {
                                if (bit20[i][j])
                                    packet.ReadInt32("Int20", i, j);
                                break;
                            }
                    }

                    if (bit1C[i][j])
                        packet.ReadByte("Byte14", i, j);
                }

                if (bit6[i])
                    packet.ReadInt16("Int6", i);
                if (bitB[i])
                    packet.ReadByte("ByteB", i);
                if (bitC[i])
                    packet.ReadByte("ByteC", i);
                if (bitEB[i])
                    packet.ReadInt32("IntEB", i);
                if (bitA[i])
                    packet.ReadByte("ByteA", i);
                if (bit4[i])
                    packet.ReadInt16("Int4", i);
            }

            if (bit14)
                packet.ReadByte("Byte14");

            for (var i = 0; i < 2; ++i)
            {
                packet.ReadInt16("Int4C", i);
                packet.ReadByte("Byte4A", i);
                packet.ReadByte("Byte4A", i);
            }

            packet.ReadInt32("Int10");

            for (var i = 0; i < bits28; ++i)
            {
                packet.ReadByte("Byte2C", i);
            }
        }
        [Parser(Opcode.SMSG_PET_BATTLE_SLOT_UPDATES)]
        public static void HandleBattleSlotUpdate(Packet packet)
        {
            var bits10 = 0;
            bits10 = (int)packet.ReadBits(25);

            var guid2 = new byte[bits10][];
            var bit8 = new bool[bits10];
            var bit12 = new bool[bits10];
            var bit13 = new bool[bits10];
            var bit21 = packet.ReadBit();

            for (var i = 0; i < bits10; ++i)
            {
                bit13[i] = packet.ReadBit();
                bit8[i] = !packet.ReadBit();
                bit12[i] = !packet.ReadBit();
                packet.ReadBit(); // fake bit
                guid2[i] = new byte[8];
                packet.StartBitStream(guid2[i], 1, 3, 2, 4, 7, 6, 0, 5);
            }

            var bit20 = packet.ReadBit();

            packet.ResetBitReader();

            for (var i = 0; i < bits10; ++i)
            {
                packet.ParseBitStream(guid2[i], 7, 5, 0, 3, 1, 4, 6, 2);

                if (bit12[i])
                    packet.ReadByte("Byte14", i);
                if (bit8[i])
                    packet.ReadInt32("Int14", i);

                packet.WriteGuid("GUID", guid2[i], i);
            }
        }

        [Parser(Opcode.SMSG_PET_BATTLE_FINAL_ROUND)]
        public static void HandleBattleFinalRound(Packet packet)
        {
            var bits14 = 0;
            bits14 = (int)packet.ReadBits(20);

            var bit1 = new bool[bits14];
            var bit2 = new bool[bits14];
            var bit3 = new bool[bits14];
            var bit4 = new bool[bits14];
            var bit6 = new bool[bits14];
            var bit10 = new bool[bits14];
            var battleAbandon = false;
            var bit24 = false;
            var bitEB = new bool[bits14];


            for (var i = 0; i < 2; ++i) // for win/lose
            {
                bit24 = packet.ReadBit("Win/Lose", i);
            }

            for (var i = 0; i < bits14; ++i)
            {
                bit3[i] = packet.ReadBit();
                bit1[i] = packet.ReadBit();
                bit2[i] = packet.ReadBit();
                bitEB[i] = packet.ReadBit();
                bit4[i] = !packet.ReadBit("unk4", i);
                bit6[i] = !packet.ReadBit("unk6", i);
                bit10[i] = !packet.ReadBit("unk10", i);
            }

            var bit11 = packet.ReadBit();
            battleAbandon = packet.ReadBit("battleAbandon");

            for (var i = 0; i < 2; ++i)
            {
                packet.ReadInt32("Int28", i);
            }

            for (var i = 0; i < bits14; ++i)
            {
                packet.ReadByte("Slot", i);

                if (bit4[i])
                    packet.ReadInt16("Level after fight", i);
                if (bit6[i])
                    packet.ReadInt16("Experience", i);

                packet.ReadInt32("Health after fight", i);
                packet.ReadInt32("Health", i);

                if (bit10[i])
                    packet.ReadInt16("Level before fight", i);
            }
        }
        [Parser(Opcode.SMSG_BATTLE_PET_ERROR)]
        public static void HandleBattlePetError(Packet packet)
        {
            var bits14 = 0;

            var bit10 = !packet.ReadBit();
            var bit14 = !packet.ReadBit();

            if (bit14)
                bits14 = (int)packet.ReadBits(4);

            if (bit10)
                packet.ReadInt32("Int10");
        }
        [Parser(Opcode.SMSG_PET_BATTLE_QUEUE_STATUS)]
        public static void HandleBattlePetQueueStatus(Packet packet)
        {
            var guid2 = new byte[8];

            var bits28 = 0;

            guid2[5] = packet.ReadBit();
            bits28 = (int)packet.ReadBits(22);
            var bit48 = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            var bit3C = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            packet.ReadInt32("Int20");
            packet.ReadXORByte(guid2, 4);
            packet.ReadInt32("Int40");
            if (bit3C)
                packet.ReadInt32("Int38");
            packet.ReadXORByte(guid2, 3);
            packet.ReadInt32("Int1C");
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 1);
            if (bit48)
                packet.ReadInt32("Int44");

            for (var i = 0; i < bits28; ++i)
            {
                packet.ReadInt32("IntED", i);
            }

            packet.ReadInt32("Int18");
            packet.ReadXORByte(guid2, 6);

            packet.WriteGuid("GUID", guid2);
        }

        [Parser(Opcode.SMSG_BATTLE_PET_TRAP_LEVEL)]
        public static void HandleBattlePetTrapLevel(Packet packet)
        {
            packet.ReadInt16("Int10");
        }

        [Parser(Opcode.SMSG_BATTLE_PET_UPDATES)]
        public static void HandleBattlePetUpdates(Packet packet)
        {
            var bits10 = 0;

            bits10 = (int)packet.ReadBits(22);

            for (var i = 0; i < bits10; ++i)
            {
                    packet.ReadInt32("IntED");
            }
        }

        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL)]
        public static void HandleBattlePetJournal(Packet packet)
        {
            byte[][] guid1;
            byte[][] guid2;
            byte[][] guid3;

            var bits36 = packet.ReadBits(25);
            var bits20 = packet.ReadBits(19);
            var bits44 = new uint[bits20];
            var bit137 = new bool[bits20];
            var bit20 = new bool[bits20];
            var bit26 = new bool[bits20];
            var bit128 = new bool[bits20];
            var bit136 = new bool[bits20];

            guid1 = new byte[bits20][];
            guid2 = new byte[bits20][];
            for (var i = 0; i < bits20; ++i)
            {
                guid1[i] = new byte[8];

                bits44[i] = packet.ReadBits(7);
                bit137[i] = !packet.ReadBit();
                guid1[i][3] = packet.ReadBit();
                bit20[i] = !packet.ReadBit();
                guid1[i][5] = packet.ReadBit();
                guid1[i][0] = packet.ReadBit();
                bit26[i] = !packet.ReadBit();
                bit128[i] = packet.ReadBit();
                guid1[i][2] = packet.ReadBit(); // v7 0-7

                if (bit128[i])
                {
                    guid2[i] = new byte[8];
                    packet.StartBitStream(guid2[i], 7, 1, 6, 2, 4, 3, 0, 5); // 112-119
                }

                bit136[i] = packet.ReadBit();
                guid1[i][7] = packet.ReadBit();
                guid1[i][4] = packet.ReadBit();
                guid1[i][6] = packet.ReadBit();
                guid1[i][1] = packet.ReadBit();
            }


            guid3 = new byte[bits36][];

            var bit8 = new bool[bits36];
            var bit13 = new bool[bits36];
            var bit12 = new bool[bits36];

            for (var i = 0; i < bits36; ++i)
            {
                bit8[i] = !packet.ReadBit();
                bit13[i] = packet.ReadBit();
                bit12[i] = !packet.ReadBit();

                packet.ReadBit(); // fake bit

                guid3[i] = new byte[8];
                packet.StartBitStream(guid3[i], 3, 5, 0, 4, 1, 6, 2, 7); //v11 0-7
            }

            packet.ReadBit("Unk bit");

            for (var i = 0; i < bits36; ++i)
            {
                packet.ReadXORByte(guid3[i], 2);
                packet.ReadXORByte(guid3[i], 7);
                packet.ReadXORByte(guid3[i], 3);
                packet.ReadXORByte(guid3[i], 5);
                packet.ReadXORByte(guid3[i], 1);
                packet.ReadXORByte(guid3[i], 0);
                packet.ReadXORByte(guid3[i], 6);
                packet.ReadXORByte(guid3[i], 4);

                if (bit8[i])
                    packet.ReadInt32("Int-8", i);
                if (bit12[i])
                    packet.ReadByte("Byte12", i);

                packet.WriteGuid("Guid3", guid3[i], i);
            }

            for (var i = 0; i < bits20; ++i)
            {
                if (bit26[i])
                    packet.ReadInt16("Int-26", i);

                packet.ReadInt32("Health", i);
                packet.ReadInt32("Speed", i);

                if (bit20[i])
                    packet.ReadInt16("Breed at this pet", i);

                if (bit128[i])
                {
                    packet.ReadXORByte(guid2[i], 2);
                    packet.ReadInt32("Int-124", i);
                    packet.ReadXORByte(guid2[i], 5);
                    packet.ReadXORByte(guid2[i], 6);
                    packet.ReadInt32("Int-120", i);
                    packet.ReadXORByte(guid2[i], 0);
                    packet.ReadXORByte(guid2[i], 4);
                    packet.ReadXORByte(guid2[i], 3);
                    packet.ReadXORByte(guid2[i], 1);
                    packet.ReadXORByte(guid2[i], 7);
                    packet.WriteGuid("Guid2", guid2[i], i);
                }
                if (bit137[i])
                    packet.ReadByte("Quality", i);

                packet.ReadXORByte(guid1[i], 5);
                packet.ReadInt16("Actual Experiance at Level", i);
                packet.ReadXORByte(guid1[i], 2);
                packet.ReadXORByte(guid1[i], 1);
                packet.ReadXORByte(guid1[i], 6);
                packet.ReadInt32("Max Health", i);
                packet.ReadInt32("Int-16", i);
                packet.ReadInt32("Int-8", i);
                packet.ReadInt32("Entry", i);
                packet.ReadInt32("Power", i);
                packet.ReadXORByte(guid1[i], 3);
                packet.ReadInt16("Level", i);
                packet.ReadXORByte(guid1[i], 0);
                packet.ReadXORByte(guid1[i], 4);
                packet.ReadWoWString("Custom Name", bits44[i], i);
                packet.ReadXORByte(guid1[i], 7);
                packet.WriteGuid("Guid1", guid1[i], i);
            }

            packet.ReadInt16("Int-26");
        }

        [Parser(Opcode.SMSG_BATTLEPET_CAGE_DATA_ERROR)]
        public static void HandleBattlePetCageDataError(Packet packet)
        {
            packet.ReadInt32("Int10");
        }

        [Parser(Opcode.CMSG_QUERY_BATTLE_PET_NAME)]
        public static void HandleBattlePetQuery(Packet packet)
        {
            var guid = new byte[8];
            var playerGUID = new byte[8];

            playerGUID[4] = packet.ReadBit();
            playerGUID[1] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            playerGUID[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            playerGUID[0] = packet.ReadBit();
            playerGUID[7] = packet.ReadBit();
            playerGUID[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            playerGUID[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            playerGUID[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(playerGUID, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(playerGUID, 1);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(playerGUID, 6);
            packet.ReadXORByte(playerGUID, 3);
            packet.ReadXORByte(playerGUID, 0);
            packet.ReadXORByte(playerGUID, 4);
            packet.ReadXORByte(playerGUID, 7);
            packet.ReadXORByte(playerGUID, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 1);

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Player GUID", playerGUID);

        }

        [Parser(Opcode.SMSG_QUERY_BATTLE_PET_NAME_RESPONSE)]
        public static void HandleBattlePetQueryResponse(Packet packet)
        {
            var bits19 = 0u;
            var count = 5;
            var bits9B = new uint[count];

            packet.ReadInt64("Int10");
            packet.ReadInt32("Int280");
            packet.ReadInt32("Entry");

            var bit18 = packet.ReadBit();
            if (bit18)
            {
                bits19 = packet.ReadBits(8);

                for (var i = 0; i < count; ++i)
                    bits9B[i] = packet.ReadBits(7);
            }

            if (bit18)
            {
                packet.ReadWoWString("Custom Name", bits19);

                for (var i = 0; i < count; ++i)
                    packet.ReadWoWString("String9B", bits9B[i]);
            }
        }

        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_ACQUIRED)]
        [Parser(Opcode.SMSG_PET_BATTLE_QUEUE_PROPOSE_MATCH)]
        public static void HandleZeroLengthPackets(Packet packet)
        {
        }
    }
}
