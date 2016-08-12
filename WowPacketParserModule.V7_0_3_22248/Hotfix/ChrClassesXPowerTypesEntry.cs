using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ChrClassesXPowerTypes, HasIndexInData = false)]
    public class ChrClassesXPowerTypesEntry
    {
        public byte ClassID { get; set; }
        public byte PowerType { get; set; }
    }
}