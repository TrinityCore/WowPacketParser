using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("phase_x_phase_group")]
    public sealed record PhaseXPhaseGroupHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PhaseID")]
        public ushort? PhaseID;

        [DBFieldName("PhaseGroupID")]
        public int? PhaseGroupID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
