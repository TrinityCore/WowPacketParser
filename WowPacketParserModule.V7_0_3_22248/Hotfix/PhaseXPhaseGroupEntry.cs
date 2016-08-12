using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.PhaseXPhaseGroup, HasIndexInData = false)]
    public class PhaseXPhaseGroupEntry
    {
        public ushort PhaseID { get; set; }
        public ushort PhaseGroupID { get; set; }
    }
}