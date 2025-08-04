using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class BattlegroundHandler
    {
        public static void ReadPackedBattlegroundQueueTypeID(Packet packet, params object[] indexes)
        {
            var packedQueueId = packet.ReadUInt64();
            var battlemasterListId = packedQueueId & 0xFFFF;
            var type = (packedQueueId >> 16) & 0xF;
            var isRated = (packedQueueId >> 20) & 1;
            var teamSize = (packedQueueId >> 24) & 0x3F;
            packet.AddValue("PackedBattlegroundQueueTypeID", $"0x{packedQueueId:X} | BattlemasterListId={battlemasterListId} Type={type} ({(BattlegroundQueueIdType)type}) IsRated={isRated} TeamSize={teamSize}", indexes);
        }

        public static void ReadBattlefieldStatus_Header(Packet packet, params object[] indexes)
        {
            LfgHandler.ReadCliRideTicket(packet, indexes);

            var queueIdCount = packet.ReadUInt32();
            packet.ReadByte("RangeMin", indexes);
            packet.ReadByte("RangeMax", indexes);
            packet.ReadByte("TeamSize", indexes);
            packet.ReadInt32("InstanceID", indexes);
            for (var i = 0u; i < queueIdCount; ++i)
                ReadPackedBattlegroundQueueTypeID(packet, indexes);

            packet.ResetBitReader();
            packet.ReadBit("RegisteredMatch", indexes);
            packet.ReadBit("TournamentRules", indexes);
        }

        public static void ReadBattlegroundCapturePointInfo(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("Guid", idx);
            packet.ReadVector2("Pos", idx);
            var state = packet.ReadByte("State", idx);

            if (state == 2 || state == 3)
            {
                packet.ReadTime64("CaptureTime", idx);
                packet.ReadUInt32("CaptureTotalDuration", idx);
            }
        }

        public static void ReadRatedPvpBracketInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32("PersonalRating", idx);
            packet.ReadInt32("Ranking", idx);
            packet.ReadInt32("SeasonPlayed", idx);
            packet.ReadInt32("SeasonWon", idx);
            packet.ReadInt32("SeasonFactionPlayed", idx);
            packet.ReadInt32("SeasonFactionWon", idx);
            packet.ReadInt32("WeeklyPlayed", idx);
            packet.ReadInt32("WeeklyWon", idx);
            packet.ReadInt32("RoundsSeasonPlayed", idx);
            packet.ReadInt32("RoundsSeasonWon", idx);
            packet.ReadInt32("RoundsWeeklyPlayed", idx);
            packet.ReadInt32("RoundsWeeklyWon", idx);
            packet.ReadInt32("BestWeeklyRating", idx);
            packet.ReadInt32("LastWeeksBestRating", idx);
            packet.ReadInt32("BestSeasonRating", idx);
            packet.ReadInt32("PvpTierID", idx);
            packet.ReadInt32("SeasonPvpTier", idx);
            packet.ReadInt32("BestWeeklyPvpTier", idx);
            packet.ReadByte("BestSeasonPvpTierEnum", idx);
            packet.ResetBitReader();
            packet.ReadBit("Disqualified", idx);
        }

        public static void ReadRatingData(Packet packet, params object[] idx)
        {
            for (int i = 0; i < 2; i++)
            {
                packet.ReadInt32("Prematch", i, idx);
                packet.ReadInt32("Postmatch", i, idx);
                packet.ReadInt32("PrematchMMR", i, idx);
            }
        }

        public static void ReadPvPStat(Packet packet, params object[] idx)
        {
            packet.ReadInt32("PvpStatID", idx);
            packet.ReadInt32("PvpStatValue", idx);
        }

        public static void ReadHonorData(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("HonorKills", idx);
            packet.ReadUInt32("Deaths", idx);
            packet.ReadUInt32("ContributionPoints", idx);
        }

        public static void ReadPvPMatchPlayerStatistics(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("PlayerGUID", idx);
            packet.ReadUInt32("Kills", idx);
            packet.ReadInt32("Faction", idx);
            packet.ReadUInt32("DamageDone", idx);
            packet.ReadUInt32("HealingDone", idx);
            var statsCount = packet.ReadUInt32("StatsCount", idx);
            packet.ReadInt32("PrimaryTalentTree", idx);
            packet.ReadByteE<Gender>("Sex", idx);
            packet.ReadByteE<Race>("Race", idx);
            packet.ReadByteE<Class>("Class", idx);
            packet.ReadInt32("CreatureID", idx);
            packet.ReadInt32("HonorLevel", idx);
            packet.ReadInt32("Role", idx);

            for (int i = 0; i < statsCount; i++)
                ReadPvPStat(packet, "PvPStat", i, idx);

            packet.ResetBitReader();
            packet.ReadBit("IsInWorld", idx);
            var hasHonor = packet.ReadBit("HasHonor", idx);
            var hasPreMatchRating = packet.ReadBit("HasPreMatchRating", idx);
            var hasRatingChange = packet.ReadBit("HasRatingChange", idx);
            var hasPreMatchMMR = packet.ReadBit("HasPreMatchMMR", idx);
            var hasMmrChange = packet.ReadBit("HasMmrChange", idx);
            var hasPostMatchMMR = packet.ReadBit("HasPostMatchMMR", idx);

            packet.ResetBitReader();

            if (hasHonor)
                ReadHonorData(packet, "Honor", idx);

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

        public static void ReadBrawlInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32("PvpBrawlId");
            packet.ReadInt32("Time");
            packet.ResetBitReader();
            packet.ReadBit("Started");
        }

        public static void ReadSpecialEventInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32("PvpBrawlID");
            packet.ReadInt32<AchievementId>("AchievementId");
            packet.ResetBitReader();
            packet.ReadBit("CanQueue");
        }

        public static void ReadRatedMatchDeserterPenalty(Packet packet, params object[] idx)
        {
            packet.ReadInt32("PersonalRatingChange");
            packet.ReadInt32<SpellId>("QueuePenaltySpellID");
            packet.ReadInt32("QueuePenaltyDuration");
        }

        [Parser(Opcode.SMSG_REPORT_PVP_PLAYER_AFK_RESULT)]
        public static void HandleReportPvPPlayerAfkResult(Packet packet)
        {
            packet.ReadPackedGuid128("Offender");
            packet.ReadByteE<ReportPvPAFKResult>("Result");
            packet.ReadByte("NumBlackMarksOnOffender");
            packet.ReadByte("NumPlayersIHaveReported");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_NEED_CONFIRMATION)]
        public static void HandleBattlefieldStatus_NeedConfirmation(Packet packet)
        {
            ReadBattlefieldStatus_Header(packet);
            packet.ReadInt32<MapId>("Mapid");
            packet.ReadInt32("Timeout");
            packet.ReadByte("Role");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_ACTIVE)]
        public static void HandleBattlefieldStatus_Active(Packet packet)
        {
            ReadBattlefieldStatus_Header(packet);

            packet.ReadInt32<MapId>("Mapid");
            packet.ReadInt32("ShutdownTimer");
            packet.ReadInt32("StartTimer");
            packet.ReadByte("ArenaFaction");

            packet.ResetBitReader();
            packet.ReadBit("LeftEarly");
            packet.ReadBit("IsInBrawl");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_QUEUED)]
        public static void HandleBattlefieldStatus_Queued(Packet packet)
        {
            ReadBattlefieldStatus_Header(packet);
            packet.ReadInt32("AverageWaitTime");
            packet.ReadInt32("WaitTime");
            packet.ReadInt32("Unused920");

            packet.ResetBitReader();

            packet.ReadBit("AsGroup");
            packet.ReadBit("EligibleForMatchmaking");
            packet.ReadBit("SuspendedQueue");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_NONE)]
        public static void HandleBattlefieldStatus_None(Packet packet)
        {
            LfgHandler.ReadCliRideTicket(packet);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_FAILED)]
        public static void HandleBattlefieldStatus_Failed(Packet packet)
        {
            LfgHandler.ReadCliRideTicket(packet);
            ReadPackedBattlegroundQueueTypeID(packet);
            packet.ReadInt32("Reason");
            packet.ReadPackedGuid128("ClientID");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST)]
        public static void HandleBattlefieldList(Packet packet)
        {
            packet.ReadPackedGuid128("BattlemasterGuid");
            packet.ReadInt32("CurrentMaxInstanceIndex"); // serverside check?
            packet.ReadInt32("BattlemasterListID");
            packet.ReadByte("MinLevel");
            packet.ReadByte("MaxLevel");
            var battlefieldsCount = packet.ReadUInt32("BattlefieldsCount");
            for (var i = 0; i < battlefieldsCount; ++i)
                packet.ReadInt32("Battlefield");

            packet.ResetBitReader();
            packet.ReadBit("PvpAnywhere");
            packet.ReadBit("HasRandomWinToday");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_POSITIONS)]
        public static void HandleBattlegroundPlayerPositions(Packet packet)
        {
            var battlegroundPlayerPositionCount = packet.ReadInt32("BattlegroundPlayerPositionCount");
            for (int i = 0; i < battlegroundPlayerPositionCount; i++)
            {
                packet.ReadPackedGuid128("Guid", i);
                packet.ReadVector2("Pos", i);
                packet.ReadByte("IconID", i);
                packet.ReadByte("ArenaSlot", i);
            }
        }

        [Parser(Opcode.SMSG_UPDATE_CAPTURE_POINT)]
        public static void HandleUpdateCapturePoint(Packet packet)
        {
            ReadBattlegroundCapturePointInfo(packet, "CapturePointInfo");
        }

        [Parser(Opcode.SMSG_CAPTURE_POINT_REMOVED)]
        public static void HandleCapturePointRemoved(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectiveGuid");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED)]
        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_LEFT)]
        public static void HandleBattlegroundPlayerJoined(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_RATED_PVP_INFO)]
        public static void HandleRatedPvPInfo(Packet packet)
        {
            for (int i = 0; i < 9; i++)
                ReadRatedPvpBracketInfo(packet, i, "Bracket");
        }

        [Parser(Opcode.SMSG_PVP_MATCH_STATISTICS)]
        public static void HandlePvpMatchStatistics(Packet packet)
        {
            var hasRatings = packet.ReadBit("HasRatings");
            var statisticsCount = packet.ReadUInt32();
            for (int i = 0; i < 2; i++)
                packet.ReadByte("PlayerCount", i);

            if (hasRatings)
                ReadRatingData(packet, "Ratings");

            for (int i = 0; i < statisticsCount; i++)
                ReadPvPMatchPlayerStatistics(packet, "Statistics", i);
        }

        [Parser(Opcode.SMSG_PVP_LOG_DATA)]
        public static void HandlePvPLogData(Packet packet)
        {
            var hasRatings = packet.ReadBit("HasRatings");
            var hasNames = packet.ReadBit("HasNames");
            var hasWinner = packet.ReadBit("HasWinner");

            if (hasNames)
            {
                uint[] stringLength = new uint[2];
                for (int i = 0; i < 2; i++)
                    stringLength[i] = packet.ReadBits("StringLength", 7, i);

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
                packet.ReadUInt32("Winner");

            for (int i = 0; i < statisticsCount; i++)
            {
                // Different than ReadPvPMatchPlayerStatistics
                packet.ReadPackedGuid128("PlayerGUID", i);
                packet.ReadUInt32("Kills", i);
                packet.ReadUInt32("DamageDone", i);
                packet.ReadUInt32("HealingDone", i);
                var statsCount = packet.ReadUInt32("StatsCount", i);
                packet.ReadInt32("PrimaryTalentTree", i);
                packet.ReadByteE<Gender>("Sex", i);
                packet.ReadByteE<Race>("Race", i);
                packet.ReadByteE<Class>("Class", i);
                packet.ReadInt32("CreatureID", i);
                packet.ReadInt32("HonorLevel", i);
                packet.ReadInt32("Role", i);

                for (int j = 0; j < statsCount; j++)
                    packet.ReadUInt32("Stat", i, j);

                packet.ResetBitReader();
                packet.ReadBit("Faction", i);
                packet.ReadBit("IsInWorld", i);
                var hasHonor = packet.ReadBit("HasHonor", i);
                var hasPreMatchRating = packet.ReadBit("HasPreMatchRating", i);
                var hasRatingChange = packet.ReadBit("HasRatingChange", i);
                var hasPreMatchMMR = packet.ReadBit("HasPreMatchMMR", i);
                var hasMmrChange = packet.ReadBit("HasMmrChange", i);

                packet.ResetBitReader();

                if (hasHonor)
                    ReadHonorData(packet, "Honor", i);

                if (hasPreMatchRating)
                    packet.ReadUInt32("PreMatchRating", i);

                if (hasRatingChange)
                    packet.ReadInt32("RatingChange", i);

                if (hasPreMatchMMR)
                    packet.ReadUInt32("PreMatchMMR", i);

                if (hasMmrChange)
                    packet.ReadInt32("MmrChange", i);
            }
        }

        [Parser(Opcode.SMSG_PVP_OPTIONS_ENABLED)]
        public static void HandlePVPOptionsEnabled(Packet packet)
        {
            packet.ReadBit("RatedBattlegrounds");
            packet.ReadBit("PugBattlegrounds");
            packet.ReadBit("WargameBattlegrounds");
            packet.ReadBit("WargameArenas");
            packet.ReadBit("RatedArenas");
            packet.ReadBit("ArenaSkirmish");
            packet.ReadBit("SoloShuffle");
            packet.ReadBit("RatedSoloShuffle");

            packet.ReadBit("BattlegroundBlitz");
            packet.ReadBit("RatedBattlegroundBlitz");
        }

        [Parser(Opcode.SMSG_REQUEST_PVP_REWARDS_RESPONSE)]
        public static void HandleRequestPVPRewardsResponse(Packet packet)
        {
            LfgHandler.ReadLfgPlayerQuestReward(packet, "FirstRandomBGWinRewards");
            LfgHandler.ReadLfgPlayerQuestReward(packet, "FirstRandomBGLossRewards");
            LfgHandler.ReadLfgPlayerQuestReward(packet, "NthRandomBGWinRewards");
            LfgHandler.ReadLfgPlayerQuestReward(packet, "NthRandomBGLossRewards");
            LfgHandler.ReadLfgPlayerQuestReward(packet, "RatedBGRewards");
            LfgHandler.ReadLfgPlayerQuestReward(packet, "Arena2v2Rewards");
            LfgHandler.ReadLfgPlayerQuestReward(packet, "Arena3v3Rewards");
            LfgHandler.ReadLfgPlayerQuestReward(packet, "Arena5v5Rewards");
        }

        [Parser(Opcode.SMSG_REQUEST_SCHEDULED_PVP_INFO_RESPONSE)]
        public static void HandleRequestScheduledPvpInfoResponse(Packet packet)
        {
            var hasBrawlInfo = packet.ReadBit("HasBrawlInfo");
            var hasSpecialEventInfo = packet.ReadBit("HasSpecialEventInfo");

            if (hasBrawlInfo)
                ReadBrawlInfo(packet, "BrawlInfo");

            if (hasSpecialEventInfo)
                ReadSpecialEventInfo(packet, "SpecialEventInfo");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_POINTS)]
        public static void HandleBattlegroundPoints(Packet packet)
        {
            packet.ReadInt16("Points");
            packet.ReadBit("Team");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_INIT)]
        public static void HandleBattlegroundInit(Packet packet)
        {
            packet.ReadUInt32("ServerTime");
            packet.ReadInt16("MaxPoints");
        }

        [Parser(Opcode.SMSG_MAP_OBJECTIVES_INIT)]
        public static void HandleMapObjectivesInit(Packet packet)
        {
            var count = packet.ReadInt32("CapturePointInfoCount");
            for (var i = 0; i < count; ++i)
                ReadBattlegroundCapturePointInfo(packet, "CapturePointInfo", i);
        }

        [Parser(Opcode.SMSG_PVP_MATCH_SET_STATE)]
        public static void HandlePvpMatchSetState(Packet packet)
        {
            packet.ReadByteE<PVPMatchState>("State");
        }

        [Parser(Opcode.SMSG_PVP_MATCH_INITIALIZE)]
        public static void HandlePvpMatchInitialize(Packet packet)
        {
            packet.ReadUInt32<MapId>("MapID");
            packet.ReadByteE<MatchState>("State");
            packet.ReadInt64("StartTime");
            packet.ReadInt64("Duration");
            packet.ReadByte("ArenaFaction");
            packet.ReadUInt32("BattlemasterListID");

            packet.ResetBitReader();
            packet.ReadBit("Registered");
            packet.ReadBit("AffectsRating");

            var hasDeserterPenalty = packet.ReadBit("HasRatedMatchDeserterPenalty");
            if (hasDeserterPenalty)
                ReadRatedMatchDeserterPenalty(packet, "RatedMatchDeserterPenalty");
        }

        [Parser(Opcode.SMSG_SEASON_INFO)]
        public static void HandleSeasonInfo(Packet packet)
        {
            packet.ReadInt32("MythicPlusDisplaySeasonID");
            packet.ReadInt32("MythicPlusMilestoneSeasonID");
            packet.ReadInt32("CurrentArenaSeason");
            packet.ReadInt32("PreviousArenaSeason");
            packet.ReadInt32("ConquestWeeklyProgressCurrencyID");
            packet.ReadInt32("PvpSeasonID");
            packet.ReadInt32("Unknown1027_1");

            packet.ReadBit("WeeklyRewardChestsEnabled");
            packet.ReadBit("CurrentArenaSeasonUsesTeams");
            packet.ReadBit("PreviousArenaSeasonUsesTeams");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_PORT_DENIED)]
        [Parser(Opcode.SMSG_BATTLEGROUND_INFO_THROTTLED)]
        public static void HandleBattlegroundZero(Packet packet)
        {
        }
    }
}
