using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.ClientConnection
{
    public unsafe struct ClientConnectionPong
    {
        public uint Serial;

        [Parser(Opcode.SMSG_PONG)]
        public static void HandleServerPong(Packet packet)
        {
            packet.ReadInt32("Serial");
        }
    }
}
