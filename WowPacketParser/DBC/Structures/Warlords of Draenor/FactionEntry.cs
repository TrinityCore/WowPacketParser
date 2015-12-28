using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    public sealed class FactionEntry
    {
        public uint   ID;
        public int    ReputationIndex;
        [StoragePresence(StoragePresenceOption.Include, ArraySize = 4)]
        public uint[] ReputationRaceMask;
        [StoragePresence(StoragePresenceOption.Include, ArraySize = 4)]
        public uint[] ReputationClassMask;
        [StoragePresence(StoragePresenceOption.Include, ArraySize = 4)]
        public int[] ReputationBase;
        [StoragePresence(StoragePresenceOption.Include, ArraySize = 4)]
        public uint[] ReputationFlags;
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
