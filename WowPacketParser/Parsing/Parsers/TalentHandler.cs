using System;
using WowPacketParser.Misc;
using WowPacketParser.Enums;

namespace WowPacketParser.Parsing.Parsers
{
    public static class TalentHandler
    {
        [Parser(Opcode.SMSG_TALENTS_INVOLUNTARILY_RESET)]
        public static void HandleTalentsInvoluntarilyReset(Packet packet)
        {
            var unk = packet.ReadByte();
            Console.WriteLine("Unk Byte: " + unk);
        }
    }
}
