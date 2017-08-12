using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SceneScript, HasIndexInData = false)]
    public class SceneScriptEntry
    {
        public string Name { get; set; }
        public string Script { get; set; }
        public ushort PrevScriptId { get; set; }
        public ushort NextScriptId { get; set; }
    }
}
