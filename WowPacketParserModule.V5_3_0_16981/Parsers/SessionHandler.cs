using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            var count = 0u;
            var count1 = 0u;
            var isQueued = packet.ReadBit("Is In Queue");

            if (isQueued)
                packet.ReadBit("unk0");

            var hasAccountData = packet.ReadBit("Has Account Data");

            if (hasAccountData)
            {
                packet.ReadBit("Unk 1");
                packet.ReadBit("Unk 2");
                count1 = packet.ReadBits("Race Activation Count", 23);
                packet.ReadBit("Unk 3");
                packet.ReadBits("Unkbits", 21);
                count = packet.ReadBits("Class Activation Count", 23);
                packet.ReadBits("Unkbits", 22);
                packet.ReadBit("Unk 4");

            }
            packet.ResetBitReader();

            if (hasAccountData)
            {
                packet.ReadByte("unk");
                for (var i = 0; i < count; ++i)
                {
                    packet.ReadByteE<ClientType>("Class Expansion", i);
                    packet.ReadByteE<Class>("Class", i);
                }

                packet.ReadInt16("UnkInt16 1");
                packet.ReadInt16("UnkInt16 2");

                for (var i = 0; i < count1; ++i)
                {
                    packet.ReadByteE<ClientType>("Race Expansion", i);
                    packet.ReadByteE<Race>("Race", i);
                }

                packet.ReadUInt32("Unk 8");
                packet.ReadUInt32("Unk 9");
                packet.ReadUInt32("Unk 10");


                packet.ReadByteE<ClientType>("Account Expansion");
                packet.ReadByteE<ClientType>("Player Expansion");

            }

            packet.ReadByteE<ResponseCode>("Auth Code");

            if (isQueued)
                packet.ReadUInt32("Unk 11");
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


        [Parser(Opcode.SMSG_LOGOUT_COMPLETE)]
        public static void HandleLogoutComplete(Packet packet)
        {
            CoreParsers.SessionHandler.LoginGuid = new WowGuid64(0);
        }
    }
}
