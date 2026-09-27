using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("transmog_outfit_slot_info")]
    public sealed record TransmogOutfitSlotInfoHotfix1200: IDataModel
    {
        [DBFieldName("InventorySlotName")]
        public string InventorySlotName;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TransmogOutfitSlotEnum")]
        public sbyte? TransmogOutfitSlotEnum;

        [DBFieldName("InventorySlotEnum")]
        public int? InventorySlotEnum;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("Unused1200")]
        public byte? Unused1200;

        [DBFieldName("TransmogCollectionType")]
        public byte? TransmogCollectionType;

        [DBFieldName("SecondarySlotID")]
        public int? SecondarySlotID;

        [DBFieldName("InventorySlotID")]
        public int? InventorySlotID;

        [DBFieldName("UnassignedAtlasID")]
        public int? UnassignedAtlasID;

        [DBFieldName("UnassignedDisplayAtlasID")]
        public int? UnassignedDisplayAtlasID;

        [DBFieldName("ItemCostMultiplier")]
        public float? ItemCostMultiplier;

        [DBFieldName("IllusionCostMultiplier")]
        public float? IllusionCostMultiplier;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
