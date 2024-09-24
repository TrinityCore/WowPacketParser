using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class TicketHandler
    {
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

        public static void ReadSupportTicketHeader(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("MapID", indexes);
            packet.ReadVector3("Position", indexes);
            packet.ReadSingle("Facing", indexes);
            packet.ReadInt32("Program", indexes);
        }

        public static void ReadSupportTicketChatLine(Packet packet, params object[] idx)
        {
            packet.ReadTime64("Timestamp", idx);

            var textLength = packet.ReadBits("TextLength", 12, idx);
            packet.ResetBitReader();
            packet.ReadWoWString("Text", textLength, idx);
        }

        public static void ReadSupportTicketChatLog(Packet packet, params object[] idx)
        {
            var linesCount = packet.ReadUInt32("LinesCount", idx);
            var hasReportLineIndex = packet.ReadBit("HasReportLineIndex", idx);
            packet.ResetBitReader();

            for (int i = 0; i < linesCount; ++i)
                ReadSupportTicketChatLine(packet, idx, "Lines", i);

            if (hasReportLineIndex)
                packet.ReadUInt32("ReportLineIndex", idx);
        }

        public static void ReadSupportTicketHorusChatLine(Packet packet, params object[] idx)
        {
            packet.ReadTime64("Timestamp", idx);
            packet.ReadPackedGuid128("AuthorGuid", idx);

            var hasClubID = packet.ReadBit();
            var hasChannelGUID = packet.ReadBit();
            var hasRealmAddress = packet.ReadBit();
            var hasSlashCmd = packet.ReadBit();
            var textLen = packet.ReadBits(12);

            if (hasClubID)
                packet.ReadUInt64("ClubID", idx);
            if (hasChannelGUID)
                packet.ReadPackedGuid128("ChannelGUID", idx);
            if (hasRealmAddress)
            {
                packet.ReadUInt32("VirtualRealmAddress", idx);
                packet.ReadUInt16("field_4", idx);
                packet.ReadByte("field_6", idx);
            }
            if (hasSlashCmd)
                packet.ReadInt32("SlashCmd", idx);

            packet.ReadWoWString("Text", textLen, idx);
        }

        public static void ReadSupportTicketHorusChatLog(Packet packet, params object[] idx)
        {
            var linesCount = packet.ReadUInt32("LinesCount", idx);
            packet.ResetBitReader();

            for (int i = 0; i < linesCount; ++i)
                ReadSupportTicketHorusChatLine(packet, idx, "Lines", i);
        }

        public static void ReadCliSupportTicketMailInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt64("MailID", idx);

            var bodyLength = packet.ReadBits("MailBodyLength", 13, idx);
            var subjectLength = packet.ReadBits("MailSubjectLength", 9, idx);

            packet.ResetBitReader();

            packet.ReadWoWString("MailBody", bodyLength, idx);
            packet.ReadWoWString("MailSubject", subjectLength, idx);
        }

        public static void ReadCliSupportTicketCalendarEventInfo(Packet packet, params object[] idx)
        {
            packet.ReadUInt64("EventID", idx);
            packet.ReadUInt64("InviteID", idx);

            var eventTitleLength = packet.ReadBits("EventTitleLength", 8, idx);

            packet.ResetBitReader();

            packet.ReadWoWString("EventTitle", eventTitleLength, idx);
        }

        public static void ReadCliSupportTicketPetInfo(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("PetGUID", idx);

            var petNameLength = packet.ReadBits("PetNameLength", 8, idx);

            packet.ResetBitReader();

            packet.ReadWoWString("PetName", petNameLength, idx);
        }

        public static void ReadCliSupportTicketGuildInfo(Packet packet, params object[] idx)
        {
            var guildNameLength = packet.ReadBits("GuildNameLength", 7, idx);
            packet.ResetBitReader();

            packet.ReadPackedGuid128("GuildGUID", idx);
            packet.ReadWoWString("GuildName", guildNameLength, idx);
        }

        public static void ReadCliSupportTicketLFGListSearchResult(Packet packet, params object[] idx)
        {
            LfgHandler.ReadCliRideTicket(packet, "RideTicket", idx);
            packet.ReadUInt32("GroupFinderActivityID", idx);
            packet.ReadByte("Unknown1007", idx);

            packet.ReadPackedGuid128("LastTitleAuthorGuid", idx);
            packet.ReadPackedGuid128("LastDescriptionAuthorGuid", idx);
            packet.ReadPackedGuid128("LastVoiceChatAuthorGuid", idx);
            packet.ReadPackedGuid128("ListingCreatorGuid", idx);
            packet.ReadPackedGuid128("Unknown735", idx);

            var titleLength = packet.ReadBits(10);
            var descriptionLength = packet.ReadBits(11);
            var voiceChatLength = packet.ReadBits(8);

            packet.ResetBitReader();

            packet.ReadWoWString("Title", titleLength, idx);
            packet.ReadWoWString("Description", descriptionLength, idx);
            packet.ReadWoWString("VoiceChat", voiceChatLength, idx);
        }

        public static void ReadCliSupportTicketLFGListApplicant(Packet packet, params object[] idx)
        {
            LfgHandler.ReadCliRideTicket(packet, "RideTicket", idx);

            var length = packet.ReadBits(9);
            packet.ResetBitReader();
            packet.ReadWoWString("Comment", length, idx);
        }

        public static void ReadClubFinderResult(Packet packet, params object[] indexes)
        {
            packet.ReadUInt64("ClubFinderPostingID", indexes);
            packet.ReadUInt64("ClubID ", indexes);
            packet.ReadPackedGuid128("ClubFinderGUID", indexes);
            var nameLen = packet.ReadBits(12);
            packet.ReadWoWString("ClubName", nameLen, indexes);
        }

        public static void ReadUnused910(Packet packet, params object[] indexes)
        {
            var len = packet.ReadBits(7);
            packet.ReadPackedGuid128("field_104", indexes);
            packet.ReadWoWString("field_0", len, indexes);
        }

        [Parser(Opcode.SMSG_GM_TICKET_CASE_STATUS)]
        public static void HandleGMTicketCaseStatus(Packet packet)
        {
            var casesCount = packet.ReadInt32("CasesCount");
            for (int i = 0; i < casesCount; i++)
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

        [Parser(Opcode.SMSG_COMPLAINT_RESULT)]
        public static void HandleComplaintResult(Packet packet)
        {
            packet.ReadUInt32("ComplaintType");
            packet.ReadByte("Result");
        }

        [Parser(Opcode.SMSG_GM_TICKET_SYSTEM_STATUS)]
        public static void HandleGMTicketSystemStatus(Packet packet)
        {
            packet.ReadInt32("Status");
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

        [Parser(Opcode.CMSG_SUBMIT_USER_FEEDBACK)]
        public static void HandleSubmitUserFeedback(Packet packet)
        {
            ReadSupportTicketHeader(packet, "Header");

            var noteLen = packet.ReadBits(24);
            packet.ReadBit("IsSuggestion");
            packet.ReadDynamicString("Note", noteLen);
        }

        [Parser(Opcode.CMSG_SUPPORT_TICKET_SUBMIT_COMPLAINT)]
        public static void HandleSubmitComplaints(Packet packet)
        {
            ReadSupportTicketHeader(packet, "Header");

            packet.ReadPackedGuid128("TargetCharacterGUID");
            packet.ReadInt32("ReportType");
            packet.ReadInt32("MajorCategory");
            packet.ReadInt32("MinorCategoryFlags");

            ReadSupportTicketChatLog(packet, "ChatLog");

            var noteLength = packet.ReadBits("NoteLength", 10);
            var hasMailInfo = packet.ReadBit("HasMailInfo");
            var hasCalendarInfo = packet.ReadBit("HasCalendarInfo");
            var hasPetInfo = packet.ReadBit("HasPetInfo");
            var hasGuildInfo = packet.ReadBit("HasGuildInfo");
            var hasLFGListSearchResult = packet.ReadBit("HasLFGListSearchResult");
            var hasLFGListApplicant = packet.ReadBit("HasLFGListApplicant");
            var hasClubMessage = packet.ReadBit("HasClubMessage");
            var hasClubFinderResult = packet.ReadBit("HasClubFinderResult");
            bool hasUnk910 = packet.ReadBit();

            packet.ResetBitReader();

            if (hasClubMessage)
            {
                packet.ReadBit("IsUsingPlayerVoice");
                packet.ResetBitReader();
            }

            ReadSupportTicketHorusChatLog(packet, "HorusChatLog");

            packet.ReadWoWString("Note", noteLength);

            if (hasMailInfo)
                ReadCliSupportTicketMailInfo(packet, "MailInfo");

            if (hasCalendarInfo)
                ReadCliSupportTicketCalendarEventInfo(packet, "CalendarInfo");

            if (hasPetInfo)
                ReadCliSupportTicketPetInfo(packet, "PetInfo");

            if (hasGuildInfo)
                ReadCliSupportTicketGuildInfo(packet, "GuidInfo");

            if (hasLFGListSearchResult)
                ReadCliSupportTicketLFGListSearchResult(packet, "LFGListSearchResult");

            if (hasLFGListApplicant)
                ReadCliSupportTicketLFGListApplicant(packet, "LFGListApplicant");

            if (hasClubFinderResult)
                ReadClubFinderResult(packet, "ClubFinderResult");

            if (hasUnk910)
                ReadUnused910(packet, "Unused910");
        }

        [Parser(Opcode.CMSG_GM_TICKET_GET_TICKET)]
        [Parser(Opcode.CMSG_GM_TICKET_GET_CASE_STATUS)]
        [Parser(Opcode.CMSG_GM_TICKET_GET_SYSTEM_STATUS)]
        [Parser(Opcode.SMSG_GM_TICKET_RESPONSE_ERROR)]
        public static void HandleGMTicketZero(Packet packet)
        {
        }
    }
}
