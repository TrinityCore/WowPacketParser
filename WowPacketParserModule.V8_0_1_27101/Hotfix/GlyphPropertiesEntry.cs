using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GlyphProperties, HasIndexInData = false)]
    public class GlyphPropertiesEntry
    {
        public uint SpellID { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_2_0_30898, true)]
        public ushort SpellIconID { get; set; }
        public byte GlyphType { get; set; }
        public byte GlyphExclusiveCategoryID { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_2_0_30898, false)]
        public int SpellIconFileDataID { get; set; }
    }
}
