using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.SMSG_SEASON_INFO)]
        public static void HandleSeasonInfo(Packet packet)
        {
            packet.ReadInt32("MythicPlusSeasonID");
            packet.ReadInt32("CurrentSeason");
            packet.ReadInt32("PreviousSeason");
            packet.ReadInt32("ConquestWeeklyProgressCurrencyID");
            packet.ReadInt32("PvpSeasonID");
            packet.ReadBit("WeeklyRewardChestsEnabled");
        }
    }
}
