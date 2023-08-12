using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class MythicPlusHandler
    {
        [Parser(Opcode.SMSG_MYTHIC_PLUS_ALL_MAP_STATS)]
        public static void HandleMythicPlusAllMapStats(Packet packet)
        {
            var runCount = packet.ReadUInt32("RunCount");
            var rewardCount = packet.ReadUInt32("RewardCount");
            packet.ReadInt32("Season");
            packet.ReadInt32("Subseason");

            for (var i = 0; i < runCount; ++i)
                Substructures.MythicPlusHandler.ReadMythicPlusRun(packet, i, "Run");

            for (var i = 0; i < rewardCount; ++i)
                ReadMythicPlusReward(packet, i, "Reward");
        }

        public static void ReadMythicPlusReward(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("field_0", indexes);
            packet.ReadInt32("field_4", indexes);
            packet.ReadInt64("field_8", indexes);
            packet.ReadInt64("field_10", indexes);
            packet.ReadInt64("field_20", indexes);
            packet.ResetBitReader();
            packet.ReadBit("field_24", indexes);
        }

        [Parser(Opcode.SMSG_MYTHIC_PLUS_SEASON_DATA)]
        public static void HandleMythicPlusSeasonData(Packet packet)
        {
           packet.ReadBit("IsMythicPlusActive");
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_COMPLETE, ClientVersionBuild.V9_1_0_39185)]
        public static void HandleChallengeModeCompleted910(Packet packet)
        {
            Substructures.MythicPlusHandler.ReadMythicPlusRun(packet, "MythicPlusRun");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
                packet.ReadSingle("NewDungeonScore");
            else
                packet.ReadInt32("NewDungeonScore");

            var memberCount = packet.ReadUInt32("MemberCount");

            packet.ResetBitReader();
            packet.ReadBit("IsPracticeRun");
            packet.ReadBit("IsAffixRecorded");
            packet.ReadBit("IsMapRecord");

            for (var i = 0; i < memberCount; ++i)
            {
                packet.ReadPackedGuid128("PlayerGUID", i);

                packet.ResetBitReader();
                var nameLen = packet.ReadBits(6);
                packet.ReadBit("IsEligibleForScore");

                packet.ReadWoWString("PlayerName", nameLen, i);
            }
        }
    }
}
