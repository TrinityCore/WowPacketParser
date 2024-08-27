using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class GarrisonHandler
    { 
        [Parser(Opcode.CMSG_ADVENTURE_MAP_START_QUEST)]
        public static void HandleAdventureMapStartQuest(Packet packet)
        {
            packet.ReadInt32<QuestId>("QuestID");
        }
    }
}
