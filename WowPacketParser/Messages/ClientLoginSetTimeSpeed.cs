using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLoginSetTimeSpeed
    {
        public float NewSpeed;
        public int ServerTimeHolidayOffset;
        public uint GameTime;
        public uint ServerTime;
        public int GameTimeHolidayOffset;
    }
}
