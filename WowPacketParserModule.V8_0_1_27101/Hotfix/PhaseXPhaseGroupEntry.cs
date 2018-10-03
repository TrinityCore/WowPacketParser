using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PhaseXPhaseGroup, HasIndexInData = false)]
    public class PhaseXPhaseGroupEntry
    {
        public ushort PhaseID { get; set; }
        public ushort PhaseGroupID { get; set; }
    }
}
