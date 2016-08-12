using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Item, HasIndexInData = false)]
    public class ItemEntry
    {
        public uint FileDataID { get; set; }
        public byte Class { get; set; }
        public byte SubClass { get; set; }
        public sbyte SoundOverrideSubclass { get; set; }
        public sbyte Material { get; set; }
        public byte InventoryType { get; set; }
        public byte Sheath { get; set; }
        public byte GroupSoundsID { get; set; }
    }
}