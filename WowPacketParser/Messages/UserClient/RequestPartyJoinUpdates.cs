using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct RequestPartyJoinUpdates
    {
        public byte PartyIndex;

        [Parser(Opcode.CMSG_REQUEST_PARTY_JOIN_UPDATES)]
        public static void HandleLeaveGroup(Packet packet)
        {
            packet.ReadByte("PartyIndex");
        }
    }
}
