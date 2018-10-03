using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BoneWindModifiers, HasIndexInData = false)]
    public class BoneWindModifiersEntry
    {
        [HotfixArray(3)]
        public float[] Multiplier { get; set; }
        public float PhaseMultiplier { get; set; }
    }
}
