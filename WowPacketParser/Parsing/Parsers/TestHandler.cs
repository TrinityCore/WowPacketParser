using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class TestHandler
    {
        [Parser(Opcode.SMSG_COMPRESSED_MULTIPLE_PACKETS)]
        public static void HandleCompressedMultiplePackets(Packet packet)
        {
            HandleMultiplePackets(packet.Inflate(packet.ReadInt32()));
        }

        [Parser(Opcode.SMSG_MULTIPLE_PACKETS)]
        public static void HandleMultiplePackets(Packet packet)
        {
            packet.Writer.WriteLine(packet.AsHex());
            packet.ReadToEnd(); // Parsing statistics will be a lie
        }

        [Parser(Opcode.TEST_422_41036)]
        public static void HandleUnk422_41036(Packet packet)
        {
            var count = packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadUInt32("Unk1", i);
                packet.ReadUInt32("Unk2", i);
                packet.ReadUInt32("Unk3", i);
            }
        }
    }
}
