using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.BattlePet
{
    public unsafe struct DeletePetCheat
    {
        public ulong BattlePetGUID;

        [Parser(Opcode.CMSG_BATTLE_PET_DELETE_PET_CHEAT)]
        public static void HandleBattlePetDeletePet(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");
        }
    }
}
