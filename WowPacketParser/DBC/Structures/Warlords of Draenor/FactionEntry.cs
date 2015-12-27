namespace WowPacketParser.DBC.Structures
{
    public sealed class FactionEntry
    {
        public uint   ID;
        public int    ReputationIndex;
        public uint   ReputationRaceMask1;
        public uint   ReputationRaceMask2;
        public uint   ReputationRaceMask3;
        public uint   ReputationRaceMask4;
        public uint   ReputationClassMask1;
        public uint   ReputationClassMask2;
        public uint   ReputationClassMask3;
        public uint   ReputationClassMask4;
        public int    ReputationBase1;
        public int    ReputationBase2;
        public int    ReputationBase3;
        public int    ReputationBase4;
        public uint   ReputationFlags1;
        public uint   ReputationFlags2;
        public uint   ReputationFlags3;
        public uint   ReputationFlags4;
        public uint   ParentFactionID;
        public float  ParentFactionModIn;
        public float  ParentFactionModOut;
        public uint   ParentFactionCapIn;
        public uint   ParentFactionCapOut;
        public string Name_lang;
        public string Description_lang;
        public uint   Expansion;
        public uint   Flags;
        public uint   FriendshipRepID;
    }
}
