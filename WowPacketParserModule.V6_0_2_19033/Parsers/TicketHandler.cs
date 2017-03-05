using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class TicketHandler
    {
        [Parser(Opcode.CMSG_GM_TICKET_GET_TICKET)]
        [Parser(Opcode.CMSG_GM_TICKET_GET_CASE_STATUS)]
        [Parser(Opcode.CMSG_GM_TICKET_GET_SYSTEM_STATUS)]
        [Parser(Opcode.SMSG_GM_TICKET_RESPONSE_ERROR)]
        public static void HandleGMTicketZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_GM_TICKET_CASE_STATUS)]
        public static void HandleGMTicketCaseStatus(Packet packet)
        {
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_2_20444))
            {
                packet.Translator.ReadTime("OldestTicketTime");
                packet.Translator.ReadTime("UpdateTime");
            }

            var int24 = packet.Translator.ReadInt32("CasesCount");
            for (int i = 0; i < int24; i++)
            {
                packet.Translator.ReadInt32("CaseID", i);
                packet.Translator.ReadInt32("CaseOpened", i);
                packet.Translator.ReadInt32("CaseStatus", i);
                packet.Translator.ReadInt16("CfgRealmID", i);
                packet.Translator.ReadInt64("CharacterID", i);
                packet.Translator.ReadInt32("WaitTimeOverrideMinutes", i);

                packet.Translator.ResetBitReader();
                var bits12 = packet.Translator.ReadBits(11);
                var bits262 = packet.Translator.ReadBits(10);

                packet.Translator.ReadWoWString("Url", bits12, i);
                packet.Translator.ReadWoWString("WaitTimeOverrideMessage", bits262, i);
            }
        }

        [Parser(Opcode.SMSG_GM_TICKET_SYSTEM_STATUS)]
        public static void HandleGMTicketSystemStatus(Packet packet)
        {
            packet.Translator.ReadInt32("Status");
        }

        [Parser(Opcode.SMSG_GM_TICKET_GET_TICKET_RESPONSE)]
        public static void HandleGMTicketGetTicketResponse(Packet packet)
        {
            packet.Translator.ReadInt32("Result");

            var hasInfo = packet.Translator.ReadBit("HasInfo");

            // ClientGMTicketInfo
            if (hasInfo)
            {
                packet.Translator.ReadInt32("TicketID");
                packet.Translator.ReadByte("Category");
                packet.Translator.ReadTime("TicketOpenTime");
                packet.Translator.ReadTime("OldestTicketTime");
                packet.Translator.ReadTime("UpdateTime");
                packet.Translator.ReadByte("AssignedToGM");
                packet.Translator.ReadByte("OpenedByGM");
                packet.Translator.ReadInt32("WaitTimeOverrideMinutes");

                packet.Translator.ResetBitReader();

                var bits1 = packet.Translator.ReadBits(11);
                var bits2022 = packet.Translator.ReadBits(10);

                packet.Translator.ReadWoWString("TicketDescription", bits1);
                packet.Translator.ReadWoWString("WaitTimeOverrideMessage", bits2022);
            }
        }

        public static void ReadComplaintOffender(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadPackedGuid128("PlayerGuid", indexes);
            packet.Translator.ReadInt32("RealmAddress", indexes);
            packet.Translator.ReadInt32("TimeSinceOffence", indexes);
        }

        public static void ReadComplaintChat(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("Command");
            packet.Translator.ReadInt32("ChannelID");

            packet.Translator.ResetBitReader();

            var len = packet.Translator.ReadBits(12);
            packet.Translator.ReadWoWString("MessageLog", len);
        }

        [Parser(Opcode.CMSG_COMPLAINT)]
        public static void HandleComplain(Packet packet)
        {
            var result = packet.Translator.ReadByte("ComplaintType");

            ReadComplaintOffender(packet, "Offender");

            switch (result)
            {
                case 0: // Mail
                    packet.Translator.ReadInt32("MailID");
                    break;
                case 1: // Chat
                    ReadComplaintChat(packet, "Chat");
                    break;
                case 2: // Calendar
                    // Order guessed
                    packet.Translator.ReadInt64("EventGuid");
                    packet.Translator.ReadInt64("InviteGuid");
                    break;
            }
        }

        [Parser(Opcode.CMSG_GM_TICKET_ACKNOWLEDGE_SURVEY)]
        public static void HandleGMTicketAcknowledgeSurvey(Packet packet)
        {
            packet.Translator.ReadInt32("CaseID");
        }

        [Parser(Opcode.CMSG_GM_TICKET_CREATE)]
        public static void HandleGMTicketCreate(Packet packet)
        {
            packet.Translator.ReadInt32<MapId>("Map");
            packet.Translator.ReadVector3("Pos");
            packet.Translator.ReadByte("Flags");

            var descriptionLength = packet.Translator.ReadBits("DescriptionLength", 11);
            packet.Translator.ResetBitReader();
            packet.Translator.ReadWoWString("Description", descriptionLength);

            packet.Translator.ReadBit("NeedResponse");
            packet.Translator.ReadBit("NeedMoreHelp");
            packet.Translator.ResetBitReader();

            var dataLength = packet.Translator.ReadInt32("DataLength");

            if (dataLength > 0)
            {
                var textCount = packet.Translator.ReadByte("TextCount");

                for (int i = 0; i < textCount /* 60 */; ++i)
                    packet.AddValue("Sent", (packet.Time - packet.Translator.ReadTime()).ToFormattedString(), i);

                var decompCount = packet.Translator.ReadInt32();
                var pkt = packet.Inflate(decompCount);
                pkt.Translator.ReadCString("Text");
                pkt.ClosePacket(false);
            }
        }

        [Parser(Opcode.CMSG_GM_TICKET_UPDATE_TEXT)]
        public static void HandleGMTicketUpdatetext(Packet packet)
        {
            var length = packet.Translator.ReadBits("DescriptionLength", 11);
            packet.Translator.ResetBitReader();

            packet.Translator.ReadWoWString("Description", length);
        }

        public static void ReadClientGMSurveyQuestion(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("QuestionID", idx);
            packet.Translator.ReadByte("Answer", idx);

            packet.Translator.ResetBitReader();
            var length = packet.Translator.ReadBits("AnswerCommentLength", 11, idx);
            packet.Translator.ResetBitReader();

            packet.Translator.ReadWoWString("AnswerComment", length, idx);
        }

        [Parser(Opcode.CMSG_GM_SURVEY_SUBMIT)]
        public static void HandleGMSurveySubmit(Packet packet)
        {
            packet.Translator.ReadInt32("SurveyID");

            var questionCount = packet.Translator.ReadBits("SurveyQuestionCount", 4);
            var commentLenght = packet.Translator.ReadBits("CommentLength", 11);

            packet.Translator.ResetBitReader();

            for (var i = 0; i < questionCount; ++i)
                ReadClientGMSurveyQuestion(packet, "SurveyQuestion", i);

            packet.Translator.ReadWoWString("Comment", commentLenght);
        }

        public static void ReadCliSupportTicketHeader(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32<MapId>("MapID", idx);
            packet.Translator.ReadVector3("Position", idx);
            packet.Translator.ReadSingle("Facing", idx);
        }

        [Parser(Opcode.CMSG_SUPPORT_TICKET_SUBMIT_SUGGESTION)]
        [Parser(Opcode.CMSG_SUPPORT_TICKET_SUBMIT_BUG)]
        public static void HandleSubmitBug(Packet packet)
        {
            ReadCliSupportTicketHeader(packet, "Header");

            var noteLength = packet.Translator.ReadBits("NoteLength", 10);
            packet.Translator.ResetBitReader();
            packet.Translator.ReadWoWString("Note", noteLength);
        }

        public static void ReadCliSupportTicketChatLine(Packet packet, params object[] idx)
        {
                packet.Translator.ReadTime("Timestamp", idx);

                var textLength = packet.Translator.ReadBits("TextLength", 12, idx);
                packet.Translator.ResetBitReader();
                packet.Translator.ReadWoWString("Text", textLength, idx);
        }

        public static void ReadCliSupportTicketChatLog(Packet packet, params object[] idx)
        {
            var linesCount = packet.Translator.ReadUInt32("LinesCount", idx);
            for (int i = 0; i < linesCount; ++i)
                ReadCliSupportTicketChatLine(packet, idx, "Lines", i);

            var hasReportLineIndex = packet.Translator.ReadBit("HasReportLineIndex", idx);
            packet.Translator.ResetBitReader();

            if (hasReportLineIndex)
                packet.Translator.ReadUInt32("ReportLineIndex", idx);
        }

        public static void ReadCliSupportTicketMailInfo(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("MailID", idx);

            var bodyLength = packet.Translator.ReadBits("MailBodyLength", 13, idx);
            var subjectLength = packet.Translator.ReadBits("MailSubjectLength", 9, idx);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadWoWString("MailBody", bodyLength, idx);
            packet.Translator.ReadWoWString("MailSubject", subjectLength, idx);
        }

        public static void ReadCliSupportTicketCalendarEventInfo(Packet packet, params object[] idx)
        {
            packet.Translator.ReadUInt64("EventID", idx); // order not confirmed
            packet.Translator.ReadUInt64("InviteID", idx); // order not confirmed

            var eventTitleLength = packet.Translator.ReadBits("EventTitleLength", 8, idx);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadWoWString("EventTitle", eventTitleLength, idx);
        }

        public static void ReadCliSupportTicketPetInfo(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("PetID", idx);

            var petNameLength = packet.Translator.ReadBits("PetNameLength", 8, idx);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadWoWString("PetName", petNameLength, idx);
        }

        public static void ReadCliSupportTicketGuildInfo(Packet packet, params object[] idx)
        {
            packet.Translator.ReadPackedGuid128("GuildID", idx);

            var guildNameLength = packet.Translator.ReadBits("GuildNameLength", 8, idx);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadWoWString("GuildName", guildNameLength, idx);
        }

        public static void ReadCliSupportTicketLFGListSearchResult(Packet packet, params object[] idx)
        {
            LfgHandler.ReadCliRideTicket(packet, "RideTicket", idx);
            packet.Translator.ReadUInt32("GroupFinderActivityID", idx);
            packet.Translator.ReadPackedGuid128("LastTitleAuthorGuid", idx);
            packet.Translator.ReadPackedGuid128("LastDescriptionAuthorGuid", idx);
            packet.Translator.ReadPackedGuid128("LastVoiceChatAuthorGuid", idx);

            var length88 = packet.Translator.ReadBits(8);
            var length217 = packet.Translator.ReadBits(11);
            var length1242 = packet.Translator.ReadBits(8);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadWoWString("Title", length88, idx);
            packet.Translator.ReadWoWString("Description", length217, idx);
            packet.Translator.ReadWoWString("VoiceChat", length1242, idx);
        }

        public static void ReadCliSupportTicketLFGListApplicant(Packet packet, params object[] idx)
        {
            LfgHandler.ReadCliRideTicket(packet, "RideTicket", idx);

            var length = packet.Translator.ReadBits(9);
            packet.Translator.ResetBitReader();
            packet.Translator.ReadWoWString("Comment", length, idx);
        }

        [Parser(Opcode.CMSG_SUPPORT_TICKET_SUBMIT_COMPLAINT)]
        public static void HandleSubmitComplaints(Packet packet)
        {
            ReadCliSupportTicketHeader(packet, "Header");
            ReadCliSupportTicketChatLog(packet, "ChatLog");

            packet.Translator.ReadPackedGuid128("TargetCharacterGUID");

            packet.Translator.ReadBits("ComplaintType", 5); // enum CliComplaintType

            var noteLength = packet.Translator.ReadBits("NoteLength", 10);

            var hasMailInfo = packet.Translator.ReadBit("HasMailInfo");
            var hasCalendarInfo = packet.Translator.ReadBit("HasCalendarInfo");
            var hasPetInfo = packet.Translator.ReadBit("HasPetInfo");
            var hasGuildInfo = packet.Translator.ReadBit("HasGuildInfo");
            var has5E4383 = packet.Translator.ReadBit("HasLFGListSearchResult");
            var has5E3DFB = packet.Translator.ReadBit("HasLFGListApplicant");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadWoWString("Note", noteLength);

            if (hasMailInfo)
                ReadCliSupportTicketMailInfo(packet, "MailInfo");

            if (hasCalendarInfo)
                ReadCliSupportTicketCalendarEventInfo(packet, "CalendarInfo");

            if (hasPetInfo)
                ReadCliSupportTicketPetInfo(packet, "PetInfo");

            if (hasGuildInfo)
                ReadCliSupportTicketGuildInfo(packet, "GuidInfo");

            if (has5E4383)
                ReadCliSupportTicketLFGListSearchResult(packet, "LFGListSearchResult");

            if (has5E3DFB)
                ReadCliSupportTicketLFGListApplicant(packet, "LFGListApplicant");
        }

        [Parser(Opcode.SMSG_GM_TICKET_RESOLVE_RESPONSE)]
        public static void HandleGMTicketResolveResponse(Packet packet)
        {
            packet.Translator.ReadBit("ShowSurvey");
        }

        [Parser(Opcode.SMSG_GM_TICKET_STATUS_UPDATE)]
        public static void HandleGMTicketStatusUpdate(Packet packet)
        {
            packet.Translator.ReadInt32("StatusInt");
        }

        [Parser(Opcode.SMSG_GM_TICKET_UPDATE)]
        public static void HandleGMTicketUpdate(Packet packet)
        {
            packet.Translator.ReadByte("Result");
        }

        [Parser(Opcode.SMSG_COMPLAINT_RESULT)]
        public static void HandleComplaintResult(Packet packet)
        {
            packet.Translator.ReadUInt32("ComplaintType");
            packet.Translator.ReadByte("Result");
        }

        [Parser(Opcode.SMSG_GM_TICKET_RESPONSE)]
        public static void HandleGMTicketResponse(Packet packet)
        {
            // TODO: confirm order

            packet.Translator.ReadUInt32("TicketID");
            packet.Translator.ReadUInt32("ResponseID");

            var descriptionLength = packet.Translator.ReadBits(11);
            var responseTextLength = packet.Translator.ReadBits(14);

            packet.Translator.ReadWoWString("Description", descriptionLength);
            packet.Translator.ReadWoWString("ResponseText", responseTextLength);
        }

    }
}
