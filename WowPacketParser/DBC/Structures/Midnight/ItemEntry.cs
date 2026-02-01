using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.Midnight
{
    [DBFile("Item")]
    public sealed class ItemEntry
    {
        [Index(true)]
        public uint ID;
        public int ClassID;
        public byte SubclassID;
        public byte Material;
        public sbyte InventoryType;
        public byte SheatheType;
        public sbyte SoundOverrideSubclassID;
        public int IconFileDataID;
        public uint ItemGroupSoundsID;
        public int ContentTuningID;
        public int ModifiedCraftingReagentItemID;
        public byte Unknown1200;
        public int CraftingQualityID;
        public int ItemSquishEraID;
        public float RecraftReagentCountPercentage;
        public byte OrderSource;
    }
}
