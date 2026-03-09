using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.Midnight
{
    [DBFile("PhaseXPhaseGroup")]
    public sealed class PhaseXPhaseGroupEntry
    {
        [Index(true)]
        public uint ID;
        public ushort PhaseID;
        [NonInlineRelation(typeof(uint))]
        public int PhaseGroupID;
    }
}
