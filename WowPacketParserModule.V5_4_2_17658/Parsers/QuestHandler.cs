using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class QuestHandler
    {
        [Parser(Opcode.CMSG_QUEST_POI_QUERY)]
        public static void HandleQuestPoiQuery(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Count");


            for (var i = 0; i < count; i++) //for (var i = 0; i < 50; i++)
                packet.Translator.ReadInt32<QuestId>("Quest ID", i);

            packet.ReadToEnd(); // Hack
        }

        [Parser(Opcode.CMSG_QUERY_QUEST_COMPLETION_NPCS)]
        public static void HandleQuestNpcQuery(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 22);

            for (var i = 0; i < count; i++)
                packet.Translator.ReadInt32<QuestId>("Quest ID", i);
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

                    var questPoiPoints = new List<QuestPOIPoint>();
                    for (int k = 0; k < pointsSize[i][j]; ++k)
                    {
                        QuestPOIPoint questPoiPoint = new QuestPOIPoint
                        {
                            Idx2 = k,
                            X = packet.Translator.ReadInt32("Point X", i, j, k),
                            Y = packet.Translator.ReadInt32("Point Y", i, j, k)
                        };
                        questPoiPoints.Add(questPoiPoint);
                    }

                    packet.Translator.ReadInt32("Unk Int32 1", i, j);
                    packet.Translator.ReadInt32("Unk Int32 2", i, j);
                    packet.Translator.ReadInt32("Unk Int32 3", i, j);
                    questPoi.MapID = (int)packet.Translator.ReadUInt32<MapId>("Map Id", i, j);
                    questPoi.ObjectiveIndex = packet.Translator.ReadInt32("Objective Index", i, j);
                    questPoi.WorldMapAreaId = (int)packet.Translator.ReadUInt32("World Map Area ID", i, j);
                    packet.Translator.ReadInt32("Unk Int32 4", i, j);
                    packet.Translator.ReadInt32("World Effect ID", i, j);

                    int idx = packet.Translator.ReadInt32("POI Index", i, j);
                    questPoi.ID = idx;

                    packet.Translator.ReadInt32("Player Condition ID", i, j);
                    questPoi.Floor = (int)packet.Translator.ReadUInt32("Floor Id", i, j);

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

        [Parser(Opcode.CMSG_QUERY_QUEST_INFO)]
        public static void HandleQuestQuery(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadInt32("Entry");
            packet.Translator.StartBitStream(guid, 1, 6, 3, 5, 0, 7, 4, 2);
            packet.Translator.ParseBitStream(guid, 0, 5, 4, 1, 7, 6, 3, 2);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_QUEST_INFO_RESPONSE)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            var hasData = packet.Translator.ReadBit();
            if (!hasData)
            {
                packet.Translator.ReadUInt32("Entry");
                return; // nothing to do
            }

            var quest = new QuestTemplate();

            var bits19E8 = (int)packet.Translator.ReadBits(9);
            var bits2604 = (int)packet.Translator.ReadBits(11);
            var bits278 = (int)packet.Translator.ReadBits(12);
            var bits2004 = packet.Translator.ReadBits(8);
            var bits2504 = packet.Translator.ReadBits(8);
            var bits1C04 = (int)packet.Translator.ReadBits(10);
            var bits78 = (int)packet.Translator.ReadBits(9);
            var bitsE30 = (int)packet.Translator.ReadBits(12);
            var bits2E10 = (int)packet.Translator.ReadBits(19);

            var len2949_20 = new uint[bits2E10];
            var bits114 = new uint[bits2E10];

           for (var i = 0; i < bits2E10; ++i)
           {
               len2949_20[i] = packet.Translator.ReadBits(8);
               bits114[i] = packet.Translator.ReadBits(22);
           }

           var bits2104 = (int)packet.Translator.ReadBits(10);

           packet.Translator.ReadInt32("Int2E34");
           packet.Translator.ReadInt32("Int2E44");
           packet.Translator.ReadInt32("Int2E9C");

           for (var i = 0; i < bits2E10; ++i)
           {
                packet.Translator.ReadInt32("Int2E14", i);
                packet.Translator.ReadWoWString("StringEA", len2949_20[i], i);
                packet.Translator.ReadByte("ByteED", i);
                packet.Translator.ReadInt32("IntEA", i);
                for (var j = 0; j < bits114[i]; ++j)
                    packet.Translator.ReadInt32("IntEC", i, j);

                packet.Translator.ReadByte("ByteED", i);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadInt32("IntED", i);
            }

            packet.Translator.ReadSingle("Float6C");
            packet.Translator.ReadInt32("Int3C");
            packet.Translator.ReadInt32("Int74");
            packet.Translator.ReadInt32("Int1BEC");
            packet.Translator.ReadInt32("Int2E5C");
            packet.Translator.ReadInt32("Int2E38");
            packet.Translator.ReadInt32("Int2E20");
            packet.Translator.ReadWoWString("String2104", bits2104);
            packet.Translator.ReadInt32("Int2C");
            packet.Translator.ReadInt32("Int2E80");
            packet.Translator.ReadWoWString("String2004", bits2004);
            packet.Translator.ReadInt32("Int2E8C");
            packet.Translator.ReadInt32("Int1BF8");
            packet.Translator.ReadSingle("Float58");
            packet.Translator.ReadInt32("Int2E60");

            for (var i = 0; i < 5; ++i)
            {
                packet.Translator.ReadInt32("int2986+40", i);
                packet.Translator.ReadInt32("int2986+0", i);
                packet.Translator.ReadInt32("int2986+20", i);
            }

            packet.Translator.ReadInt32("Int4C");
            packet.Translator.ReadInt32("Int2E84");
            packet.Translator.ReadInt32("Int2E48");
            packet.Translator.ReadInt32("Int1C00");
            packet.Translator.ReadInt32("Int2E30");
            packet.Translator.ReadInt32("Int2E40");
            packet.Translator.ReadInt32("Int2E94");
            packet.Translator.ReadWoWString("String2604", bits2604);
            packet.Translator.ReadInt32("Int2E0C");
            packet.Translator.ReadInt32("Int2E98");
            packet.Translator.ReadInt32("Int2E24");
            packet.Translator.ReadInt32("Int2E90");
            packet.Translator.ReadInt32("Int60");
            packet.Translator.ReadWoWString("String19E8", bits19E8);
            packet.Translator.ReadInt32("Int2E58");
            packet.Translator.ReadInt32("Int2E6C");
            packet.Translator.ReadInt32("Int2E64");
            packet.Translator.ReadInt32("Int5C");
            packet.Translator.ReadInt32("Int2E78");

            for (var i = 0; i < 4; ++i)
            {
                packet.Translator.ReadInt32("int3001+16", i);
                packet.Translator.ReadInt32("int3001+0", i);
            }

            packet.Translator.ReadInt32("Int18");
            packet.Translator.ReadInt32("Int2E7C");
            packet.Translator.ReadWoWString("String78", bits78);
            packet.Translator.ReadWoWString("StringE30", bitsE30);
            packet.Translator.ReadInt32("Int2E74");
            packet.Translator.ReadInt32("Int2E88");
            packet.Translator.ReadInt32("Int30");
            packet.Translator.ReadInt32("Int40");
            packet.Translator.ReadInt32("Int1BF4");
            packet.Translator.ReadInt32("Int2EA4");
            packet.Translator.ReadInt32("Int2E68");
            packet.Translator.ReadInt32("Int48");
            packet.Translator.ReadInt32("Int28");
            packet.Translator.ReadInt32("Int50");
            packet.Translator.ReadInt32("Int44");
            packet.Translator.ReadInt32("Int2EA0");
            packet.Translator.ReadInt32("Int2E54");
            packet.Translator.ReadInt32("Int2E04");
            packet.Translator.ReadInt32("Int64");
            packet.Translator.ReadInt32("Int68");
            packet.Translator.ReadInt32("Int2E28");
            packet.Translator.ReadInt32("Int2E50");
            packet.Translator.ReadInt32("Int2E4C");
            packet.Translator.ReadInt32("Int24");
            packet.Translator.ReadWoWString("String278", bits278);
            packet.Translator.ReadWoWString("String2504", bits2504);
            packet.Translator.ReadInt32("Int54");
            packet.Translator.ReadInt32("Int2E2C");
            packet.Translator.ReadSingle("Float70");
            packet.Translator.ReadInt32("Int1C");
            packet.Translator.ReadInt32("Int2E3C");
            packet.Translator.ReadInt32("Int38");
            packet.Translator.ReadInt32("Int1BE8");
            packet.Translator.ReadInt32("Int2E70");
            packet.Translator.ReadInt32("Int2E08");
            packet.Translator.ReadInt32("Int34");
            packet.Translator.ReadInt32("Int1BF0");
            packet.Translator.ReadInt32("Int1BFC");
            packet.Translator.ReadWoWString("String1C04", bits1C04);
            packet.Translator.ReadInt32("Int20");

            var id = packet.Translator.ReadInt32("Quest Id");

            //packet.AddSniffData(StoreNameType.Quest, id.Key, "QUERY_RESPONSE");
            //Storage.QuestTemplates.Add((uint)id.Key, quest, packet.TimeSpan);
        }
    }
}
