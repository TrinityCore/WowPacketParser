using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct GetRanks
    {
        public ulong GuildGUID;

        [Parser(Opcode.CMSG_GUILD_GET_RANKS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleGuildRanks(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_GUILD_GET_RANKS, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleGuildRanks430(Packet packet)
        {
            var guid = packet.StartBitStream(7, 2, 0, 4, 6, 5, 1, 3);
            packet.ParseBitStream(guid, 7, 5, 2, 6, 1, 4, 0, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GUILD_GET_RANKS, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRanks433(Packet packet)
        {
            var guid = packet.StartBitStream(2, 6, 1, 0, 5, 3, 7, 4);
            packet.ParseBitStream(guid, 3, 6, 5, 4, 0, 7, 2, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GUILD_GET_RANKS, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleGuildRanks434(Packet packet)
        {
            var guid = packet.StartBitStream(2, 3, 0, 6, 4, 7, 5, 1);
            packet.ParseBitStream(guid, 3, 4, 5, 7, 1, 0, 6, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GUILD_GET_RANKS, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildRanks547(Packet packet)
        {
            var guid = packet.StartBitStream(2, 7, 0, 5, 6, 3, 4, 1);
            packet.ParseBitStream(guid, 6, 0, 1, 7, 2, 3, 4, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GUILD_GET_RANKS, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildGuildGUID(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
        }
    }
}
