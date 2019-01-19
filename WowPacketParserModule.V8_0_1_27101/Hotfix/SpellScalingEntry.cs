using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellScaling, HasIndexInData = false)]
    public class SpellScalingEntry
    {
        public int SpellID { get; set; }
        public int Class { get; set; }
        public uint MinScalingLevel { get; set; }
        public uint MaxScalingLevel { get; set; }
        public ushort ScalesFromItemLevel { get; set; }
    }
}
