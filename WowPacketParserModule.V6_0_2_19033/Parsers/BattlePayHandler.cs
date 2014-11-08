using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class BattlePayHandler
    {
        [Parser(Opcode.SMSG_BATTLE_PAY_GET_PURCHASE_LIST_RESPONSE)]
        public static void HandleBattlePayGetPurchaseListResponse(Packet packet)
        {
            packet.ReadUInt32("Result");

            var int6 = packet.ReadUInt32("BattlePayPurchaseCount");

            for (int i = 0; i < int6; i++)
            {
                packet.ReadInt64("PurchaseID", i);
                packet.ReadInt32("Status", i);
                packet.ReadInt32("ResultCode", i);
                packet.ReadInt32("ProductID", i);

                packet.ResetBitReader();

                var bits20 = packet.ReadBits(8);
                packet.ReadWoWString("WalletName", bits20, i);
            }
        }
    }
}