using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_child_equipment")]
    public sealed record ItemChildEquipmentHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ParentItemID")]
        public uint? ParentItemID;

        [DBFieldName("ChildItemID")]
        public int? ChildItemID;

        [DBFieldName("ChildItemEquipSlot")]
        public int? ChildItemEquipSlot;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
