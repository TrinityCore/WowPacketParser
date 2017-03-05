using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            packet.Translator.ReadSingle("Unk Float");
            var guid = packet.Translator.StartBitStream(2, 3, 7, 4, 0, 1, 5, 6);
            packet.Translator.ParseBitStream(guid, 0, 1, 3, 4, 7, 6, 2, 5);
            CoreParsers.SessionHandler.LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            packet.Translator.ReadUInt32("Key pt1");
            packet.Translator.ReadUInt32("Key pt2");
            packet.Translator.ReadUInt32("Key pt3");
            packet.Translator.ReadUInt32("Key pt4");
            packet.Translator.ReadUInt32("Key pt5");
            packet.Translator.ReadUInt32("Key pt6");
            packet.Translator.ReadUInt32("Key pt7");
            packet.Translator.ReadUInt32("Key pt8");
            packet.Translator.ReadByte("Unk Byte");
            packet.Translator.ReadUInt32("Server Seed");
        }

        [Parser(Opcode.CMSG_AUTH_SESSION)]
        public static void HandleAuthSession(Packet packet)
        {
            var sha = new byte[20];
            packet.Translator.ReadUInt32("UInt32 2");
            sha[4] = packet.Translator.ReadByte();
            packet.Translator.ReadUInt32("UInt32 4");
            packet.Translator.ReadByte("Unk Byte");
            sha[19] = packet.Translator.ReadByte();
            sha[12] = packet.Translator.ReadByte();
            sha[9]= packet.Translator.ReadByte();
            sha[6] = packet.Translator.ReadByte();
            sha[18] = packet.Translator.ReadByte();
            sha[17] = packet.Translator.ReadByte();
            sha[8] = packet.Translator.ReadByte();
            sha[13] = packet.Translator.ReadByte();
            sha[1] = packet.Translator.ReadByte();
            sha[10] = packet.Translator.ReadByte();
            sha[11] = packet.Translator.ReadByte();
            sha[15] = packet.Translator.ReadByte();
            packet.Translator.ReadUInt32("Client seed");
            sha[3] = packet.Translator.ReadByte();
            sha[14] = packet.Translator.ReadByte();
            sha[7] = packet.Translator.ReadByte();
            packet.Translator.ReadInt64("Int64");
            packet.Translator.ReadByte("Unk Byte");
            packet.Translator.ReadUInt32("UInt32 1");
            sha[5] = packet.Translator.ReadByte();
            sha[0] = packet.Translator.ReadByte();
            packet.Translator.ReadInt16E<ClientVersionBuild>("Client Build");//20
            sha[16] = packet.Translator.ReadByte();
            sha[2] = packet.Translator.ReadByte();
            packet.Translator.ReadUInt32("UInt32 3");

            var addons = new Packet(packet.Translator.ReadBytes(packet.Translator.ReadInt32()), packet.Opcode, packet.Time, packet.Direction,
                packet.Number, packet.Formatter, packet.FileName);
            CoreParsers.AddonHandler.ReadClientAddonsList(addons);
            addons.ClosePacket(false);

            var size = (int)packet.Translator.ReadBits(11);
            packet.Translator.ReadBit("Unk bit");
            packet.Translator.ResetBitReader();
            packet.Translator.ReadBytesString("Account name", size);
            packet.AddValue("Proof SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            var isQueued = packet.Translator.ReadBit("Is In Queue");
            var hasAccountData = packet.Translator.ReadBit("Has Account Data");

            var classCount = 0u;
            var raceCount = 0u;
            var bits23 = 0u;
            var bits6 = 0u;
            bool bit112 = false;
            bool bit116 = false;
            uint[] count1096 = null;
            uint[] len4 = null;
            uint[] len5 = null;
            uint[] len69 = null;
            uint[] len261 = null;
            if (hasAccountData)
            {
                bits23 = packet.Translator.ReadBits("Unkbits", 21);
                packet.Translator.ReadBit("Unk 1");
                classCount = packet.Translator.ReadBits("Class Activation Count", 23);
                packet.Translator.ReadBit("Unk 2");

                bit112 = packet.Translator.ReadBit();
                count1096 = new uint[bits23];
                len4 = new uint[bits23];
                len69 = new uint[bits23];
                for (var i = 0; i < bits23; ++i)
                {
                    count1096[i] = packet.Translator.ReadBits(23);
                    len4[i] = packet.Translator.ReadBits(7);
                    len69[i] = packet.Translator.ReadBits(10);
                }

                bits6 = packet.Translator.ReadBits(21);
                len5 = new uint[bits6];
                len261 = new uint[bits6];
                for (var i = 0; i < bits6; ++i)
                {
                    len261[i] = packet.Translator.ReadBits(8);
                    packet.Translator.ReadBit("unk bit", i);
                    len5[i] = packet.Translator.ReadBits(8);
                }
                bit116 = packet.Translator.ReadBit();
                raceCount = packet.Translator.ReadBits("Race Activation Count", 23);
            }

            if (isQueued)
                packet.Translator.ReadBit("unk0");

            packet.Translator.ResetBitReader();

            if (isQueued)
                packet.Translator.ReadUInt32("Unk 11");

            if (hasAccountData)
            {
                for (var i = 0; i < classCount; ++i)
                {
                    packet.Translator.ReadByteE<Class>("Class", i);
                    packet.Translator.ReadByteE<ClientType>("Class Expansion", i);
                }

                packet.Translator.ReadUInt32("Unk int 12");
                for (var i = 0; i < raceCount; ++i)
                {
                    packet.Translator.ReadByteE<ClientType>("Race Expansion", i);
                    packet.Translator.ReadByteE<Race>("Race", i);
                }

                packet.Translator.ReadInt32("Unk int 10");
                packet.Translator.ReadInt32("Unk int 14");
                if (bit112)
                    packet.Translator.ReadInt16("UnkInt16 1");

                for (var i = 0; i < bits23; ++i)
                {
                    packet.Translator.ReadInt32("Unk int32", i);
                    for (var j = 0; j < count1096[i]; ++j)
                    {
                        packet.Translator.ReadByte("Unk byte 1", i, j);
                        packet.Translator.ReadByte("Unk byte 0", i, j);
                    }
                    packet.Translator.ReadWoWString("String 1", len69[i], i);
                    packet.Translator.ReadWoWString("String 2", len4[i], i);
                }
                packet.Translator.ReadByteE<ClientType>("Player Expansion");
                packet.Translator.ReadByteE<ClientType>("Account Expansion");
                if (bit116)
                    packet.Translator.ReadInt16("UnkInt16 2");

                for (var i = 0; i < bits6; ++i)
                {
                    packet.Translator.ReadWoWString("Realm", len5[i], i);
                    packet.Translator.ReadWoWString("Realm", len261[i], i);
                    packet.Translator.ReadInt32("Realm Id", i);
                }
                packet.Translator.ReadInt32("Unk int 5");
                packet.Translator.ReadInt32("Unk int 11");
            }

            packet.Translator.ReadByteE<ResponseCode>("Auth Code");
        }

        [Parser(Opcode.SMSG_MOTD)]
        public static void HandleMessageOfTheDay(Packet packet)
        {
            var lineCount = packet.Translator.ReadBits("Line Count", 4);
            var lineLength = new int[lineCount];
            for (var i = 0; i < lineCount; i++)
                lineLength[i] = (int)packet.Translator.ReadBits(7);

            for (var i = 0; i < lineCount; i++)
                packet.Translator.ReadWoWString("Line", lineLength[i], i);
        }

        [Parser(Opcode.SMSG_CONNECT_TO)]
        public static void HandleRedirectClient(Packet packet)
        {
            packet.Translator.ReadUInt64("Unk Long");
            packet.Translator.ReadByte("Unk Byte");
            packet.Translator.ReadUInt32("Token");
            packet.Translator.ReadBytes("RSA Hash", 0x100);
        }

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION)]
        public static void HandleRedirectAuthProof(Packet packet)
        {
            var sha = new byte[20];
            packet.Translator.ReadInt64("Int64 Unk1"); // Key or DosResponse
            packet.Translator.ReadInt64("Int64 Unk2"); // Key or DosResponse
            sha[0] = packet.Translator.ReadByte();
            sha[12] = packet.Translator.ReadByte();
            sha[1] = packet.Translator.ReadByte();
            sha[4] = packet.Translator.ReadByte();
            sha[5] = packet.Translator.ReadByte();
            sha[7] = packet.Translator.ReadByte();
            sha[18] = packet.Translator.ReadByte();
            sha[2] = packet.Translator.ReadByte();
            sha[19] = packet.Translator.ReadByte();
            sha[8] = packet.Translator.ReadByte();
            sha[11] = packet.Translator.ReadByte();
            sha[13] = packet.Translator.ReadByte();
            sha[14] = packet.Translator.ReadByte();
            sha[9] = packet.Translator.ReadByte();
            sha[6] = packet.Translator.ReadByte();
            sha[3] = packet.Translator.ReadByte();
            sha[10] = packet.Translator.ReadByte();
            sha[15] = packet.Translator.ReadByte();
            sha[17] = packet.Translator.ReadByte();
            sha[16] = packet.Translator.ReadByte();
            packet.AddValue("SHA-1 Hash", sha);
        }
    }
}
