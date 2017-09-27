using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.SMSG_PLAYER_DIFFICULTY_CHANGE)]
        public static void HandleSetDifficulty(Packet packet)
        {
            packet.ReadInt32E<MapDifficulty>("Difficulty");
        }
    }
}
