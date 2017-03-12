using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGameTimeUpdate
    {
        public int GameTimeHolidayOffset;
        public int ServerTimeHolidayOffset;
        public uint GameTime;
        public uint ServerTime;
    }
}
