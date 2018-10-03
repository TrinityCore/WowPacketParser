using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Hotfixes, HasIndexInData = false)]
    public class HotfixesEntry
    {
        public string Tablename { get; set; }
        public int ObjectID { get; set; }
        public int Flags { get; set; }
    }
}
