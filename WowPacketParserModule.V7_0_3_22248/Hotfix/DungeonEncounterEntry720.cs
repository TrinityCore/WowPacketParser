using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_2_0_23826.Hotfix
{
    [HotfixStructure(DB2Hash.DungeonEncounter, ClientVersionBuild.V7_2_0_23826)]
    public class DungeonEncounterEntry
    {
        public string Name { get; set; }
        public uint CreatureDisplayID { get; set; }
        public ushort MapID { get; set; }
        public byte DifficultyID { get; set; }
        public byte Bit { get; set; }
        public byte Flags { get; set; }
        public uint ID { get; set; }
        public uint OrderIndex { get; set; }
        public uint TextureFileDataID { get; set; }
    }
}