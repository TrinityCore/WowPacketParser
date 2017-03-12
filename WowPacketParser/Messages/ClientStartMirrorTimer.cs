using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientStartMirrorTimer
    {
        public int Scale;
        public int MaxValue;
        public int Timer;
        public int SpellID;
        public int Value;
        public bool Paused;
    }
}
