using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.BattlePay
{
    public unsafe struct StartPurchase
    {
        public ulong TargetCharacter;
        public uint ProductID;
        public uint ClientToken;

        [Parser(Opcode.CMSG_BATTLE_PAY_START_PURCHASE)]
        public static void HandleBattlePayStartPurchase(Packet packet)
        {
            packet.ReadInt32("ClientToken");
            packet.ReadInt32("ProductID");
            packet.ReadPackedGuid128("TargetCharacter");
        }
    }
}
