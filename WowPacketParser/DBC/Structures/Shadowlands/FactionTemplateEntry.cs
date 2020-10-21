using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.Shadowlands
{
    [DBFile("FactionTemplate")]
    public sealed class FactionTemplateEntry
    {
        [Index(true)]
        public uint ID;
        public ushort Faction;
        public ushort Flags;
        public byte FactionGroup;
        public byte FriendGroup;
        public byte EnemyGroup;
        [Cardinality(4)]
        public ushort[] Enemies = new ushort[4];
        [Cardinality(4)]
        public ushort[] Friend = new ushort[4];
    }
}
