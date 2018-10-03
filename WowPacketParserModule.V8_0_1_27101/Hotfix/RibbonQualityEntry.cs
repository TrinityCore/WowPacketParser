using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.RibbonQuality, HasIndexInData = false)]
    public class RibbonQualityEntry
    {
        public byte NumStrips { get; set; }
        public float MaxSampleTimeDelta { get; set; }
        public float AngleThreshold { get; set; }
        public float MinDistancePerSlice { get; set; }
        public uint Flags { get; set; }
    }
}
