using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class TicketHandler
    {
        [Parser(Opcode.CMSG_GM_SURVEY_SUBMIT)]
        public static void HandleGMSurveySubmit(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Survey Question Count");
            for (var i = 0; i < count; ++i)
            {
                var gmsurveyid = packet.Translator.ReadUInt32("GM Survey Id", i);
                if (gmsurveyid == 0)
                    break;
                packet.Translator.ReadByte("Question Number", i);
                packet.Translator.ReadCString("Answer", i);
            }

            packet.Translator.ReadCString("Comment");
        }

        [Parser(Opcode.CMSG_GM_TICKET_CREATE)]
        public static void HandleGMTicketCreate(Packet packet)
        {
            packet.Translator.ReadInt32<MapId>("Map ID");
            packet.Translator.ReadVector3("Position");
            packet.Translator.ReadCString("Text");
            packet.Translator.ReadUInt32("Need Response");
            packet.Translator.ReadBool("Need GM interaction");
            var count = packet.Translator.ReadInt32("Count");

            for (int i = 0; i < count; i++)
                packet.AddValue("Sent", (packet.Time - packet.Translator.ReadTime()).ToFormattedString(), i);

            if (count == 0)
                packet.Translator.ReadInt32("Unk Int32");
            else
            {
                var decompCount = packet.Translator.ReadInt32();
                var pkt = packet.Inflate(decompCount);
                pkt.Translator.ReadCString("String");
                pkt.ClosePacket(false);
            }
        }

        [Parser(Opcode.SMSG_GM_TICKET_STATUS_UPDATE)]
        public static void HandleGMTicketStatusUpdate(Packet packet)
        {
            packet.Translator.ReadInt32("StatusInt");
        }

        [Parser(Opcode.SMSG_GM_TICKET_GET_SYSTEM_STATUS)]
        public static void HandleGMTicketSystemStatus(Packet packet)
        {
            packet.Translator.ReadUInt32("Response");
        }

        [Parser(Opcode.SMSG_GMRESPONSE_RECEIVED)]
        public static void HandleGMResponseReceived(Packet packet)
        {
            packet.Translator.ReadUInt32("Response ID");
            packet.Translator.ReadUInt32("Ticket ID");
            packet.Translator.ReadCString("Description");
            for (var i = 1; i <= 4; i++)
                packet.Translator.ReadCString("Response", i);
        }

        [Parser(Opcode.SMSG_GM_TICKET_GET_TICKET)]
        public static void HandleGetGMTicket(Packet packet)
        {
            var ticketStatus = packet.Translator.ReadInt32E<GMTicketStatus>("TicketStatus");
            if (ticketStatus != GMTicketStatus.HasText)
                return;

            packet.Translator.ReadInt32("TicketID");
            packet.Translator.ReadCString("Description");
            packet.Translator.ReadByte("Category");
            packet.Translator.ReadSingle("Ticket Age");
            packet.Translator.ReadSingle("Oldest Ticket Time");
            packet.Translator.ReadSingle("Update Time");
            packet.Translator.ReadBool("Assigned to GM");
            packet.Translator.ReadBool("Opened by GM");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595))
            {
                packet.Translator.ReadCString("Average wait time Text");
                packet.Translator.ReadUInt32("Average wait time");
            }
        }

        [Parser(Opcode.SMSG_GM_TICKET_CREATE)]
        [Parser(Opcode.SMSG_GM_TICKET_UPDATE_TEXT)]
        [Parser(Opcode.SMSG_GM_TICKET_DELETE_TICKET)]
        public static void HandleCreateUpdateGMTicket(Packet packet)
        {
            packet.Translator.ReadInt32E<GMTicketResponse>("TicketResponse");
        }

        [Parser(Opcode.CMSG_GM_TICKET_UPDATE_TEXT)]
        public static void HandleGMTicketUpdatetext(Packet packet)
        {
            packet.Translator.ReadCString("New Ticket Text");
        }

        [Parser(Opcode.SMSG_GMRESPONSE_STATUS_UPDATE)]
        public static void HandleGMResponseStatusUpdate(Packet packet)
        {
            packet.Translator.ReadByte("Get survey");
        }

        [Parser(Opcode.CMSG_GM_TICKET_GET_TICKET)]
        [Parser(Opcode.CMSG_GM_TICKET_GET_SYSTEM_STATUS)]
        [Parser(Opcode.CMSG_GM_TICKET_RESPONSE_RESOLVE)]
        [Parser(Opcode.CMSG_GM_TICKET_DELETE_TICKET)]
        public static void HandleTicketZeroLengthPackets(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_COMPLAINT)]
        public static void HandleComplain(Packet packet)
        {
            bool fromChat = packet.Translator.ReadBool("From Chat"); // false = from mail
            packet.Translator.ReadGuid("Guid");
            packet.Translator.ReadInt32E<Language>("Language");
            packet.Translator.ReadInt32E<ChatMessageType>("Type");
            packet.Translator.ReadInt32("Channel ID");

            if (fromChat)
            {
                packet.Translator.ReadTime("Time ago");
                packet.Translator.ReadCString("Complain");
            }
        }

        [Parser(Opcode.CMSG_SUPPORT_TICKET_SUBMIT_BUG)]
        public static void HandleSubmitBug(Packet packet)
        {
            var length = packet.Translator.ReadBits(12);
            var pos = new Vector4();

            packet.Translator.ReadWoWString("Text", length);
            pos.Y = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            packet.Translator.ReadInt32("Map ID");
            pos.O = packet.Translator.ReadSingle();
            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.CMSG_SUPPORT_TICKET_SUBMIT_COMPLAINT)]
        public static void HandleSubmitComplain(Packet packet)
        {
            var pos = new Vector4();
            var guid = new byte[8];

            guid[5] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();

            var length = packet.Translator.ReadBits(12);

            guid[3] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();

            packet.Translator.ReadBits("Unk bits", 4); // ##

            guid[6] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.ReadWoWString("Text", length);

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);

            pos.Y = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            packet.Translator.ReadInt32("Unk Int32 1"); // ##
            pos.O = packet.Translator.ReadSingle();

            packet.Translator.ReadBit("Unk bit"); // ##

            var count = packet.Translator.ReadBits("Count", 22);
            var strLength = new uint[count];
            for (int i = 0; i < count; ++i)
                strLength[i] = packet.Translator.ReadBits(13);

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadTime("Time", i);
                packet.Translator.ReadWoWString("Data", strLength[i], i);
            }

            packet.Translator.ReadInt32("Unk Int32 2");  // ##

            packet.Translator.WriteGuid("Guid", guid);
            packet.AddValue("Position", pos);
        }
    }
}
