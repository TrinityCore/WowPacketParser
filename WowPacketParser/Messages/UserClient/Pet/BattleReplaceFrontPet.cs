using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.Pet
{
    public unsafe struct BattleReplaceFrontPet
    {
        public sbyte FrontPet;

        [Parser(Opcode.CMSG_PET_BATTLE_REPLACE_FRONT_PET)]
        public static void HandlePetBattleReplaceFrontPet(Packet packet)
        {
            packet.ReadSByte("FrontPet");
        }
    }
}
