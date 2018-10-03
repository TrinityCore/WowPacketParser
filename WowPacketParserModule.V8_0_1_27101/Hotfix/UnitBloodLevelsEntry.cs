using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UnitBloodLevels, HasIndexInData = false)]
    public class UnitBloodLevelsEntry
    {
        [HotfixArray(3)]
        public byte[] Violencelevel { get; set; }
    }
}
