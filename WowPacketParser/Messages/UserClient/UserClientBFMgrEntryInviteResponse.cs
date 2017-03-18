using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientBFMgrEntryInviteResponse
    {
        public ulong QueueID;
        public bool AcceptedInvite;

        [Parser(Opcode.CMSG_BF_MGR_ENTRY_INVITE_RESPONSE)]
        public static void HandleBattlefieldMgrEntryInviteResponse434(Packet packet)
        {
            var guid = new byte[8];
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            packet.ReadBit("Accepted");
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();


            packet.ParseBitStream(guid, 0, 3, 4, 2, 1, 6, 7, 5);
            packet.WriteGuid("Guid", guid);
        }
    }
}
