using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientChallengeModeDumpLeadersResult
    {
        public int MapID;
        public bool Success;
        public List<ClientChallengeModeAttempt> Leaders;
    }
}
