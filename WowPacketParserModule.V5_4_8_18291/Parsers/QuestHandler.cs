using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class QuestHandler
    {
        [Parser(Opcode.CMSG_QUESTGIVER_STATUS_QUERY)]
        public static void HandleQuestgiverStatusQuery(Packet packet)
        {
            var guid = packet.StartBitStream(4, 3, 2, 1, 0, 5, 7, 6);
            packet.ParseBitStream(guid, 5, 7, 4, 0, 2, 1, 6, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUESTGIVER_STATUS)]
        public static void HandleQuestgiverStatus(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 7, 4, 2, 5, 3, 6, 0);
            packet.ReadXORByte(guid, 7);
            packet.ReadEnum<QuestGiverStatus4x>("Status", TypeCode.Int32);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_QUESTGIVER_STATUS_MULTIPLE)]
        public static void HandleQuestgiverStatusMultiple(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);

            var guid = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                guid[i] = new byte[8];

                packet.StartBitStream(guid[i], 4, 0, 3, 6, 5, 7, 1, 2);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guid[i], 6);
                packet.ReadXORByte(guid[i], 2);
                packet.ReadXORByte(guid[i], 7);
                packet.ReadXORByte(guid[i], 5);
                packet.ReadXORByte(guid[i], 4);
                packet.ReadEnum<QuestGiverStatus4x>("Status", TypeCode.Int32);
                packet.ReadXORByte(guid[i], 1);
                packet.ReadXORByte(guid[i], 3);
                packet.ReadXORByte(guid[i], 0);

                packet.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.CMSG_QUEST_QUERY)]
        public static void HandleQuestQuery(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Entry");
            packet.StartBitStream(guid, 0, 5, 2, 7, 6, 4, 1, 3);
            packet.ParseBitStream(guid, 4, 1, 7, 5, 2, 3, 6, 0);

            packet.WriteGuid("Guid", guid);
        }
    }
}
