using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class TicketHandler
    {
        public static void ReadHorusChatLine(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("Timestamp", indexes);
            packet.ReadPackedGuid128("AuthorGUID", indexes);

            var hasClubID = packet.ReadBit();
            var hasChannelGUID = packet.ReadBit();
            var hasRealmAddress = packet.ReadBit();
            var hasSlashCmd = packet.ReadBit();
            var textLen = packet.ReadBits(12);

            if (hasClubID)
                packet.ReadUInt64("ClubID", indexes);
            if (hasChannelGUID)
                packet.ReadPackedGuid128("ChannelGUID", indexes);
            if (hasRealmAddress)
            {
                packet.ReadUInt32("VirtualRealmAddress", indexes);
                packet.ReadUInt16("field_4", indexes);
                packet.ReadByte("field_6", indexes);
            }
            if (hasSlashCmd)
                packet.ReadInt32("SlashCmd", indexes);

            packet.ReadWoWString("Text", textLen, indexes);
        }

        public static void ReadHorusChatLog(Packet packet, params object[] indexes)
        {
            var lines = packet.ReadUInt32();
            for (int i = 0; i < lines; i++)
                ReadHorusChatLine(packet, i, indexes);
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

        [Parser(Opcode.CMSG_SUPPORT_TICKET_SUBMIT_COMPLAINT, ClientVersionBuild.V8_2_0_30898)]
        public static void HandleSubmitComplaints(Packet packet)
        {
            V6_0_2_19033.Parsers.TicketHandler.ReadCliSupportTicketHeader(packet, "Header");
            V6_0_2_19033.Parsers.TicketHandler.ReadCliSupportTicketChatLog(packet, "ChatLog");

            packet.ReadPackedGuid128("TargetCharacterGUID");

            packet.ReadBits("ComplaintType", 5); // enum CliComplaintType

            var noteLength = packet.ReadBits("NoteLength", 10);

            var hasMailInfo = packet.ReadBit("HasMailInfo");
            var hasCalendarInfo = packet.ReadBit("HasCalendarInfo");
            var hasPetInfo = packet.ReadBit("HasPetInfo");
            var hasGuildInfo = packet.ReadBit("HasGuildInfo");
            var hasLFGListSearchResult = packet.ReadBit("HasLFGListSearchResult");
            var hasLFGListApplicant = packet.ReadBit("HasLFGListApplicant");
            var hasClubMessage = packet.ReadBit("HasClubMessage");
            var hasClubFinderResult = packet.ReadBit("HasClubFinderResult");

            bool hasUnk910 = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185))
                hasUnk910 = packet.ReadBit();

            packet.ResetBitReader();

            if (hasClubMessage)
            {
                packet.ReadBit("IsUsingPlayerVoice");
                packet.ResetBitReader();
            }
            packet.ReadWoWString("Note", noteLength);

            if (hasMailInfo)
                V6_0_2_19033.Parsers.TicketHandler.ReadCliSupportTicketMailInfo(packet, "MailInfo");

            if (hasCalendarInfo)
                V6_0_2_19033.Parsers.TicketHandler.ReadCliSupportTicketCalendarEventInfo(packet, "CalendarInfo");

            if (hasPetInfo)
                V6_0_2_19033.Parsers.TicketHandler.ReadCliSupportTicketPetInfo(packet, "PetInfo");

            if (hasGuildInfo)
                V6_0_2_19033.Parsers.TicketHandler.ReadCliSupportTicketGuildInfo(packet, "GuidInfo");

            if (hasLFGListSearchResult)
                V6_0_2_19033.Parsers.TicketHandler.ReadCliSupportTicketLFGListSearchResult(packet, "LFGListSearchResult");

            if (hasLFGListApplicant)
                V6_0_2_19033.Parsers.TicketHandler.ReadCliSupportTicketLFGListApplicant(packet, "LFGListApplicant");

            if (hasClubFinderResult)
                ReadClubFinderResult(packet, "ClubFinderResult");

            if (hasUnk910)
                ReadUnused910(packet, "Unused910");
        }

        public static void ReadSupportTicketHeader(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("MapID", indexes);
            packet.ReadVector3("Position", indexes);
            packet.ReadSingle("Facing", indexes);
            packet.ReadInt32("Program", indexes);
        }

        [Parser(Opcode.CMSG_SUBMIT_USER_FEEDBACK)]
        public static void HandleSubmitUserFeedback(Packet packet)
        {
            ReadSupportTicketHeader(packet, "Header");

            var noteLen = packet.ReadBits(24);
            packet.ReadBit("IsSuggestion");
            packet.ReadWoWString("Note", noteLen);
        }
    }
}
