using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("modifier_tree")]
    public sealed record ModifierTreeHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Parent")]
        public uint? Parent;

        [DBFieldName("Operator")]
        public sbyte? Operator;

        [DBFieldName("Amount")]
        public sbyte? Amount;

        [DBFieldName("Type")]
        public int? Type;

        [DBFieldName("Asset")]
        public int? Asset;

        [DBFieldName("SecondaryAsset")]
        public int? SecondaryAsset;

        [DBFieldName("TertiaryAsset")]
        public int? TertiaryAsset;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
