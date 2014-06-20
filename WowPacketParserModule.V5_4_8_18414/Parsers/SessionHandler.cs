using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using Guid = WowPacketParser.Misc.Guid;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
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

            sha[18] = packet.ReadByte();
            sha[14] = packet.ReadByte();
            sha[3] = packet.ReadByte();
            sha[4] = packet.ReadByte();
            sha[0] = packet.ReadByte();

            packet.ReadUInt32("dword44");

            sha[11] = packet.ReadByte();
            packet.ReadUInt32("Client seed");
            sha[19] = packet.ReadByte();

            packet.ReadByte("byte28");
            packet.ReadByte("byte29");

            sha[2] = packet.ReadByte();
            sha[9] = packet.ReadByte();
            sha[12] = packet.ReadByte();

            packet.ReadUInt64("Unk, something to do with redirection");
            packet.ReadUInt32("Billling Plan Flags");

            sha[16] = packet.ReadByte();
            sha[5] = packet.ReadByte();
            sha[6] = packet.ReadByte();
            sha[8] = packet.ReadByte();
            packet.ReadUInt16("Client Build");
            sha[17] = packet.ReadByte();
            sha[7] = packet.ReadByte();
            sha[13] = packet.ReadByte();
            sha[15] = packet.ReadByte();
            sha[1] = packet.ReadByte();
            sha[10] = packet.ReadByte();

            using (var addons = new Packet(packet.ReadBytes(packet.ReadInt32()), packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName))
            {
                var pkt2 = addons;
                CoreParsers.AddonHandler.ReadClientAddonsList(ref pkt2);
            }

            packet.ReadBit("Unk bit");
            var size = (int)packet.ReadBits("Size", 11);
            packet.WriteLine("Account name: {0}", Encoding.UTF8.GetString(packet.ReadBytes(size)));
            packet.WriteLine("Proof SHA-1 Hash: " + Utilities.ByteArrayToHexString(sha));
        }

        [Parser(Opcode.CMSG_PLAYER_LOGIN)]
        public static void HandlePlayerLogin(Packet packet)
        {
            packet.ReadSingle("Unk Float");

            var guid = packet.StartBitStream(7, 6, 3, 2, 1, 5, 4, 0); //???
            packet.ParseBitStream(guid, 1, 4, 0, 2, 7, 5, 6, 3); //???

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
            var hasAccountData = packet.ReadBit("Has Account Data"); //120

            var count = 0u;
            uint[,] count_1 = new uint[1,2];
            var count1 = 0u;
            uint[,] count1_1 = new uint[1,3];
            var ClassCount = 0u;
            var RaceCount = 0u;
            bool hasByte112 = false;
            bool hasByte108 = false;

            if (hasAccountData)
            {
                count = packet.ReadBits("unk count", 21); //20
                count_1 = new uint[count, 2];

                for (uint i = 0; i < count; ++i)
                {
                    count_1[i, 0] = packet.ReadBits("unk count bytes lenght1", 8); //+29
                    count_1[i, 1] = packet.ReadBits("unk count bytes lenght2", 8); //+285
                    packet.ReadBit("count unk bit"); //+28
                }//+=520

                ClassCount = packet.ReadBits("Class Count", 23); //72

                count1 = packet.ReadBits("unk count", 21); //88
                hasByte112 = packet.ReadBit("Byte112"); //112
                packet.ReadBit("Byte104"); //104
                packet.ReadBit("Byte114"); //114
                hasByte108 = packet.ReadBit("Byte108"); //108

                count1_1 = new uint[count1, 3];

                for (uint i = 0; i < count1; ++i)
                {
                    count1_1[i, 0] = packet.ReadBits("count1 Unk shit", 23); //23*4+1096
                    count1_1[i, 1] = packet.ReadBits("count1 unk bits", 7);  //23*4+4
                    count1_1[i, 2] = packet.ReadBits("count1 unk shit2", 10);//23*4+69
                }//+=1112

                RaceCount = packet.ReadBits("RaceCount", 23); //56

                packet.ReadBit("byte50"); //50
            }

            var hasQueueData = packet.ReadBit("Has Queue Data"); //136
            if (hasQueueData)
            {
                packet.ReadBit("Unk queue bit"); //132
                packet.ReadUInt32("Queue position"); //32*4
            }

            if (hasAccountData)
            {
                for (uint i = 0; i < count; ++i)
                {
                    packet.ReadUInt32("Unk stuff"); //+24
                    packet.ReadWoWString("Realm name ?", count_1[i, 0]);
                    packet.ReadWoWString("Realm name ?", count_1[i, 1]);
                    //v18+=520
                }

                for (int i = 0; i < RaceCount; ++i)
                {
                    packet.ReadEnum<Race>("Race", TypeCode.Byte, i); //+61+i*2
                    packet.ReadEnum<ClientType>("Race Expansion", TypeCode.Byte, i); //+62+i*2
                }

                for (uint i = 0; i < count1; ++i)
                {
                    packet.ReadWoWString("String ?", count1_1[i, 2]);
                    packet.ReadWoWString("String ?", count1_1[i, 1]);
                    for (uint j = 0; j < count1_1[i, 0]; ++j)
                    {
                        packet.ReadByte("Class or Race"); //+23*4+1100+v24+j*2
                        packet.ReadByte("Class or Race"); //+23*4+1101+v24+j*2
                    }

                    packet.ReadUInt32("dword34"); //23*4+v24
                    //v24+=1112
                }

                for (int i = 0; i < ClassCount; ++i)
                {
                    packet.ReadEnum<Class>("Class", TypeCode.Byte, i); //+19*4+1+i*2
                    packet.ReadEnum<ClientType>("Class Expansion", TypeCode.Byte, i); //+19*4+2+i*2
                }

                packet.ReadUInt32("dword52"); //52

                if (hasByte108)
                    packet.ReadUInt16("word106"); //106

                packet.ReadByte("byte48"); //48

                packet.ReadUInt32("dword40"); //40
                packet.ReadUInt32("dword36"); //36
                packet.ReadByte("byte49"); //49

                if (hasByte112)
                    packet.ReadUInt16("word110"); //110

                packet.ReadUInt32("dword116"); //116
                packet.ReadUInt32("dword44"); //44
                packet.ReadUInt32("dword16"); //16
            }
            packet.ReadEnum<ResponseCode>("Auth Code", TypeCode.Byte); //124
        }

        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            packet.ReadSingle("X");
            packet.ReadSingle("O");
            packet.ReadSingle("Y");
            packet.ReadUInt32("Map");
            packet.ReadSingle("Z");
        }

        [Parser(Opcode.SMSG_MOTD)]
        public static void HandleMessageOfTheDay(Packet packet)
        {
            var count = packet.ReadBits("Line Count", 4);
            var counts = new uint[count];
            for (var i = 0; i < count; ++i)
            {
                counts[i] = packet.ReadBits(7); //20
                //v16+=100;
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadWoWString("", counts[i], i);
                //v16+=100;
            }
        }

        [Parser(Opcode.SMSG_TRANSFER_PENDING)]
        public static void HandleTransferPending(Packet packet)
        {
            var unkbit = packet.ReadBit("unk");
            var isTransport = packet.ReadBit("IsTransport");
            packet.ReadUInt32("Map");

            if (isTransport)
            {
                packet.ReadUInt32("MapID");
                packet.ReadUInt32("TransportID");
            }
        }
    }
}
