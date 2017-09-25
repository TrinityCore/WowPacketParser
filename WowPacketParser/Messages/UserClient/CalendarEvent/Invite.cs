using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.CalendarEvent
{
    public unsafe struct Invite
    {
        public ulong ModeratorID;
        public bool IsSignUp;
        public bool Creating;
        public ulong EventID;
        public string Name;

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE)]
        public static void HandleCalendarEventInvite(Packet packet)
        {
            packet.ReadPackedGuid128("InviteGUID");
            packet.ReadUInt64("EventID");
            packet.ReadUInt64("InviteID");
            packet.ReadByte("Level");
            packet.ReadByteE<CalendarEventStatus>("Status");
            packet.ReadByteE<CalendarEventType>("Type");
            packet.ReadPackedTime("ResponseTime");
            packet.ReadBit("ClearPending");
        }
    }
}
