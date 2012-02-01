using System;

using WowPacketParser.Misc;
using WowPacketParser.Enums;

namespace WowPacketParser.Parsing.Parsers
{
    public static class TicketHandler
    {
        [Parser(Opcode.CMSG_GMTICKET_CREATE)]
        public static void HandleGMTicketCreate(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");
            var vector = packet.ReadVector3();
            packet.WriteLine("Position: {0}", vector);
            packet.ReadCString("Text");
            packet.ReadUInt32("Unk UInt32 1");
            packet.ReadBoolean("Need Response");
            // FIXME: 3.3.3a has many more data here..
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
            packet.ReadUInt32("Unk 1");
            packet.ReadUInt32("Unk 2");
            packet.ReadCString("Text");
            for (var i = 1; i <= 4; i++)
                packet.ReadCString("Response", i);
        }

        [Parser(Opcode.SMSG_GMTICKET_GETTICKET)]
        [Parser(Opcode.SMSG_GMTICKET_CREATE)]
        [Parser(Opcode.SMSG_GMTICKET_UPDATETEXT)]
        public static void HandleGetTicket(Packet packet)
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
        public static void HandleTicketZeroLengthPackets(Packet packet)
        {
        }
    }
}
