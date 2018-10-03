using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GroupFinderActivity, HasIndexInData = false)]
    public class GroupFinderActivityEntry
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public byte GroupFinderCategoryID { get; set; }
        public sbyte OrderIndex { get; set; }
        public byte GroupFinderActivityGrpID { get; set; }
        public byte MinLevel { get; set; }
        public byte MaxLevelSuggestion { get; set; }
        public uint Flags { get; set; }
        public ushort MinGearLevelSuggestion { get; set; }
        public ushort MapID { get; set; }
        public byte DifficultyID { get; set; }
        public ushort AreaID { get; set; }
        public byte MaxPlayers { get; set; }
        public byte DisplayType { get; set; }
    }
}
