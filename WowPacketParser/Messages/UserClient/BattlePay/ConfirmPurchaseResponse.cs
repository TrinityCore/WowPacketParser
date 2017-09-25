using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.BattlePay
{
    public unsafe struct ConfirmPurchaseResponse
    {
        public ulong ClientCurrentPriceFixedPoint;
        public bool ConfirmPurchase;
        public uint ServerToken;

        [Parser(Opcode.CMSG_BATTLE_PAY_CONFIRM_PURCHASE_RESPONSE)]
        public static void HandleBattlePayConfirmPurchaseResponse(Packet packet)
        {
            packet.ReadBit("ConfirmPurchase");
            packet.ReadInt32("ServerToken");
            packet.ReadInt64("ClientCurrentPriceFixedPoint");
        }
    }
}
