using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PrestigeLevelInfo, HasIndexInData = false)]
    public class PrestigeLevelInfoEntry
    {
        public string Name { get; set; }
        public int PrestigeLevel { get; set; }
        public int BadgeTextureFileDataID { get; set; }
        public byte Flags { get; set; }
        public int AwardedAchievementID { get; set; }
    }
}
