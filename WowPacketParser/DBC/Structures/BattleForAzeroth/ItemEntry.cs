namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("Item")]
    public sealed class ItemEntry
    {
        public byte ClassID;
        public byte SubclassID;
        public byte Material;
        public sbyte InventoryType;
        public byte SheatheType;
        public sbyte SoundOverrideSubclassID;
        public int IconFileDataID;
        public byte ItemGroupSoundsID;
    }
}
