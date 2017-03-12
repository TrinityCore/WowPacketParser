using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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
