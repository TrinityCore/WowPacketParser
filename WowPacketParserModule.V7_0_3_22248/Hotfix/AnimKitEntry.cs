using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.AnimKit, HasIndexInData = false)]
    public class AnimKitEntry
    {
        public uint OneShotDuration { get; set; }
        public ushort OneShotStopAnimKitID { get; set; }
        public ushort LowDefAnimKitID { get; set; }
    }
}