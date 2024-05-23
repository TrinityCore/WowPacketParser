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
    }
}
