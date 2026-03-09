using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.Midnight
{
    [DBFile("Criteria")]
    public sealed class CriteriaEntry
    {
        [Index(false)]
        public uint ID;
        public short Type;
        public int Asset;
        public uint ModifierTreeID;
        public int StartEvent;
        public int StartAsset;
        public ushort StartTimer;
        public int FailEvent;
        public int FailAsset;
        public int Flags;
        public short EligibilityWorldStateID;
        public sbyte EligibilityWorldStateValue;
    }
}
