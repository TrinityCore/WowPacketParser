using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct ClearTradeItem
    {
        public byte TradeSlot;

        [Parser(Opcode.CMSG_CLEAR_TRADE_ITEM)]
        public static void HandleClearTradeItem(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596) && ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_4_15595)) // Need correct versions
                packet.ReadInt32("Slot");
            else
                packet.ReadByte("Slot");
        }
    }
}
