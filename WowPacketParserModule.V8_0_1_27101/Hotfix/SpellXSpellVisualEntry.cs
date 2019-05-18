using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellXSpellVisual)]
    public class SpellXSpellVisualEntry
    {
        public int ID { get; set; }
        public byte DifficultyID { get; set; }
        public uint SpellVisualID { get; set; }
        public float Probability { get; set; }
        public byte Flags { get; set; }
        public byte Priority { get; set; }
        public int SpellIconFileID { get; set; }
        public int ActiveIconFileID { get; set; }
        public ushort ViewerUnitConditionID { get; set; }
        public uint ViewerPlayerConditionID { get; set; }
        public ushort CasterUnitConditionID { get; set; }
        public uint CasterPlayerConditionID { get; set; }
        public int SpellID { get; set; }
    }
}
