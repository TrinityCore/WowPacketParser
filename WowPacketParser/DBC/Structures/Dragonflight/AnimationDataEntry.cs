using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.Dragonflight
{
    [DBFile("AnimationData")]
    public sealed class AnimationDataEntry
    {
        [Index(true)]
        public uint ID;
        public ushort Fallback;
        public byte BehaviorTier;
        public int BehaviorID;
        [Cardinality(2)]
        public int[] Flags = new int[2];
    }
}
