using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SceneScriptPackage, HasIndexInData = false)]
    public class SceneScriptPackageEntry
    {
        public string Name { get; set; }
    }
}
