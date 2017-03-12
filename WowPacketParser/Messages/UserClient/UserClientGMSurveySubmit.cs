using System.Collections.Generic;
using WowPacketParser.Messages.Client;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientGMSurveySubmit
    {
        public List<ClientGMSurveyQuestion> SurveyQuestion;
        public int SurveyID;
        public string Comment;
    }
}
