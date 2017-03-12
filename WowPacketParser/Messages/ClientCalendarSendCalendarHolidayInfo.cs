using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCalendarSendCalendarHolidayInfo
    {
        public int HolidayID;
        public int Region;
        public int Looping;
        public int Priority;
        public int FilterType;
        public string TextureFilename;
        public fixed int Date[26];
        public fixed int Duration[10];
        public fixed int CalendarFlags[10];
    }
}
