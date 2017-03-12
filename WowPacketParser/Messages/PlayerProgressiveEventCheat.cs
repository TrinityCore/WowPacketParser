using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerProgressiveEventCheat
    {
        public ProgressiveEventCheat Type;
        public int ItemID;
        public int Count;
        public int EventID;
    }
}
