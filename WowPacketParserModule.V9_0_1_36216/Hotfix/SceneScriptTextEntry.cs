using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.SceneScriptText, HasIndexInData = false)]
    public class SceneScriptTextEntry
    {
        public string Name { get; set; }
        public string Script { get; set; }
    }
}
