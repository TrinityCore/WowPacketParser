using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetTimeZoneInformation
    {
        public string ServerTimeTZ;
        public string GameTimeTZ;
    }
}
