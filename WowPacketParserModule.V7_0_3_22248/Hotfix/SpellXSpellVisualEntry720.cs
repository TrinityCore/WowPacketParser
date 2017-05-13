using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_2
    .Hotfix
{
    [HotfixStructure(DB2Hash.SpellXSpellVisual, ClientVersionBuild.V7_2_0_23826, HasIndexInData = false)]
    public class SpellXSpellVisualEntry
    {
        public uint SpellID { get; set; }
        public uint SpellVisualID { get; set; }
        public uint ID { get; set; }
        public float Chance { get; set; }
        public ushort CasterPlayerConditionID { get; set; }
        public ushort CasterUnitConditionID { get; set; }
        public ushort PlayerConditionID { get; set; }
        public ushort UnitConditionID { get; set; }
        public uint IconFileDataID { get; set; }
        public uint ActiveIconFileDataID { get; set; }
        public byte Flags { get; set; }
        public byte DifficultyID { get; set; }
        public byte Priority { get; set; }
    }
}