using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct ReadyCheckResponse
    {
        public byte PartyIndex;
        public ulong PartyGUID;
        public bool IsReady;

        [Parser(Opcode.CMSG_READY_CHECK_RESPONSE)]
        public static void HandleClientReadyCheckResponse(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("PartyGUID");
            packet.ReadBit("IsReady");
        }
    }
}
