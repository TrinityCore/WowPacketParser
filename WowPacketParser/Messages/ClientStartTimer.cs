using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientStartTimer
    {
        public UnixTime TimeRemaining;
        public UnixTime TotalTime;
        public int Type;
    }
}
