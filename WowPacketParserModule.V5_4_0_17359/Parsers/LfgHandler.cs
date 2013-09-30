using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
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
