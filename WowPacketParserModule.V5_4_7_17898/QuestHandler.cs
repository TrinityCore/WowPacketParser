using System;
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
            var count = packet.ReadUInt32("Count");


            for (var i = 0; i < count; i++) //for (var i = 0; i < 50; i++)
                packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID", i);

            packet.ReadToEnd(); // Hack
        }

        [Parser(Opcode.CMSG_QUEST_NPC_QUERY)]
        public static void HandleQuestNpcQuery(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);

            for (var i = 0; i < count; i++)
                packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID", i);
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

                    packet.ReadInt32("Unk Int32 1", i, j);
                    packet.ReadInt32("Unk Int32 2", i, j);
                    packet.ReadInt32("Unk Int32 3", i, j);
                    questPoi.Map = (uint)packet.ReadEntryWithName<UInt32>(StoreNameType.Map, "Map Id", i, j);
                    questPoi.ObjectiveIndex = packet.ReadInt32("Objective Index", i, j);
                    questPoi.WorldMapAreaId = packet.ReadUInt32("World Map Area", i, j);
                    packet.ReadInt32("Unk Int32 4", i, j);
                    packet.ReadInt32("Unk Int32 5", i, j);
                    questPoi.Idx = (uint)packet.ReadInt32("POI Index", i, j);
                    packet.ReadInt32("Unk Int32 6", i, j);
                    questPoi.FloorId = packet.ReadUInt32("Floor Id", i, j);

                    questPOIs.Add(questPoi);
                }

                var questId = packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID", i);
                packet.ReadInt32("POI Counter?", i);

                foreach (var questpoi in questPOIs)
                    Storage.QuestPOIs.Add(new Tuple<uint, uint>((uint)questId, questpoi.Idx), questpoi, packet.TimeSpan);
            }
        }

        [Parser(Opcode.CMSG_QUEST_QUERY)]
        public static void HandleQuestQuery(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Entry");
            packet.StartBitStream(guid, 1, 6, 3, 5, 0, 7, 4, 2);
            packet.ParseBitStream(guid, 0, 5, 4, 1, 7, 6, 3, 2);

            packet.WriteGuid("Guid", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUEST_QUERY_RESPONSE)]
        public static void HandleQuestQueryResponse(Packet packet)
        {
            var hasData = packet.ReadBit();
            if (!hasData)
            {
                packet.ReadUInt32("Entry");
                return; // nothing to do
            }

            var quest = new QuestTemplate();

            var bits19E8 = (int)packet.ReadBits(9);
            var bits2604 = (int)packet.ReadBits(11);
            var bits278 = (int)packet.ReadBits(12);
            var bits2004 = packet.ReadBits(8);
            var bits2504 = packet.ReadBits(8);
            var bits1C04 = (int)packet.ReadBits(10);
            var bits78 = (int)packet.ReadBits(9);
            var bitsE30 = (int)packet.ReadBits(12);
            var bits2E10 = (int)packet.ReadBits(19);

            var len2949_20 = new uint[bits2E10];
            var bits114 = new uint[bits2E10];

           for (var i = 0; i < bits2E10; ++i)
           {
               len2949_20[i] = packet.ReadBits(8);
               bits114[i] = packet.ReadBits(22);
           }

           var bits2104 = (int)packet.ReadBits(10);

           packet.ReadInt32("Int2E34");
           packet.ReadInt32("Int2E44");
           packet.ReadInt32("Int2E9C");

           for (var i = 0; i < bits2E10; ++i)
           {
                packet.ReadInt32("Int2E14", i);
                packet.ReadWoWString("StringEA", len2949_20[i], i);
                packet.ReadByte("ByteED", i);
                packet.ReadInt32("IntEA", i);
                for (var j = 0; j < bits114[i]; ++j)
                    packet.ReadInt32("IntEC", i, j);

                packet.ReadByte("ByteED", i);
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);
            }

            packet.ReadSingle("Float6C");
            packet.ReadInt32("Int3C");
            packet.ReadInt32("Int74");
            packet.ReadInt32("Int1BEC");
            packet.ReadInt32("Int2E5C");
            packet.ReadInt32("Int2E38");
            packet.ReadInt32("Int2E20");
            packet.ReadWoWString("String2104", bits2104);
            packet.ReadInt32("Int2C");
            packet.ReadInt32("Int2E80");
            packet.ReadWoWString("String2004", bits2004);
            packet.ReadInt32("Int2E8C");
            packet.ReadInt32("Int1BF8");
            packet.ReadSingle("Float58");
            packet.ReadInt32("Int2E60");

            for (var i = 0; i < 5; ++i)
            {
                packet.ReadInt32("int2986+40", i);
                packet.ReadInt32("int2986+0", i);
                packet.ReadInt32("int2986+20", i);
            }

            packet.ReadInt32("Int4C");
            packet.ReadInt32("Int2E84");
            packet.ReadInt32("Int2E48");
            packet.ReadInt32("Int1C00");
            packet.ReadInt32("Int2E30");
            packet.ReadInt32("Int2E40");
            packet.ReadInt32("Int2E94");
            packet.ReadWoWString("String2604", bits2604);
            packet.ReadInt32("Int2E0C");
            packet.ReadInt32("Int2E98");
            packet.ReadInt32("Int2E24");
            packet.ReadInt32("Int2E90");
            packet.ReadInt32("Int60");
            packet.ReadWoWString("String19E8", bits19E8);
            packet.ReadInt32("Int2E58");
            packet.ReadInt32("Int2E6C");
            packet.ReadInt32("Int2E64");
            packet.ReadInt32("Int5C");
            packet.ReadInt32("Int2E78");

            for (var i = 0; i < 4; ++i)
            {
                packet.ReadInt32("int3001+16", i);
                packet.ReadInt32("int3001+0", i);
            }

            packet.ReadInt32("Int18");
            packet.ReadInt32("Int2E7C");
            packet.ReadWoWString("String78", bits78);
            packet.ReadWoWString("StringE30", bitsE30);
            packet.ReadInt32("Int2E74");
            packet.ReadInt32("Int2E88");
            packet.ReadInt32("Int30");
            packet.ReadInt32("Int40");
            packet.ReadInt32("Int1BF4");
            packet.ReadInt32("Int2EA4");
            packet.ReadInt32("Int2E68");
            packet.ReadInt32("Int48");
            packet.ReadInt32("Int28");
            packet.ReadInt32("Int50");
            packet.ReadInt32("Int44");
            packet.ReadInt32("Int2EA0");
            packet.ReadInt32("Int2E54");
            packet.ReadInt32("Int2E04");
            packet.ReadInt32("Int64");
            packet.ReadInt32("Int68");
            packet.ReadInt32("Int2E28");
            packet.ReadInt32("Int2E50");
            packet.ReadInt32("Int2E4C");
            packet.ReadInt32("Int24");
            packet.ReadWoWString("String278", bits278);
            packet.ReadWoWString("String2504", bits2504);
            packet.ReadInt32("Int54");
            packet.ReadInt32("Int2E2C");
            packet.ReadSingle("Float70");
            packet.ReadInt32("Int1C");
            packet.ReadInt32("Int2E3C");
            packet.ReadInt32("Int38");
            packet.ReadInt32("Int1BE8");
            packet.ReadInt32("Int2E70");
            packet.ReadInt32("Int2E08");
            packet.ReadInt32("Int34");
            packet.ReadInt32("Int1BF0");
            packet.ReadInt32("Int1BFC");
            packet.ReadWoWString("String1C04", bits1C04);
            packet.ReadInt32("Int20");

            var id = packet.ReadInt32("Quest Id");

            //packet.AddSniffData(StoreNameType.Quest, id.Key, "QUERY_RESPONSE");
            //Storage.QuestTemplates.Add((uint)id.Key, quest, packet.TimeSpan);
        }
    }
}
