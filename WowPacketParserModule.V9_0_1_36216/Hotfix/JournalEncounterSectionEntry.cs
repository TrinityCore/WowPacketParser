using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.JournalEncounterSection, HasIndexInData = false)]
    public class JournalEncounterSectionEntry
    {
        public string Title { get; set; }
        public string BodyText { get; set; }
        public ushort JournalEncounterID { get; set; }
        public byte OrderIndex { get; set; }
        public ushort ParentSectionID { get; set; }
        public ushort FirstChildSectionID { get; set; }
        public ushort NextSiblingSectionID { get; set; }
        public byte Type { get; set; }
        public uint IconCreatureDisplayInfoID { get; set; }
        public int UiModelSceneID { get; set; }
        public int SpellID { get; set; }
        public int IconFileDataID { get; set; }
        public ushort Flags { get; set; }
        public ushort IconFlags { get; set; }
        public sbyte DifficultyMask { get; set; }
    }
}
