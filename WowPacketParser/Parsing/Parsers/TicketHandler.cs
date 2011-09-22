using System;
using WowPacketParser.Misc;
using WowPacketParser.Enums;

namespace WowPacketParser.Parsing.Parsers
{
    public static class TicketHandler
    {
        [Parser(Opcode.SMSG_GMTICKET_SYSTEMSTATUS)]
        public static void HandleTalentsInvoluntarilyReset(Packet packet)
        {
              packet.ReadUInt32("Response"); // Boolean?
        }

        [Parser(Opcode.SMSG_GMRESPONSE_RECEIVED)]
        public static void HandleGMResponseReceived(Packet packet)
        {
            for (var i = 0; i < 8; i++)
            {
                packet.ReadByte("Unk " + i);
            }

            packet.ReadCString("Text");

            packet.ReadCString("Response");
        
            for (var i = 0; i < 3; i++) // Always 0?
            {
                packet.ReadByte("Unk2 " + i);
            }
        }

        [Parser(Opcode.SMSG_GMTICKET_GETTICKET)]
        public static void HandleGetTicket(Packet packet)
        {
            packet.ReadInt32("Unknown");
        }

        [Parser(Opcode.CMSG_GMTICKET_GETTICKET)]
        [Parser(Opcode.CMSG_GMTICKET_SYSTEMSTATUS)]
        public static void HandleTicketZeroLengthPackets(Packet packet)
        {
        }
    }
}
