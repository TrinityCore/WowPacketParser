using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFile("CreatureFamily")]

    public sealed class CreatureFamilyEntry
    {
        public float MinScale;
        public float MaxScale;
        public string Name;
        public uint IconFileDataID;
        public ushort[] SkillLine;
        public ushort PetFoodMask;
        public byte MinScaleLevel;
        public byte MaxScaleLevel;
        public byte PetTalentType;
    }
}
