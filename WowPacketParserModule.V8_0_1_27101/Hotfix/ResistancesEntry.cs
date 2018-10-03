using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Resistances, HasIndexInData = false)]
    public class ResistancesEntry
    {
        public string Name { get; set; }
        public byte Flags { get; set; }
        public uint FizzleSoundID { get; set; }
    }
}
