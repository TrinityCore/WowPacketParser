using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class CombatHandler
    {
        [Parser(Opcode.CMSG_SET_WAR_MODE)]
        public static void HandleToggleWarmode(Packet packet)
        {
            packet.ReadBit("Enable");
        }
    }
}
