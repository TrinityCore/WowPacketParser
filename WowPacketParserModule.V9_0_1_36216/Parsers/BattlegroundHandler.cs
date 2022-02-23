using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.SMSG_SEASON_INFO)]
        public static void HandleSeasonInfo(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
                packet.ReadInt32("MythicPlusDisplaySeasonID");

            packet.ReadInt32("MythicPlusMilestoneSeasonID");
            packet.ReadInt32("CurrentArenaSeason");
            packet.ReadInt32("PreviousArenaSeason");
            packet.ReadInt32("ConquestWeeklyProgressCurrencyID");
            packet.ReadInt32("PvpSeasonID");
            packet.ReadBit("WeeklyRewardChestsEnabled");
        }
    }
}
