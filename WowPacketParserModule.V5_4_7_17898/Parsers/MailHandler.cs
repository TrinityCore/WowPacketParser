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

        [Parser(Opcode.CMSG_MAIL_GET_LIST)]
        public static void HandleShowMailbox(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 5, 3, 6, 0, 1, 4, 7, 2);
            packet.ParseBitStream(guid, 4, 7, 3, 5, 6, 0, 1, 2);

            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_MAIL_LIST_RESULT)]
        public static void HandleMailListResult(Packet packet)
        {
            var count = packet.ReadBits("Count", 18);

            var guid = new byte[count][];

            var bit1C = new bool[count];
            var bit24 = new bool[count];
            var bit10 = new bool[count];
            var bit2C = new bool[count];

            var subjectLength = new uint[count];
            var itemCount = new uint[count];
            var bodyLength = new uint[count];
            var bit84 = new uint[count][];

            for (var i = 0; i < count; ++i)
            {
                bit2C[i] = packet.ReadBit();
                subjectLength[i] = packet.ReadBits(8);
                bit1C[i] = packet.ReadBit();
                bit10[i] = packet.ReadBit();
                bit24[i] = packet.ReadBit();

                if (bit10[i])
                {
                    guid[i] = new byte[8];
                    packet.StartBitStream(guid[i], 6, 0, 3, 2, 5, 7, 1, 4);
                }

                bodyLength[i] = packet.ReadBits(13);
                itemCount[i] = packet.ReadBits(17);

                bit84[i] = new uint[itemCount[i]];

                for (var j = 0; j < itemCount[i]; ++j)
                    bit84[i][j] = packet.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {

                for (var j = 0; j < itemCount[i]; ++j)
                {
                    packet.AddValue("bit84", bit84[i][j], i, j);
                    packet.ReadInt32("IntED", i, j);
                    packet.ReadUInt32<ItemId>("Item Id", i, j);
                    packet.ReadInt32("IntED", i, j);
                    packet.ReadInt32("Int14", i, j);
                    packet.ReadByte("ByteED", i, j);

                    for (var k = 0; k < 8; ++k)
                    {
                        packet.ReadInt32("Int14", i, j, k);
                        packet.ReadInt32("Int14", i, j, k);
                        packet.ReadInt32("Int14", i, j, k);
                    }

                    packet.ReadInt32("IntED", i, j);
                    packet.ReadInt32("IntED", i, j);
                    packet.ReadInt32("IntED", i, j);

                    var len = packet.ReadInt32();

                    packet.ReadBytes(len);

                    packet.ReadInt32("IntED", i, j);
                }

                packet.ReadInt64("Int30", i);

                if (bit1C[i])
                    packet.ReadInt32("IntED", i);

                packet.ReadInt32("IntED", i);

                if (bit10[i])
                {
                    packet.ParseBitStream(guid[i], 7, 0, 6, 5, 4, 2, 3, 1);
                    packet.WriteGuid("Guid", guid[i]);
                }

                packet.ReadByte("ByteED", i);

                packet.ReadWoWString("Subject", subjectLength[i], i);

                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);
                packet.ReadInt32("IntED", i);

                if (bit24[i])
                    packet.ReadInt32("IntED", i);

                packet.ReadSingle("Time", i);
                packet.ReadWoWString("Body", bodyLength[i], i);
                packet.ReadInt32("Int14", i);

                if (bit2C[i])
                    packet.ReadInt32("IntED", i);

                packet.ReadInt64("Int40", i);
            }

            packet.ReadUInt32("Total Mails");
        }
    }
}