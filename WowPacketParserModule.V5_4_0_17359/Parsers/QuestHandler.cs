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
                packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID", i);

            packet.ReadToEnd(); // Hack
        }

        [Parser(Opcode.CMSG_QUEST_NPC_QUERY)]
        public static void HandleQuestNpcQuery(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);
            packet.ResetBitReader();

            for (var i = 0; i < count; i++)
                packet.ReadEntryWithName<Int32>(StoreNameType.Quest, "Quest ID", i);
        }
    }
}
