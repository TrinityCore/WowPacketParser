using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ZoneStory, HasIndexInData = false)]
    public class ZoneStoryEntry
    {
        public byte PlayerFactionGroupID { get; set; }
        public uint DisplayAchievementID { get; set; }
        public uint DisplayUIMapID { get; set; }
        public int PlayerUIMapID { get; set; }
    }
}
