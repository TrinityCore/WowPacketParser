using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class PetHandler
    {
        [Parser(Opcode.CMSG_PET_ABANDON_BY_NUMBER)]
        public static void HandlePetAbandonByNumber(Packet packet)
        {
            packet.ReadUInt32("PetNumber");
        }
        
        [Parser(Opcode.SMSG_PET_DISMISS_SOUND, ClientVersionBuild.V10_1_7_51187)]
        public static void HandlePetDismissSound(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadInt32("CreatureDisplayInfoID");
            packet.ReadVector3("ModelPosition");
        }
    }
}
