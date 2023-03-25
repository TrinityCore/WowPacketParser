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

            packet.ReadByteE<MailType>("SenderType", idx);
            packet.ReadInt64("Cod", idx);
            packet.ReadInt32("StationeryID", idx);
            packet.ReadInt64("SentMoney", idx);
            packet.ReadInt32("Flags", idx);
            packet.ReadSingle("DaysLeft", idx);
            packet.ReadInt32("MailTemplateID", idx);
            var attachmentsCount = packet.ReadInt32("AttachmentsCount", idx);

            packet.ResetBitReader();

            var bit24 = packet.ReadBit("HasSenderCharacter", idx);
            var bit52 = packet.ReadBit("HasAltSenderID", idx);

            var bits23 = packet.ReadBits(8);
            var bits87 = packet.ReadBits(13);

            for (var i = 0; i < attachmentsCount; ++i) // Attachments
                ReadMailAttachedItem(packet, idx, i, "MailAttachedItem");

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
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_5_47777))
                packet.ReadUInt64("AttachID");
            else
                packet.ReadInt32("AttachID");
            packet.ReadInt32("Count", idx);
            packet.ReadInt32("Charges", idx);
            packet.ReadInt32("MaxDurability", idx);
            packet.ReadInt32("Durability", idx);

            // ItemInstance
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

        [Parser(Opcode.SMSG_MAIL_LIST_RESULT)]
        public static void HandleMailListResult(Packet packet)
        {
            var mailsCount = packet.ReadInt32("MailsCount");
            packet.ReadInt32("TotalNumRecords");

            for (var i = 0; i < mailsCount; ++i)
                ReadMailListEntry(packet, i, "MailListEntry");
        }
    }
}
