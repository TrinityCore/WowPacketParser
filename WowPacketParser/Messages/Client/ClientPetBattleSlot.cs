namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPetBattleSlot
    {
        public ulong BattlePetGUID;
        public int CollarID;
        public byte SlotIndex;
        public bool Locked;
    }
}
