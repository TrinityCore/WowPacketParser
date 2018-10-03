using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.LfgDungeonsGroupingMap, HasIndexInData = false)]
    public class LfgDungeonsGroupingMapEntry
    {
        public ushort RandomLfgDungeonsID { get; set; }
        public byte GroupID { get; set; }
        public ushort LfgDungeonsID { get; set; }
    }
}
