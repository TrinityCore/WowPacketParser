using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.CreatureFamily, HasIndexInData = false)]
    public class CreatureFamilyEntry
    {
        public float MinScale { get; set; }
        public float MaxScale { get; set; }
        public string Name { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, true)]
        public string IconFile { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, false)]
        public uint IconFileDataID { get; set; }
        [HotfixArray(2)]
        public ushort[] SkillLine { get; set; }
        public ushort PetFoodMask { get; set; }
        public byte MinScaleLevel { get; set; }
        public byte MaxScaleLevel { get; set; }
        public byte PetTalentType { get; set; }
    }
}