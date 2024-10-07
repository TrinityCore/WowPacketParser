using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_level_selector")]
    public sealed record ItemLevelSelectorHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MinItemLevel")]
        public ushort? MinItemLevel;

        [DBFieldName("ItemLevelSelectorQualitySetID")]
        public ushort? ItemLevelSelectorQualitySetID;

        [DBFieldName("AzeriteUnlockMappingSet")]
        public ushort? AzeriteUnlockMappingSet;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
