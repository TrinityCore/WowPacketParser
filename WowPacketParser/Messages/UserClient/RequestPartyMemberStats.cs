using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct RequestPartyMemberStats
    {
        public ulong Target;
        public byte PartyIndex;

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS, ClientVersionBuild.Zero, ClientVersionBuild.V5_4_0_17359)]
        public static void HandleRequestPartyMemberStats(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS, ClientVersionBuild.V5_4_0_17359, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleRequestPartyMemberStats540(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadByte("Online?");
            packet.StartBitStream(guid, 7, 1, 4, 3, 6, 2, 5, 0);
            packet.ParseBitStream(guid, 7, 0, 4, 2, 1, 6, 5, 3);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleRequestPartyMemberStats542(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadByte("Online?");
            packet.StartBitStream(guid, 7, 1, 4, 3, 6, 5, 0, 2);
            packet.ParseBitStream(guid, 3, 4, 7, 6, 0, 1, 5, 2);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleRequestPartyMemberStats547(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadByte("Online?");

            packet.StartBitStream(guid, 5, 3, 4, 1, 6, 0, 2, 7);
            packet.ParseBitStream(guid, 0, 3, 1, 2, 7, 5, 4, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleRequestPartyMemberStats548(Packet packet)
        {
            packet.ReadByte("Flags");
            var guid = new byte[8];
            packet.StartBitStream(guid, 7, 4, 0, 1, 3, 6, 2, 5);
            packet.ReadXORBytes(guid, 3, 6, 5, 2, 1, 4, 0, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_REQUEST_PARTY_MEMBER_STATS, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleRequestPartyMemberStats602(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            packet.ReadPackedGuid128("Target");
        }
    }
}
