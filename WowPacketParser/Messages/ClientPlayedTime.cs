using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPlayedTime
    {
        public int TotalTime;
        public bool TriggerEvent;
        public int LevelTime;
    }
}
