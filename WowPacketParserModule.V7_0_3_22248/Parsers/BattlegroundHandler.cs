using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.CMSG_REQUEST_RATED_PVP_INFO)]
        [Parser(Opcode.CMSG_REQUEST_SCHEDULED_PVP_INFO)]
        public static void HandleBattlegroundZero(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_ARENA)]
        public static void HandleBattlemasterJoinArena(Packet packet)
        {
            packet.ReadByte("TeamSizeIndex");
            packet.ReadByteE<LfgRoleFlag>("Roles");
        }

        public static void ReadPlayerData(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("PlayerGUID", idx);
            packet.ReadUInt32("Kills", idx);
            packet.ReadUInt32("DamageDone", idx);
            packet.ReadUInt32("HealingDone", idx);
            var statsCount = packet.ReadUInt32("StatsCount", idx);
            packet.ReadInt32("PrimaryTalentTree", idx);
            packet.ReadInt32("PrimaryTalentTreeNameIndex", idx);
            packet.ReadInt32E<Race>("Race", idx);
            packet.ReadUInt32("Prestige", idx);

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
                V6_0_2_19033.Parsers.BattlegroundHandler.ReadRatingData(packet, "Ratings");

            if (hasWinner)
                packet.ReadByte("Winner");

            for (int i = 0; i < playersCount; i++)
                ReadPlayerData(packet, "Players", i);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_INVITE)]
        public static void HandleBFMgrQueueInvite(Packet packet)
        {
            V6_0_2_19033.Parsers.BattlegroundHandler.ReadPackedBattlegroundQueueTypeID(packet);
            packet.ReadByte("BattleState");

            packet.ReadInt32("Timeout");
            packet.ReadInt32("MinLevel");
            packet.ReadInt32("MaxLevel");
            packet.ReadInt32<MapId>("MapID");
            packet.ReadInt32("InstanceID");

            packet.ResetBitReader();
            packet.ReadBit("Index");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_QUEUE_REQUEST_RESPONSE)]
        public static void HandleBFMgrQueueRequestResponse(Packet packet)
        {
            V6_0_2_19033.Parsers.BattlegroundHandler.ReadPackedBattlegroundQueueTypeID(packet);
            packet.ReadInt32<AreaId>("AreaID");
            packet.ReadSByte("Result");
            packet.ReadPackedGuid128("FailedPlayerGUID");
            packet.ReadSByte("BattleState");
            packet.ResetBitReader();
            packet.ReadBit("LoggingIn");
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_MGR_ENTERING)]
        public static void HandleBFMgrEntering(Packet packet)
        {
            packet.ReadBit("ClearedAFK");
            packet.ReadBit("Relocated");
            packet.ReadBit("OnOffense");
            V6_0_2_19033.Parsers.BattlegroundHandler.ReadPackedBattlegroundQueueTypeID(packet);
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_LIST)]
        public static void HandleBattlefieldList(Packet packet)
        {
            packet.ReadPackedGuid128("BattlemasterGuid");
            packet.ReadInt32("BattlemasterListID");
            packet.ReadByte("MinLevel");
            packet.ReadByte("MaxLevel");
            var battlefieldsCount = packet.ReadUInt32("BattlefieldsCount");
            for (var i = 0; i < battlefieldsCount; ++i) // Battlefields
                packet.ReadInt32("Battlefield");

            packet.ResetBitReader();
            packet.ReadBit("PvpAnywhere");
            packet.ReadBit("HasRandomWinToday");
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

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_2_0_52038))
            {
                packet.ReadBit("SoloShuffle");
                packet.ReadBit("RatedSoloShuffle");
                packet.ReadBit("BattlegroundBlitz");
                packet.ReadBit("RatedBattlegroundBlitz");
            }
        }

        [Parser(Opcode.SMSG_REPORT_PVP_PLAYER_AFK_RESULT)]
        public static void HandleReportPvPPlayerAfkResult(Packet packet)
        {
            packet.ReadPackedGuid128("Offender");
            packet.ReadByteE<ReportPvPAFKResult>("Result");
            packet.ReadByte("NumBlackMarksOnOffender");
            packet.ReadByte("NumPlayersIHaveReported");
        }

        [Parser(Opcode.SMSG_REQUEST_PVP_REWARDS_RESPONSE)]
        public static void HandleRequestPVPRewardsResponse(Packet packet)
        {
            V6_0_2_19033.Parsers.LfgHandler.ReadLfgPlayerQuestReward(packet, "RandomBGRewards");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_1_0_22900))
            {
                packet.ResetBitReader();
                packet.ReadBit("HasWonRatedBg10v10");
                packet.ReadBit("HasWonArenaSkirmish");
                packet.ReadBit("HasWonArena2v2");
                packet.ReadBit("HasWonArena3v3");
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_0_23706))
                {
                    packet.ReadBit("HasWonBrawlBG");
                    packet.ReadBit("HasWonBrawlArena");
                }
            }
            V6_0_2_19033.Parsers.LfgHandler.ReadLfgPlayerQuestReward(packet, "RatedBGRewards");
            V6_0_2_19033.Parsers.LfgHandler.ReadLfgPlayerQuestReward(packet, "ArenaSkirmishRewards");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_1_0_22900))
            {
                V6_0_2_19033.Parsers.LfgHandler.ReadLfgPlayerQuestReward(packet, "Arena2v2Rewards");
                V6_0_2_19033.Parsers.LfgHandler.ReadLfgPlayerQuestReward(packet, "Arena3v3Rewards");
            }
            else
                V6_0_2_19033.Parsers.LfgHandler.ReadLfgPlayerQuestReward(packet, "ArenaRewards");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_0_23706))
            {
                V6_0_2_19033.Parsers.LfgHandler.ReadLfgPlayerQuestReward(packet, "BrawlBGRewards");
                V6_0_2_19033.Parsers.LfgHandler.ReadLfgPlayerQuestReward(packet, "BrawlArenaRewards");
            }
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_0_1_27101))
                V6_0_2_19033.Parsers.LfgHandler.ReadLfgPlayerQuestReward(packet, "EpicBGRewards");
        }

        public static void ReadRatedPvpBracketInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32("PersonalRating", idx);
            packet.ReadInt32("Ranking", idx);
            packet.ReadInt32("SeasonPlayed", idx);
            packet.ReadInt32("SeasonWon", idx);
            packet.ReadInt32("Unused1", idx); // equal to SeasonPlayed
            packet.ReadInt32("Unused2", idx); // equal to SeasonWon
            packet.ReadInt32("WeeklyPlayed", idx);
            packet.ReadInt32("WeeklyWon", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
            {
                packet.ReadInt32("RoundsSeasonPlayed", idx);
                packet.ReadInt32("RoundsSeasonWon", idx);
                packet.ReadInt32("RoundsWeeklyPlayed", idx);
                packet.ReadInt32("RoundsWeeklyWon", idx);
            }
            packet.ReadInt32("BestWeeklyRating", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_1_0_22900))
                packet.ReadInt32("LastWeeksBestRating", idx);

            packet.ReadInt32("BestSeasonRating", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_0_1_27101))
            {
                packet.ReadInt32("PvpTier", idx);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_3_7_35249))
                {
                    packet.ReadInt32("Unused3", idx);
                    if (ClientVersion.RemovedInVersion(ClientVersionBuild.V10_0_2_46479))
                        packet.ReadInt32("WeeklyBestWinPvpTierID", idx);
                }
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
                {
                    packet.ReadInt32("Unused4", idx);
                    packet.ReadInt32("Rank", idx);
                }
                packet.ResetBitReader();
                packet.ReadBit("Disqualified", idx);
            }
        }

        [Parser(Opcode.SMSG_RATED_BATTLEFIELD_INFO)]
        [Parser(Opcode.SMSG_RATED_PVP_INFO)]
        public static void HandleRatedBattlefieldInfo(Packet packet)
        {
            var bracketNum = 6;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_0_46181))
                bracketNum = 7;

            for (int i = 0; i < bracketNum; i++)
                ReadRatedPvpBracketInfo(packet, i);
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_BRAWL)]
        public static void HandleBattlemasterBrawl(Packet packet)
        {
            packet.ReadByteE<LfgRoleFlag>("Roles");
        }

        [Parser(Opcode.SMSG_REQUEST_SCHEDULED_PVP_INFO_RESPONSE)]
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

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN_SKIRMISH)]
        public static void HandleBattlemasterJoinSkirmish(Packet packet)
        {
            packet.ResetBitReader();
            packet.ReadBit("JoinAsGroup");
            packet.ReadBit("IsRequeue");
            packet.ReadByteE<LfgRoleFlag>("Roles");
            packet.ReadByte("Bracket");
        }

        [Parser(Opcode.CMSG_REQUEST_CROWD_CONTROL_SPELL)]
        public static void HandleRequestCrowdControlSpell(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGuid");
        }

        [Parser(Opcode.SMSG_ARENA_CROWD_CONTROL_SPELL_RESULT)]
        public static void HandleArenaCrowdControlSpellResult(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGuid");
            packet.ReadInt32("CrowdControlSpellID");
        }

        [Parser(Opcode.SMSG_BATTLEGROUND_INIT)]
        public static void HandleBattlegroundInit(Packet packet)
        {
            packet.ReadInt32("ServerTime");
            packet.ReadInt16("MaxPoints");
        }
    }
}
