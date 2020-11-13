using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class CombatLogHandler
    {
        public static void ReadContentTuningParams(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();

            packet.ReadSingle("PlayerItemLevel", idx);
            packet.ReadSingle("TargetItemLevel", idx);
            packet.ReadInt16("PlayerLevelDelta", idx);
            packet.ReadUInt16("ScalingHealthItemLevelCurveID", idx);
            packet.ReadByte("TargetLevel", idx);
            packet.ReadByte("Expansion", idx);
            packet.ReadByte("TargetMinScalingLevel", idx);
            packet.ReadByte("TargetMaxScalingLevel", idx);
            packet.ReadSByte("TargetScalingLevelDelta", idx);
            packet.ReadBits("Type", 4, idx);
            packet.ReadBit("ScalesWithItemLevel", idx);
        }

        [Parser(Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            packet.ReadPackedGuid128("Me");
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadPackedGuid128("CastID");

            packet.ReadInt32<SpellId>("SpellID");
            SpellHandler.ReadSpellCastVisual(packet, "Visual");
            packet.ReadInt32("Damage");
            packet.ReadInt32("OriginalDamage");
            packet.ReadInt32("OverKill");

            packet.ReadByte("SchoolMask");

            packet.ReadInt32("Absorbed");
            packet.ReadInt32("Resisted");
            packet.ReadInt32("ShieldBlock");

            packet.ResetBitReader();

            packet.ReadBit("Periodic");

            packet.ReadBitsE<AttackerStateFlags>("Flags", 7);

            var hasDebugData = packet.ReadBit("HasDebugData");
            var hasLogData = packet.ReadBit("HasLogData");
            var hasContentTuning = packet.ReadBit("HasContentTuning");

            if (hasLogData)
                V8_0_1_27101.Parsers.SpellHandler.ReadSpellCastLogData(packet, "SpellCastLogData");

            if (hasDebugData)
                V8_0_1_27101.Parsers.CombatLogHandler.ReadSpellNonMeleeDebugData(packet, "DebugData");

            if (hasContentTuning)
                ReadContentTuningParams(packet, "ContentTuning");
        }
    }
}
