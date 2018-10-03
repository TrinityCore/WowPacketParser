using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BattlemasterList, HasIndexInData = false)]
    public class BattlemasterListEntry
    {
        public string Name { get; set; }
        public string GameType { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public sbyte InstanceType { get; set; }
        public sbyte MinLevel { get; set; }
        public sbyte MaxLevel { get; set; }
        public sbyte RatedPlayers { get; set; }
        public sbyte MinPlayers { get; set; }
        public sbyte MaxPlayers { get; set; }
        public sbyte GroupsAllowed { get; set; }
        public sbyte MaxGroupSize { get; set; }
        public short HolidayWorldState { get; set; }
        public sbyte Flags { get; set; }
        public int IconFileDataID { get; set; }
        public short RequiredPlayerConditionID { get; set; }
        [HotfixArray(16)]
        public short[] MapID { get; set; }
    }
}
