namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct PetBattleFullUpdate
    {
        public ushort WaitingForFrontPetsMaxSecs;
        public ushort PvpMaxRoundTime;
        public int CurRound;
        public uint NpcCreatureID;
        public uint NpcDisplayID;
        public sbyte CurPetBattleState;
        public byte ForfeitPenalty;
        public ulong InitialWildPetGUID;
        public bool IsPVP;
        public bool CanAwardXP;
        public PetBattlePlayerUpdate[/*2*/] Players;
        public PetBattleEnviroUpdate[/*3*/] Enviros;
    }
}
