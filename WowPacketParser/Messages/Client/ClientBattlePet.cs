namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBattlePet
    {
        public ulong BattlePetGUID;
        public int SpeciesID;
        public int CreatureID;
        public int DisplayID;
        public ushort BreedID;
        public ushort Level;
        public ushort Xp;
        public ushort BattlePetDBFlags;
        public int Power;
        public int Health;
        public int MaxHealth;
        public int Speed;
        public string CustomName;
        public ClientBattlePetOwnerInfo? OwnerInfo; // Optional
        public bool NoRename;
        public byte BreedQuality;
    }
}
