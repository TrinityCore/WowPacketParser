using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.TheWarWithin
{
    [DBFile("Criteria")]
    public sealed class CriteriaEntry
    {
        [Index(true)]
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
