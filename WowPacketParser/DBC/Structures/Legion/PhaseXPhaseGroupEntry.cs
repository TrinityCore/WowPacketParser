using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFile("PhaseXPhaseGroup")]

    public sealed class PhaseXPhaseGroupEntry
    {
        public ushort PhaseID;
        public ushort PhaseGroupID;
    }
}
