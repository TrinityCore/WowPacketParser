using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct LeaveGroup
    {
        public byte PartyIndex;

        [Parser(Opcode.CMSG_LEAVE_GROUP)]
        public static void HandleLeaveGroup(Packet packet)
        {
            packet.ReadByte("PartyIndex");
        }
    }
}
