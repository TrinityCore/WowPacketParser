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
            packet.ReadTime("OldestTicketTime");
            packet.ReadTime("UpdateTime");

            var int24 = packet.ReadInt32("CasesCount");
            for (int i = 0; i < int24; i++)
            {
                packet.ReadInt32("CaseID", i);
                packet.ReadInt32("CaseOpened", i);
                packet.ReadInt32("CaseStatus", i);
                packet.ReadInt16("CfgRealmID", i);
                packet.ReadInt64("CharacterID", i);
                packet.ReadInt32("WaitTimeOverrideMinutes", i);

                packet.ResetBitReader();
                var bits12 = packet.ReadBits(11);
                var bits262 = packet.ReadBits(10);

                packet.ReadWoWString("Url", bits12, i);
                packet.ReadWoWString("WaitTimeOverrideMessage", bits262, i);
            }
        }

        [Parser(Opcode.SMSG_GM_TICKET_SYSTEM_STATUS)]
        public static void HandleGMTicketSystemStatus(Packet packet)
        {
            packet.ReadInt32("Status");
        }

        [Parser(Opcode.SMSG_GM_TICKET_GET_TICKET_RESPONSE)]
        public static void HandleGMTicketGetTicketResponse(Packet packet)
        {
            packet.ReadInt32("Result");

            var hasInfo = packet.ReadBit("HasInfo");

            // ClientGMTicketInfo
            if (hasInfo)
            {
                packet.ReadInt32("TicketID");
                packet.ReadByte("Category");
                packet.ReadTime("TicketOpenTime");
                packet.ReadTime("OldestTicketTime");
                packet.ReadTime("UpdateTime");
                packet.ReadByte("AssignedToGM");
                packet.ReadByte("OpenedByGM");
                packet.ReadInt32("WaitTimeOverrideMinutes");

                packet.ResetBitReader();

                var bits1 = packet.ReadBits(11);
                var bits2022 = packet.ReadBits(10);

                packet.ReadWoWString("TicketDescription", bits1);
                packet.ReadWoWString("WaitTimeOverrideMessage", bits2022);
            }
        }

        public static void ReadComplaintOffender(Packet packet, params object[] indexes)
        {
            packet.ReadPackedGuid128("PlayerGuid", indexes);
            packet.ReadInt32("RealmAddress", indexes);
            packet.ReadInt32("TimeSinceOffence", indexes);
        }

        public static void ReadComplaintChat(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("Command");
            packet.ReadInt32("ChannelID");

            packet.ResetBitReader();

            var len = packet.ReadBits(12);
            packet.ReadWoWString("MessageLog", len);
        }

        [Parser(Opcode.CMSG_COMPLAINT)]
        public static void HandleComplain(Packet packet)
        {
            var result = packet.ReadByte("ComplaintType");

            ReadComplaintOffender(packet, "Offender");

            switch (result)
            {
                case 0: // Mail
                    packet.ReadInt32("MailID");
                    break;
                case 1: // Chat
                    ReadComplaintChat(packet, "Chat");
                    break;
                case 2: // Calendar
                    // Order guessed
                    packet.ReadInt64("EventGuid");
                    packet.ReadInt64("InviteGuid");
                    break;
            }
        }

        [Parser(Opcode.CMSG_GM_TICKET_ACKNOWLEDGE_SURVEY)]
        public static void HandleGMTicketAcknowledgeSurvey(Packet packet)
        {
            packet.ReadInt32("CaseID");
        }

        [Parser(Opcode.CMSG_GM_TICKET_CREATE)]
        public static void HandleGMTicketCreate(Packet packet)
        {
            packet.ReadInt32<MapId>("Map");
            packet.ReadVector3("Pos");
            packet.ReadByte("Flags");

            var descriptionLength = packet.ReadBits("DescriptionLength", 11);
            packet.ResetBitReader();
            packet.ReadWoWString("Description", descriptionLength);

            packet.ReadBit("NeedResponse");
            packet.ReadBit("NeedMoreHelp");
            packet.ResetBitReader();

            var dataLength = packet.ReadInt32("DataLength");

            if (dataLength > 0)
            {
                var textCount = packet.ReadByte("TextCount");

                for (int i = 0; i < textCount /* 60 */; ++i)
                    packet.AddValue("Sent", (packet.Time - packet.ReadTime()).ToFormattedString(), i);

                var decompCount = packet.ReadInt32();
                var pkt = packet.Inflate(decompCount);
                pkt.ReadCString("Text");
                pkt.ClosePacket(false);
            }
        }

        [Parser(Opcode.CMSG_GM_TICKET_UPDATE_TEXT)]
        public static void HandleGMTicketUpdatetext(Packet packet)
        {
            var length = packet.ReadBits("DescriptionLength", 11);
            packet.ResetBitReader();

            packet.ReadWoWString("Description", length);
        }

        public static void ReadClientGMSurveyQuestion(Packet packet, params object[] idx)
        {
            packet.ReadInt32("QuestionID", idx);
            packet.ReadByte("Answer", idx);

            packet.ResetBitReader();
            var length = packet.ReadBits("AnswerCommentLength", 11, idx);
            packet.ResetBitReader();

            packet.ReadWoWString("AnswerComment", length, idx);
        }

        [Parser(Opcode.CMSG_GM_SURVEY_SUBMIT)]
        public static void HandleGMSurveySubmit(Packet packet)
        {
            packet.ReadInt32("SurveyID");

            var questionCount = packet.ReadBits("SurveyQuestionCount", 4);
            var commentLenght = packet.ReadBits("CommentLength", 11);

            packet.ResetBitReader();

            for (var i = 0; i < questionCount; ++i)
                ReadClientGMSurveyQuestion(packet, "SurveyQuestion", i);

            packet.ReadWoWString("Comment", commentLenght);
        }

        public static void ReadCliSupportTicketHeader(Packet packet, params object[] idx)
        {
            packet.ReadInt32<MapId>("MapID", idx);
            packet.ReadVector3("Position", idx);
            packet.ReadSingle("Facing", idx);
        }

        [Parser(Opcode.CMSG_SUPPORT_TICKET_SUBMIT_SUGGESTION)]
        [Parser(Opcode.CMSG_SUPPORT_TICKET_SUBMIT_BUG)]
        public static void HandleSubmitBug(Packet packet)
        {
            ReadCliSupportTicketHeader(packet, "Header");

            var noteLength = packet.ReadBits("NoteLength", 10);
            packet.ResetBitReader();
            packet.ReadWoWString("Note", noteLength);
        }

        public static void ReadCliSupportTicketChatLine(Packet packet, params object[] idx)
        {
                packet.ReadTime("Timestamp", idx);

                var textLength = packet.ReadBits("TextLength", 12, idx);
                packet.ResetBitReader();
                packet.ReadWoWString("Text", textLength, idx);
        }

        public static void ReadCliSupportTicketChatLog(Packet packet, params object[] idx)
        {
            var linesCount = packet.ReadUInt32("LinesCount", idx);
            for (int i = 0; i < linesCount; ++i)
                ReadCliSupportTicketChatLine(packet, idx, "Lines", i);

            var hasReportLineIndex = packet.ReadBit("HasReportLineIndex", idx);
            packet.ResetBitReader();

            if (hasReportLineIndex)
                packet.ReadUInt32("ReportLineIndex", idx);
        }

        public static void ReadCliSupportTicketMailInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32("MailID", idx);

            var bodyLength = packet.ReadBits("MailBodyLength", 13, idx);
            var subjectLength = packet.ReadBits("MailSubjectLength", 9, idx);

            packet.ResetBitReader();

            packet.ReadWoWString("MailBody", bodyLength, idx);
            packet.ReadWoWString("MailSubject", subjectLength, idx);
        }

        public static void ReadCliSupportTicketCalendarEventInfo(Packet packet, params object[] idx)
        {
            packet.ReadUInt64("EventID", idx); // order not confirmed
            packet.ReadUInt64("InviteID", idx); // order not confirmed

            var eventTitleLength = packet.ReadBits("EventTitleLength", 8, idx);

            packet.ResetBitReader();

            packet.ReadWoWString("EventTitle", eventTitleLength, idx);
        }

        public static void ReadCliSupportTicketPetInfo(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("PetID", idx);

            var petNameLength = packet.ReadBits("PetNameLength", 8, idx);

            packet.ResetBitReader();

            packet.ReadWoWString("PetName", petNameLength, idx);
        }

        public static void ReadCliSupportTicketGuildInfo(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("GuildID", idx);

            var guildNameLength = packet.ReadBits("GuildNameLength", 8, idx);

            packet.ResetBitReader();

            packet.ReadWoWString("GuildName", guildNameLength, idx);
        }

        public static void Read5E4383(Packet packet, params object[] idx)
        {
            LfgHandler.ReadCliRideTicket(packet, "RideTicket", idx);
            packet.ReadPackedGuid128("40", idx);
            packet.ReadPackedGuid128("56", idx);
            packet.ReadPackedGuid128("72", idx);

            var length88 = packet.ReadBits("88", 8, idx);
            var length217 = packet.ReadBits("217", 8, idx);
            var length1242 = packet.ReadBits("1242", 8, idx);

            packet.ResetBitReader();

            packet.ReadWoWString("88", length88, idx);
            packet.ReadWoWString("217", length217, idx);
            packet.ReadWoWString("1242", length1242, idx);
        }

        public static void Read5E3DFB(Packet packet, params object[] idx)
        {
            LfgHandler.ReadCliRideTicket(packet, "RideTicket", idx);

            var length = packet.ReadBits("32", 9, idx);
            packet.ResetBitReader();
            packet.ReadWoWString("32", length, idx);
        }

        [Parser(Opcode.CMSG_SUPPORT_TICKET_SUBMIT_COMPLAINT)]
        public static void HandleSubmitComplaints(Packet packet)
        {
            ReadCliSupportTicketHeader(packet, "Header");
            ReadCliSupportTicketChatLog(packet, "ChatLog");

            packet.ReadPackedGuid128("TargetCharacterGUID");

            packet.ReadBits("ComplaintType", 5); // enum CliComplaintType

            var noteLength = packet.ReadBits("NoteLength", 10);

            var hasMailInfo = packet.ReadBit("HasMailInfo");
            var hasCalendarInfo = packet.ReadBit("HasCalendarInfo");
            var hasPetInfo = packet.ReadBit("HasPetInfo");
            var hasGuildInfo = packet.ReadBit("HasGuildInfo");
            var has5E4383 = packet.ReadBit("Has5E4383");
            var has5E3DFB = packet.ReadBit("Has5E3DFB");

            packet.ResetBitReader();

            packet.ReadWoWString("Note", noteLength);

            if (hasMailInfo)
                ReadCliSupportTicketMailInfo(packet, "MailInfo");

            if (hasCalendarInfo)
                ReadCliSupportTicketCalendarEventInfo(packet, "CalendarInfo");

            if (hasPetInfo)
                ReadCliSupportTicketPetInfo(packet, "PetInfo");

            if (hasGuildInfo)
                ReadCliSupportTicketGuildInfo(packet, "GuidInfo");

            if (has5E4383)
                Read5E4383(packet, "5E4383");

            if (has5E3DFB)
                Read5E3DFB(packet, "5E3DFB");
        }

        [Parser(Opcode.SMSG_GM_TICKET_RESOLVE_RESPONSE)]
        public static void HandleGMTicketResolveResponse(Packet packet)
        {
            packet.ReadBit("ShowSurvey");
        }

        [Parser(Opcode.SMSG_GM_TICKET_STATUS_UPDATE)]
        public static void HandleGMTicketStatusUpdate(Packet packet)
        {
            packet.ReadInt32("StatusInt");
        }

        [Parser(Opcode.SMSG_GM_TICKET_UPDATE)]
        public static void HandleGMTicketUpdate(Packet packet)
        {
            packet.ReadByte("Result");
        }

        [Parser(Opcode.SMSG_COMPLAINT_RESULT)]
        public static void HandleComplaintResult(Packet packet)
        {
            packet.ReadUInt32("ComplaintType");
            packet.ReadByte("Result");
        }

        [Parser(Opcode.SMSG_GM_TICKET_RESPONSE)]
        public static void HandleGMTicketResponse(Packet packet)
        {
            // TODO: confirm order

            packet.ReadUInt32("TicketID");
            packet.ReadUInt32("ResponseID");

            var descriptionLength = packet.ReadBits(11);
            var responseTextLength = packet.ReadBits(14);

            packet.ReadWoWString("Description", descriptionLength);
            packet.ReadWoWString("ResponseText", responseTextLength);
        }

    }
}
