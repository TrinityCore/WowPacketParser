using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class TicketHandler
    {
        [Parser(Opcode.CMSG_GM_TICKET_GET_TICKET)]
        [Parser(Opcode.CMSG_GM_TICKET_GET_CASE_STATUS)]
        [Parser(Opcode.CMSG_GM_TICKET_GET_SYSTEM_STATUS)]
        [Parser(Opcode.SMSG_GM_TICKET_RESPONSE_ERROR)]
        public static void HandleGMTicketZero(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_GM_TICKET_CASE_STATUS)]
        public static void HandleGMTicketCaseStatus(Packet packet)
        {
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_2_20444))
            {
                packet.ReadTime("OldestTicketTime");
                packet.ReadTime("UpdateTime");
            }

            var int24 = packet.ReadInt32("CasesCount");
            for (int i = 0; i < int24; i++)
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

        [Parser(Opcode.SMSG_GM_TICKET_SYSTEM_STATUS)]
        public static void HandleGMTicketSystemStatus(Packet packet)
        {
            packet.ReadInt32("Status");
        }

        [Parser(Opcode.SMSG_GM_TICKET_GET_TICKET_RESPONSE)]
        public static void HandleGMTicketGetTicketResponse(Packet packet)
        {
            packet.ReadInt32("Result");

            var hasInfo = packet.ReadBit("HasInfo");

            // ClientGMTicketInfo
            if (hasInfo)
            {
                packet.ReadInt32("TicketID");
                packet.ReadByte("Category");
                packet.ReadTime("TicketOpenTime");
                packet.ReadTime("OldestTicketTime");
                packet.ReadTime("UpdateTime");
                packet.ReadByte("AssignedToGM");
                packet.ReadByte("OpenedByGM");
                packet.ReadInt32("WaitTimeOverrideMinutes");

                packet.ResetBitReader();

                var bits1 = packet.ReadBits(11);
                var bits2022 = packet.ReadBits(10);

                packet.ReadWoWString("TicketDescription", bits1);
                packet.ReadWoWString("WaitTimeOverrideMessage", bits2022);
            }
        }

        [Parser(Opcode.SMSG_GM_TICKET_RESOLVE_RESPONSE)]
        public static void HandleGMTicketResolveResponse(Packet packet)
        {
            packet.ReadBit("ShowSurvey");
        }

        [Parser(Opcode.SMSG_GM_TICKET_STATUS_UPDATE)]
        public static void HandleGMTicketStatusUpdate(Packet packet)
        {
            packet.ReadInt32("StatusInt");
        }

        [Parser(Opcode.SMSG_GM_TICKET_UPDATE)]
        public static void HandleGMTicketUpdate(Packet packet)
        {
            packet.ReadByte("Result");
        }

        [Parser(Opcode.SMSG_COMPLAINT_RESULT)]
        public static void HandleComplaintResult(Packet packet)
        {
            packet.ReadUInt32("ComplaintType");
            packet.ReadByte("Result");
        }

        [Parser(Opcode.SMSG_GM_TICKET_RESPONSE)]
        public static void HandleGMTicketResponse(Packet packet)
        {
            // TODO: confirm order

            packet.ReadUInt32("TicketID");
            packet.ReadUInt32("ResponseID");

            var descriptionLength = packet.ReadBits(11);
            var responseTextLength = packet.ReadBits(14);

            packet.ReadWoWString("Description", descriptionLength);
            packet.ReadWoWString("ResponseText", responseTextLength);
        }

    }
}
