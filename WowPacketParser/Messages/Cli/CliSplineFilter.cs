using System.Collections.Generic;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliSplineFilter
    {
        public List<CliSplineFilterKey> FilterKeys;
        public ushort FilterFlags;
    }
}
