using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.ChrUpgradeBucket)]
    public class ChrUpgradeBucketEntry
    {
        public uint ID { get; set; }
        public uint TierID { get; set; }
        public uint SpecializationID { get; set; }
    }
}