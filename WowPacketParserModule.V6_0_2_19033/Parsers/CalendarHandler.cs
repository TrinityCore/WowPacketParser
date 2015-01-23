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

        [Parser(Opcode.SMSG_CALENDAR_SEND_CALENDAR)]
        public static void HandleCalendarSendCalendar(Packet packet)
        {
            packet.ReadTime("ServerNow");
            packet.ReadInt32("ServerTime");
            packet.ReadTime("RaidOrigin");

            var int52 = packet.ReadInt32("CalendarSendCalendarInviteInfoCount");
            var int68 = packet.ReadInt32("CalendarSendCalendarEventInfoCount");
            var int36 = packet.ReadInt32("CalendarSendCalendarRaidLockoutInfoCount");
            var int16 = packet.ReadInt32("CalendarSendCalendarRaidResetInfoCount");

            for (int i = 0; i < int52; i++)
            {
                // sub_5F9CFB
                packet.ReadInt64("EventID", i);
                packet.ReadInt64("InviteID", i);

                packet.ReadByte("Status", i);
                packet.ReadByte("Moderator", i);
                packet.ReadByte("InviteType", i);

                packet.ReadPackedGuid128("InviterGUID", i);
            }

            for (int i = 0; i < int68; i++)
            {
                // sub_6073D9
                packet.ReadInt64("EventID", i);

                packet.ReadByte("EventType", i);

                packet.ReadInt32("Date", i);
                packet.ReadInt32("Flags", i);
                packet.ReadInt32("TextureID", i);

                packet.ReadPackedGuid128("EventGuildID", i);
                packet.ReadPackedGuid128("OwnerGUID", i);

                packet.ResetBitReader();

                var len = packet.ReadBits(8);
                packet.ReadWoWString("EventName", len, i);
            }

            for (int i = 0; i < int36; i++)
            {
                // sub_5F9975
                packet.ReadInt64("InstanceID", i);

                packet.ReadInt32("MapID", i);
                packet.ReadUInt32("DifficultyID", i);
                packet.ReadInt32("ExpireTime", i);
            }

            for (int i = 0; i < int16; i++)
            {
                // sub_5F9A7E
                packet.ReadInt32("MapID", i);
                packet.ReadInt32("Duration", i);
                packet.ReadInt32("Offset", i);
            }
        }
    }
}
