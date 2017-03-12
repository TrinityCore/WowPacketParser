using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ElaspedTimer
    {
        public uint TimerID;
        public UnixTime CurrentDuration;
    }
}
