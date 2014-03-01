using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class QuestHandler
    {
        [Parser(Opcode.CMSG_QUEST_POI_QUERY)]
        public static void HandleQuestPoiQuery(Packet packet)
        {
            var quest = new int[50];
            for (var i = 0; i < 50; i++)
                quest[i] = packet.ReadInt32();

            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
                packet.WriteLine("Quest ID: {0}", StoreGetters.GetName(StoreNameType.Quest, quest[i]));
        }

        [Parser(Opcode.CMSG_QUEST_NPC_QUERY)]
        public static void HandleQuestNpcQuery(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);

            for (var i = 0; i < count; i++)
                packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID", i);
        }

        [Parser(Opcode.SMSG_QUEST_NPC_QUERY_RESPONSE)]
        public static void HandleQuestNpcQueryResponse(Packet packet)
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
                    packet.ReadInt32("Unk Int32 4", i, j);

                    for (var k = 0u; k < pointsSize[i][j]; ++k)
                    {
                        var questPoiPoint = new QuestPOIPoint
                        {
                            Index = k,
                            X = packet.ReadInt32("Point X", i, j, (int)k),
                            Y = packet.ReadInt32("Point Y", i, j, (int)k)
                        };
                        questPoi.Points.Add(questPoiPoint);
                    }

                    questPoi.Map = (uint)packet.ReadEntryWithName<UInt32>(StoreNameType.Map, "Map Id", i, j);
                    packet.ReadInt32("Unk Int32 4", i, j);
                    packet.ReadInt32("Unk Int32 5", i, j);
                    questPoi.FloorId = packet.ReadUInt32("Floor Id", i, j);
                    questPoi.WorldMapAreaId = packet.ReadUInt32("World Map Area", i, j);
                    questPoi.Idx = (uint)packet.ReadInt32("POI Index", i, j);
                    questPoi.ObjectiveIndex = packet.ReadInt32("Objective Index", i, j);

                    questPOIs.Add(questPoi);
                }

                packet.ReadInt32("POI Counter?", i);
                var questId = packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID", i);

                foreach (var questpoi in questPOIs)
                    Storage.QuestPOIs.Add(new Tuple<uint, uint>((uint)questId, questpoi.Idx), questpoi, packet.TimeSpan);
            }

            packet.ReadInt32("Count?");
        }
    }
}
