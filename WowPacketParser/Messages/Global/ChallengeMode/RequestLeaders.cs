using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Global.ChallengeMode
{
    public unsafe struct RequestLeaders
    {
        public UnixTime LastGuildUpdate;
        public int MapID;
        public UnixTime LastRealmUpdate;


        [Parser(Opcode.CMSG_CHALLENGE_MODE_REQUEST_LEADERS)]
        public static void HandleChallengeModeRequestLeaders(Packet packet)
        {
            packet.ReadInt32("MapId");
            packet.ReadTime("LastGuildUpdate");
            packet.ReadTime("LastRealmUpdate");
        }
    }
}
