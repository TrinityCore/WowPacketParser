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
    }
}
