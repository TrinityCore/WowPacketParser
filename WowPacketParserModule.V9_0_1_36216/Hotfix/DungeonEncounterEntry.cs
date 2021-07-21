using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.DungeonEncounter, ClientVersionBuild.V9_0_1_36216, ClientVersionBuild.V9_1_0_39185)]
    public class DungeonEncounterEntry
    {
        public string Name { get; set; }
        public uint ID { get; set; }
        public short MapID { get; set; }
        public int DifficultyID { get; set; }
        public int OrderIndex { get; set; }
        public int CompleteWorldStateID { get; set; }
        public sbyte Bit { get; set; }
        public int CreatureDisplayID { get; set; }
        public byte Flags { get; set; }
        public int SpellIconFileID { get; set; }
        public int Faction { get; set; }
    }
}
