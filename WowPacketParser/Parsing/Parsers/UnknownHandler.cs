using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using Guid = System.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class UnknownHandler
    {
        [Parser(Opcode.SMSG_UNKNOWN_1240)]
        public static void HandleUnknown1240(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            var str = packet.ReadCString();
            Console.WriteLine("Unk String: " + str);
        }

        [Parser(Opcode.SMSG_UNKNOWN_1295)]
        [Parser(Opcode.CMSG_UNKNOWN_1296)]
        public static void HandleUnknownRedirectPackets(Packet packet)
        {
            var unk = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + unk);
        }

        [Parser(Opcode.CMSG_SET_ALLOW_LOW_LEVEL_RAID1)]
        [Parser(Opcode.CMSG_SET_ALLOW_LOW_LEVEL_RAID2)]
        public static void HandleUnknownLowLevelRaidPackets(Packet packet)
        {
            var unk = packet.ReadBoolean();
            Console.WriteLine("Allow: " + unk);
        }
    }
}
