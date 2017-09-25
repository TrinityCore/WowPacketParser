using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct BattlenetChallengeResponse
    {
        public BattlenetChallengeResult Result;
        public uint Token;
        public string BattlenetError;

        [Parser(Opcode.CMSG_BATTLENET_CHALLENGE_RESPONSE)]
        public static void HandleBattlenetChallengeResponse(Packet packet)
        {
            packet.ReadUInt32("Token");
            var result = packet.ReadBits(3);
            if (result == 3)
            {
                var bits24 = packet.ReadBits(6);
                packet.ReadWoWString("BattlenetError", bits24);
            }
        }
    }
}
