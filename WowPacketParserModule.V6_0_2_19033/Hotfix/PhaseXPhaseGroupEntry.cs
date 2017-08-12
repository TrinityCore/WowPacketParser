using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.PhaseXPhaseGroup)]
    public class PhaseXPhaseGroupEntry
    {
        public uint ID { get; set; }
        public uint PhaseID { get; set; }
        public uint PhaseGroupID { get; set; }
    }
}