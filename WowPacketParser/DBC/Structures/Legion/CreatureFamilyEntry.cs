using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures.Legion
{
    [DBFile("CreatureFamily")]

    public sealed class CreatureFamilyEntry
    {
        public string Name;
        public float MinScale;
        public float MaxScale;
        public int IconFileID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public short[] SkillLine;
        public short PetFoodMask;
        public byte MinScaleLevel;
        public byte MaxScaleLevel;
        public byte PetTalentType;
    }
}
