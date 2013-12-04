using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using Guid = WowPacketParser.Misc.Guid;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
{
    public static class SessionHandler
    {
        public static Guid LoginGuid;

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            var count14 = 0u;
            var count22 = 0u;
            var count5 = 0u;
            var count18 = 0u;
            bool smth = false;
            bool smth2 = false;
            uint[] countabc = new uint[5000];
            uint[][] strlen = new uint[50000][];
            packet.ReadEnum<ResponseCode>("Auth Code", TypeCode.Byte);
            var isQueued = packet.ReadBit("Is In Queue");

            if (isQueued)
                packet.ReadBit("unk0");

            var hasAccountData = packet.ReadBit("Has Account Data");


            if (hasAccountData)
            {
                smth = packet.ReadBit("Unk 1");
                count5 = packet.ReadBits("Unk21_1 Count0", 21);
                count22 = packet.ReadBits("Unk21_1 Count", 21);
                count14 = packet.ReadBits("Class Activation Count", 23);
                countabc = new uint[count22];
                for (int i = 0; i < count22; i++)
                {
                    countabc[i] = packet.ReadBits("Unk1Loop", 23, i);
                    packet.ReadBits("Unk2Loop", 7, i);
                    packet.ReadBits("Unk3Loop", 10, i);
                }
                smth2 = packet.ReadBit("unksmth 2");
                packet.ReadBit("unk");
                for (var i = 0; i < count5; ++i)
                {
                    strlen[i] = new uint[2];
                    strlen[i][0] = packet.ReadBits("unk", 8, i);
                    packet.ReadBit("unk", i);
                    strlen[i][1] = packet.ReadBits("unk", 8, i);
                }

                packet.ReadBit("Unk 3");
                count18 = packet.ReadBits("Race Activation Count", 23);

            }
            //packet.ResetBitReader();

            if (hasAccountData)
            {

                packet.ReadUInt32("Unk 6");
                packet.ReadUInt32("Unk 7");

                for (var i = 0; i < count22; ++i)
                {
                    for (int j = 0; j < countabc[i]; j++)
                    {
                        packet.ReadByte("unk", i, j);
                        packet.ReadByte("unk", i, j);
                    }

                    packet.ReadUInt32("WTF", i);
                }

                packet.ReadByte("Unk 6");

                for (int j = 0; j < count14; j++)
                {
                    packet.ReadEnum<ClientType>("Class Expansion", TypeCode.Byte, j);
                    packet.ReadEnum<Race>("Race", TypeCode.Byte, j);
                }

                packet.ReadByte("unk 8");
                packet.ReadInt32("unk 32");

                for (int j = 0; j < count18; j++)
                {
                    packet.ReadEnum<Class>("Class", TypeCode.Byte, j);
                    packet.ReadEnum<ClientType>("Class Expansion", TypeCode.Byte, j);
                }

                if (smth)
                    packet.ReadInt16("unk16");

                for (int i = 0; i < count5; i++)
                {
                    packet.ReadWoWString("Str1", strlen[i][0], i);
                    packet.ReadWoWString("Str1", strlen[i][1], i);
                    packet.ReadUInt32("WTF", i);
                }

                packet.ReadInt32("unk");
                packet.ReadInt32("unk");

                if (smth2)
                    packet.ReadInt16("unk16");


            }

            if (isQueued)
                packet.ReadUInt32("Queue Pos");
        }
		
		[Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            packet.ReadSingle("Z");
            packet.ReadSingle("Y");
            packet.ReadUInt32("Map");
            packet.ReadSingle("O");
            packet.ReadSingle("X");
        }
    }
}
