using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures.Legion
{
    [DBFile("CreatureDifficulty")]

    public sealed class CreatureDifficultyEntry
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public int[] Flags;
        public ushort FactionTemplateID;
        public sbyte Expansion;
        public sbyte MinLevel;
        public sbyte MaxLevel;
        public int CreatureID;
    }
}
