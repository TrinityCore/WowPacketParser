using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.DungeonEncounter)]
    public class DungeonEncounterEntry
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public short MapID { get; set; }
        public sbyte DifficultyID { get; set; }
        public int OrderIndex { get; set; }
        public sbyte Bit { get; set; }
        public int CreatureDisplayID { get; set; }
        public byte Flags { get; set; }
        public int SpellIconFileID { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, false)]
        public int Unknown1 { get; set; }
    }
}
