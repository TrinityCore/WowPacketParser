namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLFGSearchResultPlayer
    {
        public ulong Guid;
        public uint ChangeMask;
        public byte Level;
        public byte ChrClass;
        public byte Race;
        public uint Armor;
        public uint SpellDamage;
        public uint PlusHealing;
        public uint CritMelee;
        public uint CritRanged;
        public uint CritSpell;
        public float Mp5;
        public float Mp5InCombat;
        public uint AttackPower;
        public uint Agility;
        public uint MaxHealth;
        public uint MaxMana;
        public uint BossKills;
        public float GearRating;
        public float AvgItemLevel;
        public uint DefenseRating;
        public uint DodgeRating;
        public uint BlockRating;
        public uint ParryRating;
        public uint HasteRating;
        public float Expertise;
        public uint SpecID;
        public uint VirtualRealmAddress;
        public uint NativeRealmAddress;
        public string Comment;
        public bool IsLeader;
        public ulong PartyGuid;
        public byte RolesDesired;
        public uint Area;
        public byte Status;
        public ulong InstanceID;
        public uint InstanceCompletedMask;
    }
}
