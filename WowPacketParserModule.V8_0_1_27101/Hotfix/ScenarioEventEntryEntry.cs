using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ScenarioEventEntry, HasIndexInData = false)]
    public class ScenarioEventEntryEntry
    {
        public byte TriggerType { get; set; }
        public uint TriggerAsset { get; set; }
    }
}
