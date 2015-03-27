using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class TradeHandler
    {
        [Parser(Opcode.CMSG_INITIATE_TRADE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleInitiateTrade(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_INITIATE_TRADE, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleInitiateTrade434(Packet packet)
        {
            var guid = packet.StartBitStream(0, 3, 5, 1, 4, 6, 7, 2);
            packet.ParseBitStream(guid, 7, 4, 3, 5, 1, 2, 6, 0);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_SET_TRADE_ITEM, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTradeItem(Packet packet)
        {
            packet.ReadByte("Trade Slot");
            packet.ReadSByte("Bag");
            packet.ReadByte("Bag Slot");
        }

        [Parser(Opcode.CMSG_SET_TRADE_ITEM, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTradeItem434(Packet packet)
        {
            packet.ReadByte("Bag Slot");
            packet.ReadByte("Trade Slot");
            packet.ReadSByte("Bag");
        }

        [Parser(Opcode.CMSG_CLEAR_TRADE_ITEM)]
        public static void HandleClearTradeItem(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596) && ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_4_15595)) // Need correct versions
                packet.ReadInt32("Slot");
            else
                packet.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_SET_TRADE_GOLD)]
        public static void HandleTradeGold(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596)) // Need correct version
                packet.ReadUInt64("Gold");
            else
                packet.ReadUInt32("Gold");
        }

        [Parser(Opcode.SMSG_TRADE_STATUS, ClientVersionBuild.Zero, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleTradeStatus(Packet packet)
        {
            var status = packet.ReadUInt32E<TradeStatus>("Status");
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

        [Parser(Opcode.SMSG_TRADE_STATUS, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleTradeStatus422(Packet packet)
        {
            var guid = new byte[8];
            guid[2] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            packet.ReadBit("Unk Bit");
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[3] = packet.ReadBit();

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);

            packet.ReadUInt32("Unk 1");
            packet.ReadByte("Unk 2");

            packet.ReadXORByte(guid, 7);

            packet.ReadInt32("Unk 3");
            packet.ReadUInt32("Unk 4");

            packet.ReadXORByte(guid, 6);

            packet.ReadUInt32("Unk 5");
            packet.ReadByte("Unk 6");

            packet.ReadXORByte(guid, 4);

            packet.ReadUInt32("Unk 7");

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);

            packet.ReadUInt32("Unk 8");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_TRADE_STATUS, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTradeStatus434(Packet packet)
        {
            packet.ReadBit("Unk Bit");
            var status = packet.ReadBitsE<TradeStatus434>("Status", 5);

            switch (status)
            {
                case TradeStatus434.BeginTrade:
                    var guid = packet.StartBitStream(2, 4, 6, 0, 1, 3, 7, 5);
                    packet.ParseBitStream(guid, 4, 1, 2, 3, 0, 7, 6, 5);
                    packet.WriteGuid("GUID", guid);
                    break;
                case TradeStatus434.CloseWindow:
                    packet.ReadBit("Unk Bit");
                    packet.ReadInt32("Unk Int32");
                    packet.ReadInt32("Unk Int32");
                    break;
                case TradeStatus434.TradeCurrency:
                case TradeStatus434.CurrencyNotTradable:
                    packet.ReadInt32("Unk Int32 1");
                    packet.ReadInt32("Unk Int32 2");
                    break;
                case TradeStatus434.NotEligible:
                case TradeStatus434.OnlyConjured:
                    packet.ReadByte("Unk Byte");
                    break;
                case TradeStatus434.OpenWindow:
                    packet.ReadInt32("Trade Id");
                    break;
            }
        }

        [Parser(Opcode.SMSG_TRADE_STATUS_EXTENDED, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6a_13623)]
        public static void HandleTradeStatusExtended(Packet packet)
        {
            packet.ReadByte("Trader");
            packet.ReadUInt32("Trade Id");
            packet.ReadUInt32("Unk Slot 1");
            packet.ReadUInt32("Unk Slot 2");
            packet.ReadUInt32("Gold");
            packet.ReadInt32<SpellId>("Spell ID");

            while (packet.CanRead())
            {
                var slot = packet.ReadByte("Slot Index");
                packet.ReadUInt32<ItemId>("Item Entry", slot);
                packet.ReadUInt32("Item Display ID", slot);
                packet.ReadUInt32("Item Count", slot);
                packet.ReadUInt32("Item Wrapped", slot);
                packet.ReadGuid("Item Gift Creator GUID", slot);
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

        [Parser(Opcode.SMSG_TRADE_STATUS_EXTENDED, ClientVersionBuild.V4_0_6a_13623, ClientVersionBuild.V4_2_2_14545)]
        public static void HandleTradeStatusExtended406(Packet packet)
        {
            packet.ReadUInt32("Unk 1");
            packet.ReadUInt32("Unk 2");
            packet.ReadBool("Unk (Has slots?)");
            packet.ReadUInt32("Unk 3");
            packet.ReadUInt32("Unk 4");
            packet.ReadUInt32("Trade Id?");
            var slots = packet.ReadUInt32("Trade Slots");
            packet.ReadUInt64("Gold");
            packet.ReadUInt32("Unk 6");

            for (var i = 0; i < slots; ++i)
            {
                packet.ReadUInt32("Unk1", i);
                packet.ReadGuid("Item Creator GUID", i);
                packet.ReadUInt32("Item Perm Enchantment Id", i);
                packet.ReadUInt32<ItemId>("Item Entry", i);
                packet.ReadUInt32("Item Enchantment Id", i, 0);
                packet.ReadUInt32("Item Durability", i);
                packet.ReadUInt32("Unk2", i);
                packet.ReadByte("Unk Byte", i);
                packet.ReadGuid("Unk Guid: (Item Gift Creator GUID?)", i);
                packet.ReadUInt32("Unk3", i);
                packet.ReadByte("Slot Index", i);
                packet.ReadUInt32("Item Max Durability", i);
                packet.ReadUInt32("Item Count", i);
                packet.ReadUInt32("Unk4", i);
                packet.ReadUInt32("Item Temporal Enchantment", i);
                packet.ReadUInt32("Item Enchantment Id", i, 1);
                packet.ReadUInt32("Item Enchantment Id", i, 2);
                packet.ReadUInt32("Unk5", i);

                // Probably the unks are one of these (+ that "item Temporal Enchantment")
                //packet.ReadUInt32("Item Display ID", slot);

                //packet.ReadUInt32("Item Wrapped", slot);
                //packet.ReadInt32("Item Spell Charges", slot);
                //packet.ReadInt32("Item Suffix Factor", slot);
                //packet.ReadInt32("Item Random Property ID", slot);
                //packet.ReadUInt32("Item Lock ID", slot);
            }
        }

        [Parser(Opcode.SMSG_TRADE_STATUS_EXTENDED, ClientVersionBuild.V4_2_2_14545, ClientVersionBuild.V4_3_0_15005)]
        public static void HandleTradeStatusExtended422(Packet packet)
        {
            packet.AsHex();
            packet.ReadInt32("Unk 1");
            packet.ReadInt32("Unk 2");
            packet.ReadInt32("Unk 3");
            packet.ReadInt32("Unk 4");
            packet.ReadUInt64("Gold");
            packet.ReadByte("Trader?");
            var count = packet.ReadInt32("Unk Count");
            packet.ReadInt32("Unk 7");
            packet.ReadInt32("Unk 8");

            var guids1 = new byte[count][];
            var guids2 = new byte[count][];
            for (var i = 0; i < count; ++i)
            {
                guids1[i] = new byte[8];
                guids2[i] = new byte[8];
            }

            for (var i = 0; i < count; ++i)
            {
                guids1[i][0] = packet.ReadBit();
                guids1[i][5] = packet.ReadBit();
                guids1[i][7] = packet.ReadBit();
                guids1[i][1] = packet.ReadBit();
                guids1[i][6] = packet.ReadBit();

                guids2[i][5] = packet.ReadBit();
                guids2[i][3] = packet.ReadBit();
                guids2[i][0] = packet.ReadBit();
                guids2[i][6] = packet.ReadBit();
                guids2[i][2] = packet.ReadBit();
                guids2[i][4] = packet.ReadBit();
                guids2[i][1] = packet.ReadBit();

                guids1[i][3] = packet.ReadBit();

                guids2[i][7] = packet.ReadBit();

                guids1[i][2] = packet.ReadBit();
                guids1[i][4] = packet.ReadBit();
            }

            for (var i = 0; i < count; ++i)
            {
                packet.ReadInt32("Unk 1", i);

                packet.ReadXORByte(guids2[i], 0);
                packet.ReadXORByte(guids2[i], 3);
                packet.ReadXORByte(guids2[i], 4);

                packet.ReadInt32("Unk 2", i);

                packet.ReadXORByte(guids1[i], 7);

                packet.ReadInt32("Item Id", i);
                packet.ReadInt32("Unk 4", i);
                packet.ReadInt32("Unk 5", i);

                packet.ReadXORByte(guids2[i], 2);
                packet.ReadXORByte(guids2[i], 5);

                packet.ReadInt32("Unk 6", i);

                packet.ReadXORByte(guids1[i], 1);
                packet.ReadXORByte(guids2[i], 6);
                packet.ReadXORByte(guids1[i], 0);

                packet.ReadInt32("Unk 7", i);
                packet.ReadUInt32("Unk 8", i);
                packet.ReadInt32("Unk 9", i);

                packet.ReadXORByte(guids1[i], 5);

                packet.ReadInt32("Unk 10", i);

                packet.ReadXORByte(guids1[i], 6);
                packet.ReadXORByte(guids2[i], 7);

                packet.ReadInt32("Unk 11", i);
                packet.ReadByte("Unk 12", i);

                packet.ReadXORByte(guids2[i], 1);

                packet.ReadInt32("Unk 13", i);
                packet.ReadInt32("Unk 14", i);
                packet.ReadByte("Unk 15", i);

                packet.ReadXORByte(guids1[i], 4);
                packet.ReadXORByte(guids1[i], 2);

                packet.ReadInt32("Unk 16", i);

                packet.ReadXORByte(guids1[i], 3);

                packet.WriteGuid("Item Creator Guid", guids1[i], i);
                packet.WriteGuid("Item Gift Creator Guid", guids2[i], i);
            }
        }

        [Parser(Opcode.SMSG_TRADE_STATUS_EXTENDED, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleTradeStatusExtended434(Packet packet)
        {
            packet.ReadInt32("Trade Id");
            packet.ReadInt32("Unk Int32 2");
            packet.ReadInt64("Gold");
            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadInt32("Unk Slot 1");
            packet.ReadInt32("Unk Int32 5");
            packet.ReadBool("Trader");
            packet.ReadInt32("Unk Slot 2");

            var count = packet.ReadBits("Count", 22);

            var guids1 = new byte[count][];
            var guids2 = new byte[count][];

            var isNotWrapped = new bool[count];

            for (int i = 0; i < count; ++i)
            {
                guids1[i] = new byte[8];
                guids1[i][7] = packet.ReadBit();
                guids1[i][1] = packet.ReadBit();
                isNotWrapped[i] = packet.ReadBit("Is Not Wrapped", i);
                guids1[i][3] = packet.ReadBit();

                if (isNotWrapped[i])
                {
                    guids2[i] = new byte[8];
                    guids2[i][7] = packet.ReadBit();
                    guids2[i][1] = packet.ReadBit();
                    guids2[i][4] = packet.ReadBit();
                    guids2[i][6] = packet.ReadBit();
                    guids2[i][2] = packet.ReadBit();
                    guids2[i][3] = packet.ReadBit();
                    guids2[i][5] = packet.ReadBit();
                    packet.ReadBit("Is Locked", i);
                    guids2[i][0] = packet.ReadBit();
                }

                guids1[i][6] = packet.ReadBit();
                guids1[i][4] = packet.ReadBit();
                guids1[i][2] = packet.ReadBit();
                guids1[i][0] = packet.ReadBit();
                guids1[i][5] = packet.ReadBit();
            }

            for (int i = 0; i < count; ++i)
            {
                if (isNotWrapped[i])
                {
                    packet.ReadXORByte(guids2[i], 1);

                    packet.ReadInt32("Item Perm Enchantment Id", i);

                    for (int j = 0; j < 3; ++j)
                        packet.ReadInt32("Item Enchantment Id", i, j);

                    packet.ReadInt32("Item Max Durability", i);

                    packet.ReadXORByte(guids2[i], 6);
                    packet.ReadXORByte(guids2[i], 2);
                    packet.ReadXORByte(guids2[i], 7);
                    packet.ReadXORByte(guids2[i], 4);

                    packet.ReadInt32("Item Reforge Id", i);
                    packet.ReadInt32("Item Durability", i);
                    packet.ReadInt32("Item Random Property ID", i);

                    packet.ReadXORByte(guids2[i], 3);

                    packet.ReadInt32("Unk Int32 7", i);

                    packet.ReadXORByte(guids2[i], 0);

                    packet.ReadInt32("Item Spell Charges", i);
                    packet.ReadInt32("Item Suffix Factor", i);

                    packet.ReadXORByte(guids2[i], 5);

                    packet.WriteGuid("Creator Guid", guids2[i], i);
                }

                packet.ReadXORByte(guids1[i], 6);
                packet.ReadXORByte(guids1[i], 1);
                packet.ReadXORByte(guids1[i], 7);
                packet.ReadXORByte(guids1[i], 4);
                packet.ReadUInt32<ItemId>("Item Entry", i);
                packet.ReadXORByte(guids1[i], 0);
                packet.ReadUInt32("Item Count", i);
                packet.ReadXORByte(guids1[i], 5);
                packet.ReadByte("Trade Slot", i);
                packet.ReadXORByte(guids1[i], 2);
                packet.ReadXORByte(guids1[i], 3);

                packet.WriteGuid("Gift Creator Guid", guids1[i], i);
            }
        }

        [Parser(Opcode.CMSG_ACCEPT_TRADE)]
        public static void HandleAcceptTrade(Packet packet)
        {
            packet.ReadUInt32("Unk UInt32 - Trade Window Is Showing?");
        }

        [Parser(Opcode.CMSG_BEGIN_TRADE)]
        public static void HandleBeginTrade(Packet packet)
        {
            if (ClientVersion.Build != ClientVersionBuild.V4_2_2_14545)
                return;

            var guid = packet.StartBitStream(5, 6, 4, 0, 2, 3, 7, 1);
            packet.ParseBitStream(guid, 5, 2, 3, 4, 1, 0, 6, 7);
            packet.WriteGuid("Guid", guid);
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
