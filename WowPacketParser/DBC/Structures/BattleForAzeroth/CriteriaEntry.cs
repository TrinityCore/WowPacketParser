namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("Criteria")]
    public sealed class CriteriaEntry
    {
        public short Type;
        public int Asset;
        public uint ModifierTreeID;
        public byte StartEvent;
        public int StartAsset;
        public ushort StartTimer;
        public byte FailEvent;
        public int FailAsset;
        public byte Flags;
        public short EligibilityWorldStateID;
        public sbyte EligibilityWorldStatevalue;
    }
}
