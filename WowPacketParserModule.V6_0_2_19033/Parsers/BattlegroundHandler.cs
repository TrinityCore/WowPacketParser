using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class BattlegroundHandler
    {
        public static void ReadBattlefieldStatus_Header(Packet packet, params object[] indexes)
        {
            LfgHandler.ReadCliRideTicket(packet);

            packet.Translator.ReadInt64("QueueID");
            packet.Translator.ReadByte("RangeMin");
            packet.Translator.ReadByte("RangeMax");
            packet.Translator.ReadByte("TeamSize");
            packet.Translator.ReadInt32("InstanceID");

            packet.Translator.ResetBitReader();
            packet.Translator.ReadBit("RegisteredMatch");
            packet.Translator.ReadBit("TournamentRules");
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
                packet.Translator.ReadInt32("PersonalRating", i);
                packet.Translator.ReadInt32("Ranking", i);
                packet.Translator.ReadInt32("SeasonPlayed", i);
                packet.Translator.ReadInt32("SeasonWon", i);
                packet.Translator.ReadInt32("WeeklyPlayed", i);
                packet.Translator.ReadInt32("WeeklyWon", i);
                packet.Translator.ReadInt32("BestWeeklyRating", i);
                packet.Translator.ReadInt32("BestSeasonRating", i);
            }
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_LIST)]
        public static void HandleBattlefieldListClient(Packet packet)
        {
            packet.Translator.ReadInt32<BgId>("ListID");
        }

        [Parser(Opcode.CMSG_BF_MGR_ENTRY_INVITE_RESPONSE)]
        [Parser(Opcode.CMSG_BF_MGR_QUEUE_INVITE_RESPONSE)]
        public static void HandleBattlefieldMgrEntryOrQueueInviteResponse(Packet packet)
        {
            packet.Translator.ReadInt64("QueueID");
            packet.Translator.ReadBit("AcceptedInvite");
        }

        [Parser(Opcode.CMSG_BF_MGR_QUEUE_EXIT_REQUEST)]
        public static void HandleBattlefieldMgrExitRequest(Packet packet)
        {
            packet.Translator.ReadInt64("QueueID");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_PORT)]
        public static void HandleBattlefieldPort(Packet packet)
        {
            LfgHandler.ReadCliRideTicket(packet);
            packet.Translator.ReadBit("AcceptedInvite");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN)]
        public static void HandleBattlemasterJoin(Packet packet)
        {
            packet.Translator.ReadInt64("QueueID");
            packet.Translator.ReadByte("Roles");

            for (int i = 0; i < 2; i++)
                packet.Translator.ReadInt32("BlacklistMap", i);

            packet.Translator.ReadBit("JoinAsGroup");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_ARENA)]
        public static void HandleBattlemasterJoinArena(Packet packet)
        {
            packet.Translator.ReadByte("TeamSizeIndex");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_ARENA_SKIRMISH)]
        public static void HandleBattlemasterJoinArenaSkirmish(Packet packet)
        {
            packet.Translator.ReadByte("Bracket");
            packet.Translator.ReadByteE<LfgRoleFlag>("Roles");
            packet.Translator.ReadBit("JoinAsGroup");
        }

        public static void ReadRatingData(Packet packet, params object[] idx)
        {
            for (int i = 0; i < 2; i++)
                packet.Translator.ReadInt32("Prematch", i, idx);

            for (int i = 0; i < 2; i++)
                packet.Translator.ReadInt32("Postmatch", i, idx);

            for (int i = 0; i < 2; i++)
                packet.Translator.ReadInt32("PrematchMMR", i, idx);
        }

        public static void ReadHonorData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadUInt32("HonorKills", idx);
            packet.Translator.ReadUInt32("Deaths", idx);
            packet.Translator.ReadUInt32("ContributionPoints", idx);
        }

        public static void ReadPlayerData(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("PlayerGUID", idx);
            packet.Translator.ReadUInt32("Kills", idx);
            packet.Translator.ReadUInt32("DamageDone", idx);
            packet.Translator.ReadUInt32("HealingDone", idx);
            var statsCount = packet.Translator.ReadUInt32("StatsCount", idx);
            packet.Translator.ReadUInt32("PrimaryTalentTree", idx);
            packet.Translator.ReadUInt32("PrimaryTalentTreeNameIndex", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_2_20444))
                packet.Translator.ReadUInt32E<Race>("Race", idx);

            for (int j = 0; j < statsCount; j++)
                packet.Translator.ReadUInt32("Stats", j, idx);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Faction", idx);
            packet.Translator.ReadBit("IsInWorld", idx);

            var hasHonor = packet.Translator.ReadBit("HasHonor", idx);
            var hasPreMatchRating = packet.Translator.ReadBit("HasPreMatchRating", idx);
            var hasRatingChange = packet.Translator.ReadBit("HasRatingChange", idx);
            var hasPreMatchMMR = packet.Translator.ReadBit("HasPreMatchMMR", idx);
            var hasMmrChange = packet.Translator.ReadBit("HasMmrChange", idx);

            packet.Translator.ResetBitReader();

            if (hasHonor)
                ReadHonorData(packet, "Honor");

            if (hasPreMatchRating)
                packet.Translator.ReadUInt32("PreMatchRating", idx);

            if (hasRatingChange)
                packet.Translator.ReadUInt32("RatingChange", idx);

            if (hasPreMatchMMR)
                packet.Translator.ReadUInt32("PreMatchMMR", idx);

            if (hasMmrChange)
                packet.Translator.ReadUInt32("MmrChange", idx);
        }

        [Parser(Opcode.SMSG_PVP_LOG_DATA)]
        public static void HandlePvPLogData(Packet packet)
        {
            var hasRatings = packet.Translator.ReadBit("HasRatings");
            var hasWinner = packet.Translator.ReadBit("HasWinner");

            var playersCount = packet.Translator.ReadUInt32("PlayersCount");

            for (int i = 0; i < 2; i++)
                packet.Translator.ReadByte("PlayerCount", i);

            if (hasRatings)
                ReadRatingData(packet, "Ratings");

            if (hasWinner)
                packet.Translator.ReadByte("Winner");

            for (int i = 0; i < playersCount; i++)
                ReadPlayerData(packet, "Players", i);
        }

        [Parser(Opcode.CMSG_AREA_SPIRIT_HEALER_QUERY)]
        [Parser(Opcode.CMSG_AREA_SPIRIT_HEALER_QUEUE)]
        public static void HandleAreaSpiritHealer(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("HealerGuid");
        }

        [Parser(Opcode.SMSG_AREA_SPIRIT_HEALER_TIME)]
        public static void HandleAreaSpiritHealerTime(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("HealerGuid");
            packet.Translator.ReadUInt32("TimeLeft");
        }

        [Parser(Opcode.SMSG_REPORT_PVP_AFK_RESULT)]
        public static void HandleReportPvPPlayerAFKResult(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Offender");

            packet.Translator.ReadByte("NumPlayersIHaveReported");
            packet.Translator.ReadByte("NumBlackMarksOnOffender");
            packet.Translator.ReadByte("Result");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST)]
        public static void HandleBattlefieldList(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("BattlemasterGuid");
            packet.Translator.ReadInt32("BattlemasterListID");
            packet.Translator.ReadByte("MaxLevel");
            packet.Translator.ReadByte("MinLevel");
            var battlefieldsCount = packet.Translator.ReadInt32("BattlefieldsCount");
            for (var i = 0; i < battlefieldsCount; ++i) // Battlefields
                packet.Translator.ReadInt32("Battlefield");

            packet.Translator.ReadBit("PvpAnywhere");
            packet.Translator.ReadBit("HasHolidayWinToday");
            packet.Translator.ReadBit("HasRandomWinToday");
            packet.Translator.ReadBit("IsRandomBG");
        }

        [Parser(Opcode.SMSG_PVP_OPTIONS_ENABLED)]
        public static void HandlePVPOptionsEnabled(Packet packet)
        {
            packet.Translator.ReadBit("RatedArenas");
            packet.Translator.ReadBit("ArenaSkirmish");
            packet.Translator.ReadBit("PugBattlegrounds");
            packet.Translator.ReadBit("WargameBattlegrounds");
            packet.Translator.ReadBit("WargameArenas");
            packet.Translator.ReadBit("RatedBattlegrounds");
        }

        [Parser(Opcode.SMSG_REQUEST_PVP_REWARDS_RESPONSE, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleRequestPVPRewardsResponse60x(Packet packet)
        {
            packet.Translator.ReadUInt32("RewardPointsThisWeek");
            packet.Translator.ReadUInt32("MaxRewardPointsThisWeek");

            packet.Translator.ReadUInt32("RatedRewardPointsThisWeek");
            packet.Translator.ReadUInt32("RatedMaxRewardPointsThisWeek");

            packet.Translator.ReadUInt32("RandomRewardPointsThisWeek");
            packet.Translator.ReadUInt32("RandomMaxRewardPointsThisWeek");

            packet.Translator.ReadUInt32("ArenaRewardPointsThisWeek");
            packet.Translator.ReadUInt32("ArenaMaxRewardPointsThisWeek");

            packet.Translator.ReadUInt32("ArenaRewardPoints");
            packet.Translator.ReadUInt32("RatedRewardPoints");
        }

        [Parser(Opcode.SMSG_REQUEST_PVP_REWARDS_RESPONSE, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleRequestPVPRewardsResponse61x(Packet packet)
        {
            packet.Translator.ReadUInt32("RewardPointsThisWeek");
            packet.Translator.ReadUInt32("MaxRewardPointsThisWeek");

            packet.Translator.ReadUInt32("RatedRewardPointsThisWeek");
            packet.Translator.ReadUInt32("RatedMaxRewardPointsThisWeek");

            packet.Translator.ReadUInt32("RandomRewardPointsThisWeek");
            packet.Translator.ReadUInt32("RandomMaxRewardPointsThisWeek");

            packet.Translator.ReadUInt32("ArenaRewardPointsThisWeek");
            packet.Translator.ReadUInt32("ArenaMaxRewardPointsThisWeek");

            packet.Translator.ReadUInt32("ArenaRewardPoints");
            packet.Translator.ReadUInt32("RatedRewardPoints");

            for (int i = 0; i < 2; i++)
                LfgHandler.ReadShortageReward(packet, i, "ShortageReward");

        }

        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_POSITIONS)]
        public static void HandleBattlegroundPlayerPositions(Packet packet)
        {
            var battlegroundPlayerPositionCount = packet.Translator.ReadInt32("BattlegroundPlayerPositionCount");
            for (int i = 0; i < battlegroundPlayerPositionCount; i++)
            {
                packet.Translator.ReadPackedGuid128("Guid", i);
                packet.Translator.ReadVector2("Pos", i);
                packet.Translator.ReadByte("IconID", i);
                packet.Translator.ReadByte("ArenaSlot", i);
            }
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_QUEUED)]
        public static void HandleBattlefieldStatus_Queued(Packet packet)
        {
            ReadBattlefieldStatus_Header(packet);
            packet.Translator.ReadInt32("AverageWaitTime");
            packet.Translator.ReadInt32("WaitTime");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("AsGroup");                   // unconfirmed order
            packet.Translator.ReadBit("SuspendedQueue");            // unconfirmed order
            packet.Translator.ReadBit("EligibleForMatchmaking");    // unconfirmed order
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

            packet.Translator.ReadInt32<MapId>("Mapid");
            packet.Translator.ReadInt32("StartTimer");
            packet.Translator.ReadInt32("ShutdownTimer");

            packet.Translator.ResetBitReader();
            packet.Translator.ReadBit("ArenaFaction");     // unconfirmed order
            packet.Translator.ReadBit("LeftEarly");        // unconfirmed order
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_NEED_CONFIRMATION)]
        public static void HandleBattlefieldStatus_NeedConfirmation(Packet packet)
        {
            ReadBattlefieldStatus_Header(packet);
            packet.Translator.ReadInt32<MapId>("Mapid");
            packet.Translator.ReadInt32("Timeout");
            packet.Translator.ReadByte("Role");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_FAILED)]
        public static void HandleBattlefieldStatus_Failed(Packet packet)
        {
            LfgHandler.ReadCliRideTicket(packet);
            packet.Translator.ReadInt64("QueueID");
            packet.Translator.ReadInt32("Reason");
            packet.Translator.ReadPackedGuid128("ClientID");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_JOINED)]
        [Parser(Opcode.SMSG_BATTLEGROUND_PLAYER_LEFT)]
        public static void HandleBattlegroundPlayerJoined(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_POINTS)]
        public static void HandleBattlegroundPoints(Packet packet)
        {
            packet.Translator.ReadInt16("Points");
            packet.Translator.ReadBit("Team");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_INIT)]
        public static void HandleBattlegroundInit(Packet packet)
        {
            packet.Translator.ReadInt16("MaxPoints");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_EJECTED)]
        public static void HandleBFMgrEjected(Packet packet)
        {
            packet.Translator.ReadInt64("QueueID");
            packet.Translator.ReadByte("BattleState");
            packet.Translator.ReadByte("Reason");
            packet.Translator.ReadBit("Relocated");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_REQUEST_RESPONSE)]
        public static void HandleBFMgrQueueRequestResponse(Packet packet)
        {
            packet.Translator.ReadInt64("QueueID");
            packet.Translator.ReadInt32("AreaID");
            packet.Translator.ReadSByte("BattleState");
            packet.Translator.ReadPackedGuid128("FailedPlayerGUID");
            packet.Translator.ReadSByte("Result");
            packet.Translator.ReadBit("LoggingIn");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_INVITE)]
        public static void HandleBFMgrQueueInvite(Packet packet)
        {
            packet.Translator.ReadInt64("QueueID");
            packet.Translator.ReadByte("BattleState");

            packet.Translator.ReadInt32("Timeout");        // unconfirmed order
            packet.Translator.ReadInt32("MinLevel");       // unconfirmed order
            packet.Translator.ReadInt32("MaxLevel");       // unconfirmed order
            packet.Translator.ReadInt32("InstanceID");     // unconfirmed order
            packet.Translator.ReadInt32<MapId>("MapID");          // unconfirmed order

            packet.Translator.ResetBitReader();
            packet.Translator.ReadBit("Index");
        }

        public static void ReadBattlegroundCapturePointInfo(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("Guid", idx);
            packet.Translator.ReadVector2("Pos", idx);
            var state = packet.Translator.ReadByte("State", idx);

            if (state == 2 || state == 3)
            {
                packet.Translator.ReadUInt32("CaptureTime", idx);
                packet.Translator.ReadUInt32("CaptureTotalDuration", idx);
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
            var count = packet.Translator.ReadInt32("CapturePointInfoCount");
            for (var i = 0; i < count; ++i)
                ReadBattlegroundCapturePointInfo(packet, "CapturePointInfo", i);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_EJECT_PENDING)]
        public static void HandleBFMgrEjectPending(Packet packet)
        {
            packet.Translator.ReadUInt64("QueueID");
            packet.Translator.ReadBit("Remove");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTERING)]
        public static void HandleBFMgrEntering(Packet packet)
        {
            packet.Translator.ReadBit("ClearedAFK");

            packet.Translator.ReadBit("OnOffense | Relocated"); // NC
            packet.Translator.ReadBit("OnOffense | Relocated"); // NC

            packet.Translator.ResetBitReader();

            packet.Translator.ReadUInt64("QueueID");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTRY_INVITE)]
        public static void HandleBFMgrEntryInvite(Packet packet)
        {
            packet.Translator.ReadUInt64("QueueID");
            packet.Translator.ReadInt32<AreaId>("AreaID");
            packet.Translator.ReadTime("ExpireTime");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_STATE_CHANGED)]
        public static void HandleBFMgrStateChanged(Packet packet)
        {
            packet.Translator.ReadUInt64("QueueID");
            packet.Translator.ReadInt32("State");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_STATUS_WAIT_FOR_GROUPS)]
        public static void HandleBattlefieldStatus_WaitForGroups(Packet packet)
        {
            ReadBattlefieldStatus_Header(packet, "Hdr");

            packet.Translator.ReadUInt32<MapId>("Mapid");
            packet.Translator.ReadUInt32("Timeout");

            for (var i = 0; i < 2; ++i)
            {
                packet.Translator.ReadByte("TotalPlayers", i);
                packet.Translator.ReadByte("AwaitingPlayers", i);
            }
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_DROP_TIMER_STARTED)]
        public static void HandleBFMgrDropTimerStarted(Packet packet)
        {
            packet.Translator.ReadUInt64("QueueID");
            packet.Translator.ReadInt32("Time");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_DROP_TIMER_CANCELED)]
        public static void HandleBFMgrDropTimerCanceled(Packet packet)
        {
            packet.Translator.ReadUInt64("QueueID");
        }
    }
}
