using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SoundFilterElem, HasIndexInData = false)]
    public class SoundFilterElemEntry
    {
        [HotfixArray(9)]
        public float[] Params { get; set; }
        public sbyte FilterType { get; set; }
        public sbyte SoundFilterID { get; set; }
    }
}
