using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("garr_plot")]
    public sealed record GarrPlotHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("PlotType")]
        public byte? PlotType;

        [DBFieldName("HordeConstructObjID")]
        public int? HordeConstructObjID;

        [DBFieldName("AllianceConstructObjID")]
        public int? AllianceConstructObjID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("UpgradeRequirement", 2)]
        public uint?[] UpgradeRequirement;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
