using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class BattlegroundHandler
    {
        [Parser(Opcode.CMSG_REQUEST_BATTLEFIELD_STATUS)]
        [Parser(Opcode.CMSG_REQUEST_CONQUEST_FORMULA_CONSTANTS)]
        [Parser(Opcode.CMSG_REQUEST_RATED_BATTLEFIELD_INFO)]
        public static void HandleBattlefieldZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_BATTLEFIELD_RATED_INFO)]
        public static void HandleBattlefieldRatedInfo(Packet packet)
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
            packet.ReadEntry<Int32>(StoreNameType.Battleground, "ListID");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_ENTRY_INVITE_RESPONSE)]
        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_QUEUE_INVITE_RESPONSE)]
        public static void HandleBattlefieldMgrEntryOrQueueInviteResponse(Packet packet)
        {
            packet.ReadInt64("QueueID");
            packet.ReadBit("AcceptedInvite");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_MGR_EXIT_REQUEST)]
        public static void HandleBattlefieldMgrExitRequest(Packet packet)
        {
            packet.ReadInt64("QueueID");
        }

        [Parser(Opcode.CMSG_BATTLEFIELD_PORT)]
        public static void HandleBattlefieldPort(Packet packet)
        {
            packet.ReadPackedGuid128("RequesterGuid");

            packet.ReadUInt32("Id");
            packet.ReadEntry<Int32>(StoreNameType.Battleground, "Type");
            packet.ReadTime("Time");

            packet.ReadBit("AcceptedInvite");
        }

        [Parser(Opcode.CMSG_BATTLEMASTER_JOIN)]
        public static void HandleBattlemasterJoin(Packet packet)
        {
            packet.ReadInt64("QueueID");
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

        [Parser(Opcode.SMSG_PVP_LOG_DATA)]
        public static void HandlePvPLogData434(Packet packet)
        {
            var bit44 = packet.ReadBit("HasRatings");
            var bit17 = packet.ReadBit("HasWinner");

            var int48 = packet.ReadUInt32("PlayerCount");

            for (int i = 0; i < 2; i++)
                packet.ReadByte("PlayerCount", i);

            if (bit44)
            {
                for (int i = 0; i < 2; i++)
                    packet.ReadInt32("Prematch", i);

                for (int i = 0; i < 2; i++)
                    packet.ReadInt32("Postmatch", i);

                for (int i = 0; i < 2; i++)
                    packet.ReadInt32("PrematchMMR", i);
            }

            if (bit17)
                packet.ReadByte("Winner");

            for (int i = 0; i < int48; i++)
            {
                packet.ReadPackedGuid128("PlayerGUID", i);
                packet.ReadUInt32("Kills", i);
                packet.ReadUInt32("DamageDone", i);
                packet.ReadUInt32("HealingDone", i);
                var int80 = packet.ReadUInt32("StatsCount", i);
                packet.ReadUInt32("PrimaryTalentTree", i);
                packet.ReadUInt32("Unk1", i);

                for (int j = 0; j < int80; j++)
                    packet.ReadUInt32("Stats", i, j);

                packet.ResetBitReader();

                packet.ReadBit("Faction", i);
                packet.ReadBit("IsInWorld", i);

                var bit36 = packet.ReadBit("HasHonor", i);
                var bit52 = packet.ReadBit("HasPreMatchRating", i);
                var bit60 = packet.ReadBit("HasRatingChange", i);
                var bit68 = packet.ReadBit("HasPreMatchMMR", i);
                var bit76 = packet.ReadBit("HasMmrChange", i);

                if (bit36)
                {
                    packet.ReadUInt32("HonorKills", i);
                    packet.ReadUInt32("Deaths", i);
                    packet.ReadUInt32("ContributionPoints", i);
                }

                if (bit52)
                    packet.ReadUInt32("PreMatchRating", i);

                if (bit60)
                    packet.ReadUInt32("RatingChange", i);

                if (bit68)
                    packet.ReadUInt32("PreMatchMMR", i);

                if (bit76)
                    packet.ReadUInt32("MmrChange", i);
            }
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
            packet.ReadByte("Result");
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

        [Parser(Opcode.SMSG_PVP_OPTIONS_ENABLED)]
        public static void HandleRequestPVPRewardsResponse(Packet packet)
        {
            packet.ReadUInt32("RandomRewardPointsThisWeek");
            packet.ReadUInt32("ArenaRewardPointsThisWeek");
            packet.ReadUInt32("MaxRewardPointsThisWeek");
            packet.ReadUInt32("RatedMaxRewardPointsThisWeek");
            packet.ReadUInt32("ArenaRewardPoints");
            packet.ReadUInt32("RandomMaxRewardPointsThisWeek");
            packet.ReadUInt32("ArenaMaxRewardPointsThisWeek");
            packet.ReadUInt32("RatedRewardPoints");
            packet.ReadUInt32("RewardPointsThisWeek");
            packet.ReadUInt32("RatedRewardPointsThisWeek");
        }
    }
}
