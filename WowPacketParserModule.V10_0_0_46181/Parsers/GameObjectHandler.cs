using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class GameObjectHandler
    {
        [Parser(Opcode.SMSG_GAME_OBJECT_INTERACTION)]
        public static void HandleGameObjectInteraction(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
            packet.ReadInt32("InteractionType");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_CLOSE_INTERACTION)]
        public static void HandleGameObjectCloseInteractionResponse(Packet packet)
        {
            packet.ReadInt32("InteractionType");
        }
    }
}
