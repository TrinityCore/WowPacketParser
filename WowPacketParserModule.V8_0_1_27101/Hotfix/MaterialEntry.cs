using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Material, HasIndexInData = false)]
    public class MaterialEntry
    {
        public byte Flags { get; set; }
        public uint FoleySoundID { get; set; }
        public uint SheatheSoundID { get; set; }
        public uint UnsheatheSoundID { get; set; }
    }
}
