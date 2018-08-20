using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using AttackSwingErr = WowPacketParserModule.V8_0_1_27101.Enums.AttackSwingErr;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.CMSG_SET_WAR_MODE)]
        public static void HandleToggleWarmode(Packet packet)
        {
            packet.ReadBit("Enable");
        }

        [Parser(Opcode.SMSG_ATTACK_SWING_ERROR)]
        public static void HandleAttackSwingError(Packet packet)
        {
            packet.ReadBitsE<AttackSwingErr>("Reason", 3);
        }

        [Parser(Opcode.CMSG_DUEL_RESPONSE)]
        public static void HandleDuelResponse(Packet packet)
        {
            packet.ReadPackedGuid128("ArbiterGUID");
            packet.ReadBit("Accepted");
            packet.ReadBit("Forfeited");
        }
    }
}
