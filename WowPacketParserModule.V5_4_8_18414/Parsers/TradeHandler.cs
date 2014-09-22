using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using TradeStatus548 = WowPacketParserModule.V5_4_8_18414.Enums.TradeStatus;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class TradeHandler
    {
        [Parser(Opcode.CMSG_ACCEPT_TRADE)]
        public static void HandleAcceptTrade(Packet packet)
        {
            packet.ReadUInt32("Unk UInt32 - Trade Window Is Showing?");
        }

        [Parser(Opcode.CMSG_BEGIN_TRADE)]
        [Parser(Opcode.CMSG_BUSY_TRADE)]
        [Parser(Opcode.CMSG_CANCEL_TRADE)]
        [Parser(Opcode.CMSG_IGNORE_TRADE)]
        [Parser(Opcode.CMSG_UNACCEPT_TRADE)]
        public static void HandleTradeNull(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_CLEAR_TRADE_ITEM)]
        public static void HandleClearTradeItem(Packet packet)
        {
             packet.ReadByte("Slot");
        }

        [Parser(Opcode.CMSG_INITIATE_TRADE)]
        public static void HandleInitiateTrade(Packet packet)
        {
            var guid = packet.StartBitStream(5, 1, 4, 2, 3, 7, 0, 6);
            packet.ParseBitStream(guid, 4, 6, 2, 0, 3, 7, 5, 1);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_SET_TRADE_GOLD)]
        public static void HandleSetTradeGold(Packet packet)
        {
            packet.ReadUInt64("Gold");
        }

        [Parser(Opcode.CMSG_SET_TRADE_ITEM)]
        public static void HandleTradeItem(Packet packet)
        {
            packet.ReadByte("Trade Slot"); // 18
            packet.ReadByte("Bag Slot"); // 17
            packet.ReadSByte("Bag"); // 16
        }

        [Parser(Opcode.SMSG_GAME_STORE_INGAME_BUY_FAILED)]
        public static void HandleGameStoreIngameBuyFailed(Packet packet)
        {
            var count = packet.ReadBits("count", 19);
            var unk44 = new uint[count];
            for (var i = 0; i < count; i++)
                unk44[i] = packet.ReadBits("unk44", 8, i);
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt64("unk24", i);
                packet.ReadInt32("unk54", i);
                var unk88 = packet.ReadInt32("unk88", i);
                packet.ReadWoWString("str", unk88, i);
                packet.ReadInt32("unk72", i);
            }
            packet.ReadInt32("unk16");
        }

        [Parser(Opcode.SMSG_TRADE_STATUS)]
        public static void HandleTradeStatus(Packet packet)
        {
            packet.ReadBit("Unk Bit");
            var status = packet.ReadEnum<TradeStatus548>("Status", 5);
            switch (status)
            {
                case TradeStatus548.TRADE_STATUS_PROPOSED:
                    var guid = packet.StartBitStream(6, 2, 1, 4, 7, 3, 0, 5);
                    packet.ParseBitStream(guid, 6, 2, 1, 7, 5, 4, 0, 3);
                    packet.WriteGuid("GUID", guid);
                    break;
                case TradeStatus548.TRADE_STATUS_FAILED:
                    if (!packet.ReadBit("!unk44")) // 44
                    {
                        packet.ReadInt32("unk36"); // 36
                        packet.ReadInt32("unk40"); // 40
                    }
                    break;
                case TradeStatus548.TRADE_STATUS_NOT_ENOUGH_CURRENCY:
                case TradeStatus548.TRADE_STATUS_CURRENCY_NOT_TRADABLE:
                    packet.ReadInt32("Unk28"); // 28
                    packet.ReadInt32("Unk24"); // 24
                    break;
                case TradeStatus548.TRADE_STATUS_WRONG_REALM:
                case TradeStatus548.TRADE_STATUS_NOT_ON_TAPLIST:
                    packet.ReadByte("Unk Byte"); // 32
                    break;
                case TradeStatus548.TRADE_STATUS_INITIATED:
                    packet.ReadInt32("Trade Id"); // 48
                    break;
            }
        }

        [Parser(Opcode.SMSG_TRADE_STATUS_EXTENDED)] // sub_73D690
        public static void HandleTradeStatusExtended(Packet packet)
        {
            packet.ReadInt32("Trade Id"); // 48
            packet.ReadInt32("Unk Int32 2"); // 40
            packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID"); // 16
            packet.ReadBoolean("Trader"); // 44
            packet.ReadInt64("Gold"); // 56
            packet.ReadInt32("Unk Slot 1"); // 68
            packet.ReadInt32("Unk Int32 5"); // 36
            packet.ReadInt32("Unk Slot 2"); // 64

            var count = packet.ReadBits("Count", 20);

            var guids1 = new byte[count][];
            var guids2 = new byte[count][];

            var isNotWrapped = new bool[count];

            for (int i = 0; i < count; i++)
            {
                guids1[i] = new byte[8];
                isNotWrapped[i] = packet.ReadBit("Is Not Wrapped", i); // 112
                guids1[i][2] = packet.ReadBit();

                if (isNotWrapped[i])
                {
                    guids2[i] = new byte[8];
                    guids2[i][3] = packet.ReadBit();
                    guids2[i][5] = packet.ReadBit();
                    guids2[i][1] = packet.ReadBit();
                    guids2[i][6] = packet.ReadBit();
                    guids2[i][0] = packet.ReadBit();
                    packet.ReadBit("Is Locked", i);
                    guids2[i][4] = packet.ReadBit();
                    guids2[i][7] = packet.ReadBit();
                    guids2[i][2] = packet.ReadBit();
                }
                guids1[i][0] = packet.ReadBit();
                guids1[i][4] = packet.ReadBit();
                guids1[i][7] = packet.ReadBit();
                guids1[i][3] = packet.ReadBit();
                guids1[i][6] = packet.ReadBit();
                guids1[i][1] = packet.ReadBit();
                guids1[i][5] = packet.ReadBit();
            }

            for (int i = 0; i < count; i++)
            {
                if (isNotWrapped[i]) // 112
                {
                    packet.ReadXORByte(guids2[i], 3);

                    packet.ReadInt32("Item Max Durability", i); // 88 ok

                    var count1 = packet.ReadInt32("count", i);
                    packet.ReadWoWString("str", count1, i);

                    packet.ReadInt32("Item Reforge Id", i); // 60

                    packet.ReadXORByte(guids2[i], 1);
                    packet.ReadXORByte(guids2[i], 5);
                    packet.ReadXORByte(guids2[i], 7);
                    packet.ReadXORByte(guids2[i], 6);
                    packet.ReadXORByte(guids2[i], 0);

                    packet.ReadInt32("Item Perm Enchantment Id", i); // 56
                    packet.ReadInt32("Item Durability", i); // 92 ok

                    packet.ReadXORByte(guids2[i], 2);

                    for (int j = 0; j < 3; j++)
                        packet.ReadInt32("Item Enchantment Id", i, j);

                    packet.ReadInt32("Item Random Property ID", i); // 84 ok
                    packet.ReadInt32("Item Spell Charges", i); // 76 ok
                    packet.ReadInt32("Item Suffix Factor", i); // 80 ok

                    packet.ReadXORByte(guids2[i], 4);

                    packet.WriteGuid("Creator Guid", guids2[i], i);
                }

                packet.ReadXORByte(guids1[i], 4);
                packet.ReadByte("Trade Slot", i);
                packet.ReadXORByte(guids1[i], 5);
                packet.ReadXORByte(guids1[i], 1);
                packet.ReadXORByte(guids1[i], 2);
                packet.ReadXORByte(guids1[i], 3);
                packet.ReadEntry<UInt32>(StoreNameType.Item, "Item Entry", i);
                packet.ReadXORByte(guids1[i], 7);
                packet.ReadXORByte(guids1[i], 0);
                packet.ReadUInt32("Item Count", i); // 46
                packet.ReadXORByte(guids1[i], 6);

                packet.WriteGuid("Gift Creator Guid", guids1[i], i);
            }
        }
    }
}
