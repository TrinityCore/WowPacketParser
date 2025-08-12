using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class IslandHandler
    {
        [Parser(Opcode.SMSG_ISLAND_AZERITE_GAIN)]
        public static void HandleIslandAzeriteGain(Packet packet)
        {
            packet.ReadInt32("Gain");
            packet.ReadPackedGuid128("PlayerGuid");
            packet.ReadPackedGuid128("SourceGuid");
            packet.ReadInt32("SourceID");
        }

        [Parser(Opcode.SMSG_ISLAND_COMPLETE)]
        public static void HandleIslandComplete(Packet packet)
        {
            packet.ReadInt32("MapID");
            packet.ReadInt32("Winner");
            var PlayerCount = packet.ReadUInt32("GroupTeamSize");
            for (int j = 0; j < PlayerCount; j++)
                CharacterHandler.ReadPlayerModelDisplayInfo(packet, "DisplayInfo", j);
        }
    }
}
