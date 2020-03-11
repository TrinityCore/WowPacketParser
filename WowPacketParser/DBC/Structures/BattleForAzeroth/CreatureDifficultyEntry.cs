namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("CreatureDifficulty")]

    public sealed class CreatureDifficultyEntry
    {
        public uint ID;
        public sbyte Expansion;
        public sbyte MinLevel;
        public sbyte MaxLevel;
        public ushort FactionTemplateID;
        public int ContentTuningID;
        public int[] Flags = new int[7];
        public int CreatureID;
    }
}
