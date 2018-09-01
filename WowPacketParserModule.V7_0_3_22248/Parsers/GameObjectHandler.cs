using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class GameObjectHandler
    {
        [Parser(Opcode.SMSG_GAME_OBJECT_SET_STATE)]
        public static void HandleGameObjectSetState(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ReadByte("State");
        }
    }
}
