using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientBFMgrQueueInviteResponse
    {
        public ulong QueueID;
        public bool AcceptedInvite;

        [Parser(Opcode.CMSG_BF_MGR_QUEUE_INVITE_RESPONSE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleBattlefieldMgrQueueInviteResponse(Packet packet)
        {
            packet.ReadInt32("Battle Id");
            packet.ReadBool("Accepted");
        }
    }
}
