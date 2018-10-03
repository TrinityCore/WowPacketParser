using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.WorldBossLockout, HasIndexInData = false)]
    public class WorldBossLockoutEntry
    {
        public string Name { get; set; }
        public ushort TrackingQuestID { get; set; }
    }
}
