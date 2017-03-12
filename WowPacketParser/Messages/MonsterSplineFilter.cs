using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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
