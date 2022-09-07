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

        [Parser(Opcode.SMSG_RATED_PVP_INFO)]
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
    }
}
