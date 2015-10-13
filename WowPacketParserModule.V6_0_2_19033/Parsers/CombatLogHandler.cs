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
            packet.ReadPackedGuid128("Me");
            packet.ReadPackedGuid128("CasterGUID");

            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadInt32("Damage");
            packet.ReadInt32("OverKill");

            packet.ReadByte("SchoolMask");

            packet.ReadInt32("ShieldBlock");
            packet.ReadInt32("Resisted");
            packet.ReadInt32("Absorbed");

            packet.ResetBitReader();

            packet.ReadBit("Periodic");

            packet.ReadBitsE<AttackerStateFlags>("Flags", ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173) ? 8 : 9);

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
                SpellParsers.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_PERIODIC_AURA_LOG)]
        public static void HandlePeriodicAuraLog(Packet packet)
        {
            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadPackedGuid128("CasterGUID");

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
                SpellParsers.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_HEAL_LOG)]
        public static void HandleSpellHealLog(Packet packet)
        {
            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadPackedGuid128("CasterGUID");

            packet.ReadInt32("SpellID");
            packet.ReadInt32("Health");
            packet.ReadInt32("OverHeal");
            packet.ReadInt32("Absorbed");

            packet.ResetBitReader();

            packet.ReadBit("Crit");
            packet.ReadBit("Multistrike");

            var hasCritRollMade = packet.ReadBit("HasCritRollMade");
            var hasCritRollNeeded = packet.ReadBit("HasCritRollNeeded");
            var hasLogData = packet.ReadBit("HasLogData");

            if (hasCritRollMade)
                packet.ReadSingle("CritRollMade");

            if (hasCritRollNeeded)
                packet.ReadSingle("CritRollNeeded");

            if (hasLogData)
                SpellParsers.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_ENERGIZE_LOG)]
        public static void HandleSpellEnergizeLog(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadPackedGuid128("TargetGUID");

            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadUInt32E<PowerType>("Type");

            packet.ReadInt32("Amount");

            packet.ResetBitReader();

            var bit100 = packet.ReadBit("HasLogData");
            if (bit100)
                SpellParsers.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_EXECUTE_LOG)]
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
                SpellParsers.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_DAMAGE_SHIELD)]
        public static void ReadSpellDamageShield(Packet packet)
        {
            packet.ReadPackedGuid128("Attacker");
            packet.ReadPackedGuid128("Defender");
            packet.ReadInt32("SpellID");
            packet.ReadInt32("TotalDamage");
            packet.ReadInt32("OverKill");
            packet.ReadInt32("SchoolMask");
            packet.ReadInt32("LogAbsorbed");

            packet.ResetBitReader();

            var bit76 = packet.ReadBit("HasLogData");
            if (bit76)
                SpellHandler.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_ENVIRONMENTAL_DAMAGE_LOG)]
        public static void HandleEnvirenmentalDamageLog(Packet packet)
        {
            packet.ReadPackedGuid128("Victim");

            packet.ReadByteE<EnvironmentDamage>("Type");

            packet.ReadInt32("Amount");
            packet.ReadInt32("Resisted");
            packet.ReadInt32("Absorbed");

            var bit76 = packet.ReadBit("HasLogData");
            if (bit76)
                SpellHandler.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_ABSORB_LOG)]
        public static void HandleSpellAbsorbLog(Packet packet)
        {
            packet.ReadPackedGuid128("Victim");
            packet.ReadPackedGuid128("Caster");

            packet.ReadInt32("InterruptedSpellID");
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadPackedGuid128("ShieldTargetGUID?");
            packet.ReadInt32("Absorbed");

            packet.ResetBitReader();

            var bit100 = packet.ReadBit("HasLogData");
            if (bit100)
                SpellParsers.ReadSpellCastLogData(packet);
        }

        [Parser(Opcode.SMSG_SPELL_INTERRUPT_LOG)]
        public static void HandleSpellInterruptLog(Packet packet)
        {
            packet.ReadPackedGuid128("Caster");
            packet.ReadPackedGuid128("Victim");

            packet.ReadInt32("InterruptedSpellID");
            packet.ReadInt32("SpellID");
        }

        [Parser(Opcode.SMSG_SPELL_OR_DAMAGE_IMMUNE)]
        public static void HandleSpellOrDamageImmune(Packet packet)
        {
            packet.ReadPackedGuid128("CasterGUID");
            packet.ReadPackedGuid128("VictimGUID");

            packet.ReadInt32<SpellId>("SpellID");

            packet.ReadBit("IsPeriodic");
        }

        [Parser(Opcode.SMSG_SPELL_MISS_LOG)]
        public static void HandleSpellMissLog(Packet packet)
        {
            packet.ReadInt32("SpellID");
            packet.ReadPackedGuid128("Caster");

            var spellLogMissEntryCount = packet.ReadInt32("SpellLogMissEntryCount");
            for (int i = 0; i < spellLogMissEntryCount; i++)
            {
                packet.ReadPackedGuid128("Victim", i);
                packet.ReadByte("MissReason", i);

                packet.ResetBitReader();

                var hasSpellLogMissDebug = packet.ReadBit("HasSpellLogMissDebug", i);
                if (hasSpellLogMissDebug)
                {
                    packet.ReadSingle("HitRoll", i);
                    packet.ReadSingle("HitRollNeededHitRollNeeded", i);
                }
            }
        }

        [Parser(Opcode.SMSG_ATTACK_SWING_LANDED_LOG)]
        public static void HandleAttackswingLandedLog(Packet packet)
        {
            SpellParsers.ReadSpellCastLogData(packet);

            packet.ReadInt32("Size");

            CombatHandler.ReadAttackRoundInfo(packet);
        }

        [Parser(Opcode.CMSG_SET_ADVANCED_COMBAT_LOGGING)]
        public static void HandleSetAdvancedCombatLogging(Packet packet)
        {
            packet.ReadBit("Enable");
        }

        [Parser(Opcode.SMSG_PROC_RESIST)]
        public static void HandleProcResist(Packet packet)
        {
            packet.ReadPackedGuid128("Caster");
            packet.ReadPackedGuid128("Target");

            packet.ReadInt32("SpellID");

            var bit20 = packet.ReadBit("HasRolled");    // unconfirmed order
            var bit32 = packet.ReadBit("HasNeeded");    // unconfirmed order

            if (bit20)
                packet.ReadSingle("Rolled");            // unconfirmed order

            if (bit32)
                packet.ReadSingle("Needed");            // unconfirmed order
        }
    }
}
