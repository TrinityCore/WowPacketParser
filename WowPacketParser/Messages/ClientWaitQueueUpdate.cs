using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientWaitQueueUpdate
    {
        public AuthWaitInfo WaitInfo;
    }
}
