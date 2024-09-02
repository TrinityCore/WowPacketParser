using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class TicketHandler
    {
        public static void ReadComplaintOffender(Packet packet, params object[] indexes)
        {
            packet.ReadPackedGuid128("PlayerGuid", indexes);
            packet.ReadInt32("RealmAddress", indexes);
            packet.ReadInt32("TimeSinceOffence", indexes);
        }

        public static void ReadComplaintChat(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("Command");
            packet.ReadInt32("ChannelID");

            packet.ResetBitReader();

            var len = packet.ReadBits(12);
            packet.ReadWoWString("MessageLog", len);
        }

        [Parser(Opcode.SMSG_GM_TICKET_CASE_STATUS)]
        public static void HandleGMTicketCaseStatus(Packet packet)
        {
            var casesCount = packet.ReadInt32("CasesCount");
            for (int i = 0; i < casesCount; i++)
            {
                packet.ReadInt32("CaseID", i);
                packet.ReadInt32("CaseOpened", i);
                packet.ReadInt32("CaseStatus", i);
                packet.ReadInt16("CfgRealmID", i);
                packet.ReadInt64("CharacterID", i);
                packet.ReadInt32("WaitTimeOverrideMinutes", i);

                packet.ResetBitReader();
                var bits12 = packet.ReadBits(11);
                var bits262 = packet.ReadBits(10);

                packet.ReadWoWString("Url", bits12, i);
                packet.ReadWoWString("WaitTimeOverrideMessage", bits262, i);
            }
        }

        [Parser(Opcode.SMSG_COMPLAINT_RESULT)]
        public static void HandleComplaintResult(Packet packet)
        {
            packet.ReadUInt32("ComplaintType");
            packet.ReadByte("Result");
        }

        [Parser(Opcode.SMSG_GM_TICKET_SYSTEM_STATUS)]
        public static void HandleGMTicketSystemStatus(Packet packet)
        {
            packet.ReadInt32("Status");
        }

        [Parser(Opcode.CMSG_COMPLAINT)]
        public static void HandleComplain(Packet packet)
        {
            var result = packet.ReadByte("ComplaintType");

            ReadComplaintOffender(packet, "Offender");

            switch (result)
            {
                case 0: // Mail
                    packet.ReadInt32("MailID");
                    break;
                case 1: // Chat
                    ReadComplaintChat(packet, "Chat");
                    break;
                case 2: // Calendar
                    packet.ReadInt64("EventGuid");
                    packet.ReadInt64("InviteGuid");
                    break;
            }
        }

        [Parser(Opcode.CMSG_GM_TICKET_ACKNOWLEDGE_SURVEY)]
        public static void HandleGMTicketAcknowledgeSurvey(Packet packet)
        {
            packet.ReadInt32("CaseID");
        }

        [Parser(Opcode.CMSG_GM_TICKET_GET_TICKET)]
        [Parser(Opcode.CMSG_GM_TICKET_GET_CASE_STATUS)]
        [Parser(Opcode.CMSG_GM_TICKET_GET_SYSTEM_STATUS)]
        [Parser(Opcode.SMSG_GM_TICKET_RESPONSE_ERROR)]
        public static void HandleGMTicketZero(Packet packet)
        {
        }
    }
}
