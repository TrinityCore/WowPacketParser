using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct QueryRecipes
    {
        public ulong GuildGUID;


        [Parser(Opcode.CMSG_GUILD_QUERY_RECIPES, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleGuildQueryRecipes(Packet packet)
        {
            var guid = packet.StartBitStream(5, 6, 1, 4, 2, 7, 0, 3);
            packet.ParseBitStream(guid, 3, 1, 0, 5, 4, 2, 6, 7);
            packet.WriteGuid("Guild GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_RECIPES, ClientVersionBuild.V5_1_0_16309, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleQueryGuildRecipes510(Packet packet)
        {
            var guid = packet.StartBitStream(1, 4, 3, 5, 0, 6, 2, 7);
            packet.ParseBitStream(guid, 5, 3, 1, 4, 0, 7, 6, 2);
            packet.WriteGuid("Guild Guid", guid);
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_RECIPES, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildGuildGUID(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
        }
    }
}
