using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.LoadingScreens, HasIndexInData = false)]
    public class LoadingScreensEntry
    {
        public int NarrowScreenFileDataID { get; set; }
        public int WideScreenFileDataID { get; set; }
        public int WideScreen169FileDataID { get; set; }
    }
}
