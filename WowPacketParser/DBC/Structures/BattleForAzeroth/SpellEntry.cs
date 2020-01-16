namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("Spell")]

    public sealed class SpellEntry
    {
        public uint ID;
        public string NameSubtext;
        public string Description;
        public string AuraDescription;
    }
}
