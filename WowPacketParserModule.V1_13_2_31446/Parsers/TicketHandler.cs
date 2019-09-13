using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class TicketHandler
    {
        public static void ReadCliSupportTicketHeader(Packet packet, params object[] idx)
        {
            packet.ReadInt32<MapId>("MapID", idx);
            packet.ReadVector3("Position", idx);
            packet.ReadSingle("Facing", idx);
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
            var hasReportLineIndex = packet.ReadBit("HasReportLineIndex", idx);
            packet.ResetBitReader();

            for (int i = 0; i < linesCount; ++i)
                ReadCliSupportTicketChatLine(packet, idx, "Lines", i);

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
            var guildNameLength = packet.ReadBits("GuildNameLength", 7, idx);
            packet.ResetBitReader();

            packet.ReadPackedGuid128("GuildID", idx);

            packet.ReadWoWString("GuildName", guildNameLength, idx);
        }

        public static void ReadCliSupportTicketLFGListSearchResult(Packet packet, params object[] idx)
        {
            LfgHandler.ReadCliRideTicket(packet, "RideTicket", idx);
            packet.ReadUInt32("GroupFinderActivityID", idx);
            packet.ReadPackedGuid128("LastTitleAuthorGuid", idx);
            packet.ReadPackedGuid128("LastDescriptionAuthorGuid", idx);
            packet.ReadPackedGuid128("LastVoiceChatAuthorGuid", idx);
            packet.ReadPackedGuid128("UnkGUID", idx);
            packet.ReadPackedGuid128("UnkGUID", idx);

            var length88 = packet.ReadBits(8);
            var length217 = packet.ReadBits(11);
            var length1242 = packet.ReadBits(8);

            packet.ResetBitReader();

            packet.ReadWoWString("Title", length88, idx);
            packet.ReadWoWString("Description", length217, idx);
            packet.ReadWoWString("VoiceChat", length1242, idx);
        }

        public static void ReadCliSupportTicketLFGListApplicant(Packet packet, params object[] idx)
        {
            LfgHandler.ReadCliRideTicket(packet, "RideTicket", idx);

            var length = packet.ReadBits(9);
            packet.ResetBitReader();
            packet.ReadWoWString("Comment", length, idx);
        }

        [Parser(Opcode.CMSG_SUPPORT_TICKET_SUBMIT_COMPLAINT)]
        public static void HandleSubmitComplaints(Packet packet)
        {
            ReadCliSupportTicketHeader(packet, "Header");
            packet.ReadPackedGuid128("TargetCharacterGUID");
            ReadCliSupportTicketChatLog(packet, "ChatLog");

            packet.ReadBits("ComplaintType", 5); // enum CliComplaintType

            var noteLength = packet.ReadBits("NoteLength", 10);

            var hasMailInfo = packet.ReadBit("HasMailInfo");
            var hasCalendarInfo = packet.ReadBit("HasCalendarInfo");
            var hasPetInfo = packet.ReadBit("HasPetInfo");
            var hasGuildInfo = packet.ReadBit("HasGuildInfo");
            var has5E4383 = packet.ReadBit("HasLFGListSearchResult");
            var has5E3DFB = packet.ReadBit("HasLFGListApplicant");
            var hasUnkBit = packet.ReadBit("UnkBit");

            packet.ResetBitReader();

            if (hasUnkBit)
                packet.ReadBits("UnkBits7", 7);

            if (hasMailInfo)
                ReadCliSupportTicketMailInfo(packet, "MailInfo");

            packet.ReadWoWString("Note", noteLength);

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
    }
}
