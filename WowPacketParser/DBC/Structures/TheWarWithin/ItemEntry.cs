using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.TheWarWithin
{
    [DBFile("Item")]
    public sealed class ItemEntry
    {
        [Index(true)]
        public uint ID;
        public byte ClassID;
        public byte SubclassID;
        public byte Material;
        public sbyte InventoryType;
        public byte SheatheType;
        public sbyte SoundOverrideSubclassID;
        public int IconFileDataID;
        public byte ItemGroupSoundsID;
        public int ContentTuningID;
        public int ModifiedCraftingReagentItemID;
        public int CraftingQualityID;
    }
}
