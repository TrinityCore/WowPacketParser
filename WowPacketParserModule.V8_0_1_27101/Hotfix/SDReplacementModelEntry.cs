using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SDReplacementModel, HasIndexInData = false)]
    public class SDReplacementModelEntry
    {
        public int SdFileDataID { get; set; }
    }
}
