using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SkillLine, HasIndexInData = false)]
    public class SkillLineEntry
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string AlternateVerb { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, true)]
        public ushort SpellIconID { get; set; }
        public ushort Flags { get; set; }
        public byte CategoryID { get; set; }
        public byte CanLink { get; set; }
        [HotfixVersion(ClientVersionBuild.V7_2_0_23826, false)]
        public uint IconFileDataID { get; set; }
        public uint ParentSkillLineID { get; set; }
    }
}