using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.TheWarWithin
{
    [DBFile("CreatureFamily")]

    public sealed class CreatureFamilyEntry
    {
        [Index(true)]
        public uint ID;
        public string Name;
        public float MinScale;
        public sbyte MinScaleLevel;
        public float MaxScale;
        public sbyte MaxScaleLevel;
        public short PetFoodMask;
        public sbyte PetTalentType;
        public int IconFileID;
        [Cardinality(2)]
        public short[] SkillLine = new short[2];
    }
}
