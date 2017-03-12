using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliPerformActionSetCheat
    {
        public ulong Target;
        public int ActionSetID;
    }
}
