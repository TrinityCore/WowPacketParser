using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GroundEffectDoodad, HasIndexInData = false)]
    public class GroundEffectDoodadEntry
    {
        public int ModelFileID { get; set; }
        public byte Flags { get; set; }
        public float Animscale { get; set; }
        public float PushScale { get; set; }
    }
}
