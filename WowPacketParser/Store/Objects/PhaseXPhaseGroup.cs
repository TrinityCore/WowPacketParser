using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("phase_group")]
    public sealed class PhaseXPhaseGroup
    {
        [DBFieldName("PhaseID")]
        public uint PhaseID;

        [DBFieldName("PhaseGroupID")]
        public uint PhaseGroupID;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
