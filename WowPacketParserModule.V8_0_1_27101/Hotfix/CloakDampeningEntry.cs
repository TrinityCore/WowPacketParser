using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CloakDampening, HasIndexInData = false)]
    public class CloakDampeningEntry
    {
        public float TabardAngle { get; set; }
        public float TabardDampening { get; set; }
        public float ExpectedWeaponSize { get; set; }
        [HotfixArray(5)]
        public float[] Angle { get; set; }
        [HotfixArray(5)]
        public float[] Dampening { get; set; }
        [HotfixArray(2)]
        public float[] TailAngle { get; set; }
        [HotfixArray(2)]
        public float[] TailDampening { get; set; }
    }
}
