using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class MailHandler
    {
        [Parser(Opcode.CMSG_QUERY_NEXT_MAIL_TIME)]
        public static void HandleNullMail(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_MAIL_COMMAND_RESULT)]
        public static void HandleMailCommandResult(Packet packet)
        {
            packet.Translator.ReadUInt32("MailID");
            packet.Translator.ReadUInt32E<MailActionType>("Command");
            packet.Translator.ReadUInt32E<MailErrorType>("ErrorCode");
            packet.Translator.ReadUInt32("BagResult");
            packet.Translator.ReadUInt32("AttachID");
            packet.Translator.ReadUInt32("QtyInInventory");
        }

        public static void ReadMailListEntry(Packet packet, params object[] idx)
        {
            packet.Translator.ReadInt32("MailID", idx);
            packet.Translator.ReadByteE<MailType>("SenderType", idx);

            if (!ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678))
            {
                packet.Translator.ResetBitReader();

                var bit4 = packet.Translator.ReadBit();
                var bit12 = packet.Translator.ReadBit();

                if (bit4)
                    packet.Translator.ReadInt32("VirtualRealmAddress", idx);

                if (bit12)
                    packet.Translator.ReadInt32("NativeRealmAddress", idx);
            }

            packet.Translator.ReadInt64("Cod", idx);
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.Translator.ReadInt32("PackageID", idx);
            packet.Translator.ReadInt32("StationeryID", idx);
            packet.Translator.ReadInt64("SentMoney", idx);
            packet.Translator.ReadInt32("Flags", idx);
            packet.Translator.ReadSingle("DaysLeft", idx);
            packet.Translator.ReadInt32("MailTemplateID", idx);

            var attachmentsCount = packet.Translator.ReadInt32("AttachmentsCount", idx);
            for (var i = 0; i < attachmentsCount; ++i) // Attachments
                ReadMailAttachedItem(packet, idx, i, "MailAttachedItem");

            packet.Translator.ResetBitReader();

            var bit24 = packet.Translator.ReadBit("HasSenderCharacter", idx);
            var bit52 = packet.Translator.ReadBit("HasAltSenderID", idx);

            var bits23 = packet.Translator.ReadBits(8);
            var bits87 = packet.Translator.ReadBits(13);

            if (bit24)
                packet.Translator.ReadPackedGuid128("SenderCharacter", idx);

            if (bit52)
                packet.Translator.ReadInt32("AltSenderID", idx);

            packet.Translator.ReadWoWString("Subject", bits23, idx);
            packet.Translator.ReadWoWString("Body", bits87, idx);
        }

        public static void ReadMailAttachedItem(Packet packet, params object[] idx)
        {
            packet.Translator.ReadByte("Position", idx);
            packet.Translator.ReadInt32("AttachID", idx);

            // ItemInstance
            ItemHandler.ReadItemInstance(packet, idx);

            for (var k = 0; k < 8; ++k)
            {
                packet.Translator.ReadInt32("Enchant", idx, k);
                packet.Translator.ReadInt32("Duration", idx, k);
                packet.Translator.ReadInt32("Charges", idx, k);
            }

            packet.Translator.ReadInt32("Count", idx);
            packet.Translator.ReadInt32("Charges", idx);
            packet.Translator.ReadInt32("MaxDurability", idx);
            packet.Translator.ReadInt32("Durability", idx);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Unlocked", idx);
        }

        [Parser(Opcode.SMSG_MAIL_LIST_RESULT)]
        public static void HandleMailListResult(Packet packet)
        {
            var mailsCount = packet.Translator.ReadInt32("MailsCount");
            packet.Translator.ReadInt32("TotalNumRecords");

            for (var i = 0; i < mailsCount; ++i)
                ReadMailListEntry(packet, i, "MailListEntry");
        }

        [Parser(Opcode.CMSG_MAIL_CREATE_TEXT_ITEM)]
        public static void HandleMailCreateTextItem(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Mailbox");
            packet.Translator.ReadUInt32("MailID");
        }

        [Parser(Opcode.CMSG_MAIL_DELETE)]
        public static void HandleMailDelete(Packet packet)
        {
            packet.Translator.ReadInt32("MailID");
            packet.Translator.ReadInt32("DeleteReason");
        }

        [Parser(Opcode.SMSG_MAIL_QUERY_NEXT_TIME_RESULT, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_0_3_19342)]
        public static void HandleMailQueryNextTimeResult60x(Packet packet)
        {
            packet.Translator.ReadSingle("NextMailTime");

            var int5 = packet.Translator.ReadInt32("NextCount");

            for (int i = 0; i < int5; i++)
            {
                packet.Translator.ReadPackedGuid128("SenderGUID", i);

                // PlayerGuidLookupHint
                packet.Translator.ResetBitReader();

                var bit4 = packet.Translator.ReadBit("HasVirtualRealmAddress", i);
                var bit12 = packet.Translator.ReadBit("HasNativeRealmAddress", i);

                if (bit4)
                    packet.Translator.ReadInt32("VirtualRealmAddress", i);

                if (bit12)
                    packet.Translator.ReadInt32("NativeRealmAddress", i);

                packet.Translator.ReadSingle("TimeLeft", i);
                packet.Translator.ReadInt32("AltSenderID", i);
                packet.Translator.ReadByte("AltSenderType", i);
                packet.Translator.ReadInt32("StationeryID", i);
            }
        }

        [Parser(Opcode.SMSG_MAIL_QUERY_NEXT_TIME_RESULT, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleMailQueryNextTimeResult61x(Packet packet)
        {
            packet.Translator.ReadSingle("NextMailTime");

            var int5 = packet.Translator.ReadInt32("NextCount");

            for (int i = 0; i < int5; i++)
            {
                packet.Translator.ReadPackedGuid128("SenderGUID", i);

                packet.Translator.ReadSingle("TimeLeft", i);
                packet.Translator.ReadInt32("AltSenderID", i);
                packet.Translator.ReadByte("AltSenderType", i);
                packet.Translator.ReadInt32("StationeryID", i);
            }
        }

        [Parser(Opcode.CMSG_MAIL_TAKE_ITEM)]
        public static void HandleMailTakeItem(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Mailbox");
            packet.Translator.ReadInt32("MailID");
            packet.Translator.ReadInt32("AttachID");
        }

        [Parser(Opcode.CMSG_SEND_MAIL)]
        public static void HandleSendMail(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Mailbox");
            packet.Translator.ReadInt32("StationeryID");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.Translator.ReadInt32("PackageID");
            packet.Translator.ReadInt64("SendMoney");
            packet.Translator.ReadInt64("Cod");

            var nameLength = packet.Translator.ReadBits(9);
            var subjectLength = packet.Translator.ReadBits(9);
            var bodyLength = packet.Translator.ReadBits(11);
            var itemCount = packet.Translator.ReadBits(5);
            packet.Translator.ResetBitReader();

            packet.Translator.ReadWoWString("Target", nameLength);
            packet.Translator.ReadWoWString("Subject", subjectLength);
            packet.Translator.ReadWoWString("Body", bodyLength);

            for (var i = 0; i < itemCount; i++)
            {
                packet.Translator.ReadByte("AttachPosition", i);
                packet.Translator.ReadPackedGuid128("ItemGUID", i);
            }
        }

        [Parser(Opcode.CMSG_MAIL_MARK_AS_READ)]
        public static void HandleMailMarkAsRead(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Mailbox");
            packet.Translator.ReadInt32("MailID");
            packet.Translator.ReadBit("BiReceipt");
        }

        [Parser(Opcode.CMSG_MAIL_TAKE_MONEY)]
        public static void HandleMailTakeMoney(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Mailbox");
            packet.Translator.ReadInt32("MailID");
            packet.Translator.ReadInt64("Money");
        }

        [Parser(Opcode.CMSG_MAIL_GET_LIST)]
        public static void HandleGetMailList(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Mailbox");
            var int32 = packet.Translator.ReadInt32("Count");
            for (int i = 0; i < int32; i++)
                packet.Translator.ReadInt64("LowGuid?");
        }

        [Parser(Opcode.SMSG_NOTIFY_RECEIVED_MAIL)]
        public static void HandleNotifyReceivedMail(Packet packet)
        {
            packet.Translator.ReadSingle("Delay");
        }

        [Parser(Opcode.CMSG_MAIL_RETURN_TO_SENDER)]
        public static void HandleMailReturnToSender(Packet packet)
        {
            packet.Translator.ReadInt32("MailID");
            packet.Translator.ReadPackedGuid128("SenderGUID");
        }

        [Parser(Opcode.SMSG_SHOW_MAILBOX)]
        public static void HandleShowMailbox(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("PostmasterGUID");
        }

    }
}
