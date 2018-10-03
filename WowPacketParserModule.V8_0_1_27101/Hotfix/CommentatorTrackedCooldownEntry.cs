using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CommentatorTrackedCooldown, HasIndexInData = false)]
    public class CommentatorTrackedCooldownEntry
    {
        public int SpellID { get; set; }
        public byte Priority { get; set; }
        public sbyte Flags { get; set; }
        public ushort ChrSpecID { get; set; }
    }
}
