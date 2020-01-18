namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("Creature")]
    public sealed class CreatureEntry
    {
        public uint ID;
        public string Name;
        public string NameAlt;
        public string Title;
        public string TitleAlt;
        public sbyte Classification;
        public byte CreatureType;
        public ushort CreatureFamily;
        public byte StartAnimState;
        public uint[] DisplayID = new uint[4];
        public float[] DisplayProbability = new float[4];
        public uint[] AlwaysItem = new uint[3];
    }
}
