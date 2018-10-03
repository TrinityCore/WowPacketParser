using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SourceInfo, HasIndexInData = false)]
    public class SourceInfoEntry
    {
        public string SourceText { get; set; }
        public sbyte PvpFaction { get; set; }
        public sbyte SourceTypeEnum { get; set; }
        public uint SpellID { get; set; }
    }
}
