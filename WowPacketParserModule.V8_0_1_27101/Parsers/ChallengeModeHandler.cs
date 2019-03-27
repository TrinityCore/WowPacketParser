using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class ChallengeModeHandler
    {
        [Parser(Opcode.CMSG_REQUEST_CHALLENGE_MODE_AFFIXES)]
        public static void HandleChallengeModeZero(Packet packet) { }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_REWARDS)]
        public static void HandleChallengeModeRewards(Packet packet)
        {
            packet.ReadBit("IsWeeklyRewardAvailable");

            packet.ReadInt32("LastWeekHighestKeyCompleted");
            packet.ReadInt32("LastWeekMapChallengeKeyEntry");
            packet.ReadInt32("CurrentWeekHighestKeyCompleted");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                packet.ReadInt32("UnkInt"); // always 13 for me
        }
    }
}
