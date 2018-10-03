using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.EnvironmentalDamage, HasIndexInData = false)]
    public class EnvironmentalDamageEntry
    {
        public byte EnumID { get; set; }
        public ushort VisualKitID { get; set; }
    }
}
