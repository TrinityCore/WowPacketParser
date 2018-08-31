using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("FactionTemplate")]
    public sealed class FactionTemplateEntry
    {
        public ushort Faction;
        public ushort Flags;
        public byte FactionGroup;
        public byte FriendGroup;
        public byte EnemyGroup;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] Enemies;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] Friend;
    }
}
