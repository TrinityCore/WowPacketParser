using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.TheWarWithin
{
    [DBFile("CreatureDifficulty")]

    public sealed class CreatureDifficultyEntry
    {
        [Index(true)]
        public uint ID;
        public int UnkMin;
        public int UnkMax;
        public ushort FactionTemplateID;
        public int ContentTuningID;
        [Cardinality(8)]
        public int[] Flags = new int[8];
        public uint CreatureID;
    }
}
