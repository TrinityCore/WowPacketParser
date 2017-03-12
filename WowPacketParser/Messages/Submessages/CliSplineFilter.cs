using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliSplineFilter
    {
        public List<CliSplineFilterKey> FilterKeys;
        public ushort FilterFlags;
    }
}
