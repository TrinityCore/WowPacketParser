using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.Pet
{
    public unsafe struct BattleInput
    {
        public PetBattleInput MsgData;

        [Parser(Opcode.CMSG_PET_BATTLE_INPUT)]
        public static void HandlePetBattleInput(Packet packet)
        {
            readPetBattleInput(packet, "PetBattleInput");
        }

        public static void readPetBattleInput(Packet packet, params object[] idx)
        {
            packet.ReadByte("MoveType");
            packet.ReadSByte("NewFrontPet");
            packet.ReadByte("DebugFlags");
            packet.ReadByte("BattleInterrupted");

            packet.ReadInt32("AbilityID");
            packet.ReadInt32("Round");

            packet.ResetBitReader();

            packet.ReadBit("IgnoreAbandonPenalty");
        }
    }
}
