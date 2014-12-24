using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class BattlegroundHandler
    {

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
    }
}
