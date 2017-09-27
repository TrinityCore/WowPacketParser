using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Parsing.Parsers;

namespace WowPacketParser.Messages.UserClient.SupportTicket
{
    public unsafe struct SubmitComplaint
    {
        public CliSupportTicketChatLog ChatLog;
        public string Note;
        public CliSupportTicketGuildInfo? GuildInfo; // Optional
        public CliSupportTicketMailInfo? MailInfo; // Optional
        public ulong TargetCharacterGUID;
        public CliSupportTicketPetInfo? PetInfo; // Optional
        public CliSupportTicketCalendarEventInfo? CalendarInfo; // Optional
        public CliComplaintType ComplaintType;
        public CliSupportTicketHeader Header;

        [Parser(Opcode.CMSG_SUPPORT_TICKET_SUBMIT_COMPLAINT)]
        public static void HandleSubmitComplain(Packet packet)
        {
            var pos = new Vector4();
            var guid = new byte[8];

            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            var length = packet.ReadBits(12);

            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();

            packet.ReadBits("Unk bits", 4); // ##

            guid[6] = packet.ReadBit();

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);

            packet.ReadWoWString("Text", length);

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);

            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            packet.ReadInt32("Unk Int32 1"); // ##
            pos.O = packet.ReadSingle();

            packet.ReadBit("Unk bit"); // ##

            var count = packet.ReadBits("Count", 22);
            var strLength = new uint[count];
            for (int i = 0; i < count; ++i)
                strLength[i] = packet.ReadBits(13);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadTime("Time", i);
                packet.ReadWoWString("Data", strLength[i], i);
            }

            packet.ReadInt32("Unk Int32 2");  // ##

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
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
            var has5E4383 = packet.ReadBit("HasLFGListSearchResult");
            var has5E3DFB = packet.ReadBit("HasLFGListApplicant");

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
                ReadCliSupportTicketLFGListSearchResult(packet, "LFGListSearchResult");

            if (has5E3DFB)
                ReadCliSupportTicketLFGListApplicant(packet, "LFGListApplicant");
        }

        private static void readCliSupportTicketHeader(Packet packet, params object[] idx)
        {
            packet.ReadInt32<MapId>("MapID", idx);
            packet.ReadVector3("Position", idx);
            packet.ReadSingle("Facing", idx);
        }

        private static void readCliSupportTicketChatLog(Packet packet, params object[] idx)
        {
            var linesCount = packet.ReadUInt32("LinesCount", idx);
            for (int i = 0; i < linesCount; ++i)
                ReadCliSupportTicketChatLine(packet, idx, "Lines", i);

            var hasReportLineIndex = packet.ReadBit("HasReportLineIndex", idx);
            packet.ResetBitReader();

            if (hasReportLineIndex)
                packet.ReadUInt32("ReportLineIndex", idx);
        }

        private static void readCliSupportTicketChatLine(Packet packet, params object[] idx)
        {
            packet.ReadTime("Timestamp", idx);

            var textLength = packet.ReadBits("TextLength", 12, idx);
            packet.ResetBitReader();
            packet.ReadWoWString("Text", textLength, idx);
        }

        private static void readCliSupportTicketMailInfo(Packet packet, params object[] idx)
        {
            packet.ReadInt32("MailID", idx);

            var bodyLength = packet.ReadBits("MailBodyLength", 13, idx);
            var subjectLength = packet.ReadBits("MailSubjectLength", 9, idx);

            packet.ResetBitReader();

            packet.ReadWoWString("MailBody", bodyLength, idx);
            packet.ReadWoWString("MailSubject", subjectLength, idx);
        }

        private static void readCliSupportTicketCalendarEventInfo(Packet packet, params object[] idx)
        {
            packet.ReadUInt64("EventID", idx); // order not confirmed
            packet.ReadUInt64("InviteID", idx); // order not confirmed

            var eventTitleLength = packet.ReadBits("EventTitleLength", 8, idx);

            packet.ResetBitReader();

            packet.ReadWoWString("EventTitle", eventTitleLength, idx);
        }

        private static void readCliSupportTicketPetInfo(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("PetID", idx);

            var petNameLength = packet.ReadBits("PetNameLength", 8, idx);

            packet.ResetBitReader();

            packet.ReadWoWString("PetName", petNameLength, idx);
        }

        private static void readCliSupportTicketGuildInfo(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("GuildID", idx);

            var guildNameLength = packet.ReadBits("GuildNameLength", 8, idx);

            packet.ResetBitReader();

            packet.ReadWoWString("GuildName", guildNameLength, idx);
        }

        private static void readCliSupportTicketLFGListSearchResult(Packet packet, params object[] idx)
        {
            ReadCliRideTicket(packet, "RideTicket", idx);
            packet.ReadUInt32("GroupFinderActivityID", idx);
            packet.ReadPackedGuid128("LastTitleAuthorGuid", idx);
            packet.ReadPackedGuid128("LastDescriptionAuthorGuid", idx);
            packet.ReadPackedGuid128("LastVoiceChatAuthorGuid", idx);

            var length88 = packet.ReadBits(8);
            var length217 = packet.ReadBits(11);
            var length1242 = packet.ReadBits(8);

            packet.ResetBitReader();

            packet.ReadWoWString("Title", length88, idx);
            packet.ReadWoWString("Description", length217, idx);
            packet.ReadWoWString("VoiceChat", length1242, idx);
        }

        private static void readCliSupportTicketLFGListApplicant(Packet packet, params object[] idx)
        {
            ReadCliRideTicket(packet, "RideTicket", idx);

            var length = packet.ReadBits(9);
            packet.ResetBitReader();
            packet.ReadWoWString("Comment", length, idx);
        }

        private static void readCliRideTicket(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("RequesterGuid", idx);
            packet.ReadInt32("Id", idx);
            packet.ReadInt32("Type", idx);
            packet.ReadTime("Time", idx);
        }
    }

}
