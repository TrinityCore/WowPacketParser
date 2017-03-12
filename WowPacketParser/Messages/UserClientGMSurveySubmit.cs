using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGMSurveySubmit
    {
        public List<ClientGMSurveyQuestion> SurveyQuestion;
        public int SurveyID;
        public string Comment;
    }
}
