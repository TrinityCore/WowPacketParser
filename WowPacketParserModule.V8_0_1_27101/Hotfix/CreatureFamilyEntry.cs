using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureFamily, HasIndexInData = false)]
    public class CreatureFamilyEntry
    {
        public string Name { get; set; }
        public float MinScale { get; set; }
        public sbyte MinScaleLevel { get; set; }
        public float MaxScale { get; set; }
        public sbyte MaxScaleLevel { get; set; }
        public short PetFoodMask { get; set; }
        public sbyte PetTalentType { get; set; }
        public int IconFileID { get; set; }
        [HotfixArray(2)]
        public short[] SkillLine { get; set; }
    }
}
