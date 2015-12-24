namespace WowPacketParser.DBC.Structures
{
    public sealed class DifficultyEntry
    {
        public uint   ID;
        public uint   FallbackDifficultyID;
        public uint   InstanceType;
        public uint   MinPlayers;
        public uint   MaxPlayers;
        public int    OldEnumValue;
        public uint   Flags;
        public uint   ToggleDifficultyID;
        public uint   GroupSizeHealthCurveID;
        public uint   GroupSizeDmgCurveID;
        public uint   GroupSizeSpellPointsCurveID;
        public string NameLang;
        public uint   ItemBonusTreeModID;
        public uint   OrderIndex;
    }
}
