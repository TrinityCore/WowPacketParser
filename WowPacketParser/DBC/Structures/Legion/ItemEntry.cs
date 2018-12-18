namespace WowPacketParser.DBC.Structures.Legion
{
    [DBFile("Item")]
    public sealed class ItemEntry
    {
        public uint FileDataID;
        public byte Class;
        public byte SubClass;
        public sbyte SoundOverrideSubclass;
        public sbyte Material;
        public byte InventoryType;
        public byte Sheath;
        public byte GroupSoundsID;
    }
}
