using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct MinimapPing
    {
        public Vector2 Position;
        public byte PartyIndex;

        [Parser(Opcode.CMSG_MINIMAP_PING)]
        public static void HandleClientMinimapPing(Packet packet)
        {
            packet.ReadVector2("Position");
            packet.ReadByte("PartyIndex");
        }
    }
}
