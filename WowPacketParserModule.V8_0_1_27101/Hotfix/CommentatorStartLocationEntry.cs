using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CommentatorStartLocation, HasIndexInData = false)]
    public class CommentatorStartLocationEntry
    {
        [HotfixArray(3)]
        public float[] Pos { get; set; }
        public int MapID { get; set; }
    }
}
