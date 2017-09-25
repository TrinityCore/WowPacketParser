using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.BFMgr
{
    public unsafe struct QueueExitRequest
    {
        public ulong QueueID;

        [Parser(Opcode.CMSG_BF_MGR_QUEUE_EXIT_REQUEST, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleBattlefieldMgrExitRequest434(Packet packet)
        {
            var guid = packet.StartBitStream(2, 0, 3, 7, 4, 5, 6, 1);
            packet.ParseBitStream(guid, 5, 2, 0, 1, 4, 3, 7, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_BF_MGR_QUEUE_EXIT_REQUEST, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleBattlefieldMgrExitRequest(Packet packet)
        {
            packet.ReadInt64("QueueID");
        }

    }
}
