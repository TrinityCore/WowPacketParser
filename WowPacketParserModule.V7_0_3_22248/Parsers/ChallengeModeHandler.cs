using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class ChallengeModeHandler
    {
        [Parser(Opcode.SMSG_CHALLENGE_MODE_REWARDS)]
        public static void HandleChallengeModeRewards(Packet packet)
        {
            packet.ReadBit("IsWeeklyRewardAvailable");
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
            packet.ReadInt32("Unk");
        }
    }
}

