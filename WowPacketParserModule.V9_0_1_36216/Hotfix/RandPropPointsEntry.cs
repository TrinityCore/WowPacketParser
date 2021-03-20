using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.RandPropPoints, HasIndexInData = false)]
    public class RandPropPointsEntry
    {
        [HotfixVersion(ClientVersionBuild.V9_0_5_37503, false)]
        public float DamageReplaceStatF { get; set; }
        [HotfixVersion(ClientVersionBuild.V9_0_5_37503, false)]
        public float DamageSecondaryF { get; set; }
        public int DamageReplaceStat { get; set; }
        public int DamageSecondary { get; set; }
        [HotfixVersion(ClientVersionBuild.V9_0_5_37503, false)]
        [HotfixArray(5)]
        public float[] EpicF { get; set; }
        [HotfixVersion(ClientVersionBuild.V9_0_5_37503, false)]
        [HotfixArray(5)]
        public float[] SuperiorF { get; set; }
        [HotfixVersion(ClientVersionBuild.V9_0_5_37503, false)]
        [HotfixArray(5)]
        public float[] GoodF { get; set; }
        [HotfixArray(5)]
        public uint[] Epic { get; set; }
        [HotfixArray(5)]
        public uint[] Superior { get; set; }
        [HotfixArray(5)]
        public uint[] Good { get; set; }
    }
}
