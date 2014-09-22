using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using Guid = WowPacketParser.Misc.WowGuid;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class SessionHandler
    {
        public static Guid LoginGuid;

        [Parser(Opcode.CMSG_AUTH_SESSION)]
        public static void HandleAuthSession(Packet packet)
        {
            var sha = new byte[20];

            packet.ReadUInt32("Unk UInt32");
            packet.ReadUInt32("Unk UInt32 2");

            sha[2] = packet.ReadByte();
            sha[11] = packet.ReadByte();
            sha[1] = packet.ReadByte();
            sha[16] = packet.ReadByte();

            packet.ReadUInt32("Billling Plan Flags");

            sha[10] = packet.ReadByte();
            sha[16] = packet.ReadByte();
            sha[13] = packet.ReadByte();
            sha[3] = packet.ReadByte();

            packet.ReadUInt64("Unk, something to do with redirection");

            sha[9] = packet.ReadByte();

            packet.ReadUInt32("dword44");

            sha[5] = packet.ReadByte();
            sha[17] = packet.ReadByte();
            sha[14] = packet.ReadByte();
            sha[12] = packet.ReadByte();

            packet.ReadByte("byte28");

            sha[7] = packet.ReadByte();

            packet.ReadUInt16("Client Build");
            packet.ReadByte("byte29");

            sha[18] = packet.ReadByte();
            sha[15] = packet.ReadByte();
            sha[8] = packet.ReadByte();
            sha[4] = packet.ReadByte();
            sha[0] = packet.ReadByte();

            packet.ReadByte("byte18");
            packet.ReadUInt32("Client seed");

            using (var addons = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName))
            {
                var pkt2 = addons;
                CoreParsers.AddonHandler.ReadClientAddonsList(ref pkt2);
            }

            var size = (int)packet.ReadBits(11);
            packet.ReadBit("Unk bit");
            packet.WriteLine("Account name: {0}", Encoding.UTF8.GetString(packet.ReadBytes(size)));
            packet.WriteLine("Proof SHA-1 Hash: " + Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            packet.ReadSingle("Unk Float");

            var guid = packet.StartBitStream(7, 6, 0, 4, 5, 2, 3, 1);
            packet.ParseBitStream(guid, 5, 0, 1, 6, 7, 2, 3, 4);

            packet.WriteGuid("Guid", guid);

            LoginGuid = new Guid(BitConverter.ToUInt64(guid, 0));
        }

        [Parser(Opcode.SMSG_AUTH_CHALLENGE)]
        public static void HandleServerAuthChallenge(Packet packet)
        {
            packet.ReadUInt32("Server Seed");
            packet.WriteLine(packet.ReadBytes(32).ToString());
            packet.ReadByte("Unk Byte");        
        }

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                var hasAccountData = packet.ReadBit("Has Account Data");

                var count = 0u;
                uint[,] count_1 = new uint[1, 2];
                var count1 = 0u;
                uint[,] count1_1 = new uint[1, 3];
                var ClassCount = 0u;
                var RaceCount = 0u;
                bool hasByte78 = false;
                bool hasByte7C = false;

                if (hasAccountData)
                {
                    count = packet.ReadBits("Realm Names", 21);
                    count_1 = new uint[count, 2];

                    for (uint i = 0; i < count; ++i)
                    {
                        count_1[i, 0] = packet.ReadBits("unk count bytes lenght1", 8);
                        packet.ReadBit("Home realm");
                        count_1[i, 1] = packet.ReadBits("unk count bytes lenght2", 8);
                    }

                    packet.ReadBit("byte74");
                    RaceCount = packet.ReadBits("RaceCount", 23);
                    count1 = packet.ReadBits("unk count", 21);
                    count1_1 = new uint[count1, 3];

                for (uint i = 0; i < count1; ++i)
                {
                    count1_1[i, 0] = packet.ReadBits("count1 Unk shit", 23);
                    count1_1[i, 1] = packet.ReadBits("count1 unk bits", 7);
                    count1_1[i, 2] = packet.ReadBits("count1 unk shit2", 10);
                }

                hasByte7C = packet.ReadBit("byte7C");
                ClassCount = packet.ReadBits("Class Count", 23);
                packet.ReadBit("byte3E");
                hasByte78 = packet.ReadBit("byte78");
                packet.ReadBit("byte7E");
            }

            var hasQueueData = packet.ReadBit("Has Queue Data");

            if (hasQueueData)
            {
                packet.ReadBit("Unk queue bit");
                packet.ReadUInt32("Queue position");
            }

            if (hasAccountData)
            {
                for (uint i = 0; i < count1; ++i)
                {
                    for (uint j = 0; j < count1_1[i, 0]; ++j)
                    {
                        packet.ReadByte("Class or Race");
                        packet.ReadByte("Class or Race");
                    }

                    packet.ReadUInt32("Unk stuff");
                    packet.ReadBytes((int)count1_1[i, 1]);
                    packet.ReadBytes((int)count1_1[i, 2]);
                }

                if (hasByte7C)
                    packet.ReadUInt16("word7A");

                for (int i = 0; i < ClassCount; ++i)
                {
                    packet.ReadEnum<Class>("Class", TypeCode.Byte, i);
                    packet.ReadEnum<ClientType>("Class Expansion", TypeCode.Byte, i);
                }

                packet.ReadByte("byte3C");

                for (int i = 0; i < RaceCount; ++i)
                {
                    packet.ReadEnum<Race>("Race", TypeCode.Byte, i);
                    packet.ReadEnum<ClientType>("Race Expansion", TypeCode.Byte, i);
                }

                packet.ReadUInt32("dword34");

                for (uint i = 0; i < count; ++i)
                {
                    packet.ReadUInt32("Unk stuff");
                    packet.ReadWoWString("Realm name ?", count_1[i, 0]);
                    packet.ReadWoWString("Realm name ?", count_1[i, 1]);
                }

                packet.ReadUInt32("dword38");
                packet.ReadUInt32("dword30");
                packet.ReadUInt32("dword40");
                packet.ReadUInt32("dword80");

                if (hasByte78)
                    packet.ReadUInt16("word76");

                packet.ReadByte("byte3D");
                packet.ReadUInt32("dword1C");
            }

            packet.ReadEnum<ResponseCode>("Auth Code", TypeCode.Byte);
        }
            else
            {
                packet.WriteLine("              : CMSG_LOOT_MONEY");
                packet.Opcode = (int)Opcode.CMSG_LOOT_MONEY;
            }
        }

        [Parser(Opcode.SMSG_LOGOUT_COMPLETE)]
        public static void HandleLogoutComplete(Packet packet)
        {
            packet.ReadBit("Unk 1");

            var guid = packet.StartBitStream(7, 3, 1, 5, 0, 6, 4, 2);
            packet.ParseBitStream(guid, 0, 5, 1, 2, 6, 3, 4, 7);

            packet.WriteGuid("Guid", guid);

            LoginGuid = new Guid(0);
        }

        [Parser(Opcode.SMSG_MOTD)]
        public static void HandleMessageOfTheDay(Packet packet)
        {
            var lineCount = packet.ReadBits("Line Count", 4);
            var LineSize = new uint[lineCount];
            for (var i = 0; i < lineCount; i++)
                LineSize[i] = packet.ReadBits(7);
            for (var i = 0; i < lineCount; i++)
                packet.ReadWoWString("", LineSize[i], i);
        }

        [Parser(Opcode.SMSG_REDIRECT_CLIENT)]
        public static void HandleRedirectClient(Packet packet)
        {
            packet.ReadUInt64("Unk, send it CMSG_AUTH_SESSION, may be bytes sent and bytes received");
            byte[] RSABuffer = new byte[256];
            RSABuffer = packet.ReadBytes(256);
            packet.ReadByte("Future connection offset in WowConnections array");
            packet.ReadUInt32("Server Token");
        }
    }
}
