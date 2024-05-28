using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_level_selector_quality_set")]
    public sealed record ItemLevelSelectorQualitySetHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("IlvlRare")]
        public short? IlvlRare;

        [DBFieldName("IlvlEpic")]
        public short? IlvlEpic;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
