using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class MailHandler
    {
        [Parser(Opcode.SMSG_RECEIVED_MAIL)]
        public static void HandleReceivedMail(Packet packet)
        {
            packet.Translator.ReadSingle("Time left"); // Sup with timers in float?
        }

        [Parser(Opcode.SMSG_SHOW_MAILBOX)]
        [Parser(Opcode.CMSG_MAIL_GET_LIST)]
        public static void HandleShowMailbox(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_MAIL_TAKE_MONEY)]
        [Parser(Opcode.CMSG_MAIL_MARK_AS_READ, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_MAIL_CREATE_TEXT_ITEM, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTakeMoney(Packet packet)
        {
            packet.Translator.ReadGuid("Mailbox GUID");
            packet.Translator.ReadUInt32("Mail Id");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596)) // need correct version
                packet.Translator.ReadUInt64("Money");
        }

        [Parser(Opcode.CMSG_MAIL_MARK_AS_READ, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.CMSG_MAIL_CREATE_TEXT_ITEM, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleMarkMail(Packet packet)
        {
            packet.Translator.ReadGuid("Mailbox GUID");
            packet.Translator.ReadUInt32("Mail Id");
        }

        [Parser(Opcode.CMSG_MAIL_DELETE)]
        public static void HandleMailDelete(Packet packet)
        {
            packet.Translator.ReadGuid("Mailbox GUID");
            packet.Translator.ReadUInt32("Mail Id");
            packet.Translator.ReadUInt32("Template Id");
        }

        [Parser(Opcode.CMSG_MAIL_RETURN_TO_SENDER)]
        public static void HandleMailReturnToSender(Packet packet)
        {
            packet.Translator.ReadGuid("Mailbox GUID");
            packet.Translator.ReadUInt32("Mail Id");
            packet.Translator.ReadGuid("Sender GUID");
        }

        [Parser(Opcode.SMSG_MAIL_LIST_RESULT)]
        public static void HandleMailListResult(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                packet.Translator.ReadUInt32("Total Mails");

            var count = packet.Translator.ReadByte("Shown Mails");
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadUInt16("Message Size", i);
                packet.Translator.ReadUInt32("Mail Id", i);

                var mailType = packet.Translator.ReadByteE<MailType>("Message Type", i);
                switch (mailType) // Read GUID if MailType.Normal, int32 (entry) if not
                {
                    case MailType.Normal:
                        packet.Translator.ReadGuid("Player GUID", i);
                        break;
                    case MailType.Creature:
                        packet.Translator.ReadInt32<UnitId>("Entry", i);
                        break;
                    case MailType.GameObject:
                        packet.Translator.ReadInt32<GOId>("Entry", i);
                        break;
                    case MailType.Item:
                        packet.Translator.ReadInt32<ItemId>("Entry", i);
                        break;
                    case (MailType)1:
                    case MailType.Auction:
                        packet.Translator.ReadInt32("Entry", i);
                        break;
                }

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                    packet.Translator.ReadUInt64("COD", i);
                else
                    packet.Translator.ReadUInt32("COD", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_3_0_10958))
                    packet.Translator.ReadUInt32("Item Text Id", i);

                packet.Translator.ReadUInt32("Package", i); // Package.dbc ID
                packet.Translator.ReadUInt32("Stationery", i); // Stationary.dbc ID
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                    packet.Translator.ReadUInt64("Money", i);
                else
                    packet.Translator.ReadUInt32("Money", i);
                packet.Translator.ReadUInt32("Flags", i);
                packet.Translator.ReadSingle("Time", i);
                packet.Translator.ReadUInt32("Template Id", i); // MailTemplate.dbc ID
                packet.Translator.ReadCString("Subject", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                    packet.Translator.ReadCString("Body", i);

                var items = packet.Translator.ReadByte("Item Count", i);
                for (var j = 0; j < items; ++j)
                {
                    packet.Translator.ReadByte("Item Index", i, j);
                    packet.Translator.ReadUInt32("Item GuidLow", i, j);
                    packet.Translator.ReadUInt32<ItemId>("Item Id", i, j);

                    int enchantmentCount = 6;
                    if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                        enchantmentCount = 7;
                    if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                        enchantmentCount = 9;
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                        enchantmentCount = 10;

                    for (var k = 0; k < enchantmentCount; ++k)
                    {
                        packet.Translator.ReadUInt32("Item Enchantment Id", i, j, k);
                        packet.Translator.ReadUInt32("Item Enchantment Duration", i, j, k);
                        packet.Translator.ReadUInt32("Item Enchantment Charges", i, j, k);
                    }

                    packet.Translator.ReadInt32("Item Random Property Id", i, j);
                    packet.Translator.ReadUInt32("Item Suffix Factor", i, j);

                    if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                        packet.Translator.ReadUInt32("Item Count", i, j);
                    else
                        packet.Translator.ReadByte("Item Count", i, j);

                    packet.Translator.ReadUInt32("Item SpellCharges", i, j);
                    packet.Translator.ReadUInt32("Item Max Durability", i, j);
                    packet.Translator.ReadUInt32("Item Durability", i, j);

                    if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                        packet.Translator.ReadByte("Unk byte", i, j);
                }
            }
        }

        [Parser(Opcode.MSG_QUERY_NEXT_MAIL_TIME)]
        public static void HandleNullMail(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                return;

            // Math.Abs(this float) > 0.0f returns whether the player has received a new mail since last visiting a mailbox
            packet.Translator.ReadSingle("Time since last time visiting a mailbox (can be < 0.0)");

            var count = packet.Translator.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadUInt64("GUID", i);
                packet.Translator.ReadUInt32("Sender Id", i);
                packet.Translator.ReadUInt32("Message type", i);
                packet.Translator.ReadUInt32("Stationery", i);
                packet.Translator.ReadSingle("Time?", i);
            }
        }

        [Parser(Opcode.SMSG_MAIL_COMMAND_RESULT)]
        public static void HandleSendMailResult(Packet packet)
        {
            packet.Translator.ReadUInt32("Mail Id");
            var action = packet.Translator.ReadUInt32E<MailActionType>("Mail Action");
            var error = packet.Translator.ReadUInt32E<MailErrorType>("Mail Error");
            if (error == MailErrorType.Equip)
                packet.Translator.ReadUInt32("Equip Error");
            else if (action == MailActionType.AttachmentExpired)
            {
                packet.Translator.ReadUInt32("Item Low GUID");
                packet.Translator.ReadUInt32("Item count");
            }
        }

        [Parser(Opcode.CMSG_SEND_MAIL, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSendMail(Packet packet)
        {
            packet.Translator.ReadGuid("Mailbox GUID");
            packet.Translator.ReadCString("Receiver");
            packet.Translator.ReadCString("Subject");
            packet.Translator.ReadCString("Body");
            packet.Translator.ReadUInt32("Stationery?");
            packet.Translator.ReadUInt32("Unk Uint32");
            var items = packet.Translator.ReadByte("Item Count");
            for (var i = 0; i < items; ++i)
            {
                packet.Translator.ReadByte("Slot", i);
                packet.Translator.ReadGuid("Item GUID", i);
            }
            packet.Translator.ReadUInt32("Money");
            packet.Translator.ReadUInt32("COD");
            packet.Translator.ReadUInt64("Unk Uint64");
            packet.Translator.ReadByte("Unk Byte");

        }

        [Parser(Opcode.CMSG_SEND_MAIL, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleSendMail434(Packet packet)
        {
            var guid = new byte[8];
            packet.Translator.ReadInt32("Unk Int32"); // MailMessage.packageId ?
            packet.Translator.ReadInt32("Stationery?");
            packet.Translator.ReadInt64("COD");
            packet.Translator.ReadInt64("Money");
            var len2 = packet.Translator.ReadBits(12);
            var len1 = packet.Translator.ReadBits(9);
            var count = packet.Translator.ReadBits("Item Count", 5);
            guid[0] = packet.Translator.ReadBit();
            var guid2 = new byte[count][];
            for (var i = 0; i < count; i++)
            {
                guid2[i] = packet.Translator.StartBitStream(2, 6, 3, 7, 1, 0, 4, 5);
            }
            guid[3] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            var len3 = packet.Translator.ReadBits(7);
            guid[2] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 4);

            for (var i = 0; i < count; i++)
            {
                if (guid2[i][6] != 0) guid2[i][6] = packet.Translator.ReadByte();
                if (guid2[i][1] != 0) guid2[i][1] = packet.Translator.ReadByte();
                if (guid2[i][7] != 0) guid2[i][7] = packet.Translator.ReadByte();
                if (guid2[i][2] != 0) guid2[i][2] = packet.Translator.ReadByte();
                packet.Translator.ReadByte("Slot", i);
                if (guid2[i][3] != 0) guid2[i][3] = packet.Translator.ReadByte();
                if (guid2[i][0] != 0) guid2[i][0] = packet.Translator.ReadByte();
                if (guid2[i][4] != 0) guid2[i][4] = packet.Translator.ReadByte();
                if (guid2[i][5] != 0) guid2[i][5] = packet.Translator.ReadByte();
                packet.Translator.WriteGuid("Item Guid", guid2[i], i);
            }

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.ReadWoWString("Subject", len1);
            packet.Translator.ReadWoWString("Receiver", len3);

            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.ReadWoWString("Body", len2);

            packet.Translator.ReadXORByte(guid, 1);

            packet.Translator.WriteGuid("Mailbox Guid", guid);
        }

        [Parser(Opcode.CMSG_MAIL_TAKE_ITEM)]
        public static void HandleMailTakeItem(Packet packet)
        {
            packet.Translator.ReadGuid("Mailbox GUID");
            packet.Translator.ReadUInt32("Mail Id");
            packet.Translator.ReadUInt32("Item Low GUID");
        }

        //CMSG_MAELSTROM_GM_SENT_MAIL
    }
}
