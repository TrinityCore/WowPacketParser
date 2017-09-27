using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.Query
{
    public unsafe struct RealmName
    {
        public uint VirtualRealmAddress;

        [Parser(Opcode.CMSG_QUERY_REALM_NAME, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleRealmQuery(Packet packet)
        {
            packet.ReadInt32("Realm Id");
        }

        [Parser(Opcode.CMSG_QUERY_REALM_NAME, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleRealmQuery602(Packet packet)
        {
            packet.ReadInt32("VirtualRealmAddress");
        }
    }
}
