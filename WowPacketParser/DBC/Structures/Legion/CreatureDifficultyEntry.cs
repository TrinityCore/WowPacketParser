using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFile("CreatureDifficulty")]

    public sealed class CreatureDifficultyEntry
    {
        public uint CreatureID;
        public uint[] Flags;
        public ushort FactionTemplateID;
        public sbyte Expansion;
        public sbyte MinLevel;
        public sbyte MaxLevel;
    }
}
