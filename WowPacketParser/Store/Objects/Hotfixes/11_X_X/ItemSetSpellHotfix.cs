using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_set_spell")]
    public sealed record ItemSetSpellHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChrSpecID")]
        public ushort? ChrSpecID;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("Threshold")]
        public byte? Threshold;

        [DBFieldName("ItemSetID")]
        public uint? ItemSetID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
