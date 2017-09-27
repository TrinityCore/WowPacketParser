using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class TicketHandler
    {
        [Parser(Opcode.SMSG_GM_TICKET_STATUS_UPDATE)]
        public static void HandleGMTicketStatusUpdate(Packet packet)
        {
            packet.ReadInt32("StatusInt");
        }

        [Parser(Opcode.SMSG_GM_TICKET_GET_SYSTEM_STATUS)]
        public static void HandleGMTicketSystemStatus(Packet packet)
        {
            packet.ReadUInt32("Response");
        }

        [Parser(Opcode.SMSG_GMRESPONSE_RECEIVED)]
        public static void HandleGMResponseReceived(Packet packet)
        {
            packet.ReadUInt32("Response ID");
            packet.ReadUInt32("Ticket ID");
            packet.ReadCString("Description");
            for (var i = 1; i <= 4; i++)
                packet.ReadCString("Response", i);
        }

        [Parser(Opcode.SMSG_GM_TICKET_GET_TICKET)]
        public static void HandleGetGMTicket(Packet packet)
        {
            var ticketStatus = packet.ReadInt32E<GMTicketStatus>("TicketStatus");
            if (ticketStatus != GMTicketStatus.HasText)
                return;

            packet.ReadInt32("TicketID");
            packet.ReadCString("Description");
            packet.ReadByte("Category");
            packet.ReadSingle("Ticket Age");
            packet.ReadSingle("Oldest Ticket Time");
            packet.ReadSingle("Update Time");
            packet.ReadBool("Assigned to GM");
            packet.ReadBool("Opened by GM");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595))
            {
                packet.ReadCString("Average wait time Text");
                packet.ReadUInt32("Average wait time");
            }
        }

        [Parser(Opcode.SMSG_GM_TICKET_CREATE)]
        [Parser(Opcode.SMSG_GM_TICKET_UPDATE_TEXT)]
        [Parser(Opcode.SMSG_GM_TICKET_DELETE_TICKET)]
        public static void HandleCreateUpdateGMTicket(Packet packet)
        {
            packet.ReadInt32E<GMTicketResponse>("TicketResponse");
        }

        [Parser(Opcode.SMSG_GMRESPONSE_STATUS_UPDATE)]
        public static void HandleGMResponseStatusUpdate(Packet packet)
        {
            packet.ReadByte("Get survey");
        }

        [Parser(Opcode.CMSG_GM_TICKET_GET_TICKET)]
        [Parser(Opcode.CMSG_GM_TICKET_GET_SYSTEM_STATUS)]
        [Parser(Opcode.CMSG_GM_TICKET_RESPONSE_RESOLVE)]
        [Parser(Opcode.CMSG_GM_TICKET_DELETE_TICKET)]
        public static void HandleTicketZeroLengthPackets(Packet packet)
        {
        }

    }
}
