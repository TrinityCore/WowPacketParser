using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class MailHandler
    {
        public static void ReadMailAttachedItem(Packet packet, params object[] idx)
        {
            packet.ReadByte("Position", idx);
            packet.ReadUInt64("AttachID");
            packet.ReadInt32("Count", idx);
            packet.ReadInt32("Charges", idx);
            packet.ReadInt32("MaxDurability", idx);
            packet.ReadInt32("Durability", idx);

            Substructures.ItemHandler.ReadItemInstance(packet, idx);

            packet.ResetBitReader();

            var bits1 = packet.ReadBits(4);
            var bits2 = packet.ReadBits(2);
            packet.ReadBit("Unlocked", idx);

            for (var i = 0; i < bits1; i++)
                Substructures.ItemHandler.ReadItemGemData(packet, idx, i);

            for (var i = 0; i < bits2; i++)
                Substructures.ItemHandler.ReadItemEnchantData(packet, idx, i);
        }

        public static void ReadMailListEntry(Packet packet, params object[] idx)
        {
            packet.ReadUInt64("MailID");
            MailType senderType = packet.ReadUInt32E<MailType>("SenderType", idx);
            packet.ReadInt64("Cod", idx);
            packet.ReadInt32("StationeryID", idx);
            packet.ReadInt64("SentMoney", idx);
            packet.ReadInt32("Flags", idx);
            packet.ReadSingle("DaysLeft", idx);
            packet.ReadInt32("MailTemplateID", idx);
            var attachmentsCount = packet.ReadInt32("AttachmentsCount", idx);

            switch (senderType)
            {
                case MailType.Normal:
                    packet.ReadPackedGuid128("SenderCharacter", idx);
                    break;
                case MailType.Auction:
                case MailType.Creature:
                case MailType.GameObject:
                case MailType.Calendar:
                case MailType.Blackmarket:
                case MailType.CommerceAuction:
                case MailType.Auction_2:
                case MailType.ArtisansConsortium:
                    packet.ReadInt32("AltSenderID", idx);
                    break;
                default:
                    break;
            }

            var subjectLen = packet.ReadBits(8);
            var bodyLen = packet.ReadBits(13);

            for (var i = 0; i < attachmentsCount; ++i) // Attachments
                ReadMailAttachedItem(packet, idx, i, "MailAttachedItem");

            packet.ReadWoWString("Subject", subjectLen, idx);
            packet.ReadWoWString("Body", bodyLen, idx);
        }

        [Parser(Opcode.SMSG_MAIL_COMMAND_RESULT)]
        public static void HandleMailCommandResult(Packet packet)
        {
            packet.ReadUInt64("MailID");
            packet.ReadInt32E<MailActionType>("Command");
            packet.ReadInt32E<MailErrorType>("ErrorCode");
            packet.ReadInt32("BagResult");
            packet.ReadUInt64("AttachID");
            packet.ReadInt32("QtyInInventory");
        }

        [Parser(Opcode.SMSG_NOTIFY_RECEIVED_MAIL)]
        public static void HandleNotifyReceivedMail(Packet packet)
        {
            packet.ReadSingle("Delay");
        }

        [Parser(Opcode.SMSG_MAIL_LIST_RESULT)]
        public static void HandleMailListResult(Packet packet)
        {
            var mailsCount = packet.ReadInt32("MailsCount");
            packet.ReadInt32("TotalNumRecords");

            for (var i = 0; i < mailsCount; ++i)
                ReadMailListEntry(packet, i, "MailListEntry");
        }

        [Parser(Opcode.SMSG_MAIL_QUERY_NEXT_TIME_RESULT)]
        public static void HandleMailQueryNextTimeResult(Packet packet)
        {
            packet.ReadSingle("NextMailTime");

            var nextCount = packet.ReadInt32("NextCount");

            for (int i = 0; i < nextCount; i++)
            {
                packet.ReadPackedGuid128("SenderGUID", i);

                packet.ReadSingle("TimeLeft", i);
                packet.ReadInt32("AltSenderID", i);
                packet.ReadByte("AltSenderType", i);
                packet.ReadInt32("StationeryID", i);
            }
        }
    }
}
