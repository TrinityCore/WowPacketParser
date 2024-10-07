using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item")]
    public sealed record ItemHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ClassID")]
        public byte? ClassID;

        [DBFieldName("SubclassID")]
        public byte? SubclassID;

        [DBFieldName("Material")]
        public byte? Material;

        [DBFieldName("InventoryType")]
        public sbyte? InventoryType;

        [DBFieldName("SheatheType")]
        public byte? SheatheType;

        [DBFieldName("SoundOverrideSubclassID")]
        public sbyte? SoundOverrideSubclassID;

        [DBFieldName("IconFileDataID")]
        public int? IconFileDataID;

        [DBFieldName("ItemGroupSoundsID")]
        public byte? ItemGroupSoundsID;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("ModifiedCraftingReagentItemID")]
        public int? ModifiedCraftingReagentItemID;

        [DBFieldName("CraftingQualityID")]
        public int? CraftingQualityID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
