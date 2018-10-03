using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Startup_Strings, HasIndexInData = false)]
    public class Startup_StringsEntry
    {
        public string Name { get; set; }
        public string Message { get; set; }
    }
}
