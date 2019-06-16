using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CurrencyTypes, HasIndexInData = false)]
    public class CurrencyTypesEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte CategoryID { get; set; }
        public int InventoryIconFileID { get; set; }
        public uint SpellWeight { get; set; }
        public byte SpellCategory { get; set; }
        public uint MaxQty { get; set; }
        public uint MaxEarnablePerWeek { get; set; }
        public uint Flags { get; set; }
        public sbyte Quality { get; set; }
        public int FactionID { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_5_29683, false)]
        public int ItemGroupSoundsID { get; set; }
    }
}
