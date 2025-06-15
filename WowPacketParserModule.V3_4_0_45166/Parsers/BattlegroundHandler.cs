using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class BattlegroundHandler
    {
        public static void ReadHonorData(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("HonorKills", idx);
            packet.ReadUInt32("Deaths", idx);
            packet.ReadUInt32("ContributionPoints", idx);
        }

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

        public static void ReadRatedPvpBracketInfo344(Packet packet, params object[] idx)
        {
            packet.ReadInt32("PersonalRating", idx);
            packet.ReadInt32("Ranking", idx);
            packet.ReadInt32("SeasonPlayed", idx);
            packet.ReadInt32("SeasonWon", idx);
            packet.ReadInt32("Unused1", idx);
            packet.ReadInt32("Unused2", idx);
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
            packet.ReadInt32("Unused3", idx);
            packet.ReadInt32("Unused4", idx);
            packet.ReadInt32("Rank", idx);
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
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_4_59817))
            {
                packet.ReadByteE<Race>("Race", idx);
                packet.ReadByteE<Class>("Class", idx);
            }
            else
            {
                packet.ReadInt32E<Race>("Race", idx);
                packet.ReadInt32E<Class>("Class", idx);
            }
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
                ReadHonorData(packet, "Honor");

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
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_4_59817))
                packet.ReadBit("JoinAsGroup");
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
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_4_4_59817))
                    packet.ReadUInt32("Winner");
                else
                    packet.ReadByte("Winner");
            }

            for (int i = 0; i < statisticsCount; i++)
                ReadPvPMatchPlayerStatistics(packet, "Statistics", i);
        }

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

        public static void ReadRatedMatchDeserterPenalty(Packet packet, params object[] idx)
        {
            packet.ReadInt32("PersonalRatingChange");
            packet.ReadInt32<SpellId>("QueuePenaltySpellID");
            packet.ReadInt32("QueuePenaltyDuration");
        }

        [Parser(Opcode.SMSG_AREA_SPIRIT_HEALER_TIME, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAreaSpiritHealerTime(Packet packet)
        {
            packet.ReadPackedGuid128("HealerGuid");
            packet.ReadUInt32("TimeLeft");
        }

        [Parser(Opcode.CMSG_ARENA_TEAM_ROSTER, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleArenaTeamQuery(Packet packet)
        {
            packet.ReadUInt32("TeamID");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_ACTIVE, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_FAILED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlefieldStatus_Failed(Packet packet)
        {
            LfgHandler.ReadCliRideTicket(packet);
            ReadPackedBattlegroundQueueTypeID(packet);
            packet.ReadInt32("Reason");
            packet.ReadPackedGuid128("ClientID");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_NEED_CONFIRMATION, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlefieldStatus_NeedConfirmation(Packet packet)
        {
            ReadBattlefieldStatus_Header(packet);
            packet.ReadInt32<MapId>("Mapid");
            packet.ReadInt32("Timeout");
            packet.ReadByte("Role");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_NONE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlefieldStatus_None(Packet packet)
        {
            LfgHandler.ReadCliRideTicket(packet);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_QUEUED, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_LEFT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlegroundPlayerJoined(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_POSITIONS, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.CMSG_REQUEST_BATTLEFIELD_STATUS, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_REQUEST_RATED_PVP_INFO, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_REQUEST_PVP_REWARDS, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.SMSG_BATTLEFIELD_PORT_DENIED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlegroundZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_REQUEST_PVP_REWARDS_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_PVP_MATCH_INITIALIZE, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_PVP_OPTIONS_ENABLED, ClientVersionBuild.V3_4_4_59817)]
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

        [Parser(Opcode.SMSG_RATED_PVP_INFO, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRatedPvPInfo(Packet packet)
        {
            for (int i = 0; i < 9; i++)
                ReadRatedPvpBracketInfo344(packet, i, "Bracket");
        }

        [Parser(Opcode.SMSG_REPORT_PVP_PLAYER_AFK_RESULT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleReportPvPPlayerAfkResult(Packet packet)
        {
            packet.ReadPackedGuid128("Offender");
            packet.ReadByteE<ReportPvPAFKResult>("Result");
            packet.ReadByte("NumBlackMarksOnOffender");
            packet.ReadByte("NumPlayersIHaveReported");
        }

        [Parser(Opcode.CMSG_AREA_SPIRIT_HEALER_QUERY, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_AREA_SPIRIT_HEALER_QUEUE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleAreaSpiritHealer(Packet packet)
        {
            packet.ReadPackedGuid128("HealerGuid");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_LIST, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlefieldListClient(Packet packet)
        {
            packet.ReadInt32<BgId>("ListID");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_PORT, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlefieldPort(Packet packet)
        {
            LfgHandler.ReadCliRideTicket(packet);
            packet.ResetBitReader();
            packet.ReadBit("AcceptedInvite");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_HELLO, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlemasterHello(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
        }


        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlemasterJoin(Packet packet)
        {
            ReadPackedBattlegroundQueueTypeID(packet);
            packet.ReadByte("Roles");

            for (int i = 0; i < 2; i++)
                packet.ReadInt32("BlacklistMap", i);

            packet.ReadPackedGuid128("BattlemasterGuid");
            packet.ReadInt32("UnkID");
            packet.ReadInt32("BattlefieldIndexSpecific");
            packet.ReadBit("JoinAsGroup");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_ARENA, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlemasterJoinArena(Packet packet)
        {
            packet.ReadPackedGuid128("BattlemasterGuid");
            packet.ReadByte("TeamSizeIndex");
            packet.ReadByteE<LfgRoleFlag>("Roles");
        }

        [Parser(Opcode.CMSG_REPORT_PVP_PLAYER_AFK, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleReportPvPPlayerAfk(Packet packet)
        {
            packet.ReadGuid("Offender");
        }

        [Parser(Opcode.CMSG_JOIN_RATED_BATTLEGROUND, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleJoinRatedBattleground(Packet packet)
        {
            packet.ReadByteE<LfgRoleFlag>("Roles");
        }

        [Parser(Opcode.SMSG_REQUEST_SCHEDULED_PVP_INFO_RESPONSE, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleRequestScheduledPVPInfoResponse(Packet packet)
        {
            packet.ReadInt32("TimeToBrawl");
            packet.ReadInt32("BattlegroundID");
            packet.ReadInt32("LFGDungeonID");
            packet.ResetBitReader();
            packet.ReadBit("Active");
            var titleLen = packet.ReadBits(9);
            var typeLen = packet.ReadBits(10);
            var objectiveLen = packet.ReadBits(14);
            packet.ReadWoWString("Title", titleLen);
            packet.ReadWoWString("Type", typeLen);
            packet.ReadWoWString("Objective", objectiveLen);
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_LEAVE, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_GET_PVP_OPTIONS_ENABLED, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_HEARTH_AND_RESURRECT, ClientVersionBuild.V3_4_4_59817)]
        [Parser(Opcode.CMSG_PVP_LOG_DATA, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleBattlegroundNull(Packet packet)
        {
        }
    }
}
