using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct DelFriend
    {
        public QualifiedGUID Player;

        [Parser(Opcode.CMSG_DEL_FRIEND, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleReadGuid(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_DEL_FRIEND, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleDeleteFriendOrIgnoreOrMute(Packet packet)
        {
            readQualifiedGUID(packet, "QualifiedGUID");
        }

        private static void readQualifiedGUID(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("VirtualRealmAddress", indexes);
            packet.ReadPackedGuid128("Guid", indexes);
        }
    }
}
