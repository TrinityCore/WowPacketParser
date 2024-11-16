using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.CataclysmClassic
{
    [DBFile("CreatureDifficulty")]
    public sealed class CreatureDifficultyEntry
    {
        [Index(true)]
        public uint ID;
        public sbyte ExpansionID;
        public sbyte MinLevel;
        public sbyte MaxLevel;
        public ushort FactionTemplateID;
        public int ContentTuningID;
        [Cardinality(8)]
        public int[] Flags = new int[8];
        [NonInlineRelation(typeof(uint))]
        public int CreatureID;
    }
}
