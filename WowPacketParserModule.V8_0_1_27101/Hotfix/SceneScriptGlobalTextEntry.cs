using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SceneScriptGlobalText, HasIndexInData = false)]
    public class SceneScriptGlobalTextEntry
    {
        public string Name { get; set; }
        public string Script { get; set; }
    }
}
