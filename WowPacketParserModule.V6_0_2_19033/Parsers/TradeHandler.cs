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
            packet.Translator.ReadUInt32("StateIndex");
        }

        [Parser(Opcode.SMSG_TRADE_UPDATED)]
        public static void HandleTradeUpdated(Packet packet)
        {
            packet.Translator.ReadByte("WhichPlayer");

            packet.Translator.ReadInt32("ID");
            packet.Translator.ReadInt32("CurrentStateIndex");
            packet.Translator.ReadInt32("ClientStateIndex");

            packet.Translator.ReadInt64("Gold");

            // Order guessed
            packet.Translator.ReadInt32("CurrencyType");
            packet.Translator.ReadInt32("CurrencyQuantity");
            packet.Translator.ReadInt32("ProposedEnchantment");

            var count = packet.Translator.ReadInt32("ItemCount");

            for (int i = 0; i < count; i++)
            {
                packet.Translator.ReadByte("Slot", i);
                packet.Translator.ReadInt32("EntryID", i);
                packet.Translator.ReadInt32("StackCount", i);

                packet.Translator.ReadPackedGuid128("GiftCreator", i);

                packet.Translator.ResetBitReader();

                var bit32 = packet.Translator.ReadBit("HasUnwrapped", i);
                if (bit32)
                {
                    ItemHandler.ReadItemInstance(packet, i);

                    packet.Translator.ReadInt32("EnchantID", i);
                    packet.Translator.ReadInt32("OnUseEnchantmentID", i);

                    for (int j = 0; j < 3; j++)
                        packet.Translator.ReadInt32("SocketEnchant", i, j);

                    packet.Translator.ReadPackedGuid128("Creator", i);

                    packet.Translator.ReadInt32("MaxDurability", i);
                    packet.Translator.ReadInt32("Durability", i);
                    packet.Translator.ReadInt32("Charges", i);

                    packet.Translator.ResetBitReader();

                    packet.Translator.ReadBit("Lock", i);
                }
            }
        }

        [Parser(Opcode.SMSG_TRADE_STATUS)]
        public static void HandleTradeStatus(Packet packet)
        {
            packet.Translator.ReadBit("FailureForYou");

            var status = packet.Translator.ReadBits("Status", 5);

            if (status == 13)
                packet.Translator.ReadBit("PartnerIsSameBnetAccount");

            if (status == 13)
            {
                packet.Translator.ReadInt32("CurrencyType");
                packet.Translator.ReadInt32("CurrencyQuantity");
            }

            if (status == 31)
                packet.Translator.ReadInt32("ID");

            if (status == 4)
            {
                packet.Translator.ReadPackedGuid128("PartnerGuid");
                packet.Translator.ReadPackedGuid128("PartnerWowAccount");
            }

            if (status == 1 || status == 0)
                packet.Translator.ReadByte("TradeSlot");

            if (status == 8 || status == 21)
            {
                packet.Translator.ReadInt32("BagResult");
                packet.Translator.ReadInt32("ItemID");
            }
        }

        [Parser(Opcode.CMSG_INITIATE_TRADE)]
        public static void HandleInitiateTrade(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.CMSG_SHOW_TRADE_SKILL)]
        public static void HandleShowTradeSkill(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("PlayerGUID");
            packet.Translator.ReadInt32<SpellId>("SpellID");
            packet.Translator.ReadInt32("SkillLineID");
        }
    }
}
