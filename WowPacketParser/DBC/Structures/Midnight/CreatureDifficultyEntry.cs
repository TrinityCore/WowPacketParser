using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.Midnight
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
        [Cardinality(9)]
        public int[] Flags = new int[9];
        [NonInlineRelation(typeof(uint))]
        public int CreatureID;
    }
}
