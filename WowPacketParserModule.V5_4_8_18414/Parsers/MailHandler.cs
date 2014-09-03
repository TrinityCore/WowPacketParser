using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class MailHandler
    {
        [Parser(Opcode.CMSG_GET_MAIL_LIST)]
        public static void HandleGetMailList(Packet packet)
        {
            var guid = packet.StartBitStream(6, 3, 7, 5, 4, 1, 2, 0);
            packet.ParseBitStream(guid, 7, 1, 6, 5, 4, 2, 3, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_MAIL_CREATE_TEXT_ITEM)]
        public static void HandleMailCreate(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_MAIL_DELETE)]
        public static void HandleMailDelete(Packet packet)
        {
            packet.ReadUInt32("Template Id");
            packet.ReadUInt32("Mail Id");
        }

        [Parser(Opcode.CMSG_MAIL_MARK_AS_READ)]
        public static void HandleMarkMail(Packet packet)
        {
            packet.ReadToEnd();
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

        [Parser(Opcode.CMSG_MAIL_TAKE_ITEM)]
        public static void HandleMailTakeItem(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_MAIL_TAKE_MONEY)]
        public static void HandleTakeMoney(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_SEND_MAIL)]
        public static void HandleSendMail(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.MSG_QUERY_NEXT_MAIL_TIME)]
        public static void HandleNullMail(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_MAIL_LIST_RESULT)]
        public static void HandleSMailListResult(Packet packet)
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
                    packet.ReadEntry<UInt32>(StoreNameType.Item, "Item Id", i, j);
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

        [Parser(Opcode.SMSG_RECEIVED_MAIL)]
        public static void HandleSReceivedMail(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SEND_MAIL_RESULT)]
        public static void HandleSSendMailResult(Packet packet)
        {
            packet.ReadUInt32("Mail Id");
            var error = packet.ReadEnum<MailErrorType>("Mail Error", TypeCode.UInt32);
            packet.ReadUInt32("Equip Error");
            var action = packet.ReadEnum<MailActionType>("Mail Action", TypeCode.UInt32);
            packet.ReadUInt32("Item Low GUID");
            packet.ReadUInt32("Item count");
        }

        [Parser(Opcode.SMSG_SHOW_MAILBOX)]
        public static void HandleSShowMailbox(Packet packet)
        {
            packet.ReadToEnd();
        }
    }
}
