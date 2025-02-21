using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.Legion
{
    [DBFile("AnimationData")]
    public sealed class AnimationDataEntry
    {
        [Index(true)]
        public uint ID;
        public int Flags;
        public ushort Fallback;
        public ushort BehaviorID;
        public byte BehaviorTier;
    }
}
