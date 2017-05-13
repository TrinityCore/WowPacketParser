using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFile("FactionTemplate")]
    public sealed class FactionTemplateEntry
    {
        public ushort Faction;
        public ushort Flags;
        public ushort[] Enemies;
        public ushort[] Friends;
        public byte Mask;
        public byte FriendMask;
        public byte EnemyMask;
    }
}
