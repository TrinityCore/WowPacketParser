using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_child_equipment")]
    public sealed record ItemChildEquipmentHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ParentItemID")]
        public uint? ParentItemID;

        [DBFieldName("ChildItemID")]
        public int? ChildItemID;

        [DBFieldName("ChildItemEquipSlot")]
        public byte? ChildItemEquipSlot;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
