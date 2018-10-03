using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemGroupSounds, HasIndexInData = false)]
    public class ItemGroupSoundsEntry
    {
        [HotfixArray(4)]
        public uint[] Sound { get; set; }
    }
}
