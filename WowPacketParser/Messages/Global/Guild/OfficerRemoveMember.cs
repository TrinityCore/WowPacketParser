using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct OfficerRemoveMember
    {
        public ulong Removee;

        [Parser(Opcode.CMSG_GUILD_OFFICER_REMOVE_MEMBER, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleGuildCreate(Packet packet)
        {
            packet.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_GUILD_OFFICER_REMOVE_MEMBER, ClientVersionBuild.V4_0_6_13596, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRemove406(Packet packet)
        {
            packet.ReadGuid("Target GUID");
            packet.ReadGuid("Removee GUID");
        }

        [Parser(Opcode.CMSG_GUILD_OFFICER_REMOVE_MEMBER, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleGuildRemove434(Packet packet)
        {
            var guid = packet.StartBitStream(6, 5, 4, 0, 1, 3, 7, 2);
            packet.ParseBitStream(guid, 2, 6, 5, 7, 1, 4, 3, 0);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_OFFICER_REMOVE_MEMBER, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleGuildRemove547(Packet packet)
        {
            var guid = packet.StartBitStream(3, 1, 6, 0, 7, 2, 5, 4);
            packet.ParseBitStream(guid, 2, 6, 0, 1, 4, 3, 5, 7);
            packet.WriteGuid("GUID", guid);
        }

    }
}
