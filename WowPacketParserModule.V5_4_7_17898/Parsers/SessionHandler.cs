using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadSingle("Unk Float");

            packet.Translator.StartBitStream(guid, 7, 6, 0, 4, 5, 2, 3, 1);
            packet.Translator.ParseBitStream(guid, 5, 0, 1, 6, 7, 2, 3, 4);

            CoreParsers.SessionHandler.LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
            packet.Translator.WriteGuid("Guid", guid);
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

        [Parser(Opcode.SMSG_SET_TIME_ZONE_INFORMATION)]
        public static void HandleSetTimeZoneInformation(Packet packet)
        {
            var len1 = packet.Translator.ReadBits(7);
            var len2 = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("Server Location", len2);
            packet.Translator.ReadWoWString("Server Location", len1);
        }

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION)]
        public static void HandleRedirectAuthProof(Packet packet)
        {
            var sha = new byte[20];
            packet.Translator.ReadInt64("Int64 Unk1"); // Key or DosResponse
            packet.Translator.ReadInt64("Int64 Unk2"); // Key or DosResponse
            sha[1] = packet.Translator.ReadByte();
            sha[14] = packet.Translator.ReadByte();
            sha[9] = packet.Translator.ReadByte();
            sha[18] = packet.Translator.ReadByte();
            sha[17] = packet.Translator.ReadByte();
            sha[8] = packet.Translator.ReadByte();
            sha[6] = packet.Translator.ReadByte();
            sha[10] = packet.Translator.ReadByte();
            sha[3] = packet.Translator.ReadByte();
            sha[16] = packet.Translator.ReadByte();
            sha[4] = packet.Translator.ReadByte();
            sha[0] = packet.Translator.ReadByte();
            sha[15] = packet.Translator.ReadByte();
            sha[2] = packet.Translator.ReadByte();
            sha[19] = packet.Translator.ReadByte();
            sha[12] = packet.Translator.ReadByte();
            sha[13] = packet.Translator.ReadByte();
            sha[5] = packet.Translator.ReadByte();
            sha[11] = packet.Translator.ReadByte();
            sha[7] = packet.Translator.ReadByte();

            packet.AddValue("SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }


        [Parser(Opcode.SMSG_CONNECT_TO)]
        public static void HandleRedirectClient(Packet packet)
        {
            packet.Translator.ReadUInt64("Unk Long");
            packet.Translator.ReadBytes("RSA Hash", 0x100);
            packet.Translator.ReadByte("Unk Byte");
            packet.Translator.ReadUInt32("Token");
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            var bit3E = false;
            var bit74 = false;
            var bit78 = false;
            var bit7C = false;
            var bit7E = false;

            var bits20 = 0u;
            var classCount = 0u;
            var raceCount = 0u;
            var bits64 = 0u;

            uint[] bits0 = null;
            uint[] bits0C = null;
            uint[] bits45 = null;
            uint[] bits68 = null;
            uint[] bits448 = null;

            var hasAccountData = packet.Translator.ReadBit("Has Account Data");
            if (hasAccountData)
            {
                bits20 = packet.Translator.ReadBits(21);

                bits0 = new uint[bits20];
                bits0C = new uint[bits20];

                for (var i = 0; i < bits20; ++i)
                {
                    bits0[i] = packet.Translator.ReadBits(8);
                    packet.Translator.ReadBit("unk bit", i);
                    bits0C[i] = packet.Translator.ReadBits(8);
                }

                bit74 = packet.Translator.ReadBit();
                classCount = packet.Translator.ReadBits("Class Activation Count", 23);
                bits64 = packet.Translator.ReadBits(21);

                for (var i = 0; i < bits64; ++i)
                {
                    bits448[i] = packet.Translator.ReadBits(23);
                    bits68[i] = packet.Translator.ReadBits(7);
                    bits45[i] = packet.Translator.ReadBits(10);
                }

                bit7C = packet.Translator.ReadBit();
                raceCount = packet.Translator.ReadBits("Race Activation Count", 23);
                bit3E = packet.Translator.ReadBit();
                bit78 = packet.Translator.ReadBit();
                bit7E = packet.Translator.ReadBit();
            }

            var isQueued = packet.Translator.ReadBit();
            if (isQueued)
            {
                packet.Translator.ReadBit("unk0");
                packet.Translator.ReadInt32("Int10");
            }

            if (hasAccountData)
            {
                for (var i = 0; i < bits64; ++i)
                {
                    for (var j = 0; j < bits448[i]; ++j)
                    {
                        packet.Translator.ReadByte("Unk byte 1", i, j);
                        packet.Translator.ReadByte("Unk byte 0", i, j);
                    }

                    packet.Translator.ReadInt32("Int68");
                    packet.Translator.ReadWoWString("StringED", bits68[i], i);
                    packet.Translator.ReadWoWString("StringED", bits45[i], i);
                }

                if (bit7C)
                    packet.Translator.ReadInt16("Int7A");

                for (var i = 0; i < classCount; ++i)
                {
                    packet.Translator.ReadByteE<ClientType>("Class Expansion", i);
                    packet.Translator.ReadByteE<Class>("Class", i);
                }

                packet.Translator.ReadByte("Byte3C");

                for (var i = 0; i < raceCount; ++i)
                {
                    packet.Translator.ReadByteE<ClientType>("Race Expansion", i);
                    packet.Translator.ReadByteE<Race>("Race", i);
                }

                packet.Translator.ReadInt32("Int34");

                for (var i = 0; i < bits20; ++i)
                {
                    packet.Translator.ReadInt32("RealmId", i);
                    packet.Translator.ReadWoWString("Realm", bits0[i], i);
                    packet.Translator.ReadWoWString("Realm", bits0C[i], i);
                }

                packet.Translator.ReadInt32("Int38");
                packet.Translator.ReadInt32("Int30");
                packet.Translator.ReadInt32("Int40");
                packet.Translator.ReadInt32("Int80");

                if (bit78)
                    packet.Translator.ReadInt16("Int76");

                packet.Translator.ReadByte("Byte3D");
                packet.Translator.ReadInt32("Int1C");
            }

            packet.Translator.ReadByteE<ResponseCode>("Auth Code");
        }

        [Parser(Opcode.SMSG_LOGOUT_COMPLETE)]
        public static void HandleLogoutComplete(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.ReadBit(); // fake bit

            packet.Translator.StartBitStream(guid, 7, 3, 1, 5, 0, 6, 4, 2);
            packet.Translator.ParseBitStream(guid, 0, 5, 1, 2, 6, 3, 4, 7);

            packet.Translator.WriteGuid("Guid", guid);

            CoreParsers.SessionHandler.LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION)]
        public static void HandleAuthSession(Packet packet)
        {
            var sha = new byte[20];

            packet.Translator.ReadUInt32("UInt32 1");
            packet.Translator.ReadUInt32("UInt32 2");

            sha[4] = packet.Translator.ReadByte();
            sha[12] = packet.Translator.ReadByte();
            sha[3] = packet.Translator.ReadByte();
            sha[7] = packet.Translator.ReadByte();

            packet.Translator.ReadUInt32("UInt32 3");

            sha[11] = packet.Translator.ReadByte();
            sha[17] = packet.Translator.ReadByte();
            sha[14] = packet.Translator.ReadByte();
            sha[5] = packet.Translator.ReadByte();

            packet.Translator.ReadInt64("Int64");

            sha[10] = packet.Translator.ReadByte();

            packet.Translator.ReadUInt32("UInt32 4");

            sha[6] = packet.Translator.ReadByte();
            sha[18] = packet.Translator.ReadByte();
            sha[15] = packet.Translator.ReadByte();
            sha[13] = packet.Translator.ReadByte();
            sha[0] = packet.Translator.ReadByte();
            sha[8] = packet.Translator.ReadByte();

            packet.Translator.ReadInt16E<ClientVersionBuild>("Client Build");

            sha[1] = packet.Translator.ReadByte();
            sha[19] = packet.Translator.ReadByte();
            sha[16] = packet.Translator.ReadByte();
            sha[9] = packet.Translator.ReadByte();
            sha[5] = packet.Translator.ReadByte();
            sha[2] = packet.Translator.ReadByte();

            packet.Translator.ReadByte("Unk Byte");

            packet.Translator.ReadUInt32("UInt32 5");
            //packet.Translator.ReadUInt32("UInt32 6");

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
    }
}
