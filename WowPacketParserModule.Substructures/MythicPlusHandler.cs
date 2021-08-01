using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParserModule.Substructures
{
    public static class MythicPlusHandler
    {
        public static void ReadDungeonScoreMapSummary(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("ChallengeModeID", indexes);
            packet.ReadInt32("MapScore", indexes);
            packet.ReadInt32("BestRunLevel", indexes);
            packet.ReadInt32("BestRunDurationMS", indexes);

            packet.ResetBitReader();
            packet.ReadBit("FinishedSuccess", indexes);
        }

        public static void ReadDungeonScoreSummary(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("CurrentSeasonScore", indexes);
            var runCount = packet.ReadUInt32("RunCount", indexes);
            for (var i = 0u; i < runCount; ++i)
                ReadDungeonScoreMapSummary(packet, indexes, i, "Run");
        }

        public static void ReadMythicPlusMember(Packet packet, params object[] indexes)
        {
            packet.ReadPackedGuid128("BnetAccountGUID", indexes);
            packet.ReadUInt64("GuildClubMemberID", indexes);
            packet.ReadPackedGuid128("GUID", indexes);
            packet.ReadPackedGuid128("GuildGUID", indexes);
            packet.ReadUInt32("NativeRealmAddress", indexes);
            packet.ReadUInt32("VirtualRealmAddress", indexes);
            packet.ReadInt16("ChrSpecializationID", indexes);
            packet.ReadInt16E<Race>("RaceID", indexes);
            packet.ReadInt32("ItemLevel", indexes);
            packet.ReadInt32("CovenantID", indexes);
            packet.ReadInt32("SoulbindID", indexes);
        }

        public static void ReadMythicPlusRun(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("MapChallengeModeID", indexes);
            packet.ReadUInt32("Level", indexes);
            packet.ReadInt32("DurationMs", indexes);
            packet.ReadTime64("StartDate", indexes);
            packet.ReadTime64("CompletionDate", indexes);
            packet.ReadInt32("Season", indexes);
            for (var i = 0; i < 4; ++i)
                packet.ReadInt32("KeystoneAffixIDs", indexes, i);

            var memberCount = packet.ReadUInt32("MemberCount", indexes);
            packet.ReadInt32("RunScore", indexes);
            for (var i = 0u; i < memberCount; ++i)
                ReadMythicPlusMember(packet, indexes, i, "Member");

            packet.ResetBitReader();
            packet.ReadBit("Completed", indexes);
        }

        public static void ReadDungeonScoreBestRunForAffix(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("KeystoneAffixID", indexes);
            packet.ReadInt32("Score", indexes);
            ReadMythicPlusRun(packet, indexes, "Run");
        }

        public static void ReadDungeonScoreMapData(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("MapChallengeModeID", indexes);
            var runCount = packet.ReadUInt32("BestRunCount", indexes);
            packet.ReadInt32("OverAllScore", indexes);
            for (var i = 0u; i < runCount; ++i)
                ReadDungeonScoreBestRunForAffix(packet, indexes, i, "BestRun");
        }

        public static void ReadDungeonScoreSeasonData(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("Season", indexes);
            var runCount = packet.ReadUInt32("MapCount", indexes);
            packet.ReadInt32("SeasonScore", indexes);
            for (var i = 0u; i < runCount; ++i)
                ReadDungeonScoreMapData(packet, indexes, i, "Map");
        }

        public static void ReadDungeonScoreData(Packet packet, params object[] indexes)
        {
            var seasonCount = packet.ReadUInt32("SeasonCount", indexes);
            packet.ReadInt32("TotalRuns", indexes);
            for (var i = 0u; i < seasonCount; ++i)
                ReadDungeonScoreSeasonData(packet, indexes, i, "Season");
        }
    }
}
