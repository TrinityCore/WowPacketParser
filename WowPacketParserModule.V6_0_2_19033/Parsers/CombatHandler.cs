using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using SpellParsers = WowPacketParserModule.V6_0_2_19033.Parsers.SpellHandler;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.SMSG_ATTACK_START)]
        public static void HandleAttackStartStart(Packet packet)
        {
            packet.ReadPackedGuid128("Attacker Guid");
            packet.ReadPackedGuid128("Victim Guid");
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            packet.ReadPackedGuid128("Attacker Guid");
            packet.ReadPackedGuid128("Victim Guid");

            packet.ReadBit("NowDead");
        }

        [Parser(Opcode.SMSG_HIGHEST_THREAT_UPDATE)]
        public static void HandleHighestThreatlistUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadPackedGuid128("HighestThreatGUID");

            var count = packet.ReadUInt32("ThreatListCount");

            // ThreatInfo
            for (var i = 0; i < count; i++)
            {
                packet.ReadPackedGuid128("UnitGUID", i);
                packet.ReadUInt32("Threat", i);
            }
        }

        [Parser(Opcode.SMSG_THREAT_UPDATE)]
        public static void HandleThreatlistUpdate(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            var int16 = packet.ReadInt32("Targets");

            for (int i = 0; i < int16; i++)
            {
                packet.ReadPackedGuid128("TargetGUID", i);
                packet.ReadInt32("Threat", i);
            }
        }

        [Parser(Opcode.SMSG_THREAT_REMOVE)]
        public static void HandleRemoveThreatlist(Packet packet)
        {
            packet.ReadPackedGuid128("UnitGUID");
            packet.ReadPackedGuid128("AboutGUID");
        }

        [Parser(Opcode.SMSG_THREAT_CLEAR)]
        public static void HandleClearThreatlist(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
        }

        [Parser(Opcode.CMSG_ATTACK_SWING)]
        public static void HandleAttackSwing(Packet packet)
        {
            packet.ReadPackedGuid128("Victim");
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_SET_SHEATHED)]
        public static void HandleSetSheathed(Packet packet)
        {
            packet.ReadInt32E<SheathState>("CurrentSheathState");
            packet.ReadBit("Animate");
        }

        [Parser(Opcode.SMSG_PARTY_KILL_LOG)]
        public static void HandlePartyKillLog(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadPackedGuid128("VictimGUID");
        }

        [Parser(Opcode.SMSG_ATTACK_SWING_ERROR)]
        public static void HandleAttackSwingError(Packet packet)
        {
            packet.ReadBitsE<AttackSwingErr>("Reason", 2);
        }

        [Parser(Opcode.SMSG_COMBAT_EVENT_FAILED)]
        public static void HandleAttackEventFailed(Packet packet)
        {
            packet.ReadPackedGuid128("Attacker");
            packet.ReadPackedGuid128("Victim");
        }

        [Parser(Opcode.SMSG_DUEL_WINNER)]
        public static void HandleDuelWinner(Packet packet)
        {
            var bits80 = packet.ReadBits(6);
            var bits24 = packet.ReadBits(6);

            packet.ReadBit("Fled");

            // Order guessed
            packet.ReadUInt32("BeatenVirtualRealmAddress");
            packet.ReadUInt32("WinnerVirtualRealmAddress");

            // Order guessed
            packet.ReadWoWString("BeatenName", bits80);
            packet.ReadWoWString("WinnerName", bits24);
        }

        [Parser(Opcode.SMSG_CAN_DUEL_RESULT)]
        public static void HandleCanDuelResult(Packet packet)
        {
            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadBit("Result");
        }

        [Parser(Opcode.SMSG_DUEL_REQUESTED)]
        public static void HandleDuelRequested(Packet packet)
        {
            packet.ReadPackedGuid128("ArbiterGUID");
            packet.ReadPackedGuid128("RequestedByGUID");
            packet.ReadPackedGuid128("RequestedByWowAccount");
        }

        [Parser(Opcode.CMSG_DUEL_RESPONSE)]
        public static void HandleDuelResponse(Packet packet)
        {
            packet.ReadPackedGuid128("ArbiterGUID");
            packet.ReadBit("Accepted");
        }

        [Parser(Opcode.CMSG_CAN_DUEL)]
        public static void HandleCanDuel(Packet packet)
        {
            packet.ReadPackedGuid128("TargetGUID");
        }

        [Parser(Opcode.SMSG_PVP_CREDIT)]
        public static void HandlePvPCredit(Packet packet)
        {
            packet.ReadInt32("Honor");
            packet.ReadPackedGuid128("Target");
            packet.ReadInt32("Rank");
        }
    }
}
