using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class MythicPlusHandler
    {
        public static void ReadMythicPlusMember(Packet packet, params object[] indexes)
        {
            packet.ReadPackedGuid128("BnetAccountGUID", indexes);
            packet.ReadUInt64("GuildClubMemberID", indexes);
            packet.ReadPackedGuid128("GUID", indexes);
            packet.ReadPackedGuid128("GuildGUID", indexes);
            packet.ReadUInt32("NativeRealmAddress", indexes);
            packet.ReadUInt32("VirtualRealmAddress", indexes);
            packet.ReadInt32("ChrSpecializationID", indexes);
            packet.ReadSByteE<Race>("RaceID", indexes);
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
            packet.ReadSingle("RunScore", indexes);
            packet.ReadInt32("Unknown_1120", indexes);

            for (var i = 0u; i < memberCount; ++i)
                ReadMythicPlusMember(packet, indexes, i, "Member");

            packet.ResetBitReader();
            packet.ReadBit("Completed", indexes);
        }

        public static void ReadDungeonScoreBestRunForAffix(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("KeystoneAffixID", indexes);
            packet.ReadSingle("Score", indexes);

            ReadMythicPlusRun(packet, indexes, "Run");
        }

        public static void ReadDungeonScoreMapData(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("MapChallengeModeID", indexes);
            var runCount = packet.ReadUInt32("BestRunCount", indexes);
            packet.ReadSingle("OverAllScore", indexes);

            for (var i = 0u; i < runCount; ++i)
                ReadDungeonScoreBestRunForAffix(packet, indexes, i, "BestRun");
        }

        public static void ReadDungeonScoreSeasonData(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("Season", indexes);
            var runCount = packet.ReadUInt32("MapCount", indexes);
            var runCount2 = packet.ReadUInt32("LadderMapCount", indexes);
            packet.ReadSingle("SeasonScore", indexes);
            packet.ReadSingle("LadderScore", indexes);

            for (var i = 0u; i < runCount; ++i)
                ReadDungeonScoreMapData(packet, indexes, i, "Map");

            for (var i = 0u; i < runCount2; ++i)
                ReadDungeonScoreMapData(packet, indexes, i, "LadderMap");
        }

        public static void ReadDungeonScoreData(Packet packet, params object[] indexes)
        {
            var seasonCount = packet.ReadUInt32("SeasonCount", indexes);
            packet.ReadInt32("TotalRuns", indexes);
            for (var i = 0u; i < seasonCount; ++i)
                ReadDungeonScoreSeasonData(packet, indexes, i, "Season");
        }

        [Parser(Opcode.SMSG_MYTHIC_PLUS_SEASON_DATA)]
        public static void HandleMythicPlusSeasonData(Packet packet)
        {
            packet.ReadBit("IsMythicPlusActive");
        }

        [Parser(Opcode.SMSG_MYTHIC_PLUS_CURRENT_AFFIXES)]
        public static void HandleMythicPlusCurrentAffixes(Packet packet)
        {
            var count = packet.ReadUInt32();
            for (int i = 0; i < count; i++)
            {
                packet.ReadInt32("KeystoneAffixID", i);
                packet.ReadInt32("RequiredSeason", i);
            }
        }

        [Parser(Opcode.SMSG_MYTHIC_PLUS_NEW_WEEK_RECORD)]
        public static void HandleMythicPlusNewWeekRecord(Packet packet)
        {
            packet.ReadInt32("MapChallengeModeID");
            packet.ReadInt32("CompletionTime"); // in ms
            packet.ReadUInt32("KeystoneLevel");
        }

        [Parser(Opcode.CMSG_MYTHIC_PLUS_REQUEST_MAP_STATS)]
        public static void HandleMythicPlusRequestMapStats(Packet packet)
        {
            packet.ReadPackedGuid128("BnetAccountGUID");
            packet.ReadUInt64("GuildClubMemberID");
        }
    }
}
