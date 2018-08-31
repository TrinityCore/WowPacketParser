using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("CreatureFamily")]

    public sealed class CreatureFamilyEntry
    {
        public string Name;
        public float MinScale;
        public sbyte MinScaleLevel;
        public float MaxScale;
        public sbyte MaxScaleLevel;
        public short PetFoodMask;
        public sbyte PetTalentType;
        public int IconFileID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public short[] SkillLine;
    }
}
