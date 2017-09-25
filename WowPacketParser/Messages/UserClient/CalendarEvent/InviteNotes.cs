using WowPacketParser.Enums;
using WowPacketParser.Messages.Client;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.CalendarEvent
{
    public unsafe struct InviteNotes
    {
        public ulong EventID;
        public ulong Guid;
        public ulong InviteID;
        public ulong ModeratorID;
        public string Notes;

        [Parser(Opcode.SMSG_CALENDAR_EVENT_INVITE_NOTES)]
        public static void HandleCalendarEventInviteNotes(Packet packet)
        {
            packet.ReadPackedGuid128("InviteGUID");
            packet.ReadUInt64("EventID");

            var notesLength = packet.ReadBits(8);
            packet.ReadBit("ClearPending");
            packet.ResetBitReader();

            packet.ReadWoWString("Notes", notesLength);
        }
    }
}
