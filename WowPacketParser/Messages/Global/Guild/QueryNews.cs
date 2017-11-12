using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct QueryNews
    {
        public ulong GuildGUID;

        [Parser(Opcode.CMSG_GUILD_QUERY_NEWS, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildQueryNews(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_GUILD_QUERY_NEWS, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildQueryNews434(Packet packet)
        {
            var guid = packet.StartBitStream(4, 2, 6, 3, 5, 0, 1, 7);
            packet.ParseBitStream(guid, 4, 1, 5, 6, 0, 3, 7, 2);
            packet.WriteGuid("GUID", guid);
        }
        
        [Parser(Opcode.CMSG_GUILD_QUERY_NEWS, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGuildQueryNews602(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
        }
    }
}
