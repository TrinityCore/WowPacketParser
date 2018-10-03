using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemVisuals, HasIndexInData = false)]
    public class ItemVisualsEntry
    {
        [HotfixArray(5)]
        public int[] ModelFileID { get; set; }
    }
}
