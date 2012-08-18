using System;
using PacketParser.DataStructures;
using PacketParser.Misc;
using PacketParser.Enums;

namespace PacketParser.Parsing.Parsers
{
    public static class TicketHandler
    {
        [Parser(Opcode.CMSG_GMSURVEY_SUBMIT)]
        public static void HandleGMSurveySubmit(Packet packet)
        {
            var count = packet.ReadUInt32("Survey Question Count");
            packet.StoreBeginList("Surveys");
            for (var i = 0; i < count; ++i)
            {
                var gmsurveyid = packet.ReadUInt32("GM Survey Id", i);
                if (gmsurveyid == 0)
                    break;
                packet.ReadByte("Question Number", i);
                packet.ReadCString("Answer", i);
            }
            packet.StoreEndList();
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

            packet.StoreBeginList("list");
            for (int i = 0; i < count; i++)
                packet.Store("Sent", (packet.Time - packet.ReadTime()).ToFormattedString(), i);
            packet.StoreEndList();

            if (count == 0)
                packet.ReadInt32("Unk Int32");
            else
            {
                var decompCount = packet.ReadInt32();
                packet.Inflate(decompCount);
                packet.Store("String", packet.ReadCString());
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
            packet.StoreBeginList("Responses");
            for (var i = 1; i <= 4; i++)
                packet.ReadCString("Response", i);
            packet.StoreEndList();
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

        [Parser(Opcode.CMSG_GMTICKET_UPDATETEXT)]
        public static void HandleGMTicketUpdatetext(Packet packet)
        {
            packet.ReadCString("New Ticket Text");
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
            packet.ReadBoolean("Unk bool 1");
            packet.ReadGuid("Guid");
            packet.ReadInt32("Unk Int32 2");
            packet.ReadInt32("Unk Int32 3");
            packet.ReadInt32("Unk Int32 4");
            packet.ReadInt32("Unk Int32 5");
            packet.ReadCString("Complain");
        }

        [Parser(Opcode.CMSG_SUBMIT_BUG)]
        public static void HandleSubmitBug(Packet packet)
        {
            var length = packet.ReadBits(12);
            var pos = new Vector4();

            packet.ReadWoWString("Text", length);
            pos.Y = packet.ReadSingle();
            pos.Z = packet.ReadSingle();
            pos.X = packet.ReadSingle();
            packet.ReadInt32("Unk Int32");
            pos.O = packet.ReadSingle();
            packet.Store("Position", pos);
        }

        [Parser(Opcode.CMSG_SUBMIT_COMPLAIN)]
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

            packet.StoreBeginList("Complains");
            for (int i = 0; i < count; ++i)
            {
                packet.ReadTime("Time", i);
                packet.ReadWoWString("Data", strLength[i], i);
            }
            packet.StoreEndList();

            packet.ReadInt32("Unk Int32 2");  // ##

            packet.StoreBitstreamGuid("Guid", guid);
            packet.Store("Position", pos);
        }
    }
}
