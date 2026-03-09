using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.Midnight
{
    [DBFile("AnimationData")]
    public sealed class AnimationDataEntry
    {
        [Index(true)]
        public uint ID;
        public ushort Fallback;
        public sbyte BehaviorTier;
        public short BehaviorID;
        [Cardinality(2)]
        public int[] Flags = new int[2];
    }
}
