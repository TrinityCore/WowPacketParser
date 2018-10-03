using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AnimKit, HasIndexInData = false)]
    public class AnimKitEntry
    {
        public uint OneShotDuration { get; set; }
        public ushort OneShotStopAnimKitID { get; set; }
        public ushort LowDefAnimKitID { get; set; }
    }
}
