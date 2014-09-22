using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class LfgHandler
    {
        [Parser(Opcode.CMSG_DUNGEON_FINDER_GET_SYSTEM_INFO)]
        public static void HandleDungeonFinderGetSystemInfo(Packet packet)
        {
            packet.ReadByte("Unk Byte");
            packet.ReadBit("Unk boolean");
        }
    }
}
