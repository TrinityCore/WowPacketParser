using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.LoadingScreenTaxiSplines, HasIndexInData = false)]
    public class LoadingScreenTaxiSplinesEntry
    {
        public ushort PathID { get; set; }
        public byte LegIndex { get; set; }
        public ushort LoadingScreenID { get; set; }
        [HotfixArray(10)]
        public float[] LocX { get; set; }
        [HotfixArray(10)]
        public float[] LocY { get; set; }
    }
}
