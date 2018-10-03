using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AnimKitBoneSet, HasIndexInData = false)]
    public class AnimKitBoneSetEntry
    {
        public string Name { get; set; }
        public sbyte BoneDataID { get; set; }
        public sbyte ParentAnimKitBoneSetID { get; set; }
        public byte ExtraBoneCount { get; set; }
        public sbyte AltAnimKitBoneSetID { get; set; }
    }
}
