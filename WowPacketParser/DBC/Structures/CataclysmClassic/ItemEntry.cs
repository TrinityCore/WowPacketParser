using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.CataclysmClassic
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
        public int RequiredLevel;
        public byte SheatheType;
        public ushort RandomSelect;
        public ushort ItemRandomSuffixGroupID;
        public sbyte SoundOverrideSubclassID;
        public ushort ScalingStatDistributionID;
        public int IconFileDataID;
        public byte ItemGroupSoundsID;
        public int ContentTuningID;
        public uint MaxDurability;
        public byte AmmunitionType;
        public int ScalingStatValue;
        [Cardinality(5)]
        public byte[] DamageType = new byte[5];
        [Cardinality(7)]
        public int[] Resistances = new int[7];
        [Cardinality(5)]
        public int[] MinDamage = new int[5];
        [Cardinality(5)]
        public int[] MaxDamage = new int[5];
    }
}
