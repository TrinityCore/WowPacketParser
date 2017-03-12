using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientChallengeModeNewPlayerRecord
    {
        public int Medal;
        public int CompletionMilliseconds;
        public int MapID;
    }
}
