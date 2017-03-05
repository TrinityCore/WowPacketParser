using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using TradeStatus530 = WowPacketParserModule.V5_3_0_16981.Enums.TradeStatus;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class TradeHandler
    {
        [Parser(Opcode.SMSG_TRADE_STATUS)]
        public static void HandleTradeStatus(Packet packet)
        {
            packet.ReadBit("Unk Bit");
            var status = packet.ReadBitsE<TradeStatus530>("Status", 5);
            switch (status)
            {
                case TradeStatus530.BeginTrade:
                    var guid = packet.StartBitStream(0, 3, 2, 4, 1, 6, 7, 5);
                    packet.ParseBitStream(guid, 5, 7, 3, 6, 4, 2, 0, 1);
                    packet.WriteGuid("GUID", guid);
                    break;
                case TradeStatus530.CloseWindow:
                    packet.ReadBit("Unk Bit");
                    packet.ReadInt32("Unk Int32");
                    packet.ReadInt32("Unk Int32");
                    break;
                case TradeStatus530.TradeCurrency:
                case TradeStatus530.CurrencyNotTradable:
                    packet.ReadInt32("Unk Int32 1");
                    packet.ReadInt32("Unk Int32 2");
                    break;
                case TradeStatus530.NotEligible:
                case TradeStatus530.OnlyConjured:
                    packet.ReadByte("Unk Byte");
                    break;
                case TradeStatus530.OpenWindow:
                    packet.ReadInt32("Trade Id");
                    break;
            }
        }
    }
}
