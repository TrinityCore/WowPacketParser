using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class PerksProgramHandler
    {
        [Parser(Opcode.SMSG_PERKS_PROGRAM_ACTIVITY_UPDATE)]
        public static void HandleperksActivityUpdateOpcode(Packet packet)
        {
            var perksactivitysize = packet.ReadUInt32("PerksActivitySize");

            for (var i = 0; i < perksactivitysize; ++i)
                packet.ReadInt32("PerksActivityUpdate");
        }

    }
}
