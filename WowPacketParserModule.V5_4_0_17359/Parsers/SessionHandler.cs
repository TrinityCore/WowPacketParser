using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class SessionHandler
    {

        [Parser(Opcode.SMSG_AUTH_RESPONSE)]
        public static void HandleAuthResponse(Packet packet)
        {
            var isQueued = packet.ReadBit("Is In Queue");
            var hasAccountData = packet.ReadBit("Has Account Data");

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
                bits23 = packet.ReadBits("Unkbits", 21);
                packet.ReadBit("Unk 1");
                classCount = packet.ReadBits("Class Activation Count", 23);
                packet.ReadBit("Unk 2");

                bit112 = packet.ReadBit();
                count1096 = new uint[bits23];
                len4 = new uint[bits23];
                len69 = new uint[bits23];
                for (var i = 0; i < bits23; ++i)
                {
                    count1096[i] = packet.ReadBits(23);
                    len4[i] = packet.ReadBits(7);
                    len69[i] = packet.ReadBits(10);
                }

                bits6 = packet.ReadBits(21);
                len5 = new uint[bits6];
                len261 = new uint[bits6];
                for (var i = 0; i < bits6; ++i)
                {
                    len261[i] = packet.ReadBits(8);
                    packet.ReadBit("unk bit", i);
                    len5[i] = packet.ReadBits(8);
                }
                bit116 = packet.ReadBit();
                raceCount = packet.ReadBits("Race Activation Count", 23);
            }

            if (isQueued)
                packet.ReadBit("unk0");

            packet.ResetBitReader();

            if (isQueued)
                packet.ReadUInt32("Unk 11");

            if (hasAccountData)
            {
                for (var i = 0; i < classCount; ++i)
                {
                    packet.ReadByteE<Class>("Class", i);
                    packet.ReadByteE<ClientType>("Class Expansion", i);
                }

                packet.ReadUInt32("Unk int 12");
                for (var i = 0; i < raceCount; ++i)
                {
                    packet.ReadByteE<ClientType>("Race Expansion", i);
                    packet.ReadByteE<Race>("Race", i);
                }

                packet.ReadInt32("Unk int 10");
                packet.ReadInt32("Unk int 14");
                if (bit112)
                    packet.ReadInt16("UnkInt16 1");

                for (var i = 0; i < bits23; ++i)
                {
                    packet.ReadInt32("Unk int32", i);
                    for (var j = 0; j < count1096[i]; ++j)
                    {
                        packet.ReadByte("Unk byte 1", i, j);
                        packet.ReadByte("Unk byte 0", i, j);
                    }
                    packet.ReadWoWString("String 1", len69[i], i);
                    packet.ReadWoWString("String 2", len4[i], i);
                }
                packet.ReadByteE<ClientType>("Player Expansion");
                packet.ReadByteE<ClientType>("Account Expansion");
                if (bit116)
                    packet.ReadInt16("UnkInt16 2");

                for (var i = 0; i < bits6; ++i)
                {
                    packet.ReadWoWString("Realm", len5[i], i);
                    packet.ReadWoWString("Realm", len261[i], i);
                    packet.ReadInt32("Realm Id", i);
                }
                packet.ReadInt32("Unk int 5");
                packet.ReadInt32("Unk int 11");
            }

            packet.ReadByteE<ResponseCode>("Auth Code");
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
    }
}
