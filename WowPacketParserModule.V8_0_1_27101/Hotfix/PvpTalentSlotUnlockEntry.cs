using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PvpTalentSlotUnlock, HasIndexInData = false)]
    public class PvpTalentSlotUnlockEntry
    {
        public sbyte Slot { get; set; }
        public int LevelRequired { get; set; }
        public int DeathKnightLevelRequired { get; set; }
        public int DemonHunterLevelRequired { get; set; }
    }
}
