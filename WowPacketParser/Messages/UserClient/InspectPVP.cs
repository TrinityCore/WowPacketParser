using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct InspectPVP
    {
        public ulong InspectTarget;
        public uint InspectRealmAddress;

        [Parser(Opcode.CMSG_INSPECT_PVP)]
        public static void HandleRequestInspectPVP(Packet packet)
        {
            packet.ReadPackedGuid128("InspectTarget");
            packet.ReadInt32("InspectRealmAddress");
        }
    }
}
