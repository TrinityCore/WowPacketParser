using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class ChallengeModeHandler
    {
        public static void ReadChallengeModeAttempt(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("InstanceRealmAddress", indexes);
            packet.Translator.ReadInt32("AttemptID", indexes);
            packet.Translator.ReadInt32("CompletionTime", indexes);
            packet.Translator.ReadPackedTime("CompletionDate", indexes);
            packet.Translator.ReadInt32("MedalEarned", indexes);

            var int12 = packet.Translator.ReadInt32("MembersCount", indexes);
            for (int i = 0; i < int12; i++)
            {
                packet.Translator.ReadInt32("VirtualRealmAddress", indexes, i);
                packet.Translator.ReadInt32("NativeRealmAddress", indexes, i);
                packet.Translator.ReadPackedGuid128("Guid", indexes, i);
                packet.Translator.ReadInt32("SpecializationID", indexes, i);
            }
        }

        public static void ReadItemReward(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("ItemID", indexes);
            packet.Translator.ReadInt32("ItemDisplayID", indexes);
            packet.Translator.ReadUInt32("Quantity", indexes);
        }

        public static void ReadMapChallengeModeReward(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("MapId", indexes);

            var int4 = packet.Translator.ReadInt32("ChallengeModeRewardCount", indexes);
            for (int i = 0; i < int4; i++)
            {
                var int1 = packet.Translator.ReadInt32("ItemRewardsCount", indexes, i);
                var in16 = packet.Translator.ReadInt32("CurrencyRewardsCOunt", indexes, i);
                packet.Translator.ReadInt32("Money", indexes, i);

                for (int j = 0; j < int1; j++)
                {
                    // sub_5FB0EE
                    ReadItemReward(packet, indexes, i, j);
                }

                for (int j = 0; j < in16; j++)
                {
                    // sub_5FCEE8
                    packet.Translator.ReadUInt32("CurrencyID", indexes, i, j);
                    packet.Translator.ReadUInt32("Quantity", indexes, i, j);
                }
            }
        }

        public static void ReadClientChallengeModeMap(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("MapId", indexes);
            packet.Translator.ReadInt32("BestCompletionMilliseconds", indexes);
            packet.Translator.ReadInt32("LastCompletionMilliseconds", indexes);
            packet.Translator.ReadInt32("BestMedal", indexes);
            packet.Translator.ReadPackedTime("BestMedalDate", indexes);

            var bestSpecCount = packet.Translator.ReadInt32("BestSpecIDCount", indexes);
            for (int i = 0; i < bestSpecCount; i++)
                packet.Translator.ReadInt16("BestSpecID", indexes, i);
        }

        [Parser(Opcode.CMSG_CHALLENGE_MODE_REQUEST_MAP_STATS)]
        [Parser(Opcode.CMSG_GET_CHALLENGE_MODE_REWARDS)]
        public static void HandleChallengeModeZero(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_CHALLENGE_MODE_REQUEST_LEADERS)]
        public static void HandleChallengeModeRequestLeaders(Packet packet)
        {
            packet.Translator.ReadInt32("MapId");
            packet.Translator.ReadTime("LastGuildUpdate");
            packet.Translator.ReadTime("LastRealmUpdate");
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_REQUEST_LEADERS_RESULT)]
        public static void HandleChallengeModeRequestLeadersResult(Packet packet)
        {
            packet.Translator.ReadInt32("MapID");
            packet.Translator.ReadTime("LastGuildUpdate");
            packet.Translator.ReadTime("LastRealmUpdate");

            var int4 = packet.Translator.ReadInt32("GuildLeadersCount");
            var int9 = packet.Translator.ReadInt32("RealmLeadersCount");

            for (int i = 0; i < int4; i++)
                ReadChallengeModeAttempt(packet, i, "GuildLeaders");

            for (int i = 0; i < int9; i++)
                ReadChallengeModeAttempt(packet, i, "RealmLeaders");
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_REWARDS)]
        public static void HandleChallegeModeRewards(Packet packet)
        {
            var int16 = packet.Translator.ReadInt32("MapChallengeModeRewardCount");
            var int32 = packet.Translator.ReadInt32("ItemRewardCount");

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
            var challengeModeMapCount = packet.Translator.ReadInt32("ChallengeModeMapCount");
            for (int i = 0; i < challengeModeMapCount; i++)
                ReadClientChallengeModeMap(packet, i);
        }
    }
}
