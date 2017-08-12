using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFile("Creature")]

    public sealed class CreatureEntry
    {
        public uint[] Item;
        public uint Mount;
        public uint[] DisplayID;
        public float[] DisplayIdProbability;
        public string Name;
        public string FemaleName;
        public string SubName;
        public string FemaleSubName;
        public byte Type;
        public byte Family;
        public byte Classification;
        public byte InhabitType;
    }
}
