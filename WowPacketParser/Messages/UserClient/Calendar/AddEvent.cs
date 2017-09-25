using WowPacketParser.Enums;
using WowPacketParser.Messages.Client;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.Calendar
{
    public unsafe struct AddEvent
    {
        public uint MaxSize;
        public ClientCalendarAddEventInfo EventInfo;

        [Parser(Opcode.CMSG_CALENDAR_ADD_EVENT)]
        public static void HandleUserClientCalendarAddEvent(Packet packet)
        {
            packet.ResetBitReader();
            var TitleLen = packet.ReadBits(8);
            var DescriptionLen = packet.ReadBits(11);

            packet.ReadByteE<CalendarEventType>("EventType");
            packet.ReadInt32("TextureID");
            packet.ReadTime("Time");
            packet.ReadInt32E<CalendarFlag>("Flags");

            var inviteInfoCount = packet.ReadInt32();

            packet.ReadWoWString("Title", TitleLen);
            packet.ReadWoWString("Description", DescriptionLen);

            for (int i = 0; i < inviteInfoCount; i++)
            {
                packet.ReadPackedGuid128("Guid");
                packet.ReadByteE<CalendarEventStatus>("Status");
                packet.ReadByteE<CalendarModerationRank>("Moderator");
            }

            packet.ReadInt32("MaxSize");
        }
    }
}
