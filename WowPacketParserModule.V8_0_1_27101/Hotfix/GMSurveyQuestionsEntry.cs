using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GMSurveyQuestions, HasIndexInData = false)]
    public class GMSurveyQuestionsEntry
    {
        public string Question { get; set; }
    }
}
