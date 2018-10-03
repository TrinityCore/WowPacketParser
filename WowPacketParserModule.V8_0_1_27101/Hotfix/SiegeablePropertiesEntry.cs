using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SiegeableProperties, HasIndexInData = false)]
    public class SiegeablePropertiesEntry
    {
        public uint Health { get; set; }
        public int DamageSpellVisualKitID { get; set; }
        public int HealingSpellVisualKitID { get; set; }
        public uint Flags { get; set; }
    }
}
