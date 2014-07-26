using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParserModule.V5_4_8_18414.Enums;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class QuestHandler
    {
        [Parser(Opcode.CMSG_QUEST_POI_QUERY)]
        public static void HandleQuestPoiQuery(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                var count = packet.ReadBits("Count", 22);

                for (var i = 0; i < count; i++)
                    packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID", i);
            }
            else
            {
                packet.WriteLine("              : SMSG_ZONE_UNDER_ATTACK");
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.CMSG_QUESTGIVER_QUERY_QUEST)]
        public static void HandleQuestgiverQueryQuest(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_QUESTLOG_REMOVE_QUEST)]
        public static void HandleQuestlogRemoveQuest(Packet packet)
        {
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_QUESTGIVER_STATUS_QUERY)]
        public static void HandleQuestgiverStatusQuery(Packet packet)
        {
            var guid = packet.StartBitStream(4, 3, 2, 1, 0, 5, 7, 6);
            packet.ParseBitStream(guid, 5, 7, 4, 0, 2, 1, 6, 3);

            packet.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.SMSG_QUEST_POI_QUERY_RESPONSE)]
        public static void HandleQuestPoiQueryResponse(Packet packet)
        {
            var count = packet.ReadBits("Count", 20);

            var counter = new uint[count];
            var points = new int[count][];

            for (var i = 0; i < count; i++)
            {
                counter[i] = packet.ReadBits("POI Counter", 18, i);
                points[i] = new int[counter[i]];
                for (var j = 0; j < counter[i]; j++)
                    points[i][j] = (int)packet.ReadBits("POI Points", 21, i, j);
            }
            for (var i = 0; i < count; i++)
            {
                for (var j = 0; j < counter[i]; j++)
                {
                    var questPoi = new QuestPOI();

                    questPoi.FloorId = packet.ReadUInt32("Floor Id", i, j);
                    questPoi.Points = new List<QuestPOIPoint>(points[i][j]);
                    for (var k = 0u; k < points[i][j]; k++)
                    {
                        var questPoiPoint = new QuestPOIPoint
                        {
                            Index = k,
                            X = packet.ReadInt32("Point X", i, j, (int)k),
                            Y = packet.ReadInt32("Point Y", i, j, (int)k)
                        };
                        questPoi.Points.Add(questPoiPoint);
                    }
                    questPoi.ObjectiveIndex = packet.ReadInt32("Objective Index", i, j);
                    var idx = packet.ReadInt32("POI Index", i, j);
                    packet.ReadInt32("Unk14*4", i, j);
                    packet.ReadInt32("Unk42*4", i, j);

                    questPoi.Map = (uint)packet.ReadEntryWithName<UInt32>(StoreNameType.Map, "Map Id", i, j);
                    packet.ReadInt32("Points Counter", i, j);
                    questPoi.WorldMapAreaId = packet.ReadUInt32("World Map Area", i, j);
                    packet.ReadInt32("Unk200", i, j);

                    questPoi.UnkInt2 = packet.ReadUInt32("Unk Int32 3", i, j);
                    questPoi.UnkInt1 = packet.ReadUInt32("Unk Int32 2", i, j);

                    Storage.QuestPOIs.Add(new Tuple<uint, uint>((uint)0, (uint)idx), questPoi, packet.TimeSpan);
                }
                var questId = packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID", i);
                packet.ReadInt32("POI size", i);
            }
            packet.ReadInt32("count");
        }

        [Parser(Opcode.SMSG_QUEST_QUERY_RESPONSE)]
        public static void HandleQuestQueryResp(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                packet.ReadToEnd();
            }
            else
            {
                packet.WriteLine("              : CMSG_NULL_0276");
            }
        }

        [Parser(Opcode.SMSG_QUESTGIVER_STATUS)]
        public static void HandleQuestgiverStatus(Packet packet)
        {
            var guid = packet.StartBitStream(1, 7, 4, 2, 5, 3, 6, 0);
            packet.ParseBitStream(guid, 7);
            packet.ReadEnum<QuestGiverStatus4x>("Status", TypeCode.Int32);
            packet.ParseBitStream(guid, 4, 6, 1, 5, 2, 0, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUESTGIVER_STATUS_MULTIPLE)]
        public static void HandleQuestgiverStatusMultiple(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);
            var guid = new byte[count][];
            for (var i = 0; i < count; i++)
                guid[i] = packet.StartBitStream(4, 0, 3, 6, 5, 7, 1, 2);

            for (var i = 0; i < count; i++)
            {
                packet.ParseBitStream(guid[i], 6, 2, 7, 5, 4);
                packet.ReadEnum<QuestGiverStatus4x>("Status", TypeCode.Int32, i);
                packet.ParseBitStream(guid[i], 1, 3, 0);
                packet.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.CMSG_QUEST_QUERY)]
        [Parser(Opcode.CMSG_QUESTGIVER_ACCEPT_QUEST)]
        [Parser(Opcode.CMSG_QUESTGIVER_CHOOSE_REWARD)]
        [Parser(Opcode.CMSG_QUESTGIVER_COMPLETE_QUEST)]
        [Parser(Opcode.CMSG_QUESTGIVER_HELLO)]
        [Parser(Opcode.CMSG_QUESTGIVER_REQUEST_REWARD)]
        [Parser(Opcode.SMSG_QUEST_CONFIRM_ACCEPT)]
        [Parser(Opcode.SMSG_QUESTGIVER_OFFER_REWARD)]
        [Parser(Opcode.SMSG_QUESTGIVER_QUEST_COMPLETE)]
        [Parser(Opcode.SMSG_QUESTGIVER_QUEST_DETAILS)]
        [Parser(Opcode.SMSG_QUESTGIVER_QUEST_LIST)]
        [Parser(Opcode.SMSG_QUESTGIVER_REQUEST_ITEMS)]
        [Parser(Opcode.SMSG_QUESTLOG_FULL)]
        [Parser(Opcode.SMSG_QUESTUPDATE_ADD_KILL)]
        [Parser(Opcode.SMSG_QUESTUPDATE_COMPLETE)]
        public static void HandleQuestNull(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
