using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.Languages, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class LanguagesEntry
    {
        public string Name { get; set; }
    }
}
