using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct MonsterSplineFilter
    {
        public List<MonsterSplineFilterKey> FilterKeys;
        public ushort FilterFlags;
        public float BaseSpeed;
        public short StartOffset;
        public float DistToPrevFilterKey;
        public short AddedToStart;
    }
}
