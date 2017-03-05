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
            packet.ReadInt32("Current Time");
            packet.ReadInt32("Int24"); // Status?

            var count = (int)packet.ReadBits(20);

            var bits420 = new uint[count];
            var bitsC = new uint[count];
            var bit410 = new bool[count];
            var hasWaitTime = new bool[count];
            var unk = new byte[count][];

            for (var i = 0; i < count; ++i)
            {
                bits420[i] = packet.ReadBits(10);
                bitsC[i] = packet.ReadBits(11);
                bit410[i] = !packet.ReadBit();
                hasWaitTime[i] = !packet.ReadBit();

                unk[i] = new byte[8];
                packet.StartBitStream(unk[i], 4, 2, 0, 5, 6, 3, 1, 7);
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ParseBitStream(unk[i], 1, 0, 2, 7, 5, 6, 3, 4);

                if (bit410[i])
                    packet.ReadInt32("IntED", i); // Category?

                packet.ReadInt32("Message ID", i);
                packet.ReadInt32("Create Time", i);
                packet.ReadWoWString("Average wait time Text", bits420[i], i);
                packet.ReadWoWString("URL", bitsC[i], i);
                packet.ReadInt32("TicketID", i);

                if (hasWaitTime[i])
                    packet.ReadInt32("Average wait time", i);

                packet.AddValue("unk", BitConverter.ToUInt64(unk[i], 0), i);
            }
        }
    }
}
