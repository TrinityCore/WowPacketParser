using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Scenario, HasIndexInData = false)]
    public class ScenarioEntry
    {
        public string Name { get; set; }
        public ushort AreaTableID { get; set; }
        public byte Type { get; set; }
        public byte Flags { get; set; }
        public uint UiTextureKitID { get; set; }
    }
}
