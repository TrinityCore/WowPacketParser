using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellScaling, HasIndexInData = false)]
    public class SpellScalingEntry
    {
        public uint SpellID { get; set; }
        public ushort ScalesFromItemLevel { get; set; }
        public int ScalingClass { get; set; }
        public uint MinScalingLevel { get; set; }
        public uint MaxScalingLevel { get; set; }
    }
}