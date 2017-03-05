using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class QuestHandler
    {
        [Parser(Opcode.CMSG_QUEST_POI_QUERY)]
        public static void HandleQuestPoiQuery(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 22);
            packet.Translator.ResetBitReader();

            for (var i = 0; i < count; i++)
                packet.Translator.ReadInt32<QuestId>("Quest ID", i);
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            packet.Translator.ReadInt32("Count?");
            uint count = packet.Translator.ReadBits("Count", 20);
            if (count == 0)
                return; // nothing to do

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

                    questPoi.Floor = (int)packet.Translator.ReadUInt32("Floor Id", i, j);

                    int idx = packet.Translator.ReadInt32("POI Index", i, j);
                    questPoi.ID = idx;

                    questPoi.ObjectiveIndex = packet.Translator.ReadInt32("Objective Index", i, j);
                    questPoi.WorldMapAreaId = (int)packet.Translator.ReadUInt32("World Map Area ID", i, j);
                    packet.Translator.ReadUInt32("Player Condition ID", i, j);
                    packet.Translator.ReadUInt32("World Effect ID", i, j);
                    questPoi.Flags = (int)packet.Translator.ReadUInt32("Unk Int32 1", i, j);
                    packet.Translator.ReadUInt32("Unk Int32 2", i, j);
                    questPoi.Priority = (int)packet.Translator.ReadUInt32("Unk Int32 3", i, j);
                    packet.Translator.ReadUInt32("Points Counter?", i, j);
                    questPoi.MapID = (int)packet.Translator.ReadUInt32<MapId>("Map Id", i, j);

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
            packet.Translator.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.Translator.ReadInt32("int1215");
            packet.Translator.ReadInt32("int896");
            packet.Translator.ReadInt32("int912");
            packet.Translator.ReadInt32("int927");
            packet.Translator.ReadInt32("int929");
            packet.Translator.ReadUInt32<QuestId>("Quest ID");
            for (var i = 0; i < 5; i++)
                packet.Translator.ReadUInt32("Reputation Value Id", i);
            for (var i = 0; i < 5; i++)
                packet.Translator.ReadUInt32("Reputation Faction", i);
            for (var i = 0; i < 5; i++)
                packet.Translator.ReadInt32("Reputation Value", i);
            packet.Translator.ReadInt32("int9128");
            packet.Translator.ReadInt32("QuestGiver Portrait");
            for (var i = 0; i < 4; i++)
                packet.Translator.ReadUInt32("Currency Id", i);
            for (var i = 0; i < 4; i++)
                packet.Translator.ReadUInt32("Currency Count", i);
            packet.Translator.ReadInt32("int914");
            packet.Translator.ReadInt32("int904");
            packet.Translator.ReadInt32("int933");
            packet.Translator.ReadInt32("int907");
            packet.Translator.ReadInt32("int920");
            packet.Translator.ReadInt32("int928");
            packet.Translator.ReadInt32("int1214");
            packet.Translator.ReadInt32("int902");
            packet.Translator.ReadInt32("int895");
            packet.Translator.ReadInt32("int898");
            packet.Translator.ReadInt32("int911");
            packet.Translator.ReadInt32("int2360");
            packet.Translator.ReadInt32("int932");
            packet.Translator.ReadInt32("int919");
            packet.Translator.ReadInt32("int910");
            packet.Translator.ReadInt32("int1213");
            packet.Translator.ReadInt32("int900");
            packet.Translator.ReadInt32("int897");
            packet.Translator.ReadInt32("int908");
            packet.Translator.ReadInt32("int899");
            packet.Translator.ReadInt32("int905");
            packet.Translator.ReadInt32("int909");
            packet.Translator.ReadInt32("int915");
            packet.Translator.ReadInt32("int894");
            packet.Translator.ReadInt32("int923");
            packet.Translator.ReadInt32("int916");
            packet.Translator.ReadInt32("int921");
            packet.Translator.ReadInt32("int913");
            packet.Translator.ReadInt32("int930");
            packet.Translator.ReadInt32("int893");
            packet.Translator.ReadInt32("int906");
            packet.Translator.ReadInt32("int903");
            packet.Translator.ReadInt32("int917");
            packet.Translator.ReadInt32("int924");
            packet.Translator.ReadInt32("int926");
            packet.Translator.ReadInt32("int925");
            packet.Translator.ReadInt32("int922");
            packet.Translator.ReadInt32("int931");
            packet.Translator.ReadInt32("int901");

            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var questTurnTargetName = packet.Translator.ReadBits(8);
            guid1[3] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            var bit5893 = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            var bits1602 = packet.Translator.ReadBits(22);
            packet.Translator.StartBitStream(guid2, 6, 3);
            var bit6432 = packet.Translator.ReadBit();
            packet.Translator.StartBitStream(guid1, 1, 2);
            var questTurnTextWindow = packet.Translator.ReadBits(10);
            var questGiverTargetName = packet.Translator.ReadBits(8);
            guid1[6] = packet.Translator.ReadBit();
            var bits75 = packet.Translator.ReadBits(21);
            guid2[7] = packet.Translator.ReadBit();
            var details = packet.Translator.ReadBits(12);
            guid2[0] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid2[4] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            var questGiverTextWindow = packet.Translator.ReadBits(10);
            var bits4 = packet.Translator.ReadBits(20);
            guid1[0] = packet.Translator.ReadBit();
            var title = packet.Translator.ReadBits(9);
            var objectives = packet.Translator.ReadBits(12);
            packet.Translator.ReadBit("bit4868");

            for (var i = 0; i < bits4; ++i)
            {
                packet.Translator.ReadInt32("int5+4", i);
                packet.Translator.ReadByte("byte5+12", i);
                packet.Translator.ReadInt32("int5+8", i);
                packet.Translator.ReadInt32("int5+0", i);
            }

            for (var i = 0; i < bits1602; ++i)
                packet.Translator.ReadInt32("int1603", i);

            for (var i = 0; i < bits75; ++i)
            {
                packet.Translator.ReadUInt32("Emote Id", i);
                packet.Translator.ReadUInt32("Emote Delay (ms)", i);
            }

            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORBytes(guid1, 5, 1, 4, 7);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadWoWString("QuestGiver Text Window", questGiverTextWindow);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORBytes(guid2, 0, 6);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadWoWString("Title", title);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadWoWString("QuestTurn Target Name", questTurnTargetName);
            packet.Translator.ReadWoWString("Details", details);
            packet.Translator.ReadWoWString("QuestTurn Text Window", questTurnTextWindow);
            packet.Translator.ReadWoWString("Objectives", objectives);
            packet.Translator.ReadXORBytes(guid2, 1, 3, 4);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadWoWString("QuestGiver Target Name", questGiverTargetName);
            packet.Translator.WriteGuid("Guid1", guid1);
            packet.Translator.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDBQueryBulk(Packet packet)
        {
            packet.Translator.ReadInt32E<DB2Hash>("DB2 File");
            var count = packet.Translator.ReadBits(21);

            var guids = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.Translator.StartBitStream(guids[i], 5, 7, 6, 1, 4, 3, 2, 1);
            }

            packet.Translator.ResetBitReader();
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORBytes(guids[i], 3, 7, 1, 6, 4, 0, 5);
                packet.Translator.ReadInt32("Unk int", i);
                packet.Translator.ReadXORByte(guids[i], 2);
                packet.Translator.WriteGuid("Guid", guids[i], i);
            }
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS)]
        public static void HandleQuestRequestItems(Packet packet)
        {
            var guid = new byte[8];

            guid[0] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("bit3552");
            var countItems = packet.Translator.ReadBits("Number of Required Items", 20);
            var countCurrencies = packet.Translator.ReadBits("Number of Required Currencies", 21);
            packet.Translator.StartBitStream(guid, 2, 6);
            var titleLen = packet.Translator.ReadBits(9);
            var textLen = packet.Translator.ReadBits(12);
            packet.Translator.StartBitStream(guid, 4, 3, 1, 5, 7);
            packet.Translator.ResetBitReader();

            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadInt32("int2596");
            packet.Translator.ReadXORByte(guid, 2);
            for (var i = 0; i < countItems; ++i)
            {
                packet.Translator.ReadUInt32("Required Item Display Id", i);
                packet.Translator.ReadUInt32<ItemId>("Required Item Id", i);
                packet.Translator.ReadUInt32("Required Item Count", i);
            }

            packet.Translator.ReadInt32("Money");
            packet.Translator.ReadInt32("int3604");
            packet.Translator.ReadInt32("Emote");

            for (var i = 0; i < countCurrencies; i++)
            {
                packet.Translator.ReadUInt32("Required Currency Count", i);
                packet.Translator.ReadUInt32("Required Currency Id", i);
            }

            packet.Translator.ReadEntry("Quest Giver Entry");
            packet.Translator.ReadXORBytes(guid, 0, 1);
            packet.Translator.ReadWoWString("Title", titleLen);
            packet.Translator.ReadWoWString("Text", textLen);
            packet.Translator.ReadInt32("int3556");
            var entry = packet.Translator.ReadUInt32<QuestId>("Quest ID");
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.Translator.ReadXORBytes(guid, 6, 3);
            packet.Translator.ReadInt32("int3544");
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}
