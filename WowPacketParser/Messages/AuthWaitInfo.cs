using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct AuthWaitInfo
    {
        public uint WaitCount;
        public bool HasFCM;
    }
}
