using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemContextPickerEntry, HasIndexInData = false)]
    public class ItemContextPickerEntryEntry
    {
        public byte ItemCreationContext { get; set; }
        public byte OrderIndex { get; set; }
        public int PVal { get; set; }
        public uint Flags { get; set; }
        public uint PlayerConditionID { get; set; }
        public uint ItemContextPickerID { get; set; }
    }
}
