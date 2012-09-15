using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class TestHandler
    {

        [Parser(16423)]
        public static void Handle16423(Packet packet)
        {
            if (packet.CanRead())
                packet.ReadByte("Unk Byte");
        }

        [Parser(0x8703, ClientVersionBuild.V4_3_0_15005)] // 4.3.0
        [Parser(0x8C1E, ClientVersionBuild.V4_3_0_15005)] // 4.3.0
        [Parser(0x9431, ClientVersionBuild.V4_3_0_15005)] // 4.3.0
        public static void HandleGenericCompressed(Packet packet)
        {
            using (var newpkt = packet.Inflate(packet.ReadInt32()))
                newpkt.AsHex();
        }

        [Parser(18438)]
        public static void Handle18438(Packet packet)
        {
            packet.ReadInt32("Unk Int 32"); //error related
        }

        [Parser(17958, ClientVersionBuild.V4_3_4_15595)] // 4.3.4
        [Parser(18997)] // 4.3.4
        [Parser(26389)] // 4.3.4
        [Parser(3860)] // 4.3.4
        public static void Handle18997(Packet packet)
        {
            packet.ReadPackedGuid("Guid");
            packet.ReadInt16("AnimKit.dbc Id");
        }

        [Parser(17205)] // 4.3.4
        public static void Handle17205(Packet packet)
        {
            packet.ReadPackedGuid("Guid");
            packet.ReadInt32("Unk");
        }

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

        [Parser(44964)] // 4.0.6a
        public static void Handle44964(Packet packet)
        {
            using (var pkt = packet.Inflate(packet.ReadInt32()))
                pkt.AsHex();
        }

        [Parser(Opcode.TEST_422_9838)]
        public static void Handle9838(Packet packet)
        {
            // sub_6C2FD0

            packet.ReadInt32("Unknown 01"); // v3 + 40
            packet.ReadInt32("Unknown 02"); // v3 + 36
            packet.ReadInt32("Unknown 03"); // v3 + 68
            packet.ReadInt32("Unknown 04"); // v3 + 72
            packet.ReadInt32("Unknown 05"); // v3 + 44
            packet.ReadInt32("Unknown 06"); // v3 + 60
            packet.ReadInt32("Unknown 07"); // v3 + 52
            packet.ReadInt32("Unknown 08"); // v3 + 24
            packet.ReadInt32("Unknown 09"); // v3 + 48
            packet.ReadInt32("Unknown 10"); // v3 + 76
            packet.ReadInt32("Unknown 11"); // v3 + 64
            packet.ReadInt32("Unknown 12"); // v3 + 56
            packet.ReadInt32("Unknown 13"); // v3 + 20
            packet.ReadInt32("Unknown 14"); // v3 + 32
            packet.ReadInt32("Unknown 15"); // v3 + 16
            packet.ReadInt32("Unknown 16"); // v3 + 84
            packet.ReadInt32("Unknown 17"); // v3 + 80
            packet.ReadInt32("Unknown 18"); // v3 + 28
        }

        [Parser(Opcode.TEST_422_13022, ClientVersionBuild.V4_2_2_14545)]
        public static void Handle13022(Packet packet)
        {
            var guid = packet.StartBitStream(3, 7, 6, 2, 5, 4, 0, 1);

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);

            packet.ReadInt32("Unk Int32");

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 6);

            packet.WriteGuid("Unk Guid?", guid);
        }
    }
}
