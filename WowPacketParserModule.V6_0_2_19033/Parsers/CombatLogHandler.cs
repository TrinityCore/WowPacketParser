using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using SpellParsers = WowPacketParser.V6_0_2_19033.Parsers.SpellHandler;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class CombatLogHandler
    {
        [Parser(Opcode.SMSG_SPELLNONMELEEDAMAGELOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            packet.ReadPackedGuid128("Me");
            packet.ReadPackedGuid128("CasterGUID");

            packet.ReadEntry<Int32>(StoreNameType.Spell, "SpellID");
            packet.ReadInt32("Damage");
            packet.ReadInt32("OverKill");

            packet.ReadByte("SchoolMask");

            packet.ReadInt32("ShieldBlock");
            packet.ReadInt32("Resisted");
            packet.ReadInt32("Absorbed");

            packet.ResetBitReader();

            packet.ReadBit("Periodic");

            packet.ReadEnum<AttackerStateFlags>("Flags", 9);

            var bit148 = packet.ReadBit();
            var bit76 = packet.ReadBit("HasLogData");

            if (bit148)
            {
                packet.ReadSingle("CritRoll");
                packet.ReadSingle("CritNeeded");
                packet.ReadSingle("HitRoll");
                packet.ReadSingle("HitNeeded");
                packet.ReadSingle("MissChance");
                packet.ReadSingle("DodgeChance");
                packet.ReadSingle("ParryChance");
                packet.ReadSingle("BlockChance");
                packet.ReadSingle("GlanceChance");
                packet.ReadSingle("CrushChance");
            }

            if (bit76)
                SpellParsers.ReadSpellCastLogData(ref packet);
        }

        [Parser(Opcode.SMSG_PERIODICAURALOG)]
        public static void HandlePeriodicAuraLog(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadPackedGuid128("TargetGUID");

            packet.ReadInt32("SpellID");

            var int24 = packet.ReadInt32("PeriodicAuraLogEffectCount");

            // PeriodicAuraLogEffect
            for (var i = 0; i < int24; i++)
            {
                packet.ReadInt32("Effect", i);
                packet.ReadInt32("Amount", i);
                packet.ReadInt32("OverHealOrKill", i);
                packet.ReadInt32("SchoolMaskOrPower", i);
                packet.ReadInt32("AbsorbedOrAmplitude", i);
                packet.ReadInt32("Resisted");

                packet.ResetBitReader();

                packet.ReadBit("Crit", i);
                packet.ReadBit("Multistrike", i);

                // PeriodicAuraLogEffectDebugInfo
                var bit36 = packet.ReadBit("HasPeriodicAuraLogEffectDebugInfo");
                if (bit36)
                {
                    packet.ReadSingle("CritRollMade", i);
                    packet.ReadSingle("CritRollNeeded", i);
                }

            }

            packet.ResetBitReader();

            var bit56 = packet.ReadBit("HasLogData");
            if (bit56)
                SpellParsers.ReadSpellCastLogData(ref packet);
        }

        [Parser(Opcode.SMSG_SPELLHEALLOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadPackedGuid128("TargetGUID");

            packet.ReadInt32("SpellID");
            packet.ReadInt32("Health");
            packet.ReadInt32("OverHeal");
            packet.ReadInt32("Absorbed");

            packet.ResetBitReader();

            packet.ReadBit("Crit");
            packet.ReadBit("Multistrike");

            var bit128 = packet.ReadBit("HasCritRollMade");
            var bit120 = packet.ReadBit("HasLogData");

            if (bit128)
                packet.ReadSingle("CritRollMade");

            if (bit120)
                SpellParsers.ReadSpellCastLogData(ref packet);
        }
    }
}
