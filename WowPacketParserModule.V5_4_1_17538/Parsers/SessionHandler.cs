using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
{
    public static class SessionHandler
    {

        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            packet.ReadSingle("Unk Float");
            var guid = packet.StartBitStream(6, 7, 1, 5, 2, 4, 3, 0);
            packet.ParseBitStream(guid, 7, 6, 0, 1, 4, 3, 2, 5);
            CoreParsers.SessionHandler.LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32("Key pt1");
            packet.ReadUInt32("Key pt2");
            packet.ReadUInt32("Key pt3");
            packet.ReadUInt32("Key pt4");
            packet.ReadUInt32("Key pt5");
            packet.ReadUInt32("Key pt6");
            packet.ReadUInt32("Key pt7");
            packet.ReadUInt32("Key pt8");
            packet.ReadUInt32("Server Seed");
        }

        [Parser(Opcode.CMSG_AUTH_SESSION)]
        public static void HandleAuthSession(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadUInt32("UInt32 2");
            sha[14] = packet.ReadByte();
            sha[8] = packet.ReadByte();
            packet.ReadUInt32("UInt32 4");
            sha[10] = packet.ReadByte();
            sha[19] = packet.ReadByte();
            sha[16] = packet.ReadByte();
            sha[13] = packet.ReadByte();
            sha[4] = packet.ReadByte();
            packet.ReadByte("Unk Byte");
            sha[9] = packet.ReadByte();
            sha[0] = packet.ReadByte();
            packet.ReadUInt32("Client seed");
            sha[5] = packet.ReadByte();
            sha[2] = packet.ReadByte();
            packet.ReadInt16E<ClientVersionBuild>("Client Build");//20
            sha[12] = packet.ReadByte();
            packet.ReadUInt32("UInt32 3");
            sha[18] = packet.ReadByte();
            sha[17] = packet.ReadByte();
            sha[11] = packet.ReadByte();
            packet.ReadInt64("Int64");
            sha[7] = packet.ReadByte();
            sha[1] = packet.ReadByte();
            sha[3] = packet.ReadByte();
            packet.ReadByte("Unk Byte");
            sha[6] = packet.ReadByte();
            packet.ReadUInt32("UInt32 1");
            sha[15] = packet.ReadByte();
            //packet.ReadUInt32("UInt32 5");

            var addons = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Writer, packet.FileName);
            CoreParsers.AddonHandler.ReadClientAddonsList(addons);
            addons.ClosePacket(false);

            var size = (int)packet.ReadBits(11);
            packet.ReadBit("Unk bit");
            packet.ResetBitReader();
            packet.ReadBytesString("Account name", size);
            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.SMSG_MOTD)]
        public static void HandleMessageOfTheDay(Packet packet)
        {
            var lineCount = packet.ReadBits("Line Count", 4);
            var lineLength = new int[lineCount];

            for (var i = 0; i < lineCount; i++)
                lineLength[i] = (int)packet.ReadBits(7);

            for (var i = 0; i < lineCount; i++)
                packet.ReadWoWString("Line", lineLength[i], i);
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            var bit4 = false;
            var bit32 = false;
            var bit68 = false;
            var bit6C = false;
            var bit70 = false;
            var bit7C = false;

            var bits14 = 0;
            var classCount = 0u;
            var raceCount = 0u;
            var bits58 = 0;
            uint[] bits0 = null;
            uint[] bits1 = null;
            uint[] bits45 = null;
            uint[] bitsEA = null;
            uint[] bits448 = null;

            packet.ReadByte("Byte84");
            var isQueued = packet.ReadBit("isQueued");

            if (bit7C)
                bit7C = packet.ReadBit();

            var hasAccountData = packet.ReadBit("Has Account Data");

            if (hasAccountData)
            {
                bit70 = packet.ReadBit("unk 1");
                bits14 = (int)packet.ReadBits(21);
                bits58 = (int)packet.ReadBits(21);
                classCount = packet.ReadBits(23);

                bits448 = new uint[bits58];
                bitsEA = new uint[bits58];
                bits45 = new uint[bits58];

                for (var i = 0; i < bits58; ++i)
                {
                    bits448[i] = packet.ReadBits(23);
                    bitsEA[i] =  packet.ReadBits(7);
                    bits45[i] = packet.ReadBits(10);
                }


                bit6C = packet.ReadBit();
                bit68 = packet.ReadBit();

                bits0 = new uint[bits14];
                bits1 = new uint[bits14];

                for (var i = 0; i < bits14; ++i)
                {
                    bits0[i] = packet.ReadBits(8);
                    bit4 = packet.ReadBit("unk bit", i);
                    bits1[i] = packet.ReadBits(8);
                }


                bit32 = packet.ReadBit();
                raceCount = packet.ReadBits("Race Activation Count", 23);
            }

            if (hasAccountData)
            {
                packet.ReadInt32("Int2C");
                packet.ReadInt32("Int28");
                for (var i = 0; i < bits58; ++i)
                {
                    for (var j = 0; j < bitsEA[i]; ++j)
                    {
                        packet.ReadByte("Byte5C", i, j);
                        packet.ReadByte("Byte5C", i, j);
                    }

                    packet.ReadInt32("Int5C");
                    packet.ReadWoWString("String5C", bits45[i], i);
                    packet.ReadWoWString("String5C", bits448[i], i);
                }

                packet.ReadByte("Byte31");

                for (var i = 0; i < raceCount; ++i)
                {
                    packet.ReadByteE<ClientType>("Race Expansion", i);
                    packet.ReadByteE<Race>("Race", i);
                }

                packet.ReadByte("Byte30");
                packet.ReadInt32("Int34");

                for (var i = 0; i < classCount; ++i)
                {
                    packet.ReadByteE<Class>("Class", i);
                    packet.ReadByteE<ClientType>("Class Expansion", i);
                }

                if (bit70)
                    packet.ReadInt16("Int6E");

                for (var i = 0; i < bits14; ++i)
                {
                    packet.ReadWoWString("Realm", bits0[i], i);
                    packet.ReadWoWString("Realm", bits1[i], i);
                    packet.ReadInt32("Realm Id", i);
                }

                packet.ReadInt32("Int10");
                packet.ReadInt32("Int24");

                if (bit6C)
                    packet.ReadInt16("Int6A");
            }

            if (isQueued)
                packet.ReadInt32("Int78");
        }
    }
}