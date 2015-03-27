using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class MailHandler
    {
        [Parser(Opcode.SMSG_MAIL_LIST_RESULT)]
        public static void HandleMailListResult(Packet packet)
        {
            packet.ReadUInt32("Total Mails");

            var count = packet.ReadBits("Count", 18);

            var guid = new byte[count][];

            var bit1C = new bool[count];
            var bit24 = new bool[count];
            var sender = new bool[count];
            var bit2C = new bool[count];

            var subjectLength = new uint[count];
            var itemCount = new uint[count];
            var bodyLength = new uint[count];
            var bit84 = new uint[count][];

            for (var i = 0; i < count; ++i)
            {
                bit2C[i] = packet.ReadBit();
                subjectLength[i] = packet.ReadBits(8);
                bodyLength[i] = packet.ReadBits(13);
                bit24[i] = packet.ReadBit();
                bit1C[i] = packet.ReadBit();
                itemCount[i] = packet.ReadBits(17);
                sender[i] = packet.ReadBit();
                if (sender[i])
                {
                    guid[i] = new byte[8];
                    packet.StartBitStream(guid[i], 2, 6, 7, 0, 5, 3, 1, 4);
                }

                bit84[i] = new uint[itemCount[i]];

                for (var j = 0; j < itemCount[i]; ++j)
                    bit84[i][j] = packet.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {

                for (var j = 0; j < itemCount[i]; ++j)
                {
                    packet.ReadInt32("GuidLow", i, j);

                    var len = packet.ReadInt32();

                    packet.ReadBytes(len);

                    packet.ReadInt32("MaxDurability", i, j);
                    packet.ReadInt32("SuffixFactor", i, j);
                    for (var k = 0; k < 8; ++k)
                    {
                        packet.ReadInt32("EnchantmentDuration", i, j, k);
                        packet.ReadInt32("EnchantmentId", i, j, k);
                        packet.ReadInt32("EnchantmentCharges", i, j, k);
                    }
                    packet.ReadInt32("ItemRandomPropertyId", i, j);
                    packet.ReadInt32("SpellCharges", i, j);
                    packet.ReadInt32("Durability", i, j);
                    packet.ReadInt32("Count", i, j);
                    packet.ReadByte("Slot", i, j);
                    packet.AddValue("bit84", bit84[i][j], i, j);
                    packet.ReadUInt32<ItemId>("Item Id", i, j);
                }

                packet.ReadWoWString("Body", bodyLength[i], i);
                packet.ReadInt32("MessageID", i);
                if (sender[i])
                {
                    packet.ParseBitStream(guid[i], 4, 0, 5, 3, 1, 7, 2, 6);
                    packet.WriteGuid("Guid", guid[i]);
                }
                packet.ReadInt32("Unk1", i);
                packet.ReadInt64("COD", i);
                packet.ReadWoWString("Subject", subjectLength[i], i);
                packet.ReadInt32("Unk2", i);
                packet.ReadSingle("Time", i);
                packet.ReadInt64("Money", i);
                packet.ReadInt32("Flags", i);

                if (bit2C[i])
                    packet.ReadInt32("Unk4", i);

                packet.ReadByte("MessageType", i);
                packet.ReadInt32("Unk5", i);

                if (bit24[i])
                    packet.ReadInt32("RealmId1", i);

                if (bit1C[i])
                    packet.ReadInt32("RealmId2", i);

            }
        }

        [Parser(Opcode.SMSG_MAIL_COMMAND_RESULT)]
        public static void HandleSendMailResult(Packet packet)
        {
            packet.ReadUInt32("Mail Id");
            packet.ReadUInt32E<MailErrorType>("Mail Error");
            packet.ReadUInt32("Equip Error");
            packet.ReadUInt32E<MailActionType>("Mail Action");
            packet.ReadUInt32("Item Low GUID");
            packet.ReadUInt32("Item count");
        }

        [Parser(Opcode.CMSG_MAIL_RETURN_TO_SENDER)]
        public static void HandleMailReturnToSender(Packet packet)
        {
            packet.ReadUInt32("Mail Id");

            var guid = new byte[8];

            packet.StartBitStream(guid, 2, 0, 4, 6, 3, 1, 7, 5);
            packet.ParseBitStream(guid, 5, 6, 2, 0, 3, 1, 4, 7);
            packet.WriteGuid("Mailbox Guid", guid);
        }
    }
}