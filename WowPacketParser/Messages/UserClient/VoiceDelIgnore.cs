using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct VoiceDelIgnore
    {
        public QualifiedGUID Player;

        [Parser(Opcode.CMSG_VOICE_DEL_IGNORE, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleDelVoiceIgnore(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_VOICE_DEL_IGNORE, ClientVersionBuild.V6_0_2_19033)]
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
