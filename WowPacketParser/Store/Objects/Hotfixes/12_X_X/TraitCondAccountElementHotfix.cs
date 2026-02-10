using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_cond_account_element")]
    public sealed record TraitCondAccountElementHotfix1200 : IDataModel
    {
        [DBFieldName("ElementValueInt")]
        public long? ElementValueInt;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PlayerDataElementAccountID")]
        public uint? PlayerDataElementAccountID;

        [DBFieldName("Comparison")]
        public byte? Comparison;

        [DBFieldName("Unused1110")]
        public int? Unused1110;

        [DBFieldName("PlayerDataElementCharacterID")]
        public int? PlayerDataElementCharacterID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
