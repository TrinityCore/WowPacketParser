using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using SpellParsers = WowPacketParserModule.V6_0_2_19033.Parsers.SpellHandler;

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

        [Parser(Opcode.SMSG_SPELLENERGIZELOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadPackedGuid128("TargetGUID");

            packet.ReadEntry<Int32>(StoreNameType.Spell, "SpellID");
            packet.ReadEnum<PowerType>("Type", TypeCode.UInt32);
            packet.ReadInt32("Amount");

            packet.ResetBitReader();

            var bit100 = packet.ReadBit("HasLogData");
            if (bit100)
                SpellParsers.ReadSpellCastLogData(ref packet);
        }

        [Parser(Opcode.SMSG_SPELLLOGEXECUTE)]
        public static void HandleSpellLogExecute(Packet packet)
        {
            packet.ReadPackedGuid128("Caster");

            packet.ReadInt32("SpellID");

            var int16 = packet.ReadInt32("EffectsCount");
            for (var i = 0; i < int16; i++)
            {
                packet.ReadInt32("Effect", i);

                var int4 = packet.ReadInt32("PowerDrainTargetsCount", i);
                var int20 = packet.ReadInt32("ExtraAttacksTargetsCount", i);
                var int36 = packet.ReadInt32("DurabilityDamageTargetsCount", i);
                var int52 = packet.ReadInt32("GenericVictimTargetsCount", i);
                var int68 = packet.ReadInt32("TradeSkillTargetsCount", i);
                var int84 = packet.ReadInt32("FeedPetTargetsCount", i);

                // ClientSpellLogEffectPowerDrainParams
                for (var j = 0; j < int4; j++)
                {
                    packet.ReadPackedGuid128("Victim");
                    packet.ReadInt32("Points");
                    packet.ReadInt32("PowerType");
                    packet.ReadSingle("Amplitude");
                }

                // ClientSpellLogEffectExtraAttacksParams
                for (var j = 0; j < int20; j++)
                {
                    packet.ReadPackedGuid128("Victim", i, j);
                    packet.ReadInt32("NumAttacks", i, j);
                }

                // ClientSpellLogEffectDurabilityDamageParams
                for (var j = 0; j < int36; j++)
                {
                    packet.ReadPackedGuid128("Victim", i, j);
                    packet.ReadInt32("ItemID", i, j);
                    packet.ReadInt32("Amount", i, j);
                }

                // ClientSpellLogEffectGenericVictimParams
                for (var j = 0; j < int52; j++)
                    packet.ReadPackedGuid128("Victim", i, j);

                // ClientSpellLogEffectTradeSkillItemParams
                for (var j = 0; j < int68; j++)
                    packet.ReadInt32("ItemID", i, j);

                // ClientSpellLogEffectFeedPetParams
                for (var j = 0; j < int84; j++)
                    packet.ReadInt32("ItemID", i, j);
            }

            var bit160 = packet.ReadBit("HasLogData");
            if (bit160)
                SpellParsers.ReadSpellCastLogData(ref packet);
        }

        [Parser(Opcode.SMSG_SPELLDAMAGESHIELD)]
        public static void ReadSpellDamageShield(Packet packet)
        {
            packet.ReadPackedGuid128("Attacker");
            packet.ReadPackedGuid128("Defender");
            packet.ReadInt32("TotalDamage");
            packet.ReadInt32("SpellID");
            packet.ReadPackedGuid128("Caster");
            packet.ReadInt32("LogAbsorbed");
            var bit76 = packet.ReadBit("HasLogData");
            if (bit76)
                SpellHandler.ReadSpellCastLogData(ref packet);
        }
    }
}
