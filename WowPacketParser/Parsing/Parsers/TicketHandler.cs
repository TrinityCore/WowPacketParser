using System;

using WowPacketParser.Misc;
using WowPacketParser.Enums;

namespace WowPacketParser.Parsing.Parsers
{
    public static class TicketHandler
    {
        [Parser(Opcode.CMSG_GMSURVEY_SUBMIT)]
        public static void HandleGMSurveySubmit(Packet packet)
        {
            var count = packet.ReadUInt32("Survey Question Count");
            for (var i = 0; i < count; ++i)
            {
                var gmsurveyid = packet.ReadUInt32("GM Survey Id", i);
                if (gmsurveyid == 0)
                    break;
                packet.ReadByte("Question Number", i);
                packet.ReadCString("Answer", i);
            }
            packet.ReadCString("Comment");
            
        }

        [Parser(Opcode.CMSG_GMTICKET_CREATE)]
        public static void HandleGMTicketCreate(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");
            packet.ReadVector3("Position");
            packet.ReadCString("Text");
            packet.ReadUInt32("Need Response");
            packet.ReadBoolean("Need GM interaction");
            var count = packet.ReadInt32("Count");

            for (int i = 0; i < count; i++)
                packet.WriteLine("[" + i + "] Sent: " + (packet.Time - packet.ReadTime()).ToFormattedString());

            if (count == 0)
                packet.ReadInt32("Unk Int32");
            else
            {
                var decompCount = packet.ReadInt32();
                packet = packet.Inflate(decompCount);
                packet.WriteLine(packet.ReadCString());
            }
        }

        [Parser(Opcode.SMSG_GM_TICKET_STATUS_UPDATE)]
        public static void HandleGMTicketStatusUpdate(Packet packet)
        {
              packet.ReadUInt32("Update");
        }

        [Parser(Opcode.SMSG_GMTICKET_SYSTEMSTATUS)]
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

        [Parser(Opcode.SMSG_GMTICKET_GETTICKET)]
        public static void HandleGetGMTicket(Packet packet)
        {
            var unk = packet.ReadInt32("Unk UInt32");
            if (unk != 6)
                return;

            packet.ReadInt32("TicketID");
            packet.ReadCString("Description");
            packet.ReadByte("Category");
            packet.ReadSingle("Ticket Age");
            packet.ReadSingle("Oldest Ticket Time");
            packet.ReadSingle("Update Time");
            packet.ReadBoolean("Assigned to GM");
            packet.ReadBoolean("Opened by GM");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595))
            {
                packet.ReadCString("Average wait time Text");
                packet.ReadUInt32("Average wait time");
            }
        }

        [Parser(Opcode.SMSG_GMTICKET_CREATE)]
        [Parser(Opcode.SMSG_GMTICKET_UPDATETEXT)]
        [Parser(Opcode.SMSG_GMTICKET_DELETETICKET)]
        public static void HandleCreateUpdateGMTicket(Packet packet)
        {
            packet.ReadInt32("Unk UInt32");
        }

        [Parser(Opcode.SMSG_GMRESPONSE_STATUS_UPDATE)]
        public static void HandleGMResponseStatusUpdate(Packet packet)
        {
            packet.ReadByte("Get survey");
        }

        [Parser(Opcode.CMSG_GMTICKET_GETTICKET)]
        [Parser(Opcode.CMSG_GMTICKET_SYSTEMSTATUS)]
        [Parser(Opcode.CMSG_GMRESPONSE_RESOLVE)]
        [Parser(Opcode.CMSG_GMTICKET_DELETETICKET)]
        public static void HandleTicketZeroLengthPackets(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_COMPLAIN)]
        public static void HandleComplain(Packet packet)
        {
            packet.ReadBoolean("Unk bool");
            packet.ReadGuid("Guid");
            packet.ReadInt32("Unk Int32");
            packet.ReadInt32("Unk Int32");
            packet.ReadInt32("Unk Int32");
            packet.ReadInt32("Unk Int32");
            packet.ReadCString("Complain");
        }
    }
}
