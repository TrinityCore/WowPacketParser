using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
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

            var queueIdCount = 0u;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_5_31921))
                queueIdCount = packet.ReadUInt32();
            else
                ReadPackedBattlegroundQueueTypeID(packet, indexes);

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

        [Parser(Opcode.CMSG_REQUEST_BATTLEFIELD_STATUS)]
        [Parser(Opcode.CMSG_REQUEST_CONQUEST_FORMULA_CONSTANTS)]
        [Parser(Opcode.CMSG_REQUEST_RATED_BATTLEFIELD_INFO)]
        public static void HandleBattlefieldZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_RATED_BATTLEFIELD_INFO)]
        public static void HandleRatedBattlefieldInfo(Packet packet)
        {
            for (int i = 0; i < 6; i++)
            {
                packet.ReadInt32("PersonalRating", i);
                packet.ReadInt32("Ranking", i);
                packet.ReadInt32("SeasonPlayed", i);
                packet.ReadInt32("SeasonWon", i);
                packet.ReadInt32("WeeklyPlayed", i);
                packet.ReadInt32("WeeklyWon", i);
                packet.ReadInt32("BestWeeklyRating", i);
                packet.ReadInt32("BestSeasonRating", i);
            }
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_LIST)]
        public static void HandleBattlefieldListClient(Packet packet)
        {
            packet.ReadInt32<BgId>("ListID");
        }

        [Parser(Opcode.CMSG_BF_MGR_ENTRY_INVITE_RESPONSE)]
        [Parser(Opcode.CMSG_BF_MGR_QUEUE_INVITE_RESPONSE)]
        public static void HandleBattlefieldMgrEntryOrQueueInviteResponse(Packet packet)
        {
            ReadPackedBattlegroundQueueTypeID(packet);
            packet.ReadBit("AcceptedInvite");
        }

        [Parser(Opcode.CMSG_BF_MGR_QUEUE_EXIT_REQUEST)]
        public static void HandleBattlefieldMgrExitRequest(Packet packet)
        {
            ReadPackedBattlegroundQueueTypeID(packet);
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_PORT)]
        public static void HandleBattlefieldPort(Packet packet)
        {
            LfgHandler.ReadCliRideTicket(packet);
            packet.ResetBitReader();
            packet.ReadBit("AcceptedInvite");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN)]
        public static void HandleBattlemasterJoin(Packet packet)
        {
            ReadPackedBattlegroundQueueTypeID(packet);
            packet.ReadByte("Roles");

            for (int i = 0; i < 2; i++)
                packet.ReadInt32("BlacklistMap", i);

            packet.ReadBit("JoinAsGroup");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_ARENA)]
        public static void HandleBattlemasterJoinArena(Packet packet)
        {
            packet.ReadByte("TeamSizeIndex");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_ARENA_SKIRMISH)]
        public static void HandleBattlemasterJoinArenaSkirmish(Packet packet)
        {
            packet.ReadByte("Bracket");
            packet.ReadByteE<LfgRoleFlag>("Roles");
            packet.ReadBit("JoinAsGroup");
        }

        public static void ReadRatingData(Packet packet, params object[] idx)
        {
            for (int i = 0; i < 2; i++)
                packet.ReadInt32("Prematch", i, idx);

            for (int i = 0; i < 2; i++)
                packet.ReadInt32("Postmatch", i, idx);

            for (int i = 0; i < 2; i++)
                packet.ReadInt32("PrematchMMR", i, idx);
        }

        public static void ReadHonorData(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("HonorKills", idx);
            packet.ReadUInt32("Deaths", idx);
            packet.ReadUInt32("ContributionPoints", idx);
        }

        public static void ReadPlayerData(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("PlayerGUID", idx);
            packet.ReadUInt32("Kills", idx);
            packet.ReadUInt32("DamageDone", idx);
            packet.ReadUInt32("HealingDone", idx);
            var statsCount = packet.ReadUInt32("StatsCount", idx);
            packet.ReadUInt32("PrimaryTalentTree", idx);
            packet.ReadUInt32("PrimaryTalentTreeNameIndex", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_2_20444))
                packet.ReadUInt32E<Race>("Race", idx);

            for (int j = 0; j < statsCount; j++)
                packet.ReadUInt32("Stats", j, idx);

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
                packet.ReadUInt32("RatingChange", idx);

            if (hasPreMatchMMR)
                packet.ReadUInt32("PreMatchMMR", idx);

            if (hasMmrChange)
                packet.ReadUInt32("MmrChange", idx);
        }

        [Parser(Opcode.SMSG_PVP_LOG_DATA)]
        public static void HandlePvPLogData(Packet packet)
        {
            var hasRatings = packet.ReadBit("HasRatings");
            var hasWinner = packet.ReadBit("HasWinner");

            var playersCount = packet.ReadUInt32("PlayersCount");

            for (int i = 0; i < 2; i++)
                packet.ReadByte("PlayerCount", i);

            if (hasRatings)
                ReadRatingData(packet, "Ratings");

            if (hasWinner)
                packet.ReadByte("Winner");

            for (int i = 0; i < playersCount; i++)
                ReadPlayerData(packet, "Players", i);
        }

        [Parser(Opcode.CMSG_AREA_SPIRIT_HEALER_QUERY)]
        [Parser(Opcode.CMSG_AREA_SPIRIT_HEALER_QUEUE)]
        public static void HandleAreaSpiritHealer(Packet packet)
        {
            packet.ReadPackedGuid128("HealerGuid");
        }

        [Parser(Opcode.SMSG_AREA_SPIRIT_HEALER_TIME)]
        public static void HandleAreaSpiritHealerTime(Packet packet)
        {
            packet.ReadPackedGuid128("HealerGuid");
            packet.ReadUInt32("TimeLeft");
        }

        [Parser(Opcode.SMSG_REPORT_PVP_AFK_RESULT)]
        public static void HandleReportPvPPlayerAFKResult(Packet packet)
        {
            packet.ReadPackedGuid128("Offender");

            packet.ReadByte("NumPlayersIHaveReported");
            packet.ReadByte("NumBlackMarksOnOffender");
            packet.ReadByteE<ReportPvPAFKResult>("Result");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST)]
        public static void HandleBattlefieldList(Packet packet)
        {
            packet.ReadPackedGuid128("BattlemasterGuid");
            packet.ReadInt32("BattlemasterListID");
            packet.ReadByte("MaxLevel");
            packet.ReadByte("MinLevel");
            var battlefieldsCount = packet.ReadInt32("BattlefieldsCount");
            for (var i = 0; i < battlefieldsCount; ++i) // Battlefields
                packet.ReadInt32("Battlefield");

            packet.ReadBit("PvpAnywhere");
            packet.ReadBit("HasHolidayWinToday");
            packet.ReadBit("HasRandomWinToday");
            packet.ReadBit("IsRandomBG");
        }

        [Parser(Opcode.SMSG_PVP_OPTIONS_ENABLED)]
        public static void HandlePVPOptionsEnabled(Packet packet)
        {
            packet.ReadBit("RatedArenas");
            packet.ReadBit("ArenaSkirmish");
            packet.ReadBit("PugBattlegrounds");
            packet.ReadBit("WargameBattlegrounds");
            packet.ReadBit("WargameArenas");
            packet.ReadBit("RatedBattlegrounds");
        }

        [Parser(Opcode.SMSG_REQUEST_PVP_REWARDS_RESPONSE, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleRequestPVPRewardsResponse60x(Packet packet)
        {
            packet.ReadUInt32("RewardPointsThisWeek");
            packet.ReadUInt32("MaxRewardPointsThisWeek");

            packet.ReadUInt32("RatedRewardPointsThisWeek");
            packet.ReadUInt32("RatedMaxRewardPointsThisWeek");

            packet.ReadUInt32("RandomRewardPointsThisWeek");
            packet.ReadUInt32("RandomMaxRewardPointsThisWeek");

            packet.ReadUInt32("ArenaRewardPointsThisWeek");
            packet.ReadUInt32("ArenaMaxRewardPointsThisWeek");

            packet.ReadUInt32("ArenaRewardPoints");
            packet.ReadUInt32("RatedRewardPoints");
        }

        [Parser(Opcode.SMSG_REQUEST_PVP_REWARDS_RESPONSE, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleRequestPVPRewardsResponse61x(Packet packet)
        {
            packet.ReadUInt32("RewardPointsThisWeek");
            packet.ReadUInt32("MaxRewardPointsThisWeek");

            packet.ReadUInt32("RatedRewardPointsThisWeek");
            packet.ReadUInt32("RatedMaxRewardPointsThisWeek");

            packet.ReadUInt32("RandomRewardPointsThisWeek");
            packet.ReadUInt32("RandomMaxRewardPointsThisWeek");

            packet.ReadUInt32("ArenaRewardPointsThisWeek");
            packet.ReadUInt32("ArenaMaxRewardPointsThisWeek");

            packet.ReadUInt32("ArenaRewardPoints");
            packet.ReadUInt32("RatedRewardPoints");

            for (int i = 0; i < 2; i++)
                LfgHandler.ReadLfgPlayerQuestReward(packet, i, "ShortageReward");
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

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_QUEUED)]
        public static void HandleBattlefieldStatus_Queued(Packet packet)
        {
            ReadBattlefieldStatus_Header(packet);
            packet.ReadInt32("AverageWaitTime");
            packet.ReadInt32("WaitTime");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
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

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_ACTIVE)]
        public static void HandleBattlefieldStatus_Active(Packet packet)
        {
            ReadBattlefieldStatus_Header(packet);

            packet.ReadInt32<MapId>("Mapid");
            packet.ReadInt32("ShutdownTimer");
            packet.ReadInt32("StartTimer");

            packet.ResetBitReader();
            packet.ReadBit("ArenaFaction");     // unconfirmed order
            packet.ReadBit("LeftEarly");        // unconfirmed order
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_NEED_CONFIRMATION)]
        public static void HandleBattlefieldStatus_NeedConfirmation(Packet packet)
        {
            ReadBattlefieldStatus_Header(packet);
            packet.ReadInt32<MapId>("Mapid");
            packet.ReadInt32("Timeout");
            packet.ReadByte("Role");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_FAILED)]
        public static void HandleBattlefieldStatus_Failed(Packet packet)
        {
            LfgHandler.ReadCliRideTicket(packet);
            ReadPackedBattlegroundQueueTypeID(packet);
            packet.ReadInt32("Reason");
            packet.ReadPackedGuid128("ClientID");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED)]
        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_LEFT)]
        public static void HandleBattlegroundPlayerJoined(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
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
            packet.ReadInt16("MaxPoints");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_EJECTED)]
        public static void HandleBFMgrEjected(Packet packet)
        {
            ReadPackedBattlegroundQueueTypeID(packet);
            packet.ReadByte("BattleState");
            packet.ReadByte("Reason");
            packet.ReadBit("Relocated");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_REQUEST_RESPONSE)]
        public static void HandleBFMgrQueueRequestResponse(Packet packet)
        {
            ReadPackedBattlegroundQueueTypeID(packet);
            packet.ReadInt32<AreaId>("AreaID");
            packet.ReadSByte("BattleState");
            packet.ReadPackedGuid128("FailedPlayerGUID");
            packet.ReadSByte("Result");
            packet.ReadBit("LoggingIn");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_INVITE)]
        public static void HandleBFMgrQueueInvite(Packet packet)
        {
            ReadPackedBattlegroundQueueTypeID(packet);
            packet.ReadByte("BattleState");

            packet.ReadInt32("Timeout");        // unconfirmed order
            packet.ReadInt32("MinLevel");       // unconfirmed order
            packet.ReadInt32("MaxLevel");       // unconfirmed order
            packet.ReadInt32("InstanceID");     // unconfirmed order
            packet.ReadInt32<MapId>("MapID");          // unconfirmed order

            packet.ResetBitReader();
            packet.ReadBit("Index");
        }

        public static void ReadBattlegroundCapturePointInfo(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("Guid", idx);
            packet.ReadVector2("Pos", idx);
            var state = packet.ReadByte("State", idx);

            if (state == 2 || state == 3)
            {
                packet.ReadUInt32("CaptureTime", idx);
                packet.ReadUInt32("CaptureTotalDuration", idx);
            }
        }

        [Parser(Opcode.SMSG_UPDATE_CAPTURE_POINT)]
        public static void HandleUpdateCapturePoint(Packet packet)
        {
            ReadBattlegroundCapturePointInfo(packet, "CapturePointInfo");
        }

        [Parser(Opcode.SMSG_MAP_OBJECTIVES_INIT)]
        public static void HandleMapObjectivesInit(Packet packet)
        {
            var count = packet.ReadInt32("CapturePointInfoCount");
            for (var i = 0; i < count; ++i)
                ReadBattlegroundCapturePointInfo(packet, "CapturePointInfo", i);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_EJECT_PENDING)]
        public static void HandleBFMgrEjectPending(Packet packet)
        {
            ReadPackedBattlegroundQueueTypeID(packet);
            packet.ReadBit("Remove");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTERING)]
        public static void HandleBFMgrEntering(Packet packet)
        {
            packet.ReadBit("ClearedAFK");

            packet.ReadBit("OnOffense | Relocated"); // NC
            packet.ReadBit("OnOffense | Relocated"); // NC

            packet.ResetBitReader();

            ReadPackedBattlegroundQueueTypeID(packet);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTRY_INVITE)]
        public static void HandleBFMgrEntryInvite(Packet packet)
        {
            ReadPackedBattlegroundQueueTypeID(packet);
            packet.ReadInt32<AreaId>("AreaID");
            packet.ReadTime("ExpireTime");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_STATE_CHANGED)]
        public static void HandleBFMgrStateChanged(Packet packet)
        {
            ReadPackedBattlegroundQueueTypeID(packet);
            packet.ReadInt32("State");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_WAIT_FOR_GROUPS)]
        public static void HandleBattlefieldStatus_WaitForGroups(Packet packet)
        {
            ReadBattlefieldStatus_Header(packet, "Hdr");

            packet.ReadUInt32<MapId>("Mapid");
            packet.ReadUInt32("Timeout");

            for (var i = 0; i < 2; ++i)
            {
                packet.ReadByte("TotalPlayers", i);
                packet.ReadByte("AwaitingPlayers", i);
            }
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_DROP_TIMER_STARTED)]
        public static void HandleBFMgrDropTimerStarted(Packet packet)
        {
            ReadPackedBattlegroundQueueTypeID(packet);
            packet.ReadInt32("Time");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_DROP_TIMER_CANCELED)]
        public static void HandleBFMgrDropTimerCanceled(Packet packet)
        {
            ReadPackedBattlegroundQueueTypeID(packet);
        }
    }
}
