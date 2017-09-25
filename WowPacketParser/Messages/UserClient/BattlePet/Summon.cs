using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.BattlePet
{
    public unsafe struct Summon
    {
        public ulong BattlePetGUID;

        [Parser(Opcode.CMSG_BATTLE_PET_SUMMON)]
        public static void HandleBattlePetSummon(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");
        }
    }
}
