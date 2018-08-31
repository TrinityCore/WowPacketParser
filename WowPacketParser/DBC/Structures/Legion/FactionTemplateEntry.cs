using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures.Legion
{
    [DBFile("FactionTemplate")]
    public sealed class FactionTemplateEntry
    {
        public ushort Faction;
        public ushort Flags;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] Enemies;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] Friend;
        public byte FactionGroup;
        public byte FriendGroup;
        public byte EnemyGroup;
    }
}
