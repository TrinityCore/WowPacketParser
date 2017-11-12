using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct DemoteMember
    {
        public ulong Demotee;

        [Parser(Opcode.CMSG_GUILD_DEMOTE_MEMBER, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleGuildDemote(Packet packet)
        {
            packet.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_GUILD_DEMOTE_MEMBER, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleGuildDemote547(Packet packet)
        {
            var guid = packet.StartBitStream(1, 0, 7, 5, 3, 2, 4, 6);
            packet.ParseBitStream(guid, 3, 4, 1, 0, 7, 2, 5, 6);
            packet.WriteGuid("GUID", guid);
        }



        [Parser(Opcode.CMSG_GUILD_DEMOTE_MEMBER, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleGuildDemote548(Packet packet)
        {
            var guid = packet.StartBitStream(3, 6, 0, 2, 7, 5, 4, 1);
            packet.ParseBitStream(guid, 7, 4, 2, 5, 1, 3, 0, 6);
            packet.WriteGuid("GUID", guid);
        }
    }
}
