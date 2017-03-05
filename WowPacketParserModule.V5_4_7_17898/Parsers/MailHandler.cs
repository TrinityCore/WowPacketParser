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

            packet.Translator.ReadInt32("IntA20");
            packet.Translator.ReadInt64("IntA40");
            packet.Translator.ReadInt32("IntA1C");
            packet.Translator.ReadInt64("IntA38");

            var count = packet.Translator.ReadBits(5);

            guid[0] = packet.Translator.ReadBit();

            var ItemGUID = new byte[count][];
            for (var i = 0; i < count; ++i)
                packet.Translator.StartBitStream(ItemGUID[i], 6, 5, 1, 3, 0, 4, 7, 2);

            guid[2] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            var len1 = (int)packet.Translator.ReadBits(11);
            var len3 = (int)packet.Translator.ReadBits(9);
            guid[1] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var len2 = (int)packet.Translator.ReadBits(9);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadXORByte(ItemGUID[i], 6);
                packet.Translator.ReadXORByte(ItemGUID[i], 3);
                packet.Translator.ReadXORByte(ItemGUID[i], 0);
                packet.Translator.ReadXORByte(ItemGUID[i], 5);
                packet.Translator.ReadXORByte(ItemGUID[i], 7);
                packet.Translator.ReadXORByte(ItemGUID[i], 4);
                packet.Translator.ReadByte("Slot", i);
                packet.Translator.ReadXORByte(ItemGUID[i], 1);
                packet.Translator.ReadXORByte(ItemGUID[i], 2);
                packet.Translator.WriteGuid("ItemGUID", ItemGUID[i]);
            }

            packet.Translator.ReadWoWString("Body", len2);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadWoWString("Receiver", len3);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadWoWString("Subject", len1);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_MAIL_GET_LIST)]
        public static void HandleShowMailbox(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 5, 3, 6, 0, 1, 4, 7, 2);
            packet.Translator.ParseBitStream(guid, 4, 7, 3, 5, 6, 0, 1, 2);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.SMSG_MAIL_LIST_RESULT)]
        public static void HandleMailListResult(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 18);

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
                bit2C[i] = packet.Translator.ReadBit();
                subjectLength[i] = packet.Translator.ReadBits(8);
                bit1C[i] = packet.Translator.ReadBit();
                bit10[i] = packet.Translator.ReadBit();
                bit24[i] = packet.Translator.ReadBit();

                if (bit10[i])
                {
                    guid[i] = new byte[8];
                    packet.Translator.StartBitStream(guid[i], 6, 0, 3, 2, 5, 7, 1, 4);
                }

                bodyLength[i] = packet.Translator.ReadBits(13);
                itemCount[i] = packet.Translator.ReadBits(17);

                bit84[i] = new uint[itemCount[i]];

                for (var j = 0; j < itemCount[i]; ++j)
                    bit84[i][j] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {

                for (var j = 0; j < itemCount[i]; ++j)
                {
                    packet.AddValue("bit84", bit84[i][j], i, j);
                    packet.Translator.ReadInt32("IntED", i, j);
                    packet.Translator.ReadUInt32<ItemId>("Item Id", i, j);
                    packet.Translator.ReadInt32("IntED", i, j);
                    packet.Translator.ReadInt32("Int14", i, j);
                    packet.Translator.ReadByte("ByteED", i, j);

                    for (var k = 0; k < 8; ++k)
                    {
                        packet.Translator.ReadInt32("Int14", i, j, k);
                        packet.Translator.ReadInt32("Int14", i, j, k);
                        packet.Translator.ReadInt32("Int14", i, j, k);
                    }

                    packet.Translator.ReadInt32("IntED", i, j);
                    packet.Translator.ReadInt32("IntED", i, j);
                    packet.Translator.ReadInt32("IntED", i, j);

                    var len = packet.Translator.ReadInt32();

                    packet.Translator.ReadBytes(len);

                    packet.Translator.ReadInt32("IntED", i, j);
                }

                packet.Translator.ReadInt64("Int30", i);

                if (bit1C[i])
                    packet.Translator.ReadInt32("IntED", i);

                packet.Translator.ReadInt32("IntED", i);

                if (bit10[i])
                {
                    packet.Translator.ParseBitStream(guid[i], 7, 0, 6, 5, 4, 2, 3, 1);
                    packet.Translator.WriteGuid("Guid", guid[i]);
                }

                packet.Translator.ReadByte("ByteED", i);

                packet.Translator.ReadWoWString("Subject", subjectLength[i], i);

                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadInt32("IntED", i);
                packet.Translator.ReadInt32("IntED", i);

                if (bit24[i])
                    packet.Translator.ReadInt32("IntED", i);

                packet.Translator.ReadSingle("Time", i);
                packet.Translator.ReadWoWString("Body", bodyLength[i], i);
                packet.Translator.ReadInt32("Int14", i);

                if (bit2C[i])
                    packet.Translator.ReadInt32("IntED", i);

                packet.Translator.ReadInt64("Int40", i);
            }

            packet.Translator.ReadUInt32("Total Mails");
        }
    }
}