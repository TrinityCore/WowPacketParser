using WowPacketParser.Enums;
using WowPacketParser.Messages.Client;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.CalendarEvent
{
    public unsafe struct ModeratorStatus
    {
        public ulong InviteID;
        public ulong EventID;
        public ulong Guid;
        public ulong ModeratorID;
        public byte Status;

        [Parser(Opcode.CMSG_CALENDAR_EVENT_MODERATOR_STATUS)]
        public static void HandleCalendarEventModeratorStatus(Packet packet)
        {
            packet.ReadPackedGuid("Invitee GUID");
            packet.ReadInt64("Event ID");
            packet.ReadInt64("Invite ID");
            packet.ReadInt64("Owner Invite ID"); // sender's invite id?
            packet.ReadInt32E<CalendarModerationRank>("Rank");
        }
    }
}
