using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.TheWarWithin
{
    [DBFile("AnimationData")]
    public sealed class AnimationDataEntry
    {
        [Index(true)]
        public uint ID;
        public ushort Fallback;
        public byte BehaviorTier;
        public short BehaviorID;
        [Cardinality(2)]
        public int[] Flags = new int[2];
    }
}
