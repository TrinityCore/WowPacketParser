using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.CMSG_ATTACK_SWING)]
        public static void HandleAttackSwing(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_DUEL_REQUESTED)]
        public static void HandleDuelRequested(Packet packet)
        {
            packet.ReadGuid("Flag GUID");
            packet.ReadGuid("Opponent GUID");
        }

        [Parser(Opcode.SMSG_DUEL_COMPLETE)]
        public static void HandleDuelComplete(Packet packet)
        {
            packet.ReadBool("Abnormal finish");
        }

        [Parser(Opcode.SMSG_DUEL_WINNER)]
        public static void HandleDuelWinner(Packet packet)
        {
            packet.ReadBool("Abnormal finish");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545)) // Probably earlier
            {
                packet.ReadCString("Name");
                packet.ReadCString("Opponent Name");
            }
            else
            {
                packet.ReadCString("Opponent Name");
                packet.ReadCString("Name");
            }
        }

        [Parser(Opcode.SMSG_DUEL_COUNTDOWN)]
        public static void HandleDuelCountDown(Packet packet)
        {
            packet.ReadInt32("Timer");
        }

        [Parser(Opcode.SMSG_RESET_RANGED_COMBAT_TIMER)]
        public static void HandleResetRangedCombatTimer(Packet packet)
        {
            packet.ReadInt32("Timer");
        }

        [Parser(Opcode.CMSG_TOGGLE_PVP)]
        public static void HandleTogglePvP(Packet packet)
        {
            // this opcode can be used in two ways: Either set new status explicitly or toggle old status
            if (packet.CanRead())
                packet.ReadBool("Enable");
        }

        [Parser(Opcode.SMSG_PVP_CREDIT)]
        public static void HandlePvPCredit(Packet packet)
        {
            packet.ReadUInt32("Honor");
            packet.ReadGuid("GUID");
            packet.ReadInt32("Rank");
        }

        [Parser(Opcode.CMSG_SET_SHEATHED)]
        public static void HandleSetSheathed(Packet packet)
        {
            packet.ReadInt32E<SheathState>("Sheath");
        }

        [Parser(Opcode.SMSG_PARTY_KILL_LOG)]
        public static void HandlePartyKillLog(Packet packet)
        {
            packet.ReadGuid("Player GUID");
            packet.ReadGuid("Victim GUID");
        }

        [Parser(Opcode.SMSG_AI_REACTION)]
        public static void HandleAIReaction(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadInt32E<AIReaction>("Reaction");
        }

        [Parser(Opcode.SMSG_UPDATE_COMBO_POINTS)]
        public static void HandleUpdateComboPoints(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadByte("Combo Points");
        }

        [Parser(Opcode.SMSG_ENVIRONMENTAL_DAMAGE_LOG)]
        public static void HandleEnvirenmentalDamageLog(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByteE<EnvironmentDamage>("Type");
            packet.ReadInt32("Damage");
            packet.ReadInt32("Absorb");
            packet.ReadInt32("Resist");
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            packet.ReadPackedGuid("Target GUID");
        }

        [Parser(Opcode.SMSG_ATTACK_START)]
        public static void HandleAttackStartStart(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadGuid("Victim GUID");
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        [Parser(Opcode.SMSG_COMBAT_EVENT_FAILED)]
        public static void HandleAttackStartStop(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadPackedGuid("Victim GUID");
            packet.ReadInt32("Unk int"); // Has something to do with facing?
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleAttackerStateUpdate406(Packet packet)
        {
            var hitInfo = packet.ReadInt32E<SpellHitInfo>("HitInfo");
            packet.ReadPackedGuid("AttackerGUID");
            packet.ReadPackedGuid("TargetGUID");
            packet.ReadInt32("Damage");
            packet.ReadInt32("OverDamage");

            var subDmgCount = packet.ReadByte("Count");
            for (var i = 0; i < subDmgCount; ++i)
            {
                packet.ReadInt32("SchoolMask", i);
                packet.ReadSingle("Float Damage", i);
                packet.ReadInt32("Int Damage", i);
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                for (var i = 0; i < subDmgCount; ++i)
                    packet.ReadInt32("Damage Absorbed", i);

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                    for (var i = 0; i < subDmgCount; ++i)
                        packet.ReadInt32("Damage Resisted", i);

            packet.ReadByteE<VictimStates>("VictimState");
            packet.ReadInt32("Unk Attacker State 0");

            packet.ReadInt32<SpellId>("Melee Spell Id");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
                packet.ReadInt32("Block Amount");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_RAGE_GAIN))
                packet.ReadInt32("Rage Gained");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_UNK0))
            {
                packet.ReadInt32("Unk Attacker State 3 1");
                packet.ReadSingle("Unk Attacker State 3 2");
                packet.ReadSingle("Unk Attacker State 3 3");
                packet.ReadSingle("Unk Attacker State 3 4");
                packet.ReadSingle("Unk Attacker State 3 5");
                packet.ReadSingle("Unk Attacker State 3 6");
                packet.ReadSingle("Unk Attacker State 3 7");
                packet.ReadSingle("Unk Attacker State 3 8");
                packet.ReadSingle("Unk Attacker State 3 9");
                packet.ReadSingle("Unk Attacker State 3 10");
                packet.ReadSingle("Unk Attacker State 3 11");
                packet.ReadInt32("Unk Attacker State 3 12");
                packet.ReadInt32("Unk Attacker State 3 13");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK | SpellHitInfo.HITINFO_UNK12))
                    packet.ReadSingle("Unk Float");
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var hitInfo = packet.ReadInt32E<SpellHitInfo>("HitInfo");
            packet.ReadPackedGuid("AttackerGUID");
            packet.ReadPackedGuid("TargetGUID");
            packet.ReadInt32("Damage");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183))
                packet.ReadInt32("OverDamage");

            var subDmgCount = packet.ReadByte();
            for (var i = 0; i < subDmgCount; ++i)
            {
                packet.ReadInt32("SchoolMask", i);
                packet.ReadSingle("Float Damage", i);
                packet.ReadInt32("Int Damage", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_3_9183) ||
                    hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                    packet.ReadInt32("Damage Absorbed", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_3_9183) ||
                    hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                    packet.ReadInt32("Damage Resisted", i);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_3_9183))
                packet.ReadByteE<VictimStates>("VictimState");
            else
                packet.ReadInt32E<VictimStates>("VictimState");

            packet.ReadInt32("Unk Attacker State 0");

            packet.ReadInt32<SpellId>("Melee Spell ID ");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_3_9183) ||
                hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK))
                packet.ReadInt32("Block Amount");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_RAGE_GAIN))
                packet.ReadInt32("Rage Gained");

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_UNK0))
            {
                packet.ReadInt32("Unk Attacker State 3 1");
                packet.ReadSingle("Unk Attacker State 3 2");
                packet.ReadSingle("Unk Attacker State 3 3");
                packet.ReadSingle("Unk Attacker State 3 4");
                packet.ReadSingle("Unk Attacker State 3 5");
                packet.ReadSingle("Unk Attacker State 3 6");
                packet.ReadSingle("Unk Attacker State 3 7");
                packet.ReadSingle("Unk Attacker State 3 8");
                packet.ReadSingle("Unk Attacker State 3 9");
                packet.ReadSingle("Unk Attacker State 3 10");
                packet.ReadSingle("Unk Attacker State 3 11");
                packet.ReadInt32("Unk Attacker State 3 12");
                packet.ReadInt32("Unk Attacker State 3 13");
                packet.ReadInt32("Unk Attacker State 3 14");
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
