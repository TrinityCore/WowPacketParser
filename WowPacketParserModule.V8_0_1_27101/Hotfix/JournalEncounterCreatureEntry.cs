using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.JournalEncounterCreature)]
    public class JournalEncounterCreatureEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }
        public ushort JournalEncounterID { get; set; }
        public uint CreatureDisplayInfoID { get; set; }
        public uint FileDataID { get; set; }
        public byte OrderIndex { get; set; }
        public uint UiModelSceneID { get; set; }
    }
}
