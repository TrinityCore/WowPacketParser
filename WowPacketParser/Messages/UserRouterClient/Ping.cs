using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserRouterClient
{
    public unsafe struct Ping
    {
        public uint Serial;
        public uint Latency;

        [Parser(Opcode.CMSG_PING, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleClientPing(Packet packet)
        {
            packet.ReadInt32("Latency");
            packet.ReadInt32("Serial");
        }

        [Parser(Opcode.CMSG_PING, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleClientPing602(Packet packet)
        {
            packet.ReadInt32("Serial");
            packet.ReadInt32("Latency");
        }
    }
}
