using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SceneScriptText, HasIndexInData = false)]
    public class SceneScriptTextEntry
    {
        public string Name { get; set; }
        public string Script { get; set; }
    }
}
