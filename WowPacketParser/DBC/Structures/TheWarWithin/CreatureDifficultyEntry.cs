using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.TheWarWithin
{
    [DBFile("CreatureDifficulty")]
    public sealed class CreatureDifficultyEntry
    {
        [Index(true)]
        public uint ID;
        public int Unknown901_0;
        public int Unknown901_1;
        public ushort FactionTemplateID;
        public int ContentTuningID;
        [Cardinality(8)]
        public int[] Flags = new int[8];
        [NonInlineRelation(typeof(uint))]
        public int CreatureID;
    }
}
