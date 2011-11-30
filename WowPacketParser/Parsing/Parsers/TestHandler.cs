using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class TestHandler
    {
        [Parser(Opcode.TEST_43_34595)]
        public static void HandleCompressedUnk43_34595(Packet packet)
        {
            HandleUnk43_34595(packet.Inflate(packet.ReadInt32()));
        }

        public static void HandleUnk43_34595(Packet packet)
        {
            packet.Writer.WriteLine(packet.AsHex());
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
