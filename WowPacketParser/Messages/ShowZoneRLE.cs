using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ShowZoneRLE
    {
        public int Run;
        public byte CurID;
    }
}
