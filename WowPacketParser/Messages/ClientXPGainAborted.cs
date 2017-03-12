using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientXPGainAborted
    {
        public ulong Victim;
        public int XpToAdd;
        public int XpGainReason;
        public int XpAbortReason;
    }
}
