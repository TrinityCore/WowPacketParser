using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ClientSceneEffect, HasIndexInData = false)]
    public class ClientSceneEffectEntry
    {
        public int SceneScriptPackageID { get; set; }
    }
}
