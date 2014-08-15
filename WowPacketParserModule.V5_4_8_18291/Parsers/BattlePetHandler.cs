using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class BattlePetHandler
    {
        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_ACQUIRED)]
        public static void HandleZeroLengthPackets(Packet packet)
        {
        }
    }
}
