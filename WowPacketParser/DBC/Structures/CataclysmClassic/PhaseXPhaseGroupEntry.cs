using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.CataclysmClassic
{
    [DBFile("PhaseXPhaseGroup")]
    public sealed class PhaseXPhaseGroupEntry
    {
        [Index(true)]
        public uint ID;
        public ushort PhaseID;
        public uint PhaseGroupID;
    }
}
