using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_2_0_23826.Hotfix
{
    [HotfixStructure(DB2Hash.Difficulty, ClientVersionBuild.V7_2_0_23826, HasIndexInData = false)]
    public class DifficultyEntry
    {
        public string Name { get; set; }
        public ushort GroupSizeHealthCurveID { get; set; }
        public ushort GroupSizeDmgCurveID { get; set; }
        public ushort GroupSizeSpellPointsCurveID { get; set; }
        public byte FallbackDifficultyID { get; set; }
        public byte InstanceType { get; set; }
        public byte MinPlayers { get; set; }
        public byte MaxPlayers { get; set; }
        public sbyte OldEnumValue { get; set; }
        public byte Flags { get; set; }
        public byte ToggleDifficultyID { get; set; }
        public byte ItemBonusTreeModID { get; set; }
        public byte OrderIndex { get; set; }
    }
}