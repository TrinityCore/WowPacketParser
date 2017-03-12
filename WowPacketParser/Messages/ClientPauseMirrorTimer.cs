using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPauseMirrorTimer
    {
        public bool Paused;
        public int Timer;
    }
}
