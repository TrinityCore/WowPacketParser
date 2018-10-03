using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.VocalUISounds, HasIndexInData = false)]
    public class VocalUISoundsEntry
    {
        public byte VocalUIEnum { get; set; }
        public byte RaceID { get; set; }
        public byte ClassID { get; set; }
        [HotfixArray(2)]
        public uint[] NormalSoundID { get; set; }
    }
}
