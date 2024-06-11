using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.SMSG_ATTACK_START)]
        public static void HandleAttackStartStart(Packet packet)
        {
            packet.ReadPackedGuid128("Attacker Guid");
            packet.ReadPackedGuid128("Victim Guid");
        }

        [Parser(Opcode.SMSG_AI_REACTION)]
        public static void HandleAIReaction(Packet packet)
        {
            PacketAIReaction aiReaction = packet.Holder.AiReaction = new();
            aiReaction.UnitGuid = packet.ReadPackedGuid128("UnitGUID").ToUniversalGuid();
            aiReaction.Reaction = (WowPacketParser.Proto.AIReaction)packet.ReadInt32E<WowPacketParser.Enums.AIReaction>("Reaction");
        }

        [Parser(Opcode.SMSG_ATTACK_STOP)]
        public static void HandleAttackStartStop(Packet packet)
        {
            packet.ReadPackedGuid128("Attacker Guid");
            packet.ReadPackedGuid128("Victim Guid");

            packet.ReadBit("NowDead");
        }

        [Parser(Opcode.SMSG_CANCEL_AUTO_REPEAT)]
        public static void HandleCancelAutoRepeat(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }
    }
}
