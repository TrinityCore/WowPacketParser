using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.BattlePet
{
    public unsafe struct DeletePet
    {
        public ulong BattlePetGUID;

        [Parser(Opcode.CMSG_CAGE_BATTLE_PET)]
        [Parser(Opcode.SMSG_BATTLE_PET_DELETED)]
        public static void HandleBattlePetDeletePet(Packet packet)
        {
            packet.ReadPackedGuid128("BattlePetGUID");
        }
    }
}
