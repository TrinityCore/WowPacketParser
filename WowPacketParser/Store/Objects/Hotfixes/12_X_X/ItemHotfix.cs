using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("item")]
    public sealed record ItemHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ClassID")]
        public int? ClassID;

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
        public uint? ItemGroupSoundsID;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("ModifiedCraftingReagentItemID")]
        public int? ModifiedCraftingReagentItemID;

        [DBFieldName("Unknown1200")]
        public byte? Unknown1200;

        [DBFieldName("CraftingQualityID")]
        public int? CraftingQualityID;

        [DBFieldName("ItemSquishEraID")]
        public int? ItemSquishEraID;

        [DBFieldName("RecraftReagentCountPercentage")]
        public float? RecraftReagentCountPercentage;

        [DBFieldName("OrderSource")]
        public byte? OrderSource;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
