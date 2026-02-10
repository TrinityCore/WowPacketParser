using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("azerite_unlock_mapping")]
    public sealed record AzeriteUnlockMappingHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ItemLevel")]
        public int? ItemLevel;

        [DBFieldName("ItemBonusListHead")]
        public int? ItemBonusListHead;

        [DBFieldName("ItemBonusListShoulders")]
        public int? ItemBonusListShoulders;

        [DBFieldName("ItemBonusListChest")]
        public int? ItemBonusListChest;

        [DBFieldName("AzeriteUnlockMappingSetID")]
        public uint? AzeriteUnlockMappingSetID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
