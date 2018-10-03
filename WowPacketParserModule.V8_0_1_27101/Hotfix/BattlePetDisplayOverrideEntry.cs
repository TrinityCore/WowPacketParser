using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetDisplayOverride, HasIndexInData = false)]
    public class BattlePetDisplayOverrideEntry
    {
        public uint BattlePetSpeciesID { get; set; }
        public uint PlayerConditionID { get; set; }
        public uint CreatureDisplayInfoID { get; set; }
        public byte PriorityCategory { get; set; }
    }
}
