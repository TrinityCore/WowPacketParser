namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("Difficulty")]
    public sealed class DifficultyEntry
    {
        public string Name;
        public byte InstanceType;
        public byte OrderIndex;
        public sbyte OldEnumValue;
        public byte FallbackDifficultyID;
        public byte MinPlayers;
        public byte MaxPlayers;
        public ushort Flags;
        public byte ItemContext;
        public byte ToggleDifficultyID;
        public ushort GroupSizeHealthCurveID;         // unproven
        public ushort GroupSizeDmgCurveID;            // unproven
        public ushort GroupSizeSpellPointsCurveID;    // unproven
    }
}
