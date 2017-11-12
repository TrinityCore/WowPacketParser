using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.ChallengeMode
{
    public unsafe struct DeleteLeaderResult
    {
        public int MapID;
        public uint AttemptID;

        [Parser(Opcode.SMSG_CHALLENGE_MODE_DELETE_LEADER_RESULT)]
        public static void HandleChallengeModeDeleteLeaderResult(Packet packet)
        {
            packet.ReadInt32("Int5");
            packet.ReadInt32("Int4");
            packet.ReadBit("unk");
        }
    }
}
