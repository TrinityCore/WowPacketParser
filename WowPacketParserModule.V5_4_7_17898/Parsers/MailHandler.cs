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
            
            var ItemGUID = new byte[count][];
            for (var i = 0; i < count; ++i)
                packet.StartBitStream(ItemGUID[i], 6, 5, 1, 3, 0, 4, 7, 2);

            guid[2] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var len1 = (int)packet.ReadBits(11);
            var len3 = (int)packet.ReadBits(9);
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var len2 = (int)packet.ReadBits(9);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            for (var i = 0; i < count; ++i)
            {
                packet.ReadXORByte(ItemGUID[i], 6);
                packet.ReadXORByte(ItemGUID[i], 3);
                packet.ReadXORByte(ItemGUID[i], 0);
                packet.ReadXORByte(ItemGUID[i], 5);
                packet.ReadXORByte(ItemGUID[i], 7);
                packet.ReadXORByte(ItemGUID[i], 4);
                packet.ReadByte("Slot", i);
                packet.ReadXORByte(ItemGUID[i], 1);
                packet.ReadXORByte(ItemGUID[i], 2);
                packet.WriteGuid("ItemGUID", ItemGUID[i]);
            }

            packet.ReadWoWString("Body", len2);
            packet.ReadXORByte(guid, 1);
            packet.ReadWoWString("Receiver", len3);
            packet.ReadXORByte(guid, 7);
            packet.ReadWoWString("Subject", len1);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GET_MAIL_LIST)]
        public static void HandleShowMailbox(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 3, 6, 0, 1, 4, 7, 2);
            packet.ParseBitStream(guid, 4, 7, 3, 5, 6, 0, 1, 2);

            packet.WriteGuid("GUID", guid);
        }
    }
}