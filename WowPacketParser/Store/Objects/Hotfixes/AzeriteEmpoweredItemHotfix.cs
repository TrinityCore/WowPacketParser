using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("azerite_empowered_item")]
    public sealed record AzeriteEmpoweredItemHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemID")]
        public int? ItemID;

        [DBFieldName("AzeriteTierUnlockSetID")]
        public uint? AzeriteTierUnlockSetID;

        [DBFieldName("AzeritePowerSetID")]
        public uint? AzeritePowerSetID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("azerite_empowered_item")]
    public sealed record AzeriteEmpoweredItemHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemID")]
        public int? ItemID;

        [DBFieldName("AzeriteTierUnlockSetID")]
        public uint? AzeriteTierUnlockSetID;

        [DBFieldName("AzeritePowerSetID")]
        public uint? AzeritePowerSetID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
