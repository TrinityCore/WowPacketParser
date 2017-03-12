using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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
