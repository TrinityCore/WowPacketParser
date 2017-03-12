using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAuraDataInfo
    {
        public int SpellID;
        public byte Flags;
        public uint ActiveFlags;
        public ushort CastLevel;
        public byte Applications;
        public ulong? CastUnit; // Optional
        public int? Duration; // Optional
        public int? Remaining; // Optional
        public List<float> Points;
        public List<float> EstimatedPoints;
    }
}
