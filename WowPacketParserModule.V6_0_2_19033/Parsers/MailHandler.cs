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
            packet.ReadUInt32("MailID");
            packet.ReadUInt32E<MailActionType>("Command");
            packet.ReadUInt32E<MailErrorType>("ErrorCode");
            packet.ReadUInt32("BagResult");
            packet.ReadUInt32("AttachID");
            packet.ReadUInt32("QtyInInventory");
        }

        public static void ReadMailListEntry(Packet packet, params object[] idx)
        {
            packet.ReadInt32("MailID", idx);
            packet.ReadByteE<MailType>("SenderType", idx);

            if (!ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678))
            {
                packet.ResetBitReader();

                var bit4 = packet.ReadBit();
                var bit12 = packet.ReadBit();

                if (bit4)
                    packet.ReadInt32("VirtualRealmAddress", idx);

                if (bit12)
                    packet.ReadInt32("NativeRealmAddress", idx);
            }

            packet.ReadInt64("Cod", idx);
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.ReadInt32("PackageID", idx);
            packet.ReadInt32("StationeryID", idx);
            packet.ReadInt64("SentMoney", idx);
            packet.ReadInt32("Flags", idx);
            packet.ReadSingle("DaysLeft", idx);
            packet.ReadInt32("MailTemplateID", idx);

            var attachmentsCount = packet.ReadInt32("AttachmentsCount", idx);
            for (var i = 0; i < attachmentsCount; ++i) // Attachments
                ReadMailAttachedItem(packet, idx, i, "MailAttachedItem");

            packet.ResetBitReader();

            var bit24 = packet.ReadBit("HasSenderCharacter", idx);
            var bit52 = packet.ReadBit("HasAltSenderID", idx);

            var bits23 = packet.ReadBits(8);
            var bits87 = packet.ReadBits(13);

            if (bit24)
                packet.ReadPackedGuid128("SenderCharacter", idx);

            if (bit52)
                packet.ReadInt32("AltSenderID", idx);

            packet.ReadWoWString("Subject", bits23, idx);
            packet.ReadWoWString("Body", bits87, idx);
        }

        public static void ReadMailAttachedItem(Packet packet, params object[] idx)
        {
            packet.ReadByte("Position", idx);
            packet.ReadInt32("AttachID", idx);

            // ItemInstance
            ItemHandler.ReadItemInstance(packet, idx);

            for (var k = 0; k < 8; ++k)
            {
                packet.ReadInt32("Enchant", idx, k);
                packet.ReadInt32("Duration", idx, k);
                packet.ReadInt32("Charges", idx, k);
            }

            packet.ReadInt32("Count", idx);
            packet.ReadInt32("Charges", idx);
            packet.ReadInt32("MaxDurability", idx);
            packet.ReadInt32("Durability", idx);

            packet.ResetBitReader();

            packet.ReadBit("Unlocked", idx);
        }

        [Parser(Opcode.SMSG_MAIL_LIST_RESULT)]
        public static void HandleMailListResult(Packet packet)
        {
            var mailsCount = packet.ReadInt32("MailsCount");
            packet.ReadInt32("TotalNumRecords");

            for (var i = 0; i < mailsCount; ++i)
                ReadMailListEntry(packet, i, "MailListEntry");
        }

        [Parser(Opcode.CMSG_MAIL_CREATE_TEXT_ITEM)]
        public static void HandleMailCreateTextItem(Packet packet)
        {
            packet.ReadPackedGuid128("Mailbox");
            packet.ReadUInt32("MailID");
        }

        [Parser(Opcode.CMSG_MAIL_DELETE)]
        public static void HandleMailDelete(Packet packet)
        {
            packet.ReadInt32("MailID");
            packet.ReadInt32("DeleteReason");
        }

        [Parser(Opcode.SMSG_MAIL_QUERY_NEXT_TIME_RESULT, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_0_3_19342)]
        public static void HandleMailQueryNextTimeResult60x(Packet packet)
        {
            packet.ReadSingle("NextMailTime");

            var int5 = packet.ReadInt32("NextCount");

            for (int i = 0; i < int5; i++)
            {
                packet.ReadPackedGuid128("SenderGUID", i);

                // PlayerGuidLookupHint
                packet.ResetBitReader();

                var bit4 = packet.ReadBit("HasVirtualRealmAddress", i);
                var bit12 = packet.ReadBit("HasNativeRealmAddress", i);

                if (bit4)
                    packet.ReadInt32("VirtualRealmAddress", i);

                if (bit12)
                    packet.ReadInt32("NativeRealmAddress", i);

                packet.ReadSingle("TimeLeft", i);
                packet.ReadInt32("AltSenderID", i);
                packet.ReadByte("AltSenderType", i);
                packet.ReadInt32("StationeryID", i);
            }
        }

        [Parser(Opcode.SMSG_MAIL_QUERY_NEXT_TIME_RESULT, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleMailQueryNextTimeResult61x(Packet packet)
        {
            packet.ReadSingle("NextMailTime");

            var int5 = packet.ReadInt32("NextCount");

            for (int i = 0; i < int5; i++)
            {
                packet.ReadPackedGuid128("SenderGUID", i);

                packet.ReadSingle("TimeLeft", i);
                packet.ReadInt32("AltSenderID", i);
                packet.ReadByte("AltSenderType", i);
                packet.ReadInt32("StationeryID", i);
            }
        }

        [Parser(Opcode.CMSG_MAIL_TAKE_ITEM)]
        public static void HandleMailTakeItem(Packet packet)
        {
            packet.ReadPackedGuid128("Mailbox");
            packet.ReadInt32("MailID");
            packet.ReadInt32("AttachID");
        }

        [Parser(Opcode.CMSG_SEND_MAIL)]
        public static void HandleSendMail(Packet packet)
        {
            packet.ReadPackedGuid128("Mailbox");
            packet.ReadInt32("StationeryID");
            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.ReadInt32("PackageID");
            packet.ReadInt64("SendMoney");
            packet.ReadInt64("Cod");

            var nameLength = packet.ReadBits(9);
            var subjectLength = packet.ReadBits(9);
            var bodyLength = packet.ReadBits(11);
            var itemCount = packet.ReadBits(5);
            packet.ResetBitReader();

            packet.ReadWoWString("Target", nameLength);
            packet.ReadWoWString("Subject", subjectLength);
            packet.ReadWoWString("Body", bodyLength);

            for (var i = 0; i < itemCount; i++)
            {
                packet.ReadByte("AttachPosition", i);
                packet.ReadPackedGuid128("ItemGUID", i);
            }
        }

        [Parser(Opcode.CMSG_MAIL_MARK_AS_READ)]
        public static void HandleMailMarkAsRead(Packet packet)
        {
            packet.ReadPackedGuid128("Mailbox");
            packet.ReadInt32("MailID");
            packet.ReadBit("BiReceipt");
        }

        [Parser(Opcode.CMSG_MAIL_TAKE_MONEY)]
        public static void HandleMailTakeMoney(Packet packet)
        {
            packet.ReadPackedGuid128("Mailbox");
            packet.ReadInt32("MailID");
            packet.ReadInt64("Money");
        }

        [Parser(Opcode.CMSG_MAIL_GET_LIST)]
        public static void HandleGetMailList(Packet packet)
        {
            packet.ReadPackedGuid128("Mailbox");
            var int32 = packet.ReadInt32("Count");
            for (int i = 0; i < int32; i++)
                packet.ReadInt64("LowGuid?");
        }

        [Parser(Opcode.SMSG_NOTIFY_RECEIVED_MAIL)]
        public static void HandleNotifyReceivedMail(Packet packet)
        {
            packet.ReadSingle("Delay");
        }

        [Parser(Opcode.CMSG_MAIL_RETURN_TO_SENDER)]
        public static void HandleMailReturnToSender(Packet packet)
        {
            packet.ReadInt32("MailID");
            packet.ReadPackedGuid128("SenderGUID");
        }

        [Parser(Opcode.SMSG_SHOW_MAILBOX)]
        public static void HandleShowMailbox(Packet packet)
        {
            packet.ReadPackedGuid128("PostmasterGUID");
        }

    }
}
