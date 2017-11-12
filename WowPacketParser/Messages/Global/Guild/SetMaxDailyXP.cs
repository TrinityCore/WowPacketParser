using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.Guild
{
    public unsafe struct SetMaxDailyXP
    {
        public int MaxGuildDailyXP;
        
        [Parser(Opcode.CMSG_GUILD_REQUEST_MAX_DAILY_XP, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleGuildRequestMaxDailyXP(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_GUILD_REQUEST_MAX_DAILY_XP, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleGuildRequestMaxDailyXP430(Packet packet)
        {
            var guid = packet.StartBitStream(6, 5, 2, 3, 0, 4, 7, 1);
            packet.ParseBitStream(guid, 1, 6, 4, 5, 3, 7, 0, 2);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_REQUEST_MAX_DAILY_XP, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleGuildRequestMaxDailyXP434(Packet packet)
        {
            var guid = packet.StartBitStream(0, 3, 5, 1, 4, 6, 7, 2);
            packet.ParseBitStream(guid, 7, 4, 3, 5, 1, 2, 6, 0);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GUILD_REQUEST_MAX_DAILY_XP, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleGuildRequestMaxDailyXP510(Packet packet)
        {
            var guid = packet.StartBitStream(5, 3, 6, 4, 7, 2, 1, 0);
            packet.ParseBitStream(guid, 4, 7, 5, 6, 0, 1, 2, 3);
            packet.WriteGuid("GUID", guid);
        }
    }
}
