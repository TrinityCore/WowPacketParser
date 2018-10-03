using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GMSurveyCurrentSurvey, HasIndexInData = false)]
    public class GMSurveyCurrentSurveyEntry
    {
        public byte GmsurveyID { get; set; }
    }
}
