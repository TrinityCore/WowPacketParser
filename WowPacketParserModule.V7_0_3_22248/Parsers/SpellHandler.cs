using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using SpellCastFailureReason = WowPacketParser.Enums.Version.V6_1_0_19678.SpellCastFailureReason;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.SMSG_SPELL_PREPARE)]
        public static void SpellPrepare(Packet packet)
        {
            packet.ReadPackedGuid128("ClientCastID");
            packet.ReadPackedGuid128("ServerCastID");
        }
    }
}
