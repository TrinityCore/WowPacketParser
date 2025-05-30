using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class MailHandler
    {
        [Parser(Opcode.CMSG_MAIL_GET_LIST)]
        public static void HandleGetMailList(Packet packet)
        {
            packet.ReadPackedGuid128("Mailbox");
        }

        public static void ReadMailListEntry(Packet packet, params object[] idx)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_5_47777))
                packet.ReadUInt64("MailID");
            else
                packet.ReadInt32("MailID");

            MailType senderType;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_5_50232))
                senderType = packet.ReadUInt32E<MailType>("SenderType", idx);
            else
                senderType = packet.ReadByteE<MailType>("SenderType", idx);
            packet.ReadInt64("Cod", idx);
            packet.ReadInt32("StationeryID", idx);
            packet.ReadInt64("SentMoney", idx);
            packet.ReadInt32("Flags", idx);
            packet.ReadSingle("DaysLeft", idx);
            packet.ReadInt32("MailTemplateID", idx);
            var attachmentsCount = packet.ReadInt32("AttachmentsCount", idx);

            packet.ResetBitReader();

            var hasSenderCharacter = false;
            var hasAltSenderID = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_5_50232))
            {
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
            }
            else
            {
                hasSenderCharacter = packet.ReadBit("HasSenderCharacter", idx);
                hasAltSenderID = packet.ReadBit("HasAltSenderID", idx);
            }

            var subjectLen = packet.ReadBits(8);
            var bodyLen = packet.ReadBits(13);

            for (var i = 0; i < attachmentsCount; ++i) // Attachments
                ReadMailAttachedItem(packet, idx, "Attachments", i);

            if (hasSenderCharacter)
                packet.ReadPackedGuid128("SenderCharacter", idx);

            if (hasAltSenderID)
                packet.ReadInt32("AltSenderID", idx);

            packet.ReadWoWString("Subject", subjectLen, idx);
            packet.ReadWoWString("Body", bodyLen, idx);
        }

        public static void ReadMailAttachedItem(Packet packet, params object[] idx)
        {
            packet.ReadByte("Position", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_5_47777))
                packet.ReadUInt64("AttachID", idx);
            else
                packet.ReadInt32("AttachID", idx);
            packet.ReadInt32("Count", idx);
            packet.ReadInt32("Charges", idx);
            packet.ReadInt32("MaxDurability", idx);
            packet.ReadInt32("Durability", idx);

            // ItemInstance
            Substructures.ItemHandler.ReadItemInstance(packet, idx, "Item");

            packet.ResetBitReader();

            var bits1 = packet.ReadBits(4);
            var bits2 = packet.ReadBits(2);
            packet.ReadBit("Unlocked", idx);

            for (var i = 0; i < bits1; i++)
                Substructures.ItemHandler.ReadItemGemData(packet, idx, "Enchants", i);

            for (var i = 0; i < bits2; i++)
                Substructures.ItemHandler.ReadItemEnchantData(packet, idx, "Gems", i);
        }

        [Parser(Opcode.SMSG_MAIL_LIST_RESULT)]
        public static void HandleMailListResult(Packet packet)
        {
            var mailsCount = packet.ReadInt32("MailsCount");
            packet.ReadInt32("TotalNumRecords");

            for (var i = 0; i < mailsCount; ++i)
                ReadMailListEntry(packet, "Mails", i);
        }
    }
}
