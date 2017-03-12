namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGMSurveyQuestion
    {
        public int QuestionID;
        public byte Answer;
        public string AnswerComment;
    }
}
