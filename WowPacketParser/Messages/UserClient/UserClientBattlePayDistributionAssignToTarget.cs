using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientBattlePayDistributionAssignToTarget
    {
        public ulong TargetCharacter;
        public ulong DistributionID;
        public uint ProductChoice;
        public uint ClientToken;

        [Parser(Opcode.CMSG_BATTLE_PAY_DISTRIBUTION_ASSIGN_TO_TARGET)]
        public static void HandleBattlePayDistributionAssignToTarget(Packet packet)
        {
            packet.ReadInt32("ClientToken");
            packet.ReadInt64("DistributionID");
            packet.ReadPackedGuid128("TargetCharacter");
            packet.ReadInt32("ProductChoice");
        }
    }
}
