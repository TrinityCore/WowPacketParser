using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.JournalEncounter)]
    public class JournalEncounterEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [HotfixArray(2, true)]
        public float []Map { get; set; }
        public uint ID { get; set; }
        public ushort JournalInstanceID { get; set; }
        public ushort DungeonEncounterID { get; set; }
        public uint OrderIndex { get; set; }
        public ushort FirstSectionID { get; set; }
        public ushort UiMapID { get; set; }
        public uint MapDisplayConditionID { get; set; }
        public byte Flags { get; set; }
        public sbyte DifficultyMask { get; set; }
    }
}
