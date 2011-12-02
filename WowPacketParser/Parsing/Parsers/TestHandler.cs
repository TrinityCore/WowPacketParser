using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Enums.Version;

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
            var i = 0;
            while (packet.CanRead())
            {
                var opcode = packet.ReadUInt16();
                packet.Writer.WriteLine("[{0}] Opcode: {1} ({2})", i, Opcodes.GetOpcodeName(opcode), opcode.ToString("X4"));
                var len = packet.ReadUInt16("Length", i);
                var bytes = packet.ReadBytes(len);
                var newpacket = new Packet(bytes, opcode, packet.Time, packet.Direction, packet.Number, packet.Writer);
                Handler.Parse(newpacket, isMultiple: true);
                ++i;
            }
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
