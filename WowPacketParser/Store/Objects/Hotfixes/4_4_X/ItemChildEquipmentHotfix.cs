using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item_child_equipment")]
    public sealed record ItemChildEquipmentHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ChildItemID")]
        public int? ChildItemID;

        [DBFieldName("ChildItemEquipSlot")]
        public byte? ChildItemEquipSlot;

        [DBFieldName("ParentItemID")]
        public int? ParentItemID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
