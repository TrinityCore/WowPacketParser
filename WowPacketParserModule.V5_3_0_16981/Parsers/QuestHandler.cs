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
            var count = packet.ReadBits("Count", 22);
            packet.ResetBitReader();

            for (var i = 0; i < count; i++)
                packet.ReadInt32<QuestId>("Quest ID", i);
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            packet.ReadInt32("Count?");
            var count = packet.ReadBits("Count", 20);
            if (count == 0)
                return; // nothing to do

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
                            Y = packet.ReadInt32("Point Y", i, j, (int)k),
                            X = packet.ReadInt32("Point X", i, j, (int)k)
                        };
                        questPoi.Points.Add(questPoiPoint);
                    }

                    questPoi.FloorId = packet.ReadUInt32("Floor Id", i, j);
                    questPoi.Idx = (uint)packet.ReadInt32("POI Index", i, j);
                    questPoi.ObjectiveIndex = packet.ReadInt32("Objective Index", i, j);
                    questPoi.WorldMapAreaId = packet.ReadUInt32("World Map Area ID", i, j);
                    packet.ReadUInt32("Player Condition ID", i, j);
                    packet.ReadUInt32("World Effect ID", i, j);
                    questPoi.UnkInt2 = packet.ReadUInt32("Unk Int32 1", i, j);
                    packet.ReadUInt32("Unk Int32 2", i, j);
                    questPoi.UnkInt1 = packet.ReadUInt32("Unk Int32 3", i, j);
                    packet.ReadUInt32("Points Counter?", i, j);
                    questPoi.Map = packet.ReadUInt32<MapId>("Map Id", i, j);
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
            packet.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.ReadInt32("int1215");
            packet.ReadInt32("int896");
            packet.ReadInt32("int912");
            packet.ReadInt32("int927");
            packet.ReadInt32("int929");
            packet.ReadUInt32<QuestId>("Quest ID");
            for (var i = 0; i < 5; i++)
                packet.ReadUInt32("Reputation Value Id", i);
            for (var i = 0; i < 5; i++)
                packet.ReadUInt32("Reputation Faction", i);
            for (var i = 0; i < 5; i++)
                packet.ReadInt32("Reputation Value", i);
            packet.ReadInt32("int9128");
            packet.ReadInt32("QuestGiver Portrait");
            for (var i = 0; i < 4; i++)
                packet.ReadUInt32("Currency Id", i);
            for (var i = 0; i < 4; i++)
                packet.ReadUInt32("Currency Count", i);
            packet.ReadInt32("int914");
            packet.ReadInt32("int904");
            packet.ReadInt32("int933");
            packet.ReadInt32("int907");
            packet.ReadInt32("int920");
            packet.ReadInt32("int928");
            packet.ReadInt32("int1214");
            packet.ReadInt32("int902");
            packet.ReadInt32("int895");
            packet.ReadInt32("int898");
            packet.ReadInt32("int911");
            packet.ReadInt32("int2360");
            packet.ReadInt32("int932");
            packet.ReadInt32("int919");
            packet.ReadInt32("int910");
            packet.ReadInt32("int1213");
            packet.ReadInt32("int900");
            packet.ReadInt32("int897");
            packet.ReadInt32("int908");
            packet.ReadInt32("int899");
            packet.ReadInt32("int905");
            packet.ReadInt32("int909");
            packet.ReadInt32("int915");
            packet.ReadInt32("int894");
            packet.ReadInt32("int923");
            packet.ReadInt32("int916");
            packet.ReadInt32("int921");
            packet.ReadInt32("int913");
            packet.ReadInt32("int930");
            packet.ReadInt32("int893");
            packet.ReadInt32("int906");
            packet.ReadInt32("int903");
            packet.ReadInt32("int917");
            packet.ReadInt32("int924");
            packet.ReadInt32("int926");
            packet.ReadInt32("int925");
            packet.ReadInt32("int922");
            packet.ReadInt32("int931");
            packet.ReadInt32("int901");

            var guid1 = new byte[8];
            var guid2 = new byte[8];
            var questTurnTargetName = packet.ReadBits(8);
            guid1[3] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            var bit5893 = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            var bits1602 = packet.ReadBits(22);
            packet.StartBitStream(guid2, 6, 3);
            var bit6432 = packet.ReadBit();
            packet.StartBitStream(guid1, 1, 2);
            var questTurnTextWindow = packet.ReadBits(10);
            var questGiverTargetName = packet.ReadBits(8);
            guid1[6] = packet.ReadBit();
            var bits75 = packet.ReadBits(21);
            guid2[7] = packet.ReadBit();
            var details = packet.ReadBits(12);
            guid2[0] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            var questGiverTextWindow = packet.ReadBits(10);
            var bits4 = packet.ReadBits(20);
            guid1[0] = packet.ReadBit();
            var title = packet.ReadBits(9);
            var objectives = packet.ReadBits(12);
            packet.ReadBit("bit4868");

            for (var i = 0; i < bits4; ++i)
            {
                packet.ReadInt32("int5+4", i);
                packet.ReadByte("byte5+12", i);
                packet.ReadInt32("int5+8", i);
                packet.ReadInt32("int5+0", i);
            }

            for (var i = 0; i < bits1602; ++i)
                packet.ReadInt32("int1603", i);

            for (var i = 0; i < bits75; ++i)
            {
                packet.ReadUInt32("Emote Id", i);
                packet.ReadUInt32("Emote Delay (ms)", i);
            }

            packet.ReadXORByte(guid2, 5);
            packet.ReadXORBytes(guid1, 5, 1, 4, 7);
            packet.ReadXORByte(guid2, 7);
            packet.ReadWoWString("QuestGiver Text Window", questGiverTextWindow);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORBytes(guid2, 0, 6);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 2);
            packet.ReadWoWString("Title", title);
            packet.ReadXORByte(guid1, 3);
            packet.ReadWoWString("QuestTurn Target Name", questTurnTargetName);
            packet.ReadWoWString("Details", details);
            packet.ReadWoWString("QuestTurn Text Window", questTurnTextWindow);
            packet.ReadWoWString("Objectives", objectives);
            packet.ReadXORBytes(guid2, 1, 3, 4);
            packet.ReadXORByte(guid1, 2);
            packet.ReadWoWString("QuestGiver Target Name", questGiverTargetName);
            packet.WriteGuid("Guid1", guid1);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.CMSG_DB_QUERY_BULK)]
        public static void HandleDBQueryBulk(Packet packet)
        {
            packet.ReadInt32E<DB2Hash>("DB2 File");
            var count = packet.ReadBits(21);

            var guids = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guids[i] = new byte[8];
                packet.StartBitStream(guids[i], 5, 7, 6, 1, 4, 3, 2, 1);
            }

            packet.ResetBitReader();
            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORBytes(guids[i], 3, 7, 1, 6, 4, 0, 5);
                packet.ReadInt32("Unk int", i);
                packet.ReadXORByte(guids[i], 2);
                packet.WriteGuid("Guid", guids[i], i);
            }
        }

        [Parser(Opcode.SMSG_QUEST_GIVER_REQUEST_ITEMS)]
        public static void HandleQuestRequestItems(Packet packet)
        {
            var guid = new byte[8];

            guid[0] = packet.ReadBit();
            packet.ReadBit("bit3552");
            var countItems = packet.ReadBits("Number of Required Items", 20);
            var countCurrencies = packet.ReadBits("Number of Required Currencies", 21);
            packet.StartBitStream(guid, 2, 6);
            var titleLen = packet.ReadBits(9);
            var textLen = packet.ReadBits(12);
            packet.StartBitStream(guid, 4, 3, 1, 5, 7);
            packet.ResetBitReader();

            packet.ReadXORByte(guid, 5);
            packet.ReadInt32("int2596");
            packet.ReadXORByte(guid, 2);
            for (var i = 0; i < countItems; ++i)
            {
                packet.ReadUInt32("Required Item Display Id", i);
                packet.ReadUInt32<ItemId>("Required Item Id", i);
                packet.ReadUInt32("Required Item Count", i);
            }

            packet.ReadInt32("Money");
            packet.ReadInt32("int3604");
            packet.ReadInt32("Emote");

            for (var i = 0; i < countCurrencies; i++)
            {
                packet.ReadUInt32("Required Currency Count", i);
                packet.ReadUInt32("Required Currency Id", i);
            }

            packet.ReadEntry("Quest Giver Entry");
            packet.ReadXORBytes(guid, 0, 1);
            packet.ReadWoWString("Title", titleLen);
            packet.ReadWoWString("Text", textLen);
            packet.ReadInt32("int3556");
            var entry = packet.ReadUInt32<QuestId>("Quest ID");
            packet.ReadXORByte(guid, 4);
            packet.ReadUInt32E<QuestFlags>("Quest Flags");
            packet.ReadXORBytes(guid, 6, 3);
            packet.ReadInt32("int3544");
            packet.ReadXORByte(guid, 7);
            packet.WriteGuid("Guid", guid);
        }
    }
}
