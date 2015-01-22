using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class CalendarHandler
    {
        [Parser(Opcode.CMSG_CALENDAR_GET)]
        public static void HandleCalendarZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_CALENDAR_SEND_NUM_PENDING)]
        public static void HandleSendCalendarNumPending(Packet packet)
        {
            packet.ReadInt32("NumPending");
        }
    }
}
