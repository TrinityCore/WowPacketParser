namespace WowPacketParser.DBC.Structures
{
    public sealed class CreatureFamilyEntry
    {
        public uint   ID;
        public float  MinScale;
        public uint   MinScaleLevel;
        public float  MaxScale;
        public uint   MaxScaleLevel;
        public uint   SkillLine1;
        public uint   SkillLine2;
        public uint   PetFoodMask;
        public uint   PetTalentType;
        public uint   CategoryEnumID;
        public string Name_lang;
        public string IconFile;
    }
}
