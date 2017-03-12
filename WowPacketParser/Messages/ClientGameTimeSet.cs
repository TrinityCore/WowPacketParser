using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGameTimeSet
    {
        public uint ServerTime;
        public int GameTimeHolidayOffset;
        public int ServerTimeHolidayOffset;
        public uint GameTime;
    }
}
