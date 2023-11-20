using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN, ClientVersionBuild.V8_1_5_29683)]
        public static void HandleBattlemasterJoin(Packet packet)
        {
            var queueCount = packet.ReadInt32();
            packet.ReadByte("Roles");

            for (int i = 0; i < 2; i++)
                packet.ReadInt32("BlacklistMap", i);

            for (int i = 0; i < queueCount; i++)
                V6_0_2_19033.Parsers.BattlegroundHandler.ReadPackedBattlegroundQueueTypeID(packet);
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_ARENA)]
        public static void HandleBattlemasterJoinArena(Packet packet)
        {
            packet.ReadByte("TeamSizeIndex");
            packet.ReadByteE<LfgRoleFlag>("Roles");
        }

        public static void ReadPvPMatchPlayerStatistics(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("PlayerGUID", idx);
            packet.ReadUInt32("Kills", idx);
            packet.ReadUInt32("DamageDone", idx);
            packet.ReadUInt32("HealingDone", idx);
            var statsCount = packet.ReadUInt32("StatsCount", idx);
            packet.ReadInt32("PrimaryTalentTree", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_5_50232))
                packet.ReadByteE<Gender>("Sex", idx);
            else
                packet.ReadInt32E<Gender>("Sex", idx);
            packet.ReadInt32E<Race>("Race", idx);
            packet.ReadInt32E<Class>("Class", idx);
            packet.ReadInt32("CreatureID", idx);
            packet.ReadInt32("HonorLevel", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
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
            var hasPostMatchMMR = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_0_49407))
                hasPostMatchMMR = packet.ReadBit("HasPostMatchMMR", idx);

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

            if (hasPostMatchMMR)
                packet.ReadUInt32("PostMatchMMR", idx);
        }

        public static void ReadRatingData820(Packet packet, params object[] idx)
        {
            for (int i = 0; i < 2; i++)
            {
                packet.ReadInt32("Prematch", i, idx);
                packet.ReadInt32("Postmatch", i, idx);
                packet.ReadInt32("PrematchMMR", i, idx);
            }
        }

        [Parser(Opcode.SMSG_PVP_LOG_DATA)]
        [Parser(Opcode.SMSG_PVP_MATCH_STATISTICS)]
        public static void HandlePvPLogData(Packet packet)
        {
            var hasRatings = packet.ReadBit("HasRatings");
            var hasWinner = false;
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V8_2_0_30898))
                hasWinner = packet.ReadBit("HasWinner");

            var statisticsCount = packet.ReadUInt32();
            for (int i = 0; i < 2; i++)
                packet.ReadByte("PlayerCount", i);

            if (hasRatings)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_0_30898))
                    ReadRatingData820(packet, "Ratings");
                else
                    V6_0_2_19033.Parsers.BattlegroundHandler.ReadRatingData(packet, "Ratings");
            }

            if (hasWinner)
                packet.ReadByte("Winner");

            for (int i = 0; i < statisticsCount; i++)
                ReadPvPMatchPlayerStatistics(packet, "Statistics", i);
        }

        [Parser(Opcode.SMSG_REQUEST_SCHEDULED_PVP_INFO_RESPONSE, ClientVersionBuild.V8_2_0_30898)]
        public static void HandleRequestScheduledPVPInfoResponse(Packet packet)
        {
            packet.ReadInt32("PvpBrawlID");
            packet.ReadInt32("TimeToBrawl");
            packet.ResetBitReader();
            packet.ReadBit("Active");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_SKIRMISH, ClientVersionBuild.V8_1_0_28724)]
        public static void HandleBattlemasterJoinSkirmish(Packet packet)
        {
            packet.ReadByteE<LfgRoleFlag>("Roles");
            packet.ReadByte("Bracket");
            packet.ResetBitReader();
            packet.ReadBit("IsRequeue");
        }

        [Parser(Opcode.SMSG_UPDATE_CAPTURE_POINT)]
        public static void HandleUpdateCapturePoint(Packet packet)
        {
            V6_0_2_19033.Parsers.BattlegroundHandler.ReadBattlegroundCapturePointInfo(packet, "CapturePointInfo");
        }

        [Parser(Opcode.SMSG_CAPTURE_POINT_REMOVED)]
        public static void HandleCapturePointRemoved(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectiveGuid");
        }

        [Parser(Opcode.SMSG_RATED_PVP_INFO)]
        public static void HandleRatedPvPInfo(Packet packet)
        {
            packet.ReadInt32("PersonalRating");
            packet.ReadInt32("Ranking");
            packet.ReadInt32("SeasonPlayed");
            packet.ReadInt32("SeasonWon");
            packet.ReadInt32("Unused1");
            packet.ReadInt32("Unused2");
            packet.ReadInt32("WeeklyPlayed");
            packet.ReadInt32("WeeklyWon");
            packet.ReadInt32("BestWeeklyRating");
            packet.ReadInt32("LastWeeksBestRating");
            packet.ReadInt32("BestSeasonRating");
            packet.ReadInt32("PvpTierID");
            packet.ReadInt32("Unused3");
            packet.ReadInt32("WeeklyBestWinPvpTierID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
            {
                packet.ReadInt32("Unused4");
                packet.ReadInt32("Rank");
            }
            packet.ReadBit("Disqualified");
        }
    }
}
