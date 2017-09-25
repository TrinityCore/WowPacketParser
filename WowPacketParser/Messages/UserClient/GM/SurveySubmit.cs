using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Messages.Client;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.GM
{
    public unsafe struct SurveySubmit
    {
        public List<ClientGMSurveyQuestion> SurveyQuestion;
        public int SurveyID;
        public string Comment;

        [Parser(Opcode.CMSG_GM_SURVEY_SUBMIT, ClientVersionBuild.Zero, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGMSurveySubmit(Packet packet)
        {
            var count = packet.ReadUInt32("Survey Question Count");
            for (var i = 0; i < count; ++i)
            {
                var gmsurveyid = packet.ReadUInt32("GM Survey Id", i);
                if (gmsurveyid == 0)
                    break;
                packet.ReadByte("Question Number", i);
                packet.ReadCString("Answer", i);
            }

            packet.ReadCString("Comment");
        }

        [Parser(Opcode.CMSG_GM_SURVEY_SUBMIT, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGMSurveySubmit602(Packet packet)
        {
            packet.ReadInt32("SurveyID");

            var questionCount = packet.ReadBits("SurveyQuestionCount", 4);
            var commentLenght = packet.ReadBits("CommentLength", 11);

            packet.ResetBitReader();

            for (var i = 0; i < questionCount; ++i)
                readClientGMSurveyQuestion(packet, "SurveyQuestion", i);

            packet.ReadWoWString("Comment", commentLenght);
        }
        
        private static void readClientGMSurveyQuestion(Packet packet, params object[] idx)
        {
            packet.ReadInt32("QuestionID", idx);
            packet.ReadByte("Answer", idx);

            packet.ResetBitReader();
            var length = packet.ReadBits("AnswerCommentLength", 11, idx);
            packet.ResetBitReader();

            packet.ReadWoWString("AnswerComment", length, idx);
        }
    }
}
