using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_0_17359.Parsers
{
    public static class MailHandler
    {

        [Parser(Opcode.CMSG_SEND_MAIL)]
        public static void HandleSendMail(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("IntA20");
            packet.ReadInt64("IntA40");
            packet.ReadInt32("IntA1C");
            packet.ReadInt64("IntA38");

            var count = packet.ReadBits(5);

            guid[0] = packet.ReadBit();
            
            var guid2 = new byte[count][];
            for (var i = 0; i < count; ++i)
                packet.StartBitStream(guid2[i], 6, 5, 1, 3, 0, 4, 7, 2);

            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var bits15 = (int)packet.ReadBits(11);
            var bits18 = (int)packet.ReadBits(9);
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var bits14A = (int)packet.ReadBits(9);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(guid2[i], 6);
                packet.ReadXORByte(guid2[i], 3);
                packet.ReadXORByte(guid2[i], 0);
                packet.ReadXORByte(guid2[i], 5);
                packet.ReadXORByte(guid2[i], 7);
                packet.ReadXORByte(guid2[i], 4);
                packet.ReadByte("Slot", i);
                packet.ReadXORByte(guid2[i], 1);
                packet.ReadXORByte(guid2[i], 2);
                packet.WriteGuid("Guid2", guid2[i]);
            }

            packet.ReadWoWString("len1", bits14A);
            packet.ReadXORByte(guid, 1);
            packet.ReadWoWString("len2", bits18);
            packet.ReadXORByte(guid, 7);
            packet.ReadWoWString("len3", bits15);

            packet.WriteGuid("Guid2", guid);
        }
    }
}