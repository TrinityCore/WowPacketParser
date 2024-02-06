using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class ChallengeModeHandler
    {
        [Parser(Opcode.SMSG_MYTHIC_PLUS_WEEKLY_REWARD_RESPONSE)]
        public static void HandleMythicPlusWeeklyRewardResponse(Packet packet)
        {
            packet.ReadBit("IsWeeklyRewardAvailable");
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_START)]
        public static void HandleChallengeModeStart(Packet packet)
        {
            packet.ReadInt32("MapId");
            packet.ReadInt32("ChallengeId");
            packet.ReadInt32("StartedChallengeLevel");
            packet.ReadInt32("Affix1");
            packet.ReadInt32("Affix2");
            packet.ReadInt32("Affix3");
            packet.ReadInt32("Unk0");
            packet.ReadInt32("Unk1");
            packet.ReadBit("IsKeyCharged");
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_REQUEST_LEADERS_RESULT)]
        public static void HandleChallengeModeRequestLeadersResult(Packet packet)
        {
            packet.ReadInt32("MapID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_0_23706))
                packet.ReadInt32("Unk");
            packet.ReadTime("LastGuildUpdate");
            packet.ReadTime("LastRealmUpdate");

            var int4 = packet.ReadInt32("GuildLeadersCount");
            var int9 = packet.ReadInt32("RealmLeadersCount");

            for (int i = 0; i < int4; i++)
                V6_0_2_19033.Parsers.ChallengeModeHandler.ReadChallengeModeAttempt(packet, i, "GuildLeaders");

            for (int i = 0; i < int9; i++)
                V6_0_2_19033.Parsers.ChallengeModeHandler.ReadChallengeModeAttempt(packet, i, "RealmLeaders");
        }

        [Parser(Opcode.CMSG_CHALLENGE_MODE_REQUEST_LEADERS)]
        public static void HandleChallengeModeRequestLeaders(Packet packet)
        {
            packet.ReadInt32("MapId");
            packet.ReadTime("LastGuildUpdate");
            packet.ReadTime("LastRealmUpdate");
            packet.ReadTime("CompletionDate");
        }

        [Parser(Opcode.SMSG_MYTHIC_PLUS_NEW_WEEK_RECORD)]
        public static void HandleMythicPlusNewWeekRecord(Packet packet)
        {
            packet.ReadInt32("MapChallengeModeID");
            packet.ReadInt32("CompletionTime"); // in ms
            packet.ReadUInt32("KeystoneLevel");
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_UPDATE_DEATH_COUNT)]
        public static void ChallengeModeUpdateDeathCount(Packet packet)
        {
            packet.ReadInt32("NewDeathCount");
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_RESET)]
        public static void HandleChallengeModeReset(Packet packet)
        {
            packet.ReadInt32("MapId");
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_COMPLETE)]
        public static void HandleChallengeModeComplete(Packet packet)
        {
            packet.ReadInt32("CompletionTime");
            packet.ReadInt32("MapId");
            packet.ReadInt32("ChallengeId");
            packet.ReadInt32("RewardLevel");
            packet.ReadBit("IsCompletedInTimer");
        }
    }
}

