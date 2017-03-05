using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using SpellParsers = WowPacketParserModule.V6_0_2_19033.Parsers.SpellHandler;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class CombatHandler
    {
        public static void ReadAttackRoundInfo(Packet packet, params object[] indexes)
        {
            var hitInfo = packet.Translator.ReadInt32E<SpellHitInfo>("HitInfo");

            packet.Translator.ReadPackedGuid128("Attacker Guid");
            packet.Translator.ReadPackedGuid128("Target Guid");

            packet.Translator.ReadInt32("Damage");
            packet.Translator.ReadInt32("OverDamage");

            var subDmgCount = packet.Translator.ReadBool("HasSubDmg");
            if (subDmgCount)
            {
                packet.Translator.ReadInt32("SchoolMask");
                packet.Translator.ReadSingle("Float Damage");
                packet.Translator.ReadInt32("Int Damage");

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_ABSORB | SpellHitInfo.HITINFO_FULL_ABSORB))
                    packet.Translator.ReadInt32("Damage Absorbed");

                if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_PARTIAL_RESIST | SpellHitInfo.HITINFO_FULL_RESIST))
                    packet.Translator.ReadInt32("Damage Resisted");
            }

            packet.Translator.ReadByteE<VictimStates>("VictimState");
            packet.Translator.ReadInt32("Unk Attacker State 0");

            packet.Translator.ReadInt32<SpellId>("Melee Spell ID");

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
            }

            if (hitInfo.HasAnyFlag(SpellHitInfo.HITINFO_BLOCK | SpellHitInfo.HITINFO_UNK12))
                packet.Translator.ReadSingle("Unk Float");
        }

        [Parser(Opcode.SMSG_ATTACKER_STATE_UPDATE)]
        public static void HandleAttackerStateUpdate(Packet packet)
        {
            var bit52 = packet.Translator.ReadBit("HasLogData");

            if (bit52)
                SpellParsers.ReadSpellCastLogData(packet);

            packet.Translator.ReadInt32("Size");

            ReadAttackRoundInfo(packet);
        }

        [Parser(Opcode.SMSG_ATTACK_START)]
        public static void HandleAttackStartStart(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Attacker Guid");
            packet.Translator.ReadPackedGuid128("Victim Guid");
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Attacker Guid");
            packet.Translator.ReadPackedGuid128("Victim Guid");

            packet.Translator.ReadBit("NowDead");
        }

        [Parser(Opcode.SMSG_HIGHEST_THREAT_UPDATE)]
        public static void HandleHighestThreatlistUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("UnitGUID");
            packet.Translator.ReadPackedGuid128("HighestThreatGUID");

            var count = packet.Translator.ReadUInt32("ThreatListCount");

            // ThreatInfo
            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadPackedGuid128("UnitGUID", i);
                packet.Translator.ReadUInt32("Threat", i);
            }
        }

        [Parser(Opcode.SMSG_THREAT_UPDATE)]
        public static void HandleThreatlistUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("UnitGUID");
            var int16 = packet.Translator.ReadInt32("Targets");

            for (int i = 0; i < int16; i++)
            {
                packet.Translator.ReadPackedGuid128("TargetGUID", i);
                packet.Translator.ReadInt32("Threat", i);
            }
        }

        [Parser(Opcode.SMSG_THREAT_REMOVE)]
        public static void HandleRemoveThreatlist(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("UnitGUID");
            packet.Translator.ReadPackedGuid128("AboutGUID");
        }

        [Parser(Opcode.SMSG_THREAT_CLEAR)]
        public static void HandleClearThreatlist(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("GUID");
        }

        [Parser(Opcode.SMSG_AI_REACTION)]
        public static void HandleAIReaction(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("UnitGUID");
            packet.Translator.ReadInt32E<AIReaction>("Reaction");
        }

        [Parser(Opcode.CMSG_ATTACK_SWING)]
        public static void HandleAttackSwing(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Victim");
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_SET_SHEATHED)]
        public static void HandleSetSheathed(Packet packet)
        {
            packet.Translator.ReadInt32E<SheathState>("CurrentSheathState");
            packet.Translator.ReadBit("Animate");
        }

        [Parser(Opcode.SMSG_PARTY_KILL_LOG)]
        public static void HandlePartyKillLog(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("PlayerGUID");
            packet.Translator.ReadPackedGuid128("VictimGUID");
        }

        [Parser(Opcode.SMSG_ATTACK_SWING_ERROR)]
        public static void HandleAttackSwingError(Packet packet)
        {
            packet.Translator.ReadBitsE<AttackSwingErr>("Reason", 2);
        }

        [Parser(Opcode.SMSG_COMBAT_EVENT_FAILED)]
        public static void HandleAttackEventFailed(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Attacker");
            packet.Translator.ReadPackedGuid128("Victim");
        }

        [Parser(Opcode.SMSG_DUEL_WINNER)]
        public static void HandleDuelWinner(Packet packet)
        {
            var bits80 = packet.Translator.ReadBits(6);
            var bits24 = packet.Translator.ReadBits(6);

            packet.Translator.ReadBit("Fled");

            // Order guessed
            packet.Translator.ReadUInt32("BeatenVirtualRealmAddress");
            packet.Translator.ReadUInt32("WinnerVirtualRealmAddress");

            // Order guessed
            packet.Translator.ReadWoWString("BeatenName", bits80);
            packet.Translator.ReadWoWString("WinnerName", bits24);
        }

        [Parser(Opcode.SMSG_CAN_DUEL_RESULT)]
        public static void HandleCanDuelResult(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TargetGUID");
            packet.Translator.ReadBit("Result");
        }

        [Parser(Opcode.SMSG_DUEL_REQUESTED)]
        public static void HandleDuelRequested(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ArbiterGUID");
            packet.Translator.ReadPackedGuid128("RequestedByGUID");
            packet.Translator.ReadPackedGuid128("RequestedByWowAccount");
        }

        [Parser(Opcode.CMSG_DUEL_RESPONSE)]
        public static void HandleDuelResponse(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ArbiterGUID");
            packet.Translator.ReadBit("Accepted");
        }

        [Parser(Opcode.CMSG_CAN_DUEL)]
        public static void HandleCanDuel(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TargetGUID");
        }

        [Parser(Opcode.SMSG_PVP_CREDIT)]
        public static void HandlePvPCredit(Packet packet)
        {
            packet.Translator.ReadInt32("Honor");
            packet.Translator.ReadPackedGuid128("Target");
            packet.Translator.ReadInt32("Rank");
        }
    }
}
