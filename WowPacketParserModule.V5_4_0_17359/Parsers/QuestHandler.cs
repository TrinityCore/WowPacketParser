using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class QuestHandler
    {
        [Parser(Opcode.CMSG_QUEST_POI_QUERY)]
        public static void HandleQuestPoiQuery(Packet packet)
        {
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; i++) // for (var i = 0; i < 50; i++)
                packet.ReadInt32<QuestId>("Quest ID", i);

            packet.ReadToEnd(); // Hack
        }

        [Parser(Opcode.CMSG_QUERY_QUEST_COMPLETION_NPCS)]
        public static void HandleQuestNpcQuery(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);
            packet.ResetBitReader();

            for (var i = 0; i < count; i++)
                packet.ReadInt32<QuestId>("Quest ID", i);
        }

        [Parser(Opcode.SMSG_QUEST_COMPLETION_NPC_RESPONSE)]
        public static void HandleUnknown6462(Packet packet)
        {
            var bits10 = (int)packet.ReadBits(21);

            var bits4 = new uint[bits10];

            for (var i = 0; i < bits10; ++i)
                bits4[i] = packet.ReadBits(22);

            for (var i = 0; i < bits10; ++i)
            {
                for (var j = 0; j < bits4[i]; ++j)
                    packet.ReadInt32("Creature", i, j);

                packet.ReadInt32("Quest Id", i);
            }
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            packet.ReadInt32("Count?");

            var count = packet.ReadBits("Count", 20);

            var POIcounter = new uint[count];
            var pointsSize = new uint[count][];

            for (var i = 0; i < count; ++i)
            {
                POIcounter[i] = packet.ReadBits("POI Counter", 18, i);
                pointsSize[i] = new uint[POIcounter[i]];

                for (var j = 0; j < POIcounter[i]; ++j)
                    pointsSize[i][j] = packet.ReadBits("Points Counter", 21, i, j);
            }

            for (var i = 0; i < count; ++i)
            {
                var questPOIs = new List<QuestPOI>();

                for (var j = 0; j < POIcounter[i]; ++j)
                {
                    var questPoi = new QuestPOI();
                    questPoi.Points = new List<QuestPOIPoint>((int)pointsSize[i][j]);

                    packet.ReadInt32("Unk Int32 1", i, j);
                    packet.ReadInt32("Unk Int32 2", i, j);
                    packet.ReadInt32("Unk Int32 3", i, j);
                    questPoi.FloorId = packet.ReadUInt32("Floor Id", i, j);
                    questPoi.WorldMapAreaId = packet.ReadUInt32("World Map Area ID", i, j);

                    for (var k = 0u; k < pointsSize[i][j]; ++k)
                    {
                        var questPoiPoint = new QuestPOIPoint
                        {
                            Index = k,
                            Y = packet.ReadInt32("Point Y", i, j, (int)k),
                            X = packet.ReadInt32("Point X", i, j, (int)k)
                        };
                        questPoi.Points.Add(questPoiPoint);
                    }

                    questPoi.ObjectiveIndex = packet.ReadInt32("Objective Index", i, j);
                    packet.ReadInt32("Points Counter?", i, j);
                    questPoi.Map = packet.ReadUInt32<MapId>("Map Id", i, j);
                    packet.ReadInt32("Player Condition ID", i, j);
                    packet.ReadInt32("World Effect ID", i, j);
                    questPoi.Idx = (uint)packet.ReadInt32("POI Index", i, j);
                    questPOIs.Add(questPoi);
                }

                var questId = packet.ReadInt32<QuestId>("Quest ID", i);
                packet.ReadInt32("POI Counter?", i);

                foreach (var questpoi in questPOIs)
                    Storage.QuestPOIs.Add(new Tuple<uint, uint>((uint)questId, questpoi.Idx), questpoi, packet.TimeSpan);
            }
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS)]
        public static void HandleQuestgiverDetails(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.ReadInt32("Int21E8");
            packet.ReadInt32("Int21F8");

            for (var i = 0; i < 5; i++)
            {
                packet.ReadUInt32("Reputation Value Id", i);
                packet.ReadUInt32("Reputation Faction", i);
                packet.ReadInt32("Reputation Value", i);
            }

            packet.ReadInt32("Int2224");
            packet.ReadInt32("Int21BC");
            packet.ReadInt32("Int21D0");
            packet.ReadInt32("Int2260");
            packet.ReadInt32("Int225C");
            packet.ReadInt32("Int21DC"); // Level?
            packet.ReadInt32("Int2228");
            packet.ReadInt32("Int2258");
            packet.ReadInt32("Int222C");
            packet.ReadInt32("IntFEC"); // QuestId?
            packet.ReadInt32("Int2220");
            packet.ReadInt32("Int2214");
            packet.ReadInt32("Int2248");
            packet.ReadInt32("Int2210");
            packet.ReadInt32("IntFF0");
            packet.ReadInt32("Int21F0");
            packet.ReadInt32("Int220C");
            packet.ReadInt32("Int24D0");
            packet.ReadInt32("Int221C");
            packet.ReadInt32("Int21D4");
            packet.ReadInt32("Int2268");
            packet.ReadInt32("Int21D8");
            packet.ReadInt32("Int24E4");
            packet.ReadInt32("Int21EC");
            packet.ReadInt32("Int21FC");

            for (var i = 0; i < 4; i++)
            {
                packet.ReadUInt32("Currency Id", i);
                packet.ReadUInt32("Currency Count", i);
            }

            packet.ReadInt32("Int21F4");
            packet.ReadInt32("Int2238");
            packet.ReadInt32("Int2208");
            packet.ReadInt32("Int2200");
            packet.ReadInt32("Int2230");
            packet.ReadInt32("Int2218");
            packet.ReadInt32("Int2264");
            packet.ReadInt32("Int224C");
            packet.ReadInt32("Int2250");
            packet.ReadInt32("IntFD4");
            packet.ReadInt32("Int223C");
            packet.ReadInt32("Int21E0");
            packet.ReadInt32("Int2204");
            packet.ReadInt32("Int2234");
            packet.ReadInt32("Int2254");
            packet.ReadInt32("Int21E4");
            packet.ReadInt32("Int226C");
            packet.ReadInt32("Int2270");
            packet.ReadInt32("Int24E8");
            packet.ReadInt32("Int2240");
            packet.ReadInt32("Int2244");
            guid1[4] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            var bits756 = (int)packet.ReadBits(10);
            guid1[2] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            var bits7353 = (int)packet.ReadBits(10);
            var bits6 = (int)packet.ReadBits(12);
            guid1[5] = packet.ReadBit();
            var bitFD0 = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            var bits24D4 = (int)packet.ReadBits(21);
            var bits21C0 = (int)packet.ReadBits(22);
            var bits1024 = (int)packet.ReadBits(12);
            var bitsFD8b = (int)packet.ReadBits(20);
            var bits1774 = (int)packet.ReadBits(8);
            guid2[5] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            var bits8377 = (int)packet.ReadBits(8);
            var bits2228 = (int)packet.ReadBits(9);
            guid2[6] = packet.ReadBit();
            var bitFE8 = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            var bit1CB8 = packet.ReadBit();

            for (var i = 0; i < bits24D4; i++)
            {
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);
            }

            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 5);
            packet.ReadWoWString("String1CB9", bits7353);
            packet.ReadWoWString("StringBD0", bits756);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 0);

            for (var i = 0; i < bitsFD8b; i++)
            {
                packet.ReadByte("ByteED", i);
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntFDC", i);
                packet.ReadInt32("IntED", i);
            }

            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid1, 3);
            packet.ReadWoWString("String1BB8", bits1774);
            packet.ReadWoWString("String20B9", bits8377);
            packet.ReadXORByte(guid2, 2);
            for (var i = 0; i < bits21C0; i++)
                packet.ReadInt32("IntEA", i);

            packet.ReadXORByte(guid2, 6);
            packet.ReadWoWString("String22D0", bits2228);
            packet.ReadXORByte(guid1, 7);
            packet.ReadWoWString("String1000", bits1024);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid1, 6);
            packet.ReadWoWString("String18", bits6);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 1);

            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);

        }
    }
}
