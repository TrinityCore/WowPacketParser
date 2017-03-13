using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.ClientConnection
{
    public unsafe struct ClientConnectionSuspendComms
    {
        public uint Serial;

        [Parser(Opcode.SMSG_SUSPEND_COMMS)]
        public static void HandleSuspendCommsPackets(Packet packet)
        {
            packet.ReadInt32("Serial");
        }
    }
}
