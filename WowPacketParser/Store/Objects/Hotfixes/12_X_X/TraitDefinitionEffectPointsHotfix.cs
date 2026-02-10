using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_definition_effect_points")]
    public sealed record TraitDefinitionEffectPointsHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitDefinitionID")]
        public uint? TraitDefinitionID;

        [DBFieldName("EffectIndex")]
        public int? EffectIndex;

        [DBFieldName("OperationType")]
        public int? OperationType;

        [DBFieldName("CurveID")]
        public int? CurveID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
