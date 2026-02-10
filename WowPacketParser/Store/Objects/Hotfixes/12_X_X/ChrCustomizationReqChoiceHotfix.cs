using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_customization_req_choice")]
    public sealed record ChrCustomizationReqChoiceHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrCustomizationChoiceID")]
        public int? ChrCustomizationChoiceID;

        [DBFieldName("ChrCustomizationReqID")]
        public uint? ChrCustomizationReqID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
