using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.NameGen, HasIndexInData = false)]
    public class NameGenEntry
    {
        public string Name { get; set; }
        public byte RaceID { get; set; }
        public byte Sex { get; set; }
    }
}
