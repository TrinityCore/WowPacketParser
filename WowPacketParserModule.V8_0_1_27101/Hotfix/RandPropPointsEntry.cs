using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.RandPropPoints, HasIndexInData = false)]
    public class RandPropPointsEntry
    {
        public int DamageReplaceStat { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_2_0_30898, false)]
        public int DamageSecondary { get; set; }
        [HotfixArray(5)]
        public uint[] Epic { get; set; }
        [HotfixArray(5)]
        public uint[] Superior { get; set; }
        [HotfixArray(5)]
        public uint[] Good { get; set; }
    }
}
