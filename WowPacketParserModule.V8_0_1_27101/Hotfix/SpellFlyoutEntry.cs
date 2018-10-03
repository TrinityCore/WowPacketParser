using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellFlyout, HasIndexInData = false)]
    public class SpellFlyoutEntry
    {
        public ulong RaceMask { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Flags { get; set; }
        public int ClassMask { get; set; }
        public int SpellIconFileID { get; set; }
    }
}
