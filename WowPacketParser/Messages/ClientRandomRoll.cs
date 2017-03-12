using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRandomRoll
    {
        public ulong Roller;
        public int Result;
        public int Max;
        public int Min;
    }
}
