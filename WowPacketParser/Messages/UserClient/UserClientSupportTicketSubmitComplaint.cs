using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientSupportTicketSubmitComplaint
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
    }
}
