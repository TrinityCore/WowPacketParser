using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAccountDataTimes
    {
        public UnixTime ServerTime;
        public uint TimeBits;
        public fixed long AccountTimes[8];
    }
}
