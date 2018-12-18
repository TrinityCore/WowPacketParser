using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class PetHandler
    {
        [Parser(Opcode.SMSG_PET_NAME_INVALID)]
        public static void HandlePetNameInvalid(Packet packet)
        {
            packet.ReadByte("Result");
            V6_0_2_19033.Parsers.PetHandler.ReadPetRenameData(packet);
        }
    }
}
