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

            packet.ReadSingle("Unk Float");

            packet.StartBitStream(guid, 7, 6, 0, 4, 5, 2, 3, 1);
            packet.ParseBitStream(guid, 5, 0, 1, 6, 7, 2, 3, 4);

            CoreParsers.SessionHandler.LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
            packet.WriteGuid("Guid", guid);
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

        [Parser(Opcode.SMSG_SET_TIME_ZONE_INFORMATION)]
        public static void HandleSetTimeZoneInformation(Packet packet)
        {
            var len1 = packet.ReadBits(7);
            var len2 = packet.ReadBits(7);
            packet.ReadWoWString("Server Location", len2);
            packet.ReadWoWString("Server Location", len1);
        }

        [Parser(Opcode.CMSG_AUTH_CONTINUED_SESSION)]
        public static void HandleRedirectAuthProof(Packet packet)
        {
            var sha = new byte[20];
            packet.ReadInt64("Int64 Unk1"); // Key or DosResponse
            packet.ReadInt64("Int64 Unk2"); // Key or DosResponse
            sha[1] = packet.ReadByte();
            sha[14] = packet.ReadByte();
            sha[9] = packet.ReadByte();
            sha[18] = packet.ReadByte();
            sha[17] = packet.ReadByte();
            sha[8] = packet.ReadByte();
            sha[6] = packet.ReadByte();
            sha[10] = packet.ReadByte();
            sha[3] = packet.ReadByte();
            sha[16] = packet.ReadByte();
            sha[4] = packet.ReadByte();
            sha[0] = packet.ReadByte();
            sha[15] = packet.ReadByte();
            sha[2] = packet.ReadByte();
            sha[19] = packet.ReadByte();
            sha[12] = packet.ReadByte();
            sha[13] = packet.ReadByte();
            sha[5] = packet.ReadByte();
            sha[11] = packet.ReadByte();
            sha[7] = packet.ReadByte();

            packet.AddValue("SHA-1 Hash", Utilities.ByteArrayToHexString(sha));
        }


        [Parser(Opcode.SMSG_CONNECT_TO)]
        public static void HandleRedirectClient(Packet packet)
        {
            packet.ReadUInt64("Unk Long");
            packet.ReadBytes("RSA Hash", 0x100);
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32("Token");
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

            var hasAccountData = packet.ReadBit("Has Account Data");
            if (hasAccountData)
            {
                bits20 = packet.ReadBits(21);

                bits0 = new uint[bits20];
                bits0C = new uint[bits20];

                for (var i = 0; i < bits20; ++i)
                {
                    bits0[i] = packet.ReadBits(8);
                    packet.ReadBit("unk bit", i);
                    bits0C[i] = packet.ReadBits(8);
                }

                bit74 = packet.ReadBit();
                classCount = packet.ReadBits("Class Activation Count", 23);
                bits64 = packet.ReadBits(21);

                for (var i = 0; i < bits64; ++i)
                {
                    bits448[i] = packet.ReadBits(23);
                    bits68[i] = packet.ReadBits(7);
                    bits45[i] = packet.ReadBits(10);
                }

                bit7C = packet.ReadBit();
                raceCount = packet.ReadBits("Race Activation Count", 23);
                bit3E = packet.ReadBit();
                bit78 = packet.ReadBit();
                bit7E = packet.ReadBit();
            }

            var isQueued = packet.ReadBit();
            if (isQueued)
            {
                packet.ReadBit("unk0");
                packet.ReadInt32("Int10");
            }

            if (hasAccountData)
            {
                for (var i = 0; i < bits64; ++i)
                {
                    for (var j = 0; j < bits448[i]; ++j)
                    {
                        packet.ReadByte("Unk byte 1", i, j);
                        packet.ReadByte("Unk byte 0", i, j);
                    }

                    packet.ReadInt32("Int68");
                    packet.ReadWoWString("StringED", bits68[i], i);
                    packet.ReadWoWString("StringED", bits45[i], i);
                }

                if (bit7C)
                    packet.ReadInt16("Int7A");

                for (var i = 0; i < classCount; ++i)
                {
                    packet.ReadByteE<ClientType>("Class Expansion", i);
                    packet.ReadByteE<Class>("Class", i);
                }

                packet.ReadByte("Byte3C");

                for (var i = 0; i < raceCount; ++i)
                {
                    packet.ReadByteE<ClientType>("Race Expansion", i);
                    packet.ReadByteE<Race>("Race", i);
                }

                packet.ReadInt32("Int34");

                for (var i = 0; i < bits20; ++i)
                {
                    packet.ReadInt32("RealmId", i);
                    packet.ReadWoWString("Realm", bits0[i], i);
                    packet.ReadWoWString("Realm", bits0C[i], i);
                }

                packet.ReadInt32("Int38");
                packet.ReadInt32("Int30");
                packet.ReadInt32("Int40");
                packet.ReadInt32("Int80");

                if (bit78)
                    packet.ReadInt16("Int76");

                packet.ReadByte("Byte3D");
                packet.ReadInt32("Int1C");
            }

            packet.ReadByteE<ResponseCode>("Auth Code");
        }

        [Parser(Opcode.SMSG_LOGOUT_COMPLETE)]
        public static void HandleLogoutComplete(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadBit(); // fake bit

            packet.StartBitStream(guid, 7, 3, 1, 5, 0, 6, 4, 2);
            packet.ParseBitStream(guid, 0, 5, 1, 2, 6, 3, 4, 7);

            packet.WriteGuid("Guid", guid);

            CoreParsers.SessionHandler.LoginGuid = new WowGuid64(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.CMSG_AUTH_SESSION)]
        public static void HandleAuthSession(Packet packet)
        {
            var sha = new byte[20];

            packet.ReadUInt32("UInt32 1");
            packet.ReadUInt32("UInt32 2");

            sha[4] = packet.ReadByte();
            sha[12] = packet.ReadByte();
            sha[3] = packet.ReadByte();
            sha[7] = packet.ReadByte();

            packet.ReadUInt32("UInt32 3");

            sha[11] = packet.ReadByte();
            sha[17] = packet.ReadByte();
            sha[14] = packet.ReadByte();
            sha[5] = packet.ReadByte();

            packet.ReadInt64("Int64");

            sha[10] = packet.ReadByte();

            packet.ReadUInt32("UInt32 4");

            sha[6] = packet.ReadByte();
            sha[18] = packet.ReadByte();
            sha[15] = packet.ReadByte();
            sha[13] = packet.ReadByte();
            sha[0] = packet.ReadByte();
            sha[8] = packet.ReadByte();

            packet.ReadInt16E<ClientVersionBuild>("Client Build");

            sha[1] = packet.ReadByte();
            sha[19] = packet.ReadByte();
            sha[16] = packet.ReadByte();
            sha[9] = packet.ReadByte();
            sha[5] = packet.ReadByte();
            sha[2] = packet.ReadByte();

            packet.ReadByte("Unk Byte");

            packet.ReadUInt32("UInt32 5");
            //packet.ReadUInt32("UInt32 6");

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
    }
}
