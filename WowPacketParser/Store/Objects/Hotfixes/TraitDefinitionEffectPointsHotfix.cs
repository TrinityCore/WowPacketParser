using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_definition_effect_points")]
    public sealed record TraitDefinitionEffectPointsHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitDefinitionID")]
        public int? TraitDefinitionID;

        [DBFieldName("EffectIndex")]
        public int? EffectIndex;

        [DBFieldName("OperationType")]
        public int? OperationType;

        [DBFieldName("CurveID")]
        public int? CurveID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("trait_definition_effect_points")]
    public sealed record TraitDefinitionEffectPointsHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitDefinitionID")]
        public int? TraitDefinitionID;

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
