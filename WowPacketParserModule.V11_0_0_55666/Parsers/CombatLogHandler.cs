using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V11_0_0_55666.Parsers
{
    public static class CombatLogHandler
    {
        [Parser(Opcode.SMSG_SPELL_ENERGIZE_LOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadPackedGuid128("TargetGUID");

            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadByteE<PowerType>("Type");

            packet.ReadInt32("Amount");
            packet.ReadInt32("OverEnergize");

            packet.ResetBitReader();

            var hasLogData = packet.ReadBit("HasLogData");
            if (hasLogData)
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet);
        }
    }
}
