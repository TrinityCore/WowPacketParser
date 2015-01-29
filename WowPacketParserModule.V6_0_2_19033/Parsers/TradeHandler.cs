using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class TradeHandler
    {
        [Parser(Opcode.CMSG_ACCEPT_TRADE)]
        public static void HandleAcceptTrade(Packet packet)
        {
            packet.ReadUInt32("StateIndex");
        }

        [Parser(Opcode.SMSG_TRADE_UPDATED)]
        public static void HandleTradeUpdated(Packet packet)
        {
            packet.ReadByte("WhichPlayer");

            packet.ReadInt32("ID");
            packet.ReadInt32("CurrentStateIndex");
            packet.ReadInt32("ClientStateIndex");

            packet.ReadInt64("Gold");

            // Order guessed
            packet.ReadInt32("CurrencyType");
            packet.ReadInt32("CurrencyQuantity");
            packet.ReadInt32("ProposedEnchantment");

            var count = packet.ReadInt32("ItemCount");

            for (int i = 0; i < count; i++)
            {
                packet.ReadByte("Slot", i);
                packet.ReadInt32("EntryID", i);
                packet.ReadInt32("StackCount", i);

                packet.ReadPackedGuid128("GiftCreator", i);

                packet.ResetBitReader();

                var bit32 = packet.ReadBit("HasUnwrapped", i);
                if (bit32)
                {
                    ItemHandler.ReadItemInstance(packet, i);

                    packet.ReadInt32("EnchantID", i);
                    packet.ReadInt32("OnUseEnchantmentID", i);

                    for (int j = 0; j < 3; j++)
                        packet.ReadInt32("SocketEnchant", i, j);

                    packet.ReadPackedGuid128("Creator", i);

                    packet.ReadInt32("MaxDurability", i);
                    packet.ReadInt32("Durability", i);
                    packet.ReadInt32("Charges", i);

                    packet.ResetBitReader();

                    packet.ReadBit("Lock", i);
                }
            }
        }

        [Parser(Opcode.SMSG_TRADE_STATUS)]
        public static void HandleTradeStatus(Packet packet)
        {
            packet.ReadBit("FailureForYou");

            var status = packet.ReadBits("Status", 5);

            if (status == 13)
                packet.ReadBit("PartnerIsSameBnetAccount");

            if (status == 13)
            {
                packet.ReadInt32("CurrencyType");
                packet.ReadInt32("CurrencyQuantity");
            }

            if (status == 31)
                packet.ReadInt32("ID");

            if (status == 4)
            {
                packet.ReadPackedGuid128("PartnerGuid");
                packet.ReadPackedGuid128("PartnerWowAccount");
            }

            if (status == 1 || status == 0)
                packet.ReadByte("TradeSlot");

            if (status == 8 || status == 21)
            {
                packet.ReadInt32("BagResult");
                packet.ReadInt32("ItemID");
            }
        }

        [Parser(Opcode.CMSG_INITIATE_TRADE)]
        public static void HandleInitiateTrade(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_SHOW_TRADE_SKILL)]
        public static void HandleShowTradeSkill(Packet packet)
        {
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadInt32("SpellID");
            packet.ReadInt32("SkillLineID");
        }
    }
}
