using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class MailHandler
    {
        [Parser(Opcode.SMSG_RECEIVED_MAIL)]
        public static void HandleReceivedMail(Packet packet)
        {
            packet.ReadSingle("Time left"); // Sup with timers in float?
        }

        [Parser(Opcode.SMSG_SHOW_MAILBOX)]
        public static void HandleShowMailbox(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_GET_MAIL_LIST)]
        public static void HandleGetMailList(Packet packet)
        {
            packet.AsHex();
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_MAIL_TAKE_MONEY)]
        public static void HandleTakeMoney(Packet packet)
        {
            packet.ReadUInt32("Mail Id");
            packet.ReadUInt64("Money");

            var guid = packet.StartBitStream(3, 5, 4, 2, 6, 1, 0, 7);
            packet.ParseBitStream(guid, 5, 6, 0, 2, 3, 7, 1, 4);

            packet.WriteGuid("Guid Target", guid);
        }

        [Parser(Opcode.CMSG_MAIL_CREATE_TEXT_ITEM)]
        public static void HandleMailCreate(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadUInt32("Mail Id");

                var guid = packet.StartBitStream(3, 1, 0, 7, 4, 5, 2, 6);
                packet.ParseBitStream(guid, 7, 0, 4, 6, 5, 2, 3, 1);

                packet.WriteGuid("Guid Target", guid);
            }
            else
            {
                packet.WriteLine("              : SMSG_???");
                //packet.Opcode = (int)Opcode.CMSG_LOOT_MONEY;
                packet.ReadToEnd();
            }

        }

        [Parser(Opcode.CMSG_MAIL_MARK_AS_READ)]
        public static void HandleMarkMail(Packet packet)
        {
            var guid = new byte[8];

            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            packet.ReadUInt32("Mail Id");
            guid[5] = packet.ReadBit();

            packet.ParseBitStream(guid, 2, 5, 0, 6, 1, 3, 7, 4);

            packet.WriteGuid("Guid", guid);

            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_MAIL_DELETE)]
        public static void HandleMailDelete(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                //packet.ReadGuid("Mailbox GUID");
                packet.ReadUInt32("Template Id");
                packet.ReadUInt32("Mail Id");
            } 
            else
            {
                packet.WriteLine("              : SMSG_???");
                //packet.Opcode = (int)Opcode.CMSG_MOUNTSPECIAL_ANIM;
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.CMSG_MAIL_RETURN_TO_SENDER)]
        public static void HandleMailReturnToSender(Packet packet)
        {
            packet.ReadUInt32("Mail Id");

            var guid = packet.StartBitStream(1, 7, 6, 3, 4, 0, 2, 5);
            packet.ParseBitStream(guid, 7, 3, 6, 0, 2, 1, 5, 4);

            packet.WriteGuid("Guid Target", guid);
        }

        [Parser(Opcode.SMSG_MAIL_LIST_RESULT)]
        public static void HandleMailListResult(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                packet.ReadUInt32("Total Mails");

            var count = packet.ReadByte("Shown Mails");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadUInt16("Message Size", i);
                packet.ReadUInt32("Mail Id", i);

                var mailType = packet.ReadEnum<MailType>("Message Type", TypeCode.Byte, i);
                switch (mailType) // Read GUID if MailType.Normal, int32 (entry) if not
                {
                    case MailType.Normal:
                        packet.ReadGuid("Player GUID", i);
                        break;
                    case MailType.Creature:
                        packet.ReadEntryWithName<Int32>(StoreNameType.Unit, "Entry", i);
                        break;
                    case MailType.GameObject:
                        packet.ReadEntryWithName<Int32>(StoreNameType.GameObject, "Entry", i);
                        break;
                    case MailType.Item:
                        packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Entry", i);
                        break;
                    case (MailType)1:
                    case MailType.Auction:
                        packet.ReadInt32("Entry", i);
                        break;
                }

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                    packet.ReadUInt64("COD", i);
                else
                    packet.ReadUInt32("COD", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_3_0_10958))
                    packet.ReadUInt32("Item Text Id", i);

                packet.ReadUInt32("Package", i); // Package.dbc ID
                packet.ReadUInt32("Stationery", i); // Stationary.dbc ID
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                    packet.ReadUInt64("Money", i);
                else
                    packet.ReadUInt32("Money", i);
                packet.ReadUInt32("Flags", i);
                packet.ReadSingle("Time", i);
                packet.ReadUInt32("Template Id", i); // MailTemplate.dbc ID
                packet.ReadCString("Subject", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
                    packet.ReadCString("Body", i);

                var items = packet.ReadByte("Item Count", i);
                for (var j = 0; j < items; ++j)
                {
                    packet.ReadByte("Item Index", i, j);
                    packet.ReadUInt32("Item GuidLow", i, j);
                    packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Item Id", i, j);

                    int enchantmentCount = 6;
                    if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                        enchantmentCount = 7;
                    if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                        enchantmentCount = 9;
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                        enchantmentCount = 10;

                    for (var k = 0; k < enchantmentCount; ++k)
                    {
                        packet.ReadUInt32("Item Enchantment Id", i, j, k);
                        packet.ReadUInt32("Item Enchantment Duration", i, j, k);
                        packet.ReadUInt32("Item Enchantment Charges", i, j, k);
                    }

                    packet.ReadInt32("Item Random Property Id", i, j);
                    packet.ReadUInt32("Item Suffix Factor", i, j);

                    if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                        packet.ReadUInt32("Item Count", i, j);
                    else
                        packet.ReadByte("Item Count", i, j);

                    packet.ReadUInt32("Item SpellCharges", i, j);
                    packet.ReadUInt32("Item Max Durability", i, j);
                    packet.ReadUInt32("Item Durability", i, j);

                    if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                        packet.ReadByte("Unk byte", i, j);
                }
            }
        }

        [Parser(Opcode.MSG_QUERY_NEXT_MAIL_TIME)]
        public static void HandleNullMail(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
                return;

            // Math.Abs(this float) > 0.0f returns whether the player has received a new mail since last visiting a mailbox
            packet.ReadSingle("Time since last time visiting a mailbox (can be < 0.0)");

            var count = packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadUInt64("GUID", i);
                packet.ReadUInt32("Sender Id", i);
                packet.ReadUInt32("Message type", i);
                packet.ReadUInt32("Stationery", i);
                packet.ReadSingle("Time?", i);
            }
        }

        [Parser(Opcode.SMSG_SEND_MAIL_RESULT)]
        public static void HandleSendMailResult(Packet packet)
        {
            packet.ReadUInt32("Mail Id");
            var action = packet.ReadEnum<MailActionType>("Mail Action", TypeCode.UInt32);
            var error = packet.ReadEnum<MailErrorType>("Mail Error", TypeCode.UInt32);
            if (error == MailErrorType.Equip)
                packet.ReadUInt32("Equip Error");
            else if (action == MailActionType.AttachmentExpired)
            {
                packet.ReadUInt32("Item Low GUID");
                packet.ReadUInt32("Item count");
            }
        }

        [Parser(Opcode.CMSG_SEND_MAIL)]
        public static void HandleSendMail(Packet packet)
        {
            var guid = new byte[8];
            packet.ReadInt32("Unk Int32"); // MailMessage.packageId ?
            packet.ReadInt32("Stationery?");
            packet.ReadInt64("COD");
            packet.ReadInt64("Money");
            var len2 = packet.ReadBits(12);
            var len1 = packet.ReadBits(9);
            var count = packet.ReadBits("Item Count", 5);
            guid[0] = packet.ReadBit();
            var guid2 = new byte[count][];
            for (var i = 0; i < count; i++)
            {
                guid2[i] = packet.StartBitStream(2, 6, 3, 7, 1, 0, 4, 5);
            }
            guid[3] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var len3 = packet.ReadBits(7);
            guid[2] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            packet.ReadXORByte(guid, 4);

            for (var i = 0; i < count; i++)
            {
                if (guid2[i][6] != 0) guid2[i][6] = packet.ReadByte();
                if (guid2[i][1] != 0) guid2[i][1] = packet.ReadByte();
                if (guid2[i][7] != 0) guid2[i][7] = packet.ReadByte();
                if (guid2[i][2] != 0) guid2[i][2] = packet.ReadByte();
                packet.ReadByte("Slot", i);
                if (guid2[i][3] != 0) guid2[i][3] = packet.ReadByte();
                if (guid2[i][0] != 0) guid2[i][0] = packet.ReadByte();
                if (guid2[i][4] != 0) guid2[i][4] = packet.ReadByte();
                if (guid2[i][5] != 0) guid2[i][5] = packet.ReadByte();
                packet.WriteGuid("Item Guid", guid2[i], i);
            }

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);

            packet.ReadWoWString("Subject", len1);
            packet.ReadWoWString("Receiver", len3);

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 0);

            packet.ReadWoWString("Body", len2);

            packet.ReadXORByte(guid, 1);

            packet.WriteGuid("Mailbox Guid", guid);
        }

        [Parser(Opcode.CMSG_MAIL_TAKE_ITEM)]
        public static void HandleMailTakeItem(Packet packet)
        {
            packet.ReadUInt32("Mail Id");
            packet.ReadUInt32("Item Low GUID");

            var guid = packet.StartBitStream(3, 2, 0, 5, 6, 1, 7, 4);
            packet.ParseBitStream(guid, 3, 2, 0, 5, 6, 7, 1, 4);

            packet.WriteGuid("Guid Target", guid);
        }

        //CMSG_MAELSTROM_GM_SENT_MAIL
    }
}
