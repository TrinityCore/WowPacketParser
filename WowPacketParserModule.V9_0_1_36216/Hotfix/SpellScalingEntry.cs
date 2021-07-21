using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.SpellScaling, HasIndexInData = false)]
    public class SpellScalingEntry
    {
        public int SpellID { get; set; }
        [HotfixVersion(ClientVersionBuild.V9_1_0_39185, true)]
        public int Class { get; set; }
        public uint MinScalingLevel { get; set; }
        public uint MaxScalingLevel { get; set; }
        public short ScalesFromItemLevel { get; set; }
    }
}
