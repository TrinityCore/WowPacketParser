using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFileName("PhaseXPhaseGroup")]

    public sealed class PhaseXPhaseGroupEntry
    {
        public ushort PhaseID;
        public ushort PhaseGroupID;
    }
}
