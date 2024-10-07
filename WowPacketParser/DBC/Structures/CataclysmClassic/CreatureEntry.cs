using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.CataclysmClassic
{
    [DBFile("Creature")]
    public sealed class CreatureEntry
    {
        [Index(true)]
        public uint ID;
        public string Name;
        public string NameAlt;
        public string Title;
        public string TitleAlt;
        public sbyte Classification;
        public byte CreatureType;
        public ushort CreatureFamily;
        public byte StartAnimState;
        [Cardinality(4)]
        public int[] DisplayID = new int[4];
        [Cardinality(4)]
        public float[] DisplayProbability = new float[4];
        [Cardinality(3)]
        public int[] AlwaysItem = new int[3];
    }
}
