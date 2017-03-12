using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct PetBattlePetUpdate
    {
        public ulong BattlePetGUID;
        public int SpeciesID;
        public int DisplayID;
        public int CollarID;
        public short Level;
        public short Xp;
        public int CurHealth;
        public int MaxHealth;
        public int Power;
        public int Speed;
        public int NpcTeamMemberID;
        public ushort BreedQuality;
        public ushort StatusFlags;
        public sbyte Slot;
        public string CustomName;
        public List<PetBattleActiveAbility> Abilities;
        public List<PetBattleActiveAura> Auras;
        public List<PetBattleActiveState> States;
    }
}
