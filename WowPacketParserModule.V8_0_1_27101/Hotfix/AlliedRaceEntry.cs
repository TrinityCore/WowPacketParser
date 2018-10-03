using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AlliedRace)]
    public class AlliedRaceEntry
    {
        public int ID { get; set; }
        public int RaceID { get; set; }
        public uint BannerColor { get; set; }
        public int CrestTextureID { get; set; }
        public int ModelBackgroundTextureID { get; set; }
        public int MaleCreatureDisplayID { get; set; }
        public int FemaleCreatureDisplayID { get; set; }
        public int UiUnlockAchievementID { get; set; }
    }
}
