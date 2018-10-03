using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AnimationData, HasIndexInData = false)]
    public class AnimationDataEntry
    {
        public ushort BehaviorID { get; set; }
        public byte BehaviorTier { get; set; }
        public int Fallback { get; set; }
        [HotfixArray(2)]
        public uint[] Flags { get; set; }
    }
}
