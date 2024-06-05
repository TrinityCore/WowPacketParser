using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class CalenderHandler
    {
        [Parser(Opcode.SMSG_CALENDAR_SEND_NUM_PENDING)]
        public static void HandleSendCalendarNumPending(Packet packet)
        {
            packet.ReadUInt32("NumPending");
        }

        [Parser(Opcode.CMSG_CALENDAR_GET_NUM_PENDING)]
        public static void HandleCalenderZero(Packet packet)
        {
        }
    }
}
