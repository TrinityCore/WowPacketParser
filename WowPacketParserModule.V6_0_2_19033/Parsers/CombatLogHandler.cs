using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using SpellParsers = WowPacketParserModule.V6_0_2_19033.Parsers.SpellHandler;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class CombatLogHandler
    {
        [Parser(Opcode.SMSG_SPELL_NON_MELEE_DAMAGE_LOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Me");
            packet.Translator.ReadPackedGuid128("CasterGUID");

            packet.Translator.ReadInt32<SpellId>("SpellID");
            packet.Translator.ReadInt32("Damage");
            packet.Translator.ReadInt32("OverKill");

            packet.Translator.ReadByte("SchoolMask");

            packet.Translator.ReadInt32("ShieldBlock");
            packet.Translator.ReadInt32("Resisted");
            packet.Translator.ReadInt32("Absorbed");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Periodic");

            packet.Translator.ReadBitsE<AttackerStateFlags>("Flags", ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173) ? 8 : 9);

            var bit148 = packet.Translator.ReadBit();
            var bit76 = packet.Translator.ReadBit("HasLogData");

            if (bit148)
            {
                packet.Translator.ReadSingle("CritRoll");
                packet.Translator.ReadSingle("CritNeeded");
                packet.Translator.ReadSingle("HitRoll");
                packet.Translator.ReadSingle("HitNeeded");
                packet.Translator.ReadSingle("MissChance");
                packet.Translator.ReadSingle("DodgeChance");
                packet.Translator.ReadSingle("ParryChance");
                packet.Translator.ReadSingle("BlockChance");
                packet.Translator.ReadSingle("GlanceChance");
                packet.Translator.ReadSingle("CrushChance");
            }

            if (bit76)
                SpellParsers.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_PERIODIC_AURA_LOG)]
        public static void HandlePeriodicAuraLog(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TargetGUID");
            packet.Translator.ReadPackedGuid128("CasterGUID");

            packet.Translator.ReadInt32<SpellId>("SpellID");

            var int24 = packet.Translator.ReadInt32("PeriodicAuraLogEffectCount");

            // PeriodicAuraLogEffect
            for (var i = 0; i < int24; i++)
            {
                packet.Translator.ReadInt32("Effect", i);
                packet.Translator.ReadInt32("Amount", i);
                packet.Translator.ReadInt32("OverHealOrKill", i);
                packet.Translator.ReadInt32("SchoolMaskOrPower", i);
                packet.Translator.ReadInt32("AbsorbedOrAmplitude", i);
                packet.Translator.ReadInt32("Resisted");

                packet.Translator.ResetBitReader();

                packet.Translator.ReadBit("Crit", i);
                packet.Translator.ReadBit("Multistrike", i);

                // PeriodicAuraLogEffectDebugInfo
                var bit36 = packet.Translator.ReadBit("HasPeriodicAuraLogEffectDebugInfo");
                if (bit36)
                {
                    packet.Translator.ReadSingle("CritRollMade", i);
                    packet.Translator.ReadSingle("CritRollNeeded", i);
                }

            }

            packet.Translator.ResetBitReader();

            var bit56 = packet.Translator.ReadBit("HasLogData");
            if (bit56)
                SpellParsers.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_LOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TargetGUID");
            packet.Translator.ReadPackedGuid128("CasterGUID");

            packet.Translator.ReadInt32<SpellId>("SpellID");
            packet.Translator.ReadInt32("Health");
            packet.Translator.ReadInt32("OverHeal");
            packet.Translator.ReadInt32("Absorbed");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Crit");
            packet.Translator.ReadBit("Multistrike");

            var hasCritRollMade = packet.Translator.ReadBit("HasCritRollMade");
            var hasCritRollNeeded = packet.Translator.ReadBit("HasCritRollNeeded");
            var hasLogData = packet.Translator.ReadBit("HasLogData");

            if (hasCritRollMade)
                packet.Translator.ReadSingle("CritRollMade");

            if (hasCritRollNeeded)
                packet.Translator.ReadSingle("CritRollNeeded");

            if (hasLogData)
                SpellParsers.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_ENERGIZE_LOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CasterGUID");
            packet.Translator.ReadPackedGuid128("TargetGUID");

            packet.Translator.ReadInt32<SpellId>("SpellID");
            packet.Translator.ReadUInt32E<PowerType>("Type");

            packet.Translator.ReadInt32("Amount");

            packet.Translator.ResetBitReader();

            var bit100 = packet.Translator.ReadBit("HasLogData");
            if (bit100)
                SpellParsers.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_EXECUTE_LOG)]
        public static void HandleSpellLogExecute(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Caster");

            packet.Translator.ReadInt32<SpellId>("SpellID");

            var int16 = packet.Translator.ReadInt32("EffectsCount");
            for (var i = 0; i < int16; i++)
            {
                packet.Translator.ReadInt32("Effect", i);

                var int4 = packet.Translator.ReadInt32("PowerDrainTargetsCount", i);
                var int20 = packet.Translator.ReadInt32("ExtraAttacksTargetsCount", i);
                var int36 = packet.Translator.ReadInt32("DurabilityDamageTargetsCount", i);
                var int52 = packet.Translator.ReadInt32("GenericVictimTargetsCount", i);
                var int68 = packet.Translator.ReadInt32("TradeSkillTargetsCount", i);
                var int84 = packet.Translator.ReadInt32("FeedPetTargetsCount", i);

                // ClientSpellLogEffectPowerDrainParams
                for (var j = 0; j < int4; j++)
                {
                    packet.Translator.ReadPackedGuid128("Victim");
                    packet.Translator.ReadInt32("Points");
                    packet.Translator.ReadInt32("PowerType");
                    packet.Translator.ReadSingle("Amplitude");
                }

                // ClientSpellLogEffectExtraAttacksParams
                for (var j = 0; j < int20; j++)
                {
                    packet.Translator.ReadPackedGuid128("Victim", i, j);
                    packet.Translator.ReadInt32("NumAttacks", i, j);
                }

                // ClientSpellLogEffectDurabilityDamageParams
                for (var j = 0; j < int36; j++)
                {
                    packet.Translator.ReadPackedGuid128("Victim", i, j);
                    packet.Translator.ReadInt32("ItemID", i, j);
                    packet.Translator.ReadInt32("Amount", i, j);
                }

                // ClientSpellLogEffectGenericVictimParams
                for (var j = 0; j < int52; j++)
                    packet.Translator.ReadPackedGuid128("Victim", i, j);

                // ClientSpellLogEffectTradeSkillItemParams
                for (var j = 0; j < int68; j++)
                    packet.Translator.ReadInt32("ItemID", i, j);

                // ClientSpellLogEffectFeedPetParams
                for (var j = 0; j < int84; j++)
                    packet.Translator.ReadInt32("ItemID", i, j);
            }

            var bit160 = packet.Translator.ReadBit("HasLogData");
            if (bit160)
                SpellParsers.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_DAMAGE_SHIELD)]
        public static void ReadSpellDamageShield(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Attacker");
            packet.Translator.ReadPackedGuid128("Defender");
            packet.Translator.ReadInt32<SpellId>("SpellID");
            packet.Translator.ReadInt32("TotalDamage");
            packet.Translator.ReadInt32("OverKill");
            packet.Translator.ReadInt32("SchoolMask");
            packet.Translator.ReadInt32("LogAbsorbed");

            packet.Translator.ResetBitReader();

            var bit76 = packet.Translator.ReadBit("HasLogData");
            if (bit76)
                SpellHandler.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_ENVIRONMENTAL_DAMAGE_LOG)]
        public static void HandleEnvirenmentalDamageLog(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Victim");

            packet.Translator.ReadByteE<EnvironmentDamage>("Type");

            packet.Translator.ReadInt32("Amount");
            packet.Translator.ReadInt32("Resisted");
            packet.Translator.ReadInt32("Absorbed");

            var bit76 = packet.Translator.ReadBit("HasLogData");
            if (bit76)
                SpellHandler.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_ABSORB_LOG)]
        public static void HandleSpellAbsorbLog(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Victim");
            packet.Translator.ReadPackedGuid128("Caster");

            packet.Translator.ReadInt32("InterruptedSpellID");
            packet.Translator.ReadInt32<SpellId>("SpellID");
            packet.Translator.ReadPackedGuid128("ShieldTargetGUID?");
            packet.Translator.ReadInt32("Absorbed");

            packet.Translator.ResetBitReader();

            var bit100 = packet.Translator.ReadBit("HasLogData");
            if (bit100)
                SpellParsers.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_INTERRUPT_LOG)]
        public static void HandleSpellInterruptLog(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Caster");
            packet.Translator.ReadPackedGuid128("Victim");

            packet.Translator.ReadInt32("InterruptedSpellID");
            packet.Translator.ReadInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_SPELL_OR_DAMAGE_IMMUNE)]
        public static void HandleSpellOrDamageImmune(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("CasterGUID");
            packet.Translator.ReadPackedGuid128("VictimGUID");

            packet.Translator.ReadInt32<SpellId>("SpellID");

            packet.Translator.ReadBit("IsPeriodic");
        }

        [Parser(Opcode.SMSG_SPELL_MISS_LOG)]
        public static void HandleSpellMissLog(Packet packet)
        {
            packet.Translator.ReadInt32<SpellId>("SpellID");
            packet.Translator.ReadPackedGuid128("Caster");

            var spellLogMissEntryCount = packet.Translator.ReadInt32("SpellLogMissEntryCount");
            for (int i = 0; i < spellLogMissEntryCount; i++)
            {
                packet.Translator.ReadPackedGuid128("Victim", i);
                packet.Translator.ReadByte("MissReason", i);

                packet.Translator.ResetBitReader();

                var hasSpellLogMissDebug = packet.Translator.ReadBit("HasSpellLogMissDebug", i);
                if (hasSpellLogMissDebug)
                {
                    packet.Translator.ReadSingle("HitRoll", i);
                    packet.Translator.ReadSingle("HitRollNeededHitRollNeeded", i);
                }
            }
        }

        [Parser(Opcode.SMSG_ATTACK_SWING_LANDED_LOG)]
        public static void HandleAttackswingLandedLog(Packet packet)
        {
            SpellParsers.ReadSpellCastLogData(packet);

            packet.Translator.ReadInt32("Size");

            CombatHandler.ReadAttackRoundInfo(packet);
        }

        [Parser(Opcode.CMSG_SET_ADVANCED_COMBAT_LOGGING)]
        public static void HandleSetAdvancedCombatLogging(Packet packet)
        {
            packet.Translator.ReadBit("Enable");
        }

        [Parser(Opcode.SMSG_PROC_RESIST)]
        public static void HandleProcResist(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Caster");
            packet.Translator.ReadPackedGuid128("Target");

            packet.Translator.ReadInt32<SpellId>("SpellID");

            var bit20 = packet.Translator.ReadBit("HasRolled");    // unconfirmed order
            var bit32 = packet.Translator.ReadBit("HasNeeded");    // unconfirmed order

            if (bit20)
                packet.Translator.ReadSingle("Rolled");            // unconfirmed order

            if (bit32)
                packet.Translator.ReadSingle("Needed");            // unconfirmed order
        }
    }
}
