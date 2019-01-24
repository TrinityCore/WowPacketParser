using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Difficulty, HasIndexInData = false)]
    public class DifficultyEntry
    {
        public string Name { get; set; }
        public byte InstanceType { get; set; }
        public byte OrderIndex { get; set; }
        public sbyte OldEnumValue { get; set; }
        public byte FallbackDifficultyID { get; set; }
        public byte MinPlayers { get; set; }
        public byte MaxPlayers { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, true)]
        public byte Flags { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, false)]
        public ushort flags { get; set; }
        public byte ItemContext { get; set; }
        public byte ToggleDifficultyID { get; set; }
        public ushort GroupSizeHealthCurveID { get; set; }
        public ushort GroupSizeDmgCurveID { get; set; }
        public ushort GroupSizeSpellPointsCurveID { get; set; }
    }
}
