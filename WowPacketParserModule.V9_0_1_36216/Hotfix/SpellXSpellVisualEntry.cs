using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.SpellXSpellVisual, ClientVersionBuild.V9_0_1_36216, ClientVersionBuild.V9_1_0_39185)]
    public class SpellXSpellVisualEntry
    {
        public uint ID { get; set; }
        public byte DifficultyID { get; set; }
        public uint SpellVisualID { get; set; }
        public float Probability { get; set; }
        public byte Priority { get; set; }
        public int SpellIconFileID { get; set; }
        public int ActiveIconFileID { get; set; }
        public ushort ViewerUnitConditionID { get; set; }
        public uint ViewerPlayerConditionID { get; set; }
        public ushort CasterUnitConditionID { get; set; }
        public uint CasterPlayerConditionID { get; set; }
        public uint SpellID { get; set; }
    }
}
