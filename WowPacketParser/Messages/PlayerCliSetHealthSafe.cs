using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetHealthSafe
    {
        public ulong Target;
        public int ProcType;
        public int Health;
        public int ProcSubType;
    }
}
