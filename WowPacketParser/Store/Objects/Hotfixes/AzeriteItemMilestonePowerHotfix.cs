using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("azerite_item_milestone_power")]
    public sealed record AzeriteItemMilestonePowerHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RequiredLevel")]
        public int? RequiredLevel;

        [DBFieldName("AzeritePowerID")]
        public int? AzeritePowerID;

        [DBFieldName("Type")]
        public int? Type;

        [DBFieldName("AutoUnlock")]
        public int? AutoUnlock;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("azerite_item_milestone_power")]
    public sealed record AzeriteItemMilestonePowerHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RequiredLevel")]
        public int? RequiredLevel;

        [DBFieldName("AzeritePowerID")]
        public int? AzeritePowerID;

        [DBFieldName("Type")]
        public int? Type;

        [DBFieldName("AutoUnlock")]
        public int? AutoUnlock;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
