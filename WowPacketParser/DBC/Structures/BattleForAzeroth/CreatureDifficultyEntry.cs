using System.Runtime.InteropServices;

namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("CreatureDifficulty")]

    public sealed class CreatureDifficultyEntry
    {
        public sbyte Expansion;
        public sbyte MinLevel;
        public sbyte MaxLevel;
        public ushort FactionTemplateID;
        public int ContentTuningID;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public int[] Flags;
        public int CreatureID;
    }
}
