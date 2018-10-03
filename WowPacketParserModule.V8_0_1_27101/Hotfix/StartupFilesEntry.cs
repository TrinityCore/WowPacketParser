using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.StartupFiles, HasIndexInData = false)]
    public class StartupFilesEntry
    {
        public int FileDataID { get; set; }
        public int Locale { get; set; }
        public int BytesRequired { get; set; }
    }
}
