using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class IslandHandler
    {
        [Parser(Opcode.SMSG_ISLAND_AZERITE_XP_GAIN)]
        public static void HandleIslandAzeriteXpGain(Packet packet)
        {            
            packet.ReadInt32("XpGain");
            packet.ReadPackedGuid128("PlayerGuid");
            packet.ReadPackedGuid128("SourceGuid");
            packet.ReadInt32("SourceID");
        }       

        [Parser(Opcode.SMSG_ISLAND_COMPLETED)]
        public static void HandleIslandCompleted(Packet packet)
        {
            packet.ReadInt32("MapID");
            packet.ReadInt32("Winner");
            var PlayerCount = packet.ReadUInt32("GroupTeamSize");
            for (int j = 0; j < PlayerCount; j++)
                CharacterHandler.ReadPlayerModelDisplayInfo(packet, "DisplayInfo", j);
        }

        [Parser(Opcode.SMSG_ISLAND_OPEN_QUEUE_NPC)]
        public static void HandleIslandOpenQueueNpc(Packet packet)
        {
            packet.ReadPackedGuid128("QueueNPCGuid");
        }

        [Parser(Opcode.CMSG_ISLAND_QUEUE)]
        public static void HandleIslandQueue(Packet packet)
        {
            packet.ReadPackedGuid128("QueueNPCGuid");
            packet.ReadInt32("ActivityID");
        }
    }
}
