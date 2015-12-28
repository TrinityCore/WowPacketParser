using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    public sealed class FactionTemplateEntry
    {
        public uint ID;
        public uint Faction;
        public uint Flags;
        public uint Mask;
        public uint FriendMask;
        public uint EnemyMask;
        [StoragePresence(StoragePresenceOption.Include, ArraySize = 4)]
        public uint[] Enemies;
        [StoragePresence(StoragePresenceOption.Include, ArraySize = 4)]
        public uint[] Friends;
    }
}
