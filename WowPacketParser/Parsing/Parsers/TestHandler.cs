using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class TestHandler
    {
        [Parser(62540)]
        public static void Handle62540(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
                packet.ReadInt32("Unk");

            for (var i = 0; i < count; i++)
                packet.ReadInt32("Unk");

            packet.ReadInt32("Unk");

            for (var i = 0; i < count; i++)
                packet.ReadInt32("Unk");

            for (var i = 0; i < count; i++)
                packet.ReadInt32("Unk");

            for (var i = 0; i < count; i++)
                packet.ReadInt32("Unk");

            for (var i = 0; i < count; i++)
                packet.ReadInt64("Unk");
        }

        [Parser(41694)]
        public static void Handle41694(Packet packet)
        {
            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
                packet.ReadInt32("Unk");

            for (var i = 0; i < count; i++)
                packet.ReadGuid("Unk");

            for (var i = 0; i < count; i++)
            {
                var count2 = packet.ReadInt32("Unk");

                for (var j = 0; j < count2; j++)
                    packet.ReadInt64("Unk");
            }

            for (var i = 0; i < count; i++)
                packet.ReadInt32("Unk");

            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("Unk");
                packet.ReadInt32("Unk");
            }

            for (var i = 0; i < count; i++)
                packet.ReadInt32("Unk");

            for (var i = 0; i < count; i++)
                packet.Writer.WriteLine(packet.ReadPackedTime());
        }

        [Parser(30332)]
        public static void Handle30332(Packet packet)
        {
            packet.ReadInt64("Unk"); // Not guid
        }

        [Parser(13438)]
        public static void Handle13438(Packet packet)
        {
            packet.ReadInt64("Unk");
            packet.ReadInt64("Unk");
            packet.ReadInt64("Unk");
            packet.ReadInt64("Unk");
            packet.ReadInt64("Unk");
        }

        [Parser(13004)]
        public static void Handle13004(Packet packet)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 4; j++)
                    packet.ReadInt32("Unk");
            }
        }

        [Parser(13516)]
        public static void Handle13516(Packet packet)
        {
            packet.ReadByte("Unk");
            packet.ReadInt32("Unk");
            packet.ReadSingle("Unk");
            packet.ReadInt32("Unk");
        }

        [Parser(Opcode.TEST_422_26948)]
        public static void Handle26948(Packet packet)
        {
            packet.AsHex();
            using (var uncompressed = packet.Inflate(packet.ReadInt32()))
                uncompressed.AsHex();
        }
    }
}
