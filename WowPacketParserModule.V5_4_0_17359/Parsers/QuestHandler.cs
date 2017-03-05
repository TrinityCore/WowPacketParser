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
            var count = packet.Translator.ReadUInt32("Count");

            for (var i = 0; i < count; i++) // for (var i = 0; i < 50; i++)
                packet.Translator.ReadInt32<QuestId>("Quest ID", i);

            packet.ReadToEnd(); // Hack
        }

        [Parser(Opcode.CMSG_QUERY_QUEST_COMPLETION_NPCS)]
        public static void HandleQuestNpcQuery(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 22);
            packet.Translator.ResetBitReader();

            for (var i = 0; i < count; i++)
                packet.Translator.ReadInt32<QuestId>("Quest ID", i);
        }

        [Parser(Opcode.SMSG_QUEST_COMPLETION_NPC_RESPONSE)]
        public static void HandleUnknown6462(Packet packet)
        {
            var bits10 = (int)packet.Translator.ReadBits(21);

            var bits4 = new uint[bits10];

            for (var i = 0; i < bits10; ++i)
                bits4[i] = packet.Translator.ReadBits(22);

            for (var i = 0; i < bits10; ++i)
            {
                for (var j = 0; j < bits4[i]; ++j)
                    packet.Translator.ReadInt32("Creature", i, j);

                packet.Translator.ReadInt32("Quest Id", i);
            }
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            packet.Translator.ReadInt32("Count?");

            uint count = packet.Translator.ReadBits("Count", 20);

            var poiCounter = new uint[count];
            var pointsSize = new uint[count][];

            for (int i = 0; i < count; ++i)
            {
                poiCounter[i] = packet.Translator.ReadBits("POI Counter", 18, i);
                pointsSize[i] = new uint[poiCounter[i]];

                for (int j = 0; j < poiCounter[i]; ++j)
                    pointsSize[i][j] = packet.Translator.ReadBits("Points Counter", 21, i, j);
            }

            for (int i = 0; i < count; ++i)
            {
                var questPOIs = new List<QuestPOI>();
                var questPoiPointsForQuest = new List<QuestPOIPoint>();

                for (int j = 0; j < poiCounter[i]; ++j)
                {
                    QuestPOI questPoi = new QuestPOI();

                    packet.Translator.ReadInt32("Unk Int32 1", i, j);
                    packet.Translator.ReadInt32("Unk Int32 2", i, j);
                    packet.Translator.ReadInt32("Unk Int32 3", i, j);
                    questPoi.Floor = (int)packet.Translator.ReadUInt32("Floor Id", i, j);
                    questPoi.WorldMapAreaId = (int)packet.Translator.ReadUInt32("World Map Area ID", i, j);

                    var questPoiPoints = new List<QuestPOIPoint>();
                    for (int k = 0; k < pointsSize[i][j]; ++k)
                    {
                        QuestPOIPoint questPoiPoint = new QuestPOIPoint
                        {
                            Idx2 = k,
                            Y = packet.Translator.ReadInt32("Point Y", i, j, k),
                            X = packet.Translator.ReadInt32("Point X", i, j, k)
                        };
                        questPoiPoints.Add(questPoiPoint);
                    }

                    questPoi.ObjectiveIndex = packet.Translator.ReadInt32("Objective Index", i, j);
                    packet.Translator.ReadInt32("Points Counter?", i, j);
                    questPoi.MapID = (int)packet.Translator.ReadUInt32<MapId>("Map Id", i, j);
                    packet.Translator.ReadInt32("Player Condition ID", i, j);
                    packet.Translator.ReadInt32("World Effect ID", i, j);

                    int idx = packet.Translator.ReadInt32("POI Index", i, j);
                    questPoi.ID = idx;

                    questPoiPoints.ForEach(p =>
                    {
                        p.Idx1 = idx;
                        questPoiPointsForQuest.Add(p);
                    });

                    questPOIs.Add(questPoi);
                }

                int questId = packet.Translator.ReadInt32<QuestId>("Quest ID", i);
                packet.Translator.ReadInt32("POI Counter?", i);

                questPoiPointsForQuest.ForEach(q =>
                {
                    q.QuestID = questId;
                    Storage.QuestPOIPoints.Add(q, packet.TimeSpan);
                });

                questPOIs.ForEach(q =>
                {
                    q.QuestID = questId;
                    Storage.QuestPOIs.Add(q, packet.TimeSpan);
                });
            }
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_QUEST_DETAILS)]
        public static void HandleQuestgiverDetails(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            packet.Translator.ReadInt32("Int21E8");
            packet.Translator.ReadInt32("Int21F8");

            for (var i = 0; i < 5; i++)
            {
                packet.Translator.ReadUInt32("Reputation Value Id", i);
                packet.Translator.ReadUInt32("Reputation Faction", i);
                packet.Translator.ReadInt32("Reputation Value", i);
            }

            packet.Translator.ReadInt32("Int2224");
            packet.Translator.ReadInt32("Int21BC");
            packet.Translator.ReadInt32("Int21D0");
            packet.Translator.ReadInt32("Int2260");
            packet.Translator.ReadInt32("Int225C");
            packet.Translator.ReadInt32("Int21DC"); // Level?
            packet.Translator.ReadInt32("Int2228");
            packet.Translator.ReadInt32("Int2258");
            packet.Translator.ReadInt32("Int222C");
            packet.Translator.ReadInt32("IntFEC"); // QuestId?
            packet.Translator.ReadInt32("Int2220");
            packet.Translator.ReadInt32("Int2214");
            packet.Translator.ReadInt32("Int2248");
            packet.Translator.ReadInt32("Int2210");
            packet.Translator.ReadInt32("IntFF0");
            packet.Translator.ReadInt32("Int21F0");
            packet.Translator.ReadInt32("Int220C");
            packet.Translator.ReadInt32("Int24D0");
            packet.Translator.ReadInt32("Int221C");
            packet.Translator.ReadInt32("Int21D4");
            packet.Translator.ReadInt32("Int2268");
            packet.Translator.ReadInt32("Int21D8");
            packet.Translator.ReadInt32("Int24E4");
            packet.Translator.ReadInt32("Int21EC");
            packet.Translator.ReadInt32("Int21FC");

            for (var i = 0; i < 4; i++)
            {
                packet.Translator.ReadUInt32("Currency Id", i);
                packet.Translator.ReadUInt32("Currency Count", i);
            }

            packet.Translator.ReadInt32("Int21F4");
            packet.Translator.ReadInt32("Int2238");
            packet.Translator.ReadInt32("Int2208");
            packet.Translator.ReadInt32("Int2200");
            packet.Translator.ReadInt32("Int2230");
            packet.Translator.ReadInt32("Int2218");
            packet.Translator.ReadInt32("Int2264");
            packet.Translator.ReadInt32("Int224C");
            packet.Translator.ReadInt32("Int2250");
            packet.Translator.ReadInt32("IntFD4");
            packet.Translator.ReadInt32("Int223C");
            packet.Translator.ReadInt32("Int21E0");
            packet.Translator.ReadInt32("Int2204");
            packet.Translator.ReadInt32("Int2234");
            packet.Translator.ReadInt32("Int2254");
            packet.Translator.ReadInt32("Int21E4");
            packet.Translator.ReadInt32("Int226C");
            packet.Translator.ReadInt32("Int2270");
            packet.Translator.ReadInt32("Int24E8");
            packet.Translator.ReadInt32("Int2240");
            packet.Translator.ReadInt32("Int2244");
            guid1[4] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            var bits756 = (int)packet.Translator.ReadBits(10);
            guid1[2] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            var bits7353 = (int)packet.Translator.ReadBits(10);
            var bits6 = (int)packet.Translator.ReadBits(12);
            guid1[5] = packet.Translator.ReadBit();
            var bitFD0 = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            var bits24D4 = (int)packet.Translator.ReadBits(21);
            var bits21C0 = (int)packet.Translator.ReadBits(22);
            var bits1024 = (int)packet.Translator.ReadBits(12);
            var bitsFD8b = (int)packet.Translator.ReadBits(20);
            var bits1774 = (int)packet.Translator.ReadBits(8);
            guid2[5] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            var bits8377 = (int)packet.Translator.ReadBits(8);
            var bits2228 = (int)packet.Translator.ReadBits(9);
            guid2[6] = packet.Translator.ReadBit();
            var bitFE8 = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            var bit1CB8 = packet.Translator.ReadBit();

            for (var i = 0; i < bits24D4; i++)
            {
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadInt32("IntED", i);
            }

            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadWoWString("String1CB9", bits7353);
            packet.Translator.ReadWoWString("StringBD0", bits756);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 0);

            for (var i = 0; i < bitsFD8b; i++)
            {
                packet.Translator.ReadByte("ByteED", i);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadInt32("IntFDC", i);
                packet.Translator.ReadInt32("IntED", i);
            }

            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadWoWString("String1BB8", bits1774);
            packet.Translator.ReadWoWString("String20B9", bits8377);
            packet.Translator.ReadXORByte(guid2, 2);
            for (var i = 0; i < bits21C0; i++)
                packet.Translator.ReadInt32("IntEA", i);

            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadWoWString("String22D0", bits2228);
            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadWoWString("String1000", bits1024);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadWoWString("String18", bits6);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 1);

            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);

        }
    }
}
