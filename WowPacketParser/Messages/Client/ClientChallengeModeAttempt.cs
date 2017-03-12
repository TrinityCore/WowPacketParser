using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientChallengeModeAttempt
    {
        public uint InstanceRealmAddress;
        public uint AttemptID;
        public int CompletionTime;
        public Data CompletionDate;
        public int MedalEarned;
        public List<ClientChallengeModeMember> Members;
    }
}
