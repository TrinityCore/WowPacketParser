using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AlliedRaceRacialAbility, HasIndexInData = false)]
    public class AlliedRaceRacialAbilityEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int IconFileDataID { get; set; }
        public byte OrderIndex { get; set; }
        public int AlliedRaceID { get; set; }
    }
}
