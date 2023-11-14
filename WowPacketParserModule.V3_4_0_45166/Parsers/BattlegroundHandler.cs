using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class BattlegroundHandler
    {
        public static void ReadRatedPvpBracketInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32("PersonalRating", idx);
            packet.ReadInt32("Ranking", idx);
            packet.ReadInt32("SeasonPlayed", idx);
            packet.ReadInt32("SeasonWon", idx);
            packet.ReadInt32("WeeklyPlayed", idx);
            packet.ReadInt32("WeeklyWon", idx);
            packet.ReadInt32("WeeklyBest", idx);
            packet.ReadInt32("SeasonBest", idx);
            packet.ReadInt32("LastWeeksBestRating", idx);
            packet.ReadInt32("PvpTierID", idx);
            packet.ReadInt32("Unused1", idx);
            packet.ReadInt32("Unused2", idx);
            packet.ReadInt32("Unused3", idx);
            packet.ReadInt32("Unused4", idx);
            packet.ReadInt32("Unused5", idx);
            packet.ResetBitReader();
            packet.ReadBit("Disqualified", idx);
        }

        public static void ReadPvPMatchPlayerStatistics(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("PlayerGUID", idx);
            packet.ReadUInt32("Kills", idx);
            packet.ReadUInt32("DamageDone", idx);
            packet.ReadUInt32("HealingDone", idx);
            var statsCount = packet.ReadUInt32("StatsCount", idx);
            packet.ReadInt32("PrimaryTalentTree", idx);
            packet.ReadByteE<Gender>("Sex", idx);
            packet.ReadInt32E<Race>("Race", idx);
            packet.ReadInt32E<Class>("Class", idx);
            packet.ReadInt32("CreatureID", idx);
            packet.ReadInt32("HonorLevel", idx);
            packet.ReadInt32("Role", idx);

            for (int i = 0; i < statsCount; i++)
                packet.ReadUInt32("Stats", i, idx);

            packet.ResetBitReader();

            packet.ReadBit("Faction", idx);
            packet.ReadBit("IsInWorld", idx);

            var hasHonor = packet.ReadBit("HasHonor", idx);
            var hasPreMatchRating = packet.ReadBit("HasPreMatchRating", idx);
            var hasRatingChange = packet.ReadBit("HasRatingChange", idx);
            var hasPreMatchMMR = packet.ReadBit("HasPreMatchMMR", idx);
            var hasMmrChange = packet.ReadBit("HasMmrChange", idx);

            packet.ResetBitReader();

            if (hasHonor)
                V6_0_2_19033.Parsers.BattlegroundHandler.ReadHonorData(packet, "Honor");

            if (hasPreMatchRating)
                packet.ReadUInt32("PreMatchRating", idx);

            if (hasRatingChange)
                packet.ReadInt32("RatingChange", idx);

            if (hasPreMatchMMR)
                packet.ReadUInt32("PreMatchMMR", idx);

            if (hasMmrChange)
                packet.ReadInt32("MmrChange", idx);
        }

        [Parser(Opcode.SMSG_RATED_PVP_INFO, ClientVersionBuild.V3_4_0_44832, ClientVersionBuild.V3_4_3_51505)]
        public static void HandleRatedBattlefieldInfo(Packet packet)
        {
            for (int i = 0; i < 6; i++)
                ReadRatedPvpBracketInfo(packet, i);
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_SKIRMISH)]
        public static void HandleBattlemasterJoinSkirmish(Packet packet)
        {
            packet.ReadPackedGuid128("BattlemasterGUID");
            packet.ReadByteE<LfgRoleFlag>("Roles");
            packet.ReadByte("Bracket");
            packet.ResetBitReader();
            packet.ReadBit("IsRequeue");
        }

        [Parser(Opcode.SMSG_PVP_LOG_DATA)]
        public static void HandlePvPLogData(Packet packet)
        {
            var hasRatings = packet.ReadBit("HasRatings");
            var hasNames = packet.ReadBit("HasNames");
            var hasWinner = packet.ReadBit("HasWinner");

            if (hasNames)
            {
                byte[] stringLength = new byte[2];
                for (int i = 0; i < 2; i++)
                    stringLength[i] = packet.ReadByte("StringLength", i);

                for (int i = 0; i < 2; i++)
                {
                    packet.ReadPackedGuid128("PlayerGUID");
                    packet.ReadWoWString("PlayerName", stringLength[i], i);
                }
            }

            var statisticsCount = packet.ReadUInt32();

            for (int i = 0; i < 2; i++)
            {
                packet.ReadByte("PlayerCount", i);
            }

            if (hasRatings)
            {
                for (int i = 0; i < 2; i++)
                {
                    packet.ReadUInt32("TeamRating", i);
                    packet.ReadUInt32("NewTeamRating", i);
                    packet.ReadUInt32("AvgMatchmakingRating", i);
                }
            }

            if (hasWinner)
                packet.ReadByte("Winner");

            for (int i = 0; i < statisticsCount; i++)
                ReadPvPMatchPlayerStatistics(packet, "Statistics", i);
        }
    }
}
