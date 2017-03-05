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
            packet.Translator.ReadUInt32("Total Mails");

            var count = packet.Translator.ReadBits("Count", 18);

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
                bit2C[i] = packet.Translator.ReadBit();
                subjectLength[i] = packet.Translator.ReadBits(8);
                bodyLength[i] = packet.Translator.ReadBits(13);
                bit24[i] = packet.Translator.ReadBit();
                bit1C[i] = packet.Translator.ReadBit();
                itemCount[i] = packet.Translator.ReadBits(17);
                sender[i] = packet.Translator.ReadBit();
                if (sender[i])
                {
                    guid[i] = new byte[8];
                    packet.Translator.StartBitStream(guid[i], 2, 6, 7, 0, 5, 3, 1, 4);
                }

                bit84[i] = new uint[itemCount[i]];

                for (var j = 0; j < itemCount[i]; ++j)
                    bit84[i][j] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {

                for (var j = 0; j < itemCount[i]; ++j)
                {
                    packet.Translator.ReadInt32("GuidLow", i, j);

                    var len = packet.Translator.ReadInt32();

                    packet.Translator.ReadBytes(len);

                    packet.Translator.ReadInt32("MaxDurability", i, j);
                    packet.Translator.ReadInt32("SuffixFactor", i, j);
                    for (var k = 0; k < 8; ++k)
                    {
                        packet.Translator.ReadInt32("EnchantmentDuration", i, j, k);
                        packet.Translator.ReadInt32("EnchantmentId", i, j, k);
                        packet.Translator.ReadInt32("EnchantmentCharges", i, j, k);
                    }
                    packet.Translator.ReadInt32("ItemRandomPropertyId", i, j);
                    packet.Translator.ReadInt32("SpellCharges", i, j);
                    packet.Translator.ReadInt32("Durability", i, j);
                    packet.Translator.ReadInt32("Count", i, j);
                    packet.Translator.ReadByte("Slot", i, j);
                    packet.AddValue("bit84", bit84[i][j], i, j);
                    packet.Translator.ReadUInt32<ItemId>("Item Id", i, j);
                }

                packet.Translator.ReadWoWString("Body", bodyLength[i], i);
                packet.Translator.ReadInt32("MessageID", i);
                if (sender[i])
                {
                    packet.Translator.ParseBitStream(guid[i], 4, 0, 5, 3, 1, 7, 2, 6);
                    packet.Translator.WriteGuid("Guid", guid[i]);
                }
                packet.Translator.ReadInt32("Unk1", i);
                packet.Translator.ReadInt64("COD", i);
                packet.Translator.ReadWoWString("Subject", subjectLength[i], i);
                packet.Translator.ReadInt32("Unk2", i);
                packet.Translator.ReadSingle("Time", i);
                packet.Translator.ReadInt64("Money", i);
                packet.Translator.ReadInt32("Flags", i);

                if (bit2C[i])
                    packet.Translator.ReadInt32("Unk4", i);

                packet.Translator.ReadByte("MessageType", i);
                packet.Translator.ReadInt32("Unk5", i);

                if (bit24[i])
                    packet.Translator.ReadInt32("RealmId1", i);

                if (bit1C[i])
                    packet.Translator.ReadInt32("RealmId2", i);

            }
        }

        [Parser(Opcode.SMSG_MAIL_COMMAND_RESULT)]
        public static void HandleSendMailResult(Packet packet)
        {
            packet.Translator.ReadUInt32("Mail Id");
            packet.Translator.ReadUInt32E<MailErrorType>("Mail Error");
            packet.Translator.ReadUInt32("Equip Error");
            packet.Translator.ReadUInt32E<MailActionType>("Mail Action");
            packet.Translator.ReadUInt32("Item Low GUID");
            packet.Translator.ReadUInt32("Item count");
        }

        [Parser(Opcode.CMSG_MAIL_RETURN_TO_SENDER)]
        public static void HandleMailReturnToSender(Packet packet)
        {
            packet.Translator.ReadUInt32("Mail Id");

            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 2, 0, 4, 6, 3, 1, 7, 5);
            packet.Translator.ParseBitStream(guid, 5, 6, 2, 0, 3, 1, 4, 7);
            packet.Translator.WriteGuid("Mailbox Guid", guid);
        }
    }
}