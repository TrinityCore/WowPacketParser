using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliSplineFilter
    {
        public List<CliSplineFilterKey> FilterKeys;
        public ushort FilterFlags;
    }
}
