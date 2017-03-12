using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientChallengeModeMap
    {
        public int MapID;
        public int BestCompletionMilliseconds;
        public int LastCompletionMilliseconds;
        public int BestMedal;
        public Data BestMedalDate;
        public List<short> BestSpecID;
    }
}
