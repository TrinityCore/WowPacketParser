using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SceneScriptPackageMember, HasIndexInData = false)]
    public class SceneScriptPackageMemberEntry
    {
        public ushort SceneScriptPackageID { get; set; }
        public ushort SceneScriptID { get; set; }
        public ushort ChildSceneScriptPackageID { get; set; }
        public byte OrderIndex { get; set; }
    }
}
