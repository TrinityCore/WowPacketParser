using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGMSurveyQuestion
    {
        public int QuestionID;
        public byte Answer;
        public string AnswerComment;
    }
}
