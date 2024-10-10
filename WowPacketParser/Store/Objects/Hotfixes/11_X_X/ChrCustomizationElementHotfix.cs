using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_customization_element")]
    public sealed record ChrCustomizationElementHotfix1100 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrCustomizationChoiceID")]
        public int? ChrCustomizationChoiceID;

        [DBFieldName("RelatedChrCustomizationChoiceID")]
        public int? RelatedChrCustomizationChoiceID;

        [DBFieldName("ChrCustomizationGeosetID")]
        public int? ChrCustomizationGeosetID;

        [DBFieldName("ChrCustomizationSkinnedModelID")]
        public int? ChrCustomizationSkinnedModelID;

        [DBFieldName("ChrCustomizationMaterialID")]
        public int? ChrCustomizationMaterialID;

        [DBFieldName("ChrCustomizationBoneSetID")]
        public int? ChrCustomizationBoneSetID;

        [DBFieldName("ChrCustomizationCondModelID")]
        public int? ChrCustomizationCondModelID;

        [DBFieldName("ChrCustomizationDisplayInfoID")]
        public int? ChrCustomizationDisplayInfoID;

        [DBFieldName("ChrCustItemGeoModifyID")]
        public int? ChrCustItemGeoModifyID;

        [DBFieldName("ChrCustomizationVoiceID")]
        public int? ChrCustomizationVoiceID;

        [DBFieldName("AnimKitID")]
        public int? AnimKitID;

        [DBFieldName("ParticleColorID")]
        public int? ParticleColorID;

        [DBFieldName("ChrCustGeoComponentLinkID")]
        public int? ChrCustGeoComponentLinkID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
