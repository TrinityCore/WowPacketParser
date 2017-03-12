using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetServerWowTime
    {
        public uint EncodedTime;
        public int HolidayOffset;
    }
}
