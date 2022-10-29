using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParserModule.Substructures
{
    public static class MythicPlusHandler
    {
        public static void ReadDungeonScoreMapSummary(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("ChallengeModeID", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
                packet.ReadSingle("MapScore", indexes);
            else
                packet.ReadInt32("MapScore", indexes);

            packet.ReadInt32("BestRunLevel", indexes);
            packet.ReadInt32("BestRunDurationMS", indexes);

            packet.ResetBitReader();
            packet.ReadBit("FinishedSuccess", indexes);
        }

        public static void ReadDungeonScoreSummary(Packet packet, params object[] indexes)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
                packet.ReadSingle("CurrentSeasonScore", indexes);
            else
                packet.ReadInt32("CurrentSeasonScore", indexes);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
                packet.ReadSingle("LifetimeBestSeasonScore", indexes);

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
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
                packet.ReadInt32("ChrSpecializationID", indexes);
            else
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
                packet.ReadUInt32("KeystoneAffixIDs", indexes, i);

            var memberCount = packet.ReadUInt32("MemberCount", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
                packet.ReadSingle("RunScore", indexes);
            else
                packet.ReadInt32("RunScore", indexes);

            for (var i = 0u; i < memberCount; ++i)
                ReadMythicPlusMember(packet, indexes, i, "Member");

            packet.ResetBitReader();
            packet.ReadBit("Completed", indexes);
        }

        public static void ReadDungeonScoreBestRunForAffix(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("KeystoneAffixID", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
                packet.ReadSingle("Score", indexes);
            else
                packet.ReadInt32("Score", indexes);

            ReadMythicPlusRun(packet, indexes, "Run");
        }

        public static void ReadDungeonScoreMapData(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("MapChallengeModeID", indexes);
            var runCount = packet.ReadUInt32("BestRunCount", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
                packet.ReadSingle("OverAllScore", indexes);
            else
                packet.ReadInt32("OverAllScore", indexes);

            for (var i = 0u; i < runCount; ++i)
                ReadDungeonScoreBestRunForAffix(packet, indexes, i, "BestRun");
        }

        public static void ReadDungeonScoreSeasonData(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("Season", indexes);
            var runCount = packet.ReadUInt32("MapCount", indexes);
            var runCount2 = 0u;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
                runCount2 = packet.ReadUInt32("Unknown920_MapCount", indexes);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
                packet.ReadSingle("SeasonScore", indexes);
            else
                packet.ReadInt32("SeasonScore", indexes);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
                packet.ReadSingle("Unknown920_1", indexes);

            for (var i = 0u; i < runCount; ++i)
                ReadDungeonScoreMapData(packet, indexes, i, "Map");

            for (var i = 0u; i < runCount2; ++i)
                ReadDungeonScoreMapData(packet, indexes, i, "Unknown920_Map");
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
