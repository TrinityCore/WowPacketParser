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

        [Parser(Opcode.CMSG_SUPPORT_TICKET_SUBMIT_BUG)]
        public static void HandleSubmitBug(Packet packet)
        {
            var length = packet.ReadBits(12);
            var pos = new Vector4();

            packet.ReadWoWString("Text", length);
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            packet.ReadInt32("Map ID");
            pos.O = packet.ReadSingle();
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.CMSG_SUPPORT_TICKET_SUBMIT_COMPLAINT)]
        public static void HandleSubmitComplain(Packet packet)
        {
            var pos = new Vector4();
            var guid = new byte[8];

            guid[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            var length = packet.ReadBits(12);

            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();

            packet.ReadBits("Unk bits", 4); // ##

            guid[6] = packet.ReadBit();

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 0);

            packet.ReadWoWString("Text", length);

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);

            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            packet.ReadInt32("Unk Int32 1"); // ##
            pos.O = packet.ReadSingle();

            packet.ReadBit("Unk bit"); // ##

            var count = packet.ReadBits("Count", 22);
            var strLength = new uint[count];
            for (int i = 0; i < count; ++i)
                strLength[i] = packet.ReadBits(13);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadTime("Time", i);
                packet.ReadWoWString("Data", strLength[i], i);
            }

            packet.ReadInt32("Unk Int32 2");  // ##

            packet.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }
    }
}
