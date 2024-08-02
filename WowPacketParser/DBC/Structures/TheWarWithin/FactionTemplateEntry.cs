using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.TheWarWithin
{
    [DBFile("FactionTemplate")]
    public sealed class FactionTemplateEntry
    {
        [Index(true)]
        public uint ID;
        public ushort Faction;
        public int Flags;
        public byte FactionGroup;
        public byte FriendGroup;
        public byte EnemyGroup;
        [Cardinality(8)]
        public ushort[] Enemies = new ushort[8];
        [Cardinality(8)]
        public ushort[] Friend = new ushort[8];
    }
}
