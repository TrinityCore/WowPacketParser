using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class TicketHandler
    {
        [Parser(Opcode.SMSG_GM_TICKET_GET_TICKET)]
        public static void HandleGetGMTicket(Packet packet)
        {
            packet.Translator.ReadInt32("Current Time");
            packet.Translator.ReadInt32("Int24"); // Status?

            var count = (int)packet.Translator.ReadBits(20);

            var bits420 = new uint[count];
            var bitsC = new uint[count];
            var bit410 = new bool[count];
            var hasWaitTime = new bool[count];
            var unk = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                bits420[i] = packet.Translator.ReadBits(10);
                bitsC[i] = packet.Translator.ReadBits(11);
                bit410[i] = !packet.Translator.ReadBit();
                hasWaitTime[i] = !packet.Translator.ReadBit();

                unk[i] = new byte[8];
                packet.Translator.StartBitStream(unk[i], 4, 2, 0, 5, 6, 3, 1, 7);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ParseBitStream(unk[i], 1, 0, 2, 7, 5, 6, 3, 4);

                if (bit410[i])
                    packet.Translator.ReadInt32("IntED", i); // Category?

                packet.Translator.ReadInt32("Message ID", i);
                packet.Translator.ReadInt32("Create Time", i);
                packet.Translator.ReadWoWString("Average wait time Text", bits420[i], i);
                packet.Translator.ReadWoWString("URL", bitsC[i], i);
                packet.Translator.ReadInt32("TicketID", i);

                if (hasWaitTime[i])
                    packet.Translator.ReadInt32("Average wait time", i);

                packet.AddValue("unk", BitConverter.ToUInt64(unk[i], 0), i);
            }
        }
    }
}
