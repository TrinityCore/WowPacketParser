using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientChallengeModeDumpLeadersResult
    {
        public int MapID;
        public bool Success;
        public List<ClientChallengeModeAttempt> Leaders;
    }
}
