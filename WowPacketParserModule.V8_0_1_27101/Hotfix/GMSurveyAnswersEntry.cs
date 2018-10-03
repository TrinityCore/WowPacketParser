using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GMSurveyAnswers, HasIndexInData = false)]
    public class GMSurveyAnswersEntry
    {
        public string Answer { get; set; }
        public byte SortIndex { get; set; }
        public uint GMSurveyQuestionID { get; set; }
    }
}
