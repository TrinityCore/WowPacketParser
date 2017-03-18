using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientBattlePetSummon
    {
        public ulong BattlePetGUID;

        [Parser(Opcode.CMSG_BATTLE_PET_SUMMON)]
        public static void HandleBattlePetSummon(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");
        }
    }
}
