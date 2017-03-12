using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct PetBattlePlayerUpdate
    {
        public ulong CharacterID;
        public int TrapAbilityID;
        public int TrapStatus;
        public ushort RoundTimeSecs;
        public List<PetBattlePetUpdate> Pets;
        public sbyte FrontPet;
        public byte InputFlags;
    }
}
