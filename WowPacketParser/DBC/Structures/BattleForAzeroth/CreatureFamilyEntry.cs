namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("CreatureFamily")]

    public sealed class CreatureFamilyEntry
    {
        public uint ID;
        public string Name;
        public float MinScale;
        public sbyte MinScaleLevel;
        public float MaxScale;
        public sbyte MaxScaleLevel;
        public short PetFoodMask;
        public sbyte PetTalentType;
        public int IconFileID;
        public short[] SkillLine = new short[2];
    }
}
