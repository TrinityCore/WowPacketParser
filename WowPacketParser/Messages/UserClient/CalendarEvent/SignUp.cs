using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.CalendarEvent
{
    public unsafe struct SignUp
    {
        public bool Tentative;
        public ulong EventID;

        [Parser(Opcode.CMSG_CALENDAR_EVENT_SIGN_UP)]
        public static void HandleCalendarEventSignup(Packet packet)
        {
            packet.ReadInt64("Event ID");
            packet.ReadByteE<CalendarEventStatus>("Status");
        }
    }
}
