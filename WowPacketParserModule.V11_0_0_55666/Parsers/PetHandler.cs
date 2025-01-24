using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class PetHandler
    {
        [Parser(Opcode.SMSG_PET_DISMISS_SOUND)]
        public static void HandlePetDismissSound(Packet packet)
        {
            packet.ReadPackedGuid128("Pet Guid");
            packet.ReadUInt32("Model ID");
            packet.ReadVector3("Position");
        }
    }
}
