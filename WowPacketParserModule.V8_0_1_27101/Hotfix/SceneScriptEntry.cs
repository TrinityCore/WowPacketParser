using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SceneScript, HasIndexInData = false)]
    public class SceneScriptEntry
    {
        public ushort FirstSceneScriptID { get; set; }
        public ushort NextSceneScriptID { get; set; }
    }
}
