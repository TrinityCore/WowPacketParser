using System;
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

        [Parser(Opcode.SMSG_SEND_MAIL_RESULT)]
        public static void HandleSendMailResult(Packet packet)
        {
            packet.ReadUInt32("MailID");
            packet.ReadEnum<MailActionType>("Command", TypeCode.UInt32);
            packet.ReadEnum<MailErrorType>("ErrorCode", TypeCode.UInt32);
            packet.ReadUInt32("BagResult");
            packet.ReadUInt32("AttachID");
            packet.ReadUInt32("QtyInInventory");
        }

        [Parser(Opcode.SMSG_MAIL_LIST_RESULT)]
        public static void HandleMailListResult(Packet packet)
        {
            packet.ReadInt32("TotalNumRecords");

            var int4 = packet.ReadInt32("Shown Mails");

            for (var i = 0; i < int4; ++i)
            {
                packet.ReadInt32("MailID", i);
                packet.ReadEnum<MailType>("SenderType", TypeCode.Byte, i);

                packet.ResetBitReader();

                var bit4 = packet.ReadBit();
                var bit12 = packet.ReadBit();

                if (bit4)
                    packet.ReadInt32("VirtualRealmAddress", i);

                if (bit12)
                    packet.ReadInt32("NativeRealmAddress", i);

                packet.ReadInt64("Cod", i);
                packet.ReadInt32("PackageID", i);
                packet.ReadInt32("StationeryID", i);
                packet.ReadInt64("SentMoney", i);
                packet.ReadInt32("Flags", i);
                packet.ReadSingle("DaysLeft", i);
                packet.ReadInt32("MailTemplateID", i);

                // CliMailAttachedItem
                var int8348 = packet.ReadInt32("AttachmentsCount", i);
                for (var j = 0; j < int8348; ++j)
                {
                    packet.ReadByte("Position", i, j);
                    packet.ReadInt32("AttachID", i, j);

                    // ItemInstance
                    packet.ReadEntry<Int32>(StoreNameType.Item, "ItemID", i, j);
                    packet.ReadInt32("RandomPropertiesSeed", i, j);
                    packet.ReadInt32("RandomPropertiesID", i, j);

                    packet.ResetBitReader();

                    var hasBonuses = packet.ReadBit("HasItemBonus", i, j);
                    var hasModifications = packet.ReadBit("HasModifications", i, j);
                    if (hasBonuses)
                    {
                        packet.ReadByte("Context", i, j);

                        var bonusCount = packet.ReadUInt32();
                        for (var k = 0; k < bonusCount; ++k)
                            packet.ReadUInt32("BonusListID", i, j, k);
                    }

                    if (hasModifications)
                    {
                        var modificationCount = packet.ReadUInt32() / 4;
                        for (var k = 0; k < modificationCount; ++k)
                            packet.ReadUInt32("Modification", i, j, k);
                    }

                    for (var k = 0; k < 8; ++k)
                    {
                        packet.ReadInt32("Enchant", i, j, k);
                        packet.ReadInt32("Duration", i, j, k);
                        packet.ReadInt32("Charges", i, j, k);
                    }

                    packet.ReadInt32("Count", i, j);
                    packet.ReadInt32("Charges", i, j);
                    packet.ReadInt32("MaxDurability", i, j);
                    packet.ReadInt32("Durability", i, j);

                    packet.ResetBitReader();

                    packet.ReadBit("Unlocked", i, j);
                }

                packet.ResetBitReader();

                var bit24 = packet.ReadBit("HasSenderCharacter", i);
                var bit52 = packet.ReadBit("HasAltSenderID", i);

                var bits23 = packet.ReadBits(8);
                var bits87 = packet.ReadBits(13);

                if (bit24)
                    packet.ReadPackedGuid128("SenderCharacter", i);

                if (bit52)
                    packet.ReadInt32("AltSenderID", i);

                packet.ReadWoWString("Subject", bits23, i);
                packet.ReadWoWString("Body", bits87, i);
            }
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
            packet.ReadInt32("DeleteReason");
            packet.ReadInt32("MailID");
        }

        [Parser(Opcode.SMSG_MAIL_QUERY_NEXT_TIME_RESULT)]
        public static void HandleMailQueryNextTimeResult(Packet packet)
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
    }
}
