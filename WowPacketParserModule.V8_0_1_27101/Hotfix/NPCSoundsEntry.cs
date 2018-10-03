using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.NPCSounds, HasIndexInData = false)]
    public class NPCSoundsEntry
    {
        [HotfixArray(4)]
        public uint[] SoundID { get; set; }
    }
}
