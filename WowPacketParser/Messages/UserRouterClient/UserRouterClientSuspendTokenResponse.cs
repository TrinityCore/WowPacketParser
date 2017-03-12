using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserRouterClient
{
    public unsafe struct UserRouterClientSuspendTokenResponse
    {
        public uint Sequence;

        [Parser(Opcode.CMSG_SUSPEND_TOKEN_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSuspendToken(Packet packet)
        {
            packet.ReadUInt32("Sequence");
        }

        [Parser(Opcode.CMSG_SUSPEND_TOKEN_RESPONSE, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleSuspendToken602(Packet packet)
        {
            packet.ReadUInt32("Count");
        }
    }
}
