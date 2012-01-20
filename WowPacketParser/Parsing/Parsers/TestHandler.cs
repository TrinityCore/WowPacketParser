using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using Guid = WowPacketParser.Misc.Guid;

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
            //packet.AsHex();
            using (var uncompressed = packet.Inflate(packet.ReadInt32()))
                uncompressed.AsHex();
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
            var bytes = new byte[8];

            bytes[0] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes[4] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes[5] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes[1] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes[6] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes[3] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes[7] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes[2] = (byte)(packet.ReadBit() ? 1 : 0);

            if (bytes[2] != 0) bytes[2] ^= packet.ReadByte();
            if (bytes[4] != 0) bytes[4] ^= packet.ReadByte();
            if (bytes[7] != 0) bytes[7] ^= packet.ReadByte();
            if (bytes[0] != 0) bytes[0] ^= packet.ReadByte();

            packet.ReadSingle("Unk float");

            if (bytes[5] != 0) bytes[5] ^= packet.ReadByte();
            if (bytes[1] != 0) bytes[1] ^= packet.ReadByte();
            if (bytes[6] != 0) bytes[6] ^= packet.ReadByte();
            if (bytes[3] != 0) bytes[3] ^= packet.ReadByte();

            packet.Writer.WriteLine("GUID: {0}", new Guid(BitConverter.ToUInt64(bytes, 0)));
        }
    }
}
