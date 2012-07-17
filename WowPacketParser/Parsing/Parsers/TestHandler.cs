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
                packet.ReadPackedTime("Time");
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

            packet.ParseBitStream(guid, 4, 1, 5, 2);

            packet.ReadInt32("Unk Int32");

            packet.ParseBitStream(guid, 0, 3, 7, 6);

            packet.ToGuid("Unk Guid?", guid);
        }

        [Parser(Opcode.TEST_434_31006, ClientVersionBuild.V4_3_4_15595)]
        public static void Handle31006(Packet packet)
        {
            var guid = packet.StartBitStream(1, 5, 7, 3, 2, 4, 0, 6);

            packet.ParseBitStream(guid, 4, 7, 0, 5, 1, 6, 2, 3);

            packet.ToGuid("Unk Guid?", guid);
        }

        [Parser(4646)] // 4.3.4 (CMSG_GUILD_ROSTER?)
        public static void HandleGuildRoster434(Packet packet)
        {
            var bytes1 = new byte[8];
            var bytes2 = new byte[8];
            bytes2[2] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes2[3] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes1[6] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes1[0] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes2[7] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes1[2] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes2[6] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes2[4] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes1[1] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes2[5] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes1[4] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes1[3] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes2[0] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes1[5] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes2[1] = (byte)(packet.ReadBit() ? 1 : 0);
            bytes1[7] = (byte)(packet.ReadBit() ? 1 : 0);

            if (bytes1[3] != 0) bytes1[3] ^= packet.ReadByte();
            if (bytes2[4] != 0) bytes2[4] ^= packet.ReadByte();
            if (bytes1[7] != 0) bytes1[7] ^= packet.ReadByte();
            if (bytes1[2] != 0) bytes1[2] ^= packet.ReadByte();
            if (bytes1[4] != 0) bytes1[4] ^= packet.ReadByte();
            if (bytes1[0] != 0) bytes1[0] ^= packet.ReadByte();
            if (bytes2[5] != 0) bytes2[5] ^= packet.ReadByte();
            if (bytes1[1] != 0) bytes1[1] ^= packet.ReadByte();
            if (bytes2[0] != 0) bytes2[0] ^= packet.ReadByte();
            if (bytes2[6] != 0) bytes2[6] ^= packet.ReadByte();
            if (bytes1[5] != 0) bytes1[5] ^= packet.ReadByte();
            if (bytes2[7] != 0) bytes2[7] ^= packet.ReadByte();
            if (bytes2[2] != 0) bytes2[2] ^= packet.ReadByte();
            if (bytes2[3] != 0) bytes2[3] ^= packet.ReadByte();
            if (bytes2[1] != 0) bytes2[1] ^= packet.ReadByte();
            if (bytes1[6] != 0) bytes1[6] ^= packet.ReadByte();
            packet.WriteLine("GUID1: {0}", new Guid(BitConverter.ToUInt64(bytes1, 0))); // Doesnt seem to be guid
            packet.WriteLine("GUID2: {0}", new Guid(BitConverter.ToUInt64(bytes2, 0))); // Doesnt seem to be guid
        }
    }
}
