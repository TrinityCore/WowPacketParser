using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAccountDataTimes
    {
        public UnixTime ServerTime;
        public uint TimeBits;
        public fixed long AccountTimes[8];
    }
}
