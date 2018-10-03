using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GMSurveySurveys, HasIndexInData = false)]
    public class GMSurveySurveysEntry
    {
        [HotfixArray(15)]
        public byte[] Q { get; set; }
    }
}
