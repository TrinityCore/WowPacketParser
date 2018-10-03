using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AnimReplacementSet, HasIndexInData = false)]
    public class AnimReplacementSetEntry
    {
        public byte ExecOrder { get; set; }
    }
}
