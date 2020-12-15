using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.SceneScriptGlobalText, HasIndexInData = false)]
    public class SceneScriptGlobalTextEntry
    {
        public string Name { get; set; }
        public string Script { get; set; }
    }
}
