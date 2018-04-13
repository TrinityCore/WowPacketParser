using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFile("Criteria")]
    public sealed class CriteriaEntry
    {
        public uint Asset;
        public uint StartAsset;
        public uint FailAsset;
        public ushort StartTime;
        public ushort ModifiertTreeId;
        public ushort EligibilityWorldStateID;
        public byte Type;
        public byte StartEvent;
        public byte FailEvent;
        public byte Flags;
        public byte EligibilityWorldStateValue;
    }
}
