using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_system")]
    public sealed record TraitSystemHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("WidgetSetID")]
        public int? WidgetSetID;

        [DBFieldName("TraitChangeSpell")]
        public int? TraitChangeSpell;

        [DBFieldName("ItemID")]
        public int? ItemID;

        [DBFieldName("VariationType")]
        public int? VariationType;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
