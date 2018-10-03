using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.TerrainTypeSounds, HasIndexInData = false)]
    public class TerrainTypeSoundsEntry
    {
        public string Name { get; set; }
    }
}
