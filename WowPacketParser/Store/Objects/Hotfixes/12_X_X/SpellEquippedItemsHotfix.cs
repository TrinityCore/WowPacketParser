using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_equipped_items")]
    public sealed record SpellEquippedItemsHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("EquippedItemClass")]
        public int? EquippedItemClass;

        [DBFieldName("EquippedItemInvTypes")]
        public int? EquippedItemInvTypes;

        [DBFieldName("EquippedItemSubclass")]
        public int? EquippedItemSubclass;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
