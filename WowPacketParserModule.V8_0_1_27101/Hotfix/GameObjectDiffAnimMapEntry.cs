using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GameObjectDiffAnimMap, HasIndexInData = false)]
    public class GameObjectDiffAnimMapEntry
    {
        public byte DifficultyID { get; set; }
        public byte Animation { get; set; }
        public ushort AttachmentDisplayID { get; set; }
        public byte GameObjectDiffAnimID { get; set; }
    }
}
