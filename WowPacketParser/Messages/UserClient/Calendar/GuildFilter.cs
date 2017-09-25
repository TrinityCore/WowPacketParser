using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.Calendar
{
    public unsafe struct GuildFilter
    {
        public byte MinLevel;
        public byte MaxLevel;
        public byte MaxRankOrder;

        [Parser(Opcode.CMSG_CALENDAR_GUILD_FILTER)]
        public static void HandleCalendarGuildFilter(Packet packet)
        {
            packet.ReadInt32("Min Level");
            packet.ReadInt32("Max Level");
            packet.ReadInt32("Min Rank");
        }
    }
}
