namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("FactionTemplate")]
    public sealed class FactionTemplateEntry
    {
        public uint ID;
        public ushort Faction;
        public ushort Flags;
        public byte FactionGroup;
        public byte FriendGroup;
        public byte EnemyGroup;
        public ushort[] Enemies = new ushort[4];
        public ushort[] Friend = new ushort[4];
    }
}
