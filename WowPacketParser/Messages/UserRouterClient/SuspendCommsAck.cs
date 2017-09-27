using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserRouterClient
{
    public unsafe struct SuspendCommsAck
    {
        public uint Serial;
        public uint Timestamp;

        [Parser(Opcode.CMSG_SUSPEND_COMMS_ACK, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSuspendCommsAck(Packet packet)
        {
            packet.ReadInt32("Serial");
        }

        [Parser(Opcode.CMSG_SUSPEND_COMMS_ACK, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSuspendCommsPackets(Packet packet)
        {
            packet.ReadInt32("Serial");
            packet.ReadInt32("Timestamp");
        }
    }
}
