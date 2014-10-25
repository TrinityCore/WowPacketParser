using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class QuestHandler
    {
        [Parser(Opcode.CMSG_QUEST_QUERY)]
        public static void HandleQuestQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_QUEST_NPC_QUERY)]
        public static void HandleQuestNpcQuery(Packet packet)
        {
            packet.ReadUInt32("Count");

            for (var i = 0; i < 50; i++)
                packet.ReadEntry<Int32>(StoreNameType.Quest, "Quest ID", i);
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            packet.ReadInt32("NumPOIs");
            var int4 = packet.ReadInt32("QuestPOIData");

            for (var i = 0; i < int4; ++i)
            {
                var questId = packet.ReadUInt32("QuestID", i);
                packet.ReadUInt32("NumBlobs", i);

                var int2 = packet.ReadInt32("QuestPOIBlobData", i);

                for (var j = 0; j < int2; ++j)
                {
                    var questPoi = new QuestPOI();
                    var idx = packet.ReadInt32("BlobIndex", i, j);
                    questPoi.ObjectiveIndex = packet.ReadInt32("ObjectiveIndex", i, j);
                    packet.ReadInt32("QuestObjectiveID", i, j);
                    packet.ReadInt32("QuestObjectID", i, j);
                    questPoi.Map = (uint)packet.ReadInt32("MapID", i, j);
                    questPoi.WorldMapAreaId = (uint)packet.ReadInt32("WorldMapAreaID", i, j);
                    questPoi.FloorId = (uint)packet.ReadInt32("Floor", i, j);
                    packet.ReadInt32("Priority", i, j);
                    packet.ReadInt32("Flags", i, j);
                    packet.ReadInt32("WorldEffectID", i, j);
                    packet.ReadInt32("PlayerConditionID", i, j);
                    packet.ReadInt32("NumPoints", i, j);
                    packet.ReadInt32("Int12", i, j);

                    var int13 = packet.ReadInt32("QuestPOIBlobPoint", i, j);
                    questPoi.Points = new List<QuestPOIPoint>(int13);
                    for (var k = 0u; k < int13; ++k)
                    {
                        var questPoiPoint = new QuestPOIPoint
                        {
                            Index = k,
                            X = packet.ReadInt32("X", i, j, (int)k),
                            Y = packet.ReadInt32("Y", i, j, (int)k)
                        };
                        questPoi.Points.Add(questPoiPoint);
                    }

                    Storage.QuestPOIs.Add(new Tuple<uint, uint>((uint)questId, (uint)idx), questPoi, packet.TimeSpan);
                }
            }
        }
    }
}
