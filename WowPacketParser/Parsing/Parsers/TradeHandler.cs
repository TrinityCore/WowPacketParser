using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class TradeHandler
    {
        [Parser(Opcode.CMSG_INITIATE_TRADE)]
        public static void HandleInitiateTrade(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_SET_TRADE_ITEM)]
        public static void HandleTradeItem(Packet packet)
        {
            packet.ReadByte("Trade Slot");
            packet.ReadSByte("Bag");
            packet.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_CLEAR_TRADE_ITEM)]
        public static void HandleClearTradeItem(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596)) // Need correct version
                packet.ReadInt32("Slot");
            else
                packet.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_SET_TRADE_GOLD)]
        public static void HandleTradeGold(Packet packet)
        {
            packet.ReadUInt32("Gold");
        }

        [Parser(Opcode.SMSG_TRADE_STATUS)]
        public static void HandleTradeStatus(Packet packet)
        {
            var status = packet.ReadEnum<TradeStatus>("Status", TypeCode.UInt32);
            switch (status)
            {
                case TradeStatus.BeginTrade:
                    packet.ReadGuid("GUID");
                    break;
                case TradeStatus.OpenWindow:
                    packet.ReadUInt32("Trade Id");
                    break;
                case TradeStatus.CloseWindow:
                    packet.ReadUInt32("Unk UInt32 1");
                    packet.ReadByte("Unk Byte");
                    packet.ReadUInt32("Unk UInt32 2");
                    break;
                case TradeStatus.OnlyConjured:
                case TradeStatus.NotEligible:
                    packet.ReadByte("Unk Byte");
                    break;
            }
        }

        [Parser(Opcode.SMSG_TRADE_STATUS_EXTENDED)]
        public static void HandleTradeStatusExtended(Packet packet)
        {
            packet.ReadByte("Trader");
            packet.ReadUInt32("Trade Id");
            packet.ReadUInt32("Unk Slot 1");
            packet.ReadUInt32("Unk Slot 2");
            packet.ReadUInt32("Gold");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");

            while (packet.CanRead())
            {
                var slot = packet.ReadByte("Slot Index");
                packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Item Entry", slot);
                packet.ReadUInt32("Item Display ID", slot);
                packet.ReadUInt32("Item Count", slot);
                packet.ReadUInt32("Item Wrapped", slot);
                packet.ReadUInt64("Item Gift Creator GUID", slot);
                packet.ReadUInt32("Item Perm Enchantment Id", slot);
                for (var i = 0; i < 3; ++i)
                    packet.ReadUInt32("Item Enchantment Id", slot, i);
                packet.ReadGuid("Item Creator GUID", slot);
                packet.ReadInt32("Item Spell Charges", slot);
                packet.ReadInt32("Item Suffix Factor", slot);
                packet.ReadInt32("Item Random Property ID", slot);
                packet.ReadUInt32("Item Lock ID", slot);
                packet.ReadUInt32("Item Max Durability", slot);
                packet.ReadUInt32("Item Durability", slot);
            }
        }

        [Parser(Opcode.CMSG_ACCEPT_TRADE)]
        public static void HandleAcceptTrade(Packet packet)
        {
            packet.ReadUInt32("Unk UInt32");
        }

        [Parser(Opcode.CMSG_IGNORE_TRADE)]
        [Parser(Opcode.CMSG_BUSY_TRADE)]
        [Parser(Opcode.CMSG_CANCEL_TRADE)]
        [Parser(Opcode.CMSG_UNACCEPT_TRADE)]
        [Parser(Opcode.CMSG_BEGIN_TRADE)]
        public static void HandleNullTrade(Packet packet)
        {
        }
    }
}
