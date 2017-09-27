using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct ResurrectResponse
    {
        public ulong Resurrecter;
        public uint Response;

        [Parser(Opcode.CMSG_RESURRECT_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleResurrectResponse(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadBool("Accept");
        }

        [Parser(Opcode.CMSG_RESURRECT_RESPONSE, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleResurrectResponse602(Packet packet)
        {
            packet.ReadPackedGuid128("Resurrecter");
            packet.ReadInt32("Response");
        }
    }
}
