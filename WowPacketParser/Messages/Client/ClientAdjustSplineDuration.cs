using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAdjustSplineDuration
    {
        public ulong Unit;
        public float Scale;

        [Parser(Opcode.SMSG_ADJUST_SPLINE_DURATION, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAdjustSplineDuration(Packet packet)
        {
            packet.ReadGuid("Unit"); // guessed
            packet.ReadSingle("Scale");
        }

        [Parser(Opcode.SMSG_ADJUST_SPLINE_DURATION, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAdjustSplineDuration6(Packet packet)
        {
            packet.ReadPackedGuid128("Unit");
            packet.ReadSingle("Scale");
        }
    }
}
