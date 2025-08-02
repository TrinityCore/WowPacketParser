using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.SMSG_PETITION_ALREADY_SIGNED)]
        public static void HandlePetitionAlreadySigned(Packet packet)
        {
            packet.ReadPackedGuid128("SignerGUID");
        }
    }
}
