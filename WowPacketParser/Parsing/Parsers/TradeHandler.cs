using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class TradeHandler
    {
        [Parser(Opcode.CMSG_INITIATE_TRADE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleInitiateTrade(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_INITIATE_TRADE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleInitiateTrade434(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(0, 3, 5, 1, 4, 6, 7, 2);
            packet.Translator.ParseBitStream(guid, 7, 4, 3, 5, 1, 2, 6, 0);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_SET_TRADE_ITEM, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTradeItem(Packet packet)
        {
            packet.Translator.ReadByte("Trade Slot");
            packet.Translator.ReadSByte("Bag");
            packet.Translator.ReadByte("Bag Slot");
        }

        [Parser(Opcode.CMSG_SET_TRADE_ITEM, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTradeItem434(Packet packet)
        {
            packet.Translator.ReadByte("Bag Slot");
            packet.Translator.ReadByte("Trade Slot");
            packet.Translator.ReadSByte("Bag");
        }

        [Parser(Opcode.CMSG_CLEAR_TRADE_ITEM)]
        public static void HandleClearTradeItem(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596) && ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_4_15595)) // Need correct versions
                packet.Translator.ReadInt32("Slot");
            else
                packet.Translator.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_SET_TRADE_GOLD)]
        public static void HandleTradeGold(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596)) // Need correct version
                packet.Translator.ReadUInt64("Gold");
            else
                packet.Translator.ReadUInt32("Gold");
        }

        [Parser(Opcode.SMSG_TRADE_STATUS, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleTradeStatus(Packet packet)
        {
            var status = packet.Translator.ReadUInt32E<TradeStatus>("Status");
            switch (status)
            {
                case TradeStatus.BeginTrade:
                    packet.Translator.ReadGuid("GUID");
                    break;
                case TradeStatus.OpenWindow:
                    packet.Translator.ReadUInt32("Trade Id");
                    break;
                case TradeStatus.CloseWindow:
                    packet.Translator.ReadUInt32("Unk UInt32 1");
                    packet.Translator.ReadByte("Unk Byte");
                    packet.Translator.ReadUInt32("Unk UInt32 2");
                    break;
                case TradeStatus.OnlyConjured:
                case TradeStatus.NotEligible:
                    packet.Translator.ReadByte("Unk Byte");
                    break;
            }
        }

        [Parser(Opcode.SMSG_TRADE_STATUS, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleTradeStatus422(Packet packet)
        {
            var guid = new byte[8];
            guid[2] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Unk Bit");
            guid[6] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);

            packet.Translator.ReadUInt32("Unk 1");
            packet.Translator.ReadByte("Unk 2");

            packet.Translator.ReadXORByte(guid, 7);

            packet.Translator.ReadInt32("Unk 3");
            packet.Translator.ReadUInt32("Unk 4");

            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.ReadUInt32("Unk 5");
            packet.Translator.ReadByte("Unk 6");

            packet.Translator.ReadXORByte(guid, 4);

            packet.Translator.ReadUInt32("Unk 7");

            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.ReadUInt32("Unk 8");

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_TRADE_STATUS, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTradeStatus434(Packet packet)
        {
            packet.Translator.ReadBit("Unk Bit");
            var status = packet.Translator.ReadBitsE<TradeStatus434>("Status", 5);

            switch (status)
            {
                case TradeStatus434.BeginTrade:
                    var guid = packet.Translator.StartBitStream(2, 4, 6, 0, 1, 3, 7, 5);
                    packet.Translator.ParseBitStream(guid, 4, 1, 2, 3, 0, 7, 6, 5);
                    packet.Translator.WriteGuid("GUID", guid);
                    break;
                case TradeStatus434.CloseWindow:
                    packet.Translator.ReadBit("Unk Bit");
                    packet.Translator.ReadInt32("Unk Int32");
                    packet.Translator.ReadInt32("Unk Int32");
                    break;
                case TradeStatus434.TradeCurrency:
                case TradeStatus434.CurrencyNotTradable:
                    packet.Translator.ReadInt32("Unk Int32 1");
                    packet.Translator.ReadInt32("Unk Int32 2");
                    break;
                case TradeStatus434.NotEligible:
                case TradeStatus434.OnlyConjured:
                    packet.Translator.ReadByte("Unk Byte");
                    break;
                case TradeStatus434.OpenWindow:
                    packet.Translator.ReadInt32("Trade Id");
                    break;
            }
        }

        [Parser(Opcode.SMSG_TRADE_STATUS_EXTENDED, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleTradeStatusExtended(Packet packet)
        {
            packet.Translator.ReadByte("Trader");
            packet.Translator.ReadUInt32("Trade Id");
            packet.Translator.ReadUInt32("Unk Slot 1");
            packet.Translator.ReadUInt32("Unk Slot 2");
            packet.Translator.ReadUInt32("Gold");
            packet.Translator.ReadInt32<SpellId>("Spell ID");

            while (packet.CanRead())
            {
                var slot = packet.Translator.ReadByte("Slot Index");
                packet.Translator.ReadUInt32<ItemId>("Item Entry", slot);
                packet.Translator.ReadUInt32("Item Display ID", slot);
                packet.Translator.ReadUInt32("Item Count", slot);
                packet.Translator.ReadUInt32("Item Wrapped", slot);
                packet.Translator.ReadGuid("Item Gift Creator GUID", slot);
                packet.Translator.ReadUInt32("Item Perm Enchantment Id", slot);
                for (var i = 0; i < 3; ++i)
                    packet.Translator.ReadUInt32("Item Enchantment Id", slot, i);
                packet.Translator.ReadGuid("Item Creator GUID", slot);
                packet.Translator.ReadInt32("Item Spell Charges", slot);
                packet.Translator.ReadInt32("Item Suffix Factor", slot);
                packet.Translator.ReadInt32("Item Random Property ID", slot);
                packet.Translator.ReadUInt32("Item Lock ID", slot);
                packet.Translator.ReadUInt32("Item Max Durability", slot);
                packet.Translator.ReadUInt32("Item Durability", slot);
            }
        }

        [Parser(Opcode.SMSG_TRADE_STATUS_EXTENDED, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleTradeStatusExtended406(Packet packet)
        {
            packet.Translator.ReadUInt32("Unk 1");
            packet.Translator.ReadUInt32("Unk 2");
            packet.Translator.ReadBool("Unk (Has slots?)");
            packet.Translator.ReadUInt32("Unk 3");
            packet.Translator.ReadUInt32("Unk 4");
            packet.Translator.ReadUInt32("Trade Id?");
            var slots = packet.Translator.ReadUInt32("Trade Slots");
            packet.Translator.ReadUInt64("Gold");
            packet.Translator.ReadUInt32("Unk 6");

            for (var i = 0; i < slots; ++i)
            {
                packet.Translator.ReadUInt32("Unk1", i);
                packet.Translator.ReadGuid("Item Creator GUID", i);
                packet.Translator.ReadUInt32("Item Perm Enchantment Id", i);
                packet.Translator.ReadUInt32<ItemId>("Item Entry", i);
                packet.Translator.ReadUInt32("Item Enchantment Id", i, 0);
                packet.Translator.ReadUInt32("Item Durability", i);
                packet.Translator.ReadUInt32("Unk2", i);
                packet.Translator.ReadByte("Unk Byte", i);
                packet.Translator.ReadGuid("Unk Guid: (Item Gift Creator GUID?)", i);
                packet.Translator.ReadUInt32("Unk3", i);
                packet.Translator.ReadByte("Slot Index", i);
                packet.Translator.ReadUInt32("Item Max Durability", i);
                packet.Translator.ReadUInt32("Item Count", i);
                packet.Translator.ReadUInt32("Unk4", i);
                packet.Translator.ReadUInt32("Item Temporal Enchantment", i);
                packet.Translator.ReadUInt32("Item Enchantment Id", i, 1);
                packet.Translator.ReadUInt32("Item Enchantment Id", i, 2);
                packet.Translator.ReadUInt32("Unk5", i);

                // Probably the unks are one of these (+ that "item Temporal Enchantment")
                //packet.Translator.ReadUInt32("Item Display ID", slot);

                //packet.Translator.ReadUInt32("Item Wrapped", slot);
                //packet.Translator.ReadInt32("Item Spell Charges", slot);
                //packet.Translator.ReadInt32("Item Suffix Factor", slot);
                //packet.Translator.ReadInt32("Item Random Property ID", slot);
                //packet.Translator.ReadUInt32("Item Lock ID", slot);
            }
        }

        [Parser(Opcode.SMSG_TRADE_STATUS_EXTENDED, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleTradeStatusExtended422(Packet packet)
        {
            packet.AsHex();
            packet.Translator.ReadInt32("Unk 1");
            packet.Translator.ReadInt32("Unk 2");
            packet.Translator.ReadInt32("Unk 3");
            packet.Translator.ReadInt32("Unk 4");
            packet.Translator.ReadUInt64("Gold");
            packet.Translator.ReadByte("Trader?");
            var count = packet.Translator.ReadInt32("Unk Count");
            packet.Translator.ReadInt32("Unk 7");
            packet.Translator.ReadInt32("Unk 8");

            var guids1 = new byte[count][];
            var guids2 = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guids1[i] = new byte[8];
                guids2[i] = new byte[8];
            }

            for (var i = 0; i < count; ++i)
            {
                guids1[i][0] = packet.Translator.ReadBit();
                guids1[i][5] = packet.Translator.ReadBit();
                guids1[i][7] = packet.Translator.ReadBit();
                guids1[i][1] = packet.Translator.ReadBit();
                guids1[i][6] = packet.Translator.ReadBit();

                guids2[i][5] = packet.Translator.ReadBit();
                guids2[i][3] = packet.Translator.ReadBit();
                guids2[i][0] = packet.Translator.ReadBit();
                guids2[i][6] = packet.Translator.ReadBit();
                guids2[i][2] = packet.Translator.ReadBit();
                guids2[i][4] = packet.Translator.ReadBit();
                guids2[i][1] = packet.Translator.ReadBit();

                guids1[i][3] = packet.Translator.ReadBit();

                guids2[i][7] = packet.Translator.ReadBit();

                guids1[i][2] = packet.Translator.ReadBit();
                guids1[i][4] = packet.Translator.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Unk 1", i);

                packet.Translator.ReadXORByte(guids2[i], 0);
                packet.Translator.ReadXORByte(guids2[i], 3);
                packet.Translator.ReadXORByte(guids2[i], 4);

                packet.Translator.ReadInt32("Unk 2", i);

                packet.Translator.ReadXORByte(guids1[i], 7);

                packet.Translator.ReadInt32("Item Id", i);
                packet.Translator.ReadInt32("Unk 4", i);
                packet.Translator.ReadInt32("Unk 5", i);

                packet.Translator.ReadXORByte(guids2[i], 2);
                packet.Translator.ReadXORByte(guids2[i], 5);

                packet.Translator.ReadInt32("Unk 6", i);

                packet.Translator.ReadXORByte(guids1[i], 1);
                packet.Translator.ReadXORByte(guids2[i], 6);
                packet.Translator.ReadXORByte(guids1[i], 0);

                packet.Translator.ReadInt32("Unk 7", i);
                packet.Translator.ReadUInt32("Unk 8", i);
                packet.Translator.ReadInt32("Unk 9", i);

                packet.Translator.ReadXORByte(guids1[i], 5);

                packet.Translator.ReadInt32("Unk 10", i);

                packet.Translator.ReadXORByte(guids1[i], 6);
                packet.Translator.ReadXORByte(guids2[i], 7);

                packet.Translator.ReadInt32("Unk 11", i);
                packet.Translator.ReadByte("Unk 12", i);

                packet.Translator.ReadXORByte(guids2[i], 1);

                packet.Translator.ReadInt32("Unk 13", i);
                packet.Translator.ReadInt32("Unk 14", i);
                packet.Translator.ReadByte("Unk 15", i);

                packet.Translator.ReadXORByte(guids1[i], 4);
                packet.Translator.ReadXORByte(guids1[i], 2);

                packet.Translator.ReadInt32("Unk 16", i);

                packet.Translator.ReadXORByte(guids1[i], 3);

                packet.Translator.WriteGuid("Item Creator Guid", guids1[i], i);
                packet.Translator.WriteGuid("Item Gift Creator Guid", guids2[i], i);
            }
        }

        [Parser(Opcode.SMSG_TRADE_STATUS_EXTENDED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTradeStatusExtended434(Packet packet)
        {
            packet.Translator.ReadInt32("Trade Id");
            packet.Translator.ReadInt32("Unk Int32 2");
            packet.Translator.ReadInt64("Gold");
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadInt32("Unk Slot 1");
            packet.Translator.ReadInt32("Unk Int32 5");
            packet.Translator.ReadBool("Trader");
            packet.Translator.ReadInt32("Unk Slot 2");

            var count = packet.Translator.ReadBits("Count", 22);

            var guids1 = new byte[count][];
            var guids2 = new byte[count][];

            var isNotWrapped = new bool[count];

            for (int i = 0; i < count; ++i)
            {
                guids1[i] = new byte[8];
                guids1[i][7] = packet.Translator.ReadBit();
                guids1[i][1] = packet.Translator.ReadBit();
                isNotWrapped[i] = packet.Translator.ReadBit("Is Not Wrapped", i);
                guids1[i][3] = packet.Translator.ReadBit();

                if (isNotWrapped[i])
                {
                    guids2[i] = new byte[8];
                    guids2[i][7] = packet.Translator.ReadBit();
                    guids2[i][1] = packet.Translator.ReadBit();
                    guids2[i][4] = packet.Translator.ReadBit();
                    guids2[i][6] = packet.Translator.ReadBit();
                    guids2[i][2] = packet.Translator.ReadBit();
                    guids2[i][3] = packet.Translator.ReadBit();
                    guids2[i][5] = packet.Translator.ReadBit();
                    packet.Translator.ReadBit("Is Locked", i);
                    guids2[i][0] = packet.Translator.ReadBit();
                }

                guids1[i][6] = packet.Translator.ReadBit();
                guids1[i][4] = packet.Translator.ReadBit();
                guids1[i][2] = packet.Translator.ReadBit();
                guids1[i][0] = packet.Translator.ReadBit();
                guids1[i][5] = packet.Translator.ReadBit();
            }

            for (int i = 0; i < count; ++i)
            {
                if (isNotWrapped[i])
                {
                    packet.Translator.ReadXORByte(guids2[i], 1);

                    packet.Translator.ReadInt32("Item Perm Enchantment Id", i);

                    for (int j = 0; j < 3; ++j)
                        packet.Translator.ReadInt32("Item Enchantment Id", i, j);

                    packet.Translator.ReadInt32("Item Max Durability", i);

                    packet.Translator.ReadXORByte(guids2[i], 6);
                    packet.Translator.ReadXORByte(guids2[i], 2);
                    packet.Translator.ReadXORByte(guids2[i], 7);
                    packet.Translator.ReadXORByte(guids2[i], 4);

                    packet.Translator.ReadInt32("Item Reforge Id", i);
                    packet.Translator.ReadInt32("Item Durability", i);
                    packet.Translator.ReadInt32("Item Random Property ID", i);

                    packet.Translator.ReadXORByte(guids2[i], 3);

                    packet.Translator.ReadInt32("Unk Int32 7", i);

                    packet.Translator.ReadXORByte(guids2[i], 0);

                    packet.Translator.ReadInt32("Item Spell Charges", i);
                    packet.Translator.ReadInt32("Item Suffix Factor", i);

                    packet.Translator.ReadXORByte(guids2[i], 5);

                    packet.Translator.WriteGuid("Creator Guid", guids2[i], i);
                }

                packet.Translator.ReadXORByte(guids1[i], 6);
                packet.Translator.ReadXORByte(guids1[i], 1);
                packet.Translator.ReadXORByte(guids1[i], 7);
                packet.Translator.ReadXORByte(guids1[i], 4);
                packet.Translator.ReadUInt32<ItemId>("Item Entry", i);
                packet.Translator.ReadXORByte(guids1[i], 0);
                packet.Translator.ReadUInt32("Item Count", i);
                packet.Translator.ReadXORByte(guids1[i], 5);
                packet.Translator.ReadByte("Trade Slot", i);
                packet.Translator.ReadXORByte(guids1[i], 2);
                packet.Translator.ReadXORByte(guids1[i], 3);

                packet.Translator.WriteGuid("Gift Creator Guid", guids1[i], i);
            }
        }

        [Parser(Opcode.CMSG_ACCEPT_TRADE)]
        public static void HandleAcceptTrade(Packet packet)
        {
            packet.Translator.ReadUInt32("Unk UInt32 - Trade Window Is Showing?");
        }

        [Parser(Opcode.CMSG_BEGIN_TRADE)]
        public static void HandleBeginTrade(Packet packet)
        {
            if (ClientVersion.Build != ClientVersionBuild.V4_2_2_14545)
                return;

            var guid = packet.Translator.StartBitStream(5, 6, 4, 0, 2, 3, 7, 1);
            packet.Translator.ParseBitStream(guid, 5, 2, 3, 4, 1, 0, 6, 7);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_IGNORE_TRADE)]
        [Parser(Opcode.CMSG_BUSY_TRADE)]
        [Parser(Opcode.CMSG_CANCEL_TRADE)]
        [Parser(Opcode.CMSG_UNACCEPT_TRADE)]
        public static void HandleNullTrade(Packet packet)
        {
        }
    }
}
