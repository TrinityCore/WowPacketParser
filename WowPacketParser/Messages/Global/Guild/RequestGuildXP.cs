using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct RequestGuildXP
    {
        public ulong GuildGUID;
        
        [Parser(Opcode.CMSG_REQUEST_GUILD_XP, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleRequestGuildXP(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_XP, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleRequestGuildXP430(Packet packet)
        {
            var guid = packet.StartBitStream(2, 4, 5, 6, 1, 0, 3, 7);
            packet.ParseBitStream(guid, 0, 1, 4, 3, 2, 6, 7, 5);
            packet.WriteGuid("GUID", guid);
        }
        
        [Parser(Opcode.CMSG_REQUEST_GUILD_XP, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleRequestGuildXP434(Packet packet)
        {
            var guid = packet.StartBitStream(2, 1, 0, 5, 4, 7, 6, 3);
            packet.ParseBitStream(guid, 7, 2, 3, 6, 1, 5, 0, 4);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_REQUEST_GUILD_XP, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleRequestGuildXP547(Packet packet)
        {
            var guid = packet.StartBitStream(3, 2, 7, 5, 6, 0, 1, 4);
            packet.ParseBitStream(guid, 3, 7, 2, 1, 5, 4, 0, 6);
            packet.WriteGuid("GUID", guid);
        }
    }
}
