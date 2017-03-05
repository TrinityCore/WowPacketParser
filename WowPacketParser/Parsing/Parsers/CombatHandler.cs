using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.CMSG_ATTACK_SWING)]
        public static void HandleAttackSwing(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_DUEL_REQUESTED)]
        public static void HandleDuelRequested(Packet packet)
        {
            packet.Translator.ReadGuid("Flag GUID");
            packet.Translator.ReadGuid("Opponent GUID");
        }

        [Parser(Opcode.SMSG_DUEL_COMPLETE)]
        public static void HandleDuelComplete(Packet packet)
        {
            packet.Translator.ReadBool("Abnormal finish");
        }

        [Parser(Opcode.SMSG_DUEL_WINNER)]
        public static void HandleDuelWinner(Packet packet)
        {
            packet.Translator.ReadBool("Abnormal finish");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545)) // Probably earlier
            {
                packet.Translator.ReadCString("Name");
                packet.Translator.ReadCString("Opponent Name");
            }
            else
            {
                packet.Translator.ReadCString("Opponent Name");
                packet.Translator.ReadCString("Name");
            }
        }

        [Parser(Opcode.SMSG_DUEL_COUNTDOWN)]
        public static void HandleDuelCountDown(Packet packet)
        {
            packet.Translator.ReadInt32("Timer");
        }

        [Parser(Opcode.SMSG_RESET_RANGED_COMBAT_TIMER)]
        public static void HandleResetRangedCombatTimer(Packet packet)
        {
            packet.Translator.ReadInt32("Timer");
        }

        [Parser(Opcode.CMSG_TOGGLE_PVP)]
        public static void HandleTogglePvP(Packet packet)
        {
            // this opcode can be used in two ways: Either set new status explicitly or toggle old status
            if (packet.CanRead())
                packet.Translator.ReadBool("Enable");
        }

        [Parser(Opcode.SMSG_PVP_CREDIT)]
        public static void HandlePvPCredit(Packet packet)
        {
            packet.Translator.ReadUInt32("Honor");
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadInt32("Rank");
        }

        [Parser(Opcode.CMSG_SET_SHEATHED)]
        public static void HandleSetSheathed(Packet packet)
        {
            packet.Translator.ReadInt32E<SheathState>("Sheath");
        }

        [Parser(Opcode.SMSG_PARTY_KILL_LOG)]
        public static void HandlePartyKillLog(Packet packet)
        {
            packet.Translator.ReadGuid("Player GUID");
            packet.Translator.ReadGuid("Victim GUID");
        }

        [Parser(Opcode.SMSG_AI_REACTION)]
        public static void HandleAIReaction(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadInt32E<AIReaction>("Reaction");
        }

        [Parser(Opcode.SMSG_UPDATE_COMBO_POINTS)]
        public static void HandleUpdateComboPoints(Packet packet)
        {
            packet.Translator.ReadPackedGuid("GUID");
            packet.Translator.ReadByte("Combo Points");
        }

        [Parser(Opcode.SMSG_ENVIRONMENTAL_DAMAGE_LOG)]
        public static void HandleEnvirenmentalDamageLog(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadByteE<EnvironmentDamage>("Type");
            packet.Translator.ReadInt32("Damage");
            packet.Translator.ReadInt32("Absorb");
            packet.Translator.ReadInt32("Resist");
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Target GUID");
        }

        [Parser(Opcode.SMSG_ATTACK_START)]
        public static void HandleAttackStartStart(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadGuid("Victim GUID");
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        [Parser(Opcode.SMSG_COMBAT_EVENT_FAILED)]
        public static void HandleAttackStartStop(Packet packet)
        {
            packet.Translator.ReadPackedGuid("GUID");
            packet.Translator.ReadPackedGuid("Victim GUID");
            packet.Translator.ReadInt32("Unk int"); // Has something to do with facing?
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleAttackerStateUpdate406(Packet packet)
        {
            var hitInfo = packet.Translator.ReadInt32E<SpellHitInfo>("HitInfo");
            packet.Translator.ReadPackedGuid("AttackerGUID");
            packet.Translator.ReadPackedGuid("TargetGUID");
            packet.Translator.ReadInt32("Damage");
            packet.Translator.ReadInt32("OverDamage");

            var subDmgCount = packet.Translator.ReadByte("Count");
            for (var i = 0; i < subDmgCount; ++i)
            {
                packet.Translator.ReadInt32("SchoolMask", i);
                packet.Translator.ReadSingle("Float Damage", i);
                packet.Translator.ReadInt32("Int Damage", i);
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                for (var i = 0; i < subDmgCount; ++i)
                    packet.Translator.ReadInt32("Damage Absorbed", i);

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                    for (var i = 0; i < subDmgCount; ++i)
                        packet.Translator.ReadInt32("Damage Resisted", i);

            packet.Translator.ReadByteE<VictimStates>("VictimState");
            packet.Translator.ReadInt32("Unk Attacker State 0");

            packet.Translator.ReadInt32<SpellId>("Melee Spell Id");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
                packet.Translator.ReadInt32("Block Amount");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_RAGE_GAIN))
                packet.Translator.ReadInt32("Rage Gained");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_UNK0))
            {
                packet.Translator.ReadInt32("Unk Attacker State 3 1");
                packet.Translator.ReadSingle("Unk Attacker State 3 2");
                packet.Translator.ReadSingle("Unk Attacker State 3 3");
                packet.Translator.ReadSingle("Unk Attacker State 3 4");
                packet.Translator.ReadSingle("Unk Attacker State 3 5");
                packet.Translator.ReadSingle("Unk Attacker State 3 6");
                packet.Translator.ReadSingle("Unk Attacker State 3 7");
                packet.Translator.ReadSingle("Unk Attacker State 3 8");
                packet.Translator.ReadSingle("Unk Attacker State 3 9");
                packet.Translator.ReadSingle("Unk Attacker State 3 10");
                packet.Translator.ReadSingle("Unk Attacker State 3 11");
                packet.Translator.ReadInt32("Unk Attacker State 3 12");
                packet.Translator.ReadInt32("Unk Attacker State 3 13");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK | SpellHitInfo.HITINFO_UNK12))
                    packet.Translator.ReadSingle("Unk Float");
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var hitInfo = packet.Translator.ReadInt32E<SpellHitInfo>("HitInfo");
            packet.Translator.ReadPackedGuid("AttackerGUID");
            packet.Translator.ReadPackedGuid("TargetGUID");
            packet.Translator.ReadInt32("Damage");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183))
                packet.Translator.ReadInt32("OverDamage");

            var subDmgCount = packet.Translator.ReadByte();
            for (var i = 0; i < subDmgCount; ++i)
            {
                packet.Translator.ReadInt32("SchoolMask", i);
                packet.Translator.ReadSingle("Float Damage", i);
                packet.Translator.ReadInt32("Int Damage", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_3_9183) ||
                    hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                    packet.Translator.ReadInt32("Damage Absorbed", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_3_9183) ||
                    hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                    packet.Translator.ReadInt32("Damage Resisted", i);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183))
                packet.Translator.ReadByteE<VictimStates>("VictimState");
            else
                packet.Translator.ReadInt32E<VictimStates>("VictimState");

            packet.Translator.ReadInt32("Unk Attacker State 0");

            packet.Translator.ReadInt32<SpellId>("Melee Spell ID ");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_3_9183) ||
                hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
                packet.Translator.ReadInt32("Block Amount");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_RAGE_GAIN))
                packet.Translator.ReadInt32("Rage Gained");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_UNK0))
            {
                packet.Translator.ReadInt32("Unk Attacker State 3 1");
                packet.Translator.ReadSingle("Unk Attacker State 3 2");
                packet.Translator.ReadSingle("Unk Attacker State 3 3");
                packet.Translator.ReadSingle("Unk Attacker State 3 4");
                packet.Translator.ReadSingle("Unk Attacker State 3 5");
                packet.Translator.ReadSingle("Unk Attacker State 3 6");
                packet.Translator.ReadSingle("Unk Attacker State 3 7");
                packet.Translator.ReadSingle("Unk Attacker State 3 8");
                packet.Translator.ReadSingle("Unk Attacker State 3 9");
                packet.Translator.ReadSingle("Unk Attacker State 3 10");
                packet.Translator.ReadSingle("Unk Attacker State 3 11");
                packet.Translator.ReadInt32("Unk Attacker State 3 12");
                packet.Translator.ReadInt32("Unk Attacker State 3 13");
                packet.Translator.ReadInt32("Unk Attacker State 3 14");
            }
        }

        [Parser(Opcode.SMSG_DUEL_OUT_OF_BOUNDS)]
        [Parser(Opcode.SMSG_CANCEL_COMBAT)]
        [Parser(Opcode.CMSG_ATTACK_STOP)]
        [Parser(Opcode.SMSG_ATTACKSWING_NOTINRANGE)]
        [Parser(Opcode.SMSG_ATTACKSWING_BADFACING)]
        [Parser(Opcode.SMSG_ATTACKSWING_DEADTARGET)]
        [Parser(Opcode.SMSG_ATTACKSWING_CANT_ATTACK)]
        public static void HandleCombatNull(Packet packet)
        {
        }
    }
}
