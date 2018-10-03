using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.LockType)]
    public class LockTypeEntry
    {
        public string Name { get; set; }
        public string ResourceName { get; set; }
        public string Verb { get; set; }
        public string CursorName { get; set; }
        public int ID { get; set; }
    }
}
