using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ChallengeModeHandler
    {
        public static void ReadChallengeModeAttempt(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("InstanceRealmAddress", indexes);
            packet.ReadInt32("AttemptID", indexes);
            packet.ReadInt32("CompletionTime", indexes);
            packet.ReadPackedTime("CompletionDate", indexes);
            packet.ReadInt32("MedalEarned", indexes);

            var int12 = packet.ReadInt32("MembersCount", indexes);
            for (int i = 0; i < int12; i++)
            {
                packet.ReadInt32("VirtualRealmAddress", indexes, i);
                packet.ReadInt32("NativeRealmAddress", indexes, i);
                packet.ReadPackedGuid128("Guid", indexes, i);
                packet.ReadInt32("SpecializationID", indexes, i);
            }
        }

        public static void ReadItemReward(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("ItemID", indexes);
            packet.ReadInt32("ItemDisplayID", indexes);
            packet.ReadUInt32("Quantity", indexes);
        }

        public static void ReadMapChallengeModeReward(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("MapId", indexes);

            var int4 = packet.ReadInt32("ChallengeModeRewardCount", indexes);
            for (int i = 0; i < int4; i++)
            {
                var int1 = packet.ReadInt32("ItemRewardsCount", indexes, i);
                var in16 = packet.ReadInt32("CurrencyRewardsCOunt", indexes, i);
                packet.ReadInt32("Money", indexes, i);

                for (int j = 0; j < int1; j++)
                {
                    // sub_5FB0EE
                    ReadItemReward(packet, indexes, i, j);
                }

                for (int j = 0; j < in16; j++)
                {
                    // sub_5FCEE8
                    packet.ReadUInt32("CurrencyID", indexes, i, j);
                    packet.ReadUInt32("Quantity", indexes, i, j);
                }
            }
        }

        public static void ReadClientChallengeModeMap(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("MapId", indexes);
            packet.ReadInt32("BestCompletionMilliseconds", indexes);
            packet.ReadInt32("LastCompletionMilliseconds", indexes);
            packet.ReadInt32("BestMedal", indexes);
            packet.ReadPackedTime("BestMedalDate", indexes);

            var bestSpecCount = packet.ReadInt32("BestSpecIDCount", indexes);
            for (int i = 0; i < bestSpecCount; i++)
                packet.ReadInt16("BestSpecID", indexes, i);
        }

        [Parser(Opcode.CMSG_CHALLENGE_MODE_REQUEST_MAP_STATS)]
        [Parser(Opcode.CMSG_GET_CHALLENGE_MODE_REWARDS)]
        public static void HandleChallengeModeZero(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_CHALLENGE_MODE_REQUEST_LEADERS)]
        public static void HandleChallengeModeRequestLeaders(Packet packet)
        {
            packet.ReadInt32("MapId");
            packet.ReadTime("LastGuildUpdate");
            packet.ReadTime("LastRealmUpdate");
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_REQUEST_LEADERS_RESULT)]
        public static void HandleChallengeModeRequestLeadersResult(Packet packet)
        {
            packet.ReadInt32("MapID");
            packet.ReadTime("LastGuildUpdate");
            packet.ReadTime("LastRealmUpdate");

            var int4 = packet.ReadInt32("GuildLeadersCount");
            var int9 = packet.ReadInt32("RealmLeadersCount");

            for (int i = 0; i < int4; i++)
                ReadChallengeModeAttempt(packet, i, "GuildLeaders");

            for (int i = 0; i < int9; i++)
                ReadChallengeModeAttempt(packet, i, "RealmLeaders");
        }

        [Parser(Opcode.SMSG_CHALLEGE_MODE_REWARDS)]
        public static void HandleChallegeModeRewards(Packet packet)
        {
            var int16 = packet.ReadInt32("MapChallengeModeRewardCount");
            var int32 = packet.ReadInt32("ItemRewardCount");

            for (int i = 0; i < int16; i++)
            {
                // sub_61BE26
                ReadMapChallengeModeReward(packet, i, "MapChallengeModeReward");
            }

            for (int i = 0; i < int32; i++)
            {
                // sub_5FB0EE
                ReadItemReward(packet, i, "ItemReward");
            }
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_ALL_MAP_STATS)]
        public static void HandleChallengeModeAllMapStats(Packet packet)
        {
            var challengeModeMapCount = packet.ReadInt32("ChallengeModeMapCount");
            for (int i = 0; i < challengeModeMapCount; i++)
                ReadClientChallengeModeMap(packet, i);
        }
    }
}
