using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientShowZonesCheatResult
    {
        public List<ShowZoneRLE> Run;
    }
}
