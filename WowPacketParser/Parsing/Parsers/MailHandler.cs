using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class MailHandler
    {
        [Parser(Opcode.SMSG_RECEIVED_MAIL)]
        public static void HandleReceivedMail(Packet packet)
        {
            var unkInt = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + unkInt);
        }
    }
}
